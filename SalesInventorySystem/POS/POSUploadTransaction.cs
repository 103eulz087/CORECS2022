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

namespace SalesInventorySystem.POS
{
    public partial class POSUploadTransaction : Form
    {
        //int progress = 0;
        public POSUploadTransaction()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //progressBar1.Minimum = 0;
            //progressBar1.Maximum = 100;
           
            backgroundWorker1.RunWorkerAsync();
            execute();

        }
        
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_UploadSalesDetails";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmtransactioncode",textBox2.Text);
                com.Parameters.AddWithValue("@parmtransactiondate", DateTime.Now.ToShortDateString());
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            for(int i=0;i<=100;i++)
            {
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(i);
                
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Successfully Uploaded");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //execute();
            //progressBar1.Minimum = 0;
            //progressBar1.Maximum = 100;

            //if (progressBar1.Value < progressBar1.Maximum)
            //{
            //    progressBar1.Value = progress;
            //    execute();
            //    progress++;
            //}
            //else
            //{
            //    timer1.Stop();
            //}

        }

        private void POSUploadTransaction_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            analyze();
        }

        void analyze()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_Analyze";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtranscode", textBox1.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
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

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
