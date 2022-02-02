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
    public partial class ViewTransactionHistoryDetailsFrm : Form
    {
        public ViewTransactionHistoryDetailsFrm()
        {
            InitializeComponent();
        }

        private void ViewTransactionHistoryDetailsFrm_Load(object sender, EventArgs e)
        {
            Database.displayLocalGrid("SELECT * FROM view_detailTransactionHistory WHERE ReferenceNo='"+ ViewTransactionHistoryFrm.refnum+"' ", dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Printing printit = new Printing();
            printit.ReprintReceipt(ViewTransactionHistoryFrm.transcode, ViewTransactionHistoryFrm.refnum, ViewTransactionHistoryFrm.amountpayable, ViewTransactionHistoryFrm.vatablesale, ViewTransactionHistoryFrm.vatexemptsale, ViewTransactionHistoryFrm.vatsale, ViewTransactionHistoryFrm.amounttendered, ViewTransactionHistoryFrm.amountchange, dataGridView1);
            //ReprintReceipt(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview)
            XtraMessageBox.Show("Print Successfull");
            this.Dispose();
        }
    }
}
