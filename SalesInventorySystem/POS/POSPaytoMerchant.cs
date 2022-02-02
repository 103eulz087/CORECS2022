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
    public partial class POSPaytoMerchant : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public static string refno="", merchantname = "", vouchercode = "",amount="",paytype="";
        public POSPaytoMerchant()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                this.Close();
            }
            else if (keyData == Keys.F1)
            {
                radcash.Checked = true;
            }
            else if (keyData == Keys.F2)
            {
                radcc.Checked = true;
            }

            return functionReturnValue;
        }

        void radChanged()
        {
            if(radcash.Checked.Equals(true))
            {
                
                txtamount.Enabled = true;
                txtamount.Focus();
            }
            else if(radcc.Checked.Equals(true))
            {
                
                txtamount.Enabled = false;
                txtamount.Focus();
            }
        }

        private void POSPaytoMerchant_Load(object sender, EventArgs e)
        {
            loadMerchant();
            txtrefno.Focus();
        }

        void loadMerchant()
        {
            Database.displaySearchlookupEdit("SELECT MerchantID,MerchantName FROM Merchants", txtmerchant, "MerchantName", "MerchantName");
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtrefno.Text) || String.IsNullOrEmpty(txtmerchant.Text) || String.IsNullOrEmpty(txtamount.Text))
            {
                XtraMessageBox.Show("Please Input Valid Fields...");
                return;
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure all fields correct?", "Confirm Payment");
                if(confirm)
                {
                    if (radcc.Checked.Equals(true)) { paytype = "Credit"; } else { paytype = "Cash"; }
                    refno = txtrefno.Text.Trim();
                    merchantname = txtmerchant.Text.Trim();
                    vouchercode = txtvouchercode.Text.Trim();
                    amount = txtamount.Text.Trim();
                    isdone = true;
                    this.Close();
                }
                else
                {
                    return;
                }
               
            }
        }
    }
}