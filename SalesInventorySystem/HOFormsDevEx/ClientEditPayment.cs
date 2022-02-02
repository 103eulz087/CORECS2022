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
    public partial class ClientEditPayment : DevExpress.XtraEditors.XtraForm
    {
        public static string discamount = "", discdebit = "", discredit = "", offsetamount = "", offsetdebit = "", offsetcredit = "", ewtamount = "", ewtdebit = "", ewtcredit = "",freightamount="",freightdebit="",freightcredit="";
        public static bool isdone = false, isdiscount = false, isoffset = false,isewt=false,isfreight=false;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public ClientEditPayment()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Enabled = true;
                populateDiscountCode();
            }
            else
            {
                groupBox1.Enabled = false;
                clearDisc();
            }
                
        }

        private void checkBoxFreight_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFreight.Checked == true)
            {
                groupBoxFreight.Enabled = true;
                populateFreight();
            }
            else
            {
                groupBoxFreight.Enabled = false;
                clearFreight();
            }
        }

        void populateDiscountCode()
        {
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtdiscdebit, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtdisccred, "AccountCode", "AccountCode");
        }
        void clearDisc()
        {
            txtdiscamount.Text = "";
            txtdisccred.Text = "";
            txtdiscdebit.Text = "";
        }
        private void checkBoxOffset_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOffset.Checked == true)
            {
                groupBoxOffset.Enabled = true;
                populateOffsetCode();
            }
            else
            {
                groupBoxOffset.Enabled = false;
                clearOffset();
            }
               
        }
        void populateOffsetCode()
        {
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtoffsetdebit, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtoffsetcredit, "AccountCode", "AccountCode");
        }
        void clearOffset()
        {
            txtoffsetamount.Text = "";
            txtoffsetdebit.Text = "";
            txtoffsetcredit.Text = "";
        }
        private void checkBoxEWT_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEWT.Checked == true)
            {
                groupBoxEWT.Enabled = true;
                populateEWTCode();
            }
            else
            {
                groupBoxEWT.Enabled = false;
                clearEWT();
            }
                
        }
        void populateEWTCode()
        {
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtewtdebit, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtewtcredit, "AccountCode", "AccountCode");
        }
        void clearEWT()
        {
            txtewtamount.Text = "";
            txtewtdebit.Text = "";
            txtewtcredit.Text = "";
        }
        void populateFreight()
        {
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtfreightdebitglcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtfreightcreditglcode, "AccountCode", "AccountCode");
        }
        void clearFreight()
        {
            txtfreightamount.Text = "";
            txtfreightdebitglcode.Text = "";
            txtfreightcreditglcode.Text = "";
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                if(String.IsNullOrEmpty(txtdiscamount.Text) || String.IsNullOrEmpty(txtdiscdebit.Text) || String.IsNullOrEmpty(txtdisccred.Text))
                {
                    XtraMessageBox.Show("All Discount Fields are Mandatory!...");
                    return;
                }
                else
                {
                    discamount = txtdiscamount.Text;
                    discdebit = txtdiscdebit.Text;
                    discredit = txtdisccred.Text;
                    isdiscount = true;
                }
            }
            if(checkBoxOffset.Checked == true)
            {
                if (String.IsNullOrEmpty(txtoffsetamount.Text) || String.IsNullOrEmpty(txtoffsetdebit.Text) || String.IsNullOrEmpty(txtoffsetcredit.Text))
                {
                    XtraMessageBox.Show("All Offset Fields are Mandatory!...");
                    return;
                }
                else
                {
                    offsetamount = txtoffsetamount.Text;
                    offsetdebit = txtoffsetdebit.Text;
                    offsetcredit = txtoffsetcredit.Text;
                    isoffset = true;
                }
            }
            if(checkBoxEWT.Checked==true)
            {
                if (String.IsNullOrEmpty(txtewtamount.Text) || String.IsNullOrEmpty(txtewtdebit.Text) || String.IsNullOrEmpty(txtewtcredit.Text))
                {
                    XtraMessageBox.Show("All EWT Fields are Mandatory!...");
                    return;
                }
                else
                {
                    ewtamount = txtewtamount.Text;
                    ewtdebit = txtewtdebit.Text;
                    ewtcredit = txtewtcredit.Text;
                    isewt = true;
                }
            }
            if (checkBoxFreight.Checked == true)
            {
                if (String.IsNullOrEmpty(txtfreightamount.Text) || String.IsNullOrEmpty(txtfreightdebitglcode.Text) || String.IsNullOrEmpty(txtfreightcreditglcode.Text))
                {
                    XtraMessageBox.Show("All Charges Fields are Mandatory!...");
                    return;
                }
                else
                {
                    freightamount = txtfreightamount.Text;
                    freightdebit = txtfreightdebitglcode.Text;
                    freightcredit = txtfreightcreditglcode.Text;
                    isfreight = true;
                }
            }
            isdone = true;
            this.Close();
        }
    }
}