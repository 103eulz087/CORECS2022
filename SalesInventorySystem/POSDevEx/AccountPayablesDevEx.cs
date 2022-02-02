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

namespace SalesInventorySystem.POSDevEx
{
    public partial class AccountPayablesDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AccountPayablesDevEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void AccountPayablesDevEx_Load(object sender, EventArgs e)
        {

        }

        void display()
        {
            if(checkBox1.Checked==false)
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0", gridControl1, gridView1);
                Database.display("SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM SupplierAccounts WHERE AccountBalance > 0", gridControl1, gridView1);
            else
                // Database.display("SELECT SupplierID,SupplierName,InvoiceNo,DueDate,ActualCost,Balance,AmountPaid FROM ShipmentOrder WHERE PaymentStatus='UNPAID' OR PaymentStatus='PARTIAL' AND Balance > 0 AND SupplierID='" + searchLookUpEdit1.Text+"'", gridControl1, gridView1);
                Database.display("SELECT SupplierID,SupplierName,FORMAT(AccountBalance,'N', 'en-us') as AccountBalance,LastMovementDate FROM SupplierAccounts WHERE AccountBalance > 0 AND SupplierID='" + searchLookUpEdit1.Text + "'", gridControl1, gridView1);
        }
        void populateCOA()
        {
            if(checkBox1.Checked==true)
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM SupplierAccounts", searchLookUpEdit1, "SupplierID", "SupplierID");
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
            Database.display("SELECT SupplierID,SupplierName,InvoiceNo,FORMAT(ActualCost,'N', 'en-us') as InvoiceAmount,InvoiceDate,DueDate,PaymentStatus,FORMAT(AmountPaid,'N', 'en-us') as AmountPaid,FORMAT(Balance,'N', 'en-us') as Balance FROM ShipmentOrder WHERE SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"SupplierID").ToString()+"'", acctdev.gridControl1, acctdev.gridView1);
        }
    }
}