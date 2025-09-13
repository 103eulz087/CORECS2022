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
    public partial class AccountMasterListDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AccountMasterListDevEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            if (chckzerobal.Checked == false && String.IsNullOrEmpty(txtapaccnt.Text))
            {

                string query = "SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,AccountStatus,LastMovementDate FROM SupplierAccounts with(nolock) WHERE AccountBalance > 0 ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                gridView1.Focus();
            }
            else if (chckzerobal.Checked == false && !String.IsNullOrEmpty(txtapaccnt.Text))
            {
                string query = "SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,AccountStatus,LastMovementDate FROM SupplierAccounts with(nolock) WHERE SupplierID='" + txtapaccnt.Text + "' ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                gridView1.Focus();
            }
            else
            {
                string query = "SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,AccountStatus,LastMovementDate FROM SupplierAccounts with(nolock) ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                gridView1.Focus();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            HOFormsDevEx.AccountPayablesDetailsDevEx acctdev = new HOFormsDevEx.AccountPayablesDetailsDevEx();
            acctdev.Show();
            Database.display("SELECT InvoiceNo,FORMAT(ActualCost,'N', 'en-us') as InvoiceAmount,InvoiceDate,DueDate,PayStatus,FORMAT(AmountPaid,'N', 'en-us') as AmountPaid,FORMAT(Balance,'N', 'en-us') as Balance FROM APAccounts WHERE SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "'", acctdev.gridControl1, acctdev.gridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayReceivables();

        }
        void displayReceivables()
        {
            if (chckzerobalar.Checked == false && String.IsNullOrEmpty(txtaraccount.Text))
                Database.display("SELECT AccountKey,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,AccountStatus,LastMovementDate FROM ClientAccounts with(nolock) WHERE AccountBalance > 0", gridControl2, gridView2);
            else if (chckzerobalar.Checked == false && !String.IsNullOrEmpty(txtaraccount.Text))
                Database.display("SELECT AccountKey,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,AccountStatus,LastMovementDate FROM ClientAccounts with(nolock) WHERE AccountKey='" + txtaraccount.Text + "'", gridControl2, gridView2);
            else
                Database.display("SELECT AccountKey,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,AccountStatus,LastMovementDate FROM ClientAccounts with(nolock) ", gridControl2, gridView2);

            //if (chckzerobalar.Checked == false)
            //    // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0", gridControl1, gridView1);
            //    Database.display("SELECT AccountID,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM ClientAccounts WHERE AccountBalance > 0", gridControl2, gridView2);
            //else
            //    // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0 AND SupplierID='" + searchLookUpEdit1.Text+"'", gridControl1, gridView1);
            //    Database.display("SELECT AccountID,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM ClientAccounts WHERE AccountBalance > 0 AND AccountID='" + txtaraccount.Text + "'", gridControl2, gridView2);
        }
        

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            HOFormsDevEx.AccountReceivablesDevEx acctdev = new HOFormsDevEx.AccountReceivablesDevEx();
            acctdev.Show();
            Database.display("SELECT CustomerID,CAST(TransactionDate as date) as TransactionDate,OrderNo,FORMAT(Amount,'N', 'en-us') as Amount,FORMAT(AmountPaid,'N', 'en-us') as AmountPaid,FORMAT(Balance,'N', 'en-us') as Balance,PaymentStatus FROM view_TransactionChargeSales WHERE CustomerID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountKey").ToString() + "'", acctdev.gridControl1, acctdev.gridView1);

        }

        private void AccountMasterListDevEx_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT SupplierKey,SupplierName FROM dbo.Supplier", txtapaccnt, "SupplierKey", "SupplierKey");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            displayLiquidations();
            gridView4.Columns["CheckAmount"].Summary.Clear();
            gridView4.Columns["CheckAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CheckAmount", "{0}");
            gridView4.Columns["LiquidationTotalAmount"].Summary.Clear();
            gridView4.Columns["LiquidationTotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "LiquidationTotalAmount", "{0}");
            gridView4.Columns["Variance"].Summary.Clear();
            gridView4.Columns["Variance"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Variance", "{0}");
            gridView4.BestFitColumns();

            //gridView5.OptionsBehavior.Editable = false;
            //gridView5.OptionsView.ColumnAutoWidth = false;
            //gridView5.OptionsView.RowAutoHeight = true;
            //gridView5.BestFitColumns();
        }
        void displayLiquidations()
        {
            if (checkBox3.Checked == false)
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0", gridControl1, gridView1);
                // Database.display("SELECT * FROM view_LiquidationMaster", gridControl3, gridView3);
                Database.GridMasterDetail("view_LiquidationMaster", "view_LiquidationDetails", "LiquidationID", "LiquidationID", "LiquidationDetails",gridControl3);
            else
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0 AND SupplierID='" + searchLookUpEdit1.Text+"'", gridControl1, gridView1);
                Database.display("SELECT * FROM view_LiquidationMaster WHERE SupplierID='" + searchLookUpEdit3.Text + "'", gridControl3, gridView4);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                searchLookUpEdit3.Enabled = true;
                Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM SupplierAccounts", searchLookUpEdit3, "SupplierID", "SupplierID");
            }

            else
                searchLookUpEdit3.Enabled = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            displayReceivables();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            displayLiquidations();
            gridView4.Columns["CheckAmount"].Summary.Clear();
            gridView4.Columns["CheckAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CheckAmount", "{0}");
            gridView4.Columns["LiquidationTotalAmount"].Summary.Clear();
            gridView4.Columns["LiquidationTotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "LiquidationTotalAmount", "{0}");
            gridView4.Columns["Variance"].Summary.Clear();
            gridView4.Columns["Variance"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Variance", "{0}");
            gridView4.BestFitColumns();

        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(gridControl1, e.Location);
            }
        }

        private void creditMemoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string suppid = "";
            suppid=gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            HOFormsDevEx.SupplierAccountsDevEx suppacctdev = new SupplierAccountsDevEx();
            suppacctdev.txtclientname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierName").ToString();
            suppacctdev.txtacctid.Text = suppid;
            suppacctdev.txtacctbalance.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountBalance").ToString();
            suppacctdev.txtacctstatus.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountStatus").ToString();
            suppacctdev.txtmvmtdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LastMovementDate").ToString();
            suppacctdev.ShowDialog(this);
        }

        private void gridView2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripAR.Show(gridControl2, e.Location);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string suppid = "";
            suppid = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountKey").ToString();
            HOFormsDevEx.ClientAccountsDevEx suppacctdev = new ClientAccountsDevEx();
            suppacctdev.txtacctname.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountName").ToString();
            suppacctdev.txtacctid.Text = suppid;
            suppacctdev.txtacctbalance.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountBalance").ToString();
            suppacctdev.txtacctstatus.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountStatus").ToString();
            //suppacctdev.txtmvmtdate.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "LastMovementDate").ToString();
            suppacctdev.ShowDialog(this);
        }
    }
}