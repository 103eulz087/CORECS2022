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
using SalesInventorySystem.HotelManagement;
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem
{
    public partial class Practice : DevExpress.XtraEditors.XtraForm
    {
        DataGridView view = new DataGridView();
        public Practice()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void viewPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotelFrmGuestPhoto asd = new HotelFrmGuestPhoto();
            //Image imahe = null;
            asd.pictureBox1.Image = null;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "select * FROM CIFImages WHERE CIFKey='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CIFKey").ToString() + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (reader["Image"] == System.DBNull.Value)
                        {
                            asd.pictureBox1.Image = null;
                        }
                        else
                        {
                            byte[] img = null;
                            img = (byte[])reader["Image"];
                            MemoryStream ms = new MemoryStream(img);
                            
                            ms.Seek(0, SeekOrigin.Begin);

                           
                            asd.pictureBox1.Image = Image.FromStream(ms);
                        }

                    }
                }
                else
                {
                    asd.pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            asd.ShowDialog(this);
        }

        private void Practice_Loadx(object sender, EventArgs e)
        {
            double totalPerItemDiscount = 0.0, total = 0.0;
            String details = "";
            string filepath = "C:\\POSTransactionX\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string paytype = "", amounttender = "", amountchange = "", TotalVatableSalesSum = "", TotalVatSaleSum = "", TotalVatExemptSum = "", CashierTransNo = "";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("SELECT Description," +
               " a.QtySold "+
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
                " FROM BatchSalesDetails a LEFT OUTER JOIN BatchSalesSummary b ON a.ReferenceNo=b.ReferenceNo " +
                "AND a.BranchCode=b.BranchCode " +
                "AND a.MachineUsed=b.MachineUsed " +
                "WHERE CAST(a.DateOrder as date)='10/30/2019' " +
                "AND a.ReferenceNo IN ('000000000000005331','000000000000005334') ", con);
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
                "WHERE CAST(a.DateOrder as date)='"+ datetoday + "' " +
                "AND a.ReferenceNo IN (SELECT ReferenceNo FROM ITCRESLS001 WHERE CAST(Transdate as Date)='"+datetoday+"' AND BranchCode='"+Login.assignedBranch+"' AND MachineUsed='"+Environment.MachineName+"') ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            Dictionary<string, List<DataRow>> receipts = new Dictionary<string, List<DataRow>>();
            //var rowz = Database.getMultipleQuery("POSInfoDetails","BranchCode='"+Login.assignedBranch+"' AND MachineUsed='"+Environment.MachineName+"'");
            Dictionary<string, List<DataRow>> load = new Dictionary<string, List<DataRow>>();
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

                details += Classes.ReceiptSetup.doHeaderDetailsX(referenceNo, " ", " ", " ", " ", " "," "," ");
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

        

        private void Practice_Load(object sender, EventArgs e)
        {
            receiptWithNoDiscount();

            //foreach (DataRow row in table.Rows)
            //{
               
            //}

            //details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            //if (Convert.ToDouble(totalPerItemDiscount) > 0)
            //{
            //    details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", totalPerItemDiscount.ToString()) + Environment.NewLine;
            //}
            //details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total.ToString()) + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;


            //if (paytype == "Credit")
            //{
            //    details += HelperFunction.PrintLeftRigthText("TENDERED:", amounttender) + Environment.NewLine + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            //}
            //details += HelperFunction.PrintLeftRigthText("TENDERED:", amounttender) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("CHANGE  :", amountchange) + Environment.NewLine + Environment.NewLine;
            //bool iszerorated = false;
            //if (iszerorated == true)
            //{
            //    details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            //}
            //else
            //{

            //    details += HelperFunction.PrintLeftRigthText("VATable Sales", TotalVatableSalesSum) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("VAT Amount", TotalVatSaleSum) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", TotalVatExemptSum) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            //}
            ////if (paytype == "Credit")
            ////{
            ////    string cardno = "", cardtype = "", cardrefno = "";
            ////    details += HelperFunction.createDottedLine() + Environment.NewLine;
            ////    details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
            ////    details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
            ////    details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
            ////    details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
            ////    details += HelperFunction.createDottedLine() + Environment.NewLine;
            ////}

            //string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            ////details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += HelperFunction.LastPagePaper();

       
        }


        void receiptWithNoDiscount()
        {
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            double totalPerItemDiscount = 0.0, total = 0.0;
            String details = "";
            string filepath = "C:\\ProgramFliesTest\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
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
               ", c.DiscountAmount" +
               ", c.DiscountType" +
               ", c.DiscountPercentage" +
               ", a.DateOrder" +
                " FROM BatchSalesDetails a INNER JOIN BatchSalesSummary b ON a.ReferenceNo=b.ReferenceNo " +
                "LEFT OUTER JOIN SalesDiscount c ON b.BranchCode=c.BranchCode and b.ReferenceNo=c.OrderNo " +
                "AND a.BranchCode=b.BranchCode " +
                "AND a.MachineUsed=b.MachineUsed " +
                "WHERE CAST(a.DateOrder as date)='07/16/2022' ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);

            //var rowz = Database.getMultipleQuery("POSInfoDetails","BranchCode='"+Login.assignedBranch+"' AND MachineUsed='"+Environment.MachineName+"'");
            Dictionary<string, List<DataRow>> receipts = new Dictionary<string, List<DataRow>>();
            string dateOrder = "",discounttype="", discountAmount = "", discountPercentage="";
            double totalvatitems = 0.0;
            foreach (DataRow row in table.Rows)
            {
                dateOrder = row["DateOrder"].ToString();
                discounttype = row["DiscountType"].ToString();
                discountAmount = row["DiscountAmount"].ToString();
                discountPercentage = row["DiscountPercentage"].ToString();
                string referenceNo = row["ReferenceNo"].ToString();
                if (!receipts.ContainsKey(referenceNo))
                    receipts[referenceNo] = new List<DataRow>();
                receipts[referenceNo].Add(row);
            }

            foreach (KeyValuePair<string, List<DataRow>> kvp in receipts) //List<DataRow> receipt
            {
                List<DataRow> receipt = kvp.Value;
                string referenceNo = kvp.Key;
                //string datebuy = kvp.Key;
                details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
                details += Classes.ReceiptSetup.doTitle("SALES INVOICE");

                details += Classes.ReceiptSetup.doHeaderDetailsX(referenceNo, " ", " ", " ", " ", " ", dateOrder,"");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                foreach (DataRow row in receipt) //mga items sa resibo
                {
                    CashierTransNo = row["CashierTransNo"].ToString();
                    TotalVatableSalesSum = row["TotalVatableSale"].ToString();
                    TotalVatSaleSum = row["TotalVATSale"].ToString();
                    TotalVatExemptSum = row["TotalVATExemptSale"].ToString();

                    amounttender = row["AmountTendered"].ToString();
                    amountchange = row["AmountChange"].ToString();
                    paytype = row["PaymentType"].ToString();
                    totalPerItemDiscount += Convert.ToDouble(row["DiscountTotal"].ToString());
                    //totalPerItemDiscount += Convert.ToDouble(row["DiscountAmount"].ToString());
                    total += Convert.ToDouble(row["TotalAmount"].ToString());
                    string addV = "";
                    
                    if (Convert.ToBoolean(row["isVat"].ToString()) == true)
                    {
                        addV = "V";
                        totalvatitems += Convert.ToDouble(row["TotalVATSale"].ToString());
                    }
                    else
                    {
                        addV = "";
                    }
                    string addD = "";
                    if (Convert.ToDouble(Convert.ToDouble(row["DiscountTotal"].ToString())) > 0)
                    //if (Convert.ToDouble(Convert.ToDouble(row["DiscountAmount"].ToString())) > 0)
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
                        //details += HelperFunction.PrintLeftRigthText(addD, "(" + row["DiscountAmount"].ToString() + ")") + Environment.NewLine;
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
                //----------------------------------------------------------------------------------------------------------------
                double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
                string netofvatindinonscitems = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInNonDiscountedItems('" + Login.assignedBranch + "','" + referenceNo+ "')");
                string netofvatindiscitems = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInDiscountedItems('" + Login.assignedBranch + "','" + referenceNo + "')");

                netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
                if (discountAmount !="")
                {
                    //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                    if (discounttype == "SENIOR")
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
                        lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountPercentage), 2);
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
                    else if (discounttype == "PWD")
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
                        lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountPercentage), 2);
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
                    else if (discounttype == "REGULAR")
                    {
                        details += HelperFunction.createDottedLine() + Environment.NewLine;
                        details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                         //details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                        details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                        details += HelperFunction.createDottedLine() + Environment.NewLine;
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////

                        lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                        netofvat = Math.Round(totalvatitems / 1.12, 2);
                        lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountPercentage), 2);
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
                //----------------------------------------------------------------------------------------------------------------
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
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            XtraMessageBox.Show(gridView1.GetGroupRowDisplayText(gridView1.FocusedRowHandle).ToString());
            
        }
    }
}