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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ClientPayments : DevExpress.XtraEditors.XtraForm
    {
        string id,paymenttype="";
        public ClientPayments()
        {
            InitializeComponent();
        }

        private void ClientPayments_Load(object sender, EventArgs e)
        {
            populateRepositorySearchLookUp();
            //txtcustid.Text = HOForms.ClientAccounts.custid;
            //txtcustname.Text = HOForms.ClientAccounts.custname;
            //display();
            populateCOA();
            //GridGroupSummaryItem ite1 = new GridGroupSummaryItem();
            //ite1.FieldName = "AmountPaid";
            //ite1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ite1.ShowInGroupColumnFooter = gridView2.Columns["AmountPaid"];
            //gridView2.GroupSummary.Add(ite1);

            //gridView2.Columns["AmountPaid"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "AmountPaid", "{0:n2}");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                this.Close();
            }
            else if (keyData == Keys.F1) //PAYMENT
            {
                radioButton1.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = false;
            }
            else if (keyData == Keys.F2) //PAYMENT
            {
                radioButton2.Checked = true;
                panelOnline.Visible = false;
                groupCheque.Visible = true;
            }
            else if (keyData == Keys.F3) //PAYMENT
            {
                radioButton3.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = true;
            }
            return functionReturnValue;
        }

        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtcreditglcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", searchLookUpEditcreditglcode, "AccountCode", "AccountCode");
        }

        void display()
        {
            //Database.display("SELECT SequenceNumber,CAST(TransactionDate as date) as TransactionDate,Reference as InvoiceNo,Amount,Balance,'0' as AmountPaid,'0' as Discount,'NONE' as Pay FROM TransactionChargeSales WHERE (PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL') AND Balance > 0 and CustomerID='"+txtcustid.Text+"'", gridControl2, gridView2);
            Database.display("SELECT SequenceNumber,CAST(TransactionDate as date) as TransactionDate,Reference as InvoiceNo,Amount,Balance,'0' as AmountPaid,'0' as Discount,'' as DiscountGLCode,'0' as OffsetAmount,'' as OffsetGLCode,'' as Pay FROM TransactionChargeSales WHERE (PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL') AND Balance > 0 and CustomerID='" + txtcustid.Text + "'", gridControl2, gridView2);
            gridView2.Columns["SequenceNumber"].Visible = false;
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double balance = 0.0, amountpaid = 0.0,discount=0.0,totalmoney=0.0;
            balance = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Balance").ToString());
            discount = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Discount").ToString());
            amountpaid = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid").ToString());
            totalmoney = discount + amountpaid;
         
            if (amountpaid > balance)
            {
                XtraMessageBox.Show("AmountPaid must not greater than Balance amount");
                return;
            }
            if(totalmoney > balance)
            {
                XtraMessageBox.Show("AmountPaid + Discount must not greater than Balance amount");
                return;
            }
            double totalamount = 0.0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                //if (gridView2.GetRowCellValue(i, "Pay").ToString() == "PAY") gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True"
                if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True")
                {
                    totalamount += Convert.ToDouble(gridView2.GetRowCellValue(i, "AmountPaid").ToString());
                }
            }
            txtamounttopay.Text = totalamount.ToString();

        }
        void populateRepositorySearchLookUp()
        {
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditglcode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditOffsetGLCode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditOffsetCreditGLCode, "AccountCode", "AccountCode");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtcreditglcode.Text))
            {
                XtraMessageBox.Show("Please select GL Code");
                return;
            }
            if (radioButton1.Checked==false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                XtraMessageBox.Show("Please select Payment Type");
                return;
            }
            
            int ctr = 0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                //if (gridView2.GetRowCellValue(i, "Pay").ToString() == "PAY")
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

            id = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
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
                double  amountpaid = 0.0;
                amountpaid = Convert.ToDouble(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountPaid").ToString());
                for(int k=0;k<=gridView2.RowCount-1;k++)
                {
                    if (gridView2.GetRowCellValue(k, "Pay").ToString() == "True" && Convert.ToDouble(gridView2.GetRowCellValue(k, "Discount").ToString()) > 0 && String.IsNullOrEmpty(gridView2.GetRowCellValue(k, "DiscountGLCode").ToString()))
                    {
                        XtraMessageBox.Show("Please Provide Discount GLCode for Discount");
                        return;
                    }
                    if (gridView2.GetRowCellValue(k, "Pay").ToString() == "True" && Convert.ToDouble(gridView2.GetRowCellValue(k, "OffsetAmount").ToString()) > 0 && String.IsNullOrEmpty(gridView2.GetRowCellValue(k, "OffsetGLCode").ToString()) && String.IsNullOrEmpty(gridView2.GetRowCellValue(k, "OffsetCreditGLCode").ToString()))
                    {
                        XtraMessageBox.Show("Please Provide Offset GLCode for Offset Amount!");
                        return;
                    }
                }
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    if (gridView2.GetRowCellValue(i, "Pay").ToString() == "True") //total amount of PAY Mode only
                    {
                        if (paymenttype == "CASH")
                        {
                            Database.ExecuteQuery("INSERT INTO TransactionCashCollectionDetails VALUES('" + id + "','" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "','" + gridView2.GetRowCellValue(i, "Amount").ToString() + "','" + gridView2.GetRowCellValue(i, "AmountPaid").ToString() + "','" + gridView2.GetRowCellValue(i, "Discount").ToString() + "','" + gridView2.GetRowCellValue(i, "DiscountGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetAmount").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetCreditGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTAmount").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTDebitGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTCreditGLCode").ToString() + "','UNPAID','0','" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "')");
                        }
                        else if (paymenttype == "CHECK")
                        {
                            Database.ExecuteQuery("INSERT INTO TransactionChequeDetails VALUES('" + id + "','" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "','" + gridView2.GetRowCellValue(i, "Amount").ToString() + "','" + gridView2.GetRowCellValue(i, "AmountPaid").ToString() + "','" + gridView2.GetRowCellValue(i, "Discount").ToString() + "','"+ gridView2.GetRowCellValue(i, "DiscountGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetAmount").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetCreditGLCode").ToString() + "''" + gridView2.GetRowCellValue(i, "EWTAmount").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTDebitGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTCreditGLCode").ToString() + "','UNPAID','" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "',0)");
                        }
                        else if (paymenttype == "ONLINE")
                        {
                            Database.ExecuteQuery("INSERT INTO TransactionOnlineDetails VALUES('" + id + "','" + gridView2.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridView2.GetRowCellValue(i, "TransactionDate").ToString() + "','" + gridView2.GetRowCellValue(i, "Amount").ToString() + "','" + gridView2.GetRowCellValue(i, "AmountPaid").ToString() + "','" + gridView2.GetRowCellValue(i, "Discount").ToString() + "','" + gridView2.GetRowCellValue(i, "DiscountGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetAmount").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "OffsetCreditGLCode").ToString() + "''" + gridView2.GetRowCellValue(i, "EWTAmount").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTDebitGLCode").ToString() + "','" + gridView2.GetRowCellValue(i, "EWTCreditGLCode").ToString() + "','UNPAID','" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "',0)");
                        }
                    }
                    
                }
                if (paymenttype == "CASH")
                {
                    Database.ExecuteQuery("INSERT INTO TransactionCashCollection VALUES('" + id + "','"+txtcrno.Text+"','" + txtcustid.Text+ "','"+txtcustname.Text+"','"+txtamounttopay.Text+"','" + txtremakrs.Text + "','" + txtdate.Text + "','" + txtcreditglcode.Text + "')");
                }
                else if (paymenttype == "CHECK")
                {
                    Database.ExecuteQuery("INSERT INTO TransactionCheque VALUES ('" + id + "','" + txtcrno.Text + "','" + txtcustid.Text + "','" + txtcustname.Text + "','"+txtchecknum.Text+"','" + txtcheckname.Text + "','" + txtcheckbankname.Text + "','"+txtcheckamount.Text+"','" + txtcheckdate.Text + "','" + txtamounttopay.Text + "','"+txtremakrs.Text+"','"+txtdate.Text+"','" + DateTime.Now.ToString() + "','" + Login.Fullname + "','"+txtcreditglcode.Text+"')");
                }
                else if (paymenttype == "ONLINE")
                {
                    Database.ExecuteQuery("INSERT INTO TransactionOnline VALUES ('" + id + "','" + txtcrno.Text + "','" + txtcustid.Text + "','" + txtcustname.Text + "','"+txtrefnoonline.Text+"','" + txtdepbankonline.Text + "','" + txtdatedeponline.Text + "','" + txtamounttopay.Text + "','" + txtremakrs.Text+"','" + txtdate.Text + "','" + Login.Fullname+ "','"+txtcreditglcode.Text+"')");
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

        void postPayment()
        {
            //string refnum = IDGenerator.getReferenceNumber();

            string refnum = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_AddPaymentClient";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmrefno", refnum);
                com.Parameters.AddWithValue("@parmvoucherid", id);
                com.Parameters.AddWithValue("@parmcustid", txtcustid.Text);
                com.Parameters.AddWithValue("@parmsuppliername", txtcustname.Text);
                com.Parameters.AddWithValue("@parmamounttopay", txtamounttopay.Text);
                com.Parameters.AddWithValue("@parmdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmpreparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmglcode", txtcreditglcode.Text);
                com.Parameters.AddWithValue("@parmcreditglcode", searchLookUpEditcreditglcode.Text);
                com.Parameters.AddWithValue("@parmpaymenttype", paymenttype);
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton2.Checked = true;
                panelOnline.Visible = false;
                groupCheque.Visible = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton3.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Pay" || view.FocusedColumn.FieldName == "AmountPaid" || view.FocusedColumn.FieldName == "DiscountGLCode" || view.FocusedColumn.FieldName == "Discount" || view.FocusedColumn.FieldName == "OffsetAmount" || view.FocusedColumn.FieldName == "OffsetGLCode" || view.FocusedColumn.FieldName == "OffsetCreditGLCode")
                e.Cancel = false;
        }

        private void gridView2_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "DiscountGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditglcode;
            if (e.Column.FieldName == "Pay")
                //e.RepositoryItem = repositoryItemComboBox1;
                e.RepositoryItem = repositoryItemCheckEditStatus;
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
            if (e.Column.FieldName == "AmountPaid")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            if (e.Column.FieldName == "Discount")
            {
                e.Appearance.BackColor = Color.LightSalmon;
            }
            if (e.Column.FieldName == "DiscountGLCode")
            {
                e.Appearance.BackColor = Color.LightCyan;
            }
            if (e.Column.FieldName == "OffsetAmount")
            {
                e.Appearance.BackColor = Color.LightCoral;
            }
            if (e.Column.FieldName == "OffsetGLCode")
            {
                e.Appearance.BackColor = Color.LightCyan;
            }
            if (e.Column.FieldName == "OffsetCreditGLCode")
            {
                e.Appearance.BackColor = Color.LightPink;
            }
            if (e.Column.FieldName == "Pay")
            {
                e.Appearance.BackColor = Color.LightGray;
            }
        }

        private void repositoryItemCheckEditStatus_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit edit = (DevExpress.XtraEditors.CheckEdit)sender;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton1.Checked = true;
                groupCheque.Visible = false;
                panelOnline.Visible = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtcreditglcode.Text))
            {
                XtraMessageBox.Show("Please select GL Code");
                return;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                XtraMessageBox.Show("Please select Payment Type");
                return;
            }

            int ctr = 0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                //if (gridView2.GetRowCellValue(i, "Pay").ToString() == "PAY")
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
    }
}