using DevExpress.XtraEditors;
using Microsoft.Win32;
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

namespace SalesInventorySystem
{
    public partial class Connection : Form
    {
        string connectionString;
        RegistryKey regkey;
        public Connection()
        {
            InitializeComponent();
        }

        private void btntest_Click(object sender, EventArgs e)
        {
            connectionString = "Data Source=" + txtservername.Text + ";Initial Catalog=" + cbodbname.Text + ";User ID =" + txtserverid.Text + ";Password=" + txtserverpassword.Text + ";";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            cnn.Open();
            if (cnn.State == ConnectionState.Open)
            {
                XtraMessageBox.Show("Connection Successful.");
            }   
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            txtservername.Text = System.Environment.MachineName.ToString();
        }

        private void cbodbname_Click(object sender, EventArgs e)
        {
            cbodbname.Items.Clear();
            display_databases();
        }

        private void display_databases()
        {
            SqlConnection cnn;
            connectionString = "Data Source=" + txtservername.Text + ";Initial Catalog=" + cbodbname.Text + ";User ID =" + txtserverid.Text + ";Password=" + txtserverpassword.Text + ";";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            try
            {
                string str = "SELECT name FROM master.dbo.sysdatabases WHERE name NOT IN ('master','model','msdb','tempdb') ";
                SqlCommand com = new SqlCommand(str, cnn);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        cbodbname.Items.Add(reader["name"].ToString());
                    }
                }
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cnn.Close();
            }
           
        }
        
        private void btnsave_Click(object sender, EventArgs e)
        {
            //regkey = Registry.CurrentUser.CreateSubKey(txtconnsettingsname.Text);
            regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsMain");
            regkey.SetValue("dbconn", "Data Source=" + txtservername.Text + ";Initial Catalog=" + cbodbname.Text + ";User ID =" + txtserverid.Text + ";Password=" + txtserverpassword.Text + ";Connection Timeout = 3600;Persist Security Info = True;");
            regkey.SetValue("servername", txtservername.Text);
            regkey.SetValue("dbname", cbodbname.Text);
            regkey.SetValue("serverid", txtserverid.Text);
            regkey.SetValue("serverpassword", txtserverpassword.Text);
            Application.Restart();
            //Thread.Sleep(3000);
        }
    }
}
