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

        public decimal AmountPaid { get; private set; }
        public decimal Discount { get; private set; }
        public decimal EWT { get; private set; }
        public decimal Offset { get; private set; }

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

        private void chckpayfull_CheckedChanged(object sender, EventArgs e)
        {
            if (chckpayfull.Checked == true) { txtamountpaid.Text = txtbalance.Text; } else { txtamountpaid.Text = "0"; }
        }

        private void txtewtamount_EditValueChanged(object sender, EventArgs e)
        {
            decimal amountpaid = Convert.ToDecimal(txtbalance.Text) - Convert.ToDecimal(txtewtamount.Text);
            txtamountpaid.Text = amountpaid.ToString();
        }

        private void chckewt_CheckedChanged(object sender, EventArgs e)
        {
            if(chckewt.Checked==true)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
            }
        }

        public SupplierAddPaymentDevEx()
        {
            InitializeComponent();
        }

        private void rad1perc_CheckedChanged(object sender, EventArgs e)
        {
            ewtCheckChanged();
        }

        private void rad2perc_CheckedChanged(object sender, EventArgs e)
        {
            ewtCheckChanged();
        }

        private void rad5perc_CheckedChanged(object sender, EventArgs e)
        {
            ewtCheckChanged();
        }

        private void rad10perc_CheckedChanged(object sender, EventArgs e)
        {
            ewtCheckChanged();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtamountpaid.Text, out var paid)) paid = 0;
            if (!decimal.TryParse(txtdiscountamount.Text, out var disc)) disc = 0;
            if (!decimal.TryParse(txtewtamount.Text, out var ewt)) ewt = 0;
            if (!decimal.TryParse(txtoffsetamount.Text, out var off)) off = 0;
            if (!decimal.TryParse(txtbalance.Text, out var bal)) bal = 0;

            var total = Math.Round(paid + disc + ewt + off, 2);
            if (total > bal)
            {
                XtraMessageBox.Show("Must not be greater than Balance.");
                return;
            }

            AmountPaid = paid;
            Discount = disc;
            EWT = ewt;
            Offset = off;

            this.DialogResult = DialogResult.OK;
            Close();

            //double total = 0.0;
            //total = Math.Round(Convert.ToDouble(txtamountpaid.Text) + Convert.ToDouble(txtdiscountamount.Text) + Convert.ToDouble(txtewtamount.Text),2);
            //if (total > Convert.ToDouble(txtbalance.Text))
            //{
            //    XtraMessageBox.Show("Must not Greater than Balance");
            //}
            //else
            //{
            //    amountpaid = txtamountpaid.Text;
            //    discount = txtdiscountamount.Text;
            //    ewt = txtewtamount.Text;
            //    offset = txtoffsetamount.Text;
            //    isdone = true;
            //    this.Close();
            //}
        }

        //decimal ewtCheckChanged()
        //{
        //    decimal ewtamt = 0m;

        //    if (rad1perc.Checked)
        //    {
        //        if (decimal.TryParse(txtbalance.Text, out decimal balance))
        //        {
        //            ewtamt = (balance / 1.12m) * 0.01m;
        //        }
        //    }else if(rad2perc.Checked)
        //    {
        //        if (decimal.TryParse(txtbalance.Text, out decimal balance))
        //        {
        //            ewtamt = (balance / 1.12m) * 0.02m;
        //        }
        //    }else if(rad5perc.Checked)
        //    {
        //        if (decimal.TryParse(txtbalance.Text, out decimal balance))
        //        {
        //            ewtamt = (balance / 1.12m) * 0.05m;
        //        }
        //    }else if(rad10perc.Checked)
        //    {
        //        if (decimal.TryParse(txtbalance.Text, out decimal balance))
        //        {
        //            ewtamt = (balance / 1.12m) * 0.1m;
        //        }
        //    }

        //    return ewtamt;
        //}
        void ewtCheckChanged()
        {
            decimal ewtamt = 0m;

            // Safely parse balance
            if (!decimal.TryParse(txtbalance.Text, out decimal balance))
            {
                txtewtamount.Text = "0.00";
                return;
            }

            // Decide calculation based on which radio is checked
            if (rad1perc.Checked)
            {
                // Example: 1% of net of VAT
                ewtamt = (balance / 1.12m) * 0.01m;
            }
            else if (rad2perc.Checked)
            {
                // Example: 2% of gross
                ewtamt = (balance / 1.12m) * 0.02m;
            }
            else if (rad5perc.Checked)
            {
                // Example: fixed 500 deduction
                ewtamt = (balance / 1.12m) * 0.05m;
            }
            else if (rad10perc.Checked)
            {
                // Example: 5% of net
                ewtamt = (balance / 1.12m) * 0.1m;
            }

            // Show result
            txtewtamount.Text = ewtamt.ToString("N2");
        }


        private void SupplierAddPaymentDevEx_Load(object sender, EventArgs e)
        {
            txtamountpaid.Focus();
        }
    }
}