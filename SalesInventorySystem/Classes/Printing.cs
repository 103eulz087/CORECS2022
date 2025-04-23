using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    class Printing
    {
        public string txtorder, txtorder2;

        //string tradename = Database.getSingleQuery("CompanyProfile", "BranchCode='"+Login.assignedBranch+"'", "CompanyName");
        //string compaddress1 = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "'", "Address1");
        //string compaddress2 = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "Address2");
        //string comptinno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "TinNo");
        //string compminno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "MinNo");
        //string compbirpermitno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "'", "BirPermitNo");
        //string compserialno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "SerialNo");


        public void printReceiptAtik()
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Temp\\";

            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += "-";
         
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\temp.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
           
        }

        public void printOrders(string refno,string waiterid,string tableno,string location, DataGridView gridview)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\Restaurant\\Orders\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            details += HelperFunction.PrintCenterText(location) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Table #:" + tableno,"Waiter:"+waiterid) + Environment.NewLine;
            details += Classes.ReceiptSetup.doTitle("ORDERS");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Qty"].Value.ToString()) + Environment.NewLine;
               
            }
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + refno + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile2(filetoprint);
        }


        public void printReceiptBillingWithDiscount(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview, string tableno, bool isDiscount, string disctype,string netdue)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Restaurant\\Billing\\";
            string filepath1 = "C:\\POSTransaction\\Restaurant\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            //string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += HelperFunction.PrintLeftText("Table #:" + tableno) + Environment.NewLine;
            details += Classes.ReceiptSetup.doTitle("TRANSACTION BILL");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                //if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                //{
                //    addV = "V";
                //}
                //else
                //{
                //    addV = "";
                //}
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Amount"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value;// + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                //details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE", total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            if (isDiscount == true)
            {
                var rowz = Database.getMultipleQuery($"SELECT DiscIDNo,DiscName,DiscountAmount FROM dbo.SalesDiscount WHERE OrderNo='{ordercode}' and isErrorCorrect=0", "DiscIDNo,DiscName,DiscountAmount");
                string DiscIDNo, DiscName, DiscountAmount;
                DiscIDNo = rowz["DiscIDNo"].ToString();
                DiscName = rowz["DiscName"].ToString();
                DiscountAmount = rowz["DiscountAmount"].ToString();
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + DiscIDNo) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + DiscName) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(DiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(netdue))) + Environment.NewLine + Environment.NewLine;

                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + DiscIDNo) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + DiscName) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(DiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(netdue))) + Environment.NewLine + Environment.NewLine;
                }
            }

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine;

            //details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);

            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = "\\LastTran.txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
            //printTextFile(filetoprint);
        }

        //IS USED
        public void printReceiptBilling(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview,string tableno)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Restaurant\\Billing\\";
            string filepath1 = "C:\\POSTransaction\\Restaurant\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            //string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += HelperFunction.PrintLeftText("Table #:"+tableno) + Environment.NewLine;
            details += Classes.ReceiptSetup.doTitle("TRANSACTION BILL");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                //if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                //{
                //    addV = "V";
                //}
                //else
                //{
                //    addV = "";
                //}
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;

               

                //details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Amount"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value;// + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["Amount"].Value;// + addV;
                double totamnt = 0.0;
                totamnt = Convert.ToDouble(b);
                details += HelperFunction.PrintLeftRigthText(a, HelperFunction.convertToNumericFormat(totamnt)) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE", total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine;

            //details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);

            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = "\\LastTran.txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
            //printTextFile(filetoprint);
        }

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION NO ONE TIME DISCOUNT 
        public void printReceipt(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype
                                , bool iszerorated = false)


        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\"+DateTime.Now.ToString("yyyyMMdd")+"\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, GlobalVariables.computerName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
           
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string mark = gridview.Rows[i].Cells["isVat"].Value.ToString();
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if(Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance=Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "("+gridview.Rows[i].Cells["Discount"].Value.ToString()+")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if(Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                var rox = Database.getMultipleQuery("POSCreditCardTransactions", $"ReferenceNo='{ordercode}' ", "CCName,CCNumber,CCType,CCBank,CCPaymentReferenceNo");
                cardno = rox["CCNumber"].ToString();
                cardtype = rox["CCType"].ToString();
                cardrefno = rox["CCPaymentReferenceNo"].ToString();
                string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            if (paytype == "Merchant")
            {
                string ReferenceNo = "", MerchantName = "", VoucherCode = "";
                var rox = Database.getMultipleQuery("POSMerchantTransactions", $"OrderNo='{ordercode}' ", "ReferenceNo,MerchantName,VoucherCode,Amount");
                ReferenceNo = rox["ReferenceNo"].ToString();
                MerchantName = rox["MerchantName"].ToString();
                VoucherCode = rox["VoucherCode"].ToString();
                //string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Merchant "+ MerchantName) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("VoucherCode: " + VoucherCode) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + ReferenceNo) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath+"\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            details += HelperFunction.LastPagePaper();
            
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filetoprint);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();


            printTextFile(filetoprint);
        }

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION NO ONE TIME DISCOUNT 
        public void printReceiptResto(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype
                                , bool iszerorated = false)


        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, GlobalVariables.computerName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string mark = gridview.Rows[i].Cells["isVat"].Value.ToString();
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                var rox = Database.getMultipleQuery("POSCreditCardTransactions", $"ReferenceNo='{ordercode}' ", "CCName,CCNumber,CCType,CCBank,CCPaymentReferenceNo");
                cardno = rox["CCNumber"].ToString();
                cardtype = rox["CCType"].ToString();
                cardrefno = rox["CCPaymentReferenceNo"].ToString();
                string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            if (paytype == "Merchant")
            {
                string ReferenceNo = "", MerchantName = "", VoucherCode = "";
                var rox = Database.getMultipleQuery("POSMerchantTransactions", $"OrderNo='{ordercode}' ", "ReferenceNo,MerchantName,VoucherCode,Amount");
                ReferenceNo = rox["ReferenceNo"].ToString();
                MerchantName = rox["MerchantName"].ToString();
                VoucherCode = rox["VoucherCode"].ToString();
                //string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Merchant " + MerchantName) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("VoucherCode: " + VoucherCode) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + ReferenceNo) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }

            txtorder =  ordercode + ".txt";
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + ordercode);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + ordercode;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();


            printTextFile(filetoprint);
        }

        public void printReceiptRestoOneLove(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype
                                , bool iszerorated = false)


        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string mark = gridview.Rows[i].Cells["isVat"].Value.ToString();
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                var rox = Database.getMultipleQuery("POSCreditCardTransactions", $"ReferenceNo='{ordercode}' ", "CCName,CCNumber,CCType,CCBank,CCPaymentReferenceNo");
                cardno = rox["CCNumber"].ToString();
                cardtype = rox["CCType"].ToString();
                cardrefno = rox["CCPaymentReferenceNo"].ToString();
                string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            if (paytype == "Merchant")
            {
                string ReferenceNo = "", MerchantName = "", VoucherCode = "";
                var rox = Database.getMultipleQuery("POSMerchantTransactions", $"OrderNo='{ordercode}' ", "ReferenceNo,MerchantName,VoucherCode,Amount");
                ReferenceNo = rox["ReferenceNo"].ToString();
                MerchantName = rox["MerchantName"].ToString();
                VoucherCode = rox["VoucherCode"].ToString();
                //string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Merchant " + MerchantName) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("VoucherCode: " + VoucherCode) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + ReferenceNo) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filetoprint);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + ordercode;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();
            for(int xxx=0;xxx<=3;xxx++)
            {
                printTextFile(filetoprint);
            }
        }
        //THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceipt(string transcode
                                , string ordercode

                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string disctype
                                , string footerlabel
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch,Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,name,address,tin,bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            var rows = Database.getMultipleQuery("SalesDiscount", $"OrderNo='{ordercode}' " +
                $"AND isErrorCorrect=0 " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}'", "DiscRemarks,DiscountPercentage,DiscountAmount");

            string discremarks = rows["DiscRemarks"].ToString();
            string discountpercentage = rows["DiscountPercentage"].ToString();
            string discountamount = rows["DiscountAmount"].ToString();
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
               
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if(disctype=="REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if(disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }
                   
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc=0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);
                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                   
                } 
                //else if (POS.POSConfirmPayment.isPwdDiscount == true)
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                //else if (POS.POSConfirmPayment.isOthersDiscount == true)
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12,2);
                    netofvat = Math.Round(totalvatitems / 1.12,2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc,2);
                    addvat = Math.Round(netofscdisc * .12,2);
                    totaltotal = Math.Round(netofscdisc + addvat,2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;// vat) + Environment.NewLine;
            // details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            if(paytype=="Credit")
            {
                string cardno="", cardtype="", cardrefno="";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-"+ cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: "+ cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: "+ cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }


            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if(isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }
            if (isDiscount)
            {
                txtorder = PointOfSale.refno + "-" + footerlabel + ".txt";
            }
            else
            {
                txtorder = PointOfSale.refno+ ".txt";
            }
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
        }

        ///--------------------------------------------------------------------------------
        /////THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptResto(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string netofvatindiscitems
                                , string netofvatindinonscitems
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string disctype
                                , string footerlabel
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            var rows = Database.getMultipleQuery("SalesDiscount", $"OrderNo='{ordercode}' " +
                $"AND isErrorCorrect=0 " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}'", "DiscRemarks,DiscountPercentage,DiscountAmount");

            string discremarks = rows["DiscRemarks"].ToString();
            string discountpercentage = rows["DiscountPercentage"].ToString();
            string discountamount = rows["DiscountAmount"].ToString();
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);


                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);
                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
                //else if (POS.POSConfirmPayment.isPwdDiscount == true)
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                //else if (POS.POSConfirmPayment.isOthersDiscount == true)
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;// vat) + Environment.NewLine;
            // details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }


            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }
            if (isDiscount)
            {
                txtorder = ordercode + "-" + footerlabel + ".txt";
            }
            else
            {
                txtorder = ordercode + ".txt";
            }
            string lastranfile = "LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
        }
        ///

        ///--------------------------------------------------------------------------------
        /////THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptRestoOneLove(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string netofvatindiscitems
                                , string netofvatindinonscitems
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string disctype
                                , string footerlabel
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;

            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            var rows = Database.getMultipleQuery("SalesDiscount", $"OrderNo='{ordercode}' " +
                $"AND isErrorCorrect=0 " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}'", "DiscRemarks,DiscountPercentage,DiscountAmount");

            string discremarks = rows["DiscRemarks"].ToString();
            string discountpercentage = rows["DiscountPercentage"].ToString();
            string discountamount = rows["DiscountAmount"].ToString();
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);


                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);
                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
                //else if (POS.POSConfirmPayment.isPwdDiscount == true)
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                //else if (POS.POSConfirmPayment.isOthersDiscount == true)
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }


            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                //details += HelperFunction.PrintCenterText(footerlabel);
            }
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }
            if (isDiscount)
            {
                txtorder = ordercode + "-" + footerlabel + ".txt";
            }
            else
            {
                txtorder = ordercode + ".txt";
            }
            string lastranfile = "LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
        }
        ///


        //THIS TEMPLATE HAS BEEN ALWAYS USED FOR REPRINT
        public void ReprintReceipt(string transcode
                                    , string ordercode
                                    , string total
                                    , string peritemdiscount
                                    , string netofvatindiscitems
                                    , string netofvatindinonscitems
                                    , string vatablesale
                                    , string vatexemptsale
                                    , string vat
                                    , string cash
                                    , string change
                                    , DataGridView gridview
                                    , bool isDiscount
                                    , string disctype
                                    , string footerlabel
                                    , string name
                                    , string address
                                    , string tin
                                    , string bussstyle
                                    , string paytype 
                                    , bool iszerorated=false
                                   )
        {

            String details = "";
            string filepath1 = "C:\\POSTransaction\\CopyForReprint\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doTitle("REPRINT");
            //details += HelperFunction.PrintLeftText("REPRINT#:*") + Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,name,address,tin,bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = "0";
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
             
                if (isDiscount)
                {
                    discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
                    double discPercent = Convert.ToDouble(discountpercentage) * 100;
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
           
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if(isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();


            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = "\\" + PointOfSale.refno + ".txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
        }

        /// ////////////////////////////////////////////////////////////////
        //THIS TEMPLATE HAS BEEN ALWAYS USED FOR REPRINT
        public void ReprintReceiptResto(string transcode
                                    , string ordercode
                                    , string total
                                    , string peritemdiscount
                                    , string netofvatindiscitems
                                    , string netofvatindinonscitems
                                    , string vatablesale
                                    , string vatexemptsale
                                    , string vat
                                    , string cash
                                    , string change
                                    , DataGridView gridview
                                    , bool isDiscount
                                    , string disctype
                                    , string footerlabel
                                    , string name
                                    , string address
                                    , string tin
                                    , string bussstyle
                                    , string paytype
                                    , bool iszerorated = false
                                   )
        {

            String details = "";
            string filepath1 = "C:\\POSTransaction\\CopyForReprint\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doTitle("REPRINT");
            //details += HelperFunction.PrintLeftText("REPRINT#:*") + Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = "0";
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }

                if (isDiscount)
                {
                    discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
                    double discPercent = Convert.ToDouble(discountpercentage) * 100;
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPaymentResto.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();


            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = ordercode + ".txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
        }

        public void ReprintReceiptRestoOneLove(string transcode
                                    , string ordercode
                                    , string total
                                    , string peritemdiscount
                                    , string netofvatindiscitems
                                    , string netofvatindinonscitems
                                    , string vatablesale
                                    , string vatexemptsale
                                    , string vat
                                    , string cash
                                    , string change
                                    , DataGridView gridview
                                    , bool isDiscount
                                    , string disctype
                                    , string footerlabel
                                    , string name
                                    , string address
                                    , string tin
                                    , string bussstyle
                                    , string paytype
                                    , bool iszerorated = false
                                   )
        {

            String details = "";
            string filepath1 = "C:\\POSTransaction\\CopyForReprint\\"; 
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            string discountpercentage = "0";
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }

                if (isDiscount)
                {
                    discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
                    double discPercent = Convert.ToDouble(discountpercentage) * 100;
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPaymentResto.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();


            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = ordercode + ".txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
        }
        //////////////////////////////////////////////////////////////////////

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION
        public void printReceiptConsolidated(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype
                                            , bool iszerorated=false)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\"+ DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,name,address,tin,bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
          
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = "\\" + cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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

        //----------------------------------------------------------------------------------------------
        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION
        public void printReceiptConsolidatedResto(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype
                                            , bool iszerorated = false)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;


            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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

        public void printReceiptConsolidatedRestoOneLove(string cashiertranscode
                                           , string transcode
                                           , string ordercode
                                           , string total
                                           , string peritemdiscount
                                           , string vatablesale
                                           , string vatexemptsale
                                           , string vat
                                           , string cash
                                           , string change
                                           , DataGridView gridview
                                           , bool isDiscount
                                           , string name
                                           , string address
                                           , string tin
                                           , string bussstyle
                                           , string paytype
                                           , bool iszerorated = false)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;


            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            // details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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
        //---------------------------------------------------------------------------------------------------------

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION
        public void printReceiptConsolidatedXX()
        {
            double totalPerItemDiscount = 0.0, total = 0.0;
            String details = "";
            string filepath = "C:\\POSTransactionX\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string paytype = "",amounttender="",amountchange="",TotalVatableSalesSum="",TotalVatSaleSum="",TotalVatExemptSum="", CashierTransNo="";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("SELECT TOP 3 * FROM BatchSalesDetails", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
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
            //if (paytype == "Credit")
            //{
            //    string cardno = "", cardtype = "", cardrefno = "";
            //    details += HelperFunction.createDottedLine() + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
            //    details += HelperFunction.createDottedLine() + Environment.NewLine;
            //}

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + CashierTransNo + "_E-JOURNAL.txt";
            txtorder2 = "\\" + CashierTransNo + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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
        //Additional Overriding method
        //THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptConsolidated(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string netofvatindiscitems
                                            , string netofvatindinonscitems
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string disctype
                                            , string footerlabel
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            ,string paytype)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///netofvatafteronetimedisc

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales:", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount:", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES:", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES:", "0.00") + Environment.NewLine + Environment.NewLine;
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }
           
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
           
            txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = "\\" + cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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
        ///
        //Additional Overriding method
        //THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptConsolidatedResto(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string netofvatindiscitems
                                            , string netofvatindinonscitems
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string disctype
                                            , string footerlabel
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///netofvatafteronetimedisc

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales:", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount:", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES:", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES:", "0.00") + Environment.NewLine + Environment.NewLine;
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);

            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder =  cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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


        public void printReceiptConsolidatedRestoOneLove(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string netofvatindiscitems
                                            , string netofvatindinonscitems
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string disctype
                                            , string footerlabel
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;
            string discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///netofvatafteronetimedisc

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;


            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);

            if (isDiscount)
            {
                //details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
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


        ///
        // IS USED VOID SELECTED ITEM BUTTON katong mo select ka sa checkbox //CHANGE TO RETURNED
        public void printReturnSelectedItem(string returntranscode,string transcode, string ordercode, DataGridView gridview)
        {
            bool isdiscounted = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + ordercode + "' " +
                "AND isErrorCorrect=0 " +
                "AND BranchCode='"+Login.assignedBranch+"' " +
                "AND MachineUsed='"+Environment.MachineName+"'");
            string discidno = "", discname = "", discremarks = "", disctype = "", discamount = "", discvatadj = "", discPercentage = "";
            double discpercentageamount = 0.0;
            
            if (isdiscounted==true)
            {
                var row = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + ordercode + "' " +
                    "AND isErrorCorrect=0 and BranchCode='" + Login.assignedBranch + "'", "DiscountType,DiscIDNo,DiscName,DiscRemarks,DiscountAmount,VatAdjustment,DiscountPercentage");
                discidno = row["DiscIDNo"].ToString();
                discname = row["DiscName"].ToString();
                discremarks = row["DiscRemarks"].ToString();
                disctype = row["DiscountType"].ToString();
                discamount = row["DiscountAmount"].ToString();
                discvatadj = row["VatAdjustment"].ToString();
                discPercentage = row["DiscountPercentage"].ToString();
                if (discPercentage == "") { discPercentage = "0"; }

                discpercentageamount = Convert.ToDouble(discPercentage) * 100;
            }
            //string disctype = Database.getSingleQuery("SalesDiscount", "OrderNo='" + ordercode + "'", "DiscountType");

            String details = "";
            string filepathConso = "C:\\POSTransaction\\ReturnedSalesConso\\";
            string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doTitle("RETURNED TRANSACTION");
            details += HelperFunction.PrintLeftText("Return Transaction No.: " + returntranscode)+Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,"Ref");
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;
            string isvat = "";

            double vatablesale = 0.0;
            double vatableWithSCDiscount = 0.0, vatableWithNoSCDiscount=0.0, vatExWithSCDiscount=0.0, vatExWithNoSCDiscount=0.0;
            double discountAmount=0.0,totalDiscountAmountSC = 0.0;
            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, addvat = 0.0, totaltotal = 0.0, totalAmount = 0.0,_totalvatablesales= 0.0, _totalvatexsales=0.0;
            string addD = "";
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string a = "  - " + gridview.Rows[i].Cells["QtySold"].Value + " @ " + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = "-" + gridview.Rows[i].Cells["TotalAmount"].Value.ToString();
                string c = gridview.Rows[i].Cells["isVat"].Value.ToString();

                totalAmount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());

                if (Convert.ToBoolean(c) == true)
                {
                    isvat = "V";
                    _totalvatablesales += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                }
                else
                {
                    isvat = "";
                    _totalvatexsales += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                }

                //--------------------------------------
                //DISPLAY A,B AND C
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(a, b + isvat) + Environment.NewLine;
                //--------------------------------------
                if (isdiscounted) //there is a onetime discount either SC,PWD AND REGULAR
                {
                    bool isSCorPWDDiscountedVat = false, isSCorPWDDiscountedNonVat = false;
                    //---------------VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                    isSCorPWDDiscountedVat = Database.checkifExist("SELECT TOP(1) ProductCode " +
                                                               "FROM dbo.BatchSalesDetails " +
                                                               "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                               "AND Description='" + gridview.Rows[i].Cells["Description"].Value.ToString() + "' " +
                                                               "AND ReferenceNo='" + ordercode + "' " +
                                                               "AND DiscountTotal <= 0 " +
                                                               "AND isVat = 1 " +
                                                               "AND ProductCode in (SELECT ProductCode " +
                                                                                       "FROM dbo.Products " +
                                                                                       "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                                       "AND isDiscount=1)");
                    //---------------NON VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                    isSCorPWDDiscountedNonVat = Database.checkifExist("SELECT TOP(1) ProductCode " +
                                                              "FROM dbo.BatchSalesDetails " +
                                                              "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                              "AND Description='" + gridview.Rows[i].Cells["Description"].Value.ToString() + "' " +
                                                              "AND ReferenceNo='" + ordercode + "' " +
                                                              "AND DiscountTotal <= 0 " +
                                                              "AND isVat = 0 " +
                                                              "AND ProductCode in (SELECT ProductCode " +
                                                                                      "FROM dbo.Products " +
                                                                                      "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                                      "AND isDiscount=1)");
                    //##############################################################
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                        discountAmount += (Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString()) / 1.12) * Convert.ToDouble(discPercentage);
                    }
                    else
                    {
                        //---------------VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                        /*##*/
                        if (isSCorPWDDiscountedVat == true && Convert.ToBoolean(c) == true)
                        {
                            vatableWithSCDiscount = Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                            details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                            discountAmount += (vatableWithSCDiscount / 1.12) * Convert.ToDouble(discPercentage);
                        }
                        else if (isSCorPWDDiscountedVat == false && Convert.ToBoolean(c) == true)
                        {
                            vatableWithNoSCDiscount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                        }
                        //---------------VAT EXEMPT PRODUCT WITH SENIOR DISCOUNT ITEM
                        /*##*/
                        if (isSCorPWDDiscountedNonVat == true && Convert.ToBoolean(c) == false)
                        {
                            vatExWithSCDiscount = Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                            details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                            discountAmount += (vatExWithSCDiscount) * Convert.ToDouble(discPercentage);

                        }
                        else if (isSCorPWDDiscountedNonVat == false && Convert.ToBoolean(c) == false)
                        {
                            vatExWithNoSCDiscount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                        }
                    }
                    totalDiscountAmountSC = Math.Round(discountAmount,2);
                }
                else
                {
                    vatableWithNoSCDiscount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                }

                //--------------------------------------IF TRUE MEANING THERE IS PER ITEM DISCOUNT
                if (Convert.ToDouble(gridview.Rows[i].Cells["DiscountTotal"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["DiscountTotal"].Value.ToString() + ")") + Environment.NewLine;
                }
                //--------------------------------------

            }//end of loop
            //--------------------------------------
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", HelperFunction.convertToNumericFormat(totalAmount*-1)) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            //--------------------------------------
            double totalvatableSales = 0.0;
            double totalVatInputSale = 0.0;
            double totalVatExemptSale = 0.0;

            if (isdiscounted == true)
            {
                string id = "";
                if (disctype == "SENIOR") { id = "OSCA SC/ID: "; }
                else if (disctype == "PWD") { id = "PWD ID: "; }
                else if (disctype == "REGULAR") { id = " "; }

                if (disctype == "SENIOR" || disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText(disctype + " DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText(id + discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(totalDiscountAmountSC * -1)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(totalDiscountAmountSC * -1)) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                }
                
                lessvat = Math.Round((vatableWithSCDiscount / 1.12) * 0.12, 2);
                netofvat = Math.Round((vatableWithSCDiscount / 1.12), 2);
                lessscdisc = Math.Round(netofvat * Convert.ToDouble(discpercentageamount), 2); //must change the percentage
                netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                addvat = Math.Round(netofscdisc * .12, 2);
                totaltotal = Math.Round(netofscdisc + addvat, 2);

                details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal * -1)) + Environment.NewLine;

                details += HelperFunction.createDottedLine() + Environment.NewLine;

                vatablesale = vatableWithNoSCDiscount / 1.12;

                totalvatableSales = Math.Round(netofscdisc + vatablesale, 2);
                totalVatInputSale = Math.Round(totalvatableSales * 0.12, 2);
                totalVatExemptSale = Math.Round((vatExWithSCDiscount + vatExWithNoSCDiscount), 2);
            }
            else
            {
                vatablesale = _totalvatablesales / 1.12;
                totalvatableSales = Math.Round(vatablesale, 2);
                totalVatInputSale = Math.Round(totalvatableSales * 0.12, 2);
                totalVatExemptSale = Math.Round(_totalvatexsales, 2);
            }
         
            details += Environment.NewLine;
           

            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales * -1)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale *-1)) + Environment.NewLine;// vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", HelperFunction.convertToNumericFormat(totalVatExemptSale *-1)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
 
            //----------------------------------------------------------------------------------------------------
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch,"");

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTERRETURN.txt");
            //string str1 = Classes.Utilities.readFile(path); 
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);


            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if (!Directory.Exists(filepathConso))
            {
                Directory.CreateDirectory(filepathConso);
            }
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + ordercode + ".txt";
            //conso
            string filetoprintConso = filepathConso + txtorder;
            StreamWriter writerConso = new StreamWriter(filepathConso + txtorder);
            writerConso.Write(details);
            writerConso.Close();
            //per folder per date
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        //IS USED void selected item katong mo right click ka
        public void printVoidSales(string transcode, string ordercode, string vatablesale, string vatexemptsale, string vat, DataGridView gridview, string qtysold, string sellingprice, string totalamount, string desc)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\VoidSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += Classes.ReceiptSetup.doTitle("VOID TRANSACTION");

            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;

            string a = sellingprice +  " @ " + totalamount;
            string b = totalamount;

            details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText(desc, b) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VOID") + Environment.NewLine;

            details += Environment.NewLine;

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CASH");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a2 = totalChange.Text;

            //details += HelperFunction.PrintRightToMiddle(totalLabel.Text, total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("O.R No.", orderr) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Login.Fullname) + Environment.NewLine;

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //StreamWriter writer = new StreamWriter(filepath + @"\rea.txt");
            txtorder = "\\" + ordercode + ".txt";
            //txtorder = "\\10009.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }


        //IS USED VOID ALL TRANSACTION NA BUTTON
        public void printVoidAllSales(string returntranscode, string transcode, string ordercode, string vatablesale, string vatexemptsale, string vat, DataGridView gridview)
        {

            String details = "";
            //string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

            string filepathConso = "C:\\POSTransaction\\ReturnedSalesConso\\";
            string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doTitle("RETURNED ALL TRANSACTION");
            details += HelperFunction.PrintLeftText("Return Transaction No.: " + returntranscode);
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, "Ref");
            details += HelperFunction.createDottedLine() + Environment.NewLine;

        
            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                
                string a = gridview.Rows[i].Cells["QtySold"].Value + " @ " + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();

                details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Description"].Value.ToString(), b) + Environment.NewLine;
               
            }
            
            details += Environment.NewLine;

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CASH");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a2 = totalChange.Text;
            
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------


            details += HelperFunction.PrintLeftRigthText("VATable Sales", "-" + vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", "-"+ vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "-"+ vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch, "");
            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if(!Directory.Exists(filepathConso))
            {
                Directory.CreateDirectory(filepathConso);
            }
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + ordercode + ".txt";
            //conso
            string filetoprintConso = filepathConso + txtorder;
            StreamWriter writerConso = new StreamWriter(filepathConso + txtorder);
            writerConso.Write(details);
            writerConso.Close();
            //per folder per date
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }
        //IS USED PRINT AUTO FULL TRANSACTION
        public void printXReadReportFullTransactionSalesDevEx(string totalvat, string totalamount, GridView gridview, string date1, string date2, string brcode)
        {

            int beginOR = 0;
            beginOR = Database.getBeginningID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            int lastOR = 0;
            lastOR = Database.getLastID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string cashiername = "-----------------";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            string petsa = DateTime.Now.ToShortDateString();
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT SUMMARY (Z)") + Environment.NewLine;
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("V896 CASHIER : " + cashiername) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            string oras = DateTime.Now.ToShortTimeString();

            string strLabel = "";
            double amount = 0.0, newtotalamount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string vattype = gridview.GetRowCellValue(i, "isVat").ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12, 2);
                    //vatableamount = amount / 1.12;
                    strLabel = "Vatable Sale";
                    newtotalamount += amount;
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                    strLabel = "Vat Exempt Sale";
                    newtotalamount += amount;
                }

                details += HelperFunction.PrintLeftRigthText(strLabel, HelperFunction.convertToNumericFormat(amount)) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", HelperFunction.convertToNumericFormat(newtotalamount)) + Environment.NewLine;
            double totalvatnew = Convert.ToDouble(totalvat) / 1.12;
            string totalvatnew1 = HelperFunction.convertToNumericFormat(totalvatnew);

            double salestotals = 0.0;
            salestotals = newtotalamount + Convert.ToDouble(totalvatnew1);

            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            string totalcashamount1 = HelperFunction.convertToNumericFormat(salestotals);
            details += HelperFunction.PrintLeftRigthText("GRAND TOTAL", totalcashamount1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
           // details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("B OR.#:" + beginOR.ToString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("L OR.#:" + lastOR.ToString()) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();
            
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            string filename = "FullTransactionSales_" + date1.Replace("/", "-") + ".txt";
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + filename;
            StreamWriter writer = new StreamWriter(filepath + filename);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }
        //IS USED PRINT AUTO FULL TRANSACTION
        public void printXReadReportFullTransactionSalesDevEx(string totalvat, string totalamount, GridView gridview, string date1, string date2, string brcode,string posname)
        {

            int beginOR = 0;
            beginOR = Database.getBeginningID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "' and MachineUsed='" + posname +"'", "ReferenceNo");
            int lastOR = 0;
            lastOR = Database.getLastID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "' and MachineUsed='" + posname + "'", "ReferenceNo");
            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string cashiername = "-----------------";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode, posname);
            string petsa = DateTime.Now.ToShortDateString();
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT SUMMARY (Z)") + Environment.NewLine;
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("V896 CASHIER : " + cashiername) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            string oras = DateTime.Now.ToShortTimeString();

            string strLabel = "";
            double amount = 0.0, newtotalamount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string vattype = gridview.GetRowCellValue(i, "isVat").ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12, 2);
                    //vatableamount = amount / 1.12;
                    strLabel = "Vatable Sale";
                    newtotalamount += amount;
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                    strLabel = "Vat Exempt Sale";
                    newtotalamount += amount;
                }

                details += HelperFunction.PrintLeftRigthText(strLabel, HelperFunction.convertToNumericFormat(amount)) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", HelperFunction.convertToNumericFormat(newtotalamount)) + Environment.NewLine;
            double totalvatnew = Convert.ToDouble(totalvat) / 1.12;
            string totalvatnew1 = HelperFunction.convertToNumericFormat(totalvatnew);

            double salestotals = 0.0;
            salestotals = newtotalamount + Convert.ToDouble(totalvatnew1);

            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            string totalcashamount1 = HelperFunction.numericFormat(salestotals);
            details += HelperFunction.PrintLeftRigthText("GRAND TOTAL", totalcashamount1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
            //details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("B OR.#:" + beginOR.ToString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("L OR.#:" + lastOR.ToString()) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            string filename = "FullTransactionSales_" + date1.Replace("/", "-") + ".txt";
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + filename;
            StreamWriter writer = new StreamWriter(filepath + filename);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }

        public void printXReadReportFullTransactionSalesDevExZReset(string totalvat, string totalamount, GridView gridview, string date1, string date2, string brcode)
        {

            int beginOR = 0;
            beginOR = Database.getBeginningID("BatchSalesSummary2", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            int lastOR = 0;
            lastOR = Database.getLastID("BatchSalesSummary2", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            //beginOR = Database.getSingleQuery("SELECT * FROM BatchSalesDetails")
            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string cashiername = "-----------------";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            //details += HelperFunction.PrintLeftText("X-Read Report ") + date1 + " -> " + date2 + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT SUMMARY (Z)") + Environment.NewLine;
            //details += HelperFunction.PrintLeftText(petsa) + Environment.NewLine;
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("V896 CASHIER : " + cashiername) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("[ PAYABLE TO BIR ]") + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine;

            //string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            //string trans = "Trans #: " + transcode;
            //string orderr = "Order #: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
            //string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();

            string strLabel = "";
            double amount = 0.0, newtotalamount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string vattype = gridview.GetRowCellValue(i, "isVat").ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12, 2);
                    //vatableamount = amount / 1.12;
                    strLabel = "Vatable Sale";
                    newtotalamount += amount;
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                    strLabel = "Vat Exempt Sale";
                    newtotalamount += amount;
                }

                details += HelperFunction.PrintLeftRigthText(strLabel, amount.ToString()) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            //   details += HelperFunction.PrinttoRight("1000.00");

            double totalcashamount = 0.0;
            //details += HelperFunction.PrintLeftRigthText("TOTAL", totalamount) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL", HelperFunction.numericFormat(newtotalamount)) + Environment.NewLine;
            //  details += HelperFunction.createDottedLine() + Environment.NewLine;
            double totalvatnew = Convert.ToDouble(totalvat) / 1.12;
            string totalvatnew1 = HelperFunction.numericFormat(totalvatnew);

            double salestotals = 0.0;
            salestotals = newtotalamount + Convert.ToDouble(totalvatnew1);

            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvatnew1) + Environment.NewLine;
            //   details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvatnew1) + Environment.NewLine;
            //   details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            // string totalcashamount1 = HelperFunction.numericFormat(totalcashamount);
            string totalcashamount1 = HelperFunction.numericFormat(salestotals);
            details += HelperFunction.PrintLeftRigthText("v896", totalcashamount1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
           // details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("B OR.#:" + beginOR.ToString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("L OR.#:" + lastOR.ToString()) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();
            //----------------------------------------------------------------------------------------------------
            //    If (Not System.IO.Directory.Exists(salefilelocation)) Then
            //    System.IO.Directory.CreateDirectory(salefilelocation)
            //End If

            //If System.IO.File.Exists(salefilelocation & "\" & batchcode & ".txt") = True Then
            //    System.IO.File.Delete(salefilelocation & "\" & batchcode & ".txt")
            //End If

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            string filename = "FullTransactionSales_" + date1.Replace("/", "-") + ".txt";
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + filename;
            StreamWriter writer = new StreamWriter(filepath + filename);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }
        //IS USED PRINT GROUP CATEGORY
        public void printXReadReportGroupSalesDevEx(string totalkilos, string totalamount, GridView gridview, string date1, string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\GroupSales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            details += Classes.ReceiptSetup.doTitle(POS.POSXreadReport.reportTitle + " - " + "GROUP CATEGORY SALES");
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //string cashiername = (isPerCashier == true) ? "positive" : "negative";

            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            details += Classes.ReceiptSetup.doTitle("GROUP CATEGORY SALES");
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("Report Type: ") + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            //  string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //details += HelperFunction.PrintLeftText(gridview.GetRowCellValue(i, "Description").ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftText(gridview.GetRowCellValue(i, "Description").ToString()) + Environment.NewLine;
                string a = "  - " + gridview.GetRowCellValue(i, "TotalKilos").ToString() + " ";
                string b = " " + gridview.GetRowCellValue(i, "TotalAmount").ToString();
                double b1 = Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                details += HelperFunction.PrintLeftRigthText(a, HelperFunction.convertToNumericFormat(b1)) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("1000.00");
            Label totalLabel = new Label();
            totalLabel.Text = HelperFunction.PrintLeftText("TOTAL");
            totalLabel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            details += HelperFunction.PrintLeftText(totalLabel.Text) + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Total Qty: "+totalkilos) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Total Amount: " + HelperFunction.convertToNumericFormat(Convert.ToDouble(totalamount))) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void printFinancialReport(string transcode,string ordercode,string total,string totalvoid,string vatablesale,string vatexemptsale,string vat,string cash,string change,DataGridView gridview)
        {
          
            String details = "";
            string filepath = "C:\\POSTransaction\\FinancialReport\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
           
            //string terminalno = "122";
            details += HelperFunction.PrintCenterText("YOUR TRADENAME HERE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("YOUR NAME HERE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("YOUR VAT REGISTERED") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TIN NO: 000-000-000-001") + Environment.NewLine + Environment.NewLine;
           
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;
            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

            details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASHIER : ", Login.Fullname+" Terminal No#: 000") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT (Z)") + Environment.NewLine;
            details += HelperFunction.PrintLeftText(petsa) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftText("V896 CASHIER : "+Login.Fullname) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", total) + Environment.NewLine; //total sales
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASH :", total) + Environment.NewLine; //total sales
            details += HelperFunction.PrintLeftRigthText("CREDIT :", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("RETURN :", totalvoid) + Environment.NewLine; //total void
            details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("GROSS SALE ", total) + Environment.NewLine; //total sales
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", total) + Environment.NewLine; //numitemsold
            details += HelperFunction.PrintLeftRigthText("Beginning Invoice: ", total) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Last OR Number: ", total) + Environment.NewLine; //lastornum
            details += HelperFunction.PrintLeftRigthText("Transaction Count: ", total) + Environment.NewLine; //numtranscunt
            details += HelperFunction.PrintLeftRigthText("Average Per Transaction: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Beg Transaction #: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Refunds: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Refunds: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Tot ServiceFee: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Discounts: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of 12% VAT: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total 12% VAT: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Add On Amt: ", total) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Vatable Amt: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Net Sales of Vat: ", total) + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Vat Amt: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Less: Input Vat: ", total) + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ACC. PREV: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CURRENT: ", total) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ACC. TOTAL: ", total) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H  C O U N T") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASHIER: "+Login.Fullname) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("[ QTY ] X [ DENOMIN ] = [ AMT ]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [    1000 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     500 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     200 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     100 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [      50 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [      20 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [      10 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [       5 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [       1 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     .25 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL     P ", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "=========") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
           // details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("L OR.#:"+"last or") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TRAN.#:" + "last or") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

            details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + PointOfSale.refno + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }

        public void ReprintReceipt(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
       
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += Classes.ReceiptSetup.doTitle("R E P R I N T");
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["QtySold"].Value + " @ " + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["TotalAmount"].Value + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
            }
            details += HelperFunction.PrinttoRight("--------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL.DUE", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("========") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            //if (!Directory.Exists(filepath))
            //{
            //    Directory.CreateDirectory(filepath);
            //}
            txtorder = "\\" + ordercode + ".txt";
            //string filetoprint = filepath + txtorder;
            //StreamWriter writer = new StreamWriter(filepath + txtorder);
            //writer.Write(details);
            //writer.Close();
            string filetoprint = filepath + txtorder;
            printTextFile(filetoprint);
        }
        
        
        

        public void printRefundSales(string transcode, string ordercode, string vatablesale, string vatexemptsale, string vat, DataGridView gridview)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\RefundSales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Branch.getBranchAddress(Login.assignedBranch)) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PRMT#: xxxxxxxxx-xxxxxxx-xxxxxxx") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN#: xxxxxxxxx   S/N: 000000000") + Environment.NewLine + Environment.NewLine;

            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                //  string qty = gridview.Rows[i].Cells["Qty"].Value.ToString();
                string a = gridview.Rows[i].Cells["QtySold"].Value + " Kg" + " x" + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();

                details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Description"].Value.ToString(), b) + Environment.NewLine;
                details += HelperFunction.PrintCenterText("REFUND") + Environment.NewLine;
            }

            details += Environment.NewLine;

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CASH");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a2 = totalChange.Text;

            //details += HelperFunction.PrintRightToMiddle(totalLabel.Text, total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------
            //details += HelperFunction.PrintLeftRigthText("VAT Exempt Sales", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Sale", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Exempt Sale", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT", vat) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("O.R No.", orderr) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Login.Fullname) + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Customer Name: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Address: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Business Style: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Signature:_________________ ") + Environment.NewLine;

            details += HelperFunction.PrintCenterText("SPIRE IT SOLUTIONS INC.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("3720 Woodland Heights Banawa Cebu City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("D.Issue: 01/01/2016 Valid 12/12/2016") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("ACCR#: XXX-XXXXXXXX-XXXXXXXXX") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS IS NOT AN OFFICIAL RECEIPT") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Thank You and Please Come Again!");
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //StreamWriter writer = new StreamWriter(filepath + @"\rea.txt");
            txtorder = "\\" + ordercode + ".txt";
            //txtorder = "\\10009.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void printReceiptDailySales(string transcode, string ordercode, string total, string cash, string change, DataGridView gridview)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Branch.getBranchAddress(Login.assignedBranch)) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PRMT#: xxxxxxxxx-xxxxxxx-xxxxxxx") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN#: xxxxxxxxx   S/N: 000000000") + Environment.NewLine + Environment.NewLine;

            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            
            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;
            details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
           
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " Kg" + " x" + gridview.Rows[i].Cells["UnitPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["Amount"].Value;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
            }

            details += Environment.NewLine;
           
            Label totalLabel = new Label();
            totalLabel.Text = HelperFunction.PrintLeftText("TOTAL");
            totalLabel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CHANGE");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a1 = totalLabel.Text;
            string a2 = totalChange.Text;

            details += HelperFunction.PrintRightToMiddle(totalLabel.Text, total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASH", cash) + Environment.NewLine;
            details += HelperFunction.PrintRightToMiddle(a2, change) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            //----------------------------------------------------------------------------------------------------
            details += HelperFunction.PrintLeftRigthText("VAT Exempt Sales", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Zero-Rated Sales", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT", "0.00") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Customer: Eulz Avancena") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Cashier: Paulo Pascual") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftText("SOLD TO : ___________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("ADDRESS : ___________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN #   : ___________________") + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("SPIRE IT SOLUTIONS INC.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("3720 Woodland Heights Banawa Cebu City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("D.Issue: 01/01/2016 Valid 12/12/2016") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("ACCR#: XXX-XXXXXXXX-XXXXXXXXX") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS IS NOT AN OFFICIAL RECEIPT") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Thank You and Please Come Again!");
            details += HelperFunction.LastPagePaper();
            //----------------------------------------------------------------------------------------------------

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //StreamWriter writer = new StreamWriter(filepath + @"\rea.txt");
            txtorder = "\\" + PointOfSale.refno + ".txt";
            //txtorder = "\\10009.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void printXReadReportGroupSales(string totalkilos, string totalamount, DataGridView gridview,string date1,string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\GroupSales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            details += Classes.ReceiptSetup.doTitle(POS.POSXreadReport.reportTitle + " - " + "GROUP CATEGORY SALES");
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
          
           string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["TotalKilos"].Value + " Kg";    
                string b = " " + gridview.Rows[i].Cells["TotalAmount"].Value;
                double b1 = Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value);
                details += HelperFunction.PrintLeftRigthText(a, b1.ToString()) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("1000.00");
            Label totalLabel = new Label();
            totalLabel.Text = HelperFunction.PrintLeftText("TOTAL");
            totalLabel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
         
            details += HelperFunction.PrintLeftText(totalLabel.Text) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText(totalkilos, totalamount) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        

        public void printXReadReportFullTransactionSales(string totalvat, string totalamount, DataGridView gridview, string date1, string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            //details += HelperFunction.PrintLeftText("X-Read Report ") + date1 + " -> " + date2 + Environment.NewLine;

            details += Classes.ReceiptSetup.doTitle(POS.POSXreadReport.reportTitle + " - " + "AUTO FULL TRANSACTION");

            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            //string trans = "Trans #: " + transcode;
            //string orderr = "Order #: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
            //string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();

            string strLabel = "";
            double amount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {  
                string vattype = gridview.Rows[i].Cells["isVat"].Value.ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                    strLabel = "Vatable Sale";
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                    strLabel = "Vat Exempt Sale";
                }
                details += HelperFunction.PrintLeftRigthText(strLabel, amount.ToString()) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //   details += HelperFunction.PrinttoRight("1000.00");

            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", totalamount) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            details += HelperFunction.PrintLeftRigthText("805", totalcashamount.ToString()) + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------
            //    If (Not System.IO.Directory.Exists(salefilelocation)) Then
            //    System.IO.Directory.CreateDirectory(salefilelocation)
            //End If

            //If System.IO.File.Exists(salefilelocation & "\" & batchcode & ".txt") = True Then
            //    System.IO.File.Delete(salefilelocation & "\" & batchcode & ".txt")
            //End If

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        

        public void printXReadReportRefundSales(string totalvat, string totalamount, DataGridView gridview, string date1, string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Refund\\";//" + Login.assignedBranch + "\\" + Login.Fullname;
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Branch.getBranchAddress(Login.assignedBranch)) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PRMT#: xxxxxxxxx-xxxxxxx-xxxxxxx") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN#: xxxxxxxxx   S/N: 000000000") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("X-Read Report ") + date1 + " -> " + date2 + Environment.NewLine;
           // details += HelperFunction.PrintLeftText("Auto Full Transaction") + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            //string trans = "Trans #: " + transcode;
            //string orderr = "Order #: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
            //string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                // string vatable = "  - " + gridview.Rows[i].Cells["TotalKilos"].Value + " Kg";
                string vatexempt = " " + gridview.Rows[i].Cells["TotalAmount"].Value;
                string vardesc = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();
                string vattype = gridview.Rows[i].Cells["isVat"].Value.ToString();
                //details += HelperFunction.PrintLeftRigthText("Vatable Sale", vatable) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(vattype, vatexempt) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //   details += HelperFunction.PrinttoRight("1000.00");

            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", totalamount) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            details += HelperFunction.PrintLeftRigthText("805", totalcashamount.ToString()) + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------
            //    If (Not System.IO.Directory.Exists(salefilelocation)) Then
            //    System.IO.Directory.CreateDirectory(salefilelocation)
            //End If

            //If System.IO.File.Exists(salefilelocation & "\" & batchcode & ".txt") = True Then
            //    System.IO.File.Delete(salefilelocation & "\" & batchcode & ".txt")
            //End If

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void rePrintReceipt(string orderno)
        {
            string filepath = "C:\\POSTransaction\\Refund\\" + Login.assignedBranch + "\\" + Login.Fullname;
        }

        public void printTextFile(string location)
        {
            bool isprint = Database.checkifExist("Select isEnablePrinting FROM POSType where isEnablePrinting=1");
            if (location != "")
            {
                if(isprint)
                {
                    //string command = "/C print /d:LPT1: '"+location+"'\\mafi.txt";
                    string command = "/C print /d:LPT1: " + location + " ";
                    ProcessStartInfo apps = new System.Diagnostics.ProcessStartInfo("cmd.exe", command);
                    // Process myprocesses = new Process();
                    apps.WindowStyle = ProcessWindowStyle.Hidden;
                    Process myprocesses = Process.Start(apps);
                    myprocesses.WaitForExit();
                    myprocesses.Close();
                }
               
            }
        }

        public void printTextFile2(string location)
        {
            bool isprint = true;// Database.checkifExist("Select isEnablePrinting FROM POSType where isEnablePrinting=1");
            if (location != "")
            {
                if (isprint)
                {
                    //string command = "/C print /d:LPT1: '"+location+"'\\mafi.txt";
                    string command = "/C print /d:LPT2: " + location + " ";
                    ProcessStartInfo apps = new System.Diagnostics.ProcessStartInfo("cmd.exe", command);
                    // Process myprocesses = new Process();
                    apps.WindowStyle = ProcessWindowStyle.Hidden;
                    Process myprocesses = Process.Start(apps);
                    myprocesses.WaitForExit();
                    myprocesses.Close();
                }
            }
        }
    }
}
