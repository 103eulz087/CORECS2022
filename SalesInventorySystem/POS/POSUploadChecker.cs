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

namespace SalesInventorySystem.POS
{
    public partial class POSUploadChecker : DevExpress.XtraEditors.XtraForm
    {
        public POSUploadChecker()
        {
            InitializeComponent();
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        void display()
        {
            progressBarControl1.Position = 20;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_POSUploadChecker";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmdatefrom", dateEdit1.Text); 
                com.Parameters.AddWithValue("@parmmachine", Environment.MachineName);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                com.ExecuteNonQuery();

                progressBarControl1.Position = 40;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                progressBarControl1.Position = 100;
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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Status").ToString() != "SUCCESS")
                {
                    contextMenuStrip1.Show(gridControl1, e.Location);
                }
            }
        }

        private void reuploadThisTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tablename = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tablename").ToString();
            //string mark = dateEdit1.Text;
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToShortDateString() + "')");
            //wla pa na endofday
            bool isOpen = Database.checkifExist($"SELECT TOP(1) BranchCode FROM dbo.POSEODMonitoring WHERE TransactionDate='{transdate}' " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}' " +
                $"AND isEndOfDay=0 ");
            if (isOpen && dateEdit1.Text == DateTime.Now.ToShortDateString())
            {
                XtraMessageBox.Show("This Transaction is still open you cannot reupload this.");
                return;
            }
            else
            {
                Database.ExecuteQuery("EXEC sp_REUploadPOSSalesSummary '" + dateEdit1.Text + "','" + Login.assignedBranch + "','" + tablename + "','" + Environment.MachineName + "' ", "SP Successfully Executed!!...");
                display();
            }  
        }

        private void POSUploadChecker_Load(object sender, EventArgs e)
        {
            progressBarControl1.Position = 0;
            dateEdit1.Text = DateTime.Today.ToShortDateString();
            //display();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
            progressBarControl1.Position = 0;
            btnGenerate.Enabled = false;
            display();
            btnGenerate.Enabled = true ;
        }
    }
}