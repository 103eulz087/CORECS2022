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
            if (checkBox1.Checked == false)
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0", gridControl1, gridView1);
                Database.display("SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM SupplierAccounts WHERE AccountBalance > 0", gridControl1, gridView1);
            else
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0 AND SupplierID='" + searchLookUpEdit1.Text+"'", gridControl1, gridView1);
                Database.display("SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM SupplierAccounts WHERE AccountBalance > 0 AND SupplierID='" + searchLookUpEdit1.Text + "'", gridControl1, gridView1);
        }
        void populateCOA()
        {
            if (checkBox1.Checked == true)
                Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM SupplierAccounts", searchLookUpEdit1, "SupplierID", "SupplierID");
            if (checkBox2.Checked == true)
                Database.displaySearchlookupEdit("SELECT AccountID,AccountName FROM ClientAccounts", searchLookUpEdit2, "AccountID", "AccountID");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                searchLookUpEdit1.Enabled = true;
                Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM SupplierAccounts", searchLookUpEdit1, "SupplierID", "SupplierID");
            }

            else
                searchLookUpEdit1.Enabled = false;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            HOFormsDevEx.AccountPayablesDetailsDevEx acctdev = new HOFormsDevEx.AccountPayablesDetailsDevEx();
            acctdev.Show();
            Database.display("SELECT SupplierID,SupplierName,InvoiceNo,FORMAT(ActualCost,'N', 'en-us') as InvoiceAmount,InvoiceDate,DueDate,PaymentStatus,FORMAT(AmountPaid,'N', 'en-us') as AmountPaid,FORMAT(Balance,'N', 'en-us') as Balance FROM ShipmentOrder WHERE SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "'", acctdev.gridControl1, acctdev.gridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayReceivables();

        }
        void displayReceivables()
        {
            if (checkBox2.Checked == false)
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0", gridControl1, gridView1);
                Database.display("SELECT AccountID,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM ClientAccounts WHERE AccountBalance > 0", gridControl2, gridView2);
            else
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0 AND SupplierID='" + searchLookUpEdit1.Text+"'", gridControl1, gridView1);
                Database.display("SELECT AccountID,AccountName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM ClientAccounts WHERE AccountBalance > 0 AND AccountID='" + searchLookUpEdit2.Text + "'", gridControl2, gridView2);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                searchLookUpEdit2.Enabled = true;
                Database.displaySearchlookupEdit("SELECT AccountID,AccountName FROM ClientAccounts", searchLookUpEdit2, "AccountID", "AccountID");
            }

            else
                searchLookUpEdit2.Enabled = false;
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            HOFormsDevEx.AccountReceivablesDevEx acctdev = new HOFormsDevEx.AccountReceivablesDevEx();
            acctdev.Show();
            Database.display("SELECT CustomerID,CAST(TransactionDate as date) as TransactionDate,OrderNo,FORMAT(Amount,'N', 'en-us') as Amount,FORMAT(AmountPaid,'N', 'en-us') as AmountPaid,FORMAT(Balance,'N', 'en-us') as Balance,PaymentStatus FROM TransactionChargeSales WHERE CustomerID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountID").ToString() + "'", acctdev.gridControl1, acctdev.gridView1);

        }

        private void AccountMasterListDevEx_Load(object sender, EventArgs e)
        {

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
    }
}