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

namespace SalesInventorySystem.POS
{
    public partial class POSplitBillFinal : DevExpress.XtraEditors.XtraForm
    {
        double total = 0.0, totalcash = 0.0, totalcredit = 0.0;
        bool iscash = false, iscredit = false;
        DataTable table;
        public static string orderno = "", cashiertransno = "", transno = "";
        public static bool transactiondone = false, isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false, isOnetimeDiscount = false;
        public static bool isdone = false;
        public static string totalCashSales = "", totalCreditSales = "";
        public POSplitBillFinal()
        {
            InitializeComponent();
        }

        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Amount"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }

        private void gridControl1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to confirm this Payment?", "Confirm Payment");
            double amountchange = 0.0;
            totalcash = 0.0; totalcredit = 0.0; total = 0.0;
            if (confirm)
            {
                
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "PaymentTypeName").ToString() == "CASH")
                    {
                        totalcash += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());
                        iscash = true;
                    }
                    if (gridView1.GetRowCellValue(i, "PaymentTypeName").ToString() != "CASH")
                    {
                        totalcredit += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());
                        iscredit = true;
                    }
                    total += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());
                }
                txttotalcash.Text = totalcash.ToString();
                txttotalcredit.Text = totalcredit.ToString();
                txtamounttender.Text = total.ToString();
                amountchange = total - Convert.ToDouble(txtamountpayable.Text);
                txtamountchange.Text = amountchange.ToString();
                btnpay.Enabled = true;
            }
           else
            {
                return;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            btnpay.Enabled = false;
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        void loadRepositoryItem()
        {
            Database.displayRepositorySearchlookupEdit("SELECT PaymentTypeID,PaymentTypeName FROM dbo.PaymentType", reppaymenttype, "PaymentTypeName", "PaymentTypeName");
            //Database.displayRepositorySearchlookupEdit("SELECT Description FROM CHartOfAccounts WHERE AccountCode like '60%'", reptypeofexpense, "Description", "Description");
            //gridView2.BestFitColumns();
            //gridView3.BestFitColumns();
        }

        private void POSplitBillFinal_Load(object sender, EventArgs e)
        {
            btnpay.Enabled = false;
            loadRepositoryItem();
            table = new DataTable();
            table.Columns.Add("PaymentTypeName");
            table.Columns.Add("ReferenceNo");
            table.Columns.Add("Amount");
            gridControl1.DataSource = table;
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if(e.Column.FieldName == "PaymentTypeName")
                e.RepositoryItem = reppaymenttype;
            if (e.Column.FieldName == "ReferenceNo")
                e.RepositoryItem = repreferenceno;
            if (e.Column.FieldName == "Amount")
                e.RepositoryItem = repamount;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                bool isEmpty = false;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "PaymentTypeName").ToString()) || String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "ReferenceNo").ToString()))
                    {
                        isEmpty = true;
                        break;
                    }
                }
                if (gridView1.RowCount == 0)
                {
                    XtraMessageBox.Show("No Expense Details Entry");
                    return;
                }
                if (isEmpty)
                {
                    XtraMessageBox.Show("Some Fields are Empty..");
                    return;
                }
                else
                {
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        if (iscredit == true && gridView1.GetRowCellValue(i, "PaymentTypeName").ToString() != "CASH")
                        {

                            //invno = txtinvoiceno.Text;
                            //update as of 04142020
                            int transactionno = Convert.ToInt32(transno)+i;
                            string newtrans = HelperFunction.sequencePadding1(transactionno.ToString(), 10);
                            Database.ExecuteQuery("INSERT INTO dbo.POSCreditCardTransactions VALUES('" + newtrans + "','" + orderno + "','" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "','" + gridView1.GetRowCellValue(i, "PaymentTypeName").ToString() + "','" + gridView1.GetRowCellValue(i, "ReferenceNo").ToString() + "','MasterCard',' ','" + gridView1.GetRowCellValue(i, "PaymentTypeName").ToString() + "','" + gridView1.GetRowCellValue(i, "PaymentTypeName").ToString() + "','" + gridView1.GetRowCellValue(i, "ReferenceNo").ToString() + "','" + txttotalcredit.Text + "','0','" + DateTime.Now.ToString() + "',' ','" + Login.Fullname + "','" + GlobalVariables.computerName + "','"+cashiertransno+"')");
                            //update as of 04142020
                        }
                    }
                    totalCashSales = txttotalcash.Text;
                    totalCreditSales = txttotalcredit.Text;
                    //Payment("MixPayment");
                    XtraMessageBox.Show("Successfully Added!");
                    isdone = true;
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void Payment(string paytype)
        {
            try
            {
                Printing printit = new Printing();
                if (txtamounttender.Text == "")
                {
                    XtraMessageBox.Show("Please Input Tender Amount!.");
                    txtamounttender.Focus();
                }
                else if (Convert.ToDouble(txtamounttender.Text) < Convert.ToDouble(txtamountpayable.Text))
                {
                    XtraMessageBox.Show("Tender Amount must not less than Amount Payable");
                }
                else
                {
                    
                    //printReceipt(string transcode,string ordercode,string total,string vatablesale,string vatexemptsale,string vat,string cash,string change,DataGridView gridview)
                    string vatablesales = String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text));
                    string vatexemptsales = String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text));
                    string vat = String.Format("{0:n2}", Convert.ToDouble(lblvat.Text));
                    string amounttender = String.Format("{0:n2}", Convert.ToDouble(txtamounttender.Text));
                    string change = String.Format("{0:n2}", Convert.ToDouble(txtamountchange.Text));
                    string amountpayable = String.Format("{0:n2}", Convert.ToDouble(txtamountpayable.Text));

                    //printit.printReceipt(lblTransactionID.Text, txtOrderNo.Text, amountpayable,"0", vatablesales, vatexemptsales, vat, amounttender, change, dataGridView1,false);
                    //printit.printReceiptConsolidated(lblTransactionID.Text,lblTransactionID.Text, txtOrderNo.Text, amountpayable, "0", vatablesales, vatexemptsales, vat, amounttender, change, dataGridView1,false);

                    //ConfirmPayment();
                    execute(paytype);

                    //Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                    //POS.POSHistoryCaption poshiscap = new POS.POSHistoryCaption();
                    //poshiscap.txtamounttenderedcap.Text = txtamounttender.Text;
                    //poshiscap.txtamountchangecap.Text = txtamountchange.Text;
                    //poshiscap.ShowDialog(this);
                    //POS.POSHistoryCaption.transactiondone = false;
                    isdone = true;
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }


        void spSaveTransaction(string discounttype, string invoiceno,string paymenttype)
        {
            bool isRetail = Database.checkifExist("Select PosType FROM dbo.POSType WHERE PosType=1");
            bool OneTimeDisc = false, ZeroRated = false;
            if (isRetail)
            {
                OneTimeDisc = PointOfSale.isOnetimeDiscount;
                ZeroRated = PointOfSale.iszeroratedsale;
            }
            else
            {
                OneTimeDisc = POSRestoDineInBilling.isOnetimeDiscount;
                ZeroRated = POSRestoDineInBilling.iszeroratedsale;
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_saveTransaction";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmrefno", orderno.Trim());
                com.Parameters.AddWithValue("@parmcashiertransno", cashiertransno.Trim());
                com.Parameters.AddWithValue("@parmreferenceno", "");
                com.Parameters.AddWithValue("@parmtransno", transno.Trim());
                com.Parameters.AddWithValue("@parmamountpayable", txtamountpayable.Text.Trim());
                com.Parameters.AddWithValue("@parmamounttender", txttotalcash.Text.Trim()); //total cash ang e store sa batchsalessummary
                //com.Parameters.AddWithValue("@parmamounttender", txtamounttender.Text.Trim());
                com.Parameters.AddWithValue("@parmamountchange", txtamountchange.Text.Trim());
                com.Parameters.AddWithValue("@parminvoice", invoiceno);
                com.Parameters.AddWithValue("@parmpaymenttype", paymenttype);
                com.Parameters.AddWithValue("@parmcustid", ""); //PointOfSale.custcode
                com.Parameters.AddWithValue("@transby", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmvatsale", lblvatsale.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", lblvatexemptsale.Text);
                com.Parameters.AddWithValue("@parmvat", lblvat.Text);
                com.Parameters.AddWithValue("@parmdiscount", txtdiscount.Text);
                //com.Parameters.AddWithValue("@parmseniorcontrolno", senioridno);//txtseniorcontrolno.Text);
                //com.Parameters.AddWithValue("@parmseniorname", seniorname);//txtseniorname.Text);
                com.Parameters.AddWithValue("@parmonetimediscount", OneTimeDisc);//isOnetimeDiscount);
                //com.Parameters.AddWithValue("@parmseniordiscountamount", seniordiscountamount);//seniordiscountAmount);
                com.Parameters.AddWithValue("@parmdiscounttype", discounttype);
                //com.Parameters.AddWithValue("@parmpwdidno", pwdIDNo);
                //com.Parameters.AddWithValue("@parmpwdname", pwdName);
                //com.Parameters.AddWithValue("@parmpwddiscountamount", pwdDiscountAmount);
                com.Parameters.AddWithValue("@parmdiscidno", "");
                com.Parameters.AddWithValue("@parmdiscname", "");
                com.Parameters.AddWithValue("@parmdiscamount", "");

                com.Parameters.AddWithValue("@parmiszeroratedsale", ZeroRated);
                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
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

        private void execute(string paymenttype)
        {
            string discounttype = "";
            if (isSeniorDiscount == true)
            {
                discounttype = "SENIOR";
            }
            else if (isPwdDiscount == true)
            {
                discounttype = "PWD";
            }
            else if (isOthersDiscount == true)
            {
                discounttype = "REGULAR";
            }
            spSaveTransaction(discounttype, txtinvoiceno.Text, paymenttype);
        }

        private void txtamounttender_EditValueChanged(object sender, EventArgs e)
        {
           
            
        }
    }
}