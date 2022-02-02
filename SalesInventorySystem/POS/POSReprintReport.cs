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
using System.IO;

namespace SalesInventorySystem.POS
{
    public partial class POSReprintReport : DevExpress.XtraEditors.XtraForm
    {
        object cashierid=null;
        public POSReprintReport()
        {
            InitializeComponent();
        }

        private void POSReprintReport_Load(object sender, EventArgs e)
        {
            radchanged();
        }

        void radchanged()
        {
            if(radx.Checked==true)
            {
                string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + txtdate.Text + "')");
                txtcashier.Enabled = true;
                Database.displaySearchlookupEdit("SELECT TransactionDate,CashierTransNo,UserID " +
                    "FROM SalesTransactionSummary " +
                    "WHERE TransactionDate='" + transdate + "' " +
                    "AND BranchCode='" + Login.assignedBranch + "' " +
                    "AND isOpen=0 " +
                    "AND MachineUsed='" + Environment.MachineName.ToString() + "' ", txtcashier, "UserID", "UserID");
            }
            else
            {
                txtcashier.Enabled = false;
            }
        }

        private void txtcashier_EditValueChanged(object sender, EventArgs e)
        {
            cashierid = SearchLookUpClass.getSingleValue(txtcashier, "CashierTransNo");
        }

        void print()
        {
            if (radx.Checked == true)
                printXread();
            else if (radz.Checked == true)
                printZRead();
        }

        void printXread()
        {
            try
            {
                string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + txtdate.Text + "')");
                string filepath = "C:\\POSTransaction\\FinancialReport\\" + transdate + "\\" + txtcashier.Text + "\\";  
                if (!Directory.Exists(filepath))
                {
                    //something error
                    XtraMessageBox.Show("File Not Exists!..");
                }
                else
                {
                    //reprint
                    string transno = cashierid + ".txt";
                    string filetoprint = filepath + transno;
                    Printing printfile = new Printing();
                    printfile.printTextFile(filetoprint);
                    XtraMessageBox.Show("Successfully Print");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void printZRead()
        {
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + txtdate.Text + "')");
            string filepath = "C:\\POSTransaction\\EndOfDay\\" + transdate + "\\";
            if (!Directory.Exists(filepath))
            {
                //something error
                XtraMessageBox.Show("File Not Exists!..");
            }
            else
            {
                //reprint
                string transno = transdate + ".txt";
                string filetoprint = filepath + transno;
                Printing printfile = new Printing();
                printfile.printTextFile(filetoprint);
                XtraMessageBox.Show("Successfully Print");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(radx.Checked==false && radz.Checked == false)
            {
                XtraMessageBox.Show("Must Select Type Of Report..");
                return;
            }
            else if(radx.Checked==true && String.IsNullOrEmpty(txtdate.Text) && String.IsNullOrEmpty(txtcashier.Text))
            {
                XtraMessageBox.Show("TextFields must not Empty!..");
                return;
            }
            else if (radz.Checked == true && String.IsNullOrEmpty(txtdate.Text))
            {
                XtraMessageBox.Show("Date Field must not Empty!..");
                return;
            }
            else
            {
                print();
            }
        }

        private void radz_CheckedChanged(object sender, EventArgs e)
        {
            radchanged();
        }

        private void radx_CheckedChanged(object sender, EventArgs e)
        {
            radchanged();
        }

        private void txtdate_EditValueChanged(object sender, EventArgs e)
        {
            radchanged();
        }
    }
}