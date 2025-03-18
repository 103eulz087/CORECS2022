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
using SalesInventorySystem.POS;

namespace SalesInventorySystem
{
    public partial class CashBeginningFrm : DevExpress.XtraEditors.XtraForm
    {
        public static double cashbegin;
        public static bool isdone = false;
        public static string cashiertransno = "";
        public CashBeginningFrm()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                this.Dispose();
            }
            else if (keyData == Keys.Enter) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                simpleButton1.PerformClick();
            }
            return functionReturnValue;
        }
        void selectPOSType()
        {
            string postype = Database.getSingleQuery("POSType", "POSType <> ' '", "POSType");
            if (postype == "1")
            {
                PointOfSale psale = new PointOfSale();
                psale.Show();
            }
            else if (postype == "2")
            {
                POSMainWithDashboard pcusatfsmr = new POSMainWithDashboard();
                pcusatfsmr.Show();
            }
            //else if (postype == "3")
            //{

            //}
        }
        void newTransaction()
        {
            try
            {
                Main m = new Main();
               
                //int referencenumber = Convert.ToInt32(IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber"));
                string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToString() + "')");
                string transno = "";// m.barStaticCashierTransNo.Caption;

                //ALREADY LOGIN
                bool isUserExistToday = Database.checkifExist("SELECT TOP(1) isOpen FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' " +
                    "AND TransactionDate='" + transdate.Trim() + "' " +
                    "AND isOpen='1' " +
                    "AND CashierTransNo='" + transno + "'"); //UserID='" + Login.isglobalUserID + "'

                //bool stillOpenTransaction = Database.checkifExist("Select TOP(1) isOpen " +
                //                                            "FROM SalesTransactionSummary " +
                //                                            "WHERE BranchCode='" + Login.assignedBranch + "' " +
                //                                            "and isOpen=1 " +
                //                                            "and UserID='" + Login.isglobalUserID + "' " +
                //                                            "and TransactionDate <> '" + transdate.Trim() + "'" +
                //                                            "AND MachineUsed='"+Environment.MachineName.ToString()+"' ");
                
                ////bool isRetail = Database.checkifExist("Select * FROM POSType WHERE PosType=1");
                //if (stillOpenTransaction)
                //{
                //    XtraMessageBox.Show("The System found out that this user are still have Unclosed Transaction.. Please Contact Admin to Close the Previous Transaction!...");
                //    POS.POSTransactionChecker asjkdh = new POS.POSTransactionChecker();
                //    Database.display("SELECT BranchCode,CashierTransNo,TransactionDate,UserID,isOpen,MachineUsed FROM SalesTransactionSummary " +
                //         "WHERE BranchCode='" + Login.assignedBranch + "' " +
                //         "and isOpen=1 " +
                //         "and UserID='" + Login.isglobalUserID + "' " +
                //         "and TransactionDate <> '" + transdate.Trim() + "' " +
                //         "AND MachineUsed='" + Environment.MachineName.ToString() + "' ", asjkdh.gridControl1,asjkdh.gridView1);
                //    asjkdh.ShowDialog(this);
                //    //return;
                //}
                if (isUserExistToday)
                {
                    selectPOSType();
                }
                
                else
                {
                    if (Convert.ToDouble(spinEdit1.Text) < 0) //trappings if the user set cash begin less than 1 or equal to 0
                    {
                        XtraMessageBox.Show("Cash Begin must not equal to zero");
                    }
                    else //set cash begin, update the cashbegin in users profile and insert as new transaction in salestransaction summary
                    {
                        cashbegin = Convert.ToDouble(spinEdit1.Text);
                        int cashiertransid = IDGenerator.getIDNumber("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' and MachineUsed='"+Environment.MachineName.ToString()+"' ", "CashierTransNo",1);
                        int postransno = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "' ", "TransactionNo", 1);
                        string POStransactionNo = HelperFunction.sequencePadding1(postransno.ToString(),10);  

                        spCashBegin(cashiertransid.ToString(), POStransactionNo);
                        this.Close();

                        if (Main.isbatchupload == true)
                        {
                            BatchUploading pcusatfsmr = new BatchUploading();
                            pcusatfsmr.Show();
                        }
                        else
                        {
                            selectPOSType();
                            this.Close();
                            string getCashierTransNo = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' AND UserID='" + Login.isglobalUserID + "' and TransactionDate='" + transdate.Trim() + "'", "CashierTransNo");
                            cashiertransno = getCashierTransNo;
                            isdone = true;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            
        }

        void spCashBegin(string cashierid, string postransnum)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_CashBegin";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmcashierid", cashierid);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmcashbeginamount", spinEdit1.Text);
                com.Parameters.AddWithValue("@parmtransno", postransnum);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmmachinename", GlobalVariables.computerName);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
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
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(spinEdit1.Text))
            {
                XtraMessageBox.Show("Cash Begin Must Not Empty");
            }
            else
            {
                HelperFunction.OpenDrawer();
                Printing printit = new Printing();
                printit.printReceiptAtik();
                newTransaction();
            }
           
        }

        string getTransactionNumber()
        {
            string num = Classes.Utilities.readTextfile("C:\\POSTransaction\\TranSeries\\");
            int ornumnew = Convert.ToInt32(num) + 1;
            return ornumnew.ToString();
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton1.PerformClick();
        }

        private void CashBeginningFrm_Load(object sender, EventArgs e)
        {
            string getCashBeginAmount = Database.getSingleQuery("POSType", "POSType is not null", "CashBeginAmount");
            spinEdit1.Text = getCashBeginAmount;
            txtbranch.Text = Login.assignedBranch;
            txtdate.Text = DateTime.Now.ToShortDateString();
            txtuserid.Text = Login.isglobalUserID;
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}