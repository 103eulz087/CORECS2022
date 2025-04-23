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
    
    public partial class POSAddQty : Form
    {
        public static bool isdone = false;
        public static string strqty = "0";
        public POSAddQty()
        {
            InitializeComponent();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                this.Dispose();
            }
            else if (keyData == Keys.Enter) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                exec();
            }
            else if (keyData == (Keys.F | Keys.Control)) //POS SETTINGS
            {
                txtqty.Focus();
            }
            return functionReturnValue;
        }

        private void POSAddQty_Load(object sender, EventArgs e)
        {
            txtqty.Focus();
        }

        void exec()
        {
            strqty = "0";
            strqty = txtqty.Text.Replace(",", "");
            if (Convert.ToDouble(strqty) > 99999)
            {
                XtraMessageBox.Show("Quantity must not greater than 99999");
                return;
            }
            else
            {
                isdone = true;
                this.Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            exec();
        }
    }
}
