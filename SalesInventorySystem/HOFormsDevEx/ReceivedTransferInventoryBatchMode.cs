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
    public partial class ReceivedTransferInventoryBatchMode : DevExpress.XtraEditors.XtraForm
    {
        int totalreceive = 0;
        public static bool isdone = false;
        public ReceivedTransferInventoryBatchMode()
        {
            InitializeComponent();
        }
        
        void ConfirmBranchReceivedOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {

                string query = "sp_ConfirmBranchReceivedTransferInventory";
                SqlCommand com = new SqlCommand(query, con);
               
                com.Parameters.AddWithValue("@parmtransno", txtshipmentno.Text);
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
          


                for (int i = 0; i <= gridViewRcvd.RowCount - 1; i++)
                {
                    string productCode = gridViewRcvd.GetRowCellValue(i, "ProductNo").ToString();
                    string barcode = gridViewRcvd.GetRowCellValue(i, "BarcodeNo").ToString();
                    float qty = Convert.ToSingle(gridViewRcvd.GetRowCellValue(i, "ActualQty")); 
                 
                    inventoryItems.Rows.Add(productCode, barcode, qty);
                }

                // Call the batch stored procedure
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
            int totalorders = Database.getCountData("SELECT COUNT(ProductNo) as Counter FROM dbo.TransferInventoryDetails  WHERE TransferNo=" + txtshipmentno.Text + "", "Counter");

            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to save this Inventory?", "Confirm Inventory Entry");
           
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
    }
}