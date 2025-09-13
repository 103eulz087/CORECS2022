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
    public partial class AddInventoryDevExBatchMode : DevExpress.XtraEditors.XtraForm
    {
        int totalreceive = 0;
        public static bool isdone = false;
        public AddInventoryDevExBatchMode()
        {
            InitializeComponent();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Quantity" && view.FocusedColumn.FieldName != "Cost")
            {
                e.Cancel = true;
            }
        }

        public DataTable BuildTransferTable(GridView gridView)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Branch", typeof(string));
            dt.Columns.Add("ShipmentNo", typeof(string));
            dt.Columns.Add("PalletNo", typeof(string));
            dt.Columns.Add("DateReceived", typeof(DateTime));
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Cost", typeof(decimal));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("TipWeight", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("Available", typeof(decimal));
            dt.Columns.Add("IsStock", typeof(bool));
            dt.Columns.Add("IsVat", typeof(bool));
            dt.Columns.Add("IsWarehouse", typeof(bool));
            dt.Columns.Add("LastMovementDate", typeof(DateTime));
            dt.Columns.Add("isProcess", typeof(bool));
            dt.Columns.Add("isSource", typeof(bool));
            dt.Columns.Add("isConversion", typeof(bool));

            int[] selectedRows = gridView.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                DataRow dr = dt.NewRow();
                dr["Branch"] = "888";
                dr["ShipmentNo"] = txtshipmentno.Text;
                dr["PalletNo"] = "0";
                dr["DateReceived"] = DateTime.Now.Date;
                dr["Product"] = gridView.GetRowCellValue(rowHandle, "OrderCode").ToString();
                dr["Description"] = gridView.GetRowCellValue(rowHandle, "Description").ToString();
                dr["Cost"] = Convert.ToDecimal(gridView.GetRowCellValue(rowHandle, "Cost"));
                dr["Barcode"] = " ";
                dr["TipWeight"] = 0;
                dr["Quantity"] = Convert.ToDecimal(gridView.GetRowCellValue(rowHandle, "Quantity"));
                dr["Available"] = dr["Quantity"];
                dr["IsStock"] = true;
                dr["IsVat"] = true;
                dr["IsWarehouse"] = true;
                dr["LastMovementDate"] = DateTime.Now.Date;
                dr["isProcess"] = false;
                dr["isSource"] = true;
                dr["isConversion"] = false;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        void executeTransfer()
        {
            try
            {
                DataTable dtTransfer = BuildTransferTable(gridView1);

                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand("sp_BulkInsertTempInventory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@TransferItems", dtTransfer);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.TempInventoryType"; // TVP name in SQL

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                totalreceive = gridView1.SelectedRowsCount;
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
        //            string productcode = gridView1.GetRowCellValue(rowHandle, "OrderCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
        //            string description = gridView1.GetRowCellValue(rowHandle, "Description").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString(); 
        //            string quantity = gridView1.GetRowCellValue(rowHandle, "Quantity").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //            totalreceive  = rowHandle;
                    
        //            if (rowHandle >= 0)
        //            {
        //                Database.ExecuteQuery("INSERT INTO TempInventory (Branch" +
        //                    ",ShipmentNo" +
        //                    ",PalletNo" +
        //                    ",DateReceived" +
        //                    ",Product" +
        //                    ",Description" +
        //                    ",Cost" +
        //                    ",Barcode" +
        //                    ",TipWeight" +
        //                    ",Quantity" +
        //                    ",Available" +
        //                    ",IsStock" +
        //                    ",IsVat" +
        //                    ",IsWarehouse" +
        //                    ",LastMovementDate" +
        //                    ",isProcess" +
        //                    ",isSource" +
        //                    ",isConversion) " +
        //                    "VALUES ('888'" +
        //                    ",'"+txtshipmentno.Text+"'" +
        //                    ",'0'" +
        //                    ",'"+DateTime.Now.ToShortDateString()+"'" +
        //                    ",'"+productcode+"'" +
        //                    ",'"+description+"'" +
        //                    ",'"+ cost + "'" + 
        //                    ",' '" +
        //                    ",0" +
        //                    ",'"+quantity+"'" +
        //                    ",'"+quantity+"'" +
        //                    ",1" +
        //                    ",1" +
        //                    ",1" +
        //                    ",'"+ DateTime.Now.ToShortDateString() + "'" +
        //                    ",0" +
        //                    ",1" +
        //                    ",0) ");
        //                //Database.ExecuteQuery("insert into InventoryBigBlue values ('888',' ',' ','" + txtbatchcode.Text + "','" + DateTime.Now.ToShortDateString() + "','" + productcode + "','" + description + "','" + barcode + "','" + quantity + "','" + quantity + "','0','" + quantity + "',0,1,0,1,'" + txtbatchcode.Text + "','" + DateTime.Now.ToShortDateString() + "',0,0,0);");
        //                //Database.ExecuteQuery("insert into InventoryTransferred values ('888', '" + productcode + "', '" + description + "', '" + DateTime.Now.ToShortDateString() + "', '" + barcode + "', '" + quantity + "', '" + DateTime.Now.ToShortDateString() + "', 1, '" + txtbatchcode.Text + "', 'auto', '" + Login.Fullname + "', 'Commissary', 'BigBlue', ' ', ' ')");
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

        void finalupdate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                //string query = "spu_postInventory";
                string query = "SP_POSTINVENTORY";
                SqlCommand com = new SqlCommand(query, con);SS
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmbranch", Login.assignedBranch);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parminvoicedate", txtinvoicedate.Text);
                com.Parameters.AddWithValue("@parmduedate", "");
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int totalorders = Database.getCountData("SELECT COUNT(distinct(OrderCode)) as Counter FROM PODETAILS  WHERE ShipmentNo=" + txtshipmentno.Text + "", "Counter");
            if (String.IsNullOrEmpty(txtinvoicedate.Text) || String.IsNullOrEmpty(txtinvoiceno.Text) || String.IsNullOrEmpty(txtduedate.Text))
            {
                XtraMessageBox.Show("Please Input All Fields!");
                return;
            }
            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to save this Inventory?", "Confirm Inventory Entry");
            if (confirmRcv)
            {
                executeTransfer();
                if (totalorders != totalreceive)
                {
                    bool confirm = HelperFunction.ConfirmDialog("The System found out that there are remaining items in OrderDetails that you do not receive.. Are you sure you want to Continue", "Dscrepancy");
                    if (confirm)
                    {
                        finalupdate();
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
                    finalupdate();
                    XtraMessageBox.Show("Successfully Added!");
                    this.Close();
                }
            }
            else
            {
                return;
            }
        }

        private void AddInventoryDevExBatchMode_Load(object sender, EventArgs e)
        {
            txtrefno.Text = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
            if (e.Column.FieldName == "Cost")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }
    }
}