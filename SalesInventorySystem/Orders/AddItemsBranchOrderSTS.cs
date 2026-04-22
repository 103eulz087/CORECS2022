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
using DevExpress.XtraGrid.Views.Grid;
using SalesInventorySystem.Classes;
using DevExpress.XtraEditors.Repository;
using System.Globalization;
using System.Data.SqlClient;

namespace SalesInventorySystem.Orders
{
    public partial class AddItemsBranchOrderSTS : DevExpress.XtraEditors.XtraForm
    {
        RepositoryItemSpinEdit repoQuantity;
        public int Quantity { get; set; }
        int totalrowhandle = 0;
        HashSet<string> selectedProductCodes = new HashSet<string>();
        //List<string> selectedProductCodes = new List<string>();
        public static bool isdone = false;
        object prodcatid = null;
        public AddItemsBranchOrderSTS()
        {
            InitializeComponent();
        }

        private void txtsrchprodcat_EditValueChanged(object sender, EventArgs e)
        {
            prodcatid = SearchLookUpClass.getSingleValue(txtsrchprodcat, "ProductCategoryID");
            populateProducts(prodcatid.ToString());
        }

        private void AddItemsBranchOrderSTS_Load(object sender, EventArgs e)
        {
      
            loadCategory();
            // Create and register editors once
            repoQuantity = new RepositoryItemSpinEdit();
            repoQuantity.MinValue = 0;
            repoQuantity.MaxValue = 99999;
            repoQuantity.IsFloatValue = false;
            gridControl1.RepositoryItems.Add(repoQuantity);
        }


        private DataTable BuildTransferDetailsTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("PONumber", typeof(string));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("Units", typeof(string));
            dt.Columns.Add("isValid", typeof(bool));
            dt.Columns.Add("Remarks", typeof(string));

            gridView1.BeginDataUpdate();
            try
            {
                for (int r = 0; r < gridView1.RowCount; r++)
                {
                    int rowHandle = gridView1.GetVisibleRowHandle(r);
                    if (rowHandle < 0) continue; // group rows / invalid

                    var qtyObj = gridView1.GetRowCellValue(rowHandle, "Quantity");
                    if (qtyObj == null) continue;

                    if (!decimal.TryParse(qtyObj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var qty))
                        continue;

                    if (qty <= 0) continue;

                    string productCode = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ProductCode"))?.Trim();
                    string desc = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Description"))?.Trim();
                    string remarks = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Remarks"))?.Trim();

                    if (string.IsNullOrWhiteSpace(productCode)) continue;

                    dt.Rows.Add(
                        txtpono.Text.Trim(),
                        productCode,
                        desc,
                        qty,
                        "kgs",
                        true,
                        remarks ?? ""
                    );
                }
            }
            finally
            {
                gridView1.EndDataUpdate();
            }

            return dt;
        }

        void save_optimized()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtpono.Text))
                {
                    XtraMessageBox.Show("PO Number is required.");
                    return;
                }

                var dt = BuildTransferDetailsTable();
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No items with Quantity > 0 to save.");
                    return;
                }

                SqlConnection con = Database.getConnection();
                con.Open();

                var tran = con.BeginTransaction();

                // 1) create temp table
                using (var cmd = new SqlCommand(@"
                    CREATE TABLE #tmpTransferDetails(
                        PONumber    varchar(10) COLLATE DATABASE_DEFAULT,
                        ProductCode varchar(10) COLLATE DATABASE_DEFAULT,
                        ProductName varchar(200) COLLATE DATABASE_DEFAULT,
                        Qty         decimal(18,3),
                        Units       varchar(10) COLLATE DATABASE_DEFAULT,
                        isValid     bit,
                        Remarks     varchar(500) COLLATE DATABASE_DEFAULT
                    );", con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2) bulk copy
                using (var bulk = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran))
                {
                    bulk.DestinationTableName = "#tmpTransferDetails";
                    bulk.BatchSize = 2000;

                    bulk.ColumnMappings.Add("PONumber", "PONumber");
                    bulk.ColumnMappings.Add("ProductCode", "ProductCode");
                    bulk.ColumnMappings.Add("ProductName", "ProductName");
                    bulk.ColumnMappings.Add("Qty", "Qty");
                    bulk.ColumnMappings.Add("Units", "Units");
                    bulk.ColumnMappings.Add("isValid", "isValid");
                    bulk.ColumnMappings.Add("Remarks", "Remarks");

                    bulk.WriteToServer(dt);
                }

                // 3) detect duplicates in ONE query
                using (var cmd = new SqlCommand(@"
                    IF EXISTS(
                        SELECT 1
                        FROM #tmpTransferDetails t
                        INNER JOIN TransferOrderDetails d
                            ON d.PONumber = t.PONumber COLLATE DATABASE_DEFAULT
                           AND d.ProductCode = t.ProductCode COLLATE DATABASE_DEFAULT
                    )
                    BEGIN
                        RAISERROR('One or more products already exist in TransferOrderDetails for this PO.', 16, 1);
                    END
                ", con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                // 4) insert all rows set-based with proper SeqNo generation
                // IMPORTANT: generate SeqNo without row-by-row getLastID.
                // We'll compute starting SeqNo once and use ROW_NUMBER().
                using (var cmd = new SqlCommand(@"
                    DECLARE @StartSeq int =
                        ISNULL((SELECT MAX(SeqNo) FROM TransferOrderDetails WHERE PONumber = @PONumber), 0);

                    INSERT INTO TransferOrderDetails
                        (PONumber, SeqNo, ProductCode, ProductName, Qty, Units, isValid, Remarks)
                    SELECT
                        t.PONumber,
                        @StartSeq + ROW_NUMBER() OVER (ORDER BY t.ProductCode) AS SeqNo,
                        t.ProductCode,
                        t.ProductName,
                        t.Qty,
                        t.Units,
                        t.isValid,
                        t.Remarks
                    FROM #tmpTransferDetails t;
                ", con, tran))
                {
                    cmd.Parameters.AddWithValue("@PONumber", txtpono.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();

                XtraMessageBox.Show($"Saved {dt.Rows.Count} item(s) successfully.");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        void save()
        {
            try
            {
                int ctr = Database.getLastID("TransferOrderDetails",
                                             "PONumber='" + txtpono.Text + "' ",
                                             "SeqNo");

                //foreach (string productCode in selectedProductCodes)
                for(int i=0;i<=gridView1.RowCount-1;i++)
                {
                    //int rowHandle = gridView1.LocateByValue("ProductCode", productCode);
                    if (gridView1.RowCount < 0) continue;

                    ctr += 1;

                    string description = gridView1.GetRowCellValue(i, "Description").ToString();
                    string quantity = gridView1.GetRowCellValue(i, "Quantity").ToString();
                    //string units = gridView1.GetRowCellValue(rowHandle, "Units").ToString();
                    string remarks = gridView1.GetRowCellValue(i, "Remarks").ToString();

                    bool checkifexists = Database.checkifExist(
                        $"SELECT TOP(1) ProductCode FROM TransferOrderDetails " +
                        $"WHERE ProductCode='{gridView1.GetRowCellValue(i, "ProductCode").ToString()}' AND PONumber='{txtpono.Text}'");

                    if (checkifexists)
                    {
                        BigAlert.Show("ALREADY EXISTS",
                            "One of the products you requested is already added. Please check!", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if(Convert.ToDouble(gridView1.GetRowCellValue(i,"Quantity")) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO TransferOrderDetails (PONumber" +
                            ",SeqNo" +
                            ",ProductCode" +
                            ",ProductName" +
                            ",Qty" +
                            ",Units" +
                            ",isValid" +
                            ",Remarks) " +
                            "VALUES ('" + txtpono.Text + "'" +
                            ",'" + ctr + "'" +
                            ",'" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "'" +
                            ",'" + description + "'" +
                            ",'" + quantity + "'" +
                            ",'kgs'" +
                            ",'1'" +
                            ",'" + remarks + "') ");
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to save this Request?", "Confirm Request Order");
            if (confirm)
            {

                //UnfilterAndSave();
                //save();
                save_optimized();
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
        }
        void loadCategory()
        {
            Database.displaySearchlookupEdit("SELECT ProductCategoryID,Description FROM ProductCategory with(nolock)", txtsrchprodcat, "Description", "Description");
        }

        void populateProducts(string catid)
        {
            string query = $"SELECT * FROM dbo.funcview_STSProductAndInventory('{Login.assignedBranch}','{catid}') ";
            HelperFunction.ShowWaitAndDisplayNonAsync(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
            gridView1.Focus();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Quantity" )
                e.Cancel = true;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
          

            if (!gridView1.IsDataRow(e.RowHandle))
                return;

            object qtyObj = gridView1.GetRowCellValue(e.RowHandle, "Quantity");
            if (qtyObj == null || qtyObj == DBNull.Value)
                return;

            if (decimal.TryParse(qtyObj.ToString(), out decimal qty) && qty > 0)
            {
                e.Appearance.BackColor = Color.LightGoldenrodYellow;
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "Preview")
            {
                label3.Text = "Add More Items";
                simpleButton1.Text = "AddMore";
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();

                // Hide rows where Quantity <= 0
                gridView1.ActiveFilter.Clear();
                gridView1.ActiveFilterString = "[Quantity] > 0";

            }
            else
            {
                gridView1.ActiveFilter.Clear();
                simpleButton1.Text = "Preview";
                label3.Text = "PREVIEW MODE – Only Quantity > 0 shown";
            }
        }
    }
}