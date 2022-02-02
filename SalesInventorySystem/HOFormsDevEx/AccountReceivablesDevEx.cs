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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AccountReceivablesDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AccountReceivablesDevEx()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void showOrderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ARViewOrderDetDevEx viewdet = new ARViewOrderDetDevEx();
            viewdet.Show();
           // string query = "SELECT ReferenceNo as OrderNo,Description,QtySold,SellingPrice,TotalAmount,CAST(DateOrder as date) as DateOrder FROM BatchSalesDetails WHERE ReferenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderNo").ToString() + "' AND BranchCode='888'";
            Database.display("SELECT ReferenceNo as OrderNo,Description,QtySold,SellingPrice,TotalAmount,CAST(DateOrder as date) as DateOrder FROM BatchSalesDetails WHERE ReferenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"OrderNo").ToString()+"' AND BranchCode='888'",viewdet.gridControl1,viewdet.gridView1);
        }

        private void showPaymentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.ARViewPayHistoryDevEx viewdet = new ARViewPayHistoryDevEx();
            viewdet.Show();
            Database.display("SELECT a.CheckNo,a.CheckName,a.CheckBankName,a.CheckDate,a.CustomerNo,b.OrderNo,b.Description,b.Balance,b.AmountPaid,b.Status FROM TransactionCheque as a INNER JOIN TransactionChequeDetails as b ON a.TransactionNo=b.TransactionNo WHERE b.OrderNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderNo").ToString() + "' AND a.BranchCode='888'", viewdet.gridControl1, viewdet.gridView1);

        }
    }
}