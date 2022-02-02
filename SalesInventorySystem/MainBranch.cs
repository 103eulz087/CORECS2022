using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class MainBranch : Form
    {
        public static string getUserTransCode = "";
        public MainBranch()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddOrder adrode = new AddOrder();
            adrode.ShowDialog(this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ViewRequest))
                {
                    form.Activate();
                    return;
                }
            }
            ViewRequest viewreq = new ViewRequest();
            //  chrginv.MdiParent = this;
            viewreq.ShowDialog(this);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(BranchInventory))
                {
                    form.Activate();
                    return;
                }
            }
            BranchInventory brancinv = new BranchInventory();
            //brancinv.MdiParent = this;
            brancinv.ShowDialog(this);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOConversion))
                {
                    form.Activate();
                    return;
                }
            }
            //AddPrimalCuts pcutfmr = new AddPrimalCuts();
            //pcutfmr.ShowDialog(this);
            HOConversion hocn = new HOConversion();
            hocn.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.ConversionReports))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.ConversionReports pcusatfsmr = new Reporting.ConversionReports();
            pcusatfsmr.Show();
        }

        void openPOS()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(PointOfSale))
                {
                    form.Activate();
                    return;
                }
            }
            PointOfSale psale = new PointOfSale();
            psale.Show();
        }

        string getTransactionNumber()
        {
            string num = Classes.Utilities.readTextfile("C:\\POSTransaction\\TranSeries\\");
            int ornumnew = Convert.ToInt32(num) + 1;
            return ornumnew.ToString();
        }


        void OpenPOSTransaction()
        {
            bool isUserExistToday = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' and DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='1' and UserID='" + Login.isglobalUserID + "'");
            if(!isUserExistToday)
            {
                CashBeginningFrm cashgbeg = new CashBeginningFrm();
                cashgbeg.ShowDialog(this);
               //int id = IDGenerator.getSalesTransactionID();
                //Database.ExecuteQuery("INSERT INTO SalesTransactionSummary VALUES('" + id + "','" + Login.assignedBranch + "','" + Login.isglobalUserID + "','" + getcashbeg + "','','" + DateTime.Now.ToString() + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','',0,0,0,'" + Login.isglobalUserID + "','','','1','" + DateTime.Now.ToString() + "','0')");
                //Database.ExecuteQuery("INSERT INTO CashiersBlotter VALUES('" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "',' ','CshBeg','" + getcashbeg + "','','" + Login.isglobalUserID + "','0')");
                //Database.ExecuteQuery("INSERT INTO TransactionCash VALUES('" + DateTime.Now.ToString() + "',' ','" + Login.isglobalUserID + "','','" + Login.assignedBranch + "','CshBeg','" + getcashbeg + "','0','','0')", "Transaction Successfully Open");
                //openPOS();
            }
            else
            {
                getUserTransCode = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' AND DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='1' and UserID='" + Login.isglobalUserID + "'","AccountCode");
                openPOS();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            OpenPOSTransaction();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //Process[] foundProcesses = Process.GetProcessesByName("SalesInventory");
            //foreach (Process p in foundProcesses)
            //{
            //    p.Kill();
            //}
            this.Dispose();
            Application.Exit();
        }

        private void MainBranch_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Login.isCashier) == true)
            {
                toolStripButton1.Visible = false;
                toolStripButton2.Visible = false;
                toolStripButton3.Visible = false;
                toolStripButton9.Visible = false;
                toolStripButton4.Visible = false;
                toolStripButton5.Visible = false;

                toolStripSeparator1.Visible = false;
                toolStripSeparator2.Visible = false;
                toolStripSeparator3.Visible = false;
                toolStripSeparator8.Visible = false;
                toolStripSeparator4.Visible = false;
                toolStripSeparator5.Visible = false;

            }
          //  Rectangle r = new Rectangle();
            //this.Size = new Size(298, Screen.PrimaryScreen.WorkingArea.Height);
            //this.MaximumSize = new Size(298, Screen.PrimaryScreen.WorkingArea.Height);
            //this.MinimumSize = new Size(298, Screen.PrimaryScreen.WorkingArea.Height);
            //this.Location = new Point(r.Right - this.Width, r.Bottom - this.Height);
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Text = Login.assignedBranch + " - " + Branch.getBranchName(Login.assignedBranch);
            label2.Text = Login.Fullname;
            label6.Text = HelperFunction.GetLocalIPAddress();
            label4.Text = Login.servername;
        }

        void syncPrize()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_SyncPrice";
            SqlCommand com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            HOForms.ProductSettings prodsets = new HOForms.ProductSettings();
            prodsets.ShowDialog(this);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(InventoryIN))
                {
                    form.Activate();
                    return;
                }
            }
            InventoryIN pcusatfsmr = new InventoryIN();
            pcusatfsmr.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POS.POSUploadTransaction))
                {
                    form.Activate();
                    return;
                }
            }
            POS.POSUploadTransaction pcusatfsmr = new POS.POSUploadTransaction();
            pcusatfsmr.Show();
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Branches.SyncPrize))
                {
                    form.Activate();
                    return;
                }
            }
            Branches.SyncPrize pcusatfsmr = new Branches.SyncPrize();
            pcusatfsmr.Show();
        }

        private void MainBranch_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.BackupDB))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.BackupDB pcusatfsmr = new HOForms.BackupDB();
            pcusatfsmr.Show();
        }
    }
}
