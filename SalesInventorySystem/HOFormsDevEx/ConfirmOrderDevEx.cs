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
    public partial class ConfirmOrderDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ConfirmOrderDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure that all data is correct?", "Confirm Order");
            if (confirm)
            {
                confirmOrder();
                XtraMessageBox.Show("Successfully Updated!...");
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
        }
        void confirmOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ConfirmOrder";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmpono", txtpono.Text);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmmachinename", GlobalVariables.computerName);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 180;
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

        private void ConfirmOrderDevEx_Load(object sender, EventArgs e)
        {
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "TotalAmount");
            Classes.DevXGridViewSettings.getTotalSummation(gridViewChargesSum, "TotalAmount","PONumber");
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    contextMenuStrip1.Show(gridControl2, e.Location);
        }

        private void updateSellingPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ConfirmOrderUpdateSPriceDevEx aksd = new ConfirmOrderUpdateSPriceDevEx();
            aksd.txtdesc.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductName").ToString();
            aksd.txtseqno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SequenceNo").ToString();
            aksd.txtsprice.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SellingPrice").ToString();
            aksd.ShowDialog(this);
            if(HOFormsDevEx.ConfirmOrderUpdateSPriceDevEx.isdone == true)
            {
                display();
                HOFormsDevEx.ConfirmOrderUpdateSPriceDevEx.isdone = false;
                aksd.Dispose();
            }
        }
        void display()
        {
            //int i = 0;
            //string str = $"'{i}'"; "'"+i+"'"

            Database.display("Select SequenceNo" +
                ",ProductNo" +
                ",ProductName" +
                ",QtyDelivered" +
                ",Cost" +
                ",SellingPrice" +
                ",DateProcessed" +
                ",ProcessedBy " +
                "FROM DeliveryDetails " +
                "WHERE PONumber='" + txtpono.Text + "' " +
                "and isReturned=0", gridControl2, gridView2);
            gridView2.Columns["SequenceNo"].Visible = false;
        }
        private void gridControlChargesSum_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripCharges.Show(gridControlChargesSum, e.Location);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM ClientChargeSalesSummary WHERE ChargeNo='"+gridViewChargesSum.GetRowCellValue(gridViewChargesSum.FocusedRowHandle,"ChargeNo").ToString()+"'");
            Database.ExecuteQuery("DELETE FROM ClientChargeSalesDetails WHERE ChargeNo='" + gridViewChargesSum.GetRowCellValue(gridViewChargesSum.FocusedRowHandle, "ChargeNo").ToString() + "'","Successfully Deleted!!!...");
            Database.GridMasterDetail("ClientChargeSalesSummary", "ClientChargeSalesDetails", "ChargeNo", "ChargeNo", "DeliveryChargeDetails", gridControlChargesSum);
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
                //analyze("spview_ForDeliverySummary",txtpono.Text,txtstatus.Text,gridControl2,gridView2);
                Database.display("Select * FROM funcview_ForDeliverySummary('" + txtpono.Text + "')", gridControl2, gridView2);
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "TotalAmount");
                //Database.display("Select * FROM funcview_ForDeliverySummary('" + txtpono.Text + "')", gridControl2, gridView2);
        }

        private void raddetailed_CheckedChanged(object sender, EventArgs e)
        {
            checkChanged();
            //if (raddetailed.Checked == true)
            //    Database.display("Select * FROM funcview_ForDeliveryDetails('" + txtpono.Text + "')", gridControl2, gridView2);
        }

        private void radsummary_CheckedChanged(object sender, EventArgs e)
        {
            checkChanged();
            //if (radsummary.Checked == true)
            //    Database.display("Select * FROM funcview_ForDeliverySummary('" + txtpono.Text + "')", gridControl2, gridView2);
        }
    }
}