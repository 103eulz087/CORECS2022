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
    public partial class POSConnectionSettings : Form
    {
        public static string spValue = "sp_AddSalesInvoiceOnline";
        public static bool isdone=false;
        public POSConnectionSettings()
        {
            InitializeComponent();
        }

        private void POSConnectionSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                spValue = "sp_AddSalesInvoiceOnline";
            }
            else
            {
                spValue = "sp_AddSalesInvoiceOffline";
            }
            isdone = true;
            this.Dispose();
        }
    }
}
