using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace SalesInventorySystem.Orders
{
    public partial class viewBranchOrderDetails : Form
    {
        public static string devno, refno, prodno, prodname, qty, cost,pono,isvat,brcode,barcode,seqno;
        //string exectype = "";
        public viewBranchOrderDetails()
        {
            InitializeComponent();
        }

        private void viewBranchOrderDetails_Load(object sender, EventArgs e)
        {
           // display();
        }

        void display()
        {
           // Database.display("SELECT * FROM view_BranchOrderDetails WHERE PONumber='" + ViewBranchOrder.refno + "' and BranchCode='" + ViewBranchOrder.branchno + "'", gridControl4, gridView4);
        }

        private void gridControl4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl4, e.Location);
            }
        }

        private void returnToInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to do this action?", "Return Inventory");
            if (ok)
            {
                brcode = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "BranchCode").ToString();
                pono = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "PONumber").ToString();
                devno = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "DeliveryNo").ToString();
                refno = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ReferenceNumber").ToString();
                prodno = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ProductNo").ToString();
                prodname = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ProductName").ToString();
                qty = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "QtyDelivered").ToString();
                isvat = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "isVat").ToString();
                cost = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "Cost").ToString();
                barcode = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "BarcodeNo").ToString();
                seqno = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SequenceNo").ToString();
                HOForms.ReturnCustomerOrder retucust = new HOForms.ReturnCustomerOrder();


                retucust.txtpono.Text = pono;
                retucust.txtdevno.Text = devno;
                retucust.txtrefno.Text = refno;
                retucust.txtprodno.Text = prodno;
                retucust.txtdesc.Text = prodname;
                retucust.txtqty.Text = qty;
                retucust.txtcost.Text = cost;
                //txtnewqty.Text = Orders.viewBranchOrderDetails.qty;
                retucust.txtreturnedqty.Text = qty;
                retucust.txtbarcode.Text = barcode;
                retucust.txtseqno.Text = seqno;


                retucust.ShowDialog(this);
            }
            else
            {
                return;
            }
            if (HOForms.ReturnCustomerOrder.isdone == true)
            {
                display();
                HOForms.ReturnCustomerOrder.isdone = false;
                this.Dispose();
            }
        }

        void returnInventory()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            int ctr = gridView4.RowCount - 1;
            try
            {
                //for (int i = 0; i <= gridView4.RowCount - 1; i++)
                //{

                    string query = "sp_ReturnDeliveredOrder"; //sp_ReturnDeliveredOrder //sp_ReturnDelivery
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                    com.Parameters.AddWithValue("@parmrefno", gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ReferenceNumber").ToString());
                    com.Parameters.AddWithValue("@parmpono", txtpono.Text);
                    com.Parameters.AddWithValue("@parmprodno", "");
                    com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                    com.Parameters.AddWithValue("@parmreturnedqty", "");
                    com.Parameters.AddWithValue("@parmcost", "");
                    com.Parameters.AddWithValue("@parmisvat", "");
                    com.Parameters.AddWithValue("@parmtype", "");
                    com.Parameters.AddWithValue("@parmreturntype", "ALL"); //@parmtype == full
                    com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                    com.Parameters.AddWithValue("@parmseqno", "");
                    com.Parameters.AddWithValue("@parmbarcode", "");
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                //}

            }
            catch (SqlException ex)
            {
                //throw new Exception(ex.StackTrace.ToString());
                XtraMessageBox.Show(ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //exectype = "ALL";
            bool isInvoiceUpdated = false,isOneItemsUpdated=false;
            isInvoiceUpdated = Database.checkifExist("Select isInvoiceUpdate FROM DeliverySummary WHERE PONumber='" + txtpono.Text + "' and isInvoiceUpdate=1");
            isOneItemsUpdated = Database.checkifExist("Select isCreditMemo FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "' and isCreditMemo=1");
            if (isInvoiceUpdated == false)
            {
                XtraMessageBox.Show("Invoice Number must be updated first..please go to Print Delivery Receipt Option!...");
                return;
            }
            if (isOneItemsUpdated == true)
            {
                XtraMessageBox.Show("There are some items alre already updated please check per item.!...");
                return;
            }
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to do this action?", "Return Inventory");
            if (ok)
            {
                returnInventory();
                XtraMessageBox.Show("Inventory Successfully Returned to Commissary");
                this.Dispose();
            }
            else
            {
                return;
            }
        }

    

        private void gridView4_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //Classes.DevXGridViewSettings.gridStrikeout(e,"Status","RETURNED");
            GridView view = (GridView)sender;
            string check = Convert.ToString(view.GetRowCellValue(e.RowHandle, "Status"));
            if (check == "RETURNED")
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
            }

            //if (e.Column.FieldName == "Status")
            //{
            //    if (Convert.ToString(e.CellValue) == "RETURNED")
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
            //    }
            //}
        }
    }
}
