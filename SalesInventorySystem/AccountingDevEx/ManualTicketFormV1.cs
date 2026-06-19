using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class ManualTicketFormV1 : XtraForm
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

        // ── Fonts ────────────────────────────────────────────────────
        private static readonly Font F_SMALL = new Font("Segoe UI", 8.5f);
        private static readonly Font F_MONO = new Font("Courier New", 9f);
        private static readonly Font F_BOLD = new Font("Segoe UI", 9f, FontStyle.Bold);
        private static readonly Font F_TITLE = new Font("Georgia", 14f, FontStyle.Bold);
        private static readonly Font F_CAP = new Font("Courier New", 7f, FontStyle.Bold);

        // ── State ─────────────────────────────────────────────────────
        private string _branch = Login.assignedBranch;
        private int _selectedManualID = 0;
        private long _batchRefID = 0;
        private DataTable _legsTable;

        public ManualTicketFormV1()
        {
            InitializeComponent();
            ApplyThemeStyles();
            WireEvents();
            PopulateLookups();
            InitLegsTable();
            LoadPendingTickets();
        }

        private void ApplyThemeStyles()
        {
            this.BackColor = C_DARK;
            this.ForeColor = C_TEXT;

            // Apply fonts and colors mapped from your original layout code
            lblBannerTitle.Font = F_TITLE;
            lblBannerTitle.ForeColor = C_TEXT;
            lblBannerSub.ForeColor = C_MUTED;

            radSourceType.Properties.Appearance.BackColor = C_CARD;
            radSourceType.Properties.Appearance.ForeColor = C_TEXT;

            cmbAdjType.Properties.Appearance.BackColor = C_CARD;
            cmbAdjType.Properties.Appearance.ForeColor = C_TEXT;
            cmbAdjType.Properties.Items.AddRange(new[] { "CM — Credit Memo", "DM — Debit Memo", "REV — Reversal", "ADJ — Adjustment" });

            lblInvoiceBalance.Font = new Font("Courier New", 13f, FontStyle.Bold);
            lblInvoiceBalance.ForeColor = C_DR;

            StyleTE(txtAdjAmount);
            StyleTE(txtOrigTicket);
            StyleTE(txtDocRef);
            StyleTE(txtRemarks);
            StyleLU(cmbSupplier);
            StyleLU(cmbInvoice);
            StyleLU(cmbLegAccount);

            cmbAPImpact.Properties.Appearance.BackColor = C_CARD;
            cmbAPImpact.Properties.Appearance.ForeColor = C_TEXT;
            cmbAPImpact.Properties.Items.AddRange(new[] { "DECREASE", "INCREASE", "NONE" });

            cmbLegDC.Properties.Appearance.BackColor = C_CARD;
            cmbLegDC.Properties.Appearance.ForeColor = C_TEXT;
            cmbLegDC.Properties.Items.AddRange(new[] { "D — Debit", "C — Credit" });

            StyleTE(txtLegAmount);
            StyleTE(txtLegDesc);

            btnPost.Appearance.BackColor = C_GOLD; btnPost.Appearance.ForeColor = C_DARK;
            btnApprove.Appearance.BackColor = C_OK; btnApprove.Appearance.ForeColor = C_DARK;
            btnReject.Appearance.BackColor = Color.FromArgb(80, 20, 20); btnReject.Appearance.ForeColor = C_ERR;
            btnRefresh.Appearance.BackColor = C_CARD; btnRefresh.Appearance.ForeColor = C_MUTED;

            ApplyGridStyle(viewLegs);
            ApplyGridStyle(viewPending);
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
                    _selectedManualID = SafeInt(viewPending.GetRowCellValue(viewPending.FocusedRowHandle, "ManualTicketID"));
                    string status = viewPending.GetRowCellValue(viewPending.FocusedRowHandle, "Status")?.ToString() ?? "";
                    btnApprove.Enabled = status == "FOR APPROVAL";
                    btnReject.Enabled = status == "FOR APPROVAL";
                }
            };
        }

        private void OnSourceTypeChanged()
        {
            cmbInvoice.EditValue = null;
            lblInvoiceBalance.Text = "0.00";
            _batchRefID = 0;
            LoadInvoices();
            bool isExpense = GetSourceType() == "EXPENSE";
            lblInvoiceBalance.ForeColor = isExpense ? C_AMBER : C_DR;
        }

        private void OnTypeChanged()
        {
            string t = GetAdjType();
            txtOrigTicket.Enabled = t == "REV";
            cmbAPImpact.EditValue = t == "DM" ? "INCREASE" : t == "ADJ" ? "NONE" : "DECREASE";
        }

        // ================================================================
        // LOOKUPS
        // ================================================================
        private void PopulateLookups()
        {
            Database.displaySearchlookupEdit(
                "SELECT SupplierID, SupplierName FROM Supplier ORDER BY SupplierName",
                cmbSupplier, "SupplierID", "SupplierID");
            Database.displaySearchlookupEdit(
                "SELECT AccountCode, Description FROM ChartOfAccounts WHERE AccountType='D' ORDER BY AccountCode",
                cmbLegAccount, "AccountCode", "AccountCode");
        }

        private void LoadInvoices()
        {
            string suppID = cmbSupplier.EditValue?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(suppID)) return;

            if (GetSourceType() == "EXPENSE")
            {
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
                    $"SELECT SequenceNo, InvoiceNo, InvoiceDate, Balance, ActualCost, PayStatus " +
                    $"FROM APACCOUNTS WHERE SupplierID = '{suppID}' AND PayStatus <> 'PAID' ORDER BY InvoiceDate DESC",
                    cmbInvoice, "InvoiceNo", "InvoiceNo");
            }
        }

        private void LoadInvoiceBalance()
        {
            string suppID = cmbSupplier.EditValue?.ToString() ?? "";
            string invNo = cmbInvoice.EditValue?.ToString() ?? "";
            _batchRefID = 0;
            if (string.IsNullOrWhiteSpace(suppID) || string.IsNullOrWhiteSpace(invNo))
            {
                lblInvoiceBalance.Text = "0.00";
                return;
            }
            try
            {
                using (var con = Database.getConnection())
                {
                    con.Open();
                    decimal bal = 0m;
                    if (GetSourceType() == "EXPENSE")
                    {
                        using (var cmd = new SqlCommand(
                            "SELECT TOP 1 BatchReferenceID, " +
                            "(SELECT SUM(Balance) FROM ExpenseMaster em2 WHERE em2.SupplierID=em.SupplierID AND em2.InvoiceNo=em.InvoiceNo AND em2.BatchReferenceID=em.BatchReferenceID) AS TotalBalance " +
                            "FROM ExpenseMaster em WHERE SupplierID=@s AND InvoiceNo=@i ORDER BY TRN_SEQ_NO", con))
                        {
                            cmd.Parameters.AddWithValue("@s", suppID);
                            cmd.Parameters.AddWithValue("@i", invNo);
                            using (var rdr = cmd.ExecuteReader())
                            {
                                if (rdr.Read())
                                {
                                    _batchRefID = rdr["BatchReferenceID"] != DBNull.Value ? Convert.ToInt64(rdr["BatchReferenceID"]) : 0L;
                                    bal = rdr["TotalBalance"] != DBNull.Value ? Convert.ToDecimal(rdr["TotalBalance"]) : 0m;
                                }
                            }
                        }
                        lblInvoiceBalance.ForeColor = bal > 0 ? C_AMBER : C_ERR;
                    }
                    else // PURCHASE
                    {
                        using (var cmd = new SqlCommand("SELECT Balance FROM APACCOUNTS WHERE SupplierID=@s AND InvoiceNo=@i ORDER BY SequenceNo DESC", con))
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
                colDC.Width = 70;
                ApplyHeader(colDC);
                colDC.AppearanceCell.ForeColor = C_GOLD;
                colDC.AppearanceCell.Options.UseForeColor = true;
            }
            if (colAmt != null)
            {
                colAmt.Width = 120;
                ApplyHeader(colAmt);
                colAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colAmt.DisplayFormat.FormatString = "N2";
                colAmt.AppearanceCell.Font = F_MONO;
                colAmt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                colAmt.AppearanceCell.Options.UseFont = true;
                colAmt.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:N2}");
            }
        }

        private void BtnAddLeg_Click(object sender, EventArgs e)
        {
            string acct = cmbLegAccount.EditValue?.ToString() ?? "";
            string dc = cmbLegDC.Text.StartsWith("D") ? "D" : "C";
            string desc = txtLegDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(acct))
            { XtraMessageBox.Show("Select an account code."); return; }

            if (!decimal.TryParse(txtLegAmount.Text.Replace(",", ""), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var amt) || amt <= 0)
            { XtraMessageBox.Show("Enter a valid positive amount."); return; }

            _legsTable.Rows.Add(acct, dc, Math.Round(amt, 2), desc);
            decimal dr = 0, cr = 0;
            foreach (DataRow r in _legsTable.Rows)
            {
                if (r["DebitCredit"].ToString() == "D") dr += (decimal)r["Amount"];
                else cr += (decimal)r["Amount"];
            }
            string diff = Math.Abs(dr - cr) < 0.01m ? "✓ Balanced" : $"Difference: {dr - cr:N2}";
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

            if (string.IsNullOrWhiteSpace(suppID)) { XtraMessageBox.Show("Select a Supplier."); return; }
            if (string.IsNullOrWhiteSpace(invNo)) { XtraMessageBox.Show("Select an Invoice."); return; }
            if (_legsTable.Rows.Count < 2) { XtraMessageBox.Show("Add at least 2 GL legs (one Debit, one Credit)."); return; }
            if (!decimal.TryParse(txtAdjAmount.Text.Replace(",", ""), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var adjAmt) || adjAmt <= 0)
            { XtraMessageBox.Show("Enter a valid Adjustment Amount."); return; }

            decimal dr = 0, cr = 0;
            foreach (DataRow r in _legsTable.Rows)
            {
                if (r["DebitCredit"].ToString() == "D") dr += (decimal)r["Amount"];
                else cr += (decimal)r["Amount"];
            }

            if (Math.Abs(dr - cr) > 0.01m)
            {
                XtraMessageBox.Show($"GL legs are not balanced.\nDebit: {dr:N2}  Credit: {cr:N2}\nDifference: {dr - cr:N2}\n\nPlease correct the amounts.", "Not Balanced", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string typeLabel = adjType == "CM" ? "Credit Memo" : adjType == "DM" ? "Debit Memo" : adjType == "REV" ? "Reversal" : "Adjustment";

            if (XtraMessageBox.Show(
                $"Post {typeLabel} for Invoice {invNo}?\nSupplier: {suppID}\nAmount: ₱{adjAmt:N2}\nAP Impact: {impact}\n\nTicket will be submitted for supervisor approval.",
                "Confirm Post", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                SetStatus($"Posting {typeLabel}…", working: true);
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_PostManualTicket", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                    cmd.Parameters.Add("@TicketDate", SqlDbType.Date).Value = DateTime.Today;
                    cmd.Parameters.Add("@AdjustmentType", SqlDbType.VarChar, 5).Value = adjType;
                    cmd.Parameters.Add("@SupplierID", SqlDbType.VarChar, 50).Value = suppID;
                    cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 80).Value = invNo;
                    cmd.Parameters.Add("@AdjustmentAmount", SqlDbType.Decimal).Value = Math.Round(adjAmt, 2);
                    cmd.Parameters.Add("@APBalanceImpact", SqlDbType.VarChar, 10).Value = impact;
                    cmd.Parameters.Add("@DocumentRef", SqlDbType.VarChar, 100).Value = string.IsNullOrEmpty(docRef) ? (object)DBNull.Value : docRef;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 2000).Value = string.IsNullOrEmpty(remarks) ? (object)DBNull.Value : remarks;
                    cmd.Parameters.Add("@SourceType", SqlDbType.VarChar, 10).Value = GetSourceType();
                    cmd.Parameters.Add("@BatchReferenceID", SqlDbType.BigInt).Value = _batchRefID > 0 ? (object)_batchRefID : DBNull.Value;
                    cmd.Parameters.Add("@OriginalTicketNo", SqlDbType.VarChar, 20).Value = string.IsNullOrEmpty(origTkt) ? (object)DBNull.Value : origTkt;
                    cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar, 50).Value = Login.Fullname;

                    var legTvp = new DataTable();
                    legTvp.Columns.Add("AccountCode", typeof(string));
                    legTvp.Columns.Add("DebitCredit", typeof(string));
                    legTvp.Columns.Add("Amount", typeof(decimal));
                    legTvp.Columns.Add("Description", typeof(string));
                    foreach (DataRow r in _legsTable.Rows) legTvp.ImportRow(r);

                    var tvpParam = cmd.Parameters.AddWithValue("@Legs", legTvp);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.ManualTicketLegTVP";

                    con.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            string tktNum = rdr["TicketNumber"]?.ToString() ?? "";
                            XtraMessageBox.Show($"Ticket {tktNum} posted successfully.\nStatus: FOR APPROVAL\n\nA supervisor must approve before APACCOUNTS is updated.", "Posted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

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
                XtraMessageBox.Show($"Post failed:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================
        // APPROVE / REJECT
        // ================================================================
        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (_selectedManualID <= 0) return;
            string tktNum = viewPending.GetRowCellValue(viewPending.FocusedRowHandle, "TicketNumber")?.ToString() ?? "";
            string inv = viewPending.GetRowCellValue(viewPending.FocusedRowHandle, "InvoiceNo")?.ToString() ?? "";
            decimal amt = SafeDecimal(viewPending.GetRowCellValue(viewPending.FocusedRowHandle, "AdjustmentAmount"));
            string impact = viewPending.GetRowCellValue(viewPending.FocusedRowHandle, "APBalanceImpact")?.ToString() ?? "";

            if (XtraMessageBox.Show($"APPROVE Ticket {tktNum}?\nInvoice: {inv}  Amount: ₱{amt:N2}\nAP Balance will {impact}.\n\nThis will update APACCOUNTS balance immediately.", "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_ApproveManualTicket", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ManualTicketID", SqlDbType.Int).Value = _selectedManualID;
                    cmd.Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 50).Value = Login.Fullname;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadPendingTickets();
                SetStatus($"Ticket {tktNum} approved — APACCOUNTS updated.");
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            catch (SqlException ex) { SetStatus($"Approve failed: {ex.Message}", error: true); }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (_selectedManualID <= 0) return;
            string reason = XtraInputBox.Show("Enter rejection reason:", "Reject Ticket", "");
            if (string.IsNullOrWhiteSpace(reason)) return;

            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("sp_RejectManualTicket", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ManualTicketID", SqlDbType.Int).Value = _selectedManualID;
                    cmd.Parameters.Add("@RejectedBy", SqlDbType.VarChar, 50).Value = Login.Fullname;
                    cmd.Parameters.Add("@RejectionReason", SqlDbType.VarChar, 500).Value = reason;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadPendingTickets();
                SetStatus("Ticket rejected and voided.");
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            catch (SqlException ex) { SetStatus($"Reject failed: {ex.Message}", error: true); }
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
                    cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar, 5).Value = _branch;
                    cmd.Parameters.Add("@SupplierID", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    cmd.Parameters.Add("@SourceType", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.Date).Value = (object)DateTime.Today.AddMonths(-2);
                    cmd.Parameters.Add("@DateTo", SqlDbType.Date).Value = (object)DateTime.Today;
                    con.Open();
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    viewPending.Columns.Clear();
                    gridPending.DataSource = dt;
                    FormatPendingGrid();
                }
            }
            catch (Exception ex) { SetStatus($"Load failed: {ex.Message}", error: true); }
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
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
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

        private string GetSourceType() => (int)(radSourceType?.EditValue ?? 0) == 1 ? "EXPENSE" : "PURCHASE";

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
            v.Appearance.EvenRow.BackColor = C_SURFACE;
            v.Appearance.EvenRow.Options.UseBackColor = true;
            v.Appearance.FocusedRow.BackColor = C_BORDER;
            v.Appearance.HeaderPanel.BackColor = C_CARD;
            v.Appearance.HeaderPanel.ForeColor = C_GOLD;
            v.Appearance.HeaderPanel.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
            v.Appearance.FooterPanel.BackColor = C_CARD;
            v.Appearance.FooterPanel.ForeColor = C_GOLD;
        }

        private void ApplyHeader(GridColumn col)
        {
            col.AppearanceHeader.BackColor = C_CARD;
            col.AppearanceHeader.ForeColor = C_GOLD;
            col.AppearanceHeader.Font = new Font("Courier New", 7.5f, FontStyle.Bold);
        }

        private void StyleLU(SearchLookUpEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
        }

        private void StyleTE(TextEdit c)
        {
            c.Properties.Appearance.BackColor = C_CARD;
            c.Properties.Appearance.ForeColor = C_TEXT;
        }

        private static decimal SafeDecimal(object v) => v == null || v == DBNull.Value ? 0m : decimal.TryParse(v.ToString().Replace(",", ""), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var r) ? r : 0m;

        private static int SafeInt(object v) => v == null || v == DBNull.Value ? 0 : int.TryParse(v.ToString(), out var r) ? r : 0;
    }
}