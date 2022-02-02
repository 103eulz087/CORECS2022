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

namespace SalesInventorySystem.POS
{
    public partial class POSCashWalletDashboard : Form
    {
        bool isexist = false;
        public POSCashWalletDashboard()
        {
            InitializeComponent();
        }

        private void chcktap_CheckedChanged(object sender, EventArgs e)
        {
            if(chcktap.Checked==true)
            {
                txttapid.Focus();
                searchLookUpEdit1.Enabled = false;
                //panel1.Enabled = false;
            }
            else
            {
                searchLookUpEdit1.Enabled = true;
                //panel1.Enabled = true;
                searchLookUpEdit1.Focus();
            }
        }

        void loadCustomers()
        {
            Database.displaySearchlookupEdit("SELECT distinct CustomerKey,CustomerID,CustomerName FROM Customers", searchLookUpEdit1, "CustomerName", "CustomerName");
        }

        private void POSCashWalletDashboard_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txttapid;
            txttapid.Focus();
            loadCustomers();
            chcktap.Checked = true;
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //customername = searchLookUpEdit1.Text;
            object custky = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "CustomerKey");
            getCustDetails(custky.ToString());
        }

        private void txttapid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                bool isExist = Database.checkifExist("SELECT TOP(1) CustomerID FROM Customers WHERE CustomerID='" + txttapid.Text + "'");
                if(!isExist)
                {
                    isexist = false;
                    XtraMessageBox.Show("Records Not Exists!!!");
                    txttapid.Text = "";
                    return;
                }
                else
                {
                    isexist = true;
                    string acctkey = Database.getSingleQuery("Customers", "CustomerID='" + txttapid.Text + "' ", "CustomerKey");
                    getCustDetails(acctkey);
                }
                //txttapid.Text = "";
                txttapid.Focus();
            }
        }

        void getCustDetails(string id)
        {
           
            var rows = Database.getMultipleQuery("ClientAccounts", "AccountKey='" + id + "'", "AccountKey,AccountName,AccountStatus,CashWalletBalance");
            string AccountKey, AccountName, AccountStatus, CashWalletBalance;
            AccountKey = rows["AccountKey"].ToString();
            AccountName = rows["AccountName"].ToString();
            AccountStatus = rows["AccountStatus"].ToString();
            CashWalletBalance = rows["CashWalletBalance"].ToString();

            txtacctid.Text = AccountKey;
            txtacctname.Text = AccountName;
            txtacctstatus.Text = AccountStatus;
            txtacctbalance.Text = CashWalletBalance;
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            loadLedger();
        }
        void loadLedger()
        {
            Database.display("SELECT CAST(TransactionDate as date) as TransactionDate,Particulars,Debit,Credit FROM CashWalletLedger WHERE AccountID='" + txtacctid.Text + "' AND CAST(TransactionDate as date) >= '" + datefromledge.Text + "' and CAST(TransactionDate as date)<= '" + datetoledge.Text + "' ORDER BY SequenceNumber ASC", gridControl2, gridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadTransactionSummary();
        }
        void loadTransactionSummary()
        {
            Database.display("SELECT CAST(TransDate as date) as TransactionDate,TransactionCode,ReferenceNo,TotalItem,TotalAmount,PaymentType,PreparedBy FROM BatchSalesSummary WHERE CustomerNo='" + txtacctid.Text + "' AND CAST(TransDate as date) >= '" + dateFromTransSum.Text + "' and CAST(TransDate as date)<= '" + dateToTransSum.Text + "' and Status='SOLD' and PaymentType='CashWallet'", gridControl1, gridView1);
            //  Database.displayLocalGrid("SELECT ReferenceNo as OrderNo,Invoice,PaymentType,TotalItem,SubTotal as TotalAmount,AmountTendered,AmountChange,TransDate FROM BatchSalesSummary WHERE CustomerNo='" + txtacctid.Text + "'", dataGridView2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(isexist)
            {
                POSLoader posload = new POSLoader();
                posload.txttapid.Text = txttapid.Text;
                posload.ShowDialog(this);
                if (POSLoader.isdone == true)
                {
                    btnExtract.PerformClick();
                    POSLoader.isdone = false;
                    posload.Dispose();
                }
            }
            else
            {
                XtraMessageBox.Show("Records Not Exists!!!");
                return;
            }
           
        }
    }
}
