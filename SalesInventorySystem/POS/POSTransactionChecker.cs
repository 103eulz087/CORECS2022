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
    public partial class POSTransactionChecker : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public static string frmTrnCheck = "",eoddate="";
        public POSTransactionChecker()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        String TransactionNum()
        {
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' and MachineUsed='"+Environment.MachineName.ToString()+"'", "TransactionNo", 1);
            return HelperFunction.sequencePadding(refnumber.ToString());
        }

        void display()
        {

            string charDate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToShortDateString() + "')");
            Database.display("SELECT BranchCode,CashierTransNo,TransactionDate,UserID,isOpen,MachineUsed FROM SalesTransactionSummary " +
                    "WHERE BranchCode='" + Login.assignedBranch + "' " +
                    "and isOpen=1 " +
                    //"and UserID='" + Login.isglobalUserID + "' " +
                    "and TransactionDate <> '" + charDate.Trim() + "' " +
                    "AND MachineUsed='" + Environment.MachineName.ToString() + "' ", gridControl1, gridView1);

            Database.display("SELECT * FROM POSEODMonitoring " +
                    "WHERE BranchCode='" + Login.assignedBranch + "' " +
                    "and TransactionDate <> '" + charDate.Trim() + "' " +
                    "AND isEndOfDay=0 ", gridControl2, gridView2);

        }

        private void closeThisTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string branchcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            string cashierid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CashierTransNo").ToString();
            string machineused = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MachineUsed").ToString();
            string userid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UserID").ToString();

            if (userid != Login.isglobalUserID)
            {
                XtraMessageBox.Show("You cannot close this Transaction, beacause the System found out that you are not the Owner of this Transaction..");
                return;
            }
            else
            {
                SqlConnection con = Database.getConnection();
                con.Open();
                try
                {
                    string query = "spr_POSCloseTransaction";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmbranhcode", branchcode);
                    com.Parameters.AddWithValue("@parmcashiertransno", cashierid);
                    com.Parameters.AddWithValue("@parmmachineused", machineused);
                    com.Parameters.AddWithValue("@parmuserid", userid);

                    com.Parameters.Add("@parmtransdate", SqlDbType.Char, 8).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmtransno", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

                    com.Parameters.Add("@parmbeginsino", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmendsino", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmbegintransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmendtransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmbeginrettransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmendrettransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;

                    com.Parameters.Add("@parmbeginningcash", SqlDbType.Money).Direction = ParameterDirection.Output;

                    com.Parameters.Add("@parmnoofsolditem", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofcancelleditem", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofvoiditem", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofdiscount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofvatitems", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofreturneditem", SqlDbType.Int).Direction = ParameterDirection.Output;

                    com.Parameters.Add("@parmnoofscdisc", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofpwddisc", SqlDbType.Int).Direction = ParameterDirection.Output;
                    com.Parameters.Add("@parmnoofregdisc", SqlDbType.Int).Direction = ParameterDirection.Output;

                    com.Parameters.Add("@parmtotalofcancelleditem", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofcancelleditem"].Precision = 12;
                    com.Parameters["@parmtotalofcancelleditem"].Scale = 2;

                    com.Parameters.Add("@parmtotalofvoiditem", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofvoiditem"].Precision = 12;
                    com.Parameters["@parmtotalofvoiditem"].Scale = 2;

                    com.Parameters.Add("@parmtotalofdiscountitem", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofdiscountitem"].Precision = 12;
                    com.Parameters["@parmtotalofdiscountitem"].Scale = 2;

                    com.Parameters.Add("@parmtotalofvatitems", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofvatitems"].Precision = 12;
                    com.Parameters["@parmtotalofvatitems"].Scale = 2;

                    com.Parameters.Add("@parmtotalofreturneditems", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofreturneditems"].Precision = 12;
                    com.Parameters["@parmtotalofreturneditems"].Scale = 2;

                    com.Parameters.Add("@parmtotalofscdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofscdisc"].Precision = 12;
                    com.Parameters["@parmtotalofscdisc"].Scale = 2;


                    com.Parameters.Add("@parmtotalofpwddisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofpwddisc"].Precision = 12;
                    com.Parameters["@parmtotalofpwddisc"].Scale = 2;

                    com.Parameters.Add("@parmtotalofregdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalofregdisc"].Precision = 12;
                    com.Parameters["@parmtotalofregdisc"].Scale = 2;

                    com.Parameters.Add("@parmvatadjustment", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmvatadjustment"].Precision = 12;
                    com.Parameters["@parmvatadjustment"].Scale = 2;

                    com.Parameters.Add("@parmvatablesale", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmvatablesale"].Precision = 12;
                    com.Parameters["@parmvatablesale"].Scale = 2;


                    com.Parameters.Add("@parmvatexemptsale", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmvatexemptsale"].Precision = 12;
                    com.Parameters["@parmvatexemptsale"].Scale = 2;

                    com.Parameters.Add("@parmvatamount", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmvatamount"].Precision = 12;
                    com.Parameters["@parmvatamount"].Scale = 2;

                    com.Parameters.Add("@parmzeroratedsale", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmzeroratedsale"].Precision = 12;
                    com.Parameters["@parmzeroratedsale"].Scale = 2;


                    com.Parameters.Add("@parmtotalcashsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalcashsales"].Precision = 12;
                    com.Parameters["@parmtotalcashsales"].Scale = 2;

                    com.Parameters.Add("@parmtotalcreditsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalcreditsales"].Precision = 12;
                    com.Parameters["@parmtotalcreditsales"].Scale = 2;

                    com.Parameters.Add("@parmtotalnetsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalnetsales"].Precision = 12;
                    com.Parameters["@parmtotalnetsales"].Scale = 2;

                    com.Parameters.Add("@parmtotalgrosssales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalgrosssales"].Precision = 12;
                    com.Parameters["@parmtotalgrosssales"].Scale = 2;

                    com.Parameters.Add("@parmtotalchargetoaccountsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                    com.Parameters["@parmtotalchargetoaccountsales"].Precision = 12;
                    com.Parameters["@parmtotalchargetoaccountsales"].Scale = 2;

                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();

                    POSClosedTransaction pocls = new POSClosedTransaction();

                    pocls.txtcashiertransno.Text = cashierid;
                    pocls.txttransactionno.Text = TransactionNum(); //balikonon

                    pocls.txttransactiondate.Text = com.Parameters["@parmtransdate"].Value.ToString();
                    pocls.txtBeginningCash.Text = com.Parameters["@parmbeginningcash"].Value.ToString();

                    pocls.txtbeginninginvoice.Text = com.Parameters["@parmbeginsino"].Value.ToString();
                    pocls.txtendingsi.Text = com.Parameters["@parmendsino"].Value.ToString();

                    pocls.txtbegtransno.Text = com.Parameters["@parmbegintransno"].Value.ToString();
                    pocls.txtendtransno.Text = TransactionNum();

                    pocls.txtbegretno.Text = com.Parameters["@parmbeginrettransno"].Value.ToString();
                    pocls.txtendretno.Text = com.Parameters["@parmendrettransno"].Value.ToString();

                    pocls.txtnoofsolditem.Text = com.Parameters["@parmnoofsolditem"].Value.ToString();
                    pocls.txtnoofcancelleditem.Text = com.Parameters["@parmnoofcancelleditem"].Value.ToString();
                    pocls.txtnoofvoiditem.Text = com.Parameters["@parmnoofvoiditem"].Value.ToString();
                    pocls.txtnoofdiscount.Text = com.Parameters["@parmnoofdiscount"].Value.ToString();
                    pocls.txtnoofvat.Text = com.Parameters["@parmnoofvatitems"].Value.ToString();
                    pocls.txtnoofreturneditem.Text = com.Parameters["@parmnoofreturneditem"].Value.ToString();

                    pocls.txtnoofscdisc.Text = com.Parameters["@parmnoofscdisc"].Value.ToString();
                    pocls.txtnoofpwddisc.Text = com.Parameters["@parmnoofpwddisc"].Value.ToString();
                    pocls.txtnoofregdisc.Text = com.Parameters["@parmnoofregdisc"].Value.ToString();

                    pocls.txtTotalCancelledTransaction.Text = com.Parameters["@parmtotalofcancelleditem"].Value.ToString();
                    pocls.txtTotalVoidTransaction.Text = com.Parameters["@parmtotalofvoiditem"].Value.ToString();
                    pocls.txtTotalDiscount.Text = com.Parameters["@parmtotalofdiscountitem"].Value.ToString();
                    pocls.txtTotalTax.Text = com.Parameters["@parmtotalofvatitems"].Value.ToString();
                    pocls.txtTotalReturnedTransaction.Text = com.Parameters["@parmtotalofreturneditems"].Value.ToString();


                    pocls.txttotalofscdisc.Text = com.Parameters["@parmtotalofscdisc"].Value.ToString();
                    pocls.txttotalofpwddisc.Text = com.Parameters["@parmtotalofpwddisc"].Value.ToString();
                    pocls.txttotalofregdisc.Text = com.Parameters["@parmtotalofregdisc"].Value.ToString();

                    pocls.txtvatablesales.Text = com.Parameters["@parmvatablesale"].Value.ToString();
                    pocls.txtvatexemptsale.Text = com.Parameters["@parmvatexemptsale"].Value.ToString();
                    pocls.txtvatamount.Text = com.Parameters["@parmvatamount"].Value.ToString();
                    pocls.txtzeroratedsale.Text = com.Parameters["@parmzeroratedsale"].Value.ToString();
                    //pocls.txtnetsalesofvat.Text = com.Parameters["@parmnetsalesofvat"].Value.ToString();

                    pocls.txtTotalCashSales.Text = com.Parameters["@parmtotalcashsales"].Value.ToString();
                    pocls.txtTotalCreditSales.Text = com.Parameters["@parmtotalcreditsales"].Value.ToString();
                    pocls.txtTotalNetSales.Text = com.Parameters["@parmtotalnetsales"].Value.ToString();
                    pocls.txttotalgross.Text = com.Parameters["@parmtotalgrosssales"].Value.ToString();
                    pocls.txtchargesales.Text = com.Parameters["@parmtotalchargetoaccountsales"].Value.ToString();

                    Database.displayLocalGrid("select CONVERT(VARCHAR(10), DateOrder, 120) AS Date " +
                        ", DatePart(hh, DateOrder) as Hour " +
                        ", SUM(QtySold) as QtySold " +
                        ", SUM(TotalAmount) AS TotalAmount " +
                        ", COUNT(*) as TotalItems " +
                        "from BatchSalesDetails " +
                        "WHERE CAST(DateOrder as date)='" + DateTime.Now.ToShortDateString() + "' " +
                        "AND CashierTransNo='" + cashierid + "' " +
                        "GROUP BY CONVERT(VARCHAR(10), DateOrder, 120), DatePart(hh, DateOrder) ", pocls.MydataGridView1);

                    pocls.ShowDialog(this);
                    if (POSClosedTransaction.isdone == true)
                    {
                        POSClosedTransaction.isdone = false;
                        pocls.Dispose();
                        isdone = true;
                        display();
                        //this.Dispose();
                        // Classes.Utilities.writeTextfile("C:\\POSTransaction\\TranSeries\\counter.txt", transcode);
                    }

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

        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(gridControl2, e.Location);
        }

        void execute()
        {
            //string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"TransactionDate").ToString() + "')");
            string transdate = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString();
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_POSZReading";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"BranchCode").ToString());
                com.Parameters.AddWithValue("@parmtransno", TransactionNum());
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

        void PrintZRead()
        {
            string dateAni = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString();

            String details = "";
            string filepath = "C:\\POSTransaction\\EndOfDay\\" + dateAni + "\\";
            string branchcode, CounterNo, TransactionNo, MachineUsed, BeginningBalance, EndingBalance, BeginSI, EndingSI, BeginRetNo, EndingRetNo, TotalCashSales, TotalCreditSales, TotalSales, TotalDiscount, VatExemptSales, VatableSales, VatAmount, DateExeute, ExecuteBy;
            string noofsolditems, noofcancelleditems, noofvoiditems, noofreturneditems, noofvatitems, noofdiscountitems, totalcancelledsales, totalvoidsales, totalreturnedsales, totalvatsales;
            string noofscdisc, noofpwddisc, noofregdisc, totalscdisc, totalpwddisc, totalregdisc, totalzeroratedsales;

           
            var rows = Database.getMultipleQuery("POSZReadingTransactions",
                "DateExecute=CAST('"+ dateAni + "' as date) " +
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
                ",ZeroRatedSale" +
                ",DateExecute" +
                ",ExecuteBy");

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
            totalzeroratedsales = rows["ZeroRatedSale"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "ZeroRatedSale");


            DateExeute = rows["DateExecute"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "DateExecute");
            ExecuteBy = rows["ExecuteBy"].ToString(); //Database.getSingleQuery("POSZReadingTransactions", "DateExecute='" + DateTime.Now.ToShortDateString() + "' and MachineUsed='" + Environment.MachineName + "' ", "ExecuteBy");

            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            details += Classes.ReceiptSetup.doHeader(branchcode);

            //string petsa = DateTime.Now.ToShortDateString();
            //string oras = DateTime.Now.ToShortTimeString();
            //string fulldate1 = petsa + ' ' + oras;
            string petsa1 = Database.getSingleResultSet("SELECT CAST('" + dateAni + "' as date) as DateExecute");
            DateTime dt = Convert.ToDateTime(petsa1); //DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

            double grosssales = 0.0;

            details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Z - READING") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Z Counter #: " + CounterNo) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran #: " + TransactionNum() ) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            grosssales = Convert.ToDouble(TotalSales);
            //details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", HelperFunction.convertToNumericFormat(grosssales))+ Environment.NewLine; //total sales txtTotalSales.Text)
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASH :", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCashSales))) + Environment.NewLine; //total sales
            details += HelperFunction.PrintLeftRigthText("CREDIT :", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCreditSales))) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("RETURN :", HelperFunction.convertToNumericFormat(txtTotalVoidTransaction.Text)) + Environment.NewLine; //total void

            details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("GROSS SALE ", HelperFunction.convertToNumericFormat(grosssales)) + Environment.NewLine; //total sales
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(BeginningBalance))) + Environment.NewLine; //numitemsold
            details += HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))) + Environment.NewLine + Environment.NewLine; //numitemsold

            details += HelperFunction.PrintLeftRigthText("Beginning SI No.: ", BeginSI) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Ending SI No.: ", EndingSI) + Environment.NewLine + Environment.NewLine; //lastornum

            details += HelperFunction.PrintLeftRigthText("Beginning Return Transaction No.: ", BeginRetNo) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Ending Return Transaction No.: ", EndingRetNo) + Environment.NewLine + Environment.NewLine; //lastornum

            details += HelperFunction.PrintLeftRigthText("Overage: ", "0.00") + Environment.NewLine;                                                                              //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", noofsolditems) + Environment.NewLine;                                                                              //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", noofcancelleditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalcancelledsales))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("No. of Void Items: ", noofvoiditems) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total Void Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalvoidsales))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", noofreturneditems) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalreturnedsales))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. VAT Items: ", noofvatitems) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of VATable Items Sold: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalvatsales))) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", noofdiscountitems) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Discounts: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;
            //////////////////////////////////////////////////////
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("DISCOUNT BREAKDOWN") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Senior Discount: ", noofscdisc) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalscdisc))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", noofpwddisc) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalpwddisc))) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Regular Discount: ", noofregdisc) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalregdisc))) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

            //////////////////////////////////////////////////////

            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatableSales))) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatAmount))) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Exempt Sale: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(VatExemptSales))) + Environment.NewLine;

            //details += HelperFunction.PrintLeftRigthText("Net Sales of VAT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtnetsalesofvat.Text))) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Less: Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("Accumulated Grand Total ", HelperFunction.convertToNumericFormat(grosssales)) + Environment.NewLine; //total sales txtTotalSales.Text)

            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string transno = dateAni + ".txt";
            string filetoprint = filepath + transno;
            string mark = filepath + transno;
            StreamWriter writer = new StreamWriter(filepath + transno);
            writer.Write(details);
            writer.Close();
            Printing printfile = new Printing();
            printfile.printTextFile(filetoprint);
            //embedToJournal();
        }

        void executeEOD()
        {
            //check if one or more cashier transaction is not yet closed
            bool isNotClosedTransaction = Database.checkifExist("SELECT TOP 1 BranchCode " +
                                                                "FROM SalesTransactionSummary " +
                                                                "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                "and isOpen=1 " +
                                                                "and DateOpen <> '" + DateTime.Now.ToShortDateString() + "'  ");
            //IF END OF DAY IS ALREADY PROCESS
            bool isExists = Database.checkifExist("SELECT TOP 1 MachineUsed " +
                                                    "FROM POSZReadingTransactions " +
                                                    "WHERE MachineUsed='" + Environment.MachineName + "' " +
                                                    "and DateExecute='" + DateTime.Now.ToShortDateString() + "' ");

            if (isNotClosedTransaction == true) //wla gi close ang transaction, wlay xread report ang cashier or si cashier wla nag close transaction
            {
                XtraMessageBox.Show("The System found out that there are some transactions are not yet closed!..");
                return;
            }
            if (isExists == true) //humana og execute ang end of day...dli na pwedi mkaduha og process
            {
                XtraMessageBox.Show("The System found out that EndOfDay Already Process!..");
                return;
            }
            else
            {
                //execute();
                //PrintZRead();
                //backup();
               // Database.ExecuteQuery("exec sp_UploadPOSSalesSummary 'CAST('"+ gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString() + "' as date)','"+ Login.assignedBranch + "', ");
                XtraMessageBox.Show("END OF DAY PROCESS SUCCESSFULLY EXECUTED!!!...");
                this.Dispose();
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
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmTrnCheck = "TRUE";
            eoddate = Database.getSingleResultSet("SELECT CAST('" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString() + "' as date)");

            //string petsadate = Database.getSingleResultSet("SELECT CAST('" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString() + "' as date)");

            bool confirm = HelperFunction.ConfirmDialog("Are you Sure you want to Execute END OF DAY Transaction?.. IF YES, Please dont close until the END OF DAY PROCESS EXECUTED SUCCESSFULLY is APPEAR", "End Of Day");
            if (confirm)
            {
                bool isoverride = false;
                isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='ENDOFDAY' AND isOverride=1");
                if (!isoverride)
                {
                    POSEndOfDay posend = new POSEndOfDay();
                     
                    posend.ShowDialog(this);
                }
                else
                {
                    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                    authfrm.ShowDialog(this);
                    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                    {
                        //executeEOD();

                        POSEndOfDay posend = new POSEndOfDay();
                         
                        posend.ShowDialog(this);

                        isdone = true;
                        AuthorizedConfirmationFrm.isconfirmedLogin = false;
                        authfrm.Dispose();
                        //Classes.Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                    }
                }
                
            }
            else
            {
                return;
            }
            display();
            if(gridView1.RowCount == 0 && gridView2.RowCount == 0)
            {
                this.Close();
            }
        }

        private void POSTransactionChecker_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void POSTransactionChecker_Load(object sender, EventArgs e)
        {

        }
    }
}