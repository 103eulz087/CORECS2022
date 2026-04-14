using DevExpress.XtraEditors;
using Newtonsoft.Json;
using SalesInventorySystem.Classes;
using SalesInventorySystem.SalesModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSEndOfDay : Form
    {
        int retryupload = 0;
        struct DataParameter
        {
            public int Process;
            public int Delay;
        }

        private DataParameter _inputParameter;
        public POSEndOfDay()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                if (btnexecuteEOD.Enabled == true) { this.Dispose(); }
               
            }
            if (keyData == (Keys.X | Keys.Control)) //PAYMENT
            {
                btnexecuteEOD.Enabled = true;
            }
            return functionReturnValue;
        }

        void checker()
        {
            //THERE ARE CASHIERS TRANSACTION THAT NOT YET CLOSED
            bool isNotClosedTransaction = Database.checkifExist("SELECT 1 " +
                                                               "FROM dbo.SalesTransactionSummary " +
                                                               "WHERE BranchCode='" + Login.assignedBranch + "' AND MachineUsed='"+Environment.MachineName+"' " +
                                                               "and isOpen=1 "); //all transaction must be closed, no filtering of date
            if (isNotClosedTransaction)
            {
                //this.Size = new Size(900, 219);
                this.StartPosition = FormStartPosition.CenterScreen;
                // lblchecker.Text = "There are cashier transactions that not yet closed!..";

                 Database.display("SELECT BranchCode,UserID,TransactionDate,isOpen,MachineUsed FROM SalesTransactionSummary " +
                        "WHERE BranchCode='" + Login.assignedBranch + "' " +
                        "and isOpen=1 " +
                        //"and UserID='" + Login.isglobalUserID + "' " +
                        //"and TransactionDate <> '" + charDate.Trim() + "' " +
                        "AND MachineUsed='" + Environment.MachineName.ToString() + "' ", gridControl1, gridView1);

                grouppendingtran.Visible = true;
                //pictureEditwrong.Visible = true;
                //pictureEditcheck.Visible = false;
                btnexecuteEOD.Enabled = false;
            }
            else
            {
               
                //this.Size = new Size(323, 219);
                this.StartPosition = FormStartPosition.CenterScreen;
                //lblchecker.Text = "All Cashier Transactions are Completed!...";
                grouppendingtran.Visible = false;
                //pictureEditcheck.Visible = true;
                //pictureEditwrong.Visible = false;
                //btnexecuteEOD.Enabled = true;
            }
                
        }

        void executeEOD()
        {
            //check if one or more cashier transaction is not yet closed
            bool EODEmailConfirm = Database.checkifExist("SELECT 1 FROM dbo.POSType WHERE EODEmailNotification=1");

            bool isNotClosedTransaction = Database.checkifExist("SELECT 1 " +
                                                                "FROM dbo.SalesTransactionSummary " +
                                                                "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                " AND MachineUsed='"+Environment.MachineName+"'" +
                                                                "and isOpen=1 "); //all transaction must be closed, no filtering of date
            //check if END OF DAY is already EXECUTED
            bool isExists = Database.checkifExist("SELECT 1 " +
                                                    "FROM dbo.POSZReadingTransactions " +
                                                    "WHERE MachineUsed='" + Environment.MachineName + "' " +
                                                    "and DateExecute='" + txttransactiondate.Text + "' ");
                                                    //"and DateExecute='" + DateTime.Now.ToShortDateString() + "' ");

            if (isNotClosedTransaction == true) //wla gi close ang transaction, wlay xread report ang cashier or si cashier wla nag close transaction
            {
                XtraMessageBox.Show("The System found out that there are some transactions are not yet closed!..");
                return;
            }
            else if (isExists == true) //humana og execute ang end of day...dli na pwedi mkaduha og process
            {
                XtraMessageBox.Show("The System found out that EndOfDay Already Process!..");
                return;
            }
            else
            {
                execute();
                //executeV2();
                //XXX(txttransactiondate.Text);
                Thread.Sleep(500);
                progressBarControl1.Position = 20;
                Thread.Sleep(500);
                if (checkBox1.Checked == true)
                {
                    PrintZRead(); //is the total sales of all machines
                    Thread.Sleep(100);
                    progressBarControl1.Position = 35;
                }

                if (EODEmailConfirm) { sendMailNotificationEOD(txttransactiondate.Text); sendMailNotificationEODGroupSales(); }
                progressBarControl1.Position = 80;
                Thread.Sleep(1000);
                backup();
                Thread.Sleep(600);
                progressBarControl1.Position = 100;
                Thread.Sleep(1000);
                //XtraMessageBox.Show("END OF DAY PROCESS SUCCESSFULLY EXECUTED!!!...");
                BigAlert.Show("END OF DAY SUCCESS", "END OF DAY PROCESS SUCCESFULLY EXECUTED!!..", MessageBoxIcon.Information);

            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            
        }

        void execute()
        {
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + txttransactiondate.Text + "')");
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_POSZReading";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtransno", txttransactionno.Text);
                com.Parameters.AddWithValue("@parmtransdate", transdate.Trim());
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
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
            con.Close();
        }

        void executeV2()
        {
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + txttransactiondate.Text + "')");
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spitcr_001";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmmachinname", Environment.MachineName); 
                com.Parameters.AddWithValue("@parmtransdate", txttransactiondate.Text);
                com.Parameters.AddWithValue("@parmpercentage", "0.7");
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            con.Close();
        }

        
        void PrintZRead()
        {
            DateTime dt1 = Convert.ToDateTime(txttransactiondate.Text);
            
            String details = "";
            string filepath = "C:\\POSTransaction\\EndOfDay\\" + dt1.ToString("yyyyMMdd") + "\\";
            //string filepath = "C:\\POSTransaction\\EndOfDay\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string branchcode, CounterNo, TransactionNo, MachineUsed, BeginningBalance, EndingBalance, BeginSI, EndingSI,BeginRetNo,EndingRetNo, TotalCashSales, TotalCreditSales, TotalSales, TotalDiscount, VatExemptSales, VatableSales, VatAmount, DateExeute, ExecuteBy, TotalNetSales;
            string noofsolditems, noofcancelleditems, noofvoiditems, noofreturneditems, noofvatitems, noofdiscountitems, totalcancelledsales, totalvoidsales, totalreturnedsales, totalvatsales;
            string noofscdisc, noofpwddisc, noofregdisc, totalscdisc, totalpwddisc, totalregdisc, totalzeroratedsales, VatAdjustment;

            var rows = Database.getMultipleQuery("POSZReadingTransactions",
              "BranchCode='"+Login.assignedBranch+"' " +
              "and CAST(DateExecute as date)='" + dt1.ToShortDateString() + "' " +
              //"and CAST(DateExecute as date)='" + DateTime.Now.ToShortDateString() + "' " +
                "and MachineUsed='" + Environment.MachineName + "' ",
                "BranchCode" +
                ",CounterNo" +
                ",TransactionNo" +
                ",MachineUsed" +
                ",BeginningBalance" +
                ",EndingBalance" +
                ",BeginningSINo" +
                ",EndingSINo" +
                ",BeginningReturnTransNo" +
                ",EndingReturnTransNo" +
                ",SoldItems" +
                ",CancelledItems" +
                ",VoidItems" +
                ",ReturnedItems" +
                ",VatItems" +
                ",DiscountItems" +
                ",SCDiscItems" +
                ",PWDDiscItems" +
                ",RegDiscItems" +
                ",TotalCashSales" +
                ",TotalCreditSales" +
                ",TotalSales" +
                ",TotalCancelledSales" +
                ",TotalVoidSales" +
                ",TotalReturnedSales" +
                ",TotalVatSales" +
                ",TotalDiscount" +
                ",TotalSCDiscount" +
                ",TotalPWDDiscount" +
                ",TotalRegDiscount" +
                ",VatExemptSale" +
                ",VatableSale" +
                ",VatInput" +
                ",VatAdjustment" +
                ",ZeroRatedSale" +
                ",DateExecute" +
                ",ExecuteBy" +
                ",TotalNetSales");

            branchcode = rows["BranchCode"].ToString(); // Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "BranchCode");
            CounterNo = rows["CounterNo"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "CounterNo");
            TransactionNo = rows["TransactionNo"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TransactionNo");
            MachineUsed = rows["MachineUsed"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "MachineUsed");
            BeginningBalance = rows["BeginningBalance"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "BeginningBalance");
            EndingBalance = rows["EndingBalance"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "EndingBalance");
            BeginSI = rows["BeginningSINo"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "BeginningSINo");
            EndingSI = rows["EndingSINo"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "EndingSINo");
            BeginRetNo = rows["BeginningReturnTransNo"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "BeginningReturnTransNo");
            EndingRetNo = rows["EndingReturnTransNo"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "EndingReturnTransNo");

            noofsolditems = rows["SoldItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "SoldItems");
            noofcancelleditems = rows["CancelledItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "CancelledItems");
            noofvoiditems = rows["VoidItems"].ToString(); // Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "VoidItems");
            noofreturneditems = rows["ReturnedItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "ReturnedItems");
            noofvatitems = rows["VatItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "VatItems");
            noofdiscountitems = rows["DiscountItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "DiscountItems");

            noofscdisc = rows["SCDiscItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "SCDiscItems");
            noofpwddisc = rows["PWDDiscItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "PWDDiscItems");
            noofregdisc = rows["RegDiscItems"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "RegDiscItems");

            TotalCashSales = rows["TotalCashSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalCashSales");
            TotalCreditSales = rows["TotalCreditSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalCreditSales");
            TotalSales = rows["TotalSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalSales");

            totalcancelledsales = rows["TotalCancelledSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalCancelledSales");
            totalvoidsales = rows["TotalVoidSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalVoidSales");
            totalreturnedsales = rows["TotalReturnedSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalReturnedSales");
            totalvatsales = rows["TotalVatSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalVatSales");

            TotalDiscount = rows["TotalDiscount"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalDiscount");

            totalscdisc = rows["TotalSCDiscount"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalSCDiscount");
            totalpwddisc = rows["TotalPWDDiscount"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalPWDDiscount");
            totalregdisc = rows["TotalRegDiscount"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "TotalRegDiscount");


            VatExemptSales = rows["VatExemptSale"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "VatExemptSale");
            VatableSales = rows["VatableSale"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "VatableSale");
            VatAmount = rows["VatInput"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "VatInput");
            VatAdjustment = rows["VatAdjustment"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "VatInput");
            totalzeroratedsales = rows["ZeroRatedSale"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "ZeroRatedSale");


            DateExeute = rows["DateExecute"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "DateExecute");
            ExecuteBy = rows["ExecuteBy"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "ExecuteBy");
            TotalNetSales = rows["TotalNetSales"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "ExecuteBy");

            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            details += Classes.ReceiptSetup.doHeader(branchcode,Environment.MachineName);

            string petsa = dt1.ToShortDateString();//DateTime.Now.ToShortDateString();
            string oras = dt1.ToShortDateString();// DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;
            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

            double grosssales = 0.0;

            details += HelperFunction.PrintLeftText(dt1.ToString(format)) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Z - READING") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Z Counter #: " + CounterNo) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran #: " + txttransactionno.Text) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            grosssales = Convert.ToDouble(TotalSales);
            //details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", HelperFunction.convertToNumericFormat(grosssales))+ Environment.NewLine; //total sales txtTotalSales.Text)
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("GROSS SALES :", HelperFunction.convertToNumericFormat(grosssales)) + Environment.NewLine; //total sales
            details += HelperFunction.PrintLeftRigthText("LESS: RETURN/REFUND:", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalreturnedsales))) + Environment.NewLine;
            double grosssalesadjusted = 0.0;
            grosssalesadjusted = Convert.ToDouble(grosssales) - Convert.ToDouble(totalreturnedsales);
            details += HelperFunction.PrintLeftRigthText("GROSS SALES ADJUSTED:", HelperFunction.convertToNumericFormat(grosssalesadjusted)) + Environment.NewLine + Environment.NewLine; //total void

            details += HelperFunction.PrintLeftText("LESS: DISCOUNTS") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of SC Discount: ", noofscdisc) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalscdisc))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", noofpwddisc) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalpwddisc))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Regular Discount: ", noofregdisc) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalregdisc))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Disc P/Item: ", noofdiscountitems) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Disc P/Item: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine;
            double overalltotaldiscounts = 0.0;
            Math.Round(overalltotaldiscounts = Convert.ToDouble(TotalDiscount) +
                                    Convert.ToDouble(totalscdisc) +
                                    Convert.ToDouble(totalpwddisc) +
                                    Convert.ToDouble(totalregdisc), 2);
            details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNTS: ", HelperFunction.convertToNumericFormat(overalltotaldiscounts)) + Environment.NewLine + Environment.NewLine;

            //double totalvatadjustment = 0.0;
            //totalvatadjustment = Database.getTotalSummation2("SalesDiscount", "isErrorCorrect=0 " +
            //    "AND MachineUsed='" + Environment.MachineName.ToString() + "' " +
            //    "AND BranchCode='" + Login.assignedBranch + "' " +
            //    "AND DiscountType <> 'OTHERS' " +
            //    "AND CAST(DateExecute as date)='" + DateTime.Now.ToShortDateString() + "' ", "VatAdjustment");
            details += HelperFunction.PrintLeftRigthText("LESS VAT ADJUSTMENT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatAdjustment) * -1)) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            //double totalnetsales = 0.0;
            //totalnetsales = Convert.ToDouble(TotalSales);// -totalvatadjustment; 
            details += HelperFunction.PrintLeftRigthText("TOTAL NET SALES: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalNetSales))) + Environment.NewLine; //total sales txtTotalSales.Text)
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("MODE OF PAYMENT") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASH:", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCashSales))) + Environment.NewLine; //total sales
            details += HelperFunction.PrintLeftRigthText("CREDIT:", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCreditSales))) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("DETAILS") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(BeginningBalance))) + Environment.NewLine; //numitemsold
            details += HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))) + Environment.NewLine + Environment.NewLine; //numitemsold

            details += HelperFunction.PrintLeftRigthText("Beginning SI No.: ", BeginSI) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Ending SI No.: ", EndingSI) + Environment.NewLine + Environment.NewLine; //lastornum

            details += HelperFunction.PrintLeftRigthText("Beg. Ret Tran No.: ", BeginRetNo) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Ending Ret Tran No.: ", EndingRetNo) + Environment.NewLine + Environment.NewLine; //lastornum

            details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", noofsolditems) + Environment.NewLine;                                                                              //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", noofcancelleditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalcancelledsales))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Void Items: ", noofvoiditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Void Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalvoidsales))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", noofreturneditems) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalreturnedsales))) + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatableSales))) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatAmount))) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Exempt Sale: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatExemptSales))) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalzeroratedsales))) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("ACCUMULATED GRAND TOTAL ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))) + Environment.NewLine; //total sales txtTotalSales.Text)
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("ProductName", "Qty") + Environment.NewLine;
            for (int x=0;x<=gridView2.RowCount-1;x++)
            {
                details += HelperFunction.PrintLeftRigthText(gridView2.GetRowCellValue(x, "ProductName").ToString(), gridView2.GetRowCellValue(x, "Qty").ToString()) + Environment.NewLine;

            }
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

            details += HelperFunction.LastPagePaper();
            //to be contnued
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //string transno = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string transno = dt1.ToString("yyyyMMdd") + ".txt";
            string filetoprint = filepath + transno;
            string mark = filepath + transno;
            StreamWriter writer = new StreamWriter(filepath + transno);
            writer.Write(details);
            writer.Close();
            Printing printfile = new Printing();
            printfile.printTextFile(filetoprint);
            //embedToJournal();
        }

        private void POSEndOfDay_Load(object sender, EventArgs e)
        {
            bool checkifDataUploading = Database.checkifExist("SELECT 1 FROM dbo.POSType WHERE DataUploading=1");
            if (!checkifDataUploading)
            {
                BigAlert.Show("SETUP NOT CONFIGURED FOR CLOUD UPLOADING",
                    "Your POSTYPE CONFIGURATION has disabled DATA UPLOADING, YOU CAN PROCEED TO STEP 2 Directly.",
                    MessageBoxIcon.Warning);
                btnuploadsales.Enabled = false;
                btnexecuteEOD.Enabled = true;
              
            }
            else
            {
                btnuploadsales.Enabled = true;
                btnexecuteEOD.Enabled = false;

            }
            
            //this.Text = this.Size.ToString();
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "'", "TransactionNo", 1);
            txttransactionno.Text = HelperFunction.sequencePadding1(refnumber.ToString(),10);
            string check = "", dateTran = "";
            DateTime dt;
            check = POS.POSTransactionChecker.frmTrnCheck;
            if(String.IsNullOrEmpty(check))
            {
                txttransactiondate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
               
                dt = Convert.ToDateTime(POS.POSTransactionChecker.eoddate);
                dateTran = dt.ToShortDateString();
                txttransactiondate.Text = dateTran;
            }
            checker();
        }

        void sendMailNotificationEOD(string dateTran)
        {
            try
            {
                string subject = "", body = "";
                var rows = Database.getMultipleQuery("POSZReadingTransactions", "MachineUsed='" + GlobalVariables.computerName + "' " +
                   "AND BranchCode='" + Login.assignedBranch + "' " +
                   "AND DateExecute='" + dateTran + "' ",
                   "BranchCode,MachineUsed,TotalCashSales,TotalCreditSales,TotalSales");
                string BranchCode, MachineUsed, TotalCashSales, TotalCreditSales, TotalSales;
                BranchCode = rows["BranchCode"].ToString();
                MachineUsed = rows["MachineUsed"].ToString();
                TotalCashSales = rows["TotalCashSales"].ToString();
                TotalCreditSales = rows["TotalCreditSales"].ToString();
                TotalSales = rows["TotalSales"].ToString();

                body += "======================== <br/>";
                body += "<b>END OF DAY REPORT</b><br/>";
                body += "======================== <br/>";
                body += "<b>Branch: </b>" + Branch.getBranchName(Login.assignedBranch) + "<br/>";// + Environment.NewLine;
                body += "<b>TransactionClosed: </b>" + dateTran + "<br/>";// + Environment.NewLine + Environment.NewLine;
                body += "<b>POS Machine: </b>" + GlobalVariables.computerName + "<br/>";// + Environment.NewLine + Environment.NewLine;

                body += "<b>TOTAL CASH SALES: </b>" + TotalCashSales + "<br/>";// Environment.NewLine;
                body += "<b>TOTAL CREDIT SALES: </b>" + TotalCreditSales + "<br/>";// Environment.NewLine;
                body += "<b>TOTAL NET SALES: </b>" + TotalSales + "<br/><br/>";// Environment.NewLine;

                body += "<b><i><font color='red'>Please dont reply this is a system generated report.</font></b></i>" + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                subject = "POS END OF DAY Transaction Report [" + Branch.getBranchName(Login.assignedBranch) + "]";
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParam(subject, body, false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void sendMailNotificationEODGroupSales()
        {
            try
            {
                Database.display($"Select Description as ProductName,SUM(QtySold) as Qty,SUM(SubTotal) as TotalAmount " +
                    $"FROM dbo.POSSalesSummary " +
                    $"Where BranchCode='{Login.assignedBranch}' " +
                    $"and CAST(DateOrder as date)='{txttransactiondate.Text}' AND MachineUsed='{GlobalVariables.computerName}' " +
                    $"GROUP BY Description Order By Description ASC", gridControl2, gridView2);

                string subject = "", body = "";
                

                body += "======================== <br/>";
                body += "<b>GROUP ITEM SALES</b><br/>";
                body += "======================== <br/>";
                body += "<b>Date Report: </b>" + txttransactiondate.Text + "<br/><br/>";// + Environment.NewLine + Environment.NewLine;
                body += "<table border='1' cellpadding='1' cellspacing='1'><tr><th>ProductName</th><th>TotalQtySold</th><th>TotalAmount</th></tr>";
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    string prodname = gridView2.GetRowCellValue(i,"ProductName").ToString();
                    string qty = gridView2.GetRowCellValue(i, "Qty").ToString();
                    string totalamount = gridView2.GetRowCellValue(i, "TotalAmount").ToString();
                
                    body += "<tr><td>" + prodname + "</td><td>" + qty + "</td><td>"  + HelperFunction.numericFormat(Convert.ToDouble(totalamount)) + "</td></tr>";
                }
                body += "</table>";
                body += "<b><i><font color='red'>Please dont reply this is a system generated report.</font></b></i>" + "<br/><br/>";// Environment.NewLine + Environment.NewLine;

                subject = "POS END OF DAY Transaction Report Group Item Sales Report [" + Branch.getBranchName(Login.assignedBranch) + "]";
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParam(subject, body, false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void backup()
        {
            string filepath = "C:\\BackupSalesInventory";
            Classes.Utilities.createDirectoryFolder(filepath);
                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_Backup";
               
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmLogin", Login.assignedBranch);
                    com.Parameters.AddWithValue("@BaseLocation", filepath);
                    com.Parameters.AddWithValue("@BackupType", "FULL");
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandTimeout = 3600;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
            con.Close();
        }




        public async Task PushSaleAsync(ZReadingDto sale)
        {
            using (var client = new HttpClient())
            {

                //string apiKey = "baf02cb4f4bd4e3681dc7c0ad77068e0x";
                string apiKey = "b25ffbdd1bcd428f9d60ad679e6e9d66";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("IssuedKey", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };
                var json = JsonConvert.SerializeObject(sale, settings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://itcoreapps.com:8181/api/sales", content);
                if (response.IsSuccessStatusCode) { MessageBox.Show("ZREAD Sale pushed successfully!"); }
                else
                {
                    MessageBox.Show("Failed to push sale: " + response.ReasonPhrase);
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to push sale: {response.ReasonPhrase}\nDetails: {errorDetails}");
                }
            }
        }



        public ZReadingDto GetInsertedZRead(string posId,string dateexecute)
        {
            ZReadingDto data = null;

            using (SqlConnection conn = Database.getConnection())
            {
                string query = "SELECT * FROM POSZReadingTransactions WHERE MachineUsed = @POSID And DateExecute=@dateexec";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@dateexec", dateexecute);
                    cmd.Parameters.AddWithValue("@POSID", posId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data = new ZReadingDto
                            {

                                //TenantID = '0001',
                                POSID = reader["MachineUsed"].ToString(),
                                UserID = reader["ExecuteBy"].ToString(),
                                CounterNo = reader["CounterNo"].ToString(),
                                BeginningBalance = reader.GetDecimal(reader.GetOrdinal("BeginningBalance")),
                                Debit = reader.GetDecimal(reader.GetOrdinal("Debit")),
                                Credit = reader.GetDecimal(reader.GetOrdinal("Credit")),
                                EndingBalance = reader.GetDecimal(reader.GetOrdinal("EndingBalance")),
                                BeginningSINo = reader["BeginningSINo"].ToString(),
                                EndingSINo = reader["EndingSINo"].ToString(),
                                BeginningReturnTransNo = reader["BeginningReturnTransNo"].ToString(),
                                EndingReturnTransNo = reader["EndingReturnTransNo"].ToString(),
                                SoldItems = reader.GetInt32(reader.GetOrdinal("SoldItems")),
                                CancelledItems = reader.GetInt32(reader.GetOrdinal("CancelledItems")),
                                VoidItems = reader.GetInt32(reader.GetOrdinal("VoidItems")),
                                VatItems = reader.GetInt32(reader.GetOrdinal("VatItems")),
                                DiscountItems = reader.GetInt32(reader.GetOrdinal("DiscountItems")),
                                SCDiscItems = reader.GetInt32(reader.GetOrdinal("SCDiscItems")),
                                PWDDiscItems = reader.GetInt32(reader.GetOrdinal("PWDDiscItems")),
                                RegDiscItems = reader.GetInt32(reader.GetOrdinal("RegDiscItems")),
                                TotalCashSales = reader.GetDecimal(reader.GetOrdinal("TotalCashSales")),
                                TotalCreditSales = reader.GetDecimal(reader.GetOrdinal("TotalCreditSales")),
                                TotalSales = reader.GetDecimal(reader.GetOrdinal("TotalSales")),
                                TotalCancelledSales = reader.GetDecimal(reader.GetOrdinal("TotalCancelledSales")),
                                TotalVoidSales = reader.GetDecimal(reader.GetOrdinal("TotalVoidSales")),
                                TotalReturnedSales = reader.GetDecimal(reader.GetOrdinal("TotalReturnedSales")),
                                TotalDiscount = reader.GetDecimal(reader.GetOrdinal("TotalDiscount")),
                                TotalSCDiscount = reader.GetDecimal(reader.GetOrdinal("TotalSCDiscount")),
                                TotalPWDDiscount = reader.GetDecimal(reader.GetOrdinal("TotalPWDDiscount")),
                                TotalRegDiscount = reader.GetDecimal(reader.GetOrdinal("TotalRegDiscount")),
                                TotalVatSales = reader.GetDecimal(reader.GetOrdinal("TotalVatSales")),
                                VatExemptSale = reader.GetDecimal(reader.GetOrdinal("VatExemptSale")),
                                VatableSale = reader.GetDecimal(reader.GetOrdinal("VatableSale")),
                                VatInput = reader.GetDecimal(reader.GetOrdinal("VatInput")),
                                ZeroRatedSale = reader.GetDecimal(reader.GetOrdinal("ZeroRatedSale")),
                                VatAdjustment = reader.GetDecimal(reader.GetOrdinal("VatAdjustment")),
                                TotalNetSales = reader.GetDecimal(reader.GetOrdinal("TotalNetSales")),
                                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateExecute"))

                            };
                        }
                    }
                }
            }

            return data;
        }

        async void pushit()
        {
            try
            {
                var zread = GetInsertedZRead(Environment.MachineName.ToString(), DateTime.Today.ToShortDateString());

                if (zread != null)
                {
                    await PushSaleAsync(zread);
                }
                else
                {
                    MessageBox.Show("No sale data found to push.");
                }
            }

            catch (Exception ex)
            {
                string errorMessage = $"Exception: {ex.Message}";

                if (ex.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage);
            }



        }

        async void EODwithUpload(DateTime transDate)
        {
            // Reset your progress bar (assuming you have a control named progressBar1)
            progressBar1.Value = 0;
            progressBar1.Maximum = 100; // We use 0-100 percentage

            // 1. Create the Progress handlers. 
            // These run ON THE UI THREAD whenever the background thread calls .Report()
            IProgress<int> progressHandler = new Progress<int>(p =>
            {
                progressBar1.Value = Math.Min(p, 100);
            });

            IProgress<string> statusHandler = new Progress<string>(msg =>
            {
                lblProgress.Text = msg;
            });

            try
            {
                //DateTime transDate = DateTime.Today;
                string branchCode = Login.assignedBranch;
                string machineName = Environment.MachineName;
                
                PosDataUploader uploader = new PosDataUploader();
               
                statusHandler.Report("Starting upload process...");

                // 2. Pass the handlers into the background task!
                await Task.Run(async () =>
                {

                    ///////////////////////////////////////// NEW
                    statusHandler.Report("Starting Sales Summary upload...");
                    await uploader.UploadTableToCloudAsync("BatchSalesSummary", "Transdate", transDate, branchCode, machineName, progressHandler, statusHandler);

                    statusHandler.Report("Starting Sales Details upload...");
                    await uploader.UploadTableToCloudAsync("BatchSalesDetails", "DateOrder", transDate, branchCode, machineName, progressHandler, statusHandler);

                    statusHandler.Report("Starting Sales Transaction upload...");
                    await uploader.UploadTableToCloudAsync("SalesTransactionSummary", "DateOpen", transDate, branchCode, machineName, progressHandler, statusHandler);

                    statusHandler.Report("Starting POSZReading Transaction upload...");
                    await uploader.UploadTableToCloudAsync("POSZReadingTransactions", "DateExecute", transDate, branchCode, machineName, progressHandler, statusHandler);

                    statusHandler.Report("Starting Sales Discount upload...");
                    await uploader.UploadTableToCloudAsync("SalesDiscount", "DateExecute", transDate, branchCode, machineName, progressHandler, statusHandler);

                    statusHandler.Report("Starting POSSales Summary upload...");
                    await uploader.UploadTableToCloudAsync("POSSalesSummary", "DateOrder", transDate, branchCode, machineName, progressHandler, statusHandler);

                    statusHandler.Report("Starting CreditCard Transaction upload...");
                    await uploader.UploadTableToCloudAsync("POSCreditCardTransactions","DateAdded", transDate, branchCode, machineName, progressHandler, statusHandler);

                    ///////////////////////////////////////

                    //statusHandler.Report("Starting Sales Summary upload...");
                    //await uploader.UploadBatchSalesSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);

                    //// If you have more tables, you would do this:
                    //statusHandler.Report("Starting Sales Details upload...");
                    //await uploader.UploadBatchSalesDetailsAsync(transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting Sales Transaction Summary upload...");
                    //await uploader.UploadBatchSalesTransactionSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                    
                    //statusHandler.Report("Starting POSZReading Transaction upload...");
                    //await uploader.UploadBatchZReadingTransactionsAsync(transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting Sales Discount upload...");
                    //await uploader.UploadBatchSalesDiscountAsync(transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting POSSales Summary upload...");
                    //await uploader.UploadBatchPOSSalesSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting CreditCard Transaction upload...");
                    //await uploader.UploadPOSCreditCardTransactionAsync(Convert.ToDateTime(dateTempforCC), branchCode, machineName, progressHandler, statusHandler);

                });

                MessageBox.Show("All end-of-day data uploaded seamlessly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblProgress.Text = "Error during upload.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }

        private async Task<bool> TestCloudConnectionAsync()
        {
            string cloudConnString = Database.getConnectionString(@"AAITCRE\ConnSettingsServer");
            try
            {
                // Add a strict 3-second timeout to the connection string just for this ping
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cloudConnString);
                builder.ConnectTimeout = 3;

                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    await conn.OpenAsync();
                    return true; // We got in! Internet is up.
                }
            }
            catch
            {
                return false; // Connection failed. Internet is down.
            }
        }

        public async Task<bool> VerifySupervisorAnalysisAsync(string branchCode, DateTime shiftDate)
        {
            string rawConnString = Database.getConnectionString(@"AAITCRE\ConnSettingsServer");
            // Use a builder to inject a short timeout so it doesn't "hang"
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(rawConnString);
            builder.ConnectTimeout = 5; // Fail fast after 5 seconds if no internet

            string cloudConnString = builder.ConnectionString;
            bool isAnalyzed = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(cloudConnString))
                {
                    await conn.OpenAsync();

                    string query = @"
                SELECT ISNULL(isDeducted, 0) 
                FROM [dbo].[ReInventoryMonitoring] 
                WHERE BranchCode = @BranchCode AND DateExecute = @ProcessDate";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        cmd.Parameters.AddWithValue("@ProcessDate", shiftDate.Date);

                        object result = await cmd.ExecuteScalarAsync();
                        if (result != null)
                        {
                            isAnalyzed = Convert.ToBoolean(result);
                        }
                    }
                }
                return isAnalyzed;
            }
            catch (SqlException)
            {
                // INTERNET IS DOWN: 
                // We log this or show a small tray notification, but we return TRUE 
                // to let the branch proceed with their local EOD.
                BigAlert.Show(
                          "NETWORK OFFLINE",
                          "Please Coordinate your Supervisor / TL or Contact Head Office",
                          MessageBoxIcon.Warning);
                return true;
            }
            catch (Exception)
            {
                // Any other weird error, we still let them proceed locally.
                BigAlert.Show(
                          "NETWORK OFFLINE",
                          "Please Coordinate your Supervisor / TL or Contact Head Office",
                          MessageBoxIcon.Warning);
                return true;
            }
        }
        private async void btnuploadsales_Click(object sender, EventArgs e)
        {
            btnuploadsales.Enabled = false;
            progressBarControl1.Position = 0;
            try
            {
                bool isUploadExists = Database.checkifExist($"SELECT 1 FROM dbo.BatchSalesDetails " +
                    $"WHERE BranchCode='{Login.assignedBranch}' " +
                    $"and CAST(DateOrder as date)='{txttransactiondate.Text}' " +
                    $"AND MachineUsed='{Environment.MachineName.ToString()}'");
                // The method returned false. But WHY? 
                // We need a quick ping test to see if the internet is actually down.
                bool isInternetDown = !await TestCloudConnectionAsync();
                
                if (isInternetDown)
                {
                    // =========================================================
                    // OFFLINE OVERRIDE MODE
                    // =========================================================
                    //DialogResult overrideResult = BigAlert.Show(
                    //    "NETWORK OFFLINE",
                    //    "Cannot reach the Cloud WMS. The Supervisor cannot verify inventory.\n\nDo you want to perform an OFFLINE End of Day? (Manager Authorization Required)",
                    //    MessageBoxIcon.Warning,
                    //    MessageBoxButtons.YesNo);
                    BigAlert.Show(
                           "NETWORK OFFLINE",
                           "Please Coordinate your Supervisor / TL or Contact Head Office",
                           MessageBoxIcon.Warning);
                    retryupload += 1;
                    btnuploadsales.Enabled = true;
                    btnexecuteEOD.Enabled = false;
                }
                else if (isUploadExists)
                {
                    BigAlert.Show(
                           "YOU ALREADY UPLOAD!..",
                           "You can now proceed to STEP-2 Execute End of Day..",
                           MessageBoxIcon.Warning);
                    btnexecuteEOD.Enabled = true;
                }
                else if (retryupload >= 3)
                {
                    BigAlert.Show(
                           "SUPERVISOR APPROVAL REQUIRED",
                           "You cannot execute End of Day.\n\nThe Supervisor has not yet analyzed today's inventory. Please notify them to complete the deduction.",
                           MessageBoxIcon.Warning);

                    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                    authfrm.ShowDialog(this);
                    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                    {
                        btnexecuteEOD.Enabled = true;
                        AuthorizedConfirmationFrm.isconfirmedLogin = false;
                        authfrm.Dispose();
                    }
                }
                else
                {
                    EODwithUpload(Convert.ToDateTime(txttransactiondate.Text));
                    BigAlert.Show("UPLOAD SALES SUCCESS", "Youre Supervisor / TL may now Analyze and Deduct Inventory...", MessageBoxIcon.Information);
                    btnexecuteEOD.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                BigAlert.Show("EOD ERROR", ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                btnexecuteEOD.Enabled = true;
            }
        }
    
        
        //NOT USED
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                
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
            this.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txttransactionno_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txttransactiondate_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblDateOpen_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
        void proccedToConfirm()
        {
            //bool confirm = HelperFunction.ConfirmDialog("Are you Sure you want to Execute END OF DAY Transaction?", "End Of Day");
            DialogResult confirm = BigAlert.Show(
                "EXECUTE EOD AND GENERATE ZREAD",
                "Are you sure you want to Execute End of Day Transaction?",
                MessageBoxIcon.Warning,
                MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                executeEOD();
                //pushit();
            }
            else
            {
                return;
            }
            BigAlert.Show("EOD SUCCESS", "End of Day completed successfully. You may now close the terminal.", MessageBoxIcon.Information);

        }
        private async void btnexecuteEOD_Click(object sender, EventArgs e)
        {
            btnexecuteEOD.Enabled = false;
            progressBarControl1.Position = 0;
            // 2. If it reaches here, the supervisor did their job!
            try
            {
                //check linkedserver=1 IF TRUE it means SUPERVISOR DEDUCTION MATTER
                //bool islinked = Database.checkifExist("SELECT 1 FROM dbo.POSType WHERE isLinkedServer=1");
                bool isAutoDeduct = Database.checkifExist("SELECT 1 FROM dbo.POSType WHERE isAutoSystemDeduct=1");
                if(!isAutoDeduct)
                {
                    // 1. Check Cloud for Supervisor Approval
                    bool canProceed = await VerifySupervisorAnalysisAsync(Login.assignedBranch, Convert.ToDateTime(txttransactiondate.Text));
                    if (!canProceed)
                    {
                        // Use our massive BigAlert class!

                        BigAlert.Show(
                            "SUPERVISOR APPROVAL REQUIRED",
                            "You cannot execute End of Day.\n\nThe Supervisor has not yet analyzed and deducted today's inventory. Please notify the Supervisor to complete the Inventory deduction.",
                            MessageBoxIcon.Warning);

                        btnexecuteEOD.Enabled = true;
                        return; // STOP EXECUTION HERE. They cannot proceed.
                    }
                    else
                    {
                        proccedToConfirm();
                    }
                }
                else
                {
                     proccedToConfirm();
                }

                // ... Execute your local Z-Reading printing ...
                // ... Lock the local POS terminal ...
            }
            catch (Exception ex)
            {
                BigAlert.Show("EOD ERROR", ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                btnexecuteEOD.Enabled = true;
            }
            this.Dispose();
        }
    }
}
