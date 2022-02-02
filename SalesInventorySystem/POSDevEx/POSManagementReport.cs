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

namespace SalesInventorySystem.POSDevEx
{
    public partial class POSManagementReport : DevExpress.XtraEditors.XtraForm
    {
        string reportype = String.Empty, groupname = String.Empty;
        public static double totalamount = 0.0, regsales = 0.0, regsalesvatonly = 0.0, targetsale = 0.0;
        public POSManagementReport()
        {
            InitializeComponent();
        }

        private void POSManagementReport_Load(object sender, EventArgs e)
        {
            populateBranch();
        }
        void populateBranch()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcodemgmtdata, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranchVAT, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT BranchCode,MachineUsed FROM POSInfoDetails WHERE BranchCode='" + txtbranch.Text + "'", txtmanageddatapermachine, "MachineUsed", "MachineUsed");
            Database.displaySearchlookupEdit("SELECT BranchCode,MachineUsed FROM POSInfoDetails WHERE BranchCode='" + txtbranchVAT.Text + "'", txtmachineVAT, "MachineUsed", "MachineUsed");
          
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtsalesdatefrom.Text) || String.IsNullOrEmpty(txtsalesdateto.Text))
            {
                XtraMessageBox.Show("Date Field must not Empty");
                return;
            }
            if (comboBoxEdit1.Text == "Group Category Sales") //123
                reportype = "GROUPCATEGORY";
            else if (comboBoxEdit1.Text == "Full Transaction Sales") //123
                reportype = "FULLTRANSACTION";
            else if (comboBoxEdit1.Text == "Group Item Sales") //123
                reportype = "GROUPITEM";
            //else if (comboBoxEdit1.Text == "Cashier Sales")//123
            //    reportype = "CASHIERSALES";
            //else if (comboBoxEdit1.Text == "Audit Logs")//123
            //    reportype = "AUDITLOGS";
            else if (comboBoxEdit1.Text == "XREAD")//123
                reportype = "XREAD";
            else if (comboBoxEdit1.Text == "ZREAD")//12
                reportype = "ZREAD";
            //else if (comboBoxEdit1.Text == "REFUND")//123
            //    reportype = "REFUND";
            //else if (comboBoxEdit1.Text == "PWD")//123
            //    reportype = "PWD";
            //else if (comboBoxEdit1.Text == "Senior Citizen")//123
            //    reportype = "SENIOR";
            //else if (comboBoxEdit1.Text == "Regular Disc")//123
            //    reportype = "REGULAR";
            else if (comboBoxEdit1.Text == "Sales Summary Report")//12
                reportype = "SALESSUMMARY";
            else if (comboBoxEdit1.Text == "CreditCard")//123
                reportype = "CREDITCARD";
            else if (comboBoxEdit1.Text == "Merchant Sales")//123
                reportype = "MERCHANT";
            else if (comboBoxEdit1.Text == "SalesIN")//123
                reportype = "SALESIN";
            //else if (comboBoxEdit1.Text == "BACKUPDATA")//123
            //    reportype = "BACKUPDATA";
            executeA(reportype);
            executeB(reportype);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            //bool ok = Database.checkifExist("SELECT BranchCode FROM BatchSalesDetails2 WHERE BranchCode='" + txtbranch.Text + "' AND CAST(DateOrder as date)='" + txtsalesdatefrom.Text + "' AND MachineUsed='"+txtmachine.Text+"'");
            //if (ok)
            //{
            //    XtraMessageBox.Show("You Already have ZRead Report");
            //    return;
            //}
            //else
            //{
            //    replicateSales();
            //    XtraMessageBox.Show("Success");
            //}
            replicateSales();
            XtraMessageBox.Show("Success");
        }
        void replicateSales()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ReplicateSales";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@brcode", txtbranch.Text);
                com.Parameters.AddWithValue("@petsa", txtsalesdatefrom.Text);
                com.Parameters.AddWithValue("@machinename", txtmachine.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally { con.Close(); }

        }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,MachineUsed FROM POSInfoDetails WHERE BranchCode='" + txtbranch.Text + "'", txtmachine, "MachineUsed", "MachineUsed");
            Database.displaySearchlookupEdit("SELECT BranchCode,MachineUsed FROM POSInfoDetails WHERE BranchCode='" + txtbranch.Text + "'", txtmachineVAT, "MachineUsed", "MachineUsed");
        }

        void executeA(string reportcategory)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl1.BeginUpdate();
            try
            {
                string query = "spr_POSReports";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                com.Parameters.AddWithValue("@parmbrcode", txtbranch.Text);
                com.Parameters.AddWithValue("@datefrom", txtsalesdatefrom.Text);
                com.Parameters.AddWithValue("@dateto", txtsalesdateto.Text);
                com.Parameters.AddWithValue("@parmprocessby", "");
                com.Parameters.AddWithValue("@parmoption", reportcategory);
                com.Parameters.AddWithValue("@parmpercashier", "0");
                com.Parameters.AddWithValue("@parmispermachine", "1");
                com.Parameters.AddWithValue("@parmmachinename", txtmachine.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                Classes.DevXGridViewSettings.setGridFormat(gridView1);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControl1.EndUpdate();
            }
            con.Close();
        }
        //GET TOTAL SALES FROM BOOK1
        Double getTotalSales()
        {
            double total = 0.0;
            string amount = Database.getSingleResultSet($"SELECT dbo.func_B2getTotalSales('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            total = Convert.ToDouble(amount);
            //total = Database.getTotalSummation2("BatchSalesDetails2", "Status = 'SOLD' and isConfirmed = 1 And BranchCode = '" + txtbrcodemgmtdata.Text + "' AND CAST(DateOrder as date)= '" + txtsalesdatemgmtdata.Text + "' AND MachineUsed='"+txtmanageddatapermachine.Text+"'", "TotalAmount");
            return Math.Round(total, 2);
        }
        //GET TOTAL REG SALES FROM BOOK1 (is BIR Registered)
        Double getTotalRegSales()
        {
            double total = 0.0;
            string amount = Database.getSingleResultSet($"SELECT dbo.func_B2getTotalVatExemptSales('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            total = Convert.ToDouble(amount);
            //total = Database.getTotalSummation2("BatchSalesDetails2", "Status = 'SOLD' and isConfirmed = 1 And BranchCode = '" + txtbrcodemgmtdata.Text + "' AND CAST(DateOrder as date) = '" + txtsalesdatemgmtdata.Text + "' AND isVat=0 AND MachineUsed='" + txtmanageddatapermachine.Text + "' ", "TotalAmount");
            //total = Database.getTotalSummation2("BatchSalesDetails2", "Status = 'SOLD' and isConfirmed = 1 And BranchCode = '" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date) = '" + dateEdit2.Text + "' AND isVat=0 AND MachineUsed='" + txtmanageddatapermachine.Text + "' AND ReferenceNo NOT IN (SELECT OrderNo FROM dbo.SalesDiscount WHERE BranchCode='"+ searchLookUpEdit2.Text + "' AND MachineUsed='" + txtmanageddatapermachine.Text + "' AND CAST(DateExecute as date) = '" + dateEdit2.Text + "')", "TotalAmount");
            return Math.Round(total, 2);
        }
        //GET TOTAL REG SALES FROM BOOK1 (Vat Exempt Products)
        Double getTotalRegSalesVatExemptOnly()
        {
            double total = 0.0;
            string amount = Database.getSingleResultSet($"SELECT dbo.func_B2getTotalVatExemptSales('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            total = Convert.ToDouble(amount);
            //total = Database.getTotalSummation2("BatchSalesDetails2", "Status = 'SOLD' and isConfirmed = 1 And BranchCode = '" + txtbrcodemgmtdata.Text + "' AND CAST(DateOrder as date) = '" + txtsalesdatemgmtdata.Text + "' AND isVat=0 AND MachineUsed='" + txtmanageddatapermachine.Text + "' ", "TotalAmount");
            //total = Database.getTotalSummation2("BatchSalesDetails2", "Status = 'SOLD' and isConfirmed = 1 And BranchCode = '" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date) = '" + dateEdit2.Text + "' AND isVat=0 AND MachineUsed='" + txtmanageddatapermachine.Text + "' AND ReferenceNo NOT IN(SELECT OrderNo FROM dbo.SalesDiscount WHERE BranchCode = '"+ searchLookUpEdit2.Text + "' AND MachineUsed = '" + txtmanageddatapermachine.Text + "' AND CAST(DateExecute as date) = '" + dateEdit2.Text + "')", "TotalAmount");
            return Math.Round(total, 2);
        }
        //GET TOTAL UNREG SALES FROM BOOK1 (BIR Unregistered)
        Double getTotalUnRegSales()
        {
            double total = 0.0;
            string amount = Database.getSingleResultSet($"SELECT dbo.func_B2getTotalVatableSales('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            total = Convert.ToDouble(amount);
            //total = Database.getTotalSummation2("BatchSalesDetails2", "Status = 'SOLD' and isConfirmed = 1 And BranchCode = '" + txtbrcodemgmtdata.Text + "' AND CAST(DateOrder as date) = '" + txtsalesdatemgmtdata.Text + "' AND isVat=1 AND MachineUsed='" + txtmanageddatapermachine.Text + "'", "TotalAmount");
            return Math.Round(total, 2);
        }

        Double getRefundAmount()
        {
            double refund = 0.0;
            //refund = getTotalSales() * Convert.ToDouble(txtpercentage.Text);
            refund = getTotalRegSales() * Convert.ToDouble(txtpercentage.Text);
            return Math.Round(refund, 2);
        }
        Double getTargetSales()
        {
            double targetsales = 0.0;
            //targetsales = getTotalSales() - getRefundAmount();
            targetsales = getTotalRegSales() - getRefundAmount();
            return Math.Round(targetsales, 2);
        }
        Double getVaraince()
        {
            double targetsales = 0.0;
            targetsales = getTotalRegSales() - getTargetSales();
            //targetsales = getTotalSales() - getTargetSales();
            return Math.Round(targetsales, 2);
        }
         Double getVarainceVAT()
        {
            double targetsales = 0.0;
            targetsales = getTotalUnRegSales() - getTargetSales();
            return Math.Round(targetsales, 2);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to reset sale manipulation?", "Reset Sales");
            if (String.IsNullOrEmpty(txtbrcodemgmtdata.Text) || String.IsNullOrEmpty(txtsalesdatemgmtdata.Text))
            {
                XtraMessageBox.Show("Please Select Branch and Date Parameters!");
                return;
            }
            if (ok)
                resetSales();
            else
                return;
        }

        void resetSales()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_resetSales";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@brcode", txtbrcodemgmtdata.Text);
                com.Parameters.AddWithValue("@petsa", txtsalesdatemgmtdata.Text);
                com.Parameters.AddWithValue("@machinename", txtmanageddatapermachine.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Success");
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Database.display("SELECT ProductCode,CategoryCode,Description,QtySold,SellingPrice,TotalAmount FROM BatchSalesDetails2 WHERE Status='SOLD' and TotalAmount > 0 and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "' AND MachineUsed='" + txtmanageddatapermachine.Text + "' AND ReferenceNo NOT IN (SELECT OrderNo FROM dbo.SalesDiscount WHERE BranchCode='" + searchLookUpEdit2.Text + "' AND MachineUsed='" + txtmanageddatapermachine.Text + "' AND CAST(DateExecute as date) = '" + dateEdit2.Text + "')", gridControl2, gridView3);
            Database.display($"SELECT * FROM dbo.func_managedata('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')", gridControl2, gridView3);

            Classes.DevXGridViewSettings.setGridFormat(gridView3);
            gridView3.Columns["CategoryCode"].Visible = false;
            gridView3.BestFitColumns();
            gridView3.Columns["QtySold"].Summary.Clear();
            gridView3.Columns["TotalAmount"].Summary.Clear();
            gridView3.Columns["QtySold"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "QtySold", "{0}");
            gridView3.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}");
            txttotalsales.Text = getTotalSales().ToString();
            txttotalsalesunreg.Text = getTotalUnRegSales().ToString();
            txtsalesregprod.Text = getTotalRegSales().ToString();
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT distinct MachineUsed FROM BatchSalesSummary WHERE CAST(TransDate as date)='" + txtsalesdatemgmtdata.Text + "' and BranchCode='" + txtbrcodemgmtdata.Text + "'", txtmanageddatapermachine, "MachineUsed", "MachineUsed");
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void gridControl4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl4, e.Location);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView11.RowCount <= 0 || gridView11.RowCount <= 0)
            {
                XtraMessageBox.Show("Please Generate Reports First!...");
                return;
            }
            else if (gridView11.RowCount > 0 && comboBoxEdit1.Text == "XREAD")
            {
                printFinancialReport(txtbranch.Text
                    , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateOpen").ToString()
                    , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MachineUsed").ToString()
                    , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UserID").ToString());
                XtraMessageBox.Show("XREAD Successfully Reprint!...");
            }
            else if (gridView11.RowCount > 0 && comboBoxEdit1.Text == "ZREAD")
            {
                PrintZRead(txtbranch.Text
                    , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateExecute").ToString()
                    , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MachineUsed").ToString());
                XtraMessageBox.Show("ZREAD Successfully Reprint!...");
            }
            //else if (gridView1.RowCount > 0 && comboBoxEdit1.Text == "BACKUPDATA")
            //{
            //    PrintZRead(txtbranch.Text
            //        , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateExecute").ToString()
            //        , gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MachineUsed").ToString(), "2");
            //    XtraMessageBox.Show("Successfully Reprint!...");
            //}
        }
        //XREAD CASHIER CLOSE TRANSACTION REPORT
        //XREAD CASHIER CLOSE TRANSACTION REPORT
        void printFinancialReport(string branch, string date, string machine, string cashier)
        {
            try
            {
                //var row = Database.getMultipleQuery("SalesDenomination", "BranchCode='"+ branch + "' " +
                //    "and TransactionDate='"+ date + "' " +
                //    "and MachineUsed='"+machine+"' " +
                //    "AND Cashier='"+cashier+"' "
                //    , "No1k" +
                //    ",Total1k" +
                //    ",No5h" +
                //    ",Total5h" +
                //    ",No2h" +
                //    ",Total2h" +
                //    ",No1h" +
                //    ",Total1h" +
                //    ",No50p" +
                //    ",Total50p" +
                //    ",No20p" +
                //    ",Total20p" +
                //    ",No10p" +
                //    ",Total10p" +
                //    ",No5p" +
                //    ",Total5p" +
                //    ",No1p" +
                //    ",Total1p" +
                //    ",No25c" +
                //    ",Total25c" +
                //    ",");
                var rowz = Database.getMultipleQuery("SalesTransactionSummary", "BranchCode='" + branch + "' " +
                    "and DateOpen='" + date + "'" +
                    "and MachineUsed = '" + machine + "' " +
                    "AND UserID='" + cashier + "' "
                    , "DateOpen" +
                    ",TransactionBegin" +
                    ",UserID" +
                    ",CashierTransNo" +
                    ",MachineUsed" +
                    ",TotalCashSales" +
                    ",TotalCreditSales" +
                    ",TotalSales" +
                    ",BeginningCash" +
                    ",TotalNetSales" +
                    ",TotalActualCash" +
                    ",CashRemitted" +
                    ",Shortage" +
                    ",Overage" +
                    ",BeginningSI" +
                    ",EndingSI" +
                    ",BeginningReturnTransNo" +
                    ",EndingReturnTransNo" +
                    ",NoOfSoldItem" +
                    ",NoOfReturnedItem" +
                    ",TotalReturnedSales" +
                    ",NoOfCancelledItem" +
                    ",TotalCancelledItem" +
                    ",NoOfVoidItem" +
                    ",TotalVoidItem" +
                    ",NoOfTaxItem" +
                    ",TotalTax" +
                    ",NoOfSCDisc" +
                    ",TotalSCDisc" +
                    ",NoOfPWDDisc" +
                    ",TotalOfPWDDisc" +
                    ",NoOfRegDisc" +
                    ",TotalOfRegDisc" +
                    ",TotalVatableSales" +
                    ",TotalVatAmount" +
                    ",TotalVatExemptSales" +
                    ",TotalZeroRatedSale" +
                    ",TotalDiscount" +
                    ",TotalNetSalesOfVat" +
                    ",NoOfDiscount" +
                    ",BeginningTransactionNo" +
                    ",EndingTransactionNo");

                string DateOpen = rowz["DateOpen"].ToString();
                string TransactionBegin = rowz["TransactionBegin"].ToString();
                string UserID = rowz["UserID"].ToString();
                string CashierTransNo = rowz["CashierTransNo"].ToString();
                string MachineUsed = rowz["MachineUsed"].ToString();
                string TotalCashSales = rowz["TotalCashSales"].ToString();
                string TotalCreditSales = rowz["TotalCreditSales"].ToString();
                string TotalSales = rowz["TotalSales"].ToString();
                string BeginningCash = rowz["BeginningCash"].ToString();
                string TotalNetSales = rowz["TotalNetSales"].ToString();
                string TotalActualCash = rowz["TotalActualCash"].ToString();
                string CashRemitted = rowz["CashRemitted"].ToString();
                string Shortage = rowz["Shortage"].ToString();
                string Overage = rowz["Overage"].ToString();
                string BeginningSI = rowz["BeginningSI"].ToString();
                string EndingSI = rowz["EndingSI"].ToString();
                string BeginningReturnTransNo = rowz["BeginningReturnTransNo"].ToString();
                string EndingReturnTransNo = rowz["EndingReturnTransNo"].ToString();
                string NoOfSoldItem = rowz["NoOfSoldItem"].ToString();
                string NoOfReturnedItem = rowz["NoOfReturnedItem"].ToString();
                string TotalReturnedSales = rowz["TotalReturnedSales"].ToString();
                string NoOfCancelledItem = rowz["NoOfCancelledItem"].ToString();
                string TotalCancelledItem = rowz["TotalCancelledItem"].ToString();
                string NoOfVoidItem = rowz["NoOfVoidItem"].ToString();
                string TotalVoidItem = rowz["TotalVoidItem"].ToString();
                string NoOfTaxItem = rowz["NoOfTaxItem"].ToString();
                string TotalTax = rowz["TotalTax"].ToString();
                string NoOfDiscount = rowz["NoOfDiscount"].ToString();
                string NoOfSCDisc = rowz["NoOfSCDisc"].ToString();
                string TotalSCDisc = rowz["TotalSCDisc"].ToString();
                string NoOfPWDDisc = rowz["NoOfPWDDisc"].ToString();
                string TotalOfPWDDisc = rowz["TotalOfPWDDisc"].ToString();
                string NoOfRegDisc = rowz["NoOfRegDisc"].ToString();
                string TotalOfRegDisc = rowz["TotalOfRegDisc"].ToString();
                string TotalVatableSales = rowz["TotalVatableSales"].ToString();
                string TotalVatAmount = rowz["TotalVatAmount"].ToString();
                string TotalVatExemptSales = rowz["TotalVatExemptSales"].ToString();
                string TotalZeroRatedSales = rowz["TotalZeroRatedSale"].ToString();
                string TotalDiscount = rowz["TotalDiscount"].ToString();
                string TotalNetSalesOfVat = rowz["TotalNetSalesOfVat"].ToString();
                string BeginningTransactionNo = rowz["BeginningTransactionNo"].ToString();
                string EndingTransactionNo = rowz["EndingTransactionNo"].ToString();

                //string No1k = row["No1k"].ToString();
                //string Total1k = row["Total1k"].ToString();
                //string No5h = row["No5h"].ToString();
                //string Total5h = row["Total5h"].ToString();
                //string No2h = row["No2h"].ToString();
                //string Total2h = row["Total2h"].ToString();
                //string No1h = row["No1h"].ToString();
                //string Total1h = row["Total1h"].ToString();
                //string No50p = row["No50p"].ToString();
                //string Total50p = row["Total50p"].ToString();
                //string No20p = row["No20p"].ToString();
                //string Total20p = row["Total20p"].ToString();
                //string No10p = row["No10p"].ToString();
                //string Total10p = row["Total10p"].ToString();
                //string No5p = row["No5p"].ToString();
                //string Total5p = row["Total5p"].ToString();
                //string No1p = row["No1p"].ToString();
                //string Total1p = row["Total1p"].ToString();
                //string No25c = row["No25c"].ToString();
                //string Total25c = row["Total25c"].ToString();

                DateTime dt = DateTime.Now;
                String details = "";
                string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + date + "')");

                string filepath = "C:\\POSTransaction\\FinancialReport\\" + branch + "\\" + transdate + "\\" + UserID + "\\";

                details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

                details += Classes.ReceiptSetup.doHeader(branch);

                string petsa = DateTime.Now.ToShortDateString();
                string oras = DateTime.Now.ToShortTimeString();
                string fulldate1 = petsa + ' ' + oras;

                string format = "dd-MMM-yyyy";

                double grosssales = 0.0, endingbalance = 0.0;

                details += HelperFunction.PrintLeftText(Convert.ToDateTime(date).ToString(format)) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER : " + UserID) + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("X - READING") + Environment.NewLine;
                details += HelperFunction.PrintCenterText("Cashier's Accountability Report") + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftText("TRAN.#: " + BeginningTransactionNo) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER : " + UserID) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                grosssales = Convert.ToDouble(TotalCashSales) + Convert.ToDouble(TotalCreditSales);
                //details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", HelperFunction.convertToNumericFormat(grosssales))+ Environment.NewLine; //total sales txtTotalSales.Text)
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CASH :", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCashSales))) + Environment.NewLine; //total sales
                details += HelperFunction.PrintLeftRigthText("CREDIT :", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCreditSales))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("RETURN :", HelperFunction.convertToNumericFormat(txtTotalVoidTransaction.Text)) + Environment.NewLine; //total void

                details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("GROSS SALE ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalSales))) + Environment.NewLine; //total sales
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(BeginningCash))) + Environment.NewLine; //numitemsold
                //endingbalance = grosssales + Convert.ToDouble(txtBeginningCash.Text);
                endingbalance = Convert.ToDouble(TotalNetSales);// + Convert.ToDouble(txtBeginningCash.Text);
                details += HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(endingbalance)) + Environment.NewLine; //numitemsold

                details += HelperFunction.PrintLeftRigthText("Actual Cash On Hand: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalActualCash))) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Cash Remittance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(CashRemitted))) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Shortage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(Shortage))) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Overage: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(Overage))) + Environment.NewLine + Environment.NewLine; //numitemsold

                //details += HelperFunction.PrintLeftRigthText("Transaction #: ", txtlasttranno.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Beginning SI No.: ", BeginningSI) + Environment.NewLine; //beginvoice
                details += HelperFunction.PrintLeftRigthText("Ending SI No.: ", EndingSI) + Environment.NewLine; //lastornum

                details += HelperFunction.PrintLeftRigthText("Beginning Return Trans No.: ", BeginningReturnTransNo) + Environment.NewLine; //beginvoice
                details += HelperFunction.PrintLeftRigthText("Ending Return Trans No.: ", EndingReturnTransNo) + Environment.NewLine; //lastornum

                details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", NoOfSoldItem) + Environment.NewLine; //numitemsold
                                                                                                                        //details += HelperFunction.PrintLeftRigthText("Transaction Count: ", txttransactioncount.Text) + Environment.NewLine; //numtranscunt
                                                                                                                        //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", NoOfReturnedItem) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalReturnedSales))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", NoOfCancelledItem) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCancelledItem))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", NoOfVoidItem) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalVoidItem))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. VAT Items: ", NoOfTaxItem) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total VATable Items Sold: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalVatableSales))) + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", NoOfDiscount) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Discounts: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("DISCOUNT BREAKDOWN") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of SC Discount: ", NoOfSCDisc) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalSCDisc))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", NoOfPWDDisc) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalOfPWDDisc))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. Regular Discount: ", NoOfRegDisc) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalOfRegDisc))) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;


                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalVatableSales))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalVatAmount))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("Net Sales of VAT: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(txtnetsalesofvat.Text))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VATable Exempt Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalVatExemptSales))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalZeroRatedSales))) + Environment.NewLine;
                details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("Less: Discount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalDiscount))) + Environment.NewLine;
                details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("ACCUMULATED GRAND TOTAL", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalNetSalesOfVat))) + Environment.NewLine; //total sales txtTotalSales.Text)

                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("C A S H  C O U N T") + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER: " + Login.Fullname) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;

                //details += HelperFunction.PrintLeftRigthText("[DENOMIN] - ", "[QTY] = [ AMT ]") + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText(" 1000 -  ", No1k + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total1k))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("  500 -  ", No5h + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total5h))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("  200 -  ", No2h + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total2h))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("  100 -  ", No1h + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total1h))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("   50 -  ", No50p + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total50p))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("   20 -  ", No20p + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total20p))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("   10 -  ", No10p + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total10p))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("    5 -  ", No5p + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total5p))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("    1 -  ", No1p + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total1p))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("  .25 -  ", No25c + " = " + HelperFunction.convertToNumericFormat(Convert.ToDouble(Total25c))) + Environment.NewLine;
                //details += HelperFunction.PrintRightToLeft(" ", "-------") + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("TOTAL     P ", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalActualCash))) + Environment.NewLine;
                //details += HelperFunction.PrintRightToLeft(" ", "=======") + Environment.NewLine;

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

                //details += HelperFunction.PrintCenterText("L OR.#:" + txtlastornumber.Text) + Environment.NewLine;


                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

                details += HelperFunction.LastPagePaper();
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string transno = UserID + ".txt";//CashierTransNo + ".txt";
                string filetoprint = filepath + transno;
                string mark = filepath + transno;
                StreamWriter writer = new StreamWriter(filepath + transno);
                writer.Write(details);
                writer.Close();

                Printing printfile = new Printing();
                printfile.printTextFile(filetoprint);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void PrintZRead(string bcode, string dateex, string terminal)
        {
            DateTime dt = Convert.ToDateTime(dateex);
            String details = "";
            string filepath = "";
            
            filepath = "C:\\ProgramFlies\\EndOfDay\\" + bcode + "\\" + terminal + "\\" + dt.ToString("yyyyMMdd") + "\\";
            
            string branchcode, CounterNo, TransactionNo, MachineUsed, BeginningBalance, EndingBalance, BeginSI, EndingSI, BeginRetNo, EndingRetNo, TotalCashSales, TotalCreditSales, TotalSales, TotalDiscount, VatExemptSales, VatableSales, VatAmount, DateExeute, ExecuteBy, TotalNetSales;
            string noofsolditems, noofcancelleditems, noofvoiditems, noofreturneditems, noofvatitems, noofdiscountitems, totalcancelledsales, totalvoidsales, totalreturnedsales, totalvatsales;
            string noofscdisc, noofpwddisc, noofregdisc, totalscdisc, totalpwddisc, totalregdisc, totalzeroratedsales, VatAdjustment;


            var rows = Database.getMultipleQuery($"POSZReadingTransactions2",
              "BranchCode='" + bcode + "' " +
              "and CAST(DateExecute as date)='" + dateex + "' " +
              "and MachineUsed='" + terminal + "' ",
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

            details += Classes.ReceiptSetup.doHeader(branchcode, terminal);



            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

            double grosssales = 0.0;

            details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Z - READING") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Z Counter #: " + CounterNo) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran #: " + TransactionNo) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: " + MachineUsed) + Environment.NewLine;
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

            details += HelperFunction.PrintLeftRigthText("Beginning Return Transaction No.: ", BeginRetNo) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Ending Return Transaction No.: ", EndingRetNo) + Environment.NewLine + Environment.NewLine; //lastornum

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
            details += HelperFunction.PrintLeftRigthText("ZERO Rated Sales: ", "0.00") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("ACCUMULATED GRAND TOTAL ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))) + Environment.NewLine; //total sales txtTotalSales.Text)
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
            // string transno = DateTime.Now.ToString("yyyyMMdd") + ".txt";dt.ToString("yyyyMMdd")
            string transno = dt.ToString("yyyyMMdd") + ".txt";
            string filetoprint = filepath + transno;
            string mark = filepath + transno;
            StreamWriter writer = new StreamWriter(filepath + transno);
            writer.Write(details);
            writer.Close();
            Printing printfile = new Printing();
            printfile.printTextFile(filetoprint);
            //embedToJournal();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            Database.display("SELECT ProductCode,CategoryCode,Description,QtySold,SellingPrice,TotalAmount FROM BatchSalesDetails2 WHERE Status='SOLD' and TotalAmount > 0 and isConfirmed=1 And BranchCode='" + txtbranchVAT.Text + "' AND CAST(DateOrder as date)='" + txtdateVAT.Text + "' AND MachineUsed='" + txtmachineVAT.Text + "'", gridControl3, gridView4);

            Classes.DevXGridViewSettings.setGridFormat(gridView4);
            gridView4.Columns["CategoryCode"].Visible = false;
            gridView4.BestFitColumns();
            gridView4.Columns["QtySold"].Summary.Clear();
            gridView4.Columns["TotalAmount"].Summary.Clear();
            gridView4.Columns["QtySold"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "QtySold", "{0}");
            gridView4.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}");
            txttotalsalesVAT.Text = getTotalSales().ToString();
            txtvatableVAT.Text = getTotalUnRegSales().ToString();
            txtvatexVAT.Text = getTotalRegSales().ToString();
        }

        private void btnanalyzeVAT_Click(object sender, EventArgs e)
        {
            txtrefundVAT.Text = getRefundAmount().ToString();
            txttargetVAT.Text = getTargetSales().ToString();
            txtvarianceVAT.Text = getVarainceVAT().ToString();//getVaraince().ToString();
            btneditdataVAT.Enabled = true;
        }

        private void btneditdataVAT_Click(object sender, EventArgs e)
        {
            //regsales = getTotalRegSales(); //getTotalRegSales [VATEXEMPT SALES]
            regsales = getTotalUnRegSales(); //getTotalRegSales [VATEXEMPT SALES]
            //way gamit
            //regsalesvatonly = getTotalRegSalesVatExemptOnly(); //getTotalRegSalesVatExemptOnly [VATEXEMPT SALES]
            targetsale = Convert.ToDouble(txttargetsales.Text);
            double goal = 0.0, goal1;
            POSDevEx.ManipulateDataDevEx showd = new ManipulateDataDevEx();
            showd.Show();
            POSDevEx.ManipulateDataDevEx.brcode = txtbranchVAT.Text;
            POSDevEx.ManipulateDataDevEx.petsa = txtdateVAT.Text;
            POSDevEx.ManipulateDataDevEx.machinename = txtmachineVAT.Text;
            //(CategoryCode='10' OR CategoryCode='11' OR CategoryCode='12' OR CategoryCode='14'  OR CategoryCode='15'  OR CategoryCode='17')
            // Database.display("SELECT SequenceNumber,ProductCode,CategoryCode,Description,QtySold,SellingPrice,TotalAmount,QtySold as NewQty,TotalAmount as NewTotalAmount,'0' as Difference FROM BatchSalesDetails2 WHERE CategoryCode in (Select ProductCategoryID from ProductCategory WHERE isVat=0) AND Status='SOLD' and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "'  ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            //Database.display("SELECT SequenceNumber,ProductCode,CategoryCode,Description,QtySold,SellingPrice,TotalAmount,QtySold as NewQty,TotalAmount as NewTotalAmount,'0' as Difference FROM BatchSalesDetails2 WHERE (CategoryCode='10' OR CategoryCode='11' OR CategoryCode='12' OR CategoryCode='14' OR CategoryCode='15') AND Status='SOLD' and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "'  ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            Database.display($"SELECT * FROM dbo.func_vatmanip('{txtbranchVAT.Text}','{txtdateVAT.Text}','{txtmachineVAT.Text}') ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            //totalamount = Database.getTotalSummation2("BatchSalesDetails2", "Status='SOLD' and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "' AND (CategoryCode='10' OR CategoryCode='11' OR CategoryCode='12')", "TotalAmount");
            showd.txtrefundamount.Text = txtrefundVAT.Text; //OK 30%
            showd.txttargetsales.Text = txttargetVAT.Text; //OK 70%
            showd.txtvariance.Text = txtvarianceVAT.Text;
            goal = regsales - Convert.ToDouble(txttargetsales.Text);
            //goal1 = regsales - goal;
            goal1 = regsales - goal;
            showd.txtgoal.Text = goal1.ToString();
            showd.gridView3.BestFitColumns();
            showd.gridView3.Columns["CategoryCode"].Visible = false;
            showd.gridView3.Columns["QtySold"].Summary.Clear();
            showd.gridView3.Columns["TotalAmount"].Summary.Clear();
            showd.gridView3.Columns["QtySold"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "QtySold", "{0}");
            showd.gridView3.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}");
            showd.gridView3.Columns["NewQty"].Summary.Clear();
            showd.gridView3.Columns["NewTotalAmount"].Summary.Clear();
            showd.gridView3.Columns["NewQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NewQty", "{0}");
            showd.gridView3.Columns["NewTotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NewTotalAmount", "{0}");
            showd.gridView3.Columns["Difference"].Summary.Clear();
            showd.gridView3.Columns["Difference"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Difference", "{0}");

            string total = Database.getSingleResultSet($"SELECT dbo.func_getVatManipValue('{txtbranchVAT.Text}','{txtdateVAT.Text}','{txtmachineVAT.Text}')");
            //if (String.IsNullOrEmpty(total)) { total = "0"; }
            double sumval = 0.0;
            sumval = Convert.ToDouble(total) - Convert.ToDouble(txtrefundVAT.Text);
            // var summaryValue = gridView3.Columns["NewTotalAmount"].SummaryItem.SummaryValue;
            //textEdit1.EditValue = summaryValue;
            //var bb = showd.gridView3.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}").ToString();
            showd.textEdit1.Text = sumval.ToString();
            //string[] hidecols = { "SequenceNumber", "CategoryCode" };
        }

        private void txtbrcodemgmtdata_EditValueChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,MachineUsed FROM POSInfoDetails WHERE BranchCode='" + txtbrcodemgmtdata.Text + "'", txtmanageddatapermachine, "MachineUsed", "MachineUsed");
        }

        Double computeTotalVAT()
        {
            double vatexemptsale = 0.0;
            double vat = 0.0;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(gridView1.GetRowCellValue(i, "isVat").ToString()) == true)
                {
                    vatexemptsale += Math.Round(Convert.ToDouble(gridView1.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                }
            }
            vat = vatexemptsale * .12;
            return vat;
        }
        private void btnanalyze_Click(object sender, EventArgs e)
        {
            double targetSales = 0.0, percentage = 0.0;
            if(radtypeall.Checked==true)
            {
                percentage = Convert.ToDouble(txtpercentage.Text) * getTotalSales();
                targetSales = getTotalSales()- percentage; 
            }
            else if(radtypevatex.Checked==true)
            {
                percentage = Convert.ToDouble(txtpercentage.Text) * getTotalRegSalesVatExemptOnly();
                targetSales= getTotalRegSalesVatExemptOnly()- percentage; 
            }
            else if(radtypevatable.Checked==true)
            {
                percentage = Convert.ToDouble(txtpercentage.Text) * getTotalUnRegSales();
                targetSales= getTotalUnRegSales()- percentage; 
            }
            txttargetsales.Text = targetSales.ToString();//getTargetSales().ToString();
            txtrefundamount.Text = percentage.ToString();//getRefundAmount().ToString();
            txtvariance.Text = percentage.ToString();//getVaraince().ToString();
            simpleButton4.Enabled = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            regsales = getTotalRegSales(); //getTotalRegSales [VATEXEMPT SALES]
            //regsales = getTotalSales(); //getTotalRegSales [VATEXEMPT SALES]
            //regsalesvatonly = getTotalRegSalesVatExemptOnly(); //getTotalRegSalesVatExemptOnly [VATEXEMPT SALES]
            targetsale = Convert.ToDouble(txttargetsales.Text);
            double goal = 0.0, goal1;
            POSDevEx.ManipulateDataDevEx showd = new ManipulateDataDevEx();
            showd.Show();
            POSDevEx.ManipulateDataDevEx.brcode = txtbrcodemgmtdata.Text;
            POSDevEx.ManipulateDataDevEx.petsa = txtsalesdatemgmtdata.Text;
            POSDevEx.ManipulateDataDevEx.machinename = txtmanageddatapermachine.Text;
            //(CategoryCode='10' OR CategoryCode='11' OR CategoryCode='12' OR CategoryCode='14'  OR CategoryCode='15'  OR CategoryCode='17')
            // Database.display("SELECT SequenceNumber,ProductCode,CategoryCode,Description,QtySold,SellingPrice,TotalAmount,QtySold as NewQty,TotalAmount as NewTotalAmount,'0' as Difference FROM BatchSalesDetails2 WHERE CategoryCode in (Select ProductCategoryID from ProductCategory WHERE isVat=0) AND Status='SOLD' and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "'  ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            //Database.display("SELECT SequenceNumber,ProductCode,CategoryCode,Description,QtySold,SellingPrice,TotalAmount,QtySold as NewQty,TotalAmount as NewTotalAmount,'0' as Difference FROM BatchSalesDetails2 WHERE (CategoryCode='10' OR CategoryCode='11' OR CategoryCode='12' OR CategoryCode='14' OR CategoryCode='15') AND Status='SOLD' and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "'  ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            if(radtypeall.Checked==true)
            {
                Database.display($"SELECT * FROM dbo.func_Allmanip('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}') ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            }
            if (radtypevatex.Checked==true)
            {
                Database.display($"SELECT * FROM dbo.func_vatexmanip('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}') ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            }
            if (radtypevatable.Checked==true)
            {
                Database.display($"SELECT * FROM dbo.func_vatablemanip('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}') ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            }

            //Database.display($"SELECT * FROM dbo.func_vatexmanip('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}') ORDER BY TotalAmount DESC", showd.gridControl2, showd.gridView3);
            //totalamount = Database.getTotalSummation2("BatchSalesDetails2", "Status='SOLD' and isConfirmed=1 And BranchCode='" + searchLookUpEdit2.Text + "' AND CAST(DateOrder as date)='" + dateEdit2.Text + "' AND (CategoryCode='10' OR CategoryCode='11' OR CategoryCode='12')", "TotalAmount");
            showd.txtrefundamount.Text = txtrefundamount.Text; //OK 30%
            showd.txttargetsales.Text = txttargetsales.Text; //OK 70%
            showd.txtvariance.Text = txtvariance.Text;

            goal = regsales - Convert.ToDouble(txttargetsales.Text);
            //goal1 = regsales - goal;
            goal1 = regsales - goal;
            //goal1 = regsalesvatonly - goal;
            showd.txtgoal.Text = goal1.ToString();
            showd.gridView3.BestFitColumns();
            showd.gridView3.Columns["CategoryCode"].Visible = false;
            showd.gridView3.Columns["QtySold"].Summary.Clear();
            showd.gridView3.Columns["TotalAmount"].Summary.Clear();
            showd.gridView3.Columns["QtySold"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "QtySold", "{0}");
            showd.gridView3.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}");
            showd.gridView3.Columns["NewQty"].Summary.Clear();
            showd.gridView3.Columns["NewTotalAmount"].Summary.Clear();
            showd.gridView3.Columns["NewQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NewQty", "{0}");
            showd.gridView3.Columns["NewTotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NewTotalAmount", "{0}");
            showd.gridView3.Columns["Difference"].Summary.Clear();
            showd.gridView3.Columns["Difference"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Difference", "{0}");

            string total = "";// Database.getSingleResultSet($"SELECT dbo.func_getVatExManipValue('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            if (radtypeall.Checked == true)
            {
                total=Database.getSingleResultSet($"SELECT dbo.func_getAllManipValue('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            }
            if (radtypevatex.Checked == true)
            {
                total=Database.getSingleResultSet($"SELECT dbo.func_getVatExManipValue('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            }
            if (radtypevatable.Checked == true)
            {
                total=Database.getSingleResultSet($"SELECT dbo.func_getVatableManipValue('{txtbrcodemgmtdata.Text}','{txtsalesdatemgmtdata.Text}','{txtmanageddatapermachine.Text}')");
            }
            //if (String.IsNullOrEmpty(total)) { total = "0"; }
            double sumval = 0.0;
            sumval = Convert.ToDouble(total) - Convert.ToDouble(txtrefundamount.Text);
            // var summaryValue = gridView3.Columns["NewTotalAmount"].SummaryItem.SummaryValue;
            //textEdit1.EditValue = summaryValue;
            //var bb = showd.gridView3.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}").ToString();
            showd.textEdit1.Text = sumval.ToString();
            //string[] hidecols = { "SequenceNumber", "CategoryCode" };
        }

        void executeB(string reportcategory)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl4.BeginUpdate();
            try
            {
                string query = "spr_POSReports2";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                com.Parameters.AddWithValue("@parmbrcode", txtbranch.Text);
                com.Parameters.AddWithValue("@datefrom", txtsalesdatefrom.Text);
                com.Parameters.AddWithValue("@dateto", txtsalesdateto.Text);
                com.Parameters.AddWithValue("@parmprocessby", "");
                com.Parameters.AddWithValue("@parmoption", reportcategory);
                com.Parameters.AddWithValue("@parmpercashier", "0");
                com.Parameters.AddWithValue("@parmispermachine", "1");
                com.Parameters.AddWithValue("@parmmachinename", txtmachine.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                gridView11.Columns.Clear();
                gridControl4.DataSource = null;
                adapter.Fill(table);
                gridControl4.DataSource = table;
                gridView11.BestFitColumns();
                Classes.DevXGridViewSettings.setGridFormat(gridView11);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControl4.EndUpdate();
            }
            con.Close();
        }
    }
}