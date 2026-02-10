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
using System.IO;
using System.Collections;
using System.Net.Mail;
using System.Diagnostics;

namespace SalesInventorySystem
{
    public partial class POSClosedTransaction : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        //double denomtotal = 0.0;
        string transno;
        public static string cashiertranscode = "";
        public POSClosedTransaction()
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
           
            return functionReturnValue;
        }
        private void POSClosedTransaction_Load(object sender, EventArgs e)
        {
            string getCashBeginAmount = Database.getSingleQuery("POSType", "POSType is not null", "CashBeginAmount");
            txtNextcashBeginning.Text = getCashBeginAmount;
            nextcashbegintextchanged();
            //checkB4ClosedTransaction();
        }

        void checkB4ClosedTransaction()
        {
            string cashierUserID = Database.getSingleQuery("SalesTransactionSummary", $"CashierTransNo='{txtcashiertransno.Text}' " +
                $"AND TransactionDate='{txttransactiondate.Text}' " +
                $"AND MachineUsed='{Environment.MachineName}' " +
                $"AND BranchCode='{Login.assignedBranch}'", "UserID");
            string cashierName = Database.getSingleQuery("Users", $"UserID='{cashierUserID}'", "FullName");

            txtcashiername.Text = cashierName;

            bool isPendingTran = Database.checkifExist("SELECT TOP(1) Status FROM BatchSalesSummary WHERE (Status='Pending' OR isHold='1') " +
                "AND BranchCode='" + Login.assignedBranch + "' " +
                "AND MachineUsed='" + Environment.MachineName + "' " +
                //"AND isHold='1' " +
                "AND CashierTransNo='" + txtcashiertransno.Text + "' ");
            if (isPendingTran)
            {
                lblchecker1.Text = "You still have Pending Transactions.";
                pictureEditwrong.Visible = true;
                pictureEditcheck.Visible = false;
                simpleButton2.Enabled = false;
                simpleButton3.Enabled = false;
                simpleButton1.Enabled = false;
                grouppendingtran.Visible = true;
                Database.display($"SELECT ReferenceNo as ORNum,TotalAmount as Amount,OnHoldName as CustName,PreparedBy " +
                      $"FROM dbo.BatchSalesSummary WHERE (Status='Pending' OR isHold='1') AND BranchCode='{Login.assignedBranch}' " +
                      $"AND MachineUsed='{Environment.MachineName}' AND CashierTransNo='{txtcashiertransno.Text}' ", gridControl1, gridView1);

            }
            else
            {
                lblchecker1.Text = "You have No Pending Transactions.";
                
                pictureEditwrong.Visible = false;
                pictureEditcheck.Visible = true;
                simpleButton2.Enabled = true;
                grouppendingtran.Visible = false;
            }
        }

        void confirm()
        {
            try
            {
                bool CashierEmailConfirmClosedTran = Database.checkifExist("SELECT isnull(UploadPerShifting,0) FROM dbo.POSType WHERE UploadPerShifting=1");
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to close your transaction?", "Close Transaction");
                string userid = Database.getSingleQuery("SalesTransactionSummary", $"CashierTransNo='{txtcashiertransno.Text}'", "UserID");
                if (simpleButton3.Visible == true)
                {
                    XtraMessageBox.Show("Please Confirm first the Denomination you entered");
                    return;
                }
                if (ok)
                {
                    //backgroundWorker1.RunWorkerAsync();
                 
                    bool isExist = false;

                    if (checkBox1.Checked == true)
                    {
                        isExist = true;
                    }
                    else
                    {
                        isExist = false;
                    }
                    execute(userid);
                    printFinancialReport(isExist, userid);
                    if (CashierEmailConfirmClosedTran) { sendMailNotification(MydataGridView1); }
                    isdone = true;
                    XtraMessageBox.Show("Transaction Successfully Closed!");
                    this.Dispose();

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            
        }


        void execute(string userid)
        {

            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_POSCloseTransaction";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);

                com.Parameters.AddWithValue("@parmtranscode", txttransactionno.Text);
                com.Parameters.AddWithValue("@parmcashiertranscode", txtcashiertransno.Text);
                com.Parameters.AddWithValue("@parmnextbegincash", txtNextcashBeginning.Text);

                com.Parameters.AddWithValue("@parmbeginningsinumber", txtbeginninginvoice.Text);
                com.Parameters.AddWithValue("@parmendingsinumber", txtendingsi.Text);
                com.Parameters.AddWithValue("@parmendingtransno", txtendtransno.Text);

                com.Parameters.AddWithValue("@parmbeginrettransno", txtbegretno.Text);
                com.Parameters.AddWithValue("@parmendrettransno", txtendretno.Text);
                /////////////////////////////////////////////////////////////////////////////////////
                com.Parameters.AddWithValue("@parmnoofsolditem", txtnoofsolditem.Text);
                com.Parameters.AddWithValue("@parmnoofcancelleditem", txtnoofcancelleditem.Text);
                com.Parameters.AddWithValue("@parmnoofvoiditem", txtnoofvoiditem.Text);
                com.Parameters.AddWithValue("@parmnoofdiscount", txtnoofdiscount.Text);
                com.Parameters.AddWithValue("@parmnoofvatitems", txtnoofvat.Text);
                com.Parameters.AddWithValue("@parmnoofreturneditem", txtnoofreturneditem.Text);

                //TOTAL COUNT
                com.Parameters.AddWithValue("@parmnoofscdiscitems", txtnoofscdisc.Text); 
                com.Parameters.AddWithValue("@parmnoofpwddiscitems", txtnoofpwddisc.Text);
                com.Parameters.AddWithValue("@parmnoofregdiscitems", txtnoofregdisc.Text);

                com.Parameters.AddWithValue("@parmvatadjustment", txtvatadjustment.Text);

                com.Parameters.AddWithValue("@parmtotalofcancelleditem", txtTotalCancelledTransaction.Text);
                com.Parameters.AddWithValue("@parmtotalofvoiditem", txtTotalVoidTransaction.Text);
                com.Parameters.AddWithValue("@parmtotalofdiscountitem", txtTotalDiscount.Text);
                com.Parameters.AddWithValue("@parmtotalofvatitems", txtTotalTax.Text);
                com.Parameters.AddWithValue("@parmtotalofreturneditems", txtTotalReturnedTransaction.Text);

                //TOTAL SUM AMOUNT
                com.Parameters.AddWithValue("@parmtotalofscdiscitems", txttotalofscdisc.Text);
                com.Parameters.AddWithValue("@parmtotalofpwddiscitems", txttotalofpwddisc.Text);
                com.Parameters.AddWithValue("@parmtotalofregdiscitems", txttotalofregdisc.Text);

                com.Parameters.AddWithValue("@parmvatablesale", txtvatablesales.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", txtvatexemptsale.Text);
                com.Parameters.AddWithValue("@parmvatamount", txtvatamount.Text);

                com.Parameters.AddWithValue("@parmtotalcashsales", txtTotalCashSales.Text);
                com.Parameters.AddWithValue("@parmtotalcreditsales", txtTotalCreditSales.Text);

                com.Parameters.AddWithValue("@parmtotalgrosssales", txttotalgross.Text);
                com.Parameters.AddWithValue("@parmtotalnetsales", txtTotalNetSales.Text);
                com.Parameters.AddWithValue("@parmtotalzeroratedsales", txtzeroratedsale.Text);

                com.Parameters.AddWithValue("@parmactualcashonhand", txtActualCashOnHand.Text);
                com.Parameters.AddWithValue("@parmcashremitted", txtCashRemitted.Text);
                com.Parameters.AddWithValue("@parmoverrage", txtoverage.Text);
                com.Parameters.AddWithValue("@parmshortage", txtshortage.Text);
                com.Parameters.AddWithValue("@parmclosedby", userid);

                com.Parameters.AddWithValue("@parm1k", txt1k.Text);
                com.Parameters.AddWithValue("@parm5h", txt5h.Text);
                com.Parameters.AddWithValue("@parm2h", txt2h.Text);
                com.Parameters.AddWithValue("@parm1h", txt1h.Text);
                com.Parameters.AddWithValue("@parm50p", txt50p.Text);
                com.Parameters.AddWithValue("@parm20p", txt20p.Text);
                com.Parameters.AddWithValue("@parm10p", txt10p.Text);
                com.Parameters.AddWithValue("@parm5p", txt5p.Text);
                com.Parameters.AddWithValue("@parm1p", txt1p.Text);
                com.Parameters.AddWithValue("@parm25c", txt25c.Text);
                com.Parameters.AddWithValue("@parm10c", txt10c.Text);
                com.Parameters.AddWithValue("@parm5c", txt5c.Text);
                com.Parameters.AddWithValue("@parm1c", txt1c.Text);
                com.Parameters.AddWithValue("@parmmachinename", GlobalVariables.computerName);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
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

        

        void sendMailNotification(DataGridView gridview)
        {
            try
            {
                cashiertranscode = txtcashiertransno.Text;
                string subject = "", body = "";
                string transactionbegin = Database.getSingleQuery("SalesTransactionSummary", "CashierTransNo='" + txtcashiertransno.Text + "' AND BranchCode='"+Login.assignedBranch+"' ", "TransactionBegin");

                body += "======================== <br/>";
                body += "<b>FINANCIAL REPORT</b><br/>";
                body += "======================== <br/>";
                body += "<b>Branch: </b>" + Branch.getBranchName(Login.assignedBranch) + "<br/>";// + Environment.NewLine;
                body += "<b>Cashier: </b>" + Login.Fullname + "<br/>";// Environment.NewLine;
                body += "<b>TransactionBegin: </b>" + transactionbegin + "<br/>";//Environment.NewLine;
                //body += "<b>TransactionBegin: </b>" + txttransactiondate.Text + "<br/>";//Environment.NewLine;
                body += "<b>TransactionClosed: </b>" + DateTime.Now.ToString() + "<br/>";// + Environment.NewLine + Environment.NewLine;
                body += "<b>POS Machine: </b>" + GlobalVariables.computerName + "<br/>";// + Environment.NewLine + Environment.NewLine;
                body += "<b>Beginning Cash: </b>" + txtBeginningCash.Text + "<br/><br/>";

                body += "<b>TOTAL CASH SALES: </b>" + txtTotalCashSales.Text + "<br/>";// Environment.NewLine;
                body += "<b>TOTAL CREDIT SALES: </b>" + txtTotalCreditSales.Text + "<br/>";// Environment.NewLine;
                body += "<b>TOTAL CHARGE TO ACCOUNT SALES: </b>" + txtchargesales.Text + "<br/>";// Environment.NewLine;
                body += "<b>TOTAL NET SALES: </b>" + txtTotalNetSales.Text + "<br/><br/>";// Environment.NewLine;

                body += "<b>Actual Cash On-Hand: </b>" + HelperFunction.numericFormat(Convert.ToDouble(txtActualCashOnHand.Text)) + "<br/>";// Environment.NewLine;
                body += "<b>Next Beginning Cash (Deposit to Cash Drawer): </b>" + HelperFunction.numericFormat(Convert.ToDouble(txtNextcashBeginning.Text)) + "<br/>";// Environment.NewLine;
                body += "<b>Cash Remittance: </b>" + HelperFunction.numericFormat(Convert.ToDouble(txtCashRemitted.Text)) + "<br/>";// Environment.NewLine;
                body += "<b>Shortage: </b>" + txtshortage.Text + "<br/>";// Environment.NewLine;
                body += "<b>Overrage: </b>" + txtoverage.Text + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                body += "<b>Vat-Exempt Sale: </b>" + txtvatexemptsale.Text + "<br/>";// Environment.NewLine;
                body += "<b>Vatable Sale: </b>" + txtvatablesales.Text + "<br/>";// Environment.NewLine;
                body += "<b>VAT Amount: </b>" + txtvatamount.Text + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                body += "<b>No. of Item Sold: </b>" + txtnoofsolditem.Text + "<br/>";// +Environment.NewLine;

                body += "<b>No. of Discount: </b>" + txtnoofdiscount.Text + "<br/>";// + Environment.NewLine;
                body += "<b>Total Discount Amount: </b>" + txtTotalDiscount.Text + "<br/>";// + Environment.NewLine;

                body += "<b>No. of Cancelled Items: </b>" + txtnoofcancelleditem.Text + "<br/>";// + Environment.NewLine;
                body += "<b>Total Amount of Cancelled Item: </b>" + txtTotalCancelledTransaction.Text + "<br/>";// + Environment.NewLine;
                body += "<b>No. of Void Items: </b>" + txtnoofvoiditem.Text + "<br/>";// Environment.NewLine;
                body += "<b>Total Amount of Void Items: </b>" + txtTotalVoidTransaction.Text + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                body += "======================== <br/>";
                body += "<b>HOURLY SALES REPORT</b> <br/>";
                body += "======================== <br/>";
                body += "<table border='1' cellpadding='1' cellspacing='1'><tr><th>Date</th><th>Time</th><th>TotalQtySold</th><th>TotalAmount</th><th>TotalItems</th></tr>";
                for (int i = 0; i <= gridview.RowCount - 1; i++)
                {
                    string orasconv = "";
                    string petsa = gridview.Rows[i].Cells["Date"].Value.ToString();
                    string oras = gridview.Rows[i].Cells["Hour"].Value.ToString();
                    string qty = gridview.Rows[i].Cells["QtySold"].Value.ToString();
                    string totalamount = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();
                    string totalitems = gridview.Rows[i].Cells["TotalItems"].Value.ToString();
                    if (oras == "1")
                        orasconv = "1AM";
                    else if (oras == "2")
                        orasconv = "2AM";
                    else if (oras == "3")
                        orasconv = "3AM";
                    else if (oras == "4")
                        orasconv = "4AM";
                    else if (oras == "5")
                        orasconv = "5AM";
                    else if (oras == "6")
                        orasconv = "6AM";
                    else if (oras == "7")
                        orasconv = "7AM";
                    else if (oras == "8")
                        orasconv = "8AM";
                    else if (oras == "9")
                        orasconv = "9AM";
                    else if (oras == "10")
                        orasconv = "10AM";
                    else if (oras == "11")
                        orasconv = "11AM";
                    else if (oras == "12")
                        orasconv = "12PM";
                    else if (oras == "13")
                        orasconv = "1PM";
                    else if (oras == "14")
                        orasconv = "2PM";
                    else if (oras == "15")
                        orasconv = "3PM";
                    else if (oras == "16")
                        orasconv = "4PM";
                    else if (oras == "17")
                        orasconv = "5PM";
                    else if (oras == "18")
                        orasconv = "6PM";
                    else if (oras == "19")
                        orasconv = "7PM";
                    else if (oras == "20")
                        orasconv = "8PM";
                    else if (oras == "21")
                        orasconv = "9PM";
                    else if (oras == "22")
                        orasconv = "10PM";
                    else if (oras == "23")
                        orasconv = "11PM";
                    else if (oras == "00")
                        orasconv = "12AM";

                    body += "<tr><td>" + petsa + "</td><td>" + orasconv + "</td><td>" + qty + "</td><td>" + HelperFunction.numericFormat(Convert.ToDouble(totalamount)) + "</td><td>" + totalitems + "</td></tr>";
                }
                body += "</table>";
                body += "<b><i><font color='red'>Please dont reply this is a system generated report.</font></b></i>" + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                subject = "POS Close Transaction Report [" + Branch.getBranchName(Login.assignedBranch) + "]";
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParam(subject, body,false);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void sendMailNotificationGrouptItemSales(DataGridView gridview)
        {
            try
            {
                cashiertranscode = txtcashiertransno.Text;
                string subject = "", body = "";
                string transactionbegin = Database.getSingleQuery("SalesTransactionSummary", "CashierTransNo='" + txtcashiertransno.Text + "' AND BranchCode='" + Login.assignedBranch + "' ", "TransactionBegin");

                
                body += "======================== <br/>";
                body += "<b>GROUP ITEM SALES REPORT</b> <br/>";
                body += "======================== <br/>";
                body += "<table border='1' cellpadding='1' cellspacing='1'><tr><th>ProductName</th><th>Time</th><th>TotalQtySold</th><th>TotalAmount</th></tr>";
                for (int i = 0; i <= gridview.RowCount - 1; i++)
                { 
                    string petsa = gridview.Rows[i].Cells["Description"].Value.ToString(); 
                    string qty = gridview.Rows[i].Cells["QtySold"].Value.ToString();
                    string totalamount = gridview.Rows[i].Cells["TotalAmount"].Value.ToString(); 
                    body += "<tr><td>" + petsa + "</td><td>" + qty + "</td><td>" + HelperFunction.numericFormat(Convert.ToDouble(totalamount)) + "</td></tr>";
                }
                body += "</table>";
                body += "<b><i><font color='red'>Please dont reply this is a system generated report.</font></b></i>" + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                subject = "POS Close Transaction Report [" + Branch.getBranchName(Login.assignedBranch) + "]";
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParam(subject, body, false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void nextcashbegintextchanged()
        {
            double cashremit = 0.0;
            cashremit = Convert.ToDouble(txtActualCashOnHand.Text) - Convert.ToDouble(txtNextcashBeginning.Text);
            txtCashRemitted.Text = Math.Round(cashremit, 2).ToString();
        }

        private void txtNextcashBeginning_EditValueChanged(object sender, EventArgs e)
        {
            //double cashremit = 0.0;
            //cashremit = Convert.ToDouble(txtActualCashOnHand.Text) - Convert.ToDouble(txtNextcashBeginning.Text);
            //txtCashRemitted.Text = Math.Round(cashremit, 2).ToString();
            nextcashbegintextchanged();
        }

        private void txtTotalBill_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtActualCashOnHand_EditValueChanged(object sender, EventArgs e)
        {
            double overrage = 0.0, shortage = 0.0, totalnet = 0.0;//cleanamount=0.0;

            //cleanamount = Convert.ToDouble(txtTotalCashSales.Text);// - Convert.ToDouble(txtTotalDiscount.Text);
            totalnet = Convert.ToDouble(txtTotalCashSales.Text) + Convert.ToDouble(txtBeginningCash.Text);
           
            if (Convert.ToDouble(txtActualCashOnHand.Text) >= totalnet)
            {
                overrage = Convert.ToDouble(txtActualCashOnHand.Text) - totalnet;
                txtoverage.Text = Math.Round(overrage, 2).ToString();
                txtshortage.Text = "0";
            }
            else if (Convert.ToDouble(txtActualCashOnHand.Text) <= totalnet)
            {
                shortage = totalnet - Convert.ToDouble(txtActualCashOnHand.Text);
                txtshortage.Text = Math.Round(shortage, 2).ToString();
                txtoverage.Text = "0";
            }
            nextcashbegintextchanged();
            //txtNextcashBeginning.Text = "0";
            //txtCashRemitted.Text="0";
        }
 
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i<=100;i++)
            {
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(i);
                //simpleButton1.Invoke(new Action(() => {
                //    printFinancialReport();
                //}));
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            progressBar1.Value = e.ProgressPercentage;
            lblpercent.Text = string.Format("Processing...{0}%", e.ProgressPercentage);
            progressBar1.Update();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Transaction Successfully Closed!");
            this.Close();
        }

        Double getDenomValue(double multiplier,string text)
        {
            double total = 0.0;
            total = Convert.ToDouble(text) * multiplier;
            return total;
        }

        public void printFinancialReport(bool isPrint,string userid)
        {
            try
            {

                String details = "";
                string filepath = "C:\\POSTransaction\\FinancialReport\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + userid + "\\";
                details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

                details += Classes.ReceiptSetup.doHeader(Login.assignedBranch,Environment.MachineName);
                
                string petsa = DateTime.Now.ToShortDateString();
                string oras = DateTime.Now.ToShortTimeString();
                string fulldate1 = petsa + ' ' + oras;
                DateTime dt = DateTime.Now;
                string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

                double endingbalance = 0.0;
                string fullname = Database.getSingleQuery("Users", $"UserID='{userid}'", "FullName");

                details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER : " + fullname) + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                //details += HelperFunction.PrintCenterText("X - READING") + Environment.NewLine;
                details += HelperFunction.PrintCenterText("Cashier's Accountability Report") + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftText("TRAN.#: " + txttransactionno.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER : " + fullname) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("GROSS SALES: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalgross.Text))) + Environment.NewLine; //total sales
                details += HelperFunction.PrintLeftRigthText("LESS: RETURN/REFUND: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalReturnedTransaction.Text)*-1)) + Environment.NewLine; //total sales
                double grosssalesadjusted = 0.0;
                grosssalesadjusted = Convert.ToDouble(txttotalgross.Text) - Convert.ToDouble(txtTotalReturnedTransaction.Text);
                details += HelperFunction.PrintLeftRigthText("GROSS SALES ADJUSTED: ", HelperFunction.numericFormat(grosssalesadjusted)) + Environment.NewLine + Environment.NewLine; //total sales
                details += HelperFunction.PrintLeftText("LESS: DISCOUNTS") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of SC Discount: ", txtnoofscdisc.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofscdisc.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", txtnoofpwddisc.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofpwddisc.Text))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. of NAAC Discount: ", txtnoofscdisc.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of NAAC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofscdisc.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of MOV Discount: ", txtnoofpwddisc.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of MOV Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofpwddisc.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of SP Discount: ", txtnoofscdisc.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of SP Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofscdisc.Text))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. Regular Discount: ", txtnoofregdisc.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofregdisc.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Disc P/Item: ", txtnoofdiscount.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Disc P/Item: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalDiscount.Text))) + Environment.NewLine;

                details += HelperFunction.createEqualLine() + Environment.NewLine;

                double overalltotaldiscounts = 0.0;
                Math.Round(overalltotaldiscounts = Convert.ToDouble(txtTotalDiscount.Text) +
                                        Convert.ToDouble(txttotalofscdisc.Text) +
                                        Convert.ToDouble(txttotalofpwddisc.Text) +
                                        Convert.ToDouble(txttotalofregdisc.Text), 2);

                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNTS: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(overalltotaldiscounts.ToString())*-1 )) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("LESS VAT ADJUSTMENT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatadjustment.Text) * -1)) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
                double totalnetsales = 0.0;
                totalnetsales = Convert.ToDouble(txtTotalNetSales.Text);
                //totalnetsales = grosssalesadjusted - overalltotaldiscounts - Convert.ToDouble(txtvatadjustment.Text);
                //details += HelperFunction.PrintLeftRigthText("TOTAL NET SALES: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalNetSales.Text))) + Environment.NewLine; //total sales txtTotalSales.Text)
                details += HelperFunction.PrintLeftRigthText("TOTAL NET SALES: ", HelperFunction.convertToNumericFormat(totalnetsales)) + Environment.NewLine; //total sales txtTotalSales.Text)
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("MODE OF PAYMENT") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                //CASH AND CREDIT MUST BE THE NET
                details += HelperFunction.PrintLeftRigthText("CASH:", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCashSales.Text))) + Environment.NewLine; //total sales
                details += HelperFunction.PrintLeftRigthText("CREDIT:", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCreditSales.Text))) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER'S AUDIT DETAILS") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtBeginningCash.Text))) + Environment.NewLine; //numitemsold
                //endingbalance = grosssales + Convert.ToDouble(txtBeginningCash.Text);
                //endingbalance = Convert.ToDouble(txtTotalNetSales.Text);// + Convert.ToDouble(txtBeginningCash.Text);
                endingbalance = totalnetsales;// + Convert.ToDouble(txtBeginningCash.Text);
                details += HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(endingbalance)) + Environment.NewLine; //numitemsold

                details += HelperFunction.PrintLeftRigthText("Actual Cash On Hand: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtActualCashOnHand.Text))) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Cash Remittance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtCashRemitted.Text))) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Shortage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtshortage.Text))) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Overage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtoverage.Text))) + Environment.NewLine + Environment.NewLine; //numitemsold

                //details += HelperFunction.PrintLeftRigthText("Transaction #: ", txtlasttranno.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Beginning SI No.: ", txtbeginninginvoice.Text) + Environment.NewLine; //beginvoice
                details += HelperFunction.PrintLeftRigthText("Ending SI No.: ", txtendingsi.Text) + Environment.NewLine; //lastornum

                details += HelperFunction.PrintLeftRigthText("Beginning Return Trans No.: ", txtbegretno.Text) + Environment.NewLine; //beginvoice
                details += HelperFunction.PrintLeftRigthText("Ending Return Trans No.: ", txtendretno.Text) + Environment.NewLine; //lastornum

                details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", txtnoofsolditem.Text) + Environment.NewLine; //numitemsold
                                                                                                                                //details += HelperFunction.PrintLeftRigthText("Transaction Count: ", txttransactioncount.Text) + Environment.NewLine; //numtranscunt
                                                                                                                                //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", txtnoofreturneditem.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalReturnedTransaction.Text))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", txtnoofcancelleditem.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCancelledTransaction.Text))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", txtnoofvoiditem.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalVoidTransaction.Text))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. VAT Items: ", txtnoofvat.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total VATable Items Sold: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalTax.Text))) + Environment.NewLine;

                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatablesales.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatamount.Text))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("Net Sales of VAT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtnetsalesofvat.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Exempt Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatexemptsale.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtzeroratedsale.Text))) + Environment.NewLine + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("ACCUMULATED GRAND TOTAL", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalNetSales.Text))) + Environment.NewLine + Environment.NewLine; //total sales txtTotalSales.Text)
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("C A S H  C O U N T") + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER: " + Login.Fullname) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("[DENOMIN] - ", "[QTY] = [ AMT ]") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("     1000 - ", txt1k.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1k.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("      500 - ", txt5h.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl5h.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("      200 - ", txt2h.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl2h.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("      100 - ", txt1h.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1h.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("       50 - ", txt50p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl50.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("       20 - ", txt20p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl20.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("       10 - ", txt10p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl10.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("        5 - ", txt5p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl5.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("        1 - ", txt1p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("      .25 - ", txt25c.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl25c.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("       .5 - ", txt5c.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl5c.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("       .1 - ", txt1c.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1c.Text))) + Environment.NewLine;
                details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("TOTAL     P ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtActualCashOnHand.Text))) + Environment.NewLine;
                details += HelperFunction.PrintRightToLeft(" ", "=======") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

                details += HelperFunction.PrintLeftText("Certified Correct By : " + fullname) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
                details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
                details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
                //details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                //details += HelperFunction.PrintCenterText("L OR.#:" + txtlastornumber.Text) + Environment.NewLine;


                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

                details += HelperFunction.LastPagePaper();
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                transno = txtcashiertransno.Text + ".txt";
                string filetoprint = filepath + transno;
                string mark = filepath + transno;
                StreamWriter writer = new StreamWriter(filepath + transno);
                writer.Write(details);
                writer.Close();
                if (isPrint == true)
                {
                    Printing printfile = new Printing();
                    printfile.printTextFile(filetoprint);
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        //public void printFinancialReport()
        //{
        //    try
        //    {

        //        String details = "";
        //        string filepath = "C:\\POSTransaction\\FinancialReport\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\";
        //        details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

        //        details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);

        //        //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
        //        //details += HelperFunction.PrintCenterText("YOUR TRADENAME HERE") + Environment.NewLine;
        //        //details += HelperFunction.PrintCenterText("YOUR NAME HERE") + Environment.NewLine;
        //        //details += HelperFunction.PrintCenterText("YOUR VAT REGISTERED") + Environment.NewLine + Environment.NewLine;
        //        //details += HelperFunction.PrintCenterText("TIN NO: 000-000-000-001") + Environment.NewLine + Environment.NewLine;

        //        string petsa = DateTime.Now.ToShortDateString();
        //        string oras = DateTime.Now.ToShortTimeString();
        //        string fulldate1 = petsa + ' ' + oras;
        //        DateTime dt = DateTime.Now;
        //        string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

        //        double grosssales = 0.0,endingbalance=0.0;
                
        //        details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("CASHIER : " + Login.Fullname) + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("X - READING") + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("Cashier's Accountability Report") + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("TRAN.#: " + txttransactionno.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("CASHIER : " + Login.Fullname) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("Terminal #: "+Environment.MachineName) + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        grosssales = Convert.ToDouble(txtTotalCashSales.Text) + Convert.ToDouble(txtTotalCreditSales.Text);
        //        //details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", HelperFunction.convertToNumericFormat(grosssales))+ Environment.NewLine; //total sales txtTotalSales.Text)
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("CASH :", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCashSales.Text))) + Environment.NewLine; //total sales
        //        details += HelperFunction.PrintLeftRigthText("CREDIT :", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCreditSales.Text))) + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("RETURN :", HelperFunction.convertToNumericFormat(txtTotalVoidTransaction.Text)) + Environment.NewLine; //total void
               
        //        details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("GROSS SALE ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalgross.Text))) + Environment.NewLine; //total sales
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtBeginningCash.Text))) + Environment.NewLine; //numitemsold
        //        //endingbalance = grosssales + Convert.ToDouble(txtBeginningCash.Text);
        //        endingbalance = Convert.ToDouble(txtTotalNetSales.Text);// + Convert.ToDouble(txtBeginningCash.Text);
        //        details += HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(endingbalance)) + Environment.NewLine; //numitemsold

        //        details += HelperFunction.PrintLeftRigthText("Actual Cash On Hand: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtActualCashOnHand.Text))) + Environment.NewLine; //numitemsold
        //        details += HelperFunction.PrintLeftRigthText("Cash Remittance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtCashRemitted.Text))) + Environment.NewLine; //numitemsold
        //        details += HelperFunction.PrintLeftRigthText("Shortage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtshortage.Text))) + Environment.NewLine; //numitemsold
        //        details += HelperFunction.PrintLeftRigthText("Overage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtoverage.Text))) + Environment.NewLine + Environment.NewLine; //numitemsold

        //        //details += HelperFunction.PrintLeftRigthText("Transaction #: ", txtlasttranno.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Beginning SI No.: ", txtbeginninginvoice.Text) + Environment.NewLine; //beginvoice
        //        details += HelperFunction.PrintLeftRigthText("Ending SI No.: ", txtendingsi.Text) + Environment.NewLine; //lastornum

        //        details += HelperFunction.PrintLeftRigthText("Beginning Return Trans No.: ", txtbegretno.Text) + Environment.NewLine; //beginvoice
        //        details += HelperFunction.PrintLeftRigthText("Ending Return Trans No.: ", txtendretno.Text) + Environment.NewLine; //lastornum

        //        details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", txtnoofsolditem.Text) + Environment.NewLine; //numitemsold
        //        //details += HelperFunction.PrintLeftRigthText("Transaction Count: ", txttransactioncount.Text) + Environment.NewLine; //numtranscunt
        //                                                                                                                                                                 //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", txtnoofreturneditem.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalReturnedTransaction.Text))) + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", txtnoofcancelleditem.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCancelledTransaction.Text))) + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", txtnoofvoiditem.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalVoidTransaction.Text))) + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("No. VAT Items: ", txtnoofvat.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total VATable Items Sold: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalTax.Text))) + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", txtnoofdiscount.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Discounts: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalDiscount.Text))) + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("DISCOUNT BREAKDOWN" ) + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of SC Discount: ", txtnoofscdisc.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofscdisc.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", txtnoofpwddisc.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofpwddisc.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. Regular Discount: ", txtnoofregdisc.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txttotalofregdisc.Text))) + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine +Environment.NewLine;


        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatablesales.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatamount.Text))) + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("Net Sales of VAT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtnetsalesofvat.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("VATable Exempt Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatexemptsale.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtzeroratedsale.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine;
                
        //        details += HelperFunction.PrintLeftRigthText("Less: Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalDiscount.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("ACCUMULATED GRAND TOTAL", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalNetSales.Text))) + Environment.NewLine; //total sales txtTotalSales.Text)

        //        details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("C A S H  C O U N T") + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("Terminal #: "+Environment.MachineName) + Environment.NewLine;
        //        details += HelperFunction.createDottedLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("CASHIER: " + Login.Fullname) + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("[DENOMIN] - ", "[QTY] = [ AMT ]") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText(" 1000 -  ", txt1k.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1k.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("  500 -  ", txt5h.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl5h.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("  200 -  ", txt2h.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl2h.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("  100 -  ", txt1h.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1h.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("   50 -  ", txt50p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl50.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("   20 -  ", txt20p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl20.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("   10 -  ", txt10p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl10.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("    5 -  ", txt5p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl5.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("    1 -  ", txt1p.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl1.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("  .25 -  ", txt25c.Text + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(lbl25c.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("TOTAL     P ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtActualCashOnHand.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "=======") + Environment.NewLine;
               
        //        details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.createDottedLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.createDottedLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
        //        //details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

        //        //details += HelperFunction.PrintCenterText("L OR.#:" + txtlastornumber.Text) + Environment.NewLine;
               

        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

        //        details += HelperFunction.LastPagePaper();
        //        if (!Directory.Exists(filepath))
        //        {
        //            Directory.CreateDirectory(filepath);
        //        }
        //        transno = txtcashiertransno.Text + ".txt";
        //        string filetoprint = filepath + transno;
        //        string mark = filepath + transno;
        //        StreamWriter writer = new StreamWriter(filepath + transno);
        //        writer.Write(details);
        //        writer.Close();
        //        Printing printfile = new Printing();
        //        printfile.printTextFile(filetoprint);
        //    }
        //    catch(SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}
 
        void inputDenomination()
        {
            
            simpleButton2.Enabled = false;
            txt1k.Enabled = true;
            txt5h.Enabled = true;
            txt2h.Enabled = true;
            txt1h.Enabled = true;
            txt50p.Enabled = true;
            txt20p.Enabled = true;
            txt10p.Enabled = true;
            txt5p.Enabled = true;
            txt1p.Enabled = true;
            txt25c.Enabled = true;
            txt10c.Enabled = true;
            txt5c.Enabled = true;
            txt1c.Enabled = true;
            simpleButton3.Visible = true;
            txt1k.Focus();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        void confirmDenomination()
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure all input fields are Correct?", "Confirm Sales Denomination");
            if (ok)
            {
                txtActualCashOnHand.Text = confirmDenominations().ToString();
             
                simpleButton2.Enabled = true;
                txt1k.Enabled = false;
                txt5h.Enabled = false;
                txt2h.Enabled = false;
                txt1h.Enabled = false;
                txt50p.Enabled = false;
                txt20p.Enabled = false;
                txt10p.Enabled = false;
                txt5p.Enabled = false;
                txt1p.Enabled = false;
                txt25c.Enabled = false;
                txt10c.Enabled = false;
                txt5c.Enabled = false;
                txt1c.Enabled = false;
                simpleButton3.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void btndone_Click(object sender, EventArgs e)
        {
            
        }

        double confirmDenominations()
        {
            double totalcashonhand = 0.0;
            totalcashonhand = Convert.ToDouble(lbl1k.Text) + 
                                Convert.ToDouble(lbl5h.Text) + 
                                Convert.ToDouble(lbl2h.Text) + 
                                Convert.ToDouble(lbl1h.Text) + 
                                Convert.ToDouble(lbl50.Text) + 
                                Convert.ToDouble(lbl20.Text) + 
                                Convert.ToDouble(lbl10.Text) + 
                                Convert.ToDouble(lbl5.Text) + 
                                Convert.ToDouble(lbl1.Text) + 
                                Convert.ToDouble(lbl25c.Text) +
                                Convert.ToDouble(lbl10c.Text) +
                                Convert.ToDouble(lbl5c.Text) +
                                Convert.ToDouble(lbl1c.Text);
            return totalcashonhand;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            confirm();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            inputDenomination();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            confirmDenomination();
        }

        
        private void txt10c_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(0.10, txt10c.Text);
            lbl10c.Text = value.ToString();
        }

        private void txt25c_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(0.25, txt25c.Text);
            lbl25c.Text = value.ToString();
        }

        private void txt1p_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(1, txt1p.Text);
            lbl1.Text = value.ToString();
        }

        private void txt5p_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(5, txt5p.Text);
            lbl5.Text = value.ToString();
        }

        private void txt10p_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(10, txt10p.Text);
            //denomtotal = value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl10.Text = value.ToString();
        }

        private void txt20p_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(20, txt20p.Text);
            //denomtotal = value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl20.Text = value.ToString();
        }

        private void txt50p_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(50, txt50p.Text);
            //denomtotal = value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl50.Text = value.ToString();
        }

        private void txt1h_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(100, txt1h.Text);
            //denomtotal = value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl1h.Text = value.ToString();
        }

        private void txt2h_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(200, txt2h.Text);
            //denomtotal = value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl2h.Text = value.ToString();
        }

        private void txt5h_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(500, txt5h.Text);
            //denomtotal = value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl5h.Text = value.ToString();
        }

        private void txt1k_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(1000, txt1k.Text);
            //denomtotal += value;
            //lbldenomtotal.Text = denomtotal.ToString();
            lbl1k.Text = value.ToString();
        }

        private void txt1k_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt5h.Focus();
        }

        private void txt5h_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt2h.Focus();
        }

        private void txt2h_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt1h.Focus();
        }

        private void txt1h_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt50p.Focus();
        }

        private void txt50p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt20p.Focus();
        }

        private void txt20p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt10p.Focus();
        }

        private void txt10p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt5p.Focus();
        }

        private void txt5p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt1p.Focus();
        }

        private void txt1p_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt25c.Focus();
        }

        private void txt25c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt10c.Focus();
        }

        private void txt10c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt5c.Focus();
        }

        private void txt5c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txt1c.Focus();
        }

        private void txt1c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                simpleButton3.Focus();
        }

        private void txt5c_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(0.05, txt5c.Text);
            lbl5c.Text = value.ToString();
        }

        private void txt1c_EditValueChanged(object sender, EventArgs e)
        {
            double value = 0.0;
            value = getDenomValue(0.01, txt1c.Text);
            lbl1c.Text = value.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt1k_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt5h_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt2h_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt1h_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt50p_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt20p_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt10p_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt5p_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt1p_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt25c_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt10c_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt5c_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txt1c_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}