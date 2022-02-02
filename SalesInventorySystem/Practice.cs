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

                details += Classes.ReceiptSetup.doHeaderDetailsX(referenceNo, " ", " ", " ", " ", " ");
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
            //
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
                " FROM BatchSalesDetails a LEFT OUTER JOIN BatchSalesSummary b ON a.ReferenceNo=b.ReferenceNo " +
                "AND a.BranchCode=b.BranchCode " +
                "AND a.MachineUsed=b.MachineUsed " +
                "WHERE CAST(a.DateOrder as date)='10/30/2019' " +
                "AND a.ReferenceNo IN ('000000000000005331','000000000000005334') ", con);
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
         
                details += Classes.ReceiptSetup.doHeaderDetailsX(referenceNo, " ", " ", " ", " ", " ");
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