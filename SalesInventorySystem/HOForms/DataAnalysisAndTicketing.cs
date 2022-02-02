using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class DataAnalysisAndTicketing : Form
    {
        //bool percashier;
        string flag = "";
        public DataAnalysisAndTicketing()
        {
            InitializeComponent();
        }
        struct DataParameter
        {
            public int Process;
            public int Delay;
        }

        private DataParameter _inputParameter;

        private void button1_Click(object sender, EventArgs e)
        {
            bool isAlreadyCost = false;
            isAlreadyCost = Database.checkifExist("SELECT BranchCode FROM BatchSalesDetails WHERE isCosting=1 and BranchCode='" + txtbrcode.Text + "' and CAST(DateOrder as date)='" + txtdate.Text + "'");
            if (isAlreadyCost)
            {
                XtraMessageBox.Show("You Already Execute this Transaction.. You can proceed for ticketing function!.");
                return;
            }
            else
            {
                analyze();
                GridGroupSummaryItem ite = new GridGroupSummaryItem();
                ite.FieldName = "QtySold";
                ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                ite.ShowInGroupColumnFooter = gridView1.Columns["QtySold"];
                gridView1.GroupSummary.Add(ite);
                button2.Enabled = true;
            }
        }

        void analyze()
        {
            bool ok = false;
            string user = "";
            
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl1.BeginUpdate();
            try
            {
                if(chckpercashier.Checked==true)
                {
                    ok = true;
                    user = txtcashier.Text;
                }
                else
                {
                    ok = false;
                    user = "";
                }
                string sp = "sp_Analyze";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmtransdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmispercashier", ok);
                com.Parameters.AddWithValue("@parmprocessby", user);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControl1.EndUpdate();
                con.Close();
            }
        }
        void populateComboBox()
        {

            try
            {
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
      
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    
        private void DataAnalysisAndTicketing_Load(object sender, EventArgs e)
        {
            populateComboBox();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT distinct ProcessedBy FROM BatchSalesDetails WHERE CAST(DateOrder as date)='" + dateTimePicker2.Text + "' and BranchCode='" + txtbrcode.Text + "'", "ProcessedBy", txtcashier);
           // Database.displaySearchlookupEdit("SELECT distinct ProcessedBy FROM BatchSalesDetails WHERE CAST(DateOrder as date)='" + dateTimePicker2.Text + "' and BranchCode='" + searchLookUpEdit1.Text + "'", searchLookUpEdit3, "ProcessedBy", "ProcessedBy");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBarControl1.Position = 0;
            try
            {
                bool flag = false;
                bool isAlreadyCost = false;
                isAlreadyCost = Database.checkifExist("SELECT BranchCode FROM BatchSalesDetails WHERE isCosting=1 and BranchCode='" + txtbrcode.Text + "' and CAST(DateOrder as date)='" + txtdate.Text + "'");
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "Status").ToString() == "FAILED" || gridView1.GetRowCellValue(i, "Status").ToString() == "NO INVENTORY")
                    {
                        flag = true;
                    }

                }
                if (flag)
                {
                    XtraMessageBox.Show("Cant Compute Sales Costing..There is a decrepancy in Inventory");
                    return;
                }
                if (isAlreadyCost)
                {
                    XtraMessageBox.Show("You Already Execute this Transaction.. You can proceed for ticketing function!.");
                    return;
                }
                if (chckpercashier.Checked == true)
                {
                    XtraMessageBox.Show("Execute Costing must be whole transaction");
                    return;
                }
                else
                {
                    //
                    ////progressBar1.Maximum = 9;
                    ////progressBar1.Step = 1;
                    //backgroundWorker1.RunWorkerAsync();
                    //computeSalesCosting2();
                    ////backgroundWorker1.ReportProgress(1);
                    ////Thread.Sleep(100);
                    ////computeSalesCosting2();
                    if (!backgroundWorker1.IsBusy)
                    {
                        _inputParameter.Delay = 100;
                        _inputParameter.Process = 1200;
                        backgroundWorker1.RunWorkerAsync(_inputParameter);

                        //XtraMessageBox.Show("Succesfully Posted!");

                    }

                }
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void computeSalesCosting2()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
                {

                    
                    string sp = "spu_ComputeSalesCosting";
                    SqlCommand com = new SqlCommand(sp, con);
                    com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                    com.Parameters.AddWithValue("@parmdate", txtdate.Text);
                    com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 3600;
                    com.CommandText = sp;
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


        void executeTickets(string processoption)
        {

            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "spr_SalesTicketing";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmtransdate",txtdate.Text);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmtype", processoption);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                XtraMessageBox.Show("Tickets Executed Successfully!");
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

      

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (flag != "SALESTICKETS")
            {
                GridView view = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    string status = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Status"]).ToString();
                    if (status == "NO INVENTORY")
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                    }
                    if (status == "FAILED")
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.BackColor = Color.Blue;
                        e.Appearance.BackColor2 = Color.LightBlue;
                    }
                }
            }
        }

        private void chckpercashier_CheckedChanged(object sender, EventArgs e)
        {
            if (chckpercashier.Checked == true)
            {
                //percashier = true;
                txtcashier.Enabled = true;
            }
            else
            {
                //percashier = false;
                txtcashier.Enabled = false;
            }
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT distinct ProcessedBy FROM BatchSalesDetails WHERE CAST(DateOrder as date)='" + dateTimePicker2.Text + "' and BranchCode='" + txtbrcode.Text + "'", "ProcessedBy", txtcashier);
           // Database.displaySearchlookupEdit("SELECT distinct ProcessedBy FROM BatchSalesDetails WHERE CAST(DateOrder as date)='" + dateTimePicker2.Text + "' and BranchCode='"+searchLookUpEdit1.Text+"'", searchLookUpEdit3, "ProcessedBy", "ProcessedBy");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flag = "SALESTICKETS";
            //gridView1.Columns.Clear();
            //gridControl1.DataSource = null;
            
            if (option.Text=="")
            {
                XtraMessageBox.Show("Please Select Options");
                return;
            }
            else
            {
                executeTickets(option.Text);
            }
            
            //POSDevEx.ExecuteTicketOptionFrmDevEx posex = new POSDevEx.ExecuteTicketOptionFrmDevEx();
            //posex.Show();
            //if(posex.isdone == true)
            //{
            //    executeTickets(posex.option);
            //}
            //posex.isdone = false;
            //posex.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //gridControl1.BeginUpdate();
            Database.display("SELECT * FROM view_SalesForCosting WHERE CAST(DateOrder as date)='" + txtdate.Text + "' and BranchCode='" + txtbrcode.Text + "' ", gridControl1, gridView1);
            gridView1.Columns["isVat"].Visible = false;
            gridView1.Columns["isConfirmed"].Visible = false;
            gridView1.Columns["isCancelled"].Visible = false;
            gridView1.Columns["isVoid"].Visible = false;
            gridView1.Columns["isErrorCorrect"].Visible = false;
            gridView1.Columns["Status"].Visible = false;
            gridView1.Columns["isCosting"].Visible = false;
            //gridControl1.EndUpdate();
            //backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            //for (int i = 0; i <= 100; i++)
            //{

            //    Thread.Sleep(100);
            //    backgroundWorker1.ReportProgress(i);

            //}
            //backgroundWorker1.ReportProgress(100);
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                computeSalesCosting2();
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
            //progressBar1.Value = e.ProgressPercentage;
            //this.Text = e.ProgressPercentage.ToString();
            progressBarControl1.EditValue = e.ProgressPercentage;
            progressBarControl1.Update();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Process has been Completed!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        void postTransactionSales()
        {

            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "spu_PostTransactionSales";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmdate", txtdate.Text);
                //com.Parameters.AddWithValue("@parmreferenceno", "88999");
                com.Parameters.AddWithValue("@parmreferenceno", "89898");
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
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
        private void button6_Click(object sender, EventArgs e)
        {
            postTransactionSales();
            XtraMessageBox.Show("Transaction Sales Executed Successfully!");
            
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void errorCorrectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ErrorCorrectSales errorcor = new HOFormsDevEx.ErrorCorrectSales();
            errorcor.Show();
            Database.display("SELECT * FROM BatchSalesDetails WHERE BranchCode='"+txtbrcode.Text+"' and CAST(DateOrder as date)='"+txtdate.Text+"' AND Product='"+gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ProductCode").ToString()+"'", errorcor.gridControl1, errorcor.gridView1);
        }
    }
}
