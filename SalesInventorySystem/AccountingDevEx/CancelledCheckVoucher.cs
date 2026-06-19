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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class CancelledCheckVoucher : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public static string reason = "";
        public CancelledCheckVoucher()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtreason.Text))
            {
                XtraMessageBox.Show("Reversal reason is required.");
                return;
            }
            else
            {
                reason = txtreason.Text;
                isdone = true;
                this.Close();
            }
        }
    }
}