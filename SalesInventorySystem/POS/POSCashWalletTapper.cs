using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSCashWalletTapper : Form
    {
        public static bool isdone = false;
        public static string netamount="", clientkey = "";
        public POSCashWalletTapper()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                btnClose.PerformClick();
            }
            else if (keyData == Keys.Enter) //PAYMENT
            {
                tap();
            }

            return functionReturnValue;
        }
        private void txttapid_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txttapid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                tap();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        void tap()
        {
            bool isExist = Database.checkifExist("SELECT TOP(1) CustomerID FROM dbo.Customers WHERE CustomerID='" + txttapid.Text + "'");
            
            if(!isExist)
            {
                XtraMessageBox.Show("INVALID ID.. No Records Found!!!...");
                txttapid.Text = "";
                return;
            }
            else //do inquiry
            {
                string getAcctKey = Database.getSingleQuery("Customers", "CustomerID='" + txttapid.Text + "'", "CustomerKey");
                string getCurrentBalance = Database.getSingleQuery("ClientAccounts", "AccountKey='" + getAcctKey + "'", "CashWalletBalance");
                double curbal = 0.0,payable=0.0;
                curbal = Convert.ToDouble(getCurrentBalance);
                payable = Convert.ToDouble(POSConfirmPayment.netamountpayable);
                if(payable > curbal)
                {
                    XtraMessageBox.Show("Insufficient Funds.. Payable must not greater than AccountBalance..");
                    return;
                }
                else
                {
                    netamount = payable.ToString();
                    clientkey = getAcctKey;
                    isdone = true;
                   
                }
            }
            this.Close();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            tap();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            isdone = false;
            this.Close();
        }

        private void POSCashWalletTapper_Load(object sender, EventArgs e)
        {
            txttapid.Focus();
        }
    }
}
