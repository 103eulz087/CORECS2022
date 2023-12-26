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
using DevExpress.XtraGrid;
using System.Threading;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class DeductInventoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        struct DataParameter
        {
            public int Process;
            public int Delay;
        }

        private DataParameter _inputParameter;
        public DeductInventoryDevEx()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            bool isAnalyze = false;
            isAnalyze = Database.checkifExist("SELECT TOP(1) BranchCode FROM dbo.ReInventoryMonitoring WHERE BranchCode='" + Login.assignedBranch + "' and CAST(DateExecute as date)='" + txtdate.Text + "'");

            if (String.IsNullOrEmpty(txtbranch.Text))
            {
                XtraMessageBox.Show("Please Select Branch");
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
                btnDeduct.Enabled = true;
                if (!isAnalyze)
                {
                    Database.ExecuteQuery("INSERT INTO ReInventoryMonitoring VALUES('" + Login.assignedBranch + "','" + txtdate.Text + "',1,0,'" + Login.Fullname + "',' ','"+Environment.MachineName.ToString()+"') ");
                }
            }
           
            //}
        }

        void analyze(bool isdone = false)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl1.BeginUpdate();
            try
            {
               
                string sp = "sp_Analyze";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
                com.Parameters.AddWithValue("@parmtransdate", txtdate.Text);
                //com.Parameters.AddWithValue("@parmispercashier", false);
                //com.Parameters.AddWithValue("@parmprocessby", Login.isglobalUserID);
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

        //not used--ang doFIFO maoy gigamit na method
        void deductInventory()
        {
            progressBarControl1.Position = 0;
            try
            {
                bool flag = false;
                bool isAlreadyCost = false;
                isAlreadyCost = Database.checkifExist("SELECT BranchCode FROM dbo.BatchSalesDetails WHERE isCosting=1 and BranchCode='" + Login.assignedBranch + "' and CAST(DateOrder as date)='" + txtdate.Text + "'");
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
                else
                {
                    progressBarControl1.Position = 20;
                    //computeSalesCosting2();
                    doFIFO();
                    Thread.Sleep(200);
                    progressBarControl1.Position = 50;
                    //if (!backgroundWorker1.IsBusy)
                    //{
                    //    _inputParameter.Delay = 100;
                    //    _inputParameter.Process = 1200;
                    //    backgroundWorker1.RunWorkerAsync(_inputParameter);
                    //}

                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }


        void sendMailNotification(string filepath,string branchcode)
        {
            try
            {
                string subject = "", body = "";
                
                body += "======================== <br/>";
                body += "<b>INVENTORY END OF DAY REPORT</b><br/>";
                body += "======================== <br/>";
                body += "<b><i><font color='red'>Please dont reply this is a system generated report.</font></b></i>" + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                subject = "INVENTORY END OF DAY REPORT [" + Branch.getBranchName(branchcode) + "]";
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParam(subject, body,true);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDeduct_Click(object sender, EventArgs e)
        {
            bool check = Database.checkifExist("SELECT TOP(1) BranchCode FROM dbo.ReInventoryMonitoring " +
                "WHERE BranchCode='" + Login.assignedBranch + "'  and CAST(DateExecute as date)='" + txtdate.Text + "' and isAnalyze=1 ");
            var rows = Database.getMultipleQuery("ReInventoryMonitoring", "BranchCode='" + Login.assignedBranch + "' and CAST(DateExecute as date)='" + txtdate.Text + "' ", "isAnalyze,isDeducted");
            string isAnalyze = rows["isAnalyze"].ToString();
            string isDeducted = rows["isDeducted"].ToString();
            if (!check)
            {
                XtraMessageBox.Show("You Cant Proceed this Inventory is Not Yet Analyze.. No Records in Monitorings");
                return;
            }
            if (Convert.ToBoolean(isAnalyze) == false)
            {
                XtraMessageBox.Show("You Cant Proceed this Inventory is Not Yet Analyze");
                return;
            }
            else if (Convert.ToBoolean(isAnalyze) == true && Convert.ToBoolean(isDeducted) == true)
            {
                XtraMessageBox.Show("You Already Execute this Transaction");
                return;
            }
            else if (Convert.ToBoolean(isAnalyze) == true && Convert.ToBoolean(isDeducted) == false)
            {
                //deductInventory();
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "Status").ToString() == "FAILED" || gridView1.GetRowCellValue(i, "Status").ToString() == "NO INVENTORY")
                    {
                        XtraMessageBox.Show("You Cant Proceed there is a Failed Inventory Status");
                        return;
                    }
                }
                doFIFO();

                progressBarControl1.Position = 80;
                //Thread.Sleep(300);
                //Database.ExecuteQuery("UPDATE ReInventoryMonitoring set isDeducted=1,DeductedBy='"+Login.Fullname+"' WHERE isAnalyze=1 and BranchCode='" + Login.assignedBranch + "' and CAST(DateExecute as date)='" + txtdate.Text + "' ");
                //Database.display("SELECT Branch,Product,Description,SUM(Available) as Available " +
                //    "FROM Inventory " +
                //    "WHERE Branch='" + txtbranch.Text + "' " +
                //    "and Available > 0 " +
                //    "AND isStock=1 " +
                //    "GROUP BY Branch,Product,Description", gridControl1, gridView1);
               displayInventoryUnitActivity();
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(txtdate.Text);
                string filepath = "C:\\ENDOFDAY_INVENTORY_REPORTS\\" + dt.ToString("yyyyMMdd") + "\\";
                Utilities.createDirectoryFolder(filepath);
                //DateTime dt = new DateTime();
                //dt = Convert.ToDateTime(txtdate.Text);
                //string filename = txtbranch.Text + "_" + dt.ToShortDateString().Replace("/",) + ".xls";
                string filename = Branch.getBranchName(txtbranch.Text) + "_" + dt.ToString("yyyyMMdd") + ".xls";
                string file = filepath + filename;
                gridControl1.ExportToXls(file);

                sendMailNotification(file, txtbranch.Text);
                
                progressBarControl1.Position = 90;
                XtraMessageBox.Show("Export Success");
                btnDeduct.Enabled = false;

                progressBarControl1.Position = 100;
            }
            
        }

        void displayInventoryUnitActivity()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "spr_InventoryUnitActivity";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
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
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
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
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
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

        void doFIFO()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_FiFoMapping";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmtransdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
                com.Parameters.AddWithValue("@parmprodcode", "");
                com.Parameters.AddWithValue("@parmqty", "");
                com.Parameters.AddWithValue("@parmoption", "2");
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

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

            progressBarControl1.EditValue = e.ProgressPercentage;
            progressBarControl1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Process has been Completed!");
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Status").ToString() != "PASSED")
                {
                    contextMenuStrip1.Show(gridControl1,e.Location);
                }
            }
        }

        private void inventoryQtyAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Branches.InventoryBranchQtyAdj adjbrn = new Branches.InventoryBranchQtyAdj();
            adjbrn.txtprodcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString();
            adjbrn.txtdesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductName").ToString();
            adjbrn.ShowDialog(this);
            if(Branches.InventoryBranchQtyAdj.isdone == true)
            {
                adjbrn.Dispose();
                btnAnalyze.PerformClick();
            }
        }

        private void DeductInventoryDevEx_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM dbo.Branches ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");
            if (Login.assignedBranch == "888")
            {
                txtbranch.Enabled = true;
            }
            else
            {
                txtbranch.Text = Login.assignedBranch;
                txtbranch.Enabled = false;
            }
                
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Status").ToString()=="PASSAR")
            {
                InventoryConversion inv = new InventoryConversion();
                Database.display($"SELECT * " +
                    $"FROM dbo.view_InventoryConversion " +
                    $"WHERE ChildProductCode='{gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString()}' " +
                    $"and Available > 0 AND BranchCode='{txtbranch.Text}' ",inv.gridControl1,inv.gridView1);
                inv.txtdate.Text = txtdate.Text;
              
                inv.txtbranch.Text = txtbranch.Text;
                inv.ShowDialog(this);
            }
            else
            {
                XtraMessageBox.Show("No Inventory Mapping");
            }
        }
    }
}