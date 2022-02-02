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

namespace SalesInventorySystem.Reporting.BIR
{
    public partial class EWTReports : DevExpress.XtraEditors.XtraForm
    {
        public EWTReports()
        {
            InitializeComponent();
        }
        void extract()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_EWTReconciliation";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmdatefrom", dateEdit1.Text);
            com.Parameters.AddWithValue("@parmdateto", dateEdit2.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            con.Close();

        }
        private void btnExtract_Click(object sender, EventArgs e)
        {
            extract();
            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "Branch");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Amount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "GrossAmount");
        }

        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\BIRReports\\";
            string filename = "EWTReports_" + DateTime.Now.ToShortDateString().Replace(@"/", "-");
            HelperFunction.exporttoexcel(gridView1, filename, filepath);
        }
    }
}