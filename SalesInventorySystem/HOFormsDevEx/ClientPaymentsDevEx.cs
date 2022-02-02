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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ClientPaymentsDevEx : DevExpress.XtraEditors.XtraForm
    {
        //double balance = 0.0, amountpaid = 0.0, discount = 0.0, totalmoney = 0.0, ewt = 0.0, vatex = 0.0, vatable = 0.0, totsales = 0.0, ewtamount = 0.0, totalbalance = 0.0, totalbalance2 = 0.0;
        bool isglobalSales = false, isglobalCharge = false;
        double advncepymentval = 0.0,totalbalance=0.0,totalamountpaid=0.0;
        string   paymenttype = "",custkey="",refno;
        int lasttransseqno;
        public ClientPaymentsDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Database.display("SELECT TransactionDate,CustomerID,Reference,Amount,AmountPaid,Balance,PaymentStatus FROM TransactionChargeSales WHERE CAST(TransactionDate as date) between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "' and CustomerID='" + groupControl1.Text + "' and Balance=0", gridControl1, gridView9);
        }

        private void ClientPaymentsDevEx_Load(object sender, EventArgs e)
        {
            txtcustid.Text = ClientAccountsDevEx.custid;
            custkey = ClientAccountsDevEx.custkey;
            txtcustname.Text = ClientAccountsDevEx.custname;
            groupControl1.Text = ClientAccountsDevEx.custid;
            populateRepositorySearchLookUp();
            populateCOA();
            display();
            //Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView2, "SequenceNo");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "InvoiceAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "Balance");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "AmountPaid");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "AdvancePayment");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "EWTAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "DiscountAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView2, "OffsetAmount");
        }

        void display()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl2.BeginUpdate();
            try
            {
                string sp = "splist_ARAccounts";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmcustkey", ClientAccountsDevEx.custkey);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView2.Columns.Clear();
                gridControl2.DataSource = null;
                adapter.Fill(table);
                gridControl2.DataSource = table;
                gridView2.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControl2.EndUpdate();
                con.Close();
            }
        }

        void radEvent()
        {
            if (radioButton1.Checked==true) //PAYMENT
            {
                radioButton1.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = false;
                groupBoxadvancepayment.Visible = false;
                groupCreditCardDetails.Visible = false;
            }
            else if (radioButton2.Checked == true) //PAYMENT
            {
                radioButton2.Checked = true;
                panelOnline.Visible = false;
                groupCheque.Visible = true;
                groupBoxadvancepayment.Visible = false;
                groupCreditCardDetails.Visible = false;
            }
            else if (radioButton3.Checked == true) //PAYMENT
            {
                radioButton3.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = true;
                groupBoxadvancepayment.Visible = false;
                groupCreditCardDetails.Visible = false;
            }
            else if (radioButton4.Checked == true) //PAYMENT
            {
                
                radioButton4.Checked = true;
                groupCheque.Visible = false;
                groupBoxadvancepayment.Visible = true;

                groupCreditCardDetails.Visible = false;
                panelOnline.Visible = false;
                txtacctbalance.Text = Math.Round(Convert.ToDouble(Database.getSingleQuery("ClientSavingsAccounts", "AccountID='" + txtcustid.Text + "'", "AccountBalance")), 2).ToString();
            }
            else if (radCreditCard.Checked == true) //PAYMENT
            {
                radCreditCard.Checked = true;
                groupCheque.Visible = false;
                groupBoxadvancepayment.Visible = false;
                groupCreditCardDetails.Visible = true;
                panelOnline.Visible = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F1) //PAYMENT
            {
                radioButton1.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = false;
                groupBoxadvancepayment.Visible = false;
            }
            else if (keyData == Keys.F2) //PAYMENT
            {
                radioButton2.Checked = true;
                panelOnline.Visible = false;
                groupCheque.Visible = true;
                groupBoxadvancepayment.Visible = false;
            }
            else if (keyData == Keys.F3) //PAYMENT
            {
                radioButton3.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = true;
                groupBoxadvancepayment.Visible = false;
            }
           else if (keyData == Keys.F4) //PAYMENT
            {
               
               radioButton4.Checked = true;
               groupCheque.Visible = false;
               groupBoxadvancepayment.Visible = true;
               panelOnline.Visible = false;
               txtacctbalance.Text = Math.Round(Convert.ToDouble(Database.getSingleQuery("ClientSavingsAccounts", "AccountID='" + txtcustid.Text + "'", "AccountBalance")),2).ToString();
           }else if (keyData == Keys.F5) //PAYMENT
            {
               
               radCreditCard.Checked = true;
               groupCheque.Visible = false;
               groupBoxadvancepayment.Visible = false;
               groupCreditCardDetails.Visible = true;
               panelOnline.Visible = false;
           }
            return functionReturnValue;
        }
        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtdebitglcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtcreditglcode, "AccountCode", "AccountCode");
        }

      
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //double balance = 0.0;
            //balance = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Balance").ToString());
            ////discount = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Discount").ToString());
            //////amountpaid = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid").ToString());
            ////vatex = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "VatExemptAmount").ToString());
            ////vatable = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "VatableAmount").ToString());
            ////ewt = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EWT").ToString());

            ////totsales = vatable + vatex;
            ////totalmoney = discount + amountpaid;
            ////ewtamount = ewt * totsales;
            ////totalbalance += balance;

            //double invocie = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceAmount"));
            //if (edit.Checked.ToString() == "True")
            //if(repositoryItemCheckEditStatus.ValueChecked.ToString())
            //{
            //    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceAmount").ToString());
            //    edit.Checked = true;
            //}
            //else if (edit.Checked.ToString() == "False")
            //{
            //    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid", "0");
            //    edit.Checked = false;
            //}
            if (e.Value == "True")
            {
                //gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid", gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance").ToString());
                HOFormsDevEx.ClientAddPaymentDevEx asdds = new ClientAddPaymentDevEx();
                asdds.txtinvoiceno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString();
                asdds.txtinvoicedate.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString();
                asdds.txtactualcost.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceAmount").ToString();
                asdds.txtbalance.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Balance").ToString();
                asdds.groupControl1.Text = txtcustid.Text + "-" + txtcustname.Text;
                asdds.ShowDialog(this);
                if (HOFormsDevEx.ClientAddPaymentDevEx.isdone == true)
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid", HOFormsDevEx.ClientAddPaymentDevEx.amountpaid);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "DiscountAmount", HOFormsDevEx.ClientAddPaymentDevEx.discount);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTAmount", HOFormsDevEx.ClientAddPaymentDevEx.ewt);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetAmount", HOFormsDevEx.ClientAddPaymentDevEx.offset);
                    HOFormsDevEx.ClientAddPaymentDevEx.isdone = false;
                    asdds.Dispose();
                }
            }
            else if (e.Value == "False")
            {
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid", "0");
            }
           
            //if (e.Column.FieldName == "EWT")
            //{
            //    ewtamount= Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EWT").ToString())* Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "VatExemptAmount").ToString())+ Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "VatableAmount").ToString());
            //    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTAmount", ewtamount.ToString());
            //}
            //if (e.Column.FieldName == "EWTAmount")
            //{
            //    totalbalance = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Balance").ToString()) - Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EWT").ToString()); 
            //    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Balance", totalbalance.ToString());
            //}

           
            //if (Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ChargeAmountPaid").ToString()) > Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ChargeBalance").ToString()))
            //{
            //    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "ChargeAmountPaid", "0");
            //    XtraMessageBox.Show("ChargeAmountPaid must not greater than Balance amount");
            //    return;
            //}

            ////if (totalmoney > balance)
            ////{
            ////    XtraMessageBox.Show("AmountPaid + Discount must not greater than Balance amount");
            ////    return;
            ////}

            double totalamount = 0.0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True")
                {
                    totalamount += Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString());
                    //totalchargeamount += Convert.ToDouble(gridView2.GetRowCellValue(i, "ChargeAmountPaid").ToString());
                }
            }
            //overalltotal = totalamount + totalchargeamount;
            txtamounttopay.Text = totalamount.ToString();//overalltotal.ToString();//totalamount.ToString();
            ////if (Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid").ToString()) > Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Balance").ToString()))
            ////{
            ////    XtraMessageBox.Show("AmountPaid must not greater than Balance amount");
            ////    return;
            ////}
        }

        void populateRepositorySearchLookUp()
        {
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditglcode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditOffsetGLCode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditOffsetCreditGLCode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditEWTDebitGLCode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditEWTCreditGLCode, "AccountCode", "AccountCode");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtdebitglcode.Text) || String.IsNullOrEmpty(txtcontrolno.Text) || String.IsNullOrEmpty(txtcrno.Text) || String.IsNullOrEmpty(txtdate.Text))
            {
                XtraMessageBox.Show("Please Filled up Mandatory Fields");
                return;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false)
            {
                XtraMessageBox.Show("Please select Payment Type");
                return;
            }

            int ctr = 0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True")
                {
                    ctr += 1;
                }
            }

            if (ctr == 0)
            {
                XtraMessageBox.Show("No Payments Executed!.. Please select PAY mode Status in Pay Columns");
                return;
            }
            else
            {
                addPayment();
            }
        }
        void addPayment()
        {
            //id = IDGenerator.getReferenceNumber();
            double totalamount = 0.0;
            refno = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            lasttransseqno = Database.getLastID("TransactionPayment", "CustomerKey='" + custkey + "'", "SEQ_NO") + 1;
            string ponumber = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString();
            try
            {
                if (radioButton1.Checked == true)
                {
                    paymenttype = "CASH";
                }
                else if (radioButton2.Checked == true)
                {
                    paymenttype = "CHECK";
                }
                else if (radioButton3.Checked == true)
                {
                    paymenttype = "ONLINE";
                }
                else if (radioButton3.Checked == true)
                {
                    paymenttype = "ADVANCEPAYMENT";
                } else if (radioButton3.Checked == true)
                {
                    paymenttype = "CREDITCARD";
                }
                double amountpaid = 0.0;
                amountpaid = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid").ToString());
                for (int k = 0; k <= gridView2.RowCount - 1; k++)
                {
                    if (gridView2.GetRowCellValue(k, "Pay").ToString() == "True" && Convert.ToDouble(gridView2.GetRowCellValue(k, "OffsetAmount").ToString()) > 0 && String.IsNullOrEmpty(gridView2.GetRowCellValue(k, "OffsetDebitGLCode").ToString()) && String.IsNullOrEmpty(gridView2.GetRowCellValue(k, "OffsetCreditGLCode").ToString()))
                    {
                        XtraMessageBox.Show("Please Provide Offset GLCode for Offset Amount!");
                        return;
                    }
                }
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True") //total amount of PAY Mode only
                    {
                        totalamount += Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString());
                        if (Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO ARPaymentDetails VALUES('"+ lasttransseqno + "'," +
                                "'" + custkey + "'," +
                                "'" + refno + "'," +
                                "'" + gridView2.GetRowCellValue(i, "OrderNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "AmountPaid").ToString() + "'," +
                                "'INVOICE PAYMENT'," +
                                "'PAYMENT'," +
                                "'"+txtdebitglcode.Text+" '," +
                                "'"+txtcreditglcode.Text+" '," +
                                "' ')");
                        }
                        if (Convert.ToDouble(gridView2.GetRowCellValue(i, "EWTAmount").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO ARPaymentDetails VALUES('" + lasttransseqno + "'," +
                                "'" + custkey + "'," +
                                "'" + refno + "'," +
                                "'" + gridView2.GetRowCellValue(i, "OrderNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "EWTAmount").ToString() + "'," +
                                "'EWT'," +
                                "'PAYMENT'," +
                                "' '," +
                                "' '," +
                                "' ')");
                        }
                        if (Convert.ToDouble(gridView2.GetRowCellValue(i, "DiscountAmount").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO ARPaymentDetails VALUES('" + lasttransseqno + "'," +
                                "'" + custkey + "'," +
                                "'" + refno + "'," +
                                "'" + gridView2.GetRowCellValue(i, "OrderNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "Discount").ToString() + "'," +
                                "'DISCOUNT'," +
                                "'PAYMENT'," +
                                "' '," +
                                "' '," +
                                "' ')");
                        }
                        if (Convert.ToDouble(gridView2.GetRowCellValue(i, "OffsetAmount").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO ARPaymentDetails VALUES('" + lasttransseqno + "'," +
                                "'" + custkey + "'," +
                                "'" + refno + "'," +
                                "'" + gridView2.GetRowCellValue(i, "OrderNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "'," +
                                "'" + gridView2.GetRowCellValue(i, "OffsetAmount").ToString() + "'," +
                                "'OFFSET'," +
                                "'PAYMENT'," +
                                "' '," +
                                "' '," +
                                "' ')");
                        }
                    }

                }
                int id = 0;
                if (paymenttype == "CASH")
                {
                    id = Database.getLastID("TransactionCashCollection", "CustomerKey='" + custkey + "'", "SEQ_NO") + 1;
                    Database.ExecuteQuery("INSERT INTO TransactionCashCollection VALUES('" + id + "','"+custkey+"','" + txtamounttopay.Text + "','" + txtremakrs.Text + "','" + txtdate.Text + "','"+Login.Fullname+"',0)");
                }
                else if (paymenttype == "CHECK")
                {
                    id = Database.getLastID("TransactionCheque", "CustomerID='" + custkey + "'", "TransactionNo") + 1;
                    Database.ExecuteQuery("INSERT INTO TransactionCheque VALUES ('" + id + "','" + Customers.getCustBranch(txtcustid.Text) + "','" + txtcontrolno.Text + "','" + txtcrno.Text + "','" + txtcustid.Text + "','" + txtcustname.Text + "','" + txtchecknum.Text + "','" + txtcheckname.Text + "','" + txtcheckbankname.Text + "','" + txtcheckamount.Text + "','" + txtcheckdate.Text + "','" + txtamounttopay.Text + "','" + txtremakrs.Text + "','" + txtdate.Text + "','" + DateTime.Now.ToString() + "','" + Login.Fullname + "','" + txtdebitglcode.Text + "')");
                }
                else if (paymenttype == "ONLINE")
                {
                    id = Database.getLastID("TransactionOnline", "CustomerID='" + custkey + "'", "TransactionNo") + 1;
                    Database.ExecuteQuery("INSERT INTO TransactionOnline VALUES ('" + id + "','" + Customers.getCustBranch(txtcustid.Text) + "','" + txtcontrolno.Text + "','" + txtcrno.Text + "','" + txtcustid.Text + "','" + txtcustname.Text + "','" + txtrefnoonline.Text + "','" + txtdepbankonline.Text + "','" + txtdatedeponline.Text + "','" + txtamounttopay.Text + "','" + txtremakrs.Text + "','" + txtdate.Text + "','" + Login.Fullname + "','" + txtdebitglcode.Text + "')");
                }
                else if (paymenttype == "ADVANCEPAYMENT")
                {
                    id = Database.getLastID("TransactionOnline", "CustomerID='" + custkey + "'", "TransactionNo") + 1;
                    Database.ExecuteQuery("INSERT INTO TransactionSavings VALUES ('" + id + "','" + Customers.getCustBranch(txtcustid.Text) + "','" + txtcontrolno.Text + "','" + txtcrno.Text + "','" + txtcustid.Text + "','" + txtcustname.Text + "','" + txtrefnoonline.Text + "','" + txtdepbankonline.Text + "','" + txtdatedeponline.Text + "','" + txtamounttopay.Text + "','" + txtremakrs.Text + "','" + txtdate.Text + "','" + Login.Fullname + "','" + txtdebitglcode.Text + "')");
                }
                else if (paymenttype == "CREDITCARD")
                {
                    id = Database.getLastID("TransactionCreditCard", "CustomerID='" + custkey + "'", "TransactionNo") + 1;
                    Database.ExecuteQuery("INSERT INTO TransactionCreditCard VALUES ('" + id + "',' ','"+txtccrefno.Text+"','888',' ',' ','"+txtccbank.Text+"',' ',' ',' ',' ','"+txtamounttopay.Text+"','"+DateTime.Now.ToShortDateString()+"','" + Login.Fullname + "')");
                }
                postPayment();
                XtraMessageBox.Show("Payment Successfully Posted");
                this.Dispose();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radEvent(); 
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radEvent();
        }

        private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view.FocusedColumn.FieldName == "Pay" || view.FocusedColumn.FieldName == "AmountPaid" || view.FocusedColumn.FieldName == "DiscountGLCode" || view.FocusedColumn.FieldName == "Discount" || view.FocusedColumn.FieldName == "OffsetAmount" || view.FocusedColumn.FieldName == "OffsetGLCode" || view.FocusedColumn.FieldName == "OffsetCreditGLCode")
            //    e.Cancel = false;
        }

        private void txtcreditglcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtdebitglcode.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            object value = view.GetRowCellValue(rowHandle, "Description");
            txtdebitdesc.Text = value.ToString();
        }

        private void searchLookUpEditcreditglcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtcreditglcode.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            object value = view.GetRowCellValue(rowHandle, "Description");
            txtcreditdesc.Text = value.ToString();
        }

        private void gridView2_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            
            if (e.Column.FieldName == "DiscountGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditglcode;
            if (e.Column.FieldName == "Pay")
                e.RepositoryItem = repositoryItemCheckEditStatus;
            //if (e.Column.FieldName == "PayCharge")
            //    e.RepositoryItem = repositoryItemCheckEditPayCharge;
            if (e.Column.FieldName == "OffsetGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditOffsetGLCode;
            if (e.Column.FieldName == "OffsetCreditGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditOffsetCreditGLCode;
            if (e.Column.FieldName == "EWTDebitGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditEWTDebitGLCode;
            if (e.Column.FieldName == "EWTCreditGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditEWTCreditGLCode;
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Balance")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "Pay" || e.Column.FieldName == "AmountPaid" || e.Column.FieldName == "ChargeAmountPaid" || e.Column.FieldName == "Discount" || e.Column.FieldName == "DiscountGLCode" || e.Column.FieldName == "OffsetAmount" || e.Column.FieldName == "OffsetGLCode" || e.Column.FieldName == "OffsetCreditGLCode" || e.Column.FieldName == "EWT" || e.Column.FieldName == "EWTAmount" || e.Column.FieldName == "EWTDebitGLCode" || e.Column.FieldName == "EWTCreditGLCode")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radEvent();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl2, e.Location);
        }

        private void editPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ClientEditPayment clientedit = new ClientEditPayment();
            clientedit.txtdelivdate.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"TransactionDate").ToString();
            clientedit.txtinvoiceno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString();
            clientedit.txtinvoiceamount.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceAmount").ToString();
            clientedit.txtfreightamount.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ChargeAmount").ToString();
            clientedit.txtewtamount.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EWTAmount").ToString();
            clientedit.txtduedate.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DueDate").ToString();
            clientedit.txtterms.Text = Database.getSingleQuery("Customers", "CustomerID='" + txtcustid.Text + "'", "Term");
            clientedit.txtdaysdelayed.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DaysDelayed").ToString();
            clientedit.txtpenaltyrate.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PenaltyRate").ToString();
            clientedit.txtsurcharge.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Surcharge").ToString();
            clientedit.ShowDialog(this);
            if(ClientEditPayment.isdone == true)
            {
                if(ClientEditPayment.isdiscount==true)
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Discount", ClientEditPayment.discamount);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "DiscountGLCode", ClientEditPayment.discdebit);
                }
                if(ClientEditPayment.isoffset == true)
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetAmount", ClientEditPayment.offsetamount);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetGLCode", ClientEditPayment.offsetdebit);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetCreditGLCode", ClientEditPayment.offsetcredit);
                }
                if (ClientEditPayment.isewt == true)
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTAmount", ClientEditPayment.ewtamount);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTDebitGLCode", ClientEditPayment.ewtdebit);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTCreditGLCode", ClientEditPayment.ewtcredit);
                }
                if (ClientEditPayment.isfreight == true)
                {
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "FreightAmount", ClientEditPayment.freightamount);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "ChargeDebitGLCode", ClientEditPayment.freightdebit);
                    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "ChargeCreditGLCode", ClientEditPayment.freightcredit);
                }
                ClientEditPayment.isdone = false;
                clientedit.Dispose();
            }
        }

        private void clearFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Discount", "0");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "DiscountGLCode", "");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetAmount", "0");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetGLCode", "");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "OffsetCreditGLCode", "");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTAmount", "0");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTDebitGLCode", "");
            gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "EWTCreditGLCode", "");
        }

        private void refreshDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display();
        }

        private void showSalesItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ClientSalesItems salesitems = new ClientSalesItems();
            //Database.GridMasterDetail("SELECT CustomerNo,ReferenceNo,Invoice,TotalItem,TotalVatableItems,TotalKilos,SubTotal,TotalAmount,TotalVatableSale,TotalVATSale,TotalVATExemptSale FROM BatchSalesSummary WHERE BranchCode='888' AND ReferenceNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderNo").ToString() + "' AND Invoice='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString() + "'", "SELECT ReferenceNo,ProductCode,Description,QtySold,SellingPrice,TaxRate,TaxTotal,SubTotal,TotalAmount FROM BatchSalesDetails WHERE BranchCode='888' AND ReferenceNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderNo").ToString() + "'","BatchSalesSummary","BatchSalesDetails", "ReferenceNo", "ReferenceNo", "SalesDetails",salesitems.gridControl2,"");
            Database.display("SELECT CustomerNo,ReferenceNo,Invoice,TotalItem,TotalVatableItems,TotalKilos,SubTotal,TotalAmount,TotalVatableSale,TotalVATSale,TotalVATExemptSale FROM BatchSalesSummary WHERE BranchCode='888' AND ReferenceNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderNo").ToString() + "' AND Invoice='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString() + "'", salesitems.gridControl2, salesitems.gridView2);
            Database.display("SELECT ReferenceNo,ProductCode,Description,QtySold,SellingPrice,TaxRate,TaxTotal,SubTotal,TotalAmount,isVat FROM BatchSalesDetails WHERE BranchCode='888' AND ReferenceNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderNo").ToString() + "' ", salesitems.gridControl1, salesitems.gridView8);
            Database.GridMasterDetail("ClientChargeSalesSummary", "ClientChargeSalesDetails", "PONumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderNo").ToString() + "'", "PONumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderNo").ToString() + "' ", "ChargeNo", "ChargeNo", "DeliveryChargeDetails", salesitems.gridControlChargesSummary);
            Classes.DevXGridViewSettings.getTotalSummation(salesitems.gridView8, "QtySold", "TaxTotal", "SubTotal", "TotalAmount");
            salesitems.ShowDialog(this);
        }

        private void gridView2_Click(object sender, EventArgs e)
        {

        }

        void postPayment()
        {
            //string refnum = IDGenerator.getReferenceNumber();
           //string refnum = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_AddPaymentClient";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmrefno", refno);
                com.Parameters.AddWithValue("@parmtransseqno", lasttransseqno);
                com.Parameters.AddWithValue("@parmcustkey", custkey);
                com.Parameters.AddWithValue("@parmcontrolno", txtcontrolno.Text);
                com.Parameters.AddWithValue("@parmcrno", txtcrno.Text);

                com.Parameters.AddWithValue("@parmamounttopay", txtamounttopay.Text);
                com.Parameters.AddWithValue("@parmdate", txtdate.Text); 
                com.Parameters.AddWithValue("@parmremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmpreparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmdebitglcode", txtdebitglcode.Text);
                com.Parameters.AddWithValue("@parmcreditglcode", txtcreditglcode.Text);
                com.Parameters.AddWithValue("@parmpaymenttype", paymenttype);
                com.Parameters.AddWithValue("@parmcheckremarks", txtremakrs.Text);
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

        private void txtremakrs_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        void confirmPayment()
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure?", "Confirm Payment");
            if (confirm == true)
            {
                bool isSales = false, isCharge = false;
                if (String.IsNullOrEmpty(txtdebitglcode.Text) || String.IsNullOrEmpty(txtcontrolno.Text) || String.IsNullOrEmpty(txtcrno.Text))
                {
                    XtraMessageBox.Show("Please Filled up Mandatory Fields");
                    return;
                }
                if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false && radCreditCard.Checked==false)
                {
                    XtraMessageBox.Show("Please select Payment Type");
                    return;
                }


                int ctr = 0, advpymntctr = 0;

                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True")
                    {
                        ctr += 1;
                        totalbalance += Convert.ToDouble(gridView2.GetRowCellValue(i, "Balance").ToString());
                        totalamountpaid += Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString());
                    }
                    if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True" && gridView2.GetRowCellValue(i, "InvoiceType").ToString() == "SALES")
                    {
                        isSales = true;

                    }
                    if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True" && gridView2.GetRowCellValue(i, "InvoiceType").ToString() == "CHARGE")
                    {
                        isCharge = true;
                    }
                    //advance payment
                    if (Convert.ToDouble(gridView2.GetRowCellValue(i, "AdvancePayment").ToString()) > 0)
                    {
                        advpymntctr += 1;
                    }
                    if (Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString()) > 0)
                    {
                        advncepymentval += Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString());

                    }

                }


                if (ctr == 0)
                {
                    XtraMessageBox.Show("No Payments Executed!.. Please select PAY mode Status in Pay Columns");
                    return;
                }
                else if (advpymntctr > 1)
                {
                    XtraMessageBox.Show("Advance Payment must assigned only in one invoice!...");
                    return;
                }

                else if (isCharge == true && isSales == true)
                {
                    XtraMessageBox.Show("Cannot Proceed Payment.. Unable to use Mixmode Transactions.. You can Execute one at a time, either CHARGE payment or SALES Invoice first.");
                    return;
                }
                else if (radioButton4.Checked == true && advncepymentval > Convert.ToDouble(txtacctbalance.Text))
                {
                    XtraMessageBox.Show("Cannot Proceed Payment.. Your Total Amount Paid is greater than your Savings Amount.!");
                    return;
                }
                else if (totalamountpaid > totalbalance)
                {
                    XtraMessageBox.Show("Total Amount Paid must not greater than Total Balance");
                    return;
                }
                else
                {
                    isglobalCharge = isCharge;
                    isglobalSales = isSales;
                    addPayment();
                }
            }
            else
            {
                return;
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            confirmPayment();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Database.display("SELECT TransactionDate,CustomerID,Reference,Amount,AmountPaid,Balance,PaymentStatus FROM TransactionChargeSales WHERE CAST(TransactionDate as date) between '" + dateEdit1.Text + "' and '" + dateEdit2.Text + "' and CustomerID='" + groupControl1.Text + "' and Balance=0", gridControl1, gridView9);
       
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            radEvent();
        }

    }
}