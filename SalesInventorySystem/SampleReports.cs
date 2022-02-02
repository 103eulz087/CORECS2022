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
    public partial class SampleReports : DevExpress.XtraEditors.XtraForm
    {
        public SampleReports()
        {
            InitializeComponent();
        }

        private void SampleReports_Load(object sender, EventArgs e)
        {
            //populate();
        }

        //void populate()
        //{
        //    Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbrcode);
        //}

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "rt_AMLACasaMultipleAccounts";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@pBranchCode",txtbranchcode.Text);
            com.Parameters.AddWithValue("@pStartDate",txtstartdate.Text);
            com.Parameters.AddWithValue("@pEndDate",txtenddate.Text);
            com.Parameters.AddWithValue("@pStartAmount",txtstartamount.Text);
            com.Parameters.AddWithValue("@pCIFType","ALL");
            com.Parameters.AddWithValue("@pScopeFilter","period");
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            execute();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}