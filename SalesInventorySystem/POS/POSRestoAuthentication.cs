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
    public partial class POSRestoAuthentication : Form
    {
        string num = "";
        public static bool isconfirmedLogin = false;
        public static string waiterid = "";
        public POSRestoAuthentication()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Enter) //PAYMENT
            {
                simpleButton1.PerformClick();
            }
            //else if (keyData == (Keys.D | Keys.Control)) //FOCUS TO SKU TEXTFIELD (keyData == (Keys.O | Keys.Control))
            //{
            //    //textEdit3.Focus();
            //    POS.POSScreenMirror posmir = new POS.POSScreenMirror();
            //    posmir.Show();
            //}

            return functionReturnValue;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool isExist = Database.checkifExist("Select TOP(1) WaiterID FROM dbo.Waiters WHERE WaiterID='" + txtuserid.Text + "'");
            if (isExist)
            {
                waiterid = txtuserid.Text;
                isconfirmedLogin = true;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("No Records Found!");
                num = "";
                txtuserid.Focus();
                txtuserid.SelectionStart = 0;
                txtuserid.SelectionLength = txtuserid.Text.Length;
                return;
            }
        }

        private void POSRestoAuthentication_Load(object sender, EventArgs e)
        {
            txtuserid.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            num = num + "1";
            txtuserid.Text = num;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            num = num + "2";
            txtuserid.Text = num;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            num = num + "3";
            txtuserid.Text = num;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            num = num + "4";
            txtuserid.Text = num;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            num = num + "5";
            txtuserid.Text = num;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            num = num + "6";
            txtuserid.Text = num;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            num = num + "7";
            txtuserid.Text = num;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            num = num + "8";
            txtuserid.Text = num;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            num = num + "9";
            txtuserid.Text = num;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            num = num + "0";
            txtuserid.Text = num;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            num = "";
            txtuserid.Text = "";
            txtuserid.Focus();
        }
    }
}
