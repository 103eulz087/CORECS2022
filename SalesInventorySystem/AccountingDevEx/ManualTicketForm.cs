using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class ManualTicketForm : XtraForm
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
        private static readonly Color C_AMBER = Color.FromArgb(245, 158, 11);
        private static readonly Font F_SMALL = new Font("Segoe UI", 8.5f);
        private static readonly Font F_MONO = new Font("Courier New", 9f);
        private static readonly Font F_BOLD = new Font("Segoe UI", 9f, FontStyle.Bold);
        private static readonly Font F_TITLE = new Font("Georgia", 14f, FontStyle.Bold);
        private static readonly Font F_CAP = new Font("Courier New", 7f, FontStyle.Bold);

        // ── Controls ──────────────────────────────────────────────────
        private ComboBoxEdit cmbAdjType;
        private SearchLookUpEdit cmbSupplier, cmbInvoice;
        private TextEdit txtDocRef, txtRemarks;
        private TextEdit txtAdjAmount;
        private ComboBoxEdit cmbAPImpact;
        private TextEdit txtOrigTicket;
        private LabelControl lblInvoiceBalance, lblStatus;
        private RadioGroup radSourceType;   // PURCHASE / EXPENSE

        // GL Legs grid (left)
        private DevExpress.XtraGrid.GridControl gridLegs;
        private GridView viewLegs;
        private DataTable _legsTable;
        private SearchLookUpEdit cmbLegAccount;
        private ComboBoxEdit cmbLegDC;
        private TextEdit txtLegAmount, txtLegDesc;

        // Pending tickets grid (right)
        private DevExpress.XtraGrid.GridControl gridPending;
        private GridView viewPending;

        private SimpleButton btnAddLeg, btnRemoveLeg,
                             btnPost, btnApprove, btnReject,
                             btnRefresh;

        // ── State ─────────────────────────────────────────────────────
        private string _branch = Login.assignedBranch;
        private int _selectedManualID = 0;
        private long _batchRefID = 0;    // set when EXPENSE invoice is selected

        public ManualTicketForm()
        {
            this.Text = "Manual Ticketing";
            this.BackColor = C_DARK;
            this.ForeColor = C_TEXT;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1100, 700);
            InitializeComponent();
            BuildUI();
            WireEvents();
            PopulateLookups();
            InitLegsTable();
            LoadPendingTickets();
        }

        // ================================================================
        // UI
        // ================================================================
        private void BuildUI()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = C_DARK,
                Padding = Padding.Empty,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 196f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 44f));

            tlp.Controls.Add(BuildBanner(), 0, 0);
            tlp.Controls.Add(BuildHeader(), 0, 1);
            tlp.Controls.Add(BuildBody(), 0, 2);
            tlp.Controls.Add(BuildToolbar(), 0, 3);
            this.Controls.Add(tlp);
        }

        private Control BuildBanner()
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = C_SURFACE };
            new Panel { Dock = DockStyle.Left, Width = 3, BackColor = C_GOLD }.Show();
            p.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 3, BackColor = C_GOLD });
            p.Controls.Add(new LabelControl
            {
                Text = "Manual Ticketing",
                Font = F_TITLE,
                ForeColor = C_TEXT,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(14, 12),
                Size = new Size(400, 28),
                BackColor = Color.Transparent
            });
            p.Controls.Add(new LabelControl
            {
                Text = "Purchase Adjustments (APACCOUNTS) · Expense Adjustments (ExpenseMaster)",
                Font = new Font("Segoe UI", 8f),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(14, 36),
                Size = new Size(600, 14),
                BackColor = Color.Transparent
            });
            p.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 2, BackColor = C_GOLD });
            return p;
        }

        private Control BuildHeader()
        {
            // FIX: Use a TableLayoutPanel with 2 rows of fields instead of
            // a single FlowLayoutPanel. This guarantees the header stays within
            // its allocated height and never overlaps the body below.
            //
            // Row 0 (caption labels, 16px) + Row 1 (controls, 28px) = 44px per line
            // Line A: AdjType | Supplier | Invoice | Balance | Amount | APImpact
            // Line B: OrigTicket | DocRef | Remarks
            // Total: 44 + 8 + 44 = 96px inside outer padding → fits in 196px row
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_CARD,
                Padding = new Padding(10, 8, 10, 6)
            };
            outer.Controls.Add(new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = C_BORDER
            });

            // ── LINE A: main fields ───────────────────────────────────────
            var lineA = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 46,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };

            // Source type toggle (180px) — PURCHASE or EXPENSE
            var grpSrc = MakeHeaderGroup("Source Type", 180);
            radSourceType = new RadioGroup { Dock = DockStyle.Fill, Font = F_SMALL };
            radSourceType.Properties.Items.AddRange(new[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(0,"PURCHASE"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(1,"EXPENSE"),
            });
            radSourceType.Properties.Appearance.BackColor = C_CARD;
            radSourceType.Properties.Appearance.ForeColor = C_TEXT;
            //radSourceType.Properties.Appearance.Options.UseAll = true;
            radSourceType.EditValue = 0;
            grpSrc.Controls.Add(radSourceType);
            lineA.Controls.Add(grpSrc);

            // Adjustment type (150px)
            var grpA1 = MakeHeaderGroup("Adjustment Type", 150);
            cmbAdjType = new ComboBoxEdit { Dock = DockStyle.Fill, Font = F_MONO };
            cmbAdjType.Properties.Appearance.BackColor = C_CARD;
            cmbAdjType.Properties.Appearance.ForeColor = C_TEXT;
            //cmbAdjType.Properties.Appearance.Options.UseAll = true;
            cmbAdjType.Properties.Items.AddRange(new[]{
                "CM — Credit Memo", "DM — Debit Memo",
                "REV — Reversal",   "ADJ — Adjustment" });
            cmbAdjType.SelectedIndex = 0;
            grpA1.Controls.Add(cmbAdjType);
            lineA.Controls.Add(grpA1);

            // Supplier (230px)
            var grpA2 = MakeHeaderGroup("Supplier", 230);
            cmbSupplier = new SearchLookUpEdit { Dock = DockStyle.Fill, Font = F_MONO };
            StyleLU(cmbSupplier); grpA2.Controls.Add(cmbSupplier);
            lineA.Controls.Add(grpA2);

            // Invoice (220px)
            var grpA3 = MakeHeaderGroup("Invoice (APACCOUNTS)", 220);
            cmbInvoice = new SearchLookUpEdit { Dock = DockStyle.Fill, Font = F_MONO };
            StyleLU(cmbInvoice); grpA3.Controls.Add(cmbInvoice);
            lineA.Controls.Add(grpA3);

            // Current balance (130px)
            var grpA4 = MakeHeaderGroup("Current Balance", 130);
            lblInvoiceBalance = new LabelControl
            {
                Text = "0.00",
                Font = new Font("Courier New", 13f, FontStyle.Bold),
                ForeColor = C_DR,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            lblInvoiceBalance.Appearance.TextOptions.VAlignment =
                DevExpress.Utils.VertAlignment.Center;
            grpA4.Controls.Add(lblInvoiceBalance);
            lineA.Controls.Add(grpA4);

            // Adjustment amount (140px)
            var grpA5 = MakeHeaderGroup("Adjustment Amount", 140);
            txtAdjAmount = new TextEdit { Dock = DockStyle.Fill, Font = F_MONO, Text = "0.00" };
            StyleTE(txtAdjAmount); grpA5.Controls.Add(txtAdjAmount);
            lineA.Controls.Add(grpA5);

            // AP Impact (120px)
            var grpA6 = MakeHeaderGroup("AP Impact", 120);
            cmbAPImpact = new ComboBoxEdit { Dock = DockStyle.Fill, Font = F_MONO };
            cmbAPImpact.Properties.Appearance.BackColor = C_CARD;
            cmbAPImpact.Properties.Appearance.ForeColor = C_TEXT;
            //cmbAPImpact.Properties.Appearance.Options.UseAll = true;
            cmbAPImpact.Properties.Items.AddRange(new[] { "DECREASE", "INCREASE", "NONE" });
            cmbAPImpact.SelectedIndex = 0;
            grpA6.Controls.Add(cmbAPImpact);
            lineA.Controls.Add(grpA6);

            // ── Spacer between lines ──────────────────────────────────────
            var spacer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 6,
                BackColor = Color.Transparent
            };

            // ── LINE B: secondary fields ──────────────────────────────────
            var lineB = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 46,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };

            // Original ticket (150px)
            var grpB1 = MakeHeaderGroup("Original Ticket # (REV)", 150);
            txtOrigTicket = new TextEdit { Dock = DockStyle.Fill, Font = F_MONO };
            StyleTE(txtOrigTicket); grpB1.Controls.Add(txtOrigTicket);
            lineB.Controls.Add(grpB1);

            // CM/DM Reference (160px)
            var grpB2 = MakeHeaderGroup("CM / DM Reference No.", 160);
            txtDocRef = new TextEdit { Dock = DockStyle.Fill, Font = F_MONO };
            StyleTE(txtDocRef); grpB2.Controls.Add(txtDocRef);
            lineB.Controls.Add(grpB2);

            // Remarks (fills the rest)
            var grpB3 = MakeHeaderGroup("Remarks", 520);
            txtRemarks = new TextEdit { Dock = DockStyle.Fill, Font = F_SMALL };
            StyleTE(txtRemarks); grpB3.Controls.Add(txtRemarks);
            lineB.Controls.Add(grpB3);

            // Add in bottom-to-top order for DockStyle.Top stacking
            outer.Controls.Add(lineB);
            outer.Controls.Add(spacer);
            outer.Controls.Add(lineA);
            return outer;
        }

        // Creates a fixed-width group panel: caption label on top, control fills below
        // Uses DockStyle on inner control — no absolute Bounds
        private Panel MakeHeaderGroup(string caption, int width)
        {
            var grp = new Panel
            {
                Width = width,
                Height = 46,           // caption 14px + gap 4px + control 28px
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 10, 0),
            };
            var lbl = new LabelControl
            {
                Text = caption.ToUpper(),
                Font = F_CAP,
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Bounds = new Rectangle(0, 0, width, 14),
                BackColor = Color.Transparent,
            };
            // inner control panel — sits below the label
            var inner = new Panel
            {
                Bounds = new Rectangle(0, 16, width, 26),
                BackColor = Color.Transparent,
            };
            grp.Controls.Add(lbl);
            grp.Controls.Add(inner);
            return inner;   // caller adds its control directly to the returned panel
        }

        private Control BuildBody()
        {
            var split = new SplitContainerControl
            {
                Dock = DockStyle.Fill,
                SplitterPosition = 600,
                //SplitterWidth = 4
            };
            split.Panel1.BackColor = C_DARK;
            split.Panel2.BackColor = C_SURFACE;

            // ── Left: GL Legs entry ────────────────────────────────
            var leftTlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = C_DARK,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            leftTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            leftTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            leftTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
            leftTlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var lblLegs = new LabelControl
            {
                Dock = DockStyle.Fill,
                Text = "GL LEGS",
                Font = F_CAP,
                ForeColor = C_GOLD,
                BackColor = C_CARD
            };
            lblLegs.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            // Add-leg bar
            var legBar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = C_SURFACE,
                Padding = new Padding(4, 4, 4, 4)
            };

            cmbLegAccount = new SearchLookUpEdit
            {
                Size = new Size(160, 24),
                Font = F_MONO,
                Margin = new Padding(0, 0, 4, 0)
            };
            StyleLU(cmbLegAccount);

            cmbLegDC = new ComboBoxEdit { Size = new Size(60, 24), Font = F_MONO };
            cmbLegDC.Properties.Appearance.BackColor = C_CARD;
            cmbLegDC.Properties.Appearance.ForeColor = C_TEXT;
            cmbLegDC.Properties.Items.AddRange(new[] { "D — Debit", "C — Credit" });
            cmbLegDC.SelectedIndex = 0;
            cmbLegDC.Margin = new Padding(0, 0, 4, 0);

            txtLegAmount = new TextEdit { Size = new Size(110, 24), Font = F_MONO, Text = "0.00" };
            StyleTE(txtLegAmount); txtLegAmount.Margin = new Padding(0, 0, 4, 0);

            txtLegDesc = new TextEdit { Size = new Size(160, 24), Font = F_SMALL };
            StyleTE(txtLegDesc); txtLegDesc.Margin = new Padding(0, 0, 4, 0);

            btnAddLeg = MakeBtn("＋ Add", C_GOLD, C_DARK, size: new Size(70, 24));
            btnRemoveLeg = MakeBtn("✖ Remove", C_CARD, C_ERR, size: new Size(80, 24));

            legBar.Controls.AddRange(new Control[]{
                cmbLegAccount, cmbLegDC, txtLegAmount, txtLegDesc,
                btnAddLeg, btnRemoveLeg });

            // Legs grid
            gridLegs = new DevExpress.XtraGrid.GridControl { Dock = DockStyle.Fill };
            viewLegs = new GridView(gridLegs);
            ApplyGridStyle(viewLegs);
            viewLegs.OptionsView.ShowFooter = true;
            gridLegs.MainView = viewLegs;

            leftTlp.Controls.Add(lblLegs, 0, 0);
            leftTlp.Controls.Add(legBar, 0, 1);
            leftTlp.Controls.Add(gridLegs, 0, 2);
            split.Panel1.Controls.Add(leftTlp);

            // ── Right: Pending approvals ───────────────────────────
            var rightTlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = C_SURFACE,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            rightTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            rightTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            rightTlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var lblPend = new LabelControl
            {
                Dock = DockStyle.Fill,
                Text = "PENDING APPROVAL",
                Font = F_CAP,
                ForeColor = C_GOLD,
                BackColor = C_CARD
            };
            lblPend.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            gridPending = new DevExpress.XtraGrid.GridControl { Dock = DockStyle.Fill };
            viewPending = new GridView(gridPending);
            ApplyGridStyle(viewPending);
            viewPending.OptionsBehavior.Editable = false;
            gridPending.MainView = viewPending;

            rightTlp.Controls.Add(lblPend, 0, 0);
            rightTlp.Controls.Add(gridPending, 0, 1);
            split.Panel2.Controls.Add(rightTlp);

            return split;
        }

        private Control BuildToolbar()
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = C_CARD };
            p.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C_BORDER });

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                AutoSize = true,
                Padding = new Padding(6, 6, 0, 6)
            };

            btnPost = MakeBtn("📋 Post Ticket", C_GOLD, C_DARK);
            btnApprove = MakeBtn("✔ Approve", C_OK, C_DARK);
            btnReject = MakeBtn("✖ Reject", Color.FromArgb(80, 20, 20), C_ERR);
            btnRefresh = MakeBtn("🔄 Refresh", C_CARD, C_MUTED);

            btnApprove.Enabled = false;
            btnReject.Enabled = false;

            foreach (var b in new[] { btnPost, btnApprove, btnReject, btnRefresh })
            {
                b.AutoSize = true; b.Height = 30;
                b.Margin = new Padding(0, 0, 6, 0);
                flow.Controls.Add(b);
            }

            lblStatus = new LabelControl
            {
                Font = F_CAP,
                ForeColor = C_MUTED,
                Dock = DockStyle.Right,
                Width = 400,
                BackColor = Color.Transparent,
                Text = "Enter adjustment details and click Post."
            };
            lblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            p.Controls.Add(lblStatus);
            p.Controls.Add(flow);
            return p;
        }

        // ================================================================
        // WIRE EVENTS
        // ================================================================
        private void WireEvents()
        {
            radSourceType.SelectedIndexChanged += (s, e) => OnSourceTypeChanged();
            cmbAdjType.SelectedIndexChanged += (s, e) => OnTypeChanged();
            cmbSupplier.EditValueChanged += (s, e) => LoadInvoices();
            cmbInvoice.EditValueChanged += (s, e) => LoadInvoiceBalance();
            btnAddLeg.Click += BtnAddLeg_Click;
            btnRemoveLeg.Click += BtnRemoveLeg_Click;
            btnPost.Click += BtnPost_Click;
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            btnRefresh.Click += (s, e) => LoadPendingTickets();

            viewPending.FocusedRowChanged += (s, e) => {
                bool has = viewPending.FocusedRowHandle >= 0;
                if (has)
                {
                    _selectedManualID = SafeInt(
                        viewPending.GetRowCellValue(
                            viewPending.FocusedRowHandle, "ManualTicketID"));
                    string status = viewPending.GetRowCellValue(
                        viewPending.FocusedRowHandle, "Status")?.ToString() ?? "";
                    btnApprove.Enabled = status == "FOR APPROVAL";
                    btnReject.Enabled = status == "FOR APPROVAL";
                }
            };
        }

        private void OnSourceTypeChanged()
        {
            // Clear invoice selection and reload when source changes
            cmbInvoice.EditValue = null;
            lblInvoiceBalance.Text = "0.00";
            _batchRefID = 0;
            LoadInvoices();
            // Adjust default AP impact hint label
            bool isExpense = GetSourceType() == "EXPENSE";
            lblInvoiceBalance.ForeColor = isExpense ? C_AMBER : C_DR;
        }

        private void OnTypeChanged()
        {
            string t = GetAdjType();
            txtOrigTicket.Enabled = t == "REV";
            cmbAPImpact.EditValue = t == "DM" ? "INCREASE"
                                  : t == "ADJ" ? "NONE"
                                  : "DECREASE";
        }

        // ================================================================
        // LOOKUPS
        // ================================================================
        private void PopulateLookups()
        {
            Database.displaySearchlookupEdit(
                "SELECT SupplierID, SupplierName FROM Supplier " +
                "ORDER BY SupplierName",
                cmbSupplier, "SupplierID", "SupplierID");

            Database.displaySearchlookupEdit(
                "SELECT AccountCode, Description FROM ChartOfAccounts " +
                "WHERE AccountType='D' ORDER BY AccountCode",
                cmbLegAccount, "AccountCode", "AccountCode");
        }

        private void LoadInvoices()
        {
            string suppID = cmbSupplier.EditValue?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(suppID)) return;

            if (GetSourceType() == "EXPENSE")
            {
                // Load from ExpenseSummary via sp_GetExpenseInvoices
                try
                {
                    using (var con = Database.getConnection())
                    using (var cmd = new SqlCommand("sp_GetExpenseInvoices", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.VarChar, 50).Value = suppID;
                        cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                        con.Open();
                        var dt = new DataTable(); new SqlDataAdapter(cmd).Fill(dt);
                        cmbInvoice.Properties.DataSource = dt;
                        cmbInvoice.Properties.ValueMember = "InvoiceNo";
                        cmbInvoice.Properties.DisplayMember = "InvoiceNo";
                        cmbInvoice.Properties.View.Columns.Clear();
                        //foreach (DataColumn col in dt.Columns)
                        //{
                        //    var vc = new DevExpress.XtraEditors.Controls.LookUpColumnInfo(
                        //        col.ColumnName, col.ColumnName);
                        //    cmbInvoice.Properties.View.Columns.Add(vc);
                        //}
                        foreach (DataColumn col in dt.Columns)
                        {
                            var gridCol = new DevExpress.XtraGrid.Columns.GridColumn
                            {
                                FieldName = col.ColumnName,
                                Caption = col.ColumnName,
                                Visible = true
                            };

                            cmbInvoice.Properties.View.Columns.Add(gridCol);
                        }

                    }
                }
                catch (Exception ex) { SetStatus($"Invoice load failed: {ex.Message}", error: true); }
            }
            else // PURCHASE
            {
                Database.displaySearchlookupEdit(
                    $"SELECT SequenceNo, InvoiceNo, InvoiceDate, Balance, " +
                    $"ActualCost, PayStatus " +
                    $"FROM APACCOUNTS " +
                    $"WHERE SupplierID = '{suppID}' " +
                    $"  AND PayStatus <> 'PAID' " +
                    $"ORDER BY InvoiceDate DESC",
                    cmbInvoice, "InvoiceNo", "InvoiceNo");
            }
        }

        private void LoadInvoiceBalance()
        {
            string suppID = cmbSupplier.EditValue?.ToString() ?? "";
            string invNo = cmbInvoice.EditValue?.ToString() ?? "";
            _batchRefID = 0;
            if (string.IsNullOrWhiteSpace(suppID) || string.IsNullOrWhiteSpace(invNo))
            { lblInvoiceBalance.Text = "0.00"; return; }
            try
            {
                using (var con = Database.getConnection())
                {
                    con.Open();
                    decimal bal = 0m;
                    if (GetSourceType() == "EXPENSE")
                    {
                        // Read BatchReferenceID and total balance from ExpenseMaster
                        using (var cmd = new SqlCommand(
                            "SELECT TOP 1 BatchReferenceID, " +
                            "(SELECT SUM(Balance) FROM ExpenseMaster em2 " +
                            " WHERE em2.SupplierID=em.SupplierID AND em2.InvoiceNo=em.InvoiceNo " +
                            " AND em2.BatchReferenceID=em.BatchReferenceID) AS TotalBalance " +
                            "FROM ExpenseMaster em " +
                            "WHERE SupplierID=@s AND InvoiceNo=@i " +
                            "ORDER BY TRN_SEQ_NO", con))
                        {
                            cmd.Parameters.AddWithValue("@s", suppID);
                            cmd.Parameters.AddWithValue("@i", invNo);
                            using (var rdr = cmd.ExecuteReader())
                            {
                                if (rdr.Read())
                                {
                                    _batchRefID = rdr["BatchReferenceID"] != DBNull.Value
                                                ? Convert.ToInt64(rdr["BatchReferenceID"]) : 0L;
                                    bal = rdr["TotalBalance"] != DBNull.Value
                                        ? Convert.ToDecimal(rdr["TotalBalance"]) : 0m;
                                }
                            }
                        }
                        lblInvoiceBalance.ForeColor = bal > 0 ? C_AMBER : C_ERR;
                    }
                    else // PURCHASE
                    {
                        using (var cmd = new SqlCommand(
                            "SELECT Balance FROM APACCOUNTS " +
                            "WHERE SupplierID=@s AND InvoiceNo=@i " +
                            "ORDER BY SequenceNo DESC", con))
                        {
                            cmd.Parameters.AddWithValue("@s", suppID);
                            cmd.Parameters.AddWithValue("@i", invNo);
                            var val = cmd.ExecuteScalar();
                            bal = val != null && val != DBNull.Value ? Convert.ToDecimal(val) : 0m;
                        }
                        lblInvoiceBalance.ForeColor = bal > 0 ? C_DR : C_ERR;
                    }
                    lblInvoiceBalance.Text = bal.ToString("N2");
                }
            }
            catch { lblInvoiceBalance.Text = "Error"; }
        }

        // ================================================================
        // LEGS GRID
        // ================================================================
        private void InitLegsTable()
        {
            _legsTable = new DataTable();
            _legsTable.Columns.Add("AccountCode", typeof(string));
            _legsTable.Columns.Add("DebitCredit", typeof(string));
            _legsTable.Columns.Add("Amount", typeof(decimal));
            _legsTable.Columns.Add("Description", typeof(string));
            gridLegs.DataSource = _legsTable;

            FormatLegsGrid();
        }

        private void FormatLegsGrid()
        {
            var colCode = viewLegs.Columns["AccountCode"];
            var colDC = viewLegs.Columns["DebitCredit"];
            var colAmt = viewLegs.Columns["Amount"];

            if (colCode != null) { colCode.Width = 130; ApplyHeader(colCode); }
            if (colDC != null)
            {
                colDC.Width = 70; ApplyHeader(colDC);
                colDC.AppearanceCell.ForeColor = C_GOLD;
                colDC.AppearanceCell.Options.UseForeColor = true;
            }
            if (colAmt != null)
            {
                colAmt.Width = 120; ApplyHeader(colAmt);
                colAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colAmt.DisplayFormat.FormatString = "N2";
                colAmt.AppearanceCell.Font = F_MONO;
                colAmt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                colAmt.AppearanceCell.Options.UseFont = true;
                colAmt.Summary.Add(DevExpress.Data.SummaryItemType.Sum,
                    "Amount", "{0:N2}");
            }
        }

        private void BtnAddLeg_Click(object sender, EventArgs e)
        {
            string acct = cmbLegAccount.EditValue?.ToString() ?? "";
            string dc = cmbLegDC.Text.StartsWith("D") ? "D" : "C";
            string desc = txtLegDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(acct))
            { XtraMessageBox.Show("Select an account code."); return; }
            if (!decimal.TryParse(txtLegAmount.Text.Replace(",", ""),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var amt)
                || amt <= 0)
            { XtraMessageBox.Show("Enter a valid positive amount."); return; }

            _legsTable.Rows.Add(acct, dc, Math.Round(amt, 2), desc);

            // Validate balance live
            decimal dr = 0, cr = 0;
            foreach (DataRow r in _legsTable.Rows)
            {
                if (r["DebitCredit"].ToString() == "D") dr += (decimal)r["Amount"];
                else cr += (decimal)r["Amount"];
            }
            string diff = Math.Abs(dr - cr) < 0.01m
                        ? "✓ Balanced" : $"Difference: {dr - cr:N2}";
            lblStatus.Text = $"Legs — DR: {dr:N2}  CR: {cr:N2}  {diff}";
            lblStatus.ForeColor = Math.Abs(dr - cr) < 0.01m ? C_OK : C_AMBER;
        }

        private void BtnRemoveLeg_Click(object sender, EventArgs e)
        {
            int rh = viewLegs.FocusedRowHandle;
            if (rh < 0) return;
            _legsTable.Rows.RemoveAt(rh);
        }

        // ================================================================
        // POST TICKET
        // ================================================================
        private void BtnPost_Click(object sender, EventArgs e)
        {
            string suppID = cmbSupplier.EditValue?.ToString() ?? "";
            string invNo = cmbInvoice.EditValue?.ToString() ?? "";
            string adjType = GetAdjType();
            string docRef = txtDocRef.Text.Trim();
            string remarks = txtRemarks.Text.Trim();
            string origTkt = txtOrigTicket.Text.Trim();
            string impact = cmbAPImpact.EditValue?.ToString() ?? "DECREASE";

            if (string.IsNullOrWhiteSpace(suppID))
            { XtraMessageBox.Show("Select a Supplier."); return; }
            if (string.IsNullOrWhiteSpace(invNo))
            { XtraMessageBox.Show("Select an Invoice."); return; }
            if (_legsTable.Rows.Count < 2)
            { XtraMessageBox.Show("Add at least 2 GL legs (one Debit, one Credit)."); return; }

            if (!decimal.TryParse(txtAdjAmount.Text.Replace(",", ""),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var adjAmt)
                || adjAmt <= 0)
            { XtraMessageBox.Show("Enter a valid Adjustment Amount."); return; }

            // Validate balanced
            decimal dr = 0, cr = 0;
            foreach (DataRow r in _legsTable.Rows)
            {
                if (r["DebitCredit"].ToString() == "D") dr += (decimal)r["Amount"];
                else cr += (decimal)r["Amount"];
            }
            if (Math.Abs(dr - cr) > 0.01m)
            {
                XtraMessageBox.Show(
                    $"GL legs are not balanced.\nDebit: {dr:N2}  Credit: {cr:N2}\n" +
                    $"Difference: {dr - cr:N2}\n\nPlease correct the amounts.",
                    "Not Balanced", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string typeLabel = adjType == "CM" ? "Credit Memo"
                             : adjType == "DM" ? "Debit Memo"
                             : adjType == "REV" ? "Reversal" : "Adjustment";

            if (XtraMessageBox.Show(
                $"Post {typeLabel} for Invoice {invNo}?\n" +
                $"Supplier: {suppID}\n" +
                $"Amount: ₱{adjAmt:N2}\n" +
                $"AP Impact: {impact}\n\n" +
                "Ticket will be submitted for supervisor approval.",
                "Confirm Post", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                SetStatus($"Posting {typeLabel}…", working: true);
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_PostManualTicket", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;

                    cmd.Parameters.Add("@BranchCode",
                        SqlDbType.VarChar, 5).Value = _branch;
                    cmd.Parameters.Add("@TicketDate",
                        SqlDbType.Date).Value = DateTime.Today;
                    cmd.Parameters.Add("@AdjustmentType",
                        SqlDbType.VarChar, 5).Value = adjType;
                    cmd.Parameters.Add("@SupplierID",
                        SqlDbType.VarChar, 50).Value = suppID;
                    cmd.Parameters.Add("@InvoiceNo",
                        SqlDbType.VarChar, 80).Value = invNo;
                    cmd.Parameters.Add("@AdjustmentAmount",
                        SqlDbType.Decimal).Value = Math.Round(adjAmt, 2);
                    cmd.Parameters.Add("@APBalanceImpact",
                        SqlDbType.VarChar, 10).Value = impact;
                    cmd.Parameters.Add("@DocumentRef",
                        SqlDbType.VarChar, 100).Value =
                        string.IsNullOrEmpty(docRef) ? (object)DBNull.Value : docRef;
                    cmd.Parameters.Add("@Remarks",
                        SqlDbType.VarChar, 2000).Value =
                        string.IsNullOrEmpty(remarks) ? (object)DBNull.Value : remarks;
                    cmd.Parameters.Add("@SourceType",
                        SqlDbType.VarChar, 10).Value = GetSourceType();
                    cmd.Parameters.Add("@BatchReferenceID",
                        SqlDbType.BigInt).Value = _batchRefID > 0
                                                            ? (object)_batchRefID
                                                            : DBNull.Value;
                    cmd.Parameters.Add("@OriginalTicketNo",
                        SqlDbType.VarChar, 20).Value =
                        string.IsNullOrEmpty(origTkt) ? (object)DBNull.Value : origTkt;
                    cmd.Parameters.Add("@PreparedBy",
                        SqlDbType.VarChar, 50).Value = Login.Fullname;

                    // Legs TVP
                    var legTvp = new DataTable();
                    legTvp.Columns.Add("AccountCode", typeof(string));
                    legTvp.Columns.Add("DebitCredit", typeof(string));
                    legTvp.Columns.Add("Amount", typeof(decimal));
                    legTvp.Columns.Add("Description", typeof(string));
                    foreach (DataRow r in _legsTable.Rows)
                        legTvp.ImportRow(r);

                    var tvpParam = cmd.Parameters.AddWithValue("@Legs", legTvp);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.ManualTicketLegTVP";

                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            string tktNum = rdr["TicketNumber"]?.ToString() ?? "";
                            XtraMessageBox.Show(
                                $"Ticket {tktNum} posted successfully.\n" +
                                "Status: FOR APPROVAL\n\n" +
                                "A supervisor must approve before APACCOUNTS is updated.",
                                "Posted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                // Clear form
                _legsTable.Rows.Clear();
                txtAdjAmount.Text = "0.00";
                txtDocRef.Text = "";
                txtRemarks.Text = "";
                txtOrigTicket.Text = "";
                LoadPendingTickets();
                SetStatus("Ticket posted. Waiting for approval.");
            }
            catch (SqlException ex)
            {
                SetStatus($"Post failed ({ex.Number}): {ex.Message}", error: true);
                XtraMessageBox.Show($"Post failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================
        // APPROVE / REJECT
        // ================================================================
        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (_selectedManualID <= 0) return;

            string tktNum = viewPending.GetRowCellValue(
                viewPending.FocusedRowHandle, "TicketNumber")?.ToString() ?? "";
            string inv = viewPending.GetRowCellValue(
                viewPending.FocusedRowHandle, "InvoiceNo")?.ToString() ?? "";
            decimal amt = SafeDecimal(viewPending.GetRowCellValue(
                viewPending.FocusedRowHandle, "AdjustmentAmount"));
            string impact = viewPending.GetRowCellValue(
                viewPending.FocusedRowHandle, "APBalanceImpact")?.ToString() ?? "";

            if (XtraMessageBox.Show(
                $"APPROVE Ticket {tktNum}?\n" +
                $"Invoice: {inv}  Amount: ₱{amt:N2}\n" +
                $"AP Balance will {impact}.\n\n" +
                "This will update APACCOUNTS balance immediately.",
                "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_ApproveManualTicket", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ManualTicketID",
                        SqlDbType.Int).Value = _selectedManualID;
                    cmd.Parameters.Add("@ApprovedBy",
                        SqlDbType.VarChar, 50).Value = Login.Fullname;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadPendingTickets();
                SetStatus($"Ticket {tktNum} approved — APACCOUNTS updated.");
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            catch (SqlException ex)
            {
                SetStatus($"Approve failed: {ex.Message}", error: true);
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (_selectedManualID <= 0) return;

            using (var dlg = new MemoEdit())
            {
                if (XtraMessageBox.Show(
                    "Enter rejection reason in remarks. Proceed?",
                    "Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    != DialogResult.Yes) return;
            }

            string reason = XtraInputBox.Show(
                "Enter rejection reason:", "Reject Ticket", "");
            if (string.IsNullOrWhiteSpace(reason)) return;

            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_RejectManualTicket", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ManualTicketID",
                        SqlDbType.Int).Value = _selectedManualID;
                    cmd.Parameters.Add("@RejectedBy",
                        SqlDbType.VarChar, 50).Value = Login.Fullname;
                    cmd.Parameters.Add("@RejectionReason",
                        SqlDbType.VarChar, 500).Value = reason;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadPendingTickets();
                SetStatus("Ticket rejected and voided.");
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            catch (SqlException ex)
            {
                SetStatus($"Reject failed: {ex.Message}", error: true);
            }
        }

        // ================================================================
        // PENDING TICKETS GRID
        // ================================================================
        private void LoadPendingTickets()
        {
            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_GetManualTickets", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BranchCode",
                        SqlDbType.VarChar, 5).Value = _branch;
                    cmd.Parameters.Add("@SupplierID",
                        SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@SourceType",
                        SqlDbType.VarChar, 10).Value = DBNull.Value; // all
                    cmd.Parameters.Add("@Status",
                        SqlDbType.VarChar, 20).Value = DBNull.Value; // all
                    cmd.Parameters.Add("@DateFrom",
                        SqlDbType.Date).Value =
                        (object)DateTime.Today.AddMonths(-2);
                    cmd.Parameters.Add("@DateTo",
                        SqlDbType.Date).Value = (object)DateTime.Today;
                    con.Open();
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    viewPending.Columns.Clear();
                    gridPending.DataSource = dt;
                    FormatPendingGrid();
                }
            }
            catch (Exception ex)
            { SetStatus($"Load failed: {ex.Message}", error: true); }
        }

        private void FormatPendingGrid()
        {
            foreach (GridColumn col in viewPending.Columns)
            {
                ApplyHeader(col);
                string fn = col.FieldName.ToUpperInvariant();
                if (fn.Contains("AMOUNT") || fn.Contains("BALANCE"))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                    col.AppearanceCell.Font = F_MONO;
                    col.AppearanceCell.Options.UseFont = true;
                    col.AppearanceCell.TextOptions.HAlignment =
                        DevExpress.Utils.HorzAlignment.Far;
                }
                if (fn == "STATUS")
                {
                    col.AppearanceCell.ForeColor = C_AMBER;
                    col.AppearanceCell.Options.UseForeColor = true;
                }
                if (fn.Contains("DATE"))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "yyyy-MM-dd";
                }
            }
            viewPending.BestFitColumns();
        }

        // ================================================================
        // HELPERS
        // ================================================================
        private string GetAdjType()
        {
            string t = cmbAdjType.Text;
            if (t.StartsWith("CM")) return "CM";
            if (t.StartsWith("DM")) return "DM";
            if (t.StartsWith("REV")) return "REV";
            return "ADJ";
        }

        private string GetSourceType() =>
            (int)(radSourceType?.EditValue ?? 0) == 1 ? "EXPENSE" : "PURCHASE";

        private void SetStatus(string msg, bool working = false, bool error = false)
        {
            if (lblStatus == null) return;
            lblStatus.Text = msg;
            lblStatus.ForeColor = error ? C_ERR : working ? C_AMBER : C_MUTED;
            lblStatus.Appearance.Options.UseForeColor = true;
            Application.DoEvents();
        }

        private void ApplyGridStyle(GridView v)
        {
            v.OptionsView.ShowGroupPanel = false;
            v.OptionsView.ShowIndicator = false;
            v.OptionsView.EnableAppearanceEvenRow = true;
            v.Appearance.Row.BackColor = C_DARK; v.Appearance.Row.ForeColor = C_TEXT;
            //v.Appearance.Row.Options.UseAll = true;
            v.Appearance.EvenRow.BackColor = C_SURFACE;
            v.Appearance.EvenRow.Options.UseBackColor = true;
            v.Appearance.FocusedRow.BackColor = C_BORDER;
            //v.Appearance.FocusedRow.Options.UseAll = true;
            v.Appearance.HeaderPanel.BackColor = C_CARD;
            v.Appearance.HeaderPanel.ForeColor = C_GOLD;
            v.Appearance.HeaderPanel.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
            //v.Appearance.HeaderPanel.Options.UseAll = true;
            v.Appearance.FooterPanel.BackColor = C_CARD;
            v.Appearance.FooterPanel.ForeColor = C_GOLD;
            //v.Appearance.FooterPanel.Options.UseAll = true;
        }

        private void ApplyHeader(GridColumn col)
        {
            col.AppearanceHeader.BackColor = C_CARD;
            col.AppearanceHeader.ForeColor = C_GOLD;
            col.AppearanceHeader.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
            //col.AppearanceHeader.Options.UseAll = true;
        }

        private Panel MakeGroup(string caption, out Panel placeholder, int width)
        {
            var grp = new Panel
            {
                Width = width + 4,
                Height = 44,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 4, 10, 0)
            };
            var lbl = new LabelControl
            {
                Text = caption.ToUpper(),
                Font = F_CAP,
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(0, 0),
                Size = new Size(width, 14),
                BackColor = Color.Transparent
            };
            placeholder = new Panel
            {
                Location = new Point(0, 15),
                Size = new Size(width, 26),
                BackColor = Color.Transparent
            };
            grp.Controls.AddRange(new Control[] { lbl, placeholder });
            return grp;
        }

        private SimpleButton MakeBtn(string text, Color bg, Color fg,
                                      Size? size = null)
        {
            var b = new SimpleButton
            {
                Text = text,
                Font = F_SMALL,
                AutoSize = true,
                Height = 30
            };
            if (size.HasValue) b.Size = size.Value;
            b.Appearance.BackColor = bg; b.Appearance.ForeColor = fg;
            b.Appearance.BorderColor = C_BORDER;
            //b.Appearance.Options.UseAll = true;
            b.Margin = new Padding(0, 0, 6, 0);
            return b;
        }

        private void StyleLU(SearchLookUpEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
            //c.Properties.Appearance.Options.UseAll = true;
        }

        private void StyleTE(TextEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
            //c.Properties.Appearance.Options.UseAll = true;
        }

        private static decimal SafeDecimal(object v)
        {
            if (v == null || v == DBNull.Value) return 0m;
            return decimal.TryParse(v.ToString().Replace(",", ""),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var r) ? r : 0m;
        }

        private static int SafeInt(object v)
            => v == null || v == DBNull.Value ? 0
             : int.TryParse(v.ToString(), out var r) ? r : 0;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 800);
            this.Name = "ManualTicketForm";
            this.ResumeLayout(false);
        }
    }
}
