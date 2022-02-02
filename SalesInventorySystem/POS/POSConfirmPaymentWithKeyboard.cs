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
    public partial class POSConfirmPaymentWithKeyboard : Form
    {
        string btntext = "";
        public POSConfirmPaymentWithKeyboard()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btntext += "1";
            txtamounttender.Text = btntext;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            btntext += "2";
            txtamounttender.Text = btntext;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            btntext += "3";
            txtamounttender.Text = btntext;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            txtamounttender.Text = "";
            btntext = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(txtamounttender.Text.Contains("."))
            {
                txtamounttender.Focus();
                txtamounttender.SelectionStart = txtamounttender.SelectionLength;
                //txtamounttender.SelectionLength = txtamounttender.Text.Length;
            }
            else
            {
                btntext += ".";
                txtamounttender.Text = btntext;
            }
           
            //int textlength = txtamounttender.Text.Length;
            //if(textlength==0)
            //{
            //    txtamounttender.Text = "";
            //}
            //else
            //{
            //    string textminus1 = txtamounttender.Text.Substring(0, textlength - 1);
            //    txtamounttender.Text = textminus1;
            //    btntext = textminus1;
            //}
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btntext += "4";
            txtamounttender.Text = btntext;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            btntext += "5";
            txtamounttender.Text = btntext;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btntext += "6";
            txtamounttender.Text = btntext;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            btntext += "7";
            txtamounttender.Text = btntext;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            btntext += "8";
            txtamounttender.Text = btntext;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            btntext += "9";
            txtamounttender.Text = btntext;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            btntext += "0";
            txtamounttender.Text = btntext;
        }
    }
}
