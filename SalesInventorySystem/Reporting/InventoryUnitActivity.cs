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

namespace SalesInventorySystem.Reporting
{
    public partial class InventoryUnitActivity : DevExpress.XtraEditors.XtraForm
    {
        public InventoryUnitActivity()
        {
            InitializeComponent();
        }

        private void InventoryUnitActivity_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
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
            var = Database.getSingleQuery("SELECT DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '" + getDate() + "') + 1, 0)) as LastDate", "LastDate");
            return var;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Database.display("Select distinct SUBSTRING(BarcodeNo,1,5) as ShipmentNo FROM DeliveryDetails WHERE DateProcessed BETWEEN '" + getDate() + "' and '" + getLastDate() + "'", gridControl2, gridView2);
            //if (String.IsNullOrEmpty(txtbrcode.Text) || String.IsNullOrWhiteSpace(monthEdit1.Text) || String.IsNullOrWhiteSpace(comboBoxEdit1.Text))
            if (String.IsNullOrEmpty(txtbrcode.Text) )
            {
                XtraMessageBox.Show("Please Filled-out the forms correctly!...");
                return;
            }
            else
            {
                execute();
            }
           
        }
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_InventoryUnitActivity";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode",txtbrcode.Text);
            //com.Parameters.AddWithValue("@parmdatefrom",getDate());
            //com.Parameters.AddWithValue("@parmdateto", getLastDate());
            
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            adapter.Fill(table);
            gridControl2.DataSource = table;
            gridView2.BestFitColumns();
            con.Close();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}