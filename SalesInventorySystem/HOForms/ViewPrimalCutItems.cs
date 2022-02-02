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

namespace SalesInventorySystem
{
    public partial class ViewPrimalCutItems : DevExpress.XtraEditors.XtraForm
    {
        public ViewPrimalCutItems()
        {
            InitializeComponent();
        }

        private void ViewPrimalCutItems_Load(object sender, EventArgs e)
        {
            //SqlConnection con =Database.getConnection();
            //con.Open();
            //string query = "spv_GetPrimalCutItems";
            //SqlCommand com = new SqlCommand(query, con);
            //SqlDataAdapter adapter = new SqlDataAdapter(com);
            //com.Parameters.AddWithValue("@refcode", ViewInventory.barcode);
            //com.CommandType = CommandType.StoredProcedure;
            //com.CommandText = query;
            //DataTable table = new DataTable();
            //try
            //{
            //    adapter.Fill(table);
            //    //  table.Columns.Add("OvertimeType");
            //  gridControl1.DataSource = table;
            //  gridView1.BestFitColumns();
            //}
            //catch (Exception ee)
            //{
            //    XtraMessageBox.Show(ee.ToString());
            //}
            //finally
            //{
            //    con.Close();
            //}
            
            //Database.ExecuteQuery("spv_GetPrimalCutItems");
            display();
          //  gridView1.Columns["Quantity"].AppearanceCell.Font = new System.Drawing.Font(gridView1.Appearance.Row.Font, System.Drawing.FontStyle.Bold);
        }

        void display()
        {
            Database.display("SELECT * FROM view_PrimalCutPerCarcass WHERE ReferenceCode='" + ViewInventory.barcode + "'", gridControl1, gridView1);
           gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "ReferenceCode")
            {
                //e.Appearance.ForeColor = Color.Yellow;
                e.Appearance.BackColor = Color.Yellow;
            }
            if (e.Column.FieldName == "Available")
            {
                if (Convert.ToDouble(e.CellValue) <= 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        void filtertab()
        {
            if (comboBox1.Text == "ALL")
            {
                Database.display("SELECT * FROM view_PrimalCutPerCarcass WHERE ReferenceCode='" + ViewInventory.barcode + "'", gridControl1, gridView1);
            }
            else if (comboBox1.Text == "STOCK")
            {
                Database.display("SELECT * FROM view_PrimalCutPerCarcass WHERE ReferenceCode='" + ViewInventory.barcode + "' AND IsStock='1'", gridControl1, gridView1);
                
            }
            else if (comboBox1.Text == "OUT OF STOCK")
            {
                Database.display("SELECT * FROM view_PrimalCutPerCarcass WHERE ReferenceCode='" + ViewInventory.barcode + "' AND IsStock='0'", gridControl1, gridView1);
                
            }
        }
    }
}