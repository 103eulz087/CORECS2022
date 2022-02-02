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
using System.Threading;
using System.Data.SqlClient;

namespace SalesInventorySystem.POS
{
    public partial class POSSyncProducts : DevExpress.XtraEditors.XtraForm
    {
        struct DataParameter
        {
            public int Process;
            public int Delay;
        }

        private DataParameter _inputParameter;
        public POSSyncProducts()
        {
            InitializeComponent();
        }

        void sync()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {

                string sp = "sp_SyncPrice";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode","002");
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                com.ExecuteNonQuery();
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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                sync();
                for (int i = 0; i < process; i++)
                {
                    if (!backgroundWorker1.CancellationPending)
                    {

                        backgroundWorker1.ReportProgress(index++ * 100 / process, String.Format("Process data {0}", i));
                        Thread.Sleep(delay);
                    }
                }
            }
            catch (Exception ex)
            {
                backgroundWorker1.CancelAsync();
                XtraMessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void POSSyncProducts_Load(object sender, EventArgs e)
        {
            progressBarControl1.Position = 0;
            if (!backgroundWorker1.IsBusy)
            {
                _inputParameter.Delay = 100;
                _inputParameter.Process = 1200;
                backgroundWorker1.RunWorkerAsync(_inputParameter);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarControl1.EditValue = e.ProgressPercentage;
            lblpercent.Text = string.Format("Processing...{0}%", e.ProgressPercentage);
            progressBarControl1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Process has been Completed!");
            this.Dispose();
        }
    }
}