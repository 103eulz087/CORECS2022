using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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

namespace SalesInventorySystem.Reporting
{

    // ================================================================
    // AccountingReportsForm v2
    //
    // CHANGES FROM v1:
    //   1. All SPs updated to WithDate family (exact date params)
    //   2. Parameter visibility matrix correctly implemented
    //   3. Result set routing: Set1→gridMain, Set2+→gridSummary
    //   4. Bank Recon now shows account selector + 3 result sets
    //   5. Consolidated shows TB/IS radio with correct param swap
    //   6. GL Detail shows 4 result sets (header, opening, detail, summary)
    //   7. Layout rebuilt with TableLayoutPanel — no Dock overlap
    //   8. Each report definition declares exactly which params it needs
    //
    // SP MATRIX (all WithDate versions):
    //   GL Detail     sp_rpt_GLDetailLedgerWithDate   @Branch @Acct @From @To
    //   Trial Balance sp_rpt_TrialBalanceWithDate      @Branch @AsOf
    //   Income Stmt   sp_rpt_IncomeStatementWithDate   @Branch @From @To
    //   Balance Sheet sp_rpt_BalanceSheetWithDate      @Branch @AsOf
    //   Bank Recon    sp_rpt_BankReconciliationWithDate @Branch @Acct @AsOf
    //   Consolidated  sp_rpt_ConsolidatedGLWithDate    @AsOf @From @To @Type
    // ================================================================

    public partial class AccountingReportsForm : XtraForm
    {
        // ── Report definitions ────────────────────────────────────────
        private enum ReportID
        {
            GLDetail = 0,
            TrialBalance = 1,
            IncomeStmt = 2,
            BalanceSheet = 3,
            BankRecon = 4,
            Consolidated = 5,
        }

        private sealed class ReportDef
        {
            public ReportID ID;
            public string Name;
            public string SP;
            public string ParamHint;
            // which param controls to show
            public bool ShowAsOf;
            public bool ShowFrom;
            public bool ShowTo;
            public bool ShowAccount;
            public bool ShowConsType;
            // labels
            public string AsOfLabel = "As-of Date";
            public string FromLabel = "Date From";
            public string ToLabel = "Date To";
        }

        private static readonly List<ReportDef> Reports = new List<ReportDef>
        {
            new ReportDef {
                ID=ReportID.GLDetail,
                Name="GL Detail Ledger",
                SP="sp_rpt_GLDetailLedgerWithDate",
                ParamHint="Shows every daily movement for one account. Returns opening balance, detail rows with ticket references, and period summary.",
                ShowFrom=true, ShowTo=true, ShowAccount=true,
                FromLabel="Date From", ToLabel="Date To",
            },
            new ReportDef {
                ID=ReportID.TrialBalance,
                Name="Trial Balance",
                SP="sp_rpt_TrialBalanceWithDate",
                ParamHint="Snapshot of all account balances at a specific date. Works for any date — not just month-end. Debit must equal Credit.",
                ShowAsOf=true,
                AsOfLabel="As-of Date",
            },
            new ReportDef {
                ID=ReportID.IncomeStmt,
                Name="Income Statement",
                SP="sp_rpt_IncomeStatementWithDate",
                ParamHint="Revenue, COGS and Expenses for a date range. Use Jan 1–Jan 31 for monthly, Jan 1–Mar 31 for quarterly, Jan 1–Dec 31 for annual.",
                ShowFrom=true, ShowTo=true,
                FromLabel="Period From", ToLabel="Period To",
            },
            new ReportDef {
                ID=ReportID.BalanceSheet,
                Name="Balance Sheet",
                SP="sp_rpt_BalanceSheetWithDate",
                ParamHint="Financial position at a specific date. Assets = Liabilities + Equity. Includes Current Earnings from IS accounts.",
                ShowAsOf=true,
                AsOfLabel="As-of Date",
            },
            new ReportDef {
                ID=ReportID.BankRecon,
                Name="Bank Reconciliation",
                SP="sp_rpt_BankReconciliationWithDate",
                ParamHint="Reconciles GL book balance against bank statement. Select the specific bank account and always use month-end date.",
                ShowAsOf=true, ShowAccount=true,
                AsOfLabel="Statement Date (month-end)",
            },
            new ReportDef {
                ID=ReportID.Consolidated,
                Name="Consolidated GL",
                SP="sp_rpt_ConsolidatedGLWithDate",
                ParamHint="All branches combined. TB mode = as-of snapshot. IS mode = period activity.",
                ShowAsOf=true, ShowFrom=true, ShowTo=true, ShowConsType=true,
                AsOfLabel="As-of Date (TB mode)",
                FromLabel="Period From (IS mode)", ToLabel="Period To (IS mode)",
            },
        };

        // ── State ─────────────────────────────────────────────────────
        private string _branch = Login.assignedBranch;

        // ── Controls ──────────────────────────────────────────────────
        private ListBoxControl lstReports;
        private SearchLookUpEdit cmbBranch, cmbAccount;
        private DateEdit dtAsOf, dtFrom, dtTo;
        private RadioGroup radConsType;
        private LabelControl lblAsOf, lblFrom, lblTo, lblAccount,
                                 lblConsType, lblHint, lblSPName,
                                 lblStatus, lblRowCount;
        private SimpleButton btnGenerate, btnExport;
        private DevExpress.XtraGrid.GridControl gridMain, gridSummary;
        private GridView viewMain, viewSummary;
        private LabelControl lblReportTitle, lblReportSub;

        // ── Colours ───────────────────────────────────────────────────
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
        private static readonly Font F_SMALL = new Font("Segoe UI", 8.5f);
        private static readonly Font F_MONO = new Font("Courier New", 9f);
        private static readonly Font F_TITLE = new Font("Georgia", 14f, FontStyle.Bold);
        private static readonly Font F_CAP = new Font("Courier New", 7f, FontStyle.Bold);

        public AccountingReportsForm()
        {
            this.Text = "JFC ERP — Accounting Reports";
            this.BackColor = C_DARK;
            this.ForeColor = C_TEXT;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1100, 650);
            InitializeComponent();
            BuildUI();
            WireEvents();
            PopulateLookups();
            SetDefaultDates();
            ApplyReportSelection();
        }

        // ================================================================
        // LAYOUT — TableLayoutPanel master, no Dock stacking
        // ================================================================
        private void BuildUI()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = C_DARK,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 72f)); // banner
            //tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52f)); // banner
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f)); // main body
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f)); // status bar

            tlp.Controls.Add(BuildBanner(), 0, 0);
            tlp.Controls.Add(BuildBody(), 0, 1);
            tlp.Controls.Add(BuildStatusBar(), 0, 2);

            this.Controls.Add(tlp);
        }

        // ── Banner ─────────────────────────────────────────────────────
        private Control BuildBanner()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = C_SURFACE };
            var accent = new Panel { Dock = DockStyle.Left, Width = 3, BackColor = C_GOLD };
            var lbl = new LabelControl
            {
                Text = "JFC ERP  ·  GL Reports",
                Font = F_TITLE,
                ForeColor = C_TEXT,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(14, 12),
                Size = new Size(360, 28),
                BackColor = Color.Transparent,
            };
            var sub = new LabelControl
            {
                Text = "GL Detail · Trial Balance · Income Statement · Balance Sheet · Bank Recon · Consolidated",
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                //Location = new Point(14, 34),
                Location = new Point(14, 44),
                Size = new Size(700, 14),
                BackColor = Color.Transparent,
            };
            var line = new Panel { Dock = DockStyle.Bottom, Height = 2, BackColor = C_GOLD };
            pnl.Controls.AddRange(new Control[] { accent, lbl, sub, line });
            return pnl;
        }

        // ── Main body: left param panel + right report area ────────────
        private Control BuildBody()
        {
            var split = new SplitContainerControl
            {
                Dock = DockStyle.Fill,
                SplitterPosition = 270,
                //SplitterWidth = 4,
            };
            split.Panel1.BackColor = C_SURFACE;
            split.Panel2.BackColor = C_DARK;
            split.Panel1.Controls.Add(BuildParamPanel());
            split.Panel2.Controls.Add(BuildReportArea());
            return split;
        }

        // ── Left: Parameter panel ──────────────────────────────────────
        private Control BuildParamPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_SURFACE,
                Padding = new Padding(12)
            };

            // Report list
            var lblRpt = MakeCap("Report Type", 0, 0);
            lstReports = new ListBoxControl
            {
                Bounds = new Rectangle(0, 18, 244, 138),
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple,
                Font = F_SMALL,
            };
            lstReports.Appearance.BackColor = C_CARD;
            lstReports.Appearance.ForeColor = C_TEXT;
            //lstReports.AppearanceFocusedItem.BackColor = C_GOLD;
            //lstReports.AppearanceFocusedItem.ForeColor = C_DARK;
            //lstReports.AppearanceHovered.BackColor = C_BORDER;
            foreach (var r in Reports) lstReports.Items.Add(r.Name);
            lstReports.SelectedIndex = 0;

            // Divider
            var div1 = new Panel { Bounds = new Rectangle(0, 164, 244, 1), BackColor = C_BORDER };

            // Parameters heading
            var lblParams = MakeCap("Parameters", 0, 172);

            // Branch
            lblAccount = MakeCap("Branch Code", 0, 190);  // reuse lblAccount temporarily
            var lblBr = MakeCap("Branch Code", 0, 190);
            cmbBranch = new SearchLookUpEdit
            {
                Bounds = new Rectangle(0, 206, 244, 24),
                Font = F_MONO
            };
            StyleLU(cmbBranch);

            // As-of Date
            lblAsOf = MakeCap("As-of Date", 0, 238);
            dtAsOf = new DateEdit { Bounds = new Rectangle(0, 254, 244, 24), Font = F_MONO };
            dtAsOf.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            dtAsOf.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            StyleDE(dtAsOf);

            // Date From
            lblFrom = MakeCap("Date From", 0, 238);
            dtFrom = new DateEdit { Bounds = new Rectangle(0, 254, 118, 24), Font = F_MONO };
            dtFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            dtFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            StyleDE(dtFrom);

            // Date To
            lblTo = MakeCap("Date To", 126, 238);
            dtTo = new DateEdit { Bounds = new Rectangle(126, 254, 118, 24), Font = F_MONO };
            dtTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            dtTo.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            StyleDE(dtTo);

            // Account Code
            lblAccount = MakeCap("Account Code", 0, 286);
            cmbAccount = new SearchLookUpEdit
            {
                Bounds = new Rectangle(0, 302, 244, 24),
                Font = F_MONO
            };
            StyleLU(cmbAccount);

            // Consolidated type radio
            lblConsType = MakeCap("Consolidated View", 0, 334);
            radConsType = new RadioGroup { Bounds = new Rectangle(0, 350, 244, 44), Font = F_SMALL };
            radConsType.Properties.Items.AddRange(new[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Trial Balance (as-of)"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Income Statement (range)"),
            });
            radConsType.Properties.Appearance.BackColor = C_CARD;
            radConsType.Properties.Appearance.ForeColor = C_TEXT;
            radConsType.EditValue = 0;

            // Hint
            var div2 = new Panel { Bounds = new Rectangle(0, 402, 244, 1), BackColor = C_BORDER };
            lblHint = new LabelControl
            {
                Bounds = new Rectangle(0, 410, 244, 52),
                Font = new Font("Segoe UI", 7.5f, FontStyle.Italic),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                //WordWrap = true,
                BackColor = Color.Transparent,
            };
            lblHint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            // SP name
            lblSPName = new LabelControl
            {
                Bounds = new Rectangle(0, 466, 244, 14),
                Font = new Font("Courier New", 7f),
                ForeColor = C_BORDER,
                AutoSizeMode = LabelAutoSizeMode.None,
                BackColor = Color.Transparent,
            };

            // Buttons
            var div3 = new Panel { Bounds = new Rectangle(0, 488, 244, 1), BackColor = C_BORDER };
            btnGenerate = new SimpleButton
            {
                Text = "▶  Generate Report",
                Bounds = new Rectangle(0, 496, 244, 32),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            };
            btnGenerate.Appearance.BackColor = C_GOLD;
            btnGenerate.Appearance.ForeColor = C_DARK;
            btnGenerate.Appearance.Options.UseBackColor = true;
            btnGenerate.Appearance.Options.UseForeColor = true;

            btnExport = new SimpleButton
            {
                Text = "⬇  Export to Excel",
                Bounds = new Rectangle(0, 534, 244, 26),
                Font = F_SMALL,
                Enabled = false,
            };
            btnExport.Appearance.BackColor = C_CARD;
            btnExport.Appearance.ForeColor = C_MUTED;
            btnExport.Appearance.BorderColor = C_BORDER;
            btnExport.Appearance.Options.UseBackColor = true;
            btnExport.Appearance.Options.UseForeColor = true;

            outer.Controls.AddRange(new Control[]{
                lblRpt, lstReports, div1, lblParams,
                lblBr, cmbBranch,
                lblAsOf, dtAsOf,
                lblFrom, lblTo, dtFrom, dtTo,
                lblAccount, cmbAccount,
                lblConsType, radConsType,
                div2, lblHint, lblSPName,
                div3, btnGenerate, btnExport,
            });
            return outer;
        }

        // ── Right: Report display area ─────────────────────────────────
        private Control BuildReportArea()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = C_DARK,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52f));  // report header
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));  // main grid
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 130f));  // summary grid

            // Report header bar
            var pnlHdr = new Panel { Dock = DockStyle.Fill, BackColor = C_SURFACE };
            var hdrLine = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = C_BORDER };
            lblReportTitle = new LabelControl
            {
                Text = "Select a report and click Generate",
                Font = F_TITLE,
                ForeColor = C_TEXT,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(14, 8),
                Size = new Size(900, 26),
                BackColor = Color.Transparent,
            };
            lblReportSub = new LabelControl
            {
                Text = "Branch 888  ·  JFC ERP Accounting Module",
                Font = new Font("Segoe UI", 8f),
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(14, 34),
                Size = new Size(900, 14),
                BackColor = Color.Transparent,
            };
            pnlHdr.Controls.AddRange(new Control[] { lblReportTitle, lblReportSub, hdrLine });
            tlp.Controls.Add(pnlHdr, 0, 0);

            // Main grid
            gridMain = new DevExpress.XtraGrid.GridControl { Dock = DockStyle.Fill };
            viewMain = new GridView(gridMain);
            ApplyGridStyle(viewMain);
            viewMain.OptionsView.ShowFooter = true;
            gridMain.MainView = viewMain;
            tlp.Controls.Add(gridMain, 0, 1);

            // Summary grid
            var pnlSumm = new Panel { Dock = DockStyle.Fill, BackColor = C_SURFACE };
            var summCap = new Panel
            {
                Dock = DockStyle.Top,
                Height = 18,
                BackColor = C_CARD,
            };
            var summLbl = new LabelControl
            {
                Text = "SUMMARY",
                Font = F_CAP,
                ForeColor = C_GOLD,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
            };
            summLbl.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            summCap.Controls.Add(summLbl);

            gridSummary = new DevExpress.XtraGrid.GridControl { Dock = DockStyle.Fill };
            var viewSumm = new GridView(gridSummary);
            ApplyGridStyle(viewSumm);
            viewSumm.OptionsView.ShowGroupPanel = false;
            viewSumm.OptionsView.ShowIndicator = false;
            gridSummary.MainView = viewSumm;
            viewSummary = viewSumm;

            pnlSumm.Controls.Add(gridSummary);
            pnlSumm.Controls.Add(summCap);
            tlp.Controls.Add(pnlSumm, 0, 2);

            return tlp;
        }

        // ── Status bar ─────────────────────────────────────────────────
        private Control BuildStatusBar()
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_CARD,
                Padding = new Padding(8, 4, 8, 4),
            };
            var top = new Panel { Dock = DockStyle.Top, Height = 1, BackColor = C_BORDER };
            lblStatus = new LabelControl
            {
                Text = "Ready",
                Font = F_CAP,
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Dock = DockStyle.Left,
                Width = 600,
                BackColor = Color.Transparent,
            };
            lblStatus.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            lblRowCount = new LabelControl
            {
                Text = "",
                Font = F_MONO,
                ForeColor = C_GOLD,
                AutoSizeMode = LabelAutoSizeMode.None,
                Dock = DockStyle.Right,
                Width = 200,
                BackColor = Color.Transparent,
            };
            lblRowCount.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            lblRowCount.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            pnl.Controls.AddRange(new Control[] { top, lblStatus, lblRowCount });
            return pnl;
        }

        // ================================================================
        // EVENTS
        // ================================================================
        private void WireEvents()
        {
            lstReports.SelectedIndexChanged += (s, e) => ApplyReportSelection();
            radConsType.SelectedIndexChanged += (s, e) => ApplyReportSelection();
            btnGenerate.Click += BtnGenerate_Click;
            btnExport.Click += BtnExport_Click;
        }

        // ================================================================
        // PARAMETER VISIBILITY — driven by the ReportDef flags
        // ================================================================
        private void ApplyReportSelection()
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0 || idx >= Reports.Count) return;
            var rpt = Reports[idx];
            bool consIS = rpt.ID == ReportID.Consolidated
                       && (int)(radConsType.EditValue ?? 0) == 1;

            // AsOf: shown for TB, BS, Recon; and Consolidated in TB mode
            bool showAsOf = rpt.ShowAsOf && !(rpt.ID == ReportID.Consolidated && consIS);
            // Range: shown for GL Detail, IS; and Consolidated in IS mode
            bool showRange = rpt.ShowFrom && rpt.ShowTo
                          && (rpt.ID != ReportID.Consolidated || consIS);

            lblAsOf.Visible = showAsOf;
            dtAsOf.Visible = showAsOf;
            lblFrom.Visible = showRange;
            lblTo.Visible = showRange;
            dtFrom.Visible = showRange;
            dtTo.Visible = showRange;
            lblAccount.Visible = rpt.ShowAccount;
            cmbAccount.Visible = rpt.ShowAccount;
            lblConsType.Visible = rpt.ShowConsType;
            radConsType.Visible = rpt.ShowConsType;

            // Update labels per report
            if (showAsOf) lblAsOf.Text = rpt.AsOfLabel.ToUpper();
            if (showRange)
            {
                lblFrom.Text = rpt.FromLabel.ToUpper();
                lblTo.Text = rpt.ToLabel.ToUpper();
            }

            lblHint.Text = rpt.ParamHint;
            lblSPName.Text = "SP: " + rpt.SP;

            lblReportTitle.Text = rpt.Name;
            lblReportSub.Text = $"Branch: {_branch}  ·  Select parameters and click Generate";

            ClearGrids();
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

            // All detail accounts — GL Detail uses any, Bank Recon filters to bank accounts
            Database.displaySearchlookupEdit(
                "SELECT AccountCode, Description FROM ChartOfAccounts " +
                "WHERE AccountType='D' ORDER BY AccountCode",
                cmbAccount, "AccountCode", "AccountCode");
        }

        private void SetDefaultDates()
        {
            var today = DateTime.Today;
            dtAsOf.EditValue = today;
            dtFrom.EditValue = new DateTime(today.Year, today.Month, 1);
            dtTo.EditValue = today;
        }

        // ================================================================
        // GENERATE REPORT
        // ================================================================
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0) return;
            var rpt = Reports[idx];

            _branch = cmbBranch.EditValue?.ToString() ?? Login.assignedBranch;
            if (string.IsNullOrWhiteSpace(_branch))
            { XtraMessageBox.Show("Select a Branch Code."); return; }

            DateTime asof = GetDate(dtAsOf);
            DateTime from = GetDate(dtFrom);
            DateTime to = GetDate(dtTo);
            string acct = cmbAccount.EditValue?.ToString() ?? "";
            bool consIS = (int)(radConsType.EditValue ?? 0) == 1;

            // Validate required params
            if ((rpt.ShowAccount) && string.IsNullOrWhiteSpace(acct))
            { XtraMessageBox.Show("Select an Account Code."); return; }

            SetStatus($"Generating {rpt.Name}…", working: true);
            ClearGrids();

            try
            {
                using (var con = Database.getConnection())
                {
                    con.Open();
                    List<DataTable> sets;

                    switch (rpt.ID)
                    {
                        case ReportID.GLDetail:
                            // Returns 4 sets: header, opening row, detail, period summary
                            sets = ExecSP(con, rpt.SP,
                                P("@BranchCode", SqlDbType.VarChar, 5, _branch),
                                P("@AccountCode", SqlDbType.VarChar, 20, acct),
                                P("@DateFrom", SqlDbType.Date, from),
                                P("@DateTo", SqlDbType.Date, to));
                            // Set 1 = header (single row, show in subtitle)
                            if (sets.Count > 0) ApplyGLDetailHeader(sets[0]);
                            // Sets 2+3 = opening + detail → union into main grid
                            if (sets.Count > 2) BindMainGrid(MergeGLDetailRows(sets[1], sets[2]));
                            // Set 4 = period summary → summary grid
                            if (sets.Count > 3) BindSummaryGrid(sets[3]);
                            break;

                        case ReportID.TrialBalance:
                            // Returns 2 sets: line items, balance check
                            sets = ExecSP(con, rpt.SP,
                                P("@BranchCode", SqlDbType.VarChar, 5, _branch),
                                P("@AsOfDate", SqlDbType.Date, asof));
                            if (sets.Count > 0) BindMainGrid(sets[0]);
                            if (sets.Count > 1) BindSummaryGrid(sets[1]);
                            break;

                        case ReportID.IncomeStmt:
                            // Returns 2 sets: line items, P&L summary
                            sets = ExecSP(con, rpt.SP,
                                P("@BranchCode", SqlDbType.VarChar, 5, _branch),
                                P("@DateFrom", SqlDbType.Date, from),
                                P("@DateTo", SqlDbType.Date, to));
                            if (sets.Count > 0) BindMainGrid(sets[0]);
                            if (sets.Count > 1) BindSummaryGrid(sets[1]);
                            break;

                        case ReportID.BalanceSheet:
                            // Returns 2 sets: line items, section totals
                            sets = ExecSP(con, rpt.SP,
                                P("@BranchCode", SqlDbType.VarChar, 5, _branch),
                                P("@AsOfDate", SqlDbType.Date, asof));
                            if (sets.Count > 0) BindMainGrid(sets[0]);
                            if (sets.Count > 1) BindSummaryGrid(sets[1]);
                            break;

                        case ReportID.BankRecon:
                            // Returns 3 sets: account header, items, summary
                            sets = ExecSP(con, rpt.SP,
                                P("@BranchCode", SqlDbType.VarChar, 5, _branch),
                                P("@AccountCode", SqlDbType.VarChar, 20, acct),
                                P("@AsOfDate", SqlDbType.Date, asof));
                            // Set 1 (header) → subtitle
                            if (sets.Count > 0) ApplyBankReconHeader(sets[0]);
                            // Set 2 (items) → main grid
                            if (sets.Count > 1) BindMainGrid(sets[1]);
                            // Set 3 (summary) → summary grid + subtitle update
                            if (sets.Count > 2)
                            {
                                BindSummaryGrid(sets[2]);
                                ApplyBankReconSummarySubtitle(sets[2]);
                            }
                            break;

                        case ReportID.Consolidated:
                            if (!consIS)
                            {
                                // TB mode: 2 sets (line items, balance check)
                                sets = ExecSP(con, rpt.SP,
                                    P("@AsOfDate", SqlDbType.Date, asof),
                                    P("@PeriodFrom", SqlDbType.Date, (object)DBNull.Value),
                                    P("@PeriodTo", SqlDbType.Date, (object)DBNull.Value),
                                    P("@ReportType", SqlDbType.VarChar, 5, "TB"));
                                if (sets.Count > 0) BindMainGrid(sets[0]);
                                if (sets.Count > 1) BindSummaryGrid(sets[1]);
                                // Set 3 = IC check if exists
                                if (sets.Count > 2) BindSummaryGrid(sets[2]);
                            }
                            else
                            {
                                // IS mode: 2 sets (line items, IS totals)
                                sets = ExecSP(con, rpt.SP,
                                    P("@AsOfDate", SqlDbType.Date, (object)DBNull.Value),
                                    P("@PeriodFrom", SqlDbType.Date, from),
                                    P("@PeriodTo", SqlDbType.Date, to),
                                    P("@ReportType", SqlDbType.VarChar, 5, "IS"));
                                if (sets.Count > 0) BindMainGrid(sets[0]);
                                if (sets.Count > 1) BindSummaryGrid(sets[1]);
                            }
                            break;
                    }
                }

                string period = BuildPeriodText(rpt, asof, from, to);
                lblReportTitle.Text = rpt.Name;
                lblReportSub.Text = $"Branch: {_branch}  ·  {period}  ·  Generated: {DateTime.Now:yyyy-MM-dd HH:mm}";
                btnExport.Enabled = true;

                int rows = viewMain.DataRowCount;
                lblRowCount.Text = $"{rows:N0} rows";
                SetStatus($"{rpt.Name} loaded — {rows} rows.");
            }
            catch (SqlException ex)
            {
                SetStatus($"Error ({ex.Number}): {ex.Message}", error: true);
                XtraMessageBox.Show($"Report failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================
        // GRID BINDING
        // ================================================================
        private void BindMainGrid(DataTable dt)
        {
            viewMain.Columns.Clear();
            gridMain.DataSource = dt;
            FormatGridColumns(viewMain, addFooter: true);
        }

        private void BindSummaryGrid(DataTable dt)
        {
            viewSummary.Columns.Clear();
            gridSummary.DataSource = dt;
            FormatGridColumns(viewSummary, addFooter: false);
        }

        private void FormatGridColumns(GridView view, bool addFooter)
        {
            foreach (GridColumn col in view.Columns)
            {
                string fn = col.FieldName.ToUpperInvariant();

                col.AppearanceHeader.BackColor = C_CARD;
                col.AppearanceHeader.ForeColor = C_GOLD;
                col.AppearanceHeader.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
                //col.AppearanceHeader.Options.UseAll = true;

                // Money columns
                if (fn.Contains("DEBIT") || fn.Contains("CREDIT") || fn.Contains("BALANCE")
                 || fn.Contains("AMOUNT") || fn.Contains("TOTAL") || fn.Contains("INCOME")
                 || fn.Contains("REVENUE") || fn.Contains("COGS") || fn.Contains("PROFIT")
                 || fn.Contains("EXPENSE") || fn.Contains("NET"))
                {
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                    col.AppearanceCell.Font = F_MONO;
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    col.AppearanceCell.Options.UseFont = true;
                    col.Width = 130;

                    if (fn.Contains("DEBIT") || fn.Contains("TBDEBIT"))
                    {
                        col.AppearanceCell.ForeColor = C_DR;
                        col.AppearanceCell.Options.UseForeColor = true;
                    }
                    else if (fn.Contains("CREDIT") || fn.Contains("TBCREDIT"))
                    {
                        col.AppearanceCell.ForeColor = C_CR;
                        col.AppearanceCell.Options.UseForeColor = true;
                    }
                    else if (fn.Contains("DIFFERENCE"))
                    {
                        col.AppearanceCell.ForeColor = C_ERR;
                        col.AppearanceCell.Options.UseForeColor = true;
                    }
                    else if (fn.Contains("NET") || fn.Contains("PROFIT") || fn.Contains("INCOME"))
                    {
                        col.AppearanceCell.ForeColor = C_OK;
                        col.AppearanceCell.Options.UseForeColor = true;
                    }

                    if (addFooter)
                        col.Summary.Add(DevExpress.Data.SummaryItemType.Sum,
                            col.FieldName, "{0:N2}");
                }
                // Date columns
                else if (fn.Contains("DATE") || fn == "PERIODEND" || fn == "PERIODFROM"
                      || fn == "PERIODTO" || fn == "ASOFDATE")
                {
                    col.DisplayFormat.FormatType = FormatType.DateTime;
                    col.DisplayFormat.FormatString = "yyyy-MM-dd";
                    col.Width = 100;
                }
                // Account code
                else if (fn == "ACCOUNTCODE" || fn.EndsWith("CODE"))
                {
                    col.AppearanceCell.Font = F_MONO;
                    col.AppearanceCell.Options.UseFont = true;
                    col.Width = 120;
                }
                // Description / Particulars — wider
                else if (fn.Contains("DESCRIPTION") || fn.Contains("PARTICULARS")
                      || fn.Contains("NAME") || fn.Contains("SECTION"))
                {
                    col.Width = 260;
                }
                // Nature / Type / narrow flags
                else if (fn == "NATURE" || fn == "YEARENDINDICATOR" || fn == "ISSECTION"
                      || fn == "BSSECTION" || fn == "ROWTYPE" || fn == "ITEMTYPE"
                      || fn == "ISINTERCOMPANY" || fn == "ISABNORMALBALANCE"
                      || fn == "ISCONTRACOGS")
                {
                    col.Width = 80;
                }
            }

            view.BestFitColumns();
        }

        private void ClearGrids()
        {
            gridMain.DataSource = null;
            gridSummary.DataSource = null;
            viewMain.Columns.Clear();
            viewSummary.Columns.Clear();
            lblRowCount.Text = "";
            btnExport.Enabled = false;
        }

        // ================================================================
        // RESULT SET HELPERS
        // ================================================================
        private void ApplyGLDetailHeader(DataTable dt)
        {
            if (dt.Rows.Count == 0) return;
            var row = dt.Rows[0];
            lblReportSub.Text =
                $"{row["AccountCode"]}  {row["AccountDescription"]}  ·  " +
                $"Nature: {row["Nature"]}  YE: {row["YearEndIndicator"]}";
        }

        private void ApplyBankReconHeader(DataTable dt)
        {
            // SET 1 returns only: AccountCode, AccountDescription, Nature, AsOfDate
            // BookBalance / BankStatementBalance are in SET 3 (summary).
            if (dt.Rows.Count == 0) return;
            var row = dt.Rows[0];
            string code = row["AccountCode"]?.ToString() ?? "";
            string desc = row["AccountDescription"]?.ToString() ?? "";
            string asof = dt.Columns.Contains("AsOfDate") && row["AsOfDate"] != DBNull.Value
                        ? Convert.ToDateTime(row["AsOfDate"]).ToString("yyyy-MM-dd") : "";
            lblReportSub.Text = $"{code}  {desc}  ·  As of: {asof}";
        }

        // After SET 3 binds, append balance figures to the subtitle
        private void ApplyBankReconSummarySubtitle(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            var row = dt.Rows[0];
            decimal book = dt.Columns.Contains("BookBalance")
                        && row["BookBalance"] != DBNull.Value
                         ? Convert.ToDecimal(row["BookBalance"]) : 0m;
            decimal bank = dt.Columns.Contains("BankStatementBalance")
                        && row["BankStatementBalance"] != DBNull.Value
                         ? Convert.ToDecimal(row["BankStatementBalance"]) : 0m;
            decimal diff = dt.Columns.Contains("Difference")
                        && row["Difference"] != DBNull.Value
                         ? Convert.ToDecimal(row["Difference"]) : 0m;
            bool ok = Math.Abs(diff) < 0.01m;
            lblReportSub.Text +=
                $"  ·  Book: ₱{book:N2}" +
                $"  ·  Bank Stmt: ₱{bank:N2}" +
                $"  ·  Diff: ₱{Math.Abs(diff):N2} " +
                (ok ? "✓ RECONCILED" : "⚠ OUT OF BALANCE");
        }

        // GL Detail: merge opening balance row + detail rows into one table
        private static DataTable MergeGLDetailRows(DataTable opening, DataTable detail)
        {
            var merged = detail.Clone();
            if (opening.Rows.Count > 0) merged.ImportRow(opening.Rows[0]);
            foreach (DataRow r in detail.Rows) merged.ImportRow(r);
            return merged;
        }

        // ================================================================
        // EXPORT
        // ================================================================
        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (gridMain.DataSource == null) return;
            using (var dlg = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"{Reports[lstReports.SelectedIndex].Name.Replace(" ", "_")}" +
                           $"_{_branch}_{DateTime.Today:yyyyMMdd}.xlsx",
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                try
                {
                    gridMain.ExportToXlsx(dlg.FileName);
                    XtraMessageBox.Show($"Exported:\n{dlg.FileName}", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                { XtraMessageBox.Show(ex.Message, "Export Error"); }
            }
        }

        // ================================================================
        // HELPERS
        // ================================================================
        private List<DataTable> ExecSP(SqlConnection con, string sp,
                                        params SqlParameter[] parms)
        {
            var result = new List<DataTable>();
            using (var cmd = new SqlCommand(sp, con)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 120
            })
            {
                cmd.Parameters.AddRange(parms);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataTable t in ds.Tables) result.Add(t);
                }
            }
            return result;
        }

        private static SqlParameter P(string n, SqlDbType t, object v)
            => new SqlParameter(n, t) { Value = v ?? DBNull.Value };

        private static SqlParameter P(string n, SqlDbType t, int size, object v)
            => new SqlParameter(n, t, size) { Value = v ?? DBNull.Value };

        private static DateTime GetDate(DateEdit de)
            => de.EditValue is DateTime dt ? dt
             : DateTime.TryParse(de.Text, out var r) ? r : DateTime.Today;

        private string BuildPeriodText(ReportDef rpt, DateTime asof,
                                        DateTime from, DateTime to)
        {
            bool consIS = rpt.ID == ReportID.Consolidated
                       && (int)(radConsType.EditValue ?? 0) == 1;
            if ((rpt.ShowAsOf && !rpt.ShowFrom) ||
                (rpt.ID == ReportID.Consolidated && !consIS))
                return $"As of {asof:yyyy-MM-dd}";
            if (rpt.ShowFrom && rpt.ShowTo)
                return $"{from:yyyy-MM-dd} to {to:yyyy-MM-dd}";
            return asof.ToString("yyyy-MM-dd");
        }

        private void SetStatus(string msg, bool working = false, bool error = false)
        {
            if (lblStatus == null) return;
            lblStatus.Text = msg;
            lblStatus.ForeColor = error ? C_ERR : working ? C_GOLD : C_MUTED;
            lblStatus.Appearance.Options.UseForeColor = true;
            Application.DoEvents();
        }

        private void ApplyGridStyle(GridView view)
        {
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.OptionsBehavior.Editable = false;

            view.Appearance.Row.BackColor = C_DARK;
            view.Appearance.Row.ForeColor = C_TEXT;
            //view.Appearance.Row.Options.UseAll = true;
            view.Appearance.EvenRow.BackColor = C_SURFACE;
            view.Appearance.EvenRow.Options.UseBackColor = true;
            view.Appearance.FocusedRow.BackColor = C_BORDER;
            view.Appearance.FocusedRow.ForeColor = C_TEXT;
            //view.Appearance.FocusedRow.Options.UseAll = true;
            view.Appearance.HeaderPanel.BackColor = C_CARD;
            view.Appearance.HeaderPanel.ForeColor = C_GOLD;
            view.Appearance.HeaderPanel.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
            //view.Appearance.HeaderPanel.Options.UseAll = true;
            view.Appearance.FooterPanel.BackColor = C_CARD;
            view.Appearance.FooterPanel.ForeColor = C_GOLD;
            view.Appearance.FooterPanel.Font = new Font("Courier New", 8.5f, FontStyle.Bold);
            //view.Appearance.FooterPanel.Options.UseAll = true;
        }

        private LabelControl MakeCap(string text, int x, int y)
            => new LabelControl
            {
                Text = text.ToUpper(),
                Font = F_CAP,
                ForeColor = C_MUTED,
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(x, y),
                Size = new Size(244, 14),
                BackColor = Color.Transparent,
            };

        private void StyleLU(SearchLookUpEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
            //c.Properties.Appearance.Options.UseAll = true;
        }

        private void StyleDE(DateEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
            //c.Properties.Appearance.Options.UseAll = true;
        }

    }

}
