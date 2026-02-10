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

namespace SalesInventorySystem.Samples
{
    public partial class SamplePlayers : DevExpress.XtraEditors.XtraForm
    {
        public SamplePlayers()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_Click(object sender, EventArgs e)
        {
           
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        void showBettingTransactions()
        {
            //// 1. Get the raw value from the grid
            //string rawValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PlayerId").ToString();

            //// 2. Check for DBNull and handle it
            ////long managerId = (rawValue == null || rawValue == DBNull.Value) ? 0 : Convert.ToInt64(rawValue);

            //// 3. Now build your query
            //if (Convert.ToInt64(rawValue) > 0)
            //{
            //    string sql = $@"SELECT  ""PlayerTransactionId"", ""TotalNoOfBet"", ""GameName"", ""TransactionNo"", ""Amount"", ""WinAmount"",""DateOfTransaction""
            //        FROM public.""PlayerTransaction"" where ""PlayerId""={rawValue}";


            //    Database.displayPg(sql, gridControl2, gridView2);
            //    gridView2.Columns[0].Visible = false;
            //}
            //else
            //{
            //    XtraMessageBox.Show("Please select a valid Manager.");
            //}            
          
            Database.display($"SELECT * FROM dbo.func_getBettingTransactions('{txtdatefrom.Text}','{txtdateto.Text}','','','')",gridControl2,gridView2);
        }

        private void showBettingTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showBettingTransactions();
            lblCaption.Text = "BETTING TRANSACTIONS";
        }

        private void SamplePlayers_Load(object sender, EventArgs e)
        {
            lblCaption.Text = "";
            txtdatefrom.Text = HelperFunction.GetPreviousMonthSameDay(DateTime.Today).ToShortDateString();
            txtdateto.Text = DateTime.Today.ToShortDateString();
        }

        private void showCreditLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblCaption.Text = "CREDIT LEDGER";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

        }
    }
}