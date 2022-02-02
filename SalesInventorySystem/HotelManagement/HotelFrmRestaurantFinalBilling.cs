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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmRestaurantFinalBilling : DevExpress.XtraEditors.XtraForm
    {
        public static bool isClosed = false,ismerge=false;
        string amount = "",num="", paymenttype="";
        public HotelFrmRestaurantFinalBilling()
        {
            InitializeComponent();
        }

        private void HotelFrmRestaurantFinalBilling_Load(object sender, EventArgs e)
        {
            lblTransactionID.Text = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' and DateOpen='" + DateTime.Now.ToShortDateString() + "' ", "AccountCode");
            int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
            txtOrderNo.Text = refnumber.ToString();
            populateTables();
        }
       
        void populateTables()
        {
            //Database.displayCheckedListBoxItemsDevEx("SELECT TableNo FROM OrderSummary where isFloat=1 and Status='Pending' and OrderType='DINE-IN'", "TableNo", checkedListBoxControl1,Database.getCustomizeConnection());
            Database.displayCheckedListBoxItemsDevEx("SELECT TableNo FROM OrderSummary where isFloat=1 and Status='Pending' and OrderType='DINE-IN'", "TableNo", checkedListBoxControl1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }
        void display()
        {
            lblTotalItems.Text = dataGridView1.RowCount.ToString();
            lblTotalAmount.Text = HelperFunction.numericFormat(computeTotalAmount());//.ToString();
            lblvat.Text = HelperFunction.numericFormat(computeVAT());//.ToString();
            lblvatexemptsale.Text = HelperFunction.numericFormat(computeVATExemptSale());//.ToString();
            lblvatsale.Text = HelperFunction.numericFormat(computeVATableSale());//.ToString();

            txtamountpayable.Text = lblTotalAmount.Text;
        }
        Double computeTotalAmount()
        {
            double total = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                amount = dataGridView1.Rows[i].Cells["Amount"].Value.ToString();
                total += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
            }
            return total;
        }
        Double computeVATableSale()
        {
            double vatexemptsale = 0.0, finalvalue = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
                {
                    vatexemptsale += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            finalvalue = vatexemptsale / 1.12;
            return finalvalue;
        }

        Double computeVATExemptSale()
        {
            double vatexemptsale = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "False")
                {
                    vatexemptsale += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            return vatexemptsale;
        }

        Double computeVAT()
        {
            double vatexemptsale = 0.0;
            double vat = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
               // if (dataGridView1.GetRowCellValue(i,"isVat").ToString() == "True")

                {
                    vatexemptsale += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            vat = Math.Round((vatexemptsale / 1.12) * 0.12, 2);
            return vat;
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            SqlDataAdapter adapter;
            DataTable table = new DataTable();
          
            int ctr = 0;

            if (checkedListBoxControl1.CheckedItemsCount == 0)
            {
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
            }
            //Database.ExecuteLocalQuery("TRUNCATE TABLE aaplock1 ", Database.getCustomizeConnection());
            Database.ExecuteQuery("TRUNCATE TABLE aaplock1 ");

            foreach (Object item in checkedListBoxControl1.CheckedItems)
            {
                Database.ExecuteQuery("INSERT INTO aaplock1 VALUES('" + item + "')");
                //Database.ExecuteLocalQuery("INSERT INTO aaplock1 VALUES('" + item + "')",Database.getCustomizeConnection());
                //string refno = Database.getSingleQuery("OrderSummary", "TableNo='" + item + "' and OrderType='DINE-IN' and isFloat=1 and Status='Pending'", "ReferenceNo",Database.getCustomizeConnection());
                string refno = Database.getSingleQuery("OrderSummary", "TableNo='" + item + "' and OrderType='DINE-IN' and isFloat=1 and Status='Pending'", "ReferenceNo");
                ctr = ctr + 1;
                SqlConnection con = Database.getConnection(); //Database.getCustomizeConnection();
                if (ctr == 1) //if one table only
                {
                    ismerge = false;
                    txtOrderNo.Text = refno;
                    con.Open();
                    string query = "SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM OrderDetailsRes WHERE ReferenceNo='" + refno + "' ";
                    SqlCommand com = new SqlCommand(query, con);
                    adapter = new SqlDataAdapter(com);
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    string query1 = "SELECT Description AS Particulars ,SUM(QtySold) AS Qty,FORMAT(SUM(TotalAmount),'N', 'en-us') AS Amount FROM OrderDetailsRes WHERE ReferenceNo='" + refno + "' GROUP BY Description";
                    SqlCommand com11 = new SqlCommand(query1, con);
                    SqlDataAdapter adapter11 = new SqlDataAdapter(com11);
                    DataTable table11 = new DataTable();
                    adapter11.Fill(table11);
                    dataGridView2.DataSource = table11;

                   
                }
                else //merge table only
                {
                    ismerge = true;
                    int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
                    txtOrderNo.Text = refnumber.ToString();
                    con.Open();
                    string query = "SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM OrderDetailsRes WHERE ReferenceNo='" + refno + "' ";
                    SqlCommand com = new SqlCommand(query, con);
                    adapter = new SqlDataAdapter(com);
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    
                    string query1 = "SELECT Description AS Particulars ,SUM(QtySold) AS Qty,FORMAT(SUM(TotalAmount),'N', 'en-us') AS Amount FROM OrderDetailsRes WHERE ReferenceNo='" + refno + "' GROUP BY Description";
                    SqlCommand com11 = new SqlCommand(query1, con);
                    SqlDataAdapter adapter11 = new SqlDataAdapter(com11);
                    DataTable table11 = new DataTable();
                    adapter11.Fill(table11);
                    dataGridView2.DataSource = table11;
                }
                

            }
            display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            num = num + "1";
            txtamounttender.Text = num;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            num = num + "2";
            txtamounttender.Text = num;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            num = num + "3";
            txtamounttender.Text = num;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            num = num + "4";
            txtamounttender.Text = num;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            num = num + "5";
            txtamounttender.Text = num;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            num = num + "6";
            txtamounttender.Text = num;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            num = num + "7";
            txtamounttender.Text = num;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            num = num + "8";
            txtamounttender.Text = num;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            num = num + "9";
            txtamounttender.Text = num;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            num = num + ".";
            txtamounttender.Text = num;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            num = num + "0";
            txtamounttender.Text = num;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Payment();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            num = "";
            txtamounttender.Text = "";
        }

        private void HotelFrmRestaurantFinalBilling_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosed = true;
        }
        private DataGridView CopyDataGridView(DataGridView dgv_org)
        {
            DataGridView dgv_copy = new DataGridView();
            try
            {
                if (dgv_copy.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    {
                        dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < dgv_org.Rows.Count; i++)
                {
                    row = (DataGridViewRow)dgv_org.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_copy.Rows.Add(row);
                }
                dgv_copy.AllowUserToAddRows = false;
                dgv_copy.Refresh();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
            }
            return dgv_copy;
        }
        private void button18_Click(object sender, EventArgs e)
        {
            
            string tableno="";
            //List<string> tableno;
            foreach(DevExpress.XtraEditors.Controls.CheckedListBoxItem items in checkedListBoxControl1.CheckedItems)
            {
                tableno += "["+items.Value.ToString()+"] ";
            }
            //Database.ExecuteLocalQuery("Truncate Table TempTable", Database.getCustomizeConnection());
            Database.ExecuteQuery("Truncate Table TempTable");

            for (int i=0;i<=dataGridView1.RowCount-1;i++)
            {
                Database.ExecuteQuery("INSERT INTO TempTable VALUES('" + dataGridView1.Rows[i].Cells["Particulars"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Qty"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Amount"].Value.ToString() + "')");
            }
            SqlConnection con = Database.getConnection();//Database.getCustomizeConnection();
            con.Open();
            string query = "SELECT Description as Particulars,SUM(Qty) As Qty,SUM(Amount) As Amount FROM TempTable GROUP BY Description";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;

            Printing printit = new Printing();
            string vatablesales = String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text));
            string vatexemptsales = String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text));
            string vat = String.Format("{0:n2}", Convert.ToDouble(lblvat.Text));
            printit.printReceiptBilling(lblTransactionID.Text, txtOrderNo.Text, lblTotalAmount.Text, vatablesales, vatexemptsales, vat, "0", "0", dataGridView2,tableno);
            //printit.printReceiptConsolidated(lbltranscode.Text, lblorderno.Text, txtamountpayable.Text, vatablesales, vatexemptsales, vat, amounttender, change, PointOfSale.mygridview);

        }

        private void txtamounttender_EditValueChanged(object sender, EventArgs e)
        {
            double payable = 0.0;
            if (txtamounttender.Text == "")
            {
                return;
            }
            else
            {
                payable = Convert.ToDouble(txtamounttender.Text) - Convert.ToDouble(txtamountpayable.Text);
                txtamountchange.Text = Math.Round(payable, 2).ToString();
            }
        }

        private void chckmerge_CheckedChanged(object sender, EventArgs e)
        {
            if(chckmerge.Checked==true)
            {
                int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
                txtOrderNo.Text = refnumber.ToString();
            }
            else
            {
                txtOrderNo.Text = "";
            }
            
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            try
            {
                txtseniorcontrolno.Focus();
                double discountAmount = 0.0, cleanamount = 0.0, newtotalamount = 0.0;
                //totalamount = Convert.ToDouble(lblTotalAmount);
                discountAmount = Convert.ToDouble(txtamountpayable.Text) * 0.05;
                cleanamount = Convert.ToDouble(txtamountpayable.Text) - discountAmount;
                AddDiscount adis = new AddDiscount();
                adis.txtamnttobediscount.Text = txtamountpayable.Text;
                adis.txtdiscountamount.Text = discountAmount.ToString();
                adis.ShowDialog(this);
                if (AddDiscount.isdone == true)
                {
                    txtdiscount.Text = AddDiscount.discountamount;
                    newtotalamount = Convert.ToDouble(txtamountpayable.Text) - Convert.ToDouble(txtdiscount.Text);
                    txtamountpayable.Text = newtotalamount.ToString();
                    txtseniorcontrolno.Text = AddDiscount.controlno;
                    txtseniorname.Text = AddDiscount.name;
                    //display();
                    AddDiscount.isdone = false;
                    adis.Dispose();
                    txtamounttender.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Payment();
        }

      
        void Payment()
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
                    if (radioButton1.Checked == true)
                    {
                        paymenttype = "Cash";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        paymenttype = "CreditCard";
                        // Database.ExecuteQuery("INSERT INTO TransactionCreditCard VALUES('" + lblTransactionID.Text + "','" + txtOrderNo.Text + "','" + lblreferenceno.Text + "','" + Login.assignedBranch + "','" + POSPaymentDetails.creditcardnum + "','" + POSPaymentDetails.creditcardname + "','" + POSPaymentDetails.creditcardbankname + "','" + POSPaymentDetails.creditcardcode + "','" + POSPaymentDetails.creditcardmerchant + "','','" + PointOfSale.custcode + "','" + txtamountpayable.Text + "','" + DateTime.Now.ToString() + "','" + Login.Fullname + "')");
                    }
                    else if (radioButton3.Checked == true)
                    {
                        paymenttype = "Cheque";
                        // Database.ExecuteQuery("INSERT INTO TransactionCheque VALUES('" + lblTransactionID.Text + "','" + txtOrderNo.Text + "','" + lblreferenceno.Text + "','" + Login.assignedBranch + "','" + POSPaymentDetails.chequenumber + "','" + POSPaymentDetails.chequename + "','" + POSPaymentDetails.chequebankname + "','" + PointOfSale.custcode + "','" + txtamountpayable.Text + "','" + DateTime.Now.ToString() + "','" + Login.Fullname + "')");
                    }
                    //printReceipt(string transcode,string ordercode,string total,string vatablesale,string vatexemptsale,string vat,string cash,string change,DataGridView gridview)
                    string vatablesales = String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text));
                    string vatexemptsales = String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text));
                    string vat = String.Format("{0:n2}", Convert.ToDouble(lblvat.Text));
                    string amounttender = String.Format("{0:n2}", Convert.ToDouble(txtamounttender.Text));
                    string change = String.Format("{0:n2}", Convert.ToDouble(txtamountchange.Text));
                    string amountpayable = String.Format("{0:n2}", Convert.ToDouble(txtamountpayable.Text));

                    //printit.printReceipt(lblTransactionID.Text, txtOrderNo.Text, amountpayable,"0", vatablesales, vatexemptsales, vat, amounttender, change, dataGridView1,false);
                    //printit.printReceiptConsolidated(lblTransactionID.Text,lblTransactionID.Text, txtOrderNo.Text, amountpayable, "0", vatablesales, vatexemptsales, vat, amounttender, change, dataGridView1,false);

                    ConfirmPayment();
                    //Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                    POS.POSHistoryCaption poshiscap = new POS.POSHistoryCaption();
                    poshiscap.txtamounttenderedcap.Text = txtamounttender.Text;
                    poshiscap.txtamountchangecap.Text = txtamountchange.Text;
                    poshiscap.ShowDialog(this);
                    POS.POSHistoryCaption.transactiondone = false;
                    isClosed = true;
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void ConfirmPayment()
        {
            SqlConnection con = Database.getConnection();//Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "sp_saveTransaction";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", "888");
                com.Parameters.AddWithValue("@parmrefno", txtOrderNo.Text.Trim());
                com.Parameters.AddWithValue("@parmreferenceno", lblTransactionID.Text.Trim());
                com.Parameters.AddWithValue("@parmtransno", lblTransactionID.Text.Trim());
                com.Parameters.AddWithValue("@parmamountpayable", txtamountpayable.Text.Trim());
                com.Parameters.AddWithValue("@parmamounttender", txtamounttender.Text.Trim());
                com.Parameters.AddWithValue("@parmamountchange", txtamountchange.Text.Trim());
                com.Parameters.AddWithValue("@parminvoice", txtinvoiceno.Text.Trim());
                com.Parameters.AddWithValue("@parmpaymenttype", paymenttype);
                com.Parameters.AddWithValue("@parmcustid", "");
                com.Parameters.AddWithValue("@transby", Login.Fullname);
                com.Parameters.AddWithValue("@parmvatsale", lblvatsale.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", lblvatexemptsale.Text);
                com.Parameters.AddWithValue("@parmvat", lblvat.Text);
                com.Parameters.AddWithValue("@parmdiscount", txtdiscount.Text);
                com.Parameters.AddWithValue("@parmseniorcontrolno", txtseniorcontrolno.Text);
                com.Parameters.AddWithValue("@parmseniorname", txtseniorname.Text);
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
            //this.Dispose();
            //this.Close();
            //Database.ExecuteQuery("UPDATE BatchSalesSummary set isFloat='0', AmountTendered='" + spinEdit1.Text + "',AmountChange='" + textEdit3.Text + "',Invoice='" + txtinvoiceno.Text + "',PaymentType='" + paymenttype + "' WHERE ReferenceNo='" + lblrefno.Text + "' ");
            //Database.ExecuteQuery("UPDATE BatchSalesDetails set isConfirmed='1',Status='OK' WHERE ReferenceNo='" + lblrefno.Text + "' ");
            //this.Close();
        }
    }
}