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
using System.IO;

namespace SalesInventorySystem.POS
{
    public partial class POSXread : DevExpress.XtraEditors.XtraForm
    {
        public POSXread()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
          
            bool confirm = HelperFunction.ConfirmDialog("Are you Sure you want to Execute END OF DAY Transaction?", "End Of Day");
            if (confirm)
            {
                executeXRead();
                //pushit();
              
            }
            else
            {
                return;
            }
            this.Dispose();
        }

        void execute()
        {
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + txttransactiondate.Text + "')");
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_POSXReading";
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
        void executeXRead()
        {
            //check if one or more cashier transaction is not yet closed
            //bool EODEmailConfirm = Database.checkifExist("SELECT isnull(EODEmailNotification,0) FROM dbo.POSType WHERE EODEmailNotification=1");

            //bool isNotClosedTransaction = Database.checkifExist("SELECT TOP(1) BranchCode " +
            //                                                    "FROM dbo.SalesTransactionSummary " +
            //                                                    "WHERE BranchCode='" + Login.assignedBranch + "' " +
            //                                                    " AND MachineUsed='" + Environment.MachineName + "'" +
            //                                                    "and isOpen=1 "); //all transaction must be closed, no filtering of date


            //if (isNotClosedTransaction == true) //wla gi close ang transaction, wlay xread report ang cashier or si cashier wla nag close transaction
            //{
            //    XtraMessageBox.Show("The System found out that there are some transactions are not yet closed!..");
            //    return;
            //}
            //else
            //{
            //    execute();

            //    if (checkBox1.Checked == true)
            //    {
            //        PrintXRead(); //is the total sales of all machines

            //    }


            //    XtraMessageBox.Show("XREAD PROCESS SUCCESSFULLY EXECUTED!!!...");

            //}

            execute();

            if (checkBox1.Checked == true)
            {
                PrintXRead(); //is the total sales of all machines

            }


            XtraMessageBox.Show("XREAD PROCESS SUCCESSFULLY EXECUTED!!!...");
        }

        void PrintXRead()
        {
            DateTime dt1 = Convert.ToDateTime(txttransactiondate.Text);

            String details = "";
            string filepath = "C:\\POSTransaction\\XREADING\\" + dt1.ToString("yyyyMMdd") + "\\";
            //string filepath = "C:\\POSTransaction\\EndOfDay\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string branchcode, CounterNo, TransactionNo, MachineUsed, BeginningBalance, EndingBalance, BeginSI, EndingSI, BeginRetNo, EndingRetNo, TotalCashSales, TotalCreditSales, TotalSales, TotalDiscount, VatExemptSales, VatableSales, VatAmount, DateExeute, ExecuteBy, TotalNetSales;
            string noofsolditems, noofcancelleditems, noofvoiditems, noofreturneditems, noofvatitems, noofdiscountitems, totalcancelledsales, totalvoidsales, totalreturnedsales, totalvatsales;
            string noofscdisc, noofpwddisc, noofregdisc, totalscdisc, totalpwddisc, totalregdisc, totalzeroratedsales, VatAdjustment;

            var rows = Database.getMultipleQuery("POSXReadingTransactions",
              "BranchCode='" + Login.assignedBranch + "' " +
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

            details += Classes.ReceiptSetup.doHeader(branchcode, Environment.MachineName);

            string petsa = dt1.ToShortDateString();//DateTime.Now.ToShortDateString();
            string oras = dt1.ToShortDateString();// DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;
            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

            double grosssales = 0.0;

            details += HelperFunction.PrintLeftText(dt1.ToString(format)) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("X - READING") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;

            details += HelperFunction.PrintLeftText("X Counter #: " + CounterNo) + Environment.NewLine;
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
            for (int x = 0; x <= gridView2.RowCount - 1; x++)
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


        private void POSXread_Load(object sender, EventArgs e)
        {
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "'", "TransactionNo", 1);
            txttransactionno.Text = HelperFunction.sequencePadding1(refnumber.ToString(), 10);
            string check = "", dateTran = "";
            DateTime dt;
            check = POS.POSTransactionChecker.frmTrnCheck;
            if (String.IsNullOrEmpty(check))
            {
                txttransactiondate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {

                dt = Convert.ToDateTime(POS.POSTransactionChecker.eoddate);
                dateTran = dt.ToShortDateString();
                txttransactiondate.Text = dateTran;
            }
            
        }
    }
}