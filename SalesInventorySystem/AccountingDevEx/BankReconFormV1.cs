using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class BankReconFormV1 : XtraForm
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

        // ── Fonts ────────────────────────────────────────────────────
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

        public BankReconFormV1()
        {
            InitializeComponent();
            ApplyThemeStyles();
            WireEvents();
            PopulateLookups();
            SetDefaultPeriod();
        }

        private void ApplyThemeStyles()
        {
            this.BackColor = C_DARK;
            this.ForeColor = C_TEXT;

            // Panels
            panelBanner.BackColor = C_SURFACE;
            panelFilter.BackColor = C_CARD;
            panelHeader.BackColor = C_SURFACE;
            panelToolbar.BackColor = C_CARD;
            splitContainerControl1.BackColor = C_DARK;
            splitContainerControl1.Panel1.BackColor = C_DARK;
            splitContainerControl1.Panel2.BackColor = C_SURFACE;

            // Labels
            lblTitle.Font = F_TITLE;
            lblTitle.ForeColor = C_TEXT;

            lblBranch.Text = $"Branch: {_branch}";
            lblBranch.Font = new Font("Courier New", 9f, FontStyle.Bold);
            lblBranch.ForeColor = C_GOLD;

            lblBookBal.Font = new Font("Courier New", 15f, FontStyle.Bold);
            lblBookBal.ForeColor = C_DR;

            lblStatus.Font = new Font("Courier New", 7.5f);
            lblStatus.ForeColor = C_MUTED;

            // TextEdit & Lookups
            txtBankBal.Font = F_MONO;
            txtBankBal.Properties.Appearance.BackColor = C_CARD;
            txtBankBal.Properties.Appearance.ForeColor = C_TEXT;

            dtPeriod.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            dtPeriod.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            StyleDate(dtPeriod);
            StyleLookUp(cmbBranch);
            StyleLookUp(cmbAccount);

            // Buttons
            btnLoad.Appearance.BackColor = C_GOLD; btnLoad.Appearance.ForeColor = C_DARK;
            btnSaveHeader.Appearance.BackColor = C_CARD; btnSaveHeader.Appearance.ForeColor = C_MUTED;

            btnAdd.Appearance.BackColor = C_GOLD; btnAdd.Appearance.ForeColor = C_DARK;
            btnEdit.Appearance.BackColor = C_CARD; btnEdit.Appearance.ForeColor = C_MUTED;
            btnResolve.Appearance.BackColor = C_CARD; btnResolve.Appearance.ForeColor = C_MUTED;
            btnDelete.Appearance.BackColor = Color.FromArgb(80, 20, 20); btnDelete.Appearance.ForeColor = C_ERR;
            btnAutoMatch.Appearance.BackColor = C_CARD; btnAutoMatch.Appearance.ForeColor = C_MUTED;
            btnPrint.Appearance.BackColor = C_CARD; btnPrint.Appearance.ForeColor = C_MUTED;

            // Grids
            viewItems.OptionsView.ShowGroupPanel = false;
            viewItems.OptionsView.ShowIndicator = false;
            viewItems.OptionsView.ShowFooter = true;
            viewItems.OptionsView.EnableAppearanceEvenRow = true;
            viewItems.OptionsBehavior.Editable = false;

            viewItems.Appearance.HeaderPanel.BackColor = C_CARD;
            viewItems.Appearance.HeaderPanel.ForeColor = C_GOLD;
            viewItems.Appearance.HeaderPanel.Font = new Font("Courier New", 8f, FontStyle.Bold);

            viewItems.Appearance.Row.BackColor = C_DARK;
            viewItems.Appearance.Row.ForeColor = C_TEXT;
            viewItems.Appearance.EvenRow.BackColor = C_SURFACE;
            viewItems.Appearance.FocusedRow.BackColor = C_BORDER;
            viewItems.Appearance.FocusedRow.ForeColor = C_TEXT;

            viewItems.Appearance.FooterPanel.BackColor = C_CARD;
            viewItems.Appearance.FooterPanel.ForeColor = C_GOLD;
            viewItems.Appearance.FooterPanel.Font = new Font("Courier New", 8.5f, FontStyle.Bold);

            // Init Disables
            btnEdit.Enabled = false;
            btnResolve.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = btnAutoMatch.Enabled = btnPrint.Enabled = false;
        }

        // ================================================================
        // WIRE EVENTS
        // ================================================================
        private void WireEvents()
        {
            btnLoad.Click += (s, e) => LoadRecon();
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
                    _selID = SafeInt(viewItems.GetRowCellValue(viewItems.FocusedRowHandle, "ReconID"));
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
                "SELECT AccountCode, Description FROM ChartOfAccounts WHERE AccountCode LIKE '10102%' AND AccountType='D' ORDER BY AccountCode",
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
            _period = dtPeriod.EditValue is DateTime dt ? dt : DateTime.TryParse(dtPeriod.Text, out var pd) ? pd : DateTime.Today;

            if (string.IsNullOrWhiteSpace(_account))
            {
                XtraMessageBox.Show("Please select a bank GL account.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch (SqlException ex) { SetStatus($"Load failed: {ex.Message}", err: true); }
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
                {
                    if (r.Read())
                    {
                        _bookBal = SafeDec(r["BookBalance"]);
                        _bankBal = SafeDec(r["BankStatementBalance"]);
                        lblBookBal.Text = _bookBal.ToString("N2");
                        txtBankBal.Text = _bankBal.ToString("N2");
                    }
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

        private void FormatCol(string field, int width, bool money, bool isDate = false)
        {
            var col = viewItems.Columns[field];
            if (col == null) return;
            col.Width = width;
            col.AppearanceHeader.ForeColor = C_GOLD;
            col.AppearanceHeader.Font = new Font("Courier New", 7.5f, FontStyle.Bold);

            if (money)
            {
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = "N2";
                col.AppearanceCell.Font = F_MONO;
                col.AppearanceCell.ForeColor = C_DR;
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
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
            {
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
                SetLabel(lblDiff, Math.Abs(diff).ToString("N2") + "  ⚠ OUT OF BALANCE", C_ERR);
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
        // CRUD HANDLERS
        // ================================================================
        private void BtnSaveHeader_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtBankBal.Text.Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out var bal))
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
            using (var dlg = new BankReconItemForm(isNew: true) { BankStatementBal = _bankBal })
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
            if (XtraMessageBox.Show("Mark this item as RESOLVED (cleared by bank)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
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
            if (XtraMessageBox.Show("Delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
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
            if (XtraMessageBox.Show("Auto-match outstanding checks against GL payments?\nMatching OC items will be marked Resolved.", "Auto-Match", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
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
            XtraMessageBox.Show("Wire to your XtraReport template here.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void StyleLookUp(SearchLookUpEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
        }

        private void StyleDate(DateEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
        }

        private static decimal SafeDec(object v) => v == null || v == DBNull.Value ? 0m : decimal.TryParse(v.ToString().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out var r) ? r : 0m;
        private static int SafeInt(object v) => v == null || v == DBNull.Value ? 0 : int.TryParse(v.ToString(), out var r) ? r : 0;
        private static DateTime SafeDate(object v) => v is DateTime dt ? dt : DateTime.TryParse(v?.ToString(), out var r) ? r : DateTime.Today;
    }
}