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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ClientShowItemsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ClientShowItemsDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (POForApproval.stat != "DELIVERED")
                {
                    contextMenuStrip1.Show(gridControl2, e.Location);
                }
            }
        }

        private void updateSellingPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(raddetailed.Checked==true)
            {
                HOFormsDevEx.ConfirmOrderUpdateSPriceDevEx aksd = new ConfirmOrderUpdateSPriceDevEx();
                aksd.txtpono.Text = txtpono.Text;
                aksd.txtdesc.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductName").ToString();
                aksd.txtseqno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SeqNo").ToString();
                aksd.txtsprice.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SellingPrice").ToString();
                aksd.ShowDialog(this);
                if (HOFormsDevEx.ConfirmOrderUpdateSPriceDevEx.isdone == true)
                {
                    display();
                    HOFormsDevEx.ConfirmOrderUpdateSPriceDevEx.isdone = false;
                    aksd.Dispose();
                }
            }
            else
            {
                XtraMessageBox.Show("You Cant Update Selling Price in Summary View, Please go to Detailed View");
                return;
            }
            
        }
        void display()
        {
            Database.display("Select * FROM funcview_ForDeliveryDetails('" + txtpono.Text + "')", gridControl2, gridView2);
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "QtyDelivered");
            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView2, "ProductName");
            //Database.display("Select SequenceNo,ProductNo,ProductName,QtyDelivered,SellingPrice,DateProcessed,ProcessedBy FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "'", gridControl2, gridView2);
            gridView2.Columns["SeqNo"].Visible = false;
        }
        void analyze(string spname, string pono,string stat, GridControl cont, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            cont.BeginUpdate();
            try
            {
                string sp = spname;
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmpono", pono);
                com.Parameters.AddWithValue("@parmpono", pono);
                com.Parameters.AddWithValue("@parmstat", stat);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                view.Columns.Clear();
                cont.DataSource = null;
                adapter.Fill(table);
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cont.EndUpdate();
                con.Close();
            }
        }
        void checkChanged()
        {
            if (raddetailed.Checked == true)
                Database.display("Select * FROM funcview_ForDeliveryDetails('" + txtpono.Text + "')", gridControl2, gridView2);
            else if (radsummary.Checked == true)
                Database.display("Select * FROM funcview_ForDeliverySummary('" + txtpono.Text + "')", gridControl2, gridView2);
                //analyze("spview_ForDeliverySummary", txtpono.Text,txtstatus.Text, gridControl2, gridView2);
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "TotalAmount");
            //Database.display("Select * FROM funcview_ForDeliverySummary('" + txtpono.Text + "')", gridControl2, gridView2);
        }
        private void raddetailed_CheckedChanged(object sender, EventArgs e)
        {
            checkChanged();
            //if (raddetailed.Checked == true)
            //{
            //    Database.display("Select * FROM funcview_ForDeliveryDetails('" + txtpono.Text + "')", gridControl2, gridView2);

            //    Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "QtyDelivered");
            //    Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView2, "ProductName");
            //}
            //Database.display("Select SequenceNo,ProductNo,ProductName,QtyDelivered,SellingPrice,DateProcessed,ProcessedBy FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "'", gridControl2, gridView2);
        }

        private void radsummary_CheckedChanged(object sender, EventArgs e)
        {
            checkChanged(); 
            //if (radsummary.Checked == true)
            //{
            //    Database.display("Select * FROM funcview_ForDeliverySummary('" + txtpono.Text + "')", gridControl2, gridView2);
            //    Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "QtyDelivered");
            //    Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView2, "ProductName");
            //}//Database.display("Select ProductNo,ProductName,SUM(QtyDelivered),SellingPrice,DateProcessed,ProcessedBy FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "' GROUP BY ProductNo,ProductName,SellingPrice,DateProcessed,ProcessedBy", gridControl2, gridView2);
        }

        private void ClientShowItemsDevEx_Load(object sender, EventArgs e)
        {
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "TotalAmount");
        }

        private void addDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (raddetailed.Checked == true)
            {
                HOFormsDevEx.ClientSalesOrderDiscount aksd = new ClientSalesOrderDiscount();
                aksd.txtpono.Text = txtpono.Text;
                aksd.txtdesc.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductName").ToString();
                aksd.txtseqno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SeqNo").ToString();
                aksd.txtsprice.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SellingPrice").ToString();
                aksd.txtqty.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "QtyDelivered").ToString();
                aksd.txttotalamount.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalAmount").ToString();
                aksd.txtpercentage.Text = "0";
                aksd.ShowDialog(this);
                if (HOFormsDevEx.ClientSalesOrderDiscount.isdone == true)
                {
                    display();
                    HOFormsDevEx.ClientSalesOrderDiscount.isdone = false;
                    aksd.Dispose();
                }
            }
            else
            {
                XtraMessageBox.Show("You Cant Update Selling Price in Summary View, Please go to Detailed View");
                return;
            }
        }
    }
}