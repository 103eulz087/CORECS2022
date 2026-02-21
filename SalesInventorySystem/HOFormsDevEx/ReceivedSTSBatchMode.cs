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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReceivedSTSBatchMode : DevExpress.XtraEditors.XtraForm
    {
        int totalreceive = 0;
        public static bool isdone = false;
        public ReceivedSTSBatchMode()
        {
            InitializeComponent();
        }

        void receiveSTS(string pono,string pcode,string qty,string barcode,string branchcode,string receiveby,string sprice,string isscan)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_AddBranchInventory";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmponumber", pono);
            com.Parameters.AddWithValue("@parmproductcode", pcode);
            com.Parameters.AddWithValue("@parmqty", qty);
            com.Parameters.AddWithValue("@parmbarcode", barcode);
            com.Parameters.AddWithValue("@parmbranchcode", branchcode);
            com.Parameters.AddWithValue("@parmreceivedby", receiveby);
            com.Parameters.AddWithValue("@parmsellingprice", sprice);
            com.Parameters.AddWithValue("@parmisscan", isscan);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }
        void ConfirmBranchReceivedOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {

                string query = "sp_ConfirmBranchRecievedOrder";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", "");
                com.Parameters.AddWithValue("@parmpono", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmbarcode", "");
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        //void executeTransfer()
        //{
        //    try
        //    {
        //        GridView view = gridControl1.FocusedView as GridView;
        //        view.SortInfo.Clear();

        //        int[] selectedRows = gridView1.GetSelectedRows();

        //        foreach (int rowHandle in selectedRows)
        //        {

        //            string productcode = gridView1.GetRowCellValue(rowHandle, "ProductNo").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
        //            string description = gridView1.GetRowCellValue(rowHandle, "ProductName").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString(); 
        //            string barcode = gridView1.GetRowCellValue(rowHandle, "BarcodeNo").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString(); 
        //            string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            string quantity = gridView1.GetRowCellValue(rowHandle, "QtyDelivered").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            string actualqty = gridView1.GetRowCellValue(rowHandle, "ActualQty").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            totalreceive = rowHandle;

        //            if (rowHandle >= 0)
        //            {

        //                receiveSTS(txtshipmentno.Text, productcode, actualqty, barcode,Login.assignedBranch,Login.isglobalUserID,"0","0");
        //            }
        //        }
        //        totalreceive = gridView1.SelectedRowsCount;
        //        isdone = true;
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}
        void executeTransfer()
        {
            try
            {
                GridView view = gridControlRcvd.FocusedView as GridView;
                view.SortInfo.Clear();

                int[] selectedRows = gridViewRcvd.GetSelectedRows();

                // Create DataTable for TVP
                DataTable inventoryItems = new DataTable();
                inventoryItems.Columns.Add("ProductCode", typeof(string));
                inventoryItems.Columns.Add("Barcode", typeof(string));
                inventoryItems.Columns.Add("Qty", typeof(float));
                inventoryItems.Columns.Add("SellingPrice", typeof(decimal));
                inventoryItems.Columns.Add("IsScan", typeof(bool));

                foreach (int rowHandle in selectedRows)
                {
                    string productCode = gridViewRcvd.GetRowCellValue(rowHandle, "ProductNo").ToString();
                    string barcode = gridViewRcvd.GetRowCellValue(rowHandle, "BarcodeNo").ToString();
                    float qty = Convert.ToSingle(gridViewRcvd.GetRowCellValue(rowHandle, "ActualQty"));
                    decimal sellingPrice = Convert.ToDecimal(gridViewRcvd.GetRowCellValue(rowHandle, "SellingPrice"));
                    bool isScan = false; // Set this based on your logic or UI checkbox

                    inventoryItems.Rows.Add(productCode, barcode, qty, sellingPrice, isScan);
                }

                // Call the batch stored procedure
                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AddBranchInventoryBatch", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PONumber", txtshipmentno.Text);
                        cmd.Parameters.AddWithValue("@BranchCode", Login.assignedBranch);
                        cmd.Parameters.AddWithValue("@ReceivedBy", Login.isglobalUserID);

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", inventoryItems);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.InventoryItemType";

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                totalreceive = selectedRows.Length;
                isdone = true;
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show("Error: " + ex.Message);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int totalorders = Database.getCountData("SELECT COUNT(ProductNo) as Counter FROM DeliveryDetails  WHERE PONumber=" + txtshipmentno.Text + "", "Counter");
             
            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to save this Inventory?", "Confirm Inventory Entry");
            //if(totalreceive <= 0)
            //{
            //    XtraMessageBox.Show("You did not received any items..");
            //    return;
            //}
            if (confirmRcv)
            {
                executeTransfer();
                if (totalorders != totalreceive)
                {
                    bool confirm = HelperFunction.ConfirmDialog("The System found out that there are remaining items in OrderDetails that you do not receive.. Are you sure you want to Continue", "Dscrepancy");
                    if (confirm)
                    {
                        ConfirmBranchReceivedOrder();
                        XtraMessageBox.Show("Successfully Added!");
                        this.Close();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    ConfirmBranchReceivedOrder();
                    XtraMessageBox.Show("Successfully Added!");
                    this.Close();
                }
            }
            else
            {
                return;
            }
            isdone = true;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "ActualQty")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "ActualQty")
                e.Cancel = true;
        }

        private void ReceivedSTSBatchMode_Load(object sender, EventArgs e)
        {

        }

        private void gridControlRcvd_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlRcvd, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridViewRcvd.DeleteRow(gridViewRcvd.FocusedRowHandle);
        }

        private void gridViewRcvd_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
    }
}