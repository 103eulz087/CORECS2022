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

namespace SalesInventorySystem.HOForms
{
    public partial class BackupDB : Form
    {
        public BackupDB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Execute Backup Process ?","Execute Backup");
            string filepath = "C:\\BackupSalesInventory";
            Classes.Utilities.createDirectoryFolder(filepath);
            if(ok)
            {
                backgroundWorker1.RunWorkerAsync();
                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_Backup";
                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmLogin",Login.assignedBranch);
                    com.Parameters.AddWithValue("@BaseLocation",filepath);
                    com.Parameters.AddWithValue("@BackupType","FULL");
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 3600;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i=1;i<=100;i++)
            {
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("SUCKESS");
            this.Dispose();
        }
    }
}
