using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SalesInventorySystem.Classes;
using System.Globalization;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class SupplierPaymentDevEx : DevExpress.XtraEditors.XtraForm
    { // ── State ────────────────────────────────────────────────────────────
        private string _referenceNo = "";
        private string _voucherId = "";
        private string _voucherType = "";
        private string _payMethod = "";  // "PURCHASE" | "EXPENSE"
         
        // ✅ Put this near the top with your fields (referenceno, voucherid, etc.)
        private class PaymentLine
        {
            public long SequenceNumber { get; set; }
            public long BatchReferenceID { get; set; }
            public string BranchCode { get; set; }
            public string InvoiceNo { get; set; }
            public string SequenceReferenceNumber { get; set; }
            public DateTime InvoiceDate { get; set; }
            public decimal ActualCost { get; set; }
            public decimal AmountPaid { get; set; }
            public decimal Balance { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal EWTAmount { get; set; }
            public decimal ReturnAllowances { get; set; }
            public decimal OffsetAmount { get; set; }
            public string Description { get; set; }
        }

        private int _loadedYear = 0;
        private int _currentYearSequence = 0;
        private void GenerateVoucherNumber()
        {
            // 1. Ensure we have a date selected before building the prefix
            if (txtcheckdate.EditValue == null || !DateTime.TryParse(txtcheckdate.EditValue.ToString(), out DateTime checkDate))
            {
                txtcheckno.Text = ""; // Clear if no date
                return;
            }

            //DateTime checkDate = Convert.ToDateTime(txtcheckdate.EditValue);
            int year = checkDate.Year;

            // 2 & 3. Get the 2-digit Year and 2-digit Month
            string yy = checkDate.ToString("yy"); // e.g., "26"
            string mm = checkDate.ToString("MM"); // e.g., "06"

            // 4. Get the incremental sequence for the year
            // We only query the database if the year changes to prevent UI lag
            if (_loadedYear != year)
            {
                _currentYearSequence = GetNextSequenceForYear(year);
                _loadedYear = year;
            }
            string seq = _currentYearSequence.ToString("D3"); // Pads to 3 digits, e.g., "006"

            // 5. Get the Bank Code
            // If cmbBank is a SearchLookUpEdit, .Text will grab the display member (e.g., "BDO").
            // If you need a specific column, use: cmbBank.Properties.View.GetFocusedRowCellValue("BankCode")?.ToString();
            string bankCodeStr = searchLookUpEdit1.EditValue?.ToString();//objbankcode?.ToString();

            //string selectedAccountCode = searchLookUpEdit1.EditValue?.ToString();
            string bank = String.IsNullOrEmpty(bankCodeStr) ? "" : GetBank(bankCodeStr);

            // 6. Get the Actual Check Number
            string actualCheck = GetLastCheckNo();

            // 7. Assemble the final string
            txtcheckno.Text = $"CV{yy}{mm}-{seq}{bank}{actualCheck}";
        }
        private string GetLastCheckNo()
        {
            return Database.getSingleQuery($"SELECT TOP(1) ISNULL(RIGHT(CheckNo,9),'') AS CheckNo FROM dbo.CheckVoucher ORDER BY SequenceNumber DESC", "CheckNo");
        }
        private string GetBank(string AcctCode)
        {
            // Grab the value and safely convert it to a string
            return Database.getSingleQuery($"SELECT TOP(1) Bank FROM dbo.BankCoa WHERE AccountCode='{AcctCode}'", "Bank");
        }
        private void WireEvents()
        {
            // Trigger the generator whenever the Date, Bank, or Check Number changes
            txtcheckdate.EditValueChanged += (s, e) => GenerateVoucherNumber();
            searchLookUpEdit1.EditValueChanged += (s, e) => GenerateVoucherNumber();
            txtcheckno.TextChanged += (s, e) => GenerateVoucherNumber();
        }
        // ── Database Helper ──────────────────────────────────────────
        private int GetNextSequenceForYear(int year)
        {
            try
            {
                using (var con = Database.getConnection())
                {
                    // Counts all checks issued in the selected year based on your CheckVoucher table
                    string query = "SELECT COUNT(*) FROM CheckVoucher WHERE YEAR(CheckDate) = @Year";

                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Year", year);
                        con.Open();

                        object result = cmd.ExecuteScalar();
                        int currentCount = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;

                        // Return the next available number
                        return currentCount + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error fetching check sequence: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1; // Default to 1 if it fails
            }
        }
        // ✅ Put this below PaymentLine (order doesn’t matter as long as both are inside the class)
        private List<PaymentLine> GetSelectedLines()
        {
            var lines = new List<PaymentLine>();

            for (int i = 0; i < gridViewMaster.RowCount; i++)
            {
                bool pay = ToBool(gridViewMaster.GetRowCellValue(i, "Pay"));
                if (!pay) continue;

                lines.Add(new PaymentLine
                {
                    SequenceNumber = Convert.ToInt64(gridViewMaster.GetRowCellValue(i, "SequenceNumber") ?? 0),
                    BatchReferenceID = Convert.ToInt64(gridViewMaster.GetRowCellValue(i, "BatchReferenceID") ?? 0),
                    BranchCode = Convert.ToString(gridViewMaster.GetRowCellValue(i, "BranchCode") ?? ""),
                    InvoiceNo = Convert.ToString(gridViewMaster.GetRowCellValue(i, "InvoiceNo") ?? ""),
                    SequenceReferenceNumber = Convert.ToString(gridViewMaster.GetRowCellValue(i, "SequenceReferenceNumber") ?? ""),
                    InvoiceDate = Convert.ToDateTime(gridViewMaster.GetRowCellValue(i, "InvoiceDate") ?? DateTime.MinValue),
                    ActualCost = Convert.ToDecimal(gridViewMaster.GetRowCellValue(i, "ActualCost") ?? 0m), //NET
                    AmountPaid = Convert.ToDecimal(gridViewMaster.GetRowCellValue(i, "AmountPaid") ?? 0m), //NET
                    Balance = SafeToDecimal(gridViewMaster.GetRowCellValue(i, "Balance")),
                    DiscountAmount = Convert.ToDecimal(gridViewMaster.GetRowCellValue(i, "DiscountAmount") ?? 0m),
                    EWTAmount = Convert.ToDecimal(gridViewMaster.GetRowCellValue(i, "EWTAmount") ?? 0m),
                    ReturnAllowances = Convert.ToDecimal(gridViewMaster.GetRowCellValue(i, "ReturnAllowances") ?? 0m),
                    OffsetAmount = Convert.ToDecimal(gridViewMaster.GetRowCellValue(i, "OffsetAmount") ?? 0m),
                    Description = Convert.ToString(gridViewMaster.GetRowCellValue(i, "Description") ?? "")
                });
            }

            return lines;
        }
        private static decimal SafeToDecimal(object value)
        {
            if (value == null || value == DBNull.Value) return 0m;

            if (value is decimal d) return d;
            if (value is double db) return (decimal)db;
            if (value is float f) return (decimal)f;
            if (value is int i) return i;
            if (value is long l) return l;

            var s = value.ToString().Trim();

            // remove thousands separators
            s = s.Replace(",", "");

            if (decimal.TryParse(s,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var result))
            {
                return result;
            }

            throw new InvalidCastException($"Cannot convert value '{value}' to decimal.");
        }

        
        public static bool isdone = false, forliquidation = false;
        public SupplierPaymentDevEx()
        {
            InitializeComponent();
            WireEvents();
            repositoryItemCheckEditStat.EditValueChanged += RepositoryItemCheckEditStat_EditValueChanged;
        }
        private void RepositoryItemCheckEditStat_EditValueChanged(object sender, EventArgs e)
        {
            gridViewMaster.PostEditor();        // commit immediately
            gridViewMaster.UpdateCurrentRow();  // trigger CellValueChanged
        }


        void radChanged()
        {
            if (radCashVoucher.Checked == true)
            {
                panelCheckVoucher.Visible = false;
            }
            else if (radCheckVoucher.Checked == true)
            {
                panelCheckVoucher.Visible = true;
            }
        }

        private void SupplierPaymentDevEx_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;

            dateFrom.Text = HelperFunction.GetPreviousMonthSameDay(today).ToShortDateString();
            dateTo.Text = today.ToShortDateString();

            //display();
            populateCOA();
            radChanged();
        }
        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", searchLookUpEdit1, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditoffsetdebitglcode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditoffsetCreditGLCode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditdiscountglcode, "AccountCode", "AccountCode");
        }
        
        private void btnadd_Click(object sender, EventArgs e)
        {

            var lines = GetSelectedLines();
            if (lines.Count == 0)
            {
                XtraMessageBox.Show("No Payments Executed!");
                return;
            }

            // validate totals per invoice
            foreach (var ln in lines)
            {
                decimal balance = Convert.ToDecimal(
                    gridViewMaster.GetRowCellValue(
                        gridViewMaster.LocateByValue("InvoiceNo", ln.InvoiceNo), "Balance") ?? 0m);

                var total = ln.AmountPaid + ln.EWTAmount + ln.DiscountAmount + ln.ReturnAllowances;
                if (total > ln.Balance)
                {
                    XtraMessageBox.Show($"Invoice {ln.InvoiceNo}: total payment exceeds balance.");
                    return;
                }

            }

            PostSupplierPayment(lines); // ONE call
            populate(); 
        }

        
        private void populate()
        {
            if (!DateTime.TryParse(dateFrom.Text, out var fromDate)) fromDate = DateTime.Today.AddMonths(-1);
            if (!DateTime.TryParse(dateTo.Text, out var toDate)) toDate = DateTime.Today;

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("splist_Accounts", con)
            { CommandType = CommandType.StoredProcedure, CommandTimeout = 3600 })
            {
                cmd.Parameters.Add("@parmdatefrom", SqlDbType.Date).Value = fromDate;
                cmd.Parameters.Add("@parmdateto", SqlDbType.Date).Value = toDate;
                cmd.Parameters.Add("@parmsupplierid", SqlDbType.VarChar, 30).Value = txtsupplierid.Text.Trim();
                cmd.Parameters.Add("@parmispurchase", SqlDbType.Bit).Value = radioButtonPurchase.Checked;
                cmd.Parameters.Add("@parmisexpense", SqlDbType.Bit).Value = radioButtonExpense.Checked;

                var table = new DataTable();
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    UseWaitCursor = true;
                    con.Open();
                    new SqlDataAdapter(cmd).Fill(table);

                    gridControlMaster.BeginUpdate();
                    gridViewMaster.Columns.Clear();
                    gridControlMaster.DataSource = table;
                    gridViewMaster.BestFitColumns();
                    FormatGridColumns();
                }
                catch (SqlException ex)
                {
                    XtraMessageBox.Show($"Error retrieving accounts: {ex.Message}",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    gridControlMaster.EndUpdate();
                    UseWaitCursor = false;
                    Cursor.Current = Cursors.Default;
                }
            }
        }   
        // Helper method to handle DevExpress UI formatting
        private void FormatGridColumns()
        {
            if (gridViewMaster.Columns["ActualCost"] != null)
            {
                gridViewMaster.Columns["ActualCost"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewMaster.Columns["ActualCost"].DisplayFormat.FormatString = "n2";
            }

            if (gridViewMaster.Columns["Balance"] != null)
            {
                gridViewMaster.Columns["Balance"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewMaster.Columns["Balance"].DisplayFormat.FormatString = "n2";
            }
        }

        private void OpenPaymentDialogForRow(int rowHandle)
        {
            using (var dlg = new SupplierAddPaymentDevEx())
            {
                dlg.txtshipno.Text = gridViewMaster.GetRowCellValue(rowHandle, "ShipmentNo")?.ToString();
                dlg.txtinvoiceno.Text = gridViewMaster.GetRowCellValue(rowHandle, "InvoiceNo")?.ToString();
                dlg.txtinvoicedate.Text = gridViewMaster.GetRowCellValue(rowHandle, "InvoiceDate")?.ToString();
                dlg.txtactualcost.Text = gridViewMaster.GetRowCellValue(rowHandle, "ActualCost")?.ToString();
                dlg.txtbalance.Text = gridViewMaster.GetRowCellValue(rowHandle, "Balance")?.ToString();
                dlg.groupControl1.Text = $"{txtsupplierid.Text}-{txtsuppliername.Text}";

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    gridViewMaster.SetRowCellValue(rowHandle, "AmountPaid", dlg.AmountPaid);
                    gridViewMaster.SetRowCellValue(rowHandle, "DiscountAmount", dlg.Discount);
                    gridViewMaster.SetRowCellValue(rowHandle, "EWTAmount", dlg.EWT);
                    gridViewMaster.SetRowCellValue(rowHandle, "ReturnAllowances", dlg.Offset);
                }
                else
                {
                    // User cancelled: uncheck Pay
                    gridViewMaster.SetRowCellValue(rowHandle, "Pay", false);
                }
            }
        }
        private DataTable BuildPaymentLinesTVP(List<PaymentLine> lines)
        {
            var dt = new DataTable();
            dt.Columns.Add("BranchCode", typeof(string));
            dt.Columns.Add("InvoiceNo", typeof(string));
            dt.Columns.Add("InvoiceDate", typeof(DateTime));
            dt.Columns.Add("SequenceReferenceNumber", typeof(string));
            dt.Columns.Add("BatchReferenceID", typeof(long));
            dt.Columns.Add("ActualCost", typeof(decimal));
            dt.Columns.Add("AmountPaid", typeof(decimal));
            dt.Columns.Add("EWTAmount", typeof(decimal));
            dt.Columns.Add("DiscountAmount", typeof(decimal));
            dt.Columns.Add("ReturnAllowances", typeof(decimal));
            dt.Columns.Add("Description", typeof(string));

            foreach (var ln in lines)
            {
                dt.Rows.Add(
                    ln.BranchCode,
                    ln.InvoiceNo,
                    ln.InvoiceDate,
                    ln.SequenceNumber,          // note: string
                    ln.BatchReferenceID,
                    ln.ActualCost,
                    ln.AmountPaid,
                    ln.EWTAmount,
                    ln.DiscountAmount,
                    ln.ReturnAllowances,
                    ln.Description
                );
              

            }
            return dt;
        }
        private void PostSupplierPayment(List<PaymentLine> lines)
        {
            // Generate IDs
            _referenceNo = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            _voucherId = IDGenerator.getIDNumberSP("sp_GetVoucherNumber", "TicketNumber");
            _voucherType = radCheckVoucher.Checked ? "CHECK" : "CASH";
            _payMethod = radioButtonPurchase.Checked ? "PURCHASE" : "EXPENSE";

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("sp_AddPaymentSupplierCompound", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 180;

                // ── scalar parameters ─────────────────────────────────────
                cmd.Parameters.Add("@parmrefno", SqlDbType.VarChar, 10).Value = _referenceNo;
                cmd.Parameters.Add("@parmvoucherid", SqlDbType.VarChar, 10).Value = _voucherId;
                cmd.Parameters.Add("@parmsupplierid", SqlDbType.VarChar, 50).Value = txtsupplierid.Text.Trim();
                cmd.Parameters.Add("@parmsuppliername", SqlDbType.VarChar, 150).Value = txtsuppliername.Text.Trim();
                cmd.Parameters.Add("@parmcheckamount", SqlDbType.Decimal).Value =
                    decimal.Parse(txtamounttopay.Text.Replace(",", ""), CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@parmcheckno", SqlDbType.VarChar, 50).Value = txtcheckno.Text.Trim();
                cmd.Parameters.Add("@parmcheckdate", SqlDbType.Date).Value =
                    string.IsNullOrWhiteSpace(txtcheckdate.Text)
                        ? (object)DBNull.Value
                        : DateTime.Parse(txtcheckdate.Text);
                cmd.Parameters.Add("@parmcheckremarks", SqlDbType.VarChar, 2000).Value = txtremakrs.Text.Trim();
                cmd.Parameters.Add("@parmpreparedby", SqlDbType.VarChar, 30).Value = Login.Fullname;
                cmd.Parameters.Add("@parmglcode", SqlDbType.VarChar, 30).Value = searchLookUpEdit1.Text.Trim();
                cmd.Parameters.Add("@parmpaymethod", SqlDbType.VarChar, 20).Value = _payMethod;
                cmd.Parameters.Add("@parmforliquidation", SqlDbType.Bit).Value = checkforliquidation.Checked;
                cmd.Parameters.Add("@parmvouchertype", SqlDbType.VarChar, 10).Value = _voucherType;

                // ── TVP parameter ─────────────────────────────────────────
                var tvpParam = cmd.Parameters.AddWithValue("@Lines", BuildPaymentLinesTVP(lines));
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "dbo.AP_PaymentLineTVP";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BigAlert.Show("SUCCESS", "Payment successfully posted.", MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    XtraMessageBox.Show($"Payment failed:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private static bool ToBool(object value)
        {
            if (value == null || value == DBNull.Value) return false;

            if (value is bool b) return b;

            var s = value.ToString().Trim();

            if (string.Equals(s, "true", StringComparison.OrdinalIgnoreCase)) return true;
            if (string.Equals(s, "false", StringComparison.OrdinalIgnoreCase)) return false;

            // common alternatives
            if (s == "1" || s.Equals("y", StringComparison.OrdinalIgnoreCase) || s.Equals("yes", StringComparison.OrdinalIgnoreCase))
                return true;

            if (s == "0" || s.Equals("n", StringComparison.OrdinalIgnoreCase) || s.Equals("no", StringComparison.OrdinalIgnoreCase))
                return false;

            // your case: "NONE" should mean unchecked
            if (s.Equals("none", StringComparison.OrdinalIgnoreCase) || s == "") return false;

            // last resort: try parse
            if (bool.TryParse(s, out var parsed)) return parsed;

            return false;
        }

        private void UpdateTotalAmountToPay()
        {
            decimal total = 0;

            for (int i = 0; i < gridViewMaster.RowCount; i++)
            {
                if (ToBool(gridViewMaster.GetRowCellValue(i, "Pay")))
                {
                    total += ToDecimal(gridViewMaster.GetRowCellValue(i, "AmountPaid"));
                }
            }

            txtamounttopay.Text = total.ToString("N2");
        }
        private void ResetRowPayment(int rowHandle)
        {
            // Turn off amounts when Pay is unchecked
            gridViewMaster.SetRowCellValue(rowHandle, "AmountPaid", 0m);
            gridViewMaster.SetRowCellValue(rowHandle, "DiscountAmount", 0m);
            gridViewMaster.SetRowCellValue(rowHandle, "EWTAmount", 0m);
            gridViewMaster.SetRowCellValue(rowHandle, "ReturnAllowances", 0m);

            // Optional: reset other fields if you have them
            // gridViewMaster.SetRowCellValue(rowHandle, "Variance", 0m);
            // gridViewMaster.SetRowCellValue(rowHandle, "Pay", false); // not needed here usually
        }
        private void InitializeRowPayment(int rowHandle)
        {
            decimal balance = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "Balance"));

            // Default values
            gridViewMaster.SetRowCellValue(rowHandle, "EWTAmount", 0m);
            gridViewMaster.SetRowCellValue(rowHandle, "DiscountAmount", 0m);
            gridViewMaster.SetRowCellValue(rowHandle, "ReturnAllowances", 0m);

            // AmountPaid starts as full balance
            gridViewMaster.SetRowCellValue(rowHandle, "AmountPaid", balance);
        }
        private void RecalculateRowAmount(int rowHandle)
        {
            decimal balance = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "Balance"));
            decimal ewt = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "EWTAmount"));
            decimal discount = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "DiscountAmount"));
            decimal offset = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "ReturnAllowances"));

            decimal totalDeduction = ewt + discount + offset;

            if (totalDeduction > balance)
            {
                ShowRowError(rowHandle, "Total deductions exceed Balance");
                return;
            }

            decimal newAmount = balance - totalDeduction;

            if (newAmount < 0)
                newAmount = 0;

            gridViewMaster.SetRowCellValue(rowHandle, "AmountPaid", newAmount);

            gridViewMaster.ClearColumnErrors();
        }

        private void MoveToNextEditableCell(int rowHandle)
        {
            gridViewMaster.FocusedRowHandle = rowHandle;

            // Decide which field to focus
            decimal ewt = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "EWTAmount"));

            var nextColumn = (ewt == 0)
                ? gridViewMaster.Columns["AmountPaid"]
                : gridViewMaster.Columns["EWTAmount"];

            gridViewMaster.FocusedColumn = nextColumn;
            gridViewMaster.ShowEditor();
        }

        private void gridViewMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            string col = e.Column.FieldName;

            if (col == "Pay")
            {
                bool isChecked = Convert.ToBoolean(e.Value);

                if (isChecked)
                    InitializeRowPayment(e.RowHandle);
                else
                    ResetRowPayment(e.RowHandle);

                UpdateTotalAmountToPay();
                return;
            }

            // ✅ Only recalc IF already checked
            if (col == "EWTAmount" || col == "DiscountAmount" || col == "ReturnAllowances")
            {
                if (!ToBool(gridViewMaster.GetRowCellValue(e.RowHandle, "Pay")))
                    return; // ✅ DO NOTHING if not checked

                RecalculateRowAmount(e.RowHandle);
                UpdateTotalAmountToPay();
            }

            if (col == "AmountPaid")
            {
                UpdateTotalAmountToPay();
            }

            

        }

        private decimal ToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0m;

            decimal.TryParse(value.ToString(), out decimal result);
            return result;
        }

        private void gridViewMaster_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Pay")
                e.RepositoryItem = repositoryItemCheckEditStat;
        }

        private void gridViewMaster_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            bool isChecked = ToBool(gridViewMaster.GetRowCellValue(e.RowHandle, "Pay"));

            if (isChecked)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }

        }
        private void ShowRowError(int rowHandle, string message)
        {
            gridViewMaster.SetColumnError(
                gridViewMaster.Columns["AmountPaid"],
                message);

            gridViewMaster.FocusedRowHandle = rowHandle;
        }
        private bool ValidateRowAmounts(int rowHandle)
        {
            decimal balance = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "Balance"));
            decimal ewt = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "EWTAmount"));
            decimal discount = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "DiscountAmount"));
            decimal offset = ToDecimal(gridViewMaster.GetRowCellValue(rowHandle, "ReturnAllowances"));

            decimal totalDeduction = ewt + discount + offset;

            if (ewt > balance)
            {
                ShowRowError(rowHandle, "EWT exceeds Balance");
                return false;
            }

            if (discount > balance)
            {
                ShowRowError(rowHandle, "Discount exceeds Balance");
                return false;
            }

            if (offset > balance)
            {
                ShowRowError(rowHandle, "Return Allowance exceeds Balance");
                return false;
            }

            if (totalDeduction > balance)
            {
                ShowRowError(rowHandle, "Total deductions exceed Balance");
                return false;
            }

            gridViewMaster.ClearColumnErrors();
            return true;
        }
        //USED TO EDIT CELL 
        private void gridViewMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            var editor = gridViewMaster.ActiveEditor;
            if (editor is DevExpress.XtraEditors.TextEdit textEdit)
            {
                textEdit.SelectAll();
            }
        }

        private void btnextract_Click(object sender, EventArgs e)
        {
            populate();

            gridViewMaster.Columns[0].Visible = false; 
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "ActualCost");
             
            if (radioButtonPurchase.Checked == true)
            {
                Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "AmountPaid"); 
                Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "DiscountAmount");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "EWTAmount");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "ReturnAllowances"); 
            }
            else
            {
            }
        }

        void printVoucher()
        {
            btnfilter.PerformClick();
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth = row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString();
                string caption1 = row["Caption1"].ToString();
                string caption2 = row["Caption2"].ToString();

                DevExReportTemplate.CheckVoucher xct = new DevExReportTemplate.CheckVoucher();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CheckVoucher'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;
                xct.xrcheckno.Text = txtcheckno.Text;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4; 
                double amounttopay = Convert.ToDouble(txtamounttopay.Text);

                string paytype = "";
                if (radioButtonPurchase.Checked == true) { paytype = "PURCHASE"; } else { paytype = "EXPENSE"; }

                xct.xrcheckdate.Text = txtcheckdate.Text;
                xct.xrpaytype.Text = paytype;
                xct.xrpaidto.Text = txtsuppliername.Text;
                xct.xrparticular.Text = txtremakrs.Text;
                xct.xramount.Text = String.Format("{0:0,0.00}", amounttopay);
              
                string str = Classes.DecimalToWordExtension.ToWords(Convert.ToDecimal(txtamounttopay.Text));

                gridViewMaster.Columns["Balance"].Visible = false;
                gridViewMaster.Columns["BranchCode"].Visible = false;
               
                if (paytype == "EXPENSE")
                {
                    gridViewMaster.Columns["BatchReferenceID"].Visible = false;
                    gridViewMaster.Columns["BatchReferenceID"].OptionsColumn.ShowInCustomizationForm = true;
                }

                gridViewMaster.Columns["Type"].Visible = false;
            
                gridViewMaster.Columns["Balance"].OptionsColumn.ShowInCustomizationForm = true;
                gridViewMaster.Columns["BranchCode"].OptionsColumn.ShowInCustomizationForm = true;
                gridViewMaster.Columns["Type"].OptionsColumn.ShowInCustomizationForm = true;
                gridViewMaster.Columns["Pay"].Visible = false;

                xct.xramountinwords.Text = str.ToString().ToUpper();
                xct.xrpreparedby.Text = Login.Fullname;
                xct.xrLabel3.Text = Database.getSingleQuery("Approvers", "UserID<>''", "UserID");
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControlMaster, gridViewMaster, "Pay"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void printCashVoucher()
        {
            btnfilter.PerformClick();
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth = row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString();
                string caption1 = row["Caption1"].ToString();
                string caption2 = row["Caption2"].ToString();

                DevExReportTemplate.Cash5Voucher xct = new DevExReportTemplate.Cash5Voucher();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CheckVoucher'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4; 
                double amounttopay = Convert.ToDouble(txtamounttopay.Text);


                xct.xrcheckdate.Text = txtcheckdate.Text;
                xct.xrpaidto.Text = txtsuppliername.Text;
                xct.xrparticular.Text = txtremakrs.Text;
                xct.xramount.Text = String.Format("{0:0,0.00}", amounttopay);
                
                string str = Classes.DecimalToWordExtension.ToWords(Convert.ToDecimal(txtamounttopay.Text));

                gridViewMaster.Columns["Balance"].Visible = false;
                gridViewMaster.Columns["BranchCode"].Visible = false;
                gridViewMaster.Columns["Pay"].Visible = false;
                gridViewMaster.Columns["Variance"].Visible = false;

                xct.xramountinwords.Text = str.ToString().ToUpper(); 
                xct.xrpreparedby.Text = Login.Fullname;
                xct.xrLabel3.Text = Database.getSingleQuery("Approvers", "UserID<>''", "UserID");
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControlMaster, gridViewMaster, "Pay"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            ClearUncheckedPayRows();
            if (radCashVoucher.Checked == true)
            {
                printCashVoucher();
            }
            else if (radCheckVoucher.Checked == true)
            {
                if (String.IsNullOrEmpty(txtcheckno.Text) || String.IsNullOrEmpty(txtcheckdate.Text))
                {
                    XtraMessageBox.Show("Please Filled-out the CheckNo/CheckDate Field");
                }
                else
                {
                    printVoucher();
                }
            }

        }
      
        private void radCashVoucher_CheckedChanged(object sender, EventArgs e)
        {
            radChanged();
        }

        private void radCheckVoucher_CheckedChanged(object sender, EventArgs e)
        {
            radChanged();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string refno = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "ReferenceNo").ToString();
            string invoiceno = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "InvoiceNo").ToString();
            if (radioButtonExpense.Checked == true && gridViewMaster.RowCount > 0)
            {
                bool checkifNotYetProcessed = Database.checkifExist("SELECT TOP(1) InvoiceNo " +
                "FROM dbo.ExpenseSummary " +
                "WHERE InvoiceNo='" + invoiceno + "' " +
                "AND SupplierID='" + txtsupplierid.Text + "' " +
                "AND ReferenceNumber='" + refno + "' " +
                "AND Status='APPROVED' ");

                if (checkifNotYetProcessed)
                {
                    bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Execute as ErrorCorrect this Transaction? ", "Confirm Error Correct");
                    if (confirm)
                    {
                    }
                    else
                    {
                        return;
                    }
                    btnextract.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("You cannot Cancel this Invoice, because it is already processed...");
                    return;
                }
            }
        }

        private void gridControlMaster_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlMaster, e.Location);
        }

        void ClearUncheckedPayRows()
        {
            for (int i = gridViewMaster.RowCount - 1; i >= 0; i--)
            {
                var val = gridViewMaster.GetRowCellValue(i, "Pay")?.ToString();

                if (string.IsNullOrEmpty(val) || val == "NONE")
                {
                    gridViewMaster.DeleteRow(i);
                }
            }
        }

      
        private void gridViewMaster_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (gridViewMaster.FocusedColumn.FieldName == "ReturnAllowances")
                {
                    int nextRow = gridViewMaster.FocusedRowHandle + 1;

                    if (nextRow < gridViewMaster.RowCount)
                    {
                        gridViewMaster.FocusedRowHandle = nextRow;
                        gridViewMaster.FocusedColumn = gridViewMaster.Columns["Pay"];
                    }

                    e.Handled = true;
                }
            }

        }
        
        private void btnfilter_Click(object sender, EventArgs e)
        {
            ClearUncheckedPayRows();
        }
    }
}