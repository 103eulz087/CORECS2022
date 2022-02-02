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

namespace SalesInventorySystem
{
    public partial class ViewTransactionHistoryFrm : DevExpress.XtraEditors.XtraForm
    {
        string orderno, invoiceno, totalitem, totalamount, paymenttype;

        public static string transcode,refnum,amountpayable, amounttendered, amountchange,vatsale,vatexemptsale,vatablesale;

        private void datetoforapproval_ValueChanged(object sender, EventArgs e)
        {
            //filtertab();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_batchTransactionSummary WHERE TransDate >= '" + datefromforapproval.Text + "' and TransDate <= '" + datetoforapproval.Text + "' and Cashier='" + Login.isglobalUserID + "' and BranchCode='" + Login.assignedBranch + "'", gridControl2, gridView2);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            transcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransactionCode").ToString();
            refnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNo").ToString();
            POS.ViewTransactionHistoryDetailsFrm viewhus = new POS.ViewTransactionHistoryDetailsFrm();
            viewhus.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_detailTransactionHistory WHERE BranchCode='"+Login.assignedBranch+"' and CAST(DateOrder as date) >= '"+datefromcancelledtran.Text+ "' and CAST(DateOrder as date) <= '" + datetocancelledtran.Text + "' and (Status='CANCELLED' OR isCancelled='1') ", gridControl3,gridView3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_detailTransactionHistory WHERE BranchCode='" + Login.assignedBranch + "' and CAST(DateOrder as date) >= '" + datefromvoidtran.Text + "' and CAST(DateOrder as date) <= '" + datetovoidtran.Text + "' and (Status='VOID' OR isVoid='1') ", gridControl4, gridView4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_detailTransactionHistory WHERE BranchCode='" + Login.assignedBranch + "' and CAST(DateOrder as date) >= '" + datefromvoidtran.Text + "' and CAST(DateOrder as date) <= '" + datetovoidtran.Text + "' and (Status='RETURNED' OR isErrorCorrect='1') ", gridControl5, gridView5);

        }

        private void datefromforapproval_ValueChanged(object sender, EventArgs e)
        {
            //filtertab();
        }

        public ViewTransactionHistoryFrm()
        {
            InitializeComponent();
        }
        private void ViewTransactionHistoryFrm_Load(object sender, EventArgs e)
        {
            filtertab();
            tabControl1.TabPages[2].Hide();
            tabControl1.TabPages[3].Hide();
        }

        private void filtertab()
        {

            if (tabControl1.SelectedTab.Equals(tabPage1))
            {
                Database.display("SELECT * FROM view_batchTransactionSummary WHERE ReferenceNo='" + txtrefno.Text + "' ", gridControl1, gridView1);
            }
            else if (tabControl1.SelectedTab.Equals(tabPage2))
            {
                Database.display("SELECT * FROM view_batchTransactionSummary WHERE TransDate >= '" + datefromforapproval.Text + "' and TransDate <= '" + datetoforapproval.Text + "' and Cashier='" + Login.isglobalUserID + "' and BranchCode='" + Login.assignedBranch + "'", gridControl2, gridView2);
            }
          
        }
        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl2, e.Location);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionCode").ToString();
            refnum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNo").ToString();
            vatsale = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalVATSale").ToString();
            vatexemptsale = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalVATExemptSale").ToString();
            vatablesale = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalVatableSale").ToString();
            amountpayable = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalAmount").ToString();
            amounttendered = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountTendered").ToString();
            amountchange = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountChange").ToString();
            POS.ViewTransactionHistoryDetailsFrm viewhus = new POS.ViewTransactionHistoryDetailsFrm();
            viewhus.ShowDialog(this);
        }

        private void reprintORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Printing printit = new Printing();
            //string transno, orderno;
            //transno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionCode").ToString();
            //orderno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNo").ToString();
            //printit.ReprintReceipt(transno, orderno, txtamountpayable.Text, vatablesales, vatexemptsales, vat, amounttender, change, PointOfSale.mygridview);

        }

        

        private void txtrefno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                orderno = Database.getSingleData("BatchSalesSummary","ReferenceNo",txtrefno.Text,"ReferenceNo");
                invoiceno = Database.getSingleData("BatchSalesSummary", "ReferenceNo", txtrefno.Text,"Invoice");
                totalitem = Database.getSingleData("BatchSalesSummary", "ReferenceNo", txtrefno.Text, "TotalItem");
                totalamount = Database.getSingleData("BatchSalesSummary", "ReferenceNo", txtrefno.Text, "SubTotal");
                amounttendered = Database.getSingleData("BatchSalesSummary", "ReferenceNo", txtrefno.Text, "AmountTendered");
                amountchange = Database.getSingleData("BatchSalesSummary", "ReferenceNo", txtrefno.Text, "AmountChange");
                paymenttype = Database.getSingleData("BatchSalesSummary", "ReferenceNo", txtrefno.Text, "PaymentType");
                txtorderno.Text = orderno;
                txtinvoiceno.Text = invoiceno;
                txttotalitem.Text = totalitem;
                txttotalamount.Text = totalamount;
                txtamounttendered.Text = amounttendered;
                txtamountchange.Text = amountchange;
                txtpaymenttype.Text = paymenttype;
                display();
            }
        }

        private void display()
        {
            Database.display("SELECT * FROM view_batchTransactionSummary WHERE ReferenceNo='" + txtrefno.Text + "' ", gridControl1, gridView1);
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            transcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionCode").ToString();
            refnum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNo").ToString();
            vatsale = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalVATSale").ToString();
            vatexemptsale = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalVATExemptSale").ToString();
            vatablesale = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalVatableSale").ToString();
            amountpayable = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TotalAmount").ToString();
            amounttendered = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountTendered").ToString();
            amountchange = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AmountChange").ToString();
            POS.ViewTransactionHistoryDetailsFrm viewhus = new POS.ViewTransactionHistoryDetailsFrm();
            viewhus.ShowDialog(this);
        }

       

        private void displayTransactions()
        {
            datefromforapproval.Text = DateTime.Now.ToShortDateString();
            datetoforapproval.Text = DateTime.Now.ToShortDateString();
            Database.display("SELECT * FROM view_batchTransactionSummary WHERE TransDate >= '"+datefromforapproval.Text+"' and TransDate <= '"+datetoforapproval.Text+"' and Cashier='"+Login.isglobalUserID + "' and BranchCode='" + Login.assignedBranch+"'", gridControl2, gridView2);
            //Database.display("SELECT * FROM view_detailTransactionHistory", gridControl3, gridView3);
            //Database.display("SELECT * FROM view_voidandCancelledTransaction Where isVoid='1' OR isCancelled='1' ", gridControl4, gridView4);
        }
    }
}