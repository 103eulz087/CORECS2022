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

namespace SalesInventorySystem.POS
{
    public partial class POSLoader : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone=false;
        public POSLoader()
        {
            InitializeComponent();
        }

        private void POSLoader_Load(object sender, EventArgs e)
        {
            txttapid.Focus();
        }

        private void txttapid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtamount.Focus();
            }
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnreload.Focus();
            }
        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtamount.Text) || String.IsNullOrEmpty(txttapid.Text))
            {
                XtraMessageBox.Show("Filled must not Empty!...");
                txttapid.Focus();
            }
            else
            {
                reload();
            }
        }

        void reload()
        {
            string getacctkey = Database.getSingleQuery("Customers", $"CustomerID='{txttapid.Text}'", "CustomerKey");
            Database.ExecuteQuery($"INSERT INTO dbo.CashWalletLedger VALUES('{getacctkey}','RELOAD',0,0,'{txtamount.Text}',0,'{txtamount.Text}','{DateTime.Now.ToString()}','{Login.isglobalUserID}',0,'{Environment.MachineName}')", "Successfully Reloaded!..");
            isdone = true;
            this.Close();
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }
    }
}