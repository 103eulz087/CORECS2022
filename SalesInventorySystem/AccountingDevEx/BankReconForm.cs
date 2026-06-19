using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.AccountingDevEx
{
    // ================================================================
    // BankReconForm v2 — Layout completely rebuilt
    //
    // ROOT CAUSES OF v1 LAYOUT BUGS (from screenshot):
    //
    //   Bug 1 — Dock stacking order wrong.
    //     WinForms DockStyle.Top stacks in REVERSE add order
    //     (last added = topmost on screen). In v1 the gold line was
    //     added last so it appeared first, pushing the banner down.
    //     Fix: use ONE TableLayoutPanel as the master container.
    //     Every row is explicit — no Dock stacking ambiguity.
    //
    //   Bug 2 — Title label floating loose on form.
    //     lblTitle was inside pnlBanner but pnlBanner had no Dock=Fill,
    //     so it collapsed and the label drew on the form surface.
    //     Fix: all band panels use Dock=Fill inside their TLP row.
    //
    //   Bug 3 — Grid completely blank.
    //     gridItems had no Dock=Fill, so it had Size(0,0).
    //     Fix: gridItems.Dock = DockStyle.Fill.
    //
    //   Bug 4 — Toolbar buttons overlapping / orange blocks.
    //     Buttons used absolute Bounds on a DockStyle.Bottom panel.
    //     Absolute positioning inside a docked panel is unreliable.
    //     Fix: FlowLayoutPanel inside the toolbar row — buttons
    //     auto-size to their text, no manual coordinates needed.
    //
    // LAYOUT ARCHITECTURE (safe for VS2017 + DevExpress 19.x):
    //   Form
    //   └─ TableLayoutPanel (master, Dock=Fill, 5 rows)
    //      ├─ Row 0 (52px)  — pnlBanner  (title + branch badge)
    //      ├─ Row 1 (50px)  — pnlFilter  (branch/account/date/load)
    //      ├─ Row 2 (62px)  — pnlHeader  (book bal, bank stmt bal)
    //      ├─ Row 3 (*)     — SplitContainerControl (grid | summary)
    //      └─ Row 4 (44px)  — pnlToolbar (flow layout of buttons)
    // ================================================================

    public partial class BankReconForm : XtraForm
    {
        // ── Colours ──────────────────────────────────────────────────
        private static readonly Color C_DARK = Color.FromArgb(15, 17, 23);
        private static readonly Color C_SURFACE = Color.FromArgb(24, 28, 39);
        private static readonly Color C_CARD = Color.FromArgb(30, 35, 51);
        private static readonly Color C_BORDER = Color.FromArgb(42, 48, 80);
        private static readonly Color C_GOLD = Color.FromArgb(201, 168, 76);
        private static readonly Color C_TEXT = Color.FromArgb(232, 230, 224);
        private static readonly Color C_MUTED = Color.FromArgb(136, 145, 170);
        private static readonly Color C_DR = Color.FromArgb(107, 174, 214);
        private static readonly Color C_CR = Color.FromArgb(252, 141, 89);
        private static readonly Color C_OK = Color.FromArgb(116, 196, 118);
        private static readonly Color C_ERR = Color.FromArgb(251, 107, 75);

        private static readonly Font F_MONO = new Font("Courier New", 9f);
        private static readonly Font F_SMALL = new Font("Segoe UI", 8.5f);
        private static readonly Font F_BOLD = new Font("Segoe UI", 9f, FontStyle.Bold);
        private static readonly Font F_TITLE = new Font("Georgia", 14f, FontStyle.Bold);

        // ── State ─────────────────────────────────────────────────────
        private string _branch = Login.assignedBranch;
        private string _account = "";
        private DateTime _period = DateTime.Today;
        private decimal _bookBal = 0m;
        private decimal _bankBal = 0m;
        private int _selID = 0;

        // ── Controls (kept as fields so event handlers can reach them) ─
        private SearchLookUpEdit cmbBranch, cmbAccount;
        private DateEdit dtPeriod;
        private LabelControl lblBookBal;
        private TextEdit txtBankBal;
        private SimpleButton btnSaveHeader;
        private DevExpress.XtraGrid.GridControl gridItems;
        private GridView viewItems;
        // Summary labels
        private LabelControl lblBankStmt, lblDIT, lblOC, lblAdjBank;
        private LabelControl lblBookSide, lblBCM, lblBDM, lblAdjBook;
        private LabelControl lblDiff;
        // Toolbar buttons
        private SimpleButton btnAdd, btnEdit, btnResolve,
                             btnDelete, btnAutoMatch, btnPrint;
        private LabelControl lblStatus;

        public BankReconForm()
        {
            this.Text = "Bank Reconciliation";
            this.BackColor = C_DARK;
            this.ForeColor = C_TEXT;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1000, 600);
            InitializeComponent();
            BuildUI();
            WireEvents();
            PopulateLookups();
            SetDefaultPeriod();
        }

        // ================================================================
        // MASTER TABLE LAYOUT — single root container, 5 rows
        // This eliminates all Dock stacking order issues.
        // ================================================================
        private void BuildUI()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = C_DARK,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52f));  // Row 0 — banner
            //tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52f));  // Row 1 — filter
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 142f));  // Row 1 — filter
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 64f));  // Row 2 — header
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));  // Row 3 — body
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 44f));  // Row 4 — toolbar

            tlp.Controls.Add(BuildBanner(), 0, 0);
            tlp.Controls.Add(BuildFilter(), 0, 1);
            tlp.Controls.Add(BuildHeader(), 0, 2);
            tlp.Controls.Add(BuildBody(), 0, 3);
            tlp.Controls.Add(BuildToolbar(), 0, 4);

            this.Controls.Add(tlp);
        }

        // ── Row 0: Banner ─────────────────────────────────────────────
        private Panel BuildBanner()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = C_SURFACE };

            // Gold left accent
            var accent = new Panel
            {
                Dock = DockStyle.Left,
                Width = 3,
                BackColor = C_GOLD
            };

            var lblTitle = new LabelControl
            {
                Text = "Bank Reconciliation",
                Font = F_TITLE,
                ForeColor = C_TEXT,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(14, 13),
                Size = new Size(340, 28),
                BackColor = Color.Transparent,
            };

            // Branch badge — right-aligned
            var lblBranch = new LabelControl
            {
                Text = $"Branch: {_branch}",
                Font = new Font("Courier New", 9f, FontStyle.Bold),
                ForeColor = C_GOLD,
                AutoSizeMode = LabelAutoSizeMode.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Size = new Size(160, 18),
                BackColor = Color.Transparent,
            };
            // Position right-aligned after layout
            pnl.SizeChanged += (s, e) =>
                lblBranch.Location = new Point(pnl.Width - 170, 17);

            // Bottom gold line
            var line = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 2,
                BackColor = C_GOLD
            };

            pnl.Controls.AddRange(new Control[] { accent, lblTitle, lblBranch, line });
            return pnl;
        }

        // ── Row 1: Filter bar ─────────────────────────────────────────
        private Panel BuildFilter()
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_CARD,
                Padding = new Padding(10, 8, 10, 6),
            };
            var bottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = C_BORDER
            };
            pnl.Controls.Add(bottom);

            // Use a FlowLayoutPanel so controls never overlap
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = Padding.Empty,
            };

            flow.Controls.Add(MakeFilterGroup("Branch", cmbBranch = new SearchLookUpEdit(),
                width: 100));
            flow.Controls.Add(MakeFilterGroup("Bank GL Account", cmbAccount = new SearchLookUpEdit(),
                width: 280));
            flow.Controls.Add(MakeFilterGroup("Statement Date (month-end)",
                dtPeriod = new DateEdit(), width: 140));

            var btnLoad = MakeBtn("Load", C_GOLD, C_DARK, icon: "▶");
            btnLoad.Click += (s, e) => LoadRecon();
            btnLoad.Margin = new Padding(4, 14, 0, 0);
            btnLoad.Size = new Size(90, 26);
            flow.Controls.Add(btnLoad);

            StyleLookUp(cmbBranch);
            StyleLookUp(cmbAccount);
            dtPeriod.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            dtPeriod.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            StyleDate(dtPeriod);

            pnl.Controls.Add(flow);
            return pnl;
        }

        private Panel MakeFilterGroup(string caption, Control ctrl, int width)
        {
            var grp = new Panel
            {
                Width = width + 4,
                Height = 44,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 4, 10, 0),
            };
            var lbl = new LabelControl
            {
                Text = caption.ToUpper(),
                Font = new Font("Courier New", 7f, FontStyle.Bold),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(0, 0),
                Size = new Size(width, 14),
                BackColor = Color.Transparent,
            };
            ctrl.Location = new Point(0, 16);
            ctrl.Size = new Size(width, 24);
            grp.Controls.AddRange(new Control[] { lbl, ctrl });
            return grp;
        }

        // ── Row 2: Header (Book Balance + Bank Statement) ─────────────
        private Panel BuildHeader()
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_SURFACE,
                Padding = new Padding(14, 10, 14, 8),
            };
            var bottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = C_BORDER
            };
            pnl.Controls.Add(bottom);

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
            };

            // Book balance group
            var grpBook = new Panel
            {
                Width = 260,
                Height = 50,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 30, 0),
            };
            var lblBookCap = new LabelControl
            {
                Text = "BALANCE PER GL (BOOK)",
                Font = new Font("Courier New", 7f, FontStyle.Bold),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(0, 0),
                Size = new Size(260, 14),
                BackColor = Color.Transparent,
            };
            lblBookBal = new LabelControl
            {
                Text = "0.00",
                Font = new Font("Courier New", 15f, FontStyle.Bold),
                ForeColor = C_DR,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(0, 16),
                Size = new Size(260, 26),
                BackColor = Color.Transparent,
            };
            grpBook.Controls.AddRange(new Control[] { lblBookCap, lblBookBal });

            // Bank statement balance group
            var grpBank = new Panel
            {
                Width = 320,
                Height = 50,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 0),
            };
            var lblBankCap = new LabelControl
            {
                Text = "BALANCE PER BANK STATEMENT",
                Font = new Font("Courier New", 7f, FontStyle.Bold),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(0, 0),
                Size = new Size(320, 14),
                BackColor = Color.Transparent,
            };
            txtBankBal = new TextEdit
            {
                Location = new Point(0, 16),
                Size = new Size(160, 24),
                Font = F_MONO,
                Text = "0.00",
            };
            txtBankBal.Properties.Appearance.BackColor = C_CARD;
            txtBankBal.Properties.Appearance.ForeColor = C_TEXT;

            btnSaveHeader = MakeBtn("Save Balance", C_CARD, C_MUTED, icon: "💾");
            btnSaveHeader.Location = new Point(168, 16);
            btnSaveHeader.Size = new Size(120, 24);
            btnSaveHeader.Appearance.BorderColor = C_BORDER;
            btnSaveHeader.Click += BtnSaveHeader_Click;

            grpBank.Controls.AddRange(new Control[] { lblBankCap, txtBankBal, btnSaveHeader });

            flow.Controls.AddRange(new Control[] { grpBook, grpBank });
            pnl.Controls.Add(flow);
            return pnl;
        }

        // ── Row 3: Body (Split — grid left, summary right) ────────────
        private Control BuildBody()
        {
            var split = new SplitContainerControl
            {
                Dock = DockStyle.Fill,
                SplitterPosition = 820,
                //SplitterWidth = 4,
                BackColor = C_DARK,
            };
            split.Panel1.BackColor = C_DARK;
            split.Panel2.BackColor = C_SURFACE;

            // ── Grid (left panel) ──────────────────────────────────
            gridItems = new DevExpress.XtraGrid.GridControl
            {
                Dock = DockStyle.Fill,  // FIX: was missing → grid had size 0,0
                BackColor = C_DARK,
            };
            viewItems = new GridView(gridItems);

            viewItems.OptionsView.ShowGroupPanel = false;
            viewItems.OptionsView.ShowIndicator = false;
            viewItems.OptionsView.ShowFooter = true;
            viewItems.OptionsView.EnableAppearanceEvenRow = true;
            viewItems.OptionsBehavior.Editable = false;

            viewItems.Appearance.HeaderPanel.BackColor = C_CARD;
            viewItems.Appearance.HeaderPanel.ForeColor = C_GOLD;
            viewItems.Appearance.HeaderPanel.Font = new Font("Courier New", 8f, FontStyle.Bold);
            viewItems.Appearance.HeaderPanel.Options.UseBackColor = true;
            viewItems.Appearance.HeaderPanel.Options.UseForeColor = true;
            viewItems.Appearance.HeaderPanel.Options.UseFont = true;

            viewItems.Appearance.Row.BackColor = C_DARK;
            viewItems.Appearance.Row.ForeColor = C_TEXT;
            viewItems.Appearance.Row.Options.UseBackColor = true;
            viewItems.Appearance.Row.Options.UseForeColor = true;

            viewItems.Appearance.EvenRow.BackColor = C_SURFACE;
            viewItems.Appearance.EvenRow.Options.UseBackColor = true;

            viewItems.Appearance.FocusedRow.BackColor = C_BORDER;
            viewItems.Appearance.FocusedRow.ForeColor = C_TEXT;
            viewItems.Appearance.FocusedRow.Options.UseBackColor = true;
            viewItems.Appearance.FocusedRow.Options.UseForeColor = true;

            viewItems.Appearance.FooterPanel.BackColor = C_CARD;
            viewItems.Appearance.FooterPanel.ForeColor = C_GOLD;
            viewItems.Appearance.FooterPanel.Font = new Font("Courier New", 8.5f, FontStyle.Bold);
            viewItems.Appearance.FooterPanel.Options.UseBackColor = true;
            viewItems.Appearance.FooterPanel.Options.UseForeColor = true;
            viewItems.Appearance.FooterPanel.Options.UseFont = true;

            gridItems.MainView = viewItems;
            split.Panel1.Controls.Add(gridItems);

            // ── Summary panel (right panel) ────────────────────────
            split.Panel2.Controls.Add(BuildSummaryPanel());
            return split;
        }

        // ── Summary Panel (inside right split pane) ───────────────────
        private Control BuildSummaryPanel()
        {
            // TableLayoutPanel: each row = one line of the reconciliation
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                BackColor = C_SURFACE,
                Padding = new Padding(14, 12, 14, 12),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            int r = 0;
            void AddRow(string caption, ref LabelControl valLbl,
                        Color valColor, bool isSection = false,
                        bool isTotal = false, bool isSpacer = false)
            {
                if (isSpacer)
                {
                    tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 8f));
                    tlp.Controls.Add(new Panel { BackColor = Color.Transparent }, 0, r);
                    tlp.Controls.Add(new Panel { BackColor = Color.Transparent }, 1, r);
                    r++; return;
                }
                int h = isSection ? 22 : isTotal ? 32 : 24;
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
                var capLbl = new LabelControl
                {
                    Text = isSection ? caption.ToUpper() : caption,
                    Font = isSection
                                   ? new Font("Courier New", 7.5f, FontStyle.Bold)
                                   : isTotal ? F_BOLD : F_SMALL,
                    ForeColor = isSection ? C_GOLD : C_MUTED,
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent,
                };
                capLbl.Appearance.TextOptions.VAlignment =
                    DevExpress.Utils.VertAlignment.Center;

                valLbl = new LabelControl
                {
                    Text = "0.00",
                    Font = isTotal
                                   ? new Font("Courier New", 12f, FontStyle.Bold)
                                   : F_MONO,
                    ForeColor = valColor,
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent,
                };
                valLbl.Appearance.TextOptions.HAlignment =
                    DevExpress.Utils.HorzAlignment.Far;
                valLbl.Appearance.TextOptions.VAlignment =
                    DevExpress.Utils.VertAlignment.Center;

                tlp.Controls.Add(capLbl, 0, r);
                tlp.Controls.Add(valLbl, 1, r);
                r++;
            }

            // Bank side
            LabelControl dummy = null;
            AddRow("Bank Statement Side", ref dummy, C_GOLD, isSection: true);
            AddRow("Balance per bank statement", ref lblBankStmt, C_TEXT);
            AddRow("Add: Deposits in transit", ref lblDIT, C_DR);
            AddRow("Less: Outstanding checks", ref lblOC, C_CR);
            AddRow("Adjusted Bank Balance", ref lblAdjBank, C_OK, isTotal: true);

            // Divider spacer
            LabelControl dummySpacer = null;
            AddRow("", ref dummySpacer, C_MUTED, isSpacer: true);

            // Book side
            LabelControl dummy2 = null;
            AddRow("Book (GL) Side", ref dummy2, C_GOLD, isSection: true);
            AddRow("Balance per GL", ref lblBookSide, C_TEXT);
            AddRow("Add: Bank credit memos", ref lblBCM, C_DR);
            AddRow("Less: Bank debit memos/charges", ref lblBDM, C_CR);
            AddRow("Adjusted Book Balance", ref lblAdjBook, C_OK, isTotal: true);

            AddRow("", ref dummySpacer, C_MUTED, isSpacer: true);

            // Difference
            LabelControl dummy3 = null;
            AddRow("Difference (must be 0.00)", ref dummy3, C_GOLD, isSection: true);
            AddRow("", ref lblDiff, C_OK, isTotal: true);

            // Fill remaining space
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            tlp.Controls.Add(new Panel { BackColor = Color.Transparent }, 0, r);
            tlp.Controls.Add(new Panel { BackColor = Color.Transparent }, 1, r);

            return tlp;
        }

        // ── Row 4: Toolbar ────────────────────────────────────────────
        // Uses FlowLayoutPanel — no absolute Bounds, no overlap.
        private Panel BuildToolbar()
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_CARD,
            };
            var top = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = C_BORDER
            };
            pnl.Controls.Add(top);

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                AutoSize = true,
                Padding = new Padding(6, 6, 0, 6),
            };

            btnAdd = MakeBtn("＋ Add Item", C_GOLD, C_DARK);
            btnEdit = MakeBtn("✎ Edit", C_CARD, C_MUTED);
            btnResolve = MakeBtn("✔ Resolve", C_CARD, C_MUTED);
            btnDelete = MakeBtn("✖ Delete", Color.FromArgb(80, 20, 20), C_ERR);
            btnAutoMatch = MakeBtn("⚡ Auto-Match", C_CARD, C_MUTED);
            btnPrint = MakeBtn("🖨 Print", C_CARD, C_MUTED);

            foreach (var b in new[] { btnAdd, btnEdit, btnResolve, btnDelete, btnAutoMatch, btnPrint })
            {
                b.Margin = new Padding(0, 0, 5, 0);
                b.Size = new Size(0, 30);   // Width=0 → AutoSize fits text
                b.AutoSize = true;
                flow.Controls.Add(b);
            }

            btnEdit.Enabled = false;
            btnResolve.Enabled = false;
            btnDelete.Enabled = false;

            lblStatus = new LabelControl
            {
                Font = new Font("Courier New", 7.5f),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Dock = DockStyle.Right,
                Width = 400,
                BackColor = Color.Transparent,
                Text = "Select a bank account and click Load.",
            };
            lblStatus.Appearance.TextOptions.HAlignment =
                DevExpress.Utils.HorzAlignment.Far;
            lblStatus.Appearance.TextOptions.VAlignment =
                DevExpress.Utils.VertAlignment.Center;

            pnl.Controls.Add(lblStatus);
            pnl.Controls.Add(flow);
            return pnl;
        }

        // ================================================================
        // WIRE EVENTS
        // ================================================================
        private void WireEvents()
        {
            btnSaveHeader.Click += BtnSaveHeader_Click;
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnResolve.Click += BtnResolve_Click;
            btnDelete.Click += BtnDelete_Click;
            btnAutoMatch.Click += BtnAutoMatch_Click;
            btnPrint.Click += BtnPrint_Click;

            viewItems.FocusedRowChanged += (s, e) =>
            {
                bool has = viewItems.FocusedRowHandle >= 0;
                btnEdit.Enabled = has;
                btnResolve.Enabled = has;
                btnDelete.Enabled = has;
                if (has)
                    _selID = SafeInt(viewItems.GetRowCellValue(
                                        viewItems.FocusedRowHandle, "ReconID"));
            };
        }

        // ================================================================
        // POPULATE LOOKUPS
        // ================================================================
        private void PopulateLookups()
        {
            Database.displaySearchlookupEdit(
                "SELECT BranchCode, BranchName FROM Branches ORDER BY BranchCode",
                cmbBranch, "BranchCode", "BranchCode");
            cmbBranch.EditValue = Login.assignedBranch;

            Database.displaySearchlookupEdit(
                "SELECT AccountCode, Description FROM ChartOfAccounts " +
                "WHERE AccountCode LIKE '10102%' AND AccountType='D' " +
                "ORDER BY AccountCode",
                cmbAccount, "AccountCode", "AccountCode");
        }

        private void SetDefaultPeriod()
        {
            var today = DateTime.Today;
            _period = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            dtPeriod.EditValue = _period;
        }

        // ================================================================
        // LOAD RECONCILIATION
        // ================================================================
        private void LoadRecon()
        {
            _branch = cmbBranch.EditValue?.ToString() ?? Login.assignedBranch;
            _account = cmbAccount.EditValue?.ToString() ?? "";
            _period = dtPeriod.EditValue is DateTime dt ? dt
                     : DateTime.TryParse(dtPeriod.Text, out var pd) ? pd : DateTime.Today;

            if (string.IsNullOrWhiteSpace(_account))
            {
                XtraMessageBox.Show("Please select a bank GL account.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                LoadHeader();
                LoadItems();
                RefreshSummary();
                SetStatus($"Loaded: {_account}  |  Period: {_period:yyyy-MM-dd}");
                btnAdd.Enabled = btnAutoMatch.Enabled = btnPrint.Enabled = true;
            }
            catch (SqlException ex)
            { SetStatus($"Load failed: {ex.Message}", err: true); }
        }

        private void LoadHeader()
        {
            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("sp_BankRecon_GetHeader", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                cmd.Parameters.Add("@AccountCode", SqlDbType.VarChar, 20).Value = _account;
                cmd.Parameters.Add("@PeriodEnd", SqlDbType.Date).Value = _period;
                con.Open();
                using (var r = cmd.ExecuteReader())
                    if (r.Read())
                    {
                        _bookBal = SafeDec(r["BookBalance"]);
                        _bankBal = SafeDec(r["BankStatementBalance"]);
                        lblBookBal.Text = _bookBal.ToString("N2");
                        txtBankBal.Text = _bankBal.ToString("N2");
                    }
            }
        }

        private void LoadItems()
        {
            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("sp_BankRecon_GetItems", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                cmd.Parameters.Add("@AccountCode", SqlDbType.VarChar, 20).Value = _account;
                cmd.Parameters.Add("@PeriodEnd", SqlDbType.Date).Value = _period;
                cmd.Parameters.Add("@ShowResolved", SqlDbType.Bit).Value = false;
                con.Open();
                var dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                BindGrid(dt);
            }
        }

        private void BindGrid(DataTable dt)
        {
            viewItems.Columns.Clear();
            gridItems.DataSource = dt;

            FormatCol("ReconID", 50, false);
            FormatCol("ItemType", 48, false);
            FormatCol("ItemTypeDescription", 140, false);
            FormatCol("ReferenceNo", 130, false);
            FormatCol("ItemDate", 90, false, isDate: true);
            FormatCol("Payee", 170, false);
            FormatCol("Amount", 120, true);
            FormatCol("Remarks", 180, false);
            FormatCol("IsResolved", 65, false);

            viewItems.BestFitColumns();
        }

        private void FormatCol(string field, int width, bool money,
                                bool isDate = false)
        {
            var col = viewItems.Columns[field];
            if (col == null) return;
            col.Width = width;
            col.AppearanceHeader.ForeColor = C_GOLD;
            col.AppearanceHeader.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
            col.AppearanceHeader.Options.UseForeColor = true;
            col.AppearanceHeader.Options.UseFont = true;
            if (money)
            {
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = "N2";
                col.AppearanceCell.Font = F_MONO;
                col.AppearanceCell.ForeColor = C_DR;
                col.AppearanceCell.TextOptions.HAlignment =
                    DevExpress.Utils.HorzAlignment.Far;
                col.AppearanceCell.Options.UseFont = true;
                col.AppearanceCell.Options.UseForeColor = true;
                col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, field, "{0:N2}");
            }
            if (isDate)
            {
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                col.DisplayFormat.FormatString = "yyyy-MM-dd";
            }
        }

        // ================================================================
        // SUMMARY REFRESH
        // ================================================================
        private void RefreshSummary()
        {
            decimal dit = 0, oc = 0, bcm = 0, bdm = 0;
            if (gridItems.DataSource is DataTable dt)
                foreach (DataRow row in dt.Rows)
                {
                    if (row["IsResolved"] is true) continue;
                    decimal amt = SafeDec(row["Amount"]);
                    switch (row["ItemType"]?.ToString())
                    {
                        case "DIT": dit += amt; break;
                        case "OC": oc += amt; break;
                        case "BCM": bcm += amt; break;
                        case "BDM": case "BC": case "NSF": bdm += amt; break;
                    }
                }

            decimal adjBank = _bankBal + dit - oc;
            decimal adjBook = _bookBal + bcm - bdm;
            decimal diff = adjBank - adjBook;
            bool balanced = Math.Abs(diff) < 0.01m;

            SetLabel(lblBankStmt, _bankBal.ToString("N2"), C_TEXT);
            SetLabel(lblDIT, $"+{dit:N2}", C_DR);
            SetLabel(lblOC, $"-{oc:N2}", C_CR);
            SetLabel(lblAdjBank, adjBank.ToString("N2"), C_OK);
            SetLabel(lblBookSide, _bookBal.ToString("N2"), C_TEXT);
            SetLabel(lblBCM, $"+{bcm:N2}", C_DR);
            SetLabel(lblBDM, $"-{bdm:N2}", C_CR);
            SetLabel(lblAdjBook, adjBook.ToString("N2"), C_OK);

            if (balanced)
            {
                SetLabel(lblDiff, "0.00  ✓ RECONCILED", C_OK);
                lblDiff.Font = new Font("Courier New", 12f, FontStyle.Bold);
            }
            else
            {
                SetLabel(lblDiff,
                    Math.Abs(diff).ToString("N2") + "  ⚠ OUT OF BALANCE", C_ERR);
                lblDiff.Font = new Font("Courier New", 12f, FontStyle.Bold);
            }
        }

        private void SetLabel(LabelControl lbl, string text, Color color)
        {
            if (lbl == null) return;
            lbl.Text = text;
            lbl.ForeColor = color;
            lbl.Appearance.Options.UseForeColor = true;
        }

        // ================================================================
        // CRUD HANDLERS  (same logic as v1, layout-independent)
        // ================================================================
        private void BtnSaveHeader_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtBankBal.Text.Replace(",", ""),
                NumberStyles.Any, CultureInfo.InvariantCulture, out var bal))
            { XtraMessageBox.Show("Enter a valid amount."); return; }
            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_BankRecon_SaveHeader", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                    cmd.Parameters.Add("@AccountCode", SqlDbType.VarChar, 20).Value = _account;
                    cmd.Parameters.Add("@PeriodEnd", SqlDbType.Date).Value = _period;
                    cmd.Parameters.Add("@BankStatementBal", SqlDbType.Decimal).Value = Math.Round(bal, 2);
                    cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = Login.Fullname;
                    con.Open(); cmd.ExecuteNonQuery();
                }
                _bankBal = bal; RefreshSummary();
                SetStatus("Bank statement balance saved.");
            }
            catch (SqlException ex) { SetStatus(ex.Message, err: true); }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new BankReconItemForm(isNew: true)
            { BankStatementBal = _bankBal })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    using (var con = Database.getConnection())
                    using (var cmd = new SqlCommand("sp_BankRecon_SaveItem", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                        cmd.Parameters.Add("@AccountCode", SqlDbType.VarChar, 20).Value = _account;
                        cmd.Parameters.Add("@PeriodEnd", SqlDbType.Date).Value = _period;
                        cmd.Parameters.Add("@BankStatementBal", SqlDbType.Decimal).Value = _bankBal;
                        cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 5).Value = dlg.ItemType;
                        cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar, 150).Value = dlg.ReferenceNo;
                        cmd.Parameters.Add("@ItemDate", SqlDbType.Date).Value = dlg.ItemDate;
                        cmd.Parameters.Add("@Payee", SqlDbType.VarChar, 200).Value = dlg.Payee;
                        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Math.Round(dlg.Amount, 2);
                        cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = dlg.Remarks;
                        cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = Login.Fullname;
                        con.Open(); cmd.ExecuteNonQuery();
                    }
                    LoadItems(); RefreshSummary(); SetStatus("Item added.");
                }
                catch (SqlException ex) { SetStatus(ex.Message, err: true); }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_selID <= 0) return;
            int rh = viewItems.FocusedRowHandle;
            using (var dlg = new BankReconItemForm(isNew: false)
            {
                ItemType = viewItems.GetRowCellValue(rh, "ItemType")?.ToString() ?? "",
                ReferenceNo = viewItems.GetRowCellValue(rh, "ReferenceNo")?.ToString() ?? "",
                ItemDate = SafeDate(viewItems.GetRowCellValue(rh, "ItemDate")),
                Payee = viewItems.GetRowCellValue(rh, "Payee")?.ToString() ?? "",
                Amount = SafeDec(viewItems.GetRowCellValue(rh, "Amount")),
                Remarks = viewItems.GetRowCellValue(rh, "Remarks")?.ToString() ?? "",
                BankStatementBal = _bankBal,
            })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    using (var con = Database.getConnection())
                    using (var cmd = new SqlCommand("sp_BankRecon_UpdateItem", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ReconID", SqlDbType.Int).Value = _selID;
                        cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 5).Value = dlg.ItemType;
                        cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar, 150).Value = dlg.ReferenceNo;
                        cmd.Parameters.Add("@ItemDate", SqlDbType.Date).Value = dlg.ItemDate;
                        cmd.Parameters.Add("@Payee", SqlDbType.VarChar, 200).Value = dlg.Payee;
                        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Math.Round(dlg.Amount, 2);
                        cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = dlg.Remarks;
                        cmd.Parameters.Add("@IsResolved", SqlDbType.Bit).Value = DBNull.Value;
                        cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = Login.Fullname;
                        con.Open(); cmd.ExecuteNonQuery();
                    }
                    LoadItems(); RefreshSummary(); SetStatus("Item updated.");
                }
                catch (SqlException ex) { SetStatus(ex.Message, err: true); }
            }
        }

        private void BtnResolve_Click(object sender, EventArgs e)
        {
            if (_selID <= 0) return;
            if (XtraMessageBox.Show("Mark this item as RESOLVED (cleared by bank)?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_BankRecon_UpdateItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReconID", SqlDbType.Int).Value = _selID;
                    cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 5).Value = DBNull.Value;
                    cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar, 150).Value = DBNull.Value;
                    cmd.Parameters.Add("@ItemDate", SqlDbType.Date).Value = DBNull.Value;
                    cmd.Parameters.Add("@Payee", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = DBNull.Value;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = DBNull.Value;
                    cmd.Parameters.Add("@IsResolved", SqlDbType.Bit).Value = true;
                    cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = Login.Fullname;
                    con.Open(); cmd.ExecuteNonQuery();
                }
                LoadItems(); RefreshSummary(); SetStatus("Item resolved.");
            }
            catch (SqlException ex) { SetStatus(ex.Message, err: true); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selID <= 0) return;
            if (XtraMessageBox.Show("Delete this item?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_BankRecon_DeleteItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReconID", SqlDbType.Int).Value = _selID;
                    cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = Login.Fullname;
                    con.Open(); cmd.ExecuteNonQuery();
                }
                LoadItems(); RefreshSummary(); SetStatus("Item deleted.");
            }
            catch (SqlException ex) { SetStatus(ex.Message, err: true); }
        }

        private void BtnAutoMatch_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(
                "Auto-match outstanding checks against GL payments?\n" +
                "Matching OC items will be marked Resolved.",
                "Auto-Match", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;
            try
            {
                int matched = 0;
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_BankRecon_AutoMatch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                    cmd.Parameters.Add("@AccountCode", SqlDbType.VarChar, 20).Value = _account;
                    cmd.Parameters.Add("@PeriodEnd", SqlDbType.Date).Value = _period;
                    cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = Login.Fullname;
                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                        if (rdr.Read()) matched = SafeInt(rdr["MatchedItems"]);
                }
                LoadItems(); RefreshSummary();
                SetStatus($"Auto-match: {matched} item(s) resolved.");
            }
            catch (SqlException ex) { SetStatus(ex.Message, err: true); }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Wire to your XtraReport template here.",
                "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================================================================
        // HELPERS
        // ================================================================
        private void SetStatus(string msg, bool err = false)
        {
            if (lblStatus == null) return;
            lblStatus.Text = msg;
            lblStatus.ForeColor = err ? C_ERR : C_MUTED;
            lblStatus.Appearance.Options.UseForeColor = true;
            Application.DoEvents();
        }

        private SimpleButton MakeBtn(string text, Color bg, Color fg,
                                     string icon = "")
        {
            var btn = new SimpleButton
            {
                Text = string.IsNullOrEmpty(icon) ? text : icon + " " + text,
                Font = F_SMALL,
                Appearance = {
                    BackColor = bg, ForeColor = fg, BorderColor = C_BORDER,
                },
            };
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            return btn;
        }

        private void StyleLookUp(SearchLookUpEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
            c.Properties.Appearance.Options.UseBackColor = true;
            c.Properties.Appearance.Options.UseForeColor = true;
        }

        private void StyleDate(DateEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
            c.Properties.Appearance.Options.UseBackColor = true;
            c.Properties.Appearance.Options.UseForeColor = true;
        }

        private static decimal SafeDec(object v)
        {
            if (v == null || v == DBNull.Value) return 0m;
            return decimal.TryParse(v.ToString().Replace(",", ""),
                NumberStyles.Any, CultureInfo.InvariantCulture, out var r) ? r : 0m;
        }
        private static int SafeInt(object v)
            => v == null || v == DBNull.Value ? 0
             : int.TryParse(v.ToString(), out var r) ? r : 0;
        private static DateTime SafeDate(object v)
            => v is DateTime dt ? dt
             : DateTime.TryParse(v?.ToString(), out var r) ? r : DateTime.Today;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 800);
            this.Name = "BankReconForm";
            this.ResumeLayout(false);
        }
    }
}