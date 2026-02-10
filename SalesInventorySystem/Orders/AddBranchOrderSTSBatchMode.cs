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

namespace SalesInventorySystem.Orders
{
    public partial class AddBranchOrderSTSBatchMode : DevExpress.XtraEditors.XtraForm
    {
        int totalreceive = 0;
        public static bool isdone = false;
        public AddBranchOrderSTSBatchMode()
        {
            InitializeComponent();
        }

        private void AddBranchOrderSTSBatchMode_Load(object sender, EventArgs e)
        {

        }

        void processSTS(string pono, string devno, string refno, string brcode, string pcatcode, string pcode, string qty, string barcode)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_AddBranchOrder";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", devno);
                com.Parameters.AddWithValue("@parmrefno", refno);
                com.Parameters.AddWithValue("@parmpono", pono);
                com.Parameters.AddWithValue("@parmprodcatcode", pcatcode);
                com.Parameters.AddWithValue("@parmprodcode", pcode);
                com.Parameters.AddWithValue("@parmqty", qty);
                com.Parameters.AddWithValue("@parmbarcode", barcode);

                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text); //initiating branhc
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                //com.Parameters.AddWithValue("@parmeffectivitydate", txteffectivedate.Text);
                com.Parameters.AddWithValue("@parmsourceseqno", "");
                com.Parameters.AddWithValue("@parmbarcodescanning", 0);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void ConfirmBranchOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_ConfirmBranchOrderSTS";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmeffectivitydate", "");
                com.Parameters.AddWithValue("@parmpono", txtponum.Text);
                com.Parameters.AddWithValue("@parmbarcode", "");
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            con.Close();
        }

        void executeTransfer()
        {
            try
            {
                DataTable dtTransfer = new DataTable();
                dtTransfer.Columns.Add("ProductCategoryCode", typeof(string));
                dtTransfer.Columns.Add("ProductCode", typeof(string));
                dtTransfer.Columns.Add("ProductName", typeof(string));
                //dtTransfer.Columns.Add("Cost", typeof(decimal));
                dtTransfer.Columns.Add("QtyRequested", typeof(decimal));
                dtTransfer.Columns.Add("Qty", typeof(decimal));
                //dtTransfer.Columns.Add("Barcode", typeof(string));

                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (int rowHandle in selectedRows)
                {
                    DataRow dr = dtTransfer.NewRow();
                    dr["ProductCategoryCode"] = Classes.Product.getProductCategoryCode(gridView1.GetRowCellValue(rowHandle, "Category").ToString());
                    dr["ProductCode"] = gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString();
                    dr["ProductName"] = gridView1.GetRowCellValue(rowHandle, "ProductName").ToString();
                    //dr["Cost"] = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Cost"));
                    dr["QtyRequested"] = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "QtyRequested"));
                    dr["Qty"] = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Qty"));
                    //dr["Barcode"] = gridView1.GetRowCellValue(rowHandle, "Barcode").ToString();
                    dtTransfer.Rows.Add(dr);
                }

                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand("sp_AddBranchOrderBatch", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TransferItems", dtTransfer);
                    cmd.Parameters.AddWithValue("@PONumber", txtponum.Text);
                    cmd.Parameters.AddWithValue("@DeliveryNo", txtdevno.Text);
                    cmd.Parameters.AddWithValue("@ReferenceNo", txtrefno.Text);
                    cmd.Parameters.AddWithValue("@BranchCode", txtbrcode.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                isdone = true;
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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

        //            //string prodcatcode = gridView1.GetRowCellValue(rowHandle, "CategoryCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
        //            string prodcatcode = Classes.Product.getProductCategoryCode(gridView1.GetRowCellValue(rowHandle, "Category").ToString());
        //            string productcode = gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
        //            string description = gridView1.GetRowCellValue(rowHandle, "ProductName").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString(); 
        //            string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            string quantityReq = gridView1.GetRowCellValue(rowHandle, "QtyRequested").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            string quantity = gridView1.GetRowCellValue(rowHandle, "Qty").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            string barcode = gridView1.GetRowCellValue(rowHandle, "Barcode").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            totalreceive = rowHandle;

        //            if (rowHandle >= 0)
        //            {
        //                processSTS(txtponum.Text, txtdevno.Text, txtrefno.Text, txtbrcode.Text, prodcatcode, productcode, quantity, barcode);
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int totalorders = Database.getCountData("SELECT TOP(1) COUNT(ProductNo) as Counter FROM DeliveryDetails with(nolock) WHERE PONumber=" + txtponum.Text + "", "Counter");

            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to save this Inventory?", "Confirm Inventory Entry");
            if (confirmRcv)
            {
                executeTransfer();
                if (totalorders != totalreceive)
                {
                    bool confirm = HelperFunction.ConfirmDialog("The System found out that there are remaining items in OrderDetails that you do not receive.. Are you sure you want to Continue", "Dscrepancy");
                    if (confirm)
                    {
                        ConfirmBranchOrder();
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
                    ConfirmBranchOrder();
                    isdone = true;
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
            if (e.Column.FieldName == "Qty")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
            if (e.RowHandle >= 0)
            {
                string status = gridView1.GetRowCellValue(e.RowHandle, "Status")?.ToString();
                if (status == "NEGATIVE INVENTORY" || status == "NO INVENTORY")
                {
                    e.Appearance.BackColor = Color.LightGray; // Grays out the row
                    e.Appearance.ForeColor = Color.DarkGray;
                }
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Qty")
                e.Cancel = true;
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int rowHandle = e.ControllerRow;
            if (rowHandle >= 0)
            {
                string status = gridView1.GetRowCellValue(rowHandle, "Status")?.ToString();
                if (status == "NEGATIVE INVENTORY" || status == "NO INVENTORY")
                {
                    gridView1.UnselectRow(rowHandle); // Manually unselect the row
                }
            }
            if (gridView1.SelectedRowsCount == gridView1.DataRowCount) // Check if "Select All" was clicked
            {
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    string status = gridView1.GetRowCellValue(i, "Status")?.ToString();
                    if (status == "NEGATIVE INVENTORY" || status == "NO INVENTORY")
                    {
                        gridView1.UnselectRow(i); // Exclude rows with NEGATIVE INVENTORY
                    }
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Qty")
            {
                double qtyrequested = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "QtyRequested").ToString());
                double qtytosend = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString());
                double qtyavailable = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AvailableInv").ToString());
                if(qtytosend > qtyavailable)
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Qty", qtyrequested.ToString());
                    XtraMessageBox.Show("Greater than Available Inventory Quantity!");
                    return;
                }
                else if(qtytosend > qtyrequested)
                {
                    bool confirm = HelperFunction.ConfirmDialog("You are about to send above quantity that requested", "Greater than Quantity Requested");
                    if (!confirm) { gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Qty", qtyrequested.ToString());  }
                }
            }
        }
    }
}