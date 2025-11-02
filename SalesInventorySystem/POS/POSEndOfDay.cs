using DevExpress.XtraEditors;
using Newtonsoft.Json;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSEndOfDay : Form
    {
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
                if (simpleButton1.Enabled == true) { this.Dispose(); }
               
            }
           
            return functionReturnValue;
        }

        void checker()
        {
            //THERE ARE CASHIERS TRANSACTION THAT NOT YET CLOSED
            bool isNotClosedTransaction = Database.checkifExist("SELECT TOP(1) BranchCode " +
                                                               "FROM dbo.SalesTransactionSummary " +
                                                               "WHERE BranchCode='" + Login.assignedBranch + "' AND MachineUsed='"+Environment.MachineName+"' " +
                                                               "and isOpen=1 "); //all transaction must be closed, no filtering of date
            if (isNotClosedTransaction)
            {
                this.Size = new Size(900, 219);
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
                simpleButton1.Enabled = false;
            }
            else
            {
               
                this.Size = new Size(323, 219);
                this.StartPosition = FormStartPosition.CenterScreen;
                //lblchecker.Text = "All Cashier Transactions are Completed!...";
                grouppendingtran.Visible = false;
                //pictureEditcheck.Visible = true;
                //pictureEditwrong.Visible = false;
                simpleButton1.Enabled = true;
            }
                
        }

        void executeEOD()
        {
            //check if one or more cashier transaction is not yet closed
            bool EODEmailConfirm = Database.checkifExist("SELECT isnull(EODEmailNotification,0) FROM dbo.POSType WHERE EODEmailNotification=1");

            bool isNotClosedTransaction = Database.checkifExist("SELECT TOP(1) BranchCode " +
                                                                "FROM dbo.SalesTransactionSummary " +
                                                                "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                " AND MachineUsed='"+Environment.MachineName+"'" +
                                                                "and isOpen=1 "); //all transaction must be closed, no filtering of date
            //check if END OF DAY is already EXECUTED
            bool isExists = Database.checkifExist("SELECT TOP(1) MachineUsed " +
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
                XtraMessageBox.Show("END OF DAY PROCESS SUCCESSFULLY EXECUTED!!!...");

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

        void XXX(string datetoday)
        {
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            double totalPerItemDiscount = 0.0, total = 0.0;
            String details = "";
            string filepath = "C:\\ProgramFlies\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string paytype = "", amounttender = "", amountchange = "", TotalVatableSalesSum = "", TotalVatSaleSum = "", TotalVatExemptSum = "", CashierTransNo = "";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            //details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("SELECT Description," +
               " a.QtySold " +
               ", a.ReferenceNo" +
               ", c.NewRefNo" +
               ", a.DiscountTotal" +
               ", a.TotalAmount" +
               ", a.SellingPrice" +
               ", a.isVat" +
               ", b.TotalVATExemptSale" +
               ", b.TotalVatableSale" +
               ", b.TotalVATSale" +
               ", b.AmountTendered" +
               ", b.AmountChange" +
               ", b.PaymentType" +
               ", b.CashierTransNo" +
                " FROM BatchSalesDetails a " +
                "LEFT OUTER JOIN BatchSalesSummary b " +
                "ON a.ReferenceNo=b.ReferenceNo " +
                "AND a.BranchCode=b.BranchCode " +
                "AND a.MachineUsed=b.MachineUsed " +
                "LEFT OUTER JOIN ITCRESLS001 c " +
                "ON a.BranchCode=c.BranchCode " +
                "AND a.MachineUsed=c.MachineUsed " +
                "AND a.ReferenceNo=c.ReferenceNo " +
                "WHERE CAST(a.DateOrder as date)='" + datetoday + "' ", con);
            //"AND a.ReferenceNo IN (SELECT ReferenceNo FROM ITCRESLS001 WHERE CAST(Transdate as Date)='" + datetoday + "' AND BranchCode='" + Login.assignedBranch + "' AND MachineUsed='" + Environment.MachineName + "') ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);

            //var rowz = Database.getMultipleQuery("POSInfoDetails","BranchCode='"+Login.assignedBranch+"' AND MachineUsed='"+Environment.MachineName+"'");
            Dictionary<string, List<DataRow>> receipts = new Dictionary<string, List<DataRow>>();
            foreach (DataRow row in table.Rows)
            {
                string referenceNo = row["ReferenceNo"].ToString();
                if (!receipts.ContainsKey(referenceNo))
                    receipts[referenceNo] = new List<DataRow>();
                receipts[referenceNo].Add(row);
            }

            foreach (KeyValuePair<string, List<DataRow>> kvp in receipts) //List<DataRow> receipt
            {
                List<DataRow> receipt = kvp.Value;
                string referenceNo = kvp.Key;
                details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
                details += Classes.ReceiptSetup.doTitle("SALES INVOICE");

                details += Classes.ReceiptSetup.doHeaderDetailsX("",referenceNo, " ", " ", " ", " ", " "," ","");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                foreach (DataRow row in receipt)
                {
                    CashierTransNo = row["CashierTransNo"].ToString();
                    TotalVatableSalesSum = row["TotalVatableSale"].ToString();
                    TotalVatSaleSum = row["TotalVATSale"].ToString();
                    TotalVatExemptSum = row["TotalVATExemptSale"].ToString();

                    amounttender = row["AmountTendered"].ToString();
                    amountchange = row["AmountChange"].ToString();
                    paytype = row["PaymentType"].ToString();
                    totalPerItemDiscount += Convert.ToDouble(row["DiscountTotal"].ToString());
                    total += Convert.ToDouble(row["TotalAmount"].ToString());
                    string addV = "";
                    if (Convert.ToBoolean(row["isVat"].ToString()) == true)
                    {
                        addV = "V";
                    }
                    else
                    {
                        addV = "";
                    }
                    string addD = "";
                    if (Convert.ToDouble(Convert.ToDouble(row["DiscountTotal"].ToString())) > 0)
                    {
                        addD = "  - (Less: Discount)";
                    }
                    else
                    {
                        addD = "";
                    }
                    details += HelperFunction.PrintLeftText(row["Description"].ToString()) + Environment.NewLine;
                    string a = "  - " + row["QtySold"].ToString() + " @ " + row["SellingPrice"].ToString();
                    double cleanbalance = 0.0;
                    cleanbalance = Convert.ToDouble(row["TotalAmount"].ToString()) + Convert.ToDouble(row["DiscountTotal"].ToString());

                    // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                    string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                    details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                    if (Convert.ToDouble(row["DiscountTotal"].ToString()) > 0)
                    {
                        details += HelperFunction.PrintLeftRigthText(addD, "(" + row["DiscountTotal"].ToString() + ")") + Environment.NewLine;
                    }
                }
                details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
                if (Convert.ToDouble(totalPerItemDiscount) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", totalPerItemDiscount.ToString()) + Environment.NewLine;
                }
                details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total.ToString()) + Environment.NewLine;
                details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
                //--------------------------------------------
                //double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
                //netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
                //if (isDiscount == true)
                //{
                //    if (disctype == "SENIOR")
                //    {
                //        details += HelperFunction.createDottedLine() + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                //        details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                //        details += HelperFunction.createDottedLine() + Environment.NewLine;

                //        ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //        ///netofvatafteronetimedisc

                //        lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                //        netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                //        //lessscdisc = Math.Round(netofvat * 0.05, 2);
                //        lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                //        netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                //        addvat = Math.Round(netofscdisc * .12, 2);
                //        totaltotal = Math.Round(netofscdisc + addvat, 2);

                //        details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                //        ///////////////////////////////////////////////////////////////////////////////////////////////////////

                //        details += HelperFunction.createDottedLine() + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                //    }
                //    else if (disctype == "PWD")
                //    {
                //        details += HelperFunction.createDottedLine() + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;

                //        details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                //        details += HelperFunction.createDottedLine() + Environment.NewLine;

                //        lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                //        netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                //        //lessscdisc = Math.Round(netofvat * 0.05, 2);
                //        lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                //        netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                //        addvat = Math.Round(netofscdisc * .12, 2);
                //        totaltotal = Math.Round(netofscdisc + addvat, 2);

                //        details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                //        details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                //    }
                //    else if (disctype == "REGULAR")
                //    {
                //        details += HelperFunction.createDottedLine() + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                //        details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.discremarks) + Environment.NewLine + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                //        details += HelperFunction.createDottedLine() + Environment.NewLine;

                //        lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                //        netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                //        //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                //        lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                //        netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                //        addvat = Math.Round(netofscdisc * .12, 2);
                //        totaltotal = Math.Round(netofscdisc + addvat, 2);

                //        details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                //        details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                //        details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                //    }

                //}
                //--------------------------------------------
                if (paytype == "Credit")
                {
                    details += HelperFunction.PrintLeftRigthText("TENDERED:", amounttender) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
                }
                details += HelperFunction.PrintLeftRigthText("TENDERED:", amounttender) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", amountchange) + Environment.NewLine + Environment.NewLine;
                bool iszerorated = false;
                if (iszerorated == true)
                {
                    details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
                }
                else
                {

                    details += HelperFunction.PrintLeftRigthText("VATable Sales", TotalVatableSalesSum) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("VAT Amount", TotalVatSaleSum) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", TotalVatExemptSum) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
                }

                details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
                //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
                details += HelperFunction.LastPagePaper();
                receipt.Clear();
            }

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string txtorder = "\\" + CashierTransNo + "_E-JOURNAL.txt";
            string filetoprint = filepath + txtorder;

            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();
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




            //details += HelperFunction.PrintLeftRigthText("CASH :", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCashSales))) + Environment.NewLine; //total sales
            //details += HelperFunction.PrintLeftRigthText("CREDIT :", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCreditSales))) + Environment.NewLine;
            ////details += HelperFunction.PrintLeftRigthText("RETURN :", HelperFunction.convertToNumericFormat(txtTotalVoidTransaction.Text)) + Environment.NewLine; //total void

            //details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("GROSS SALE ", HelperFunction.convertToNumericFormat(grosssales)) + Environment.NewLine; //total sales
            //details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(BeginningBalance))) + Environment.NewLine; //numitemsold
            //details += HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))) + Environment.NewLine + Environment.NewLine; //numitemsold

            //details += HelperFunction.PrintLeftRigthText("Beginning SI No.: ", BeginSI) + Environment.NewLine; //beginvoice
            //details += HelperFunction.PrintLeftRigthText("Ending SI No.: ", EndingSI) + Environment.NewLine + Environment.NewLine; //lastornum

            //details += HelperFunction.PrintLeftRigthText("Beginning Return Transaction No.: ", BeginRetNo) + Environment.NewLine; //beginvoice
            //details += HelperFunction.PrintLeftRigthText("Ending Return Transaction No.: ", EndingRetNo) + Environment.NewLine + Environment.NewLine; //lastornum

            //details += HelperFunction.PrintLeftRigthText("Overage: ", "0.00") + Environment.NewLine;                                                                              //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", noofsolditems) + Environment.NewLine;                                                                              //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", noofcancelleditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalcancelledsales))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Void Items: ", noofvoiditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Void Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalvoidsales))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", noofreturneditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalreturnedsales))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. VAT Items: ", noofvatitems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Amount of VATable Items Sold: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalvatsales))) + Environment.NewLine + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", noofdiscountitems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Discounts: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;
            ////////////////////////////////////////////////////////
            //details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("DISCOUNT BREAKDOWN") + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("No. of Senior Discount: ", noofscdisc) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalscdisc))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", noofpwddisc) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalpwddisc))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Regular Discount: ", noofregdisc) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalregdisc))) + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

            ////////////////////////////////////////////////////////
           
            //details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatableSales))) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatAmount))) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("VAT Exempt Sale: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatExemptSales))) + Environment.NewLine;

            ////details += HelperFunction.PrintLeftRigthText("Net Sales of VAT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtnetsalesofvat.Text))) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", "0.00") + Environment.NewLine;
            //details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Less: Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;
            //details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("Accumulated Grand Total ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))) + Environment.NewLine; //total sales txtTotalSales.Text)

            //details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            //details += HelperFunction.createAsteriskLine() + Environment.NewLine;
           
            //details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            //details += HelperFunction.createDottedLine() + Environment.NewLine;
            
            //details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            //details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;

            //details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            //details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

            //details += HelperFunction.LastPagePaper();
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

        //void embedToJournal()
        //{
        //    string details = String.Empty;
        //    string filepath = "C:\\POSTransaction\\FinancialReport\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\ZREAD\\"+txtcashiertransno.Text+".txt";

        //    //string filepath = "C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt"; //open this or file
        //    //string filepathpath = "C:\\POSTransaction\\CopyForReprint\\CopyForReprint\\" + txtorno.Text + ".txt"; //open this or file
        //    var fileContent = string.Empty;
        //    using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
        //    {
        //        fileContent = File.ReadAllText(filepath); //copy this or file
        //    }

        //    //embed to ejournal file
        //    string filepathorig = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
        //    string txtorder = "\\" + txtcashiertransno.Text + "_E-JOURNAL.txt";
        //    string filetoprint = filepathorig + txtorder;
        //    StreamWriter writer;//,writer22;

        //    if (!Directory.Exists(filepathorig))
        //    {
        //        Directory.CreateDirectory(filepathorig);
        //        writer = new StreamWriter(filetoprint);
        //    }
        //    else
        //    {
        //        writer = new StreamWriter(filetoprint, true);
        //    }
        //    writer.Write(fileContent);
        //    writer.Close();
        //}

        private void POSEndOfDay_Load(object sender, EventArgs e)
        {
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

                var json = JsonConvert.SerializeObject(sale);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://itcore-apps.com:8181/api/zreadings", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("ZRead pushed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to push sale: " + response.ReasonPhrase);
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

                                TenantID = 1,
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


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            progressBarControl1.Position = 0;
            bool confirm = HelperFunction.ConfirmDialog("Are you Sure you want to Execute END OF DAY Transaction?", "End Of Day");
            if (confirm)
            {
                executeEOD();
              //  pushit();
                //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                //authfrm.ShowDialog(this);
                //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                //{
                   
                //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                //    authfrm.Dispose();
                //}
            }
            else
            {
                return;
            }
            this.Dispose();
            //POSUploadChecker posUpload = new POSUploadChecker();
            //posUpload.TopMost = true;
            //posUpload.Show();
            
            
        }
        //NOT USED
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                
                //backup();
                //Thread.Sleep(1000);
                //uploadBatchSalesSummary();
                //Thread.Sleep(1000);
                //uploadBatchSalesDetails();
                //Thread.Sleep(1000);
                //uploadSalesTransactionSummary();
                //Thread.Sleep(1000);
                //uploadTransactionCash();
                //Thread.Sleep(1000);
                //uploadSalesDiscount();
                //Thread.Sleep(1000);
                //uploadSalesDenomination();
                //Thread.Sleep(1000);
                //uploadPOSReturnTransaction();
                //Thread.Sleep(1000);
                //uploadPOSZReadingTransactions();
                //Thread.Sleep(1000);
                //uploadPOSCreditCardTransactions();

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

        void uploadBatchSalesSummary()
        {
            Database.display("SELECT * FROM BatchSalesSummary WHERE BranchCode='"+Login.assignedBranch+"' " +
                "AND CAST(TransDate as date)='"+DateTime.Today.ToShortDateString()+"' " +
                "AND MachineUsed='"+Environment.MachineName+"'"
                ,gridControlBatchSalesSummary,gridViewBatchSalesSummary);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewBatchSalesSummary.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO BatchSalesSummary VALUES(" +
                "'" + gridViewBatchSalesSummary.GetRowCellValue(I, "BranchCode").ToString()              + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "ReferenceNo").ToString()             + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "CashierTransNo").ToString()          + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "CustomerNo").ToString()              + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "Invoice").ToString()                 + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalItem").ToString()               + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalItemSold").ToString()           + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalItemCancelled").ToString()      + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalItemVoid").ToString()           + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalItemReturned").ToString()       + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalItemDiscount").ToString()       + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalVatableItems").ToString()       + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalSoldAmount").ToString()         + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalCancelledAmount").ToString()    + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalVoidAmount").ToString()         + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalReturnedAmount").ToString()     + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalDiscountAmount").ToString()     + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalCharge").ToString()             + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalUnitPrice").ToString()          + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalSellingPrice").ToString()       + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalTax").ToString()                + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalKilos").ToString()              + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "SubTotal").ToString()                + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalAmount").ToString()             + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalIncome").ToString()             + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalVATSale").ToString()            + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalVATExemptSale").ToString()      + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "TotalVatableSale").ToString()        + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "PaymentType").ToString()             + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "AdvancePayment").ToString()          + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "AmountTendered").ToString()          + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "AmountChange").ToString()            + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "isFloat").ToString()                 + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "isHold").ToString()                  + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "isVoid").ToString()                  + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "isErrorCorrect").ToString()          + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "OnHoldName").ToString()              + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "VoidBy").ToString()                  + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "Transdate").ToString()               + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "PreparedBy").ToString()              + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "MachineUsed").ToString()             + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "Posted").ToString()                  + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "Status").ToString()                  + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "isUpload").ToString()                + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "isPosted").ToString()                + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "DiscountType").ToString()            + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "SeniorControlNo").ToString()         + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "SeniorName").ToString()              + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "SeniorDiscount").ToString()          + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "PwdIDNo").ToString()                 + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "PwdName").ToString()                 + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "PwdDiscountAmount").ToString()       + "'" +
                ",'" +gridViewBatchSalesSummary.GetRowCellValue(I, "ZeroRatedSale").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
                
            }
        }

        void uploadBatchSalesDetails()
        {
            Database.display("SELECT * FROM BatchSalesDetails WHERE BranchCode='" + Login.assignedBranch + "' " +
                "AND CAST(DateOrder as date)='" + DateTime.Today.ToShortDateString() + "' " +
                "AND MachineUsed='" + Environment.MachineName + "'"
                , gridControlBatchSalesDetails, gridViewBatchSalesDetails);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewBatchSalesDetails.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO BatchSalesDetails VALUES(" +
                "'" +  gridViewBatchSalesDetails.GetRowCellValue(I, "SequenceNumber").ToString()          + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "BranchCode").ToString()             + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "ReferenceNo").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "CashierTransNo").ToString()         + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "ProductCode").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "CategoryCode").ToString()           + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "Barcode").ToString()                + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "Description").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "Cost").ToString()                   + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "OriginalSellingPrice").ToString()   + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "SellingPrice").ToString()           + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "QtySold").ToString()                + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "PrevQty").ToString()                + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "UnitMeasure").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "DiscountRate").ToString()           + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "DiscountTotal").ToString()          + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "ChargeRate").ToString()             + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "ChargeTotal").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "TaxRate").ToString()                + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "TaxTotal").ToString()               + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "SubTotal").ToString()               + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "Income").ToString()                 + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isHold").ToString()                 + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isConfirmed").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isCancelled").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isVoid").ToString()                 + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isErrorCorrect").ToString()         + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isVat").ToString()                  + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "HoldBy").ToString()                 + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "CancelledBy").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "VoidBy").ToString()                 + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "ProcessedBy").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "TotalAmount").ToString()            + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "Status").ToString()                 + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "DateOrder").ToString()              + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isUpload").ToString()               + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isPosted").ToString()               + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "isCosting").ToString()              + "'" +
                ",'" + gridViewBatchSalesDetails.GetRowCellValue(I, "MachineUsed").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }

           
        }

        void uploadTransactionCash()
        {
            Database.display("SELECT * FROM TransactionCash WHERE Branch='" + Login.assignedBranch + "' " +
                   "AND CAST(TransactionDate as date)='" + DateTime.Today.ToShortDateString() + "' " +
                   "AND MachineUsed='" + Environment.MachineName + "'"
                   , gridControlTransactionCash, gridViewTransactionCash);
            Thread.Sleep(500);
            for (int I=0;I<=gridViewTransactionCash.RowCount-1;I++)
            {
                Database.ExecuteQuery2("INSERT INTO TransactionCash VALUES(" +
              "'" + gridViewTransactionCash.GetRowCellValue(I, "TransactionDate").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "CashierTransNo").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "Reference").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "EnteredBy").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "ApproveBy").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "Branch").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "TransCode").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "Amount").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "ErrorTag").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "Remarks").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "isUpload").ToString() + "'" +
              ",'" + gridViewTransactionCash.GetRowCellValue(I, "MachineUsed").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
           
        }

        void uploadSalesTransactionSummary()
        {
            Database.display("SELECT * FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' " +
                   "AND CAST(DateOpen as date)='" + DateTime.Today.ToShortDateString() + "' " +
                   "AND MachineUsed='" + Environment.MachineName + "'"
                   , gridControlSalesTransactionSummary, gridViewSalesTransactionSummary);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewSalesTransactionSummary.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO SalesTransactionSummary VALUES(" +
                "'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "CashierTransNo").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TransactionDate").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "UserID").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "BeginningCash").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "BeginningSI").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "EndingSI").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "BeginningTransactionNo").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "EndingTransactionNo").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "BeginningReturnTransNo").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "EndingReturnTransNo").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NextBeginningCash").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TransactionBegin").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfSoldItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalSoldItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfCancelledItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalCancelledItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfVoidItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalVoidItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfReturnedItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalReturnedSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfDiscount").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalDiscount").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfSCDisc").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalSCDisc").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfPWDDisc").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalOfPWDDisc").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfRegDisc").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalOfRegDisc").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfCharges").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalCharge").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "NoOfTaxItem").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalTax").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalCashSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalCreditSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalActualCash").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalNetSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalCoins").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "CashRemitted").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "OpenBy").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "ClosedBy").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "DateClosed").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "isOpen").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "DateOpen").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "isUpload").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "Shortage").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "Overage").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalVatableSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalVatExemptSales").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalNetSalesOfVat").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalVatAmount").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "TotalZeroRatedSale").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "VatAdjustment").ToString() + "'" +
                   ",'" + gridViewSalesTransactionSummary.GetRowCellValue(I, "MachineUsed").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));

            }
            
        }

        void uploadSalesDenomination()
        {
            Database.display("SELECT * FROM SalesDenomination WHERE BranchCode='" + Login.assignedBranch + "' " +
                     "AND CAST(TransactionDate as date)='" + DateTime.Today.ToShortDateString() + "' " +
                     "AND MachineUsed='" + Environment.MachineName + "'"
                     , gridControlSalesDenom, gridViewSalesDenom);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewSalesDenom.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO SalesDenomination VALUES(" +
                "'" + gridViewSalesDenom.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "TransactionCode").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Cashier").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "TransactionDate").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No1k").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total1k").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No5h").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total5h").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No2h").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total2h").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No1h").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total1h").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No50p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total50p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No20p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total20p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No10p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total10p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No5p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total5p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No1p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total1p").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No25c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total25c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No10c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total10c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No5c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total5c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "No1c").ToString() + "'" +
                      ",'" + gridViewSalesDenom.GetRowCellValue(I, "Total1c").ToString() + "'" +
                   ",'" + gridViewSalesDenom.GetRowCellValue(I, "MachineUsed").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
        }

        void uploadSalesDiscount()
        {
            Database.display("SELECT * FROM SalesDiscount WHERE BranchCode='" + Login.assignedBranch + "' " +
                        "AND CAST(DateExecute as date)='" + DateTime.Today.ToShortDateString() + "' " +
                        "AND MachineUsed='" + Environment.MachineName + "'"
                        , gridControlSalesDiscount, gridViewSalesDiscount);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewSalesDiscount.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO SalesDiscount VALUES(" +
                        "'" + gridViewSalesDiscount.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "TransactionNo").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "OrderNo").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "CashierTransNo").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "DiscountType").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "DiscountAmount").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "VatAdjustment").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "DiscName").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "DiscIDNo").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "DiscRemarks").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "Cashier").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "MachineUsed").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "DateExecute").ToString() + "'" +
                        ",'" + gridViewSalesDiscount.GetRowCellValue(I, "isErrorCorrect").ToString() + "'" +
                   ",'" + gridViewSalesDiscount.GetRowCellValue(I, "VatExemptAdj").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
        }

        void uploadPOSZReadingTransactions()
        {
            Database.display("SELECT * FROM POSZReadingTransactions WHERE BranchCode='" + Login.assignedBranch + "' " +
                          "AND CAST(DateExecute as date)='" + DateTime.Today.ToShortDateString() + "' " +
                          "AND MachineUsed='" + Environment.MachineName + "'"
                          , gridControlZReading, gridViewZReading);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewZReading.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO POSZReadingTransactions VALUES(" +
                       "'" + gridViewZReading.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "CounterNo").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TransactionNo").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "MachineUsed").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "BeginningBalance").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "Debit").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "Credit").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "EndingBalance").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "BeginningSINo").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "EndingSINo").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "BeginningReturnTransNo").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "EndingReturnTransNo").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "SoldItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "CancelledItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "VoidItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "ReturnedItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "VatItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "DiscountItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "SCDiscItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "PWDDiscItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "RegDiscItems").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalCashSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalCreditSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalCancelledSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalVoidSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalReturnedSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalDiscount").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalSCDiscount").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalPWDDiscount").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalRegDiscount").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "TotalVatSales").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "VatExemptSale").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "VatableSale").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "VatInput").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "ZeroRatedSale").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "VatAdjustment").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "DateExecute").ToString() + "'" +
                      ",'" + gridViewZReading.GetRowCellValue(I, "ExecuteBy").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
            
        }

        void uploadPOSCreditCardTransactions()
        {
            Database.display("SELECT * FROM POSCreditCardTransactions WHERE BranchCode='" + Login.assignedBranch + "' " +
                             "AND CAST(TransactionDate as date)='" + DateTime.Today.ToShortDateString() + "' " +
                             "AND MachineUsed='" + Environment.MachineName + "'"
                             , gridControlPOSCreditcard, gridViewPOSCreditcard);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewPOSCreditcard.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO POSCreditCardTransactions VALUES(" +
                       "'" + gridViewPOSCreditcard.GetRowCellValue(I, "TransactionCode").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "ReferenceNo").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "TransactionDate").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCName").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCNumber").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCType").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCExpirydate").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCBank").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCBankMerchant").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "CCPaymentReferenceNo").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "Amount").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "isCleared").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "DateAdded").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "DateCleared").ToString() + "'" +
                        ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "ProcessedBy").ToString() + "'" + 
                      ",'" + gridViewPOSCreditcard.GetRowCellValue(I, "MachineUsed").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
        }

        void uploadPOSReturnTransaction()
        {
            Database.display("SELECT * FROM POSReturnTransaction WHERE BranchCode='" + Login.assignedBranch + "' " +
                                "AND CAST(DateExecute as date)='" + DateTime.Today.ToShortDateString() + "' " +
                                "AND MachineUsed='" + Environment.MachineName + "'"
                                , gridControlPOSReturn, gridViewPOSReturn);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewPOSReturn.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO POSReturnTransaction VALUES(" +
                       "'" + gridViewPOSReturn.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "ReturnTransactionNo").ToString() + "'" +
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "OrderNo").ToString() + "'" +
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "TransactionNo").ToString() + "'" +
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "CashierTransCode").ToString() + "'" +
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "DateExecute").ToString() + "'" +
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "ExecuteBy").ToString() + "'" + 
                       ",'" + gridViewPOSReturn.GetRowCellValue(I, "MachineUsed").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
        }

        void uploadPOSTransaction()
        {
            Database.display("SELECT * FROM POSTransaction WHERE BranchCode='" + Login.assignedBranch + "' " +
                                "AND CAST(DateAdded as date)='" + DateTime.Today.ToShortDateString() + "' " +
                                "AND MachineUsed='" + Environment.MachineName + "'"
                                , gridControlPOSTransaction, gridViewPOSTransaction);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewPOSTransaction.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO POSTransaction VALUES(" +
                       "'" + gridViewPOSTransaction.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "TransactionNo").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "MachineUsed").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "Type").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "DateAdded").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "ExecuteBy").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "ReprintCtr").ToString() + "'" +
                       ",'" + gridViewPOSTransaction.GetRowCellValue(I, "ReturnedCtr").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }
            
        }
        void uploadPOSEOD()
        {
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToString() + "')");
            Database.display("SELECT * FROM POSEODMonitoring WHERE BranchCode='" + Login.assignedBranch + "' " +
                                "AND TransactionDate='" + transdate.Trim() + "' " +
                                "AND MachineUsed='" + Environment.MachineName + "'"
                                , gridControlPOSEOD, gridViewPOSEOD);
            Thread.Sleep(500);
            for (int I = 0; I <= gridViewPOSEOD.RowCount - 1; I++)
            {
                Database.ExecuteQuery2("INSERT INTO POSEODMonitoring VALUES(" +
                       "'" + gridViewPOSEOD.GetRowCellValue(I, "BranchCode").ToString() + "'" +
                       ",'" + gridViewPOSEOD.GetRowCellValue(I, "TransactionDate").ToString() + "'" +
                       ",'" + gridViewPOSEOD.GetRowCellValue(I, "MachineUsed").ToString() + "'" +
                       ",'" + gridViewPOSEOD.GetRowCellValue(I, "isCashBegin").ToString() + "'" +
                       ",'" + gridViewPOSEOD.GetRowCellValue(I, "isEndOfDay").ToString() + "')"
               , Database.getConnection(@"AAITCRE\ConnSettingsCloud"));
            }

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
    }
}
