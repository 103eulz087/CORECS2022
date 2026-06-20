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
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReceivedTransferInventoryBatchMode : DevExpress.XtraEditors.XtraForm
    {
        int totalreceive = 0;
        public static bool isdone = false;
        object categorycode = null;
        public ReceivedTransferInventoryBatchMode()
        {
            InitializeComponent();
        }

        //void ConfirmBranchReceivedOrder()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    try
        //    {

        //        string query = "sp_ConfirmBranchReceivedTransferInventory";
        //        SqlCommand com = new SqlCommand(query, con);

        //        com.Parameters.AddWithValue("@parmtransno", txtshipmentno.Text);
        //        com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
        //        com.Parameters.AddWithValue("@preparedby", Login.Fullname);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = query;
        //        com.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        void ConfirmBranchReceivedOrder()
        {
            if (string.IsNullOrEmpty(txtcategory.Text))
            {
                XtraMessageBox.Show("Please select a Category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if(txtcategory.Text== "Return to Supplier" && string.IsNullOrEmpty(labelsupplier.Text))
            {
                XtraMessageBox.Show("Please select a Supplier.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


                using (SqlConnection con = Database.getConnection())
                {
                    try
                    {
                        con.Open();

                        using (SqlCommand com = new SqlCommand("sp_ConfirmBranchReceivedTransferInventory", con))
                        {
                            com.CommandType = CommandType.StoredProcedure;

                            com.Parameters.Add("@parmtransno", SqlDbType.VarChar).Value = txtshipmentno.Text;
                            com.Parameters.Add("@parmbranchcode", SqlDbType.VarChar).Value = Login.assignedBranch;
                            com.Parameters.Add("@preparedby", SqlDbType.VarChar).Value = Login.Fullname;

                        // ✅ NEW
                            com.Parameters.Add("@category", SqlDbType.VarChar).Value = categorycode.ToString();//txtcategory.Text;
                            com.Parameters.Add("@remarks", SqlDbType.VarChar).Value = txtremarks.Text ?? "";
                            com.Parameters.Add("@suppliername", SqlDbType.VarChar).Value = txtsupplier.Text ?? "";

                        com.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Error: " + ex.Message);
                    }
                }
        }

        private void executeTransfer()
        {
            try
            {
                GridView view = gridControlRcvd.FocusedView as GridView;

                // 1. Guard clause: Ensure there is actually data to process
                if (view == null || view.RowCount == 0)
                {
                    XtraMessageBox.Show("There are no items in the grid to receive.", "Empty List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Create DataTable for TVP
                DataTable inventoryItems = new DataTable();
                inventoryItems.Columns.Add("ProductCode", typeof(string));
                inventoryItems.Columns.Add("Barcode", typeof(string));
                inventoryItems.Columns.Add("Qty", typeof(float));

                // 3. LOOP THROUGH EVERY ROW IN THE GRID
                for (int i = 0; i < view.RowCount; i++)
                {
                    // IsDataRow ignores Group Headers or Auto-Filter rows
                    if (view.IsDataRow(i))
                    {
                        string productCode = view.GetRowCellValue(i, "ProductNo")?.ToString() ?? "";
                        string barcode = view.GetRowCellValue(i, "BarcodeNo")?.ToString() ?? "";
                        float qty = Convert.ToSingle(view.GetRowCellValue(i, "ActualQty"));

                        inventoryItems.Rows.Add(productCode, barcode, qty);
                    }
                }

                // 4. Call the batch stored procedure
                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AddBranchTransferInventoryBatch", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransferNo", txtshipmentno.Text);
                        cmd.Parameters.AddWithValue("@BranchCodeRcvr", Login.assignedBranch);
                        cmd.Parameters.AddWithValue("@ReceivedBy", Login.isglobalUserID);

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", inventoryItems);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.TransferInventoryItemType";

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                isdone = true;
                XtraMessageBox.Show($"Successfully received {inventoryItems.Rows.Count} items!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optional: Clear the grid or reload it from the database now that they are received
                // gridControlRcvd.DataSource = null; 
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //void executeTransfer()
        //{
        //    try
        //    {
        //        GridView view = gridControlRcvd.FocusedView as GridView;
        //        view.SortInfo.Clear();

        //        int[] selectedRows = gridViewRcvd.GetSelectedRows();

        //        // Create DataTable for TVP
        //        DataTable inventoryItems = new DataTable();
        //        inventoryItems.Columns.Add("ProductCode", typeof(string));
        //        inventoryItems.Columns.Add("Barcode", typeof(string));
        //        inventoryItems.Columns.Add("Qty", typeof(float));
          


        //        for (int i = 0; i <= gridViewRcvd.RowCount - 1; i++)
        //        {
        //            string productCode = gridViewRcvd.GetRowCellValue(i, "ProductNo").ToString();
        //            string barcode = gridViewRcvd.GetRowCellValue(i, "BarcodeNo").ToString();
        //            float qty = Convert.ToSingle(gridViewRcvd.GetRowCellValue(i, "ActualQty")); 
                 
        //            inventoryItems.Rows.Add(productCode, barcode, qty);
        //        }

        //        // Call the batch stored procedure
        //        using (SqlConnection conn = Database.getConnection())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_AddBranchTransferInventoryBatch", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@TransferNo", txtshipmentno.Text);
        //                cmd.Parameters.AddWithValue("@BranchCodeRcvr", Login.assignedBranch); 
        //                cmd.Parameters.AddWithValue("@ReceivedBy", Login.isglobalUserID);

        //                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", inventoryItems);
        //                tvpParam.SqlDbType = SqlDbType.Structured;
        //                tvpParam.TypeName = "dbo.TransferInventoryItemType";

        //                conn.Open();
        //                cmd.ExecuteNonQuery();
        //            }
        //        }

        //        totalreceive = selectedRows.Length;
        //        isdone = true;
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int totalorders = Database.getCountData("SELECT COUNT(ProductNo) as Counter FROM dbo.TransferInventoryDetails  WHERE TransferNo=" + txtshipmentno.Text + "", "Counter");

            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to save this Inventory?", "Confirm Inventory Entry");
           
            if (confirmRcv)
            {
                executeTransfer();
                ConfirmBranchReceivedOrder();
                XtraMessageBox.Show("Successfully Added!");
                this.Close();
            }
            else
            {
                return;
            }
            isdone = true;
        }

        private void gridViewRcvd_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "ActualQty")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
        }

        private void gridViewRcvd_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "ActualQty")
                e.Cancel = true;
        }

        private void gridControlRcvd_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlRcvd, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qtydel = gridViewRcvd.GetRowCellValue(gridViewRcvd.FocusedRowHandle, "QtyDelivered").ToString();
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.xrshipno.Text = "TRANSFER#:" + txtshipmentno.Text;//gridViewRcvd.GetRowCellValue(gridViewRcvd.FocusedRowHandle, "TransferNo").ToString();
            bprint.xrpalletno.Text = "n/a";
            bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
            bprint.lblprodtype.Text = gridViewRcvd.GetRowCellValue(gridViewRcvd.FocusedRowHandle, "ProductName").ToString();
            bprint.lbltotalkilos.Text = qtydel;
            bprint.xrBarCode2.Text = gridViewRcvd.GetRowCellValue(gridViewRcvd.FocusedRowHandle, "BarcodeNo").ToString();
            bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void ReceivedTransferInventoryBatchMode_Load(object sender, EventArgs e)
        {
            populateReceiveCategoryMaster();
            populateSupplier();
        }
        void populateReceiveCategoryMaster()
        {
            Database.displaySearchlookupEdit("SELECT CategoryCode,Description FROM dbo.ReceiveCategoryMaster ", txtcategory, "Description", "Description");
        }
        void populateSupplier()
        {
            Database.displaySearchlookupEdit("SELECT SupplierKey,SupplierName FROM dbo.Supplier ", txtsupplier, "SupplierName", "SupplierName");
        }

        private void txtcategory_EditValueChanged(object sender, EventArgs e)
        {
            categorycode = SearchLookUpClass.getSingleValue(txtcategory, "CategoryCode");
            if (categorycode.ToString() == "2")
            {
                txtsupplier.Enabled = true;
                BigAlert.Show("SELECT SUPPLIER", "You need to select a Supplier, this will not be ADDED to INVENTORY", MessageBoxIcon.Warning);
            }
            else if((string)categorycode != "1")
            {
                BigAlert.Show("WARNING", "This will not be ADDED to INVENTORY", MessageBoxIcon.Warning);
            }
            //string category = cmbCategory.Text;

            //if (category != "TransferStock")
            //{
            //    lblWarning.Text = "⚠ This will NOT be added to inventory!";
            //    lblWarning.Visible = true;
            //}
            //else
            //{
            //    lblWarning.Visible = false;
            //}

        }
    }
}