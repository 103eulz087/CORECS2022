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
    public partial class POSEncodeCustomerInfo : Form
    {
        public static bool isdone = false;
        public static string custname, custaddress, custtin, custbusiness;
        public POSEncodeCustomerInfo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            txtcustnamercpt.Focus();
        }

        void add()
        {
            custname = txtcustnamercpt.Text;
            custaddress = txtcustaddressrcpt.Text;
            custtin = txtcusttinrcpt.Text;
            custbusiness = txtcustbussstyle.Text;
            isdone = true;
        }

        void clear()
        {
            txtcustaddressrcpt.Text = "";
            txtcustbussstyle.Text = "";
            txtcustnamercpt.Text = "";
            txtcusttinrcpt.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcustaddressrcpt.Text) || String.IsNullOrEmpty(txtcustbussstyle.Text) || String.IsNullOrEmpty(txtcustnamercpt.Text) || String.IsNullOrEmpty(txtcusttinrcpt.Text))
            {
                XtraMessageBox.Show("All Fields are Mandatory!");
                return;
            }
            else
            {
                add();
                this.Close();
            }
        }
    }
}
