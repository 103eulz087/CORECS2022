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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class PostExpenseDevExFrm : DevExpress.XtraEditors.XtraForm
    { // ── resolved at load / lookup time ───────────────────────────
        private string _suppId = "";   // SupplierID (long key)
        private string _suppName = "";
        private bool _initialized = false;
        DataTable table;
        bool ok = false;
        object suppid,shipmentno;
        public PostExpenseDevExFrm()
        {
            InitializeComponent();
           
        }

        private void PostExpenseDevExFrm_Load(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            //datefrom.Text = new DateTime(now.Year, now.Month, 1).ToShortDateString();
            //dateto.Text = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).ToShortDateString();
            datefrom.EditValue = new DateTime(now.Year, now.Month, 1);
            dateto.EditValue = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            // Initialize empty table
            table = new DataTable();
            table.Columns.Add("BranchCode");
            table.Columns.Add("TypeOfExpense");
            table.Columns.Add("Particulars");
            table.Columns.Add("Amount");
            gridControl1.DataSource = table;
            // Defer heavy DB calls until Shown

            this.Shown -= PostExpenseDevExFrm_Shown;
            this.Shown += PostExpenseDevExFrm_Shown;

            // Better to wire this once in designer or constructor,
            // but if you do it here, make sure it only runs once per form instance



        }
        void populateBranches2()
        {
            Database.displayDevComboBoxItems("SELECT BranchCode FROM Branches", "BranchCode", txtbrcodesum);
        }
        void displayvendor()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", txtvendor, "SupplierName", "SupplierName");
        }
        void displayPurchaseList()
        {
            Database.displaySearchlookupEdit("select ShipmentNo, SupplierId, SupplierName FROM dbo.view_POSUMMARYREP WHERE Status <> 'CANCELLED'" , txtpo, "SupplierName", "SupplierName");
        }
        void loadRepositoryItem()
        {
            Database.displayRepositorySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", repbrcode, "BranchCode", "BranchCode");
            //Database.displayRepositorySearchlookupEdit("SELECT Description FROM CHartOfAccounts WHERE AccountCode like '60%'", reptypeofexpense, "Description", "Description");
            Database.displayRepositorySearchlookupEdit("SELECT * FROM ExpensesList", reptypeofexpense, "ExpenseName", "ExpenseName");
            gridView2.BestFitColumns();
            gridView3.BestFitColumns();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Amount"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }


        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "BranchCode")
                e.RepositoryItem = repbrcode;
            if (e.Column.FieldName == "TypeOfExpense")
                e.RepositoryItem = reptypeofexpense;
            if (e.Column.FieldName == "Particulars")
                e.RepositoryItem = repparticulars;
            if (e.Column.FieldName == "Amount")
                e.RepositoryItem = repamount;
        }
        private DataTable BuildExpenseTVP()
        {
            var dt = new DataTable();
            dt.Columns.Add("BranchCode", typeof(string));
            dt.Columns.Add("ExpenseName", typeof(string));
            dt.Columns.Add("Particulars", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                var branch = gridView1.GetRowCellValue(i, "BranchCode")?.ToString()?.Trim();
                var expType = gridView1.GetRowCellValue(i, "TypeOfExpense")?.ToString()?.Trim();
                var remarks = gridView1.GetRowCellValue(i, "Particulars")?.ToString()?.Trim() ?? "";

                if (!decimal.TryParse(
                        gridView1.GetRowCellValue(i, "Amount")?.ToString(),
                        out var amount) || amount <= 0)
                    continue;   // skip zero/invalid rows silently (ValidateGridRows catches real errors)

                dt.Rows.Add(branch, expType,  remarks, amount);
            }

            return dt;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            // Grid must have rows
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No expense detail lines entered.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // All rows must have BranchCode and TypeOfExpense
            if (!ValidateGridRows()) return;

            // Invoice number required
            if (string.IsNullOrWhiteSpace(txtinvoiceno.Text))
            {
                XtraMessageBox.Show("Invoice No. is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build TVP
            var tvpTable = BuildExpenseTVP();
            if (tvpTable == null || tvpTable.Rows.Count == 0)
            {
                XtraMessageBox.Show("No valid expense lines to post (check Amount > 0).", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("dbo.sp_PostExpense", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;

                    cmd.Parameters.Add("@parmrefno", SqlDbType.VarChar, 10).Value = txtrefno.Text.Trim();
                    cmd.Parameters.Add("@parmbatchrefno", SqlDbType.BigInt).Value = Convert.ToInt64(txtbatchid.Text.Trim());
                    cmd.Parameters.Add("@parmsupplierid", SqlDbType.VarChar, 100).Value = suppid.ToString();
                    cmd.Parameters.Add("@parminvoiceno", SqlDbType.VarChar, 150).Value = txtinvoiceno.Text.Trim();
                    cmd.Parameters.Add("@parmexpensedate", SqlDbType.Date).Value = Convert.ToDateTime(txtexpdate.Text);
                    cmd.Parameters.Add("@parmremarks", SqlDbType.VarChar, 2000).Value = txtremarks.Text.Trim();
                    cmd.Parameters.Add("@parmuser", SqlDbType.VarChar, 40).Value = Login.Fullname;

                    var tvpParam = cmd.Parameters.AddWithValue("@Lines", tvpTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.ExpenseEntryTVP";

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                XtraMessageBox.Show("Expense successfully submitted for approval.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (SqlException ex)
            {
                // Show SQL error number + message for easier debugging
                XtraMessageBox.Show(
                    $"Database error ({ex.Number}): {ex.Message}",
                    "Post Expense Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateGridRows()
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                var branch = gridView1.GetRowCellValue(i, "BranchCode")?.ToString();
                var expType = gridView1.GetRowCellValue(i, "TypeOfExpense")?.ToString();

                if (string.IsNullOrWhiteSpace(branch) || string.IsNullOrWhiteSpace(expType))
                {
                    gridView1.FocusedRowHandle = i;
                    XtraMessageBox.Show(
                        $"Row {i + 1}: BranchCode and Type of Expense are required.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Amount must be numeric and > 0
                var rawAmt = gridView1.GetRowCellValue(i, "Amount")?.ToString();
                if (!decimal.TryParse(rawAmt, out var amt) || amt <= 0)
                {
                    gridView1.FocusedRowHandle = i;
                    XtraMessageBox.Show(
                        $"Row {i + 1}: Amount must be a positive number.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
       

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void btnget_Click(object sender, EventArgs e)
        {
            getquery();
        }
        void getquery()
        {

            if (checkBox1.Checked == true) //if all branch
            {
                ok = true;
            }
            else
            {
                ok = false;
            }

            if (ok)
            {
                Database.display("SELECT * FROM view_ExpenseMaster WHERE ExpenseDate >= '" + datefrom.Text + "' and ExpenseDate <= '" + dateto.Text + "'", gridControl2, gridView2);
            }
            else
            {
                Database.display("SELECT * FROM view_ExpenseMaster WHERE ExpenseDate >= '" + datefrom.Text + "' and ExpenseDate <= '" + dateto.Text + "' AND BranchCode='" + txtbrcodesum.Text + "'", gridControl2, gridView2);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ok = true;
                txtbrcodesum.Text = "";
                txtbrcodesum.Enabled = false;
            }
            else
            {
                ok = false;
                txtbrcodesum.Enabled = true;
                Database.displayDevComboBoxItems("SELECT BranchCode from Branches", "BranchCode", txtbrcodesum);
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(gridControl2, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "isErrorCorrect").ToString()) == true)
            {
                XtraMessageBox.Show("Entry Already Corrected!");
                return;
            }
            else
            {
                reverseExpense();
                XtraMessageBox.Show("Success");
                btnget.PerformClick();
            }
        }
        void reverseExpense()
        {
            try
            {

                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_ExpenseReversal";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmvoucherid", txtrefno.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.Parameters.AddWithValue("@parmseq", gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"SequenceNumber").ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "isErrorCorrect"));
            if (check)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void txtvendor_EditValueChanged(object sender, EventArgs e)
        {
            suppid = SearchLookUpClass.getSingleValue(txtvendor, "SupplierID");
        }

        private bool _poLoaded = false;
        private bool _isLoadingPO = false;

        private async void chcklinktopo_CheckedChanged(object sender, EventArgs e)
        {

            if (chcklinktopo.Checked)
            {
                txtpo.Enabled = false; // disable while loading

                // Prevent duplicate calls
                if (_poLoaded || _isLoadingPO)
                {
                    txtpo.Enabled = true;
                    return;
                }

                try
                {
                    _isLoadingPO = true;
                    Cursor = Cursors.WaitCursor;

                    var purchaseList = await GetDataTableAsync(@"
                SELECT ShipmentNo, SupplierId, SupplierName
                FROM dbo.view_POSUMMARYREP
                WHERE Status <> 'CANCELLED'");

                    BindPurchaseList(purchaseList);

                    _poLoaded = true;
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    _isLoadingPO = false;
                    txtpo.Enabled = true;
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                // Disable AND clear selection
                txtpo.EditValue = null;
                txtpo.Properties.DataSource = null;
                txtpo.Enabled = false;

                // Optional: if you want reload every time user checks again
                // _poLoaded = false;
            }

        }

        private async void PostExpenseDevExFrm_Shown(object sender, EventArgs e)
        {

            if (_initialized) return;
            _initialized = true;

            await InitializeFormAsync();

        }
        private async Task InitializeFormAsync()
        {
            try
            {
                UseWaitCursor = true;

                txtrefno.Text = await Task.Run(() =>
                    IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber"));

                txtbatchid.Text = await Task.Run(() =>
                    IDGenerator.getIDNumberSP("sp_GetBatchReferenceID", "BatchReferenceID"));

                var branches = await GetDataTableAsync("SELECT BranchCode, BranchName FROM Branches");
                var vendors = await GetDataTableAsync("SELECT SupplierID, SupplierName FROM Supplier");
                var expenses = await GetDataTableAsync("SELECT ExpenseName FROM ExpensesList");

                BindBranchesToComboBox(branches);
                BindVendors(vendors);
                BindRepositoryItems(branches, expenses);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Error loading form: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
        private async Task<DataTable> GetDataTableAsync(string sql)
        {
            var dt = new DataTable();

            using (var con = Database.getConnection()) // replace with your actual connection string
            using (var cmd = new SqlCommand(sql, con))
            {
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        private void BindBranchesToComboBox(DataTable branches)
        {
            txtbrcodesum.Properties.BeginUpdate();
            try
            {
                txtbrcodesum.Properties.Items.Clear();

                foreach (DataRow row in branches.Rows)
                {
                    txtbrcodesum.Properties.Items.Add(row["BranchCode"]?.ToString());
                }

                // Optional: no default selected item
                txtbrcodesum.SelectedIndex = -1;
                txtbrcodesum.EditValue = null;
            }
            finally
            {
                txtbrcodesum.Properties.EndUpdate();
            }
        }

        private void BindVendors(DataTable vendors)
        {
            txtvendor.Properties.BeginUpdate();
            try
            {
                txtvendor.Properties.DataSource = vendors;
                txtvendor.Properties.DisplayMember = "SupplierName";
                txtvendor.Properties.ValueMember = "SupplierID";
                txtvendor.Properties.PopulateViewColumns();

                if (txtvendor.Properties.View.Columns["SupplierID"] != null)
                    txtvendor.Properties.View.Columns["SupplierID"].Visible = false;
            }
            finally
            {
                txtvendor.Properties.EndUpdate();
            }
        }
        private void BindPurchaseList(DataTable purchaseList)
        {
            txtpo.Properties.BeginUpdate();
            try
            {
                txtpo.Properties.DataSource = purchaseList;
                txtpo.Properties.DisplayMember = "SupplierName";
                txtpo.Properties.ValueMember = "ShipmentNo"; // better unique key if PO selection is by shipment
                txtpo.Properties.PopulateViewColumns();

                if (txtpo.Properties.View.Columns["SupplierId"] != null)
                    txtpo.Properties.View.Columns["SupplierId"].Visible = false;
            }
            finally
            {
                txtpo.Properties.EndUpdate();
            }
        }
        private void BindRepositoryItems(DataTable branches, DataTable expenses)
        {
            repbrcode.BeginUpdate();
            reptypeofexpense.BeginUpdate();

            try
            {

                // Add computed column for display
                if (!branches.Columns.Contains("DisplayText"))
                {
                    branches.Columns.Add("DisplayText", typeof(string));

                    foreach (DataRow row in branches.Rows)
                    {
                        row["DisplayText"] = $"{row["BranchCode"]} - {row["BranchName"]}";
                    }
                }

                // Bind to repository
                repbrcode.DataSource = branches;
                repbrcode.DisplayMember = "DisplayText";   // what user sees
                repbrcode.ValueMember = "BranchCode";      // what gets saved

              
                //repbrcode.DataSource = branches;
                //repbrcode.DisplayMember = "BranchCode";
                //repbrcode.ValueMember = "BranchCode";

                reptypeofexpense.DataSource = expenses;
                reptypeofexpense.DisplayMember = "ExpenseName";
                reptypeofexpense.ValueMember = "ExpenseName";
            }
            finally
            {
                repbrcode.EndUpdate();
                reptypeofexpense.EndUpdate();
            }

            // BestFit can be costly, so keep it after binding
            gridView2.BestFitColumns();
            gridView3.BestFitColumns();
        }

        private void txtpo_EditValueChanged(object sender, EventArgs e)
        {
            shipmentno = SearchLookUpClass.getSingleValue(txtpo, "ShipmentNo");
        }
    }
}