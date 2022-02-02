using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class POSMain : Form
    {
        public static string getUserTransCode = "";
        public POSMain()
        {
            InitializeComponent();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            POSStandAloneSetup.POSStandAloneSettingsBoard pcusatfsmr = new POSStandAloneSetup.POSStandAloneSettingsBoard();
            pcusatfsmr.ShowDialog(this);
        }
        void OpenPOSTransaction()
        {
            bool isUserExistToday = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' and DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='1' and UserID='" + Login.isglobalUserID + "'");
            if (!isUserExistToday)
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
                getUserTransCode = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' AND DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='1' and UserID='" + Login.isglobalUserID + "'", "AccountCode");
                openPOS();
            }
        }
        void openPOS()
        {
            bool isRetail = Database.checkifExist("Select isRetail FROM POSType WHERE isRetail=1");
            if(isRetail == true)
            {
                PointOfSale psale = new PointOfSale();
                psale.ShowDialog(this);
                
                //this.Close();
            }
            else
            {
                HotelManagement.HotelFrmRestaurant pcusatfsmr = new HotelManagement.HotelFrmRestaurant();
                pcusatfsmr.ShowDialog(this);
                //this.Close();
            }
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(PointOfSale))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //PointOfSale psale = new PointOfSale();
            //psale.Show();
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            bool isExists = Database.checkifExist("SELECT MachineUsed FROM POSZReadingTransactions WHERE MachineUsed='" + Environment.MachineName + "' and DateExecute='" + DateTime.Now.ToShortDateString() + "' ");
            bool notyetEOF = Database.checkifExist("Select isEndOfDay FROM POSEODMonitoring WHERE isEndOfDay=0 and DateExecute='" + DateTime.Now.ToShortDateString() + "'");
            //if(notyetEOF==true) //naay transaction nga wla ka end of day..bsan ang mga cashier kay nag close transaction pro ang admin or supervisor wla ka execute og end of day.
            //{
            //    XtraMessageBox.Show("The System found out that some transactions are still not yet END OF DAY Process..Please contact Admin..");
            //    return;
            //}
            if (isExists == true)//dli na mka login kay na execute na ang end of day transaction sa karon nga adlaw...so by next day na pwedi ka cash begin or login.
            {
                XtraMessageBox.Show("The System found out that END OF DAY Transaction is Already Process for today's date...You can login on the next business day/s..");
                return;
            }
            else
            {
                OpenPOSTransaction();
            }
            
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ReInventoryIn asdkk = new ReInventoryIn();
            asdkk.ShowDialog(this);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            BranchInventory brancinv = new BranchInventory();
            brancinv.ShowDialog(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            HOForms.POSTransactions postra = new HOForms.POSTransactions();
            postra.ShowDialog(this);
        }

        private void POSMain_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Text = Login.assignedBranch + " - " + Branch.getBranchName(Login.assignedBranch);
            label2.Text = Login.Fullname;
            label6.Text = HelperFunction.GetLocalIPAddress();
            label4.Text = Login.servername;
            if(Convert.ToBoolean(Login.isCashier) == true && Convert.ToBoolean(Login.isglobalAdmin)==false)
            {
                toolStripButton5.Visible = false;
                toolStripButton7.Visible = false;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == (Keys.X | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            {
                //Reading();
            }
            return functionReturnValue;
        }

        //void Reading()
        //{

        //}

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

        //        double grosssales = 0.0;

        //        details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("Z READING") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText(petsa) + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
                
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        grosssales = Convert.ToDouble(txtTotalSales.Text) + Convert.ToDouble(txtTotalCreditSales.Text);
        //        details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", HelperFunction.convertToNumericFormat(grosssales)) + Environment.NewLine; //total sales txtTotalSales.Text)
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("CASH :", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalSales.Text))) + Environment.NewLine; //total sales
        //        details += HelperFunction.PrintLeftRigthText("CREDIT :", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCreditSales.Text))) + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("RETURN :", HelperFunction.convertToNumericFormat(txtTotalVoidTransaction.Text)) + Environment.NewLine; //total void

        //        details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("GROSS SALE ", HelperFunction.convertToNumericFormat(grosssales)) + Environment.NewLine; //total sales
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

        //        details += HelperFunction.PrintLeftRigthText("BeginningCash: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtBeginningCash.Text))) + Environment.NewLine; //numitemsold
        //        details += HelperFunction.PrintLeftRigthText("Shortage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtshortage.Text))) + Environment.NewLine; //numitemsold
        //        details += HelperFunction.PrintLeftRigthText("Overage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtoverage.Text))) + Environment.NewLine + Environment.NewLine; //numitemsold

        //        details += HelperFunction.PrintLeftRigthText("Transaction #: ", Classes.POSTransactionZ.getLastTransactionNo().ToString()) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", Classes.POSTransactionZ.getTotalSoldItems().ToString()) + Environment.NewLine; //numitemsold
        //        details += HelperFunction.PrintLeftRigthText("Beginning Invoice: ", txtbeginninginvoice.Text) + Environment.NewLine; //beginvoice
        //        details += HelperFunction.PrintLeftRigthText("Last OR Number: ", txtlastornumber.Text) + Environment.NewLine; //lastornum
        //        details += HelperFunction.PrintLeftRigthText("Transaction Count: ", txttransactioncount.Text) + Environment.NewLine; //numtranscunt
        //                                                                                                                             //details += HelperFunction.PrintLeftRigthText("Average Per Transaction: ", "") + Environment.NewLine;
        //                                                                                                                             //details += HelperFunction.PrintLeftRigthText("Beg Transaction #: ", "") + Environment.NewLine;
        //                                                                                                                             //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", txtnoofreturneditem.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalReturnedTransaction.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", txtnoofcancelleditem.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalCancelledTransaction.Text))) + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("Tot ServiceFee: ", "0.00") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", txtnoofvoiditem.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalVoidTransaction.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", txtnoofdiscount.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total Discounts: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalDiscount.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("No. VAT Items: ", txtnoofvat.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Total VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtTotalTax.Text))) + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("Total Add On Amt: ", HelperFunction.convertToNumericFormat(txtTotalCharges.Text)) + Environment.NewLine + Environment.NewLine;

        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
        //        details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("VATable Exempt Sale: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatexemptsale.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("VATable Amt: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatablesales.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Net Sales of VAT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtnetsalesofvat.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("VATable Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtvatamount.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("Less: Input Vat: ", "") + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine + Environment.NewLine;

        //        details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
        //        //details += HelperFunction.createEqualLine() + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("ACC. PREV: ", HelperFunction.convertToNumericFormat(txtTotalSales.Text)) + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("CURRENT: ", HelperFunction.convertToNumericFormat(txtTotalSales.Text)) + Environment.NewLine + Environment.NewLine;
        //        //details += HelperFunction.PrintLeftRigthText("ACC. TOTAL: ", HelperFunction.convertToNumericFormat(txtTotalSales.Text)) + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("C A S H  C O U N T") + Environment.NewLine;
        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
        //        details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
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
        //        details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
        //        details += HelperFunction.PrintLeftRigthText("TOTAL     P ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtActualCashOnHand.Text))) + Environment.NewLine;
        //        details += HelperFunction.PrintRightToLeft(" ", "=========") + Environment.NewLine;
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

        //        details += HelperFunction.PrintCenterText("L OR.#:" + txtlastornumber.Text) + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("TRAN.#:" + txttransno.Text) + Environment.NewLine + Environment.NewLine;

        //        details += HelperFunction.createAsteriskLine() + Environment.NewLine;
        //        details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

        //        details += HelperFunction.LastPagePaper();
        //        if (!Directory.Exists(filepath))
        //        {
        //            Directory.CreateDirectory(filepath);
        //        }
        //        transno = txtlasttranno.Text + ".txt";
        //        string filetoprint = filepath + transno;
        //        string mark = filepath + transno;
        //        StreamWriter writer = new StreamWriter(filepath + transno);
        //        writer.Write(details);
        //        writer.Close();
        //        Printing printfile = new Printing();
        //        printfile.printTextFile(filetoprint);
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}

    }
}
