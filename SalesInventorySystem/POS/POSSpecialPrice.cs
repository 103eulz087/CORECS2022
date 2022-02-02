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
    public partial class POSSpecialPrice : Form
    {
        public static string spprice = "",stat="NO";
        public static bool isconfirmed = false;
        public POSSpecialPrice()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Enter) //PAYMENT
            {
                addbtn.PerformClick();
            }
            else if (keyData == Keys.Escape) //PAYMENT
            {
                isconfirmed = true;
                this.Close();
            }
            return functionReturnValue;
        }


        private void addbtn_Click(object sender, EventArgs e)
        {
            if (txtspecialprice.Text == "")
            {
                XtraMessageBox.Show("Please Input Valid Fields");
                return;
            }
            else
            {
                stat = "YES";
                spprice = txtspecialprice.Text;
                isconfirmed = true;
                this.Close();
            }
           
        }

        private void POSSpecialPrice_Load(object sender, EventArgs e)
        {
            txtspecialprice.Focus();
        }
    }
}
