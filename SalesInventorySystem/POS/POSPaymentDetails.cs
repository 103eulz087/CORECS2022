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
    public partial class POSPaymentDetails : Form
    {
        public static string creditcardnum, creditcardname, creditcardbankname, creditcardmerchant, creditcardcode, chequename, chequenumber, chequebankname,creditcardrefno,creditcardtype,creditcardexpirydate;

        private void groupCreditCardDetails_Enter(object sender, EventArgs e)
        {

        }

        private void txtccbank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdConfirm.Focus();
            }
        }

        private void txtccrefno_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtccbank.Focus();
            }
        }

        public static bool isdone = false;
        public POSPaymentDetails()
        {
            InitializeComponent();
        }

        private void POSPaymentDetails_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtccrefno;
            txtccrefno.Focus();
            Database.displayComboBoxItems("SELECT BankName FROM dbo.ListOfBanks", "BankName", txtccbank);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                this.Close();
            }
          
            return functionReturnValue;
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            groupCheque.Visible = false;
            if(String.IsNullOrEmpty(txtccbank.Text) || String.IsNullOrEmpty(txtccrefno.Text))
            //if(String.IsNullOrEmpty(txtexpirydate.Text) || String.IsNullOrEmpty(txtcardtype.Text) || String.IsNullOrEmpty(txtccmerchant.Text) || String.IsNullOrEmpty(txtccmerchant.Text) || String.IsNullOrEmpty(txtccbank.Text) || String.IsNullOrEmpty(txtccnumber.Text) || String.IsNullOrEmpty(txtccrefno.Text))
            {
                XtraMessageBox.Show("All Fields are mandatory!...");
                return;
            }
            else
            {
                creditcardtype= txtcardtype.Text.Trim();
                creditcardexpirydate = txtexpirydate.Text.Trim();
                creditcardmerchant = txtccmerchant.Text.Trim();
                creditcardname = txtccname.Text.Trim();
                creditcardbankname = txtccbank.Text.Trim();
                creditcardnum = txtccnumber.Text.Trim();
                creditcardrefno = txtccrefno.Text.Trim();
                isdone = true;
                this.Close();
            }
           
            //if (groupCheque.Visible == true)
            //{
            //    //Classes.Utilities.checkEmptyTextBox(txtcheckname, "Must not Empty");
            //    //Classes.Utilities.checkEmptyTextBox(txtchecknum, "Must not Empty");
            //    //Classes.Utilities.checkEmptyTextBox(txtcheckbankname, "Must not Empty");
            //    chequename = txtcheckname.Text.Trim();
            //    chequenumber = txtchecknum.Text.Trim();
            //    chequebankname = txtcheckbankname.Text.Trim();
            //}
            //if (groupCreditCardDetails.Visible == true)
            //{

            //    //Classes.Utilities.checkEmptyTextBox(txtccname, "Must not Empty");
            //    //Classes.Utilities.checkEmptyTextBox(txtccbank, "Must not Empty");
            //    //Classes.Utilities.checkEmptyTextBox(txtccnumber, "Must not Empty");
            //    //Classes.Utilities.checkEmptyTextBox(txtcccode, "Must not Empty");
            //    creditcardmerchant = txtccmerchant.Text.Trim();
            //    creditcardname = txtccname.Text.Trim();
            //    creditcardbankname = txtccbank.Text.Trim();
            //    creditcardnum = txtccnumber.Text.Trim();
            //}
            this.Close();
        }
    }
}
