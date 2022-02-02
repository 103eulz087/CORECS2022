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

namespace SalesInventorySystem
{
    public partial class ReuploadSales : DevExpress.XtraEditors.XtraForm
    {
        struct DataParameter
        {
            public int Process;
            public int Delay;
        }

        private DataParameter _inputParameter;
        public ReuploadSales()
        {
            InitializeComponent();
        }

        private void ReuploadSales_Load(object sender, EventArgs e)
        {
            populateBranch();
            //populateCashier();
        }

        void populateBranch()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", searchLookUpEdit1, "BranchCode", "BranchCode");
        }

        void populateCashier()
        {
            Database.displaySearchlookupEdit("SELECT UserID,FullName FROM Users WHERE AssignedBranch='"+searchLookUpEdit1.Text+"' and isCashier='1' ", searchLookUpEdit2, "UserID", "UserID");
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //populateCashier();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                execute();
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

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarControl1.EditValue = e.ProgressPercentage;
            progressBarControl1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Process has been Completed!");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to re upload this table?", "Re Upload Data");
            if (ok)
            {
                if (!backgroundWorker1.IsBusy)
                {
                    _inputParameter.Delay = 100;
                    _inputParameter.Process = 1200;
                    backgroundWorker1.RunWorkerAsync(_inputParameter);
                }
            }
            else
            {
                return;
            }


            //Database.ExecuteQuery("SELECT TOP 1 SupplierID FROM Supplier","success");

        }

        void execute()
        {
            progressBarControl1.Position = 0;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_UploadSalesDetailsFull";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmtransactiondate", dateEdit1.Text);
                com.Parameters.AddWithValue("@parmbranchcode", searchLookUpEdit1.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
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
            //SqlConnection con = Database.getConnection();
            //con.Open();
            //string query = "sp_ReUploadSalesDetails";
            //    SqlCommand com = new SqlCommand(query, con);
            //    com.Parameters.AddWithValue("@parmtransactiondate", dateEdit1.Text);
            //    com.Parameters.AddWithValue("@parmbranchcode", searchLookUpEdit1.Text);
            //    com.Parameters.AddWithValue("@parmuser", searchLookUpEdit2.Text);
            //    com.Parameters.AddWithValue("@parmtablename", comboBox1.Text);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.CommandText = query;
            //    com.CommandTimeout = 3600;
            //    com.ExecuteNonQuery();
            //con.Close();
        }
    }
}