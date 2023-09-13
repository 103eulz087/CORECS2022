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
    public partial class SupplierAddPaymentDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string amountpaid, discount, ewt, offset;
        public static bool isdone = false;

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery($"UPDATE APACCOUNTS SET " +
                $"ActualCost='{txtactualcost.Text}', " +
                $"Balance='{txtbalance.Text}', " +
                $"PayStatus='UNPAID' " +
                $"WHERE ShipmentNo='{txtshipno.Text}' " +
                $"AND InvoiceNo='{txtinvoiceno.Text}' " +
                $"AND SupplierID='{groupControl1.Text}' ","Succesfully Updated");
            isdone = true;
            this.Close();
        }

        public SupplierAddPaymentDevEx()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double total = 0.0;
            total = Math.Round(Convert.ToDouble(txtamountpaid.Text) + Convert.ToDouble(txtdiscountamount.Text) + Convert.ToDouble(txtewtamount.Text),2);
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

        private void SupplierAddPaymentDevEx_Load(object sender, EventArgs e)
        {
            txtamountpaid.Focus();
        }
    }
}