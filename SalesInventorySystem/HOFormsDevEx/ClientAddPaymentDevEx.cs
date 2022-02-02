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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ClientAddPaymentDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string amountpaid, discount, ewt, offset;
        public static bool isdone = false;

        private void ClientAddPaymentDevEx_Load(object sender, EventArgs e)
        {
            txtamountpaid.Focus();
        }

        public ClientAddPaymentDevEx()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double total = 0.0;
            total = Convert.ToDouble(txtamountpaid.Text) + Convert.ToDouble(txtdiscountamount.Text) + Convert.ToDouble(txtewtamount.Text);
            if (total > Convert.ToDouble(txtbalance.Text))
            {
                XtraMessageBox.Show("Must not Greater than Balance");
            }
            else
            {
                amountpaid = txtamountpaid.Text;
                discount = txtdiscountamount.Text;
                ewt = txtewtamount.Text;
                offset = txtoffsetamount.Text;
                isdone = true;
                this.Close();
            }
        }
    }
}