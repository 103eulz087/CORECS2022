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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AnalysisAndTicketing : DevExpress.XtraEditors.XtraForm
    {
        public AnalysisAndTicketing()
        {
            InitializeComponent();
        }

        private void AnalysisAndTicketing_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.display("Select distinct SUBSTRING(BarcodeNo,1,5) as ShipmentNo FROM DeliveryDetails WHERE DateProcessed BETWEEN '" + getDate() + "' and '" + getLastDate() + "'", gridControl2, gridView2);
        }

        String getDate()
        {
            string var = "";
            var = Database.getSingleQuery("SELECT PARSE('" + monthEdit1.Text + " " + comboBoxEdit1.Text + "' as date) as Value", "Value");
            return var;
        }
        
        String getLastDate()
        {
            string var = "";
            var = Database.getSingleQuery("SELECT DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '"+ getDate()+"') + 1, 0)) as LastDate", "LastDate");
            return var;
        }

        private void showItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HOFormsDevEx.ViewInventoryItemsDevEx sadsd = new ViewInventoryItemsDevEx();
            //sadsd.Show();
            //Database.displaySearchlookupEdit("Select distinct SUBSTRING(BarcodeNo,1,5) FROM DeliveryDetails WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "'", sadsd.txtproducts, "Product", "Product");
        }

        private void viewInventoryStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.AnalysisInventoryStatus sadsd = new AnalysisInventoryStatus();
            Database.display("SELECT ShipmentNo,Product,Description,SUM(Quantity) as Quantity,SUM(Available) as Available FROM Inventory WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "' GROUP BY ShipmentNo,Product,Description", sadsd.gridControl2, sadsd.gridView2) ;
            sadsd.ShowDialog(this);
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl2, e.Location);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            executeTickets();
        }
        void executeTickets()
        {
            string option = "";
            if (radlist.Checked == true)
            {
                option = radlist.Text;
            }
            else 
            {
                option = radupdate.Text;
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "spr_CostTicketing";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmdatefrom", getDate());
                com.Parameters.AddWithValue("@parmdateto", getLastDate());
                com.Parameters.AddWithValue("@parmtype", option);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView2.Columns.Clear();
                gridControl2.DataSource = null;
                adapter.Fill(table);
                gridControl2.DataSource = table;
                gridView2.BestFitColumns();
                XtraMessageBox.Show("Tickets Executed Successfully!");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void updateSellingPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ViewInventoryItemsDevEx sadsd = new ViewInventoryItemsDevEx();
            sadsd.txtshipmentno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            Database.displaySearchlookupEdit("Select distinct Product,Description,Cost FROM Inventory WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "'", sadsd.txtproducts, "Product", "Product");
            sadsd.ShowDialog(this);
        }
    }
}