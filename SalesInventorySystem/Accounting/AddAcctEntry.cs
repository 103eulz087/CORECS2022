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

namespace SalesInventorySystem.Accounting
{
    public partial class AddAcctEntry : Form
    {
        //DataTable table;
        public static string acctcode, acctitle, debit, credit;
        public AddAcctEntry()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchAccountCode sacForm = new SearchAccountCode();
            sacForm.FormClosed += new FormClosedEventHandler(sacForm_FormClosed);
            sacForm.ShowDialog(this);
        }
        void sacForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtacctcode.Text = SearchAccountCode.acctcode;
            txtaccttitle.Text = SearchAccountCode.acctdesc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            acctcode = txtacctcode.Text;
            acctitle = txtaccttitle.Text;
            debit = txtdebit.Text;
            credit = txtcredit.Text;
            if (HelperFunction.isTextfieldEmpty(txtacctcode, txtaccttitle, txtcredit, txtdebit))
            {
                XtraMessageBox.Show("Please Input All Fields");
            }
            else if (Convert.ToDouble(txtdebit.Text) == Convert.ToDouble(txtcredit.Text))
            {
                XtraMessageBox.Show("Debit and Credit Fields must not Equal!");
            }
            else
            {
                this.Close();
            }
        }
    }
}
