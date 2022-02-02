using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Branches
{
    public partial class SyncPrize : Form
    {
        public SyncPrize()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i=0;i<=100;i++)
            {
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Sync");
            if (ok)
            {
                backgroundWorker1.RunWorkerAsync();
                if(comboBox1.Text=="Products Price")
                { syncPrize(); }
                else if(comboBox1.Text == "Users")
                { SyncUsers(); }
                else if (comboBox1.Text == "Settings")
                { SyncSettings(); }
                else if (comboBox1.Text == "SP")
                { syncSP(); }
            }
            else
            {
                return;
            }
            
        }

        void syncPrize()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_SyncPrice";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        void syncSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_UpdateSPClient";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmmachine", Environment.MachineName);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        void SyncUsers()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_SyncUser";
            SqlCommand com = new SqlCommand(query, con);
            //com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        void SyncSettings()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_SyncSettings";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Success");
            this.Dispose();
        }
    }
}
