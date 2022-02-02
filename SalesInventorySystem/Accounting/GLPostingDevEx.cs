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
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.Accounting
{
    public partial class GLPostingDevEx : DevExpress.XtraEditors.XtraForm
    {
       
        public GLPostingDevEx()
        {
            InitializeComponent();
        }

        struct DataParameter
        {
            public int Process;
            public int Delay;
        }

        private DataParameter _inputParameter;

        private void GLPostingDevEx_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            progressBarControl1.Position = 0;

            if(String.IsNullOrEmpty(txtbrcode.Text) || String.IsNullOrEmpty(monthEdit1.Text) || String.IsNullOrEmpty(comboBoxEdit1.Text))
            {
                XtraMessageBox.Show("All Fields are Mandatory!...");
                return;
            }
            else
            {
                if (!backgroundWorker1.IsBusy)
                {
                    _inputParameter.Delay = 100;
                    _inputParameter.Process = 1200;
                    backgroundWorker1.RunWorkerAsync(_inputParameter);
                  
                    //XtraMessageBox.Show("Succesfully Posted!");
                   
                }
               
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Process has been Completed!");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarControl1.EditValue = e.ProgressPercentage;
            progressBarControl1.Update();
            //progressBar1.Value = e.ProgressPercentage;
            //progressBar1.Update();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                execute();
                for (int i=0;i<process;i++)
                {
                    if(!backgroundWorker1.CancellationPending)
                    {
                   
                        backgroundWorker1.ReportProgress(index++ * 100 / process, String.Format("Process data {0}", i));
                        
                      
                        Thread.Sleep(delay);
                    }
                }
            }
            catch(Exception ex)
            {
                backgroundWorker1.CancelAsync();
                XtraMessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtbrcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtbrcode.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            object value = view.GetRowCellValue(rowHandle, "BranchName");
            txtbrname.Text = value.ToString();
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_GLPosting";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@Branch", txtbrcode.Text);
                com.Parameters.AddWithValue("@PPostDate", monthEdit1.Text+" "+comboBoxEdit1.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = query;
                com.ExecuteNonQuery();
               
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

        
    }
}