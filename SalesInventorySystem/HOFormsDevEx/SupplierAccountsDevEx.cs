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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class SupplierAccountsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string supplierid, suppliername,referenceKey;
        public SupplierAccountsDevEx()
        {
            InitializeComponent();
        }

        double getBalance()
        {
            double total = 0.0, purchasetotal=0.0;
            //double purchasetotal = Database.getTotalSummation2("APACCOUNTS","SupplierID='"+ Classes.Suppliers.getSupplierID(searchLookUpEdit1.Text) + "' AND PayStatus <> 'FULLYPAID' ","Balance");
            //double expensetotal = Database.getTotalSummation2("ExpenseMaster","SupplierID='"+ Classes.Suppliers.getSupplierID(searchLookUpEdit1.Text) + "' AND Status <> 'FULLYPAID' AND isErrorCorrect=0 ", "Balance");
            string expensetotal = Database.getSingleQuery("SELECT TOP(1) AccountBalance FROM dbo.SupplierAccounts WHERE SupplierID='" + Classes.Suppliers.getSupplierID(searchLookUpEdit1.Text) + "' ", "AccountBalance");
            total = purchasetotal + Convert.ToDouble(expensetotal);
            return Math.Round(total, 2);
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            txtacctid.Text = Classes.Suppliers.getSupplierID(searchLookUpEdit1.Text);
            txtacctstatus.Text = Classes.Suppliers.getSupplierStatus(searchLookUpEdit1.Text);
            txtacctbalance.Text = getBalance().ToString();//Classes.Suppliers.getSupplierBalance(searchLookUpEdit1.Text);
            txtmvmtdate.Text = Classes.Suppliers.getSupplierLastMovementDate(searchLookUpEdit1.Text);
        }

        private void btnget_Click(object sender, EventArgs e)
        {
            loadLedger();
        }
        void loadLedger()
        {
            //Database.display("SELECT * FROM view_APLedger WHERE SupplierID='" + txtacctid.Text + "' AND CAST(TransactionDate as Date) >= '" + dateTimePicker1.Text + "' and CAST(TransactionDate as date)<= '" + dateTimePicker2.Text + "' ORDER BY TRN_SEQ_NO", gridControl2, gridView2);
            string query = "SELECT * FROM view_APLedger WHERE SupplierID='" + txtacctid.Text + "' AND CAST(TransactionDate as Date) >= '" + dateTimePicker1.Text + "' and CAST(TransactionDate as date)<= '" + dateTimePicker2.Text + "' ORDER BY TRN_SEQ_NO ";
            HelperFunction.ShowWaitAndDisplay(query, gridControl2, gridView2, "Please wait", "Populating data into the database...");
            gridView2.Focus();
            gridView2.Columns["SupplierID"].Visible = false;
            gridView2.Columns["SupplierID"].OptionsColumn.ShowInCustomizationForm = true;
            gridView2.Columns["PostingDate"].Visible = false;
            gridView2.Columns["PostingDate"].OptionsColumn.ShowInCustomizationForm = true;
            gridView2.Columns["PostingDate"].Visible = false;
            gridView2.Columns["PostingDate"].OptionsColumn.ShowInCustomizationForm = true;
            gridView2.Columns["TransCode"].Visible = false;
            gridView2.Columns["TransCode"].OptionsColumn.ShowInCustomizationForm = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadPurchases();
        }
        void loadPurchases()
        {
            if (checkBox2.Checked == true)
            {
                string query = "SELECT * FROM vw_SupplierPurchases WHERE SupplierID='" + txtacctid.Text + "' ORDER BY SequenceNo DESC ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                gridView1.Focus();
            }
            else
            {
                string query = "SELECT * FROM vw_SupplierPurchases WHERE SupplierID='" + txtacctid.Text + "' AND CAST(InvoiceDate as Date) between '" + datefrompurch.Text + "' and '" + datetopurch.Text + "' ORDER BY SequenceNo ASC ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                gridView1.Focus();
            }
        }

        void loadPayments()
        {
            if (checkBox3.Checked == true)
            {
                string query = "SELECT SEQ_NO,DatePaid,ReferenceNumber,Amount,VoucherType,ExecuteBy,DateUpdate,UpdateBy,ErrorCorrect FROM TransactionPaymentAP WHERE SupplierKey='" + txtacctid.Text + "' ORDER BY SEQ_NO ASC";
                HelperFunction.ShowWaitAndDisplay(query, gridControl3, gridView3, "Please wait", "Populating data into the database...");
                gridView3.Focus();
            }
            else
            {
                string query = "SELECT SEQ_NO,DatePaid,ReferenceNumber,Amount,VoucherType,ExecuteBy,DateUpdate,UpdateBy,ErrorCorrect FROM TransactionPaymentAP WHERE SupplierKey='" + txtacctid.Text + "' AND CAST(DatePaid as Date) >= '" + datefrompay.Text + "' and CAST(DatePaid as date)<= '" + datetopay.Text + "' ORDER BY SEQ_NO ASC";
                HelperFunction.ShowWaitAndDisplay(query, gridControl3, gridView3, "Please wait", "Populating data into the database...");
                gridView3.Focus();
            }
        }
        void loadExpenses()
        {
            if (checkBox4.Checked == true)
            {
                string query = "SELECT TRN_SEQ_NO,BranchCode,ExpenseDate,ReferenceNumber,InvoiceNo,ExpenseName,Amount,Remarks,Status,Balance,AmountPaid,EWTAmount,DiscountAmount,OffsetAmount,isErrorCorrect FROM ExpenseMaster WHERE SupplierID='" + txtacctid.Text + "' ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl4, gridView4, "Please wait", "Populating data into the database...");
                gridView4.Focus();
            }
            else
            {
                string query = "SELECT TRN_SEQ_NO,BranchCode,ExpenseDate,ReferenceNumber,InvoiceNo,ExpenseName,Amount,Remarks,Status,Balance,AmountPaid,EWTAmount,DiscountAmount,OffsetAmount,isErrorCorrect FROM ExpenseMaster WHERE SupplierID='" + txtacctid.Text + "' AND CAST(ExpenseDate as Date) >= '" + expdatefrom.Text + "' and CAST(ExpenseDate as date)<= '" + expdateto.Text + "' ";
                HelperFunction.ShowWaitAndDisplay(query, gridControl4, gridView4, "Please wait", "Populating data into the database...");
                gridView4.Focus();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadPayments();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                datetopay.Enabled = false;
                datefrompay.Enabled = false;
            }
            else
            {
                datetopay.Enabled = true;
                datefrompay.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadExpenses();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                expdatefrom.Enabled = false;
                expdateto.Enabled = false;
            }
            else
            {
                expdatefrom.Enabled = true;
                expdateto.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            supplierid = txtacctid.Text;
            suppliername = searchLookUpEdit1.Text;
            HOForms.TransactionPayment adhipma = new HOForms.TransactionPayment();
            adhipma.Show();
            if (HOForms.TransactionPayment.isdone == true)
            {
                //searchLookUpEdit1.Text = "";
                txtacctid.Text = "";
                txtacctbalance.Text = "";
                txtacctstatus.Text = "";
                txtmvmtdate.Text = "";
                txtacctid.Text = Classes.Suppliers.getSupplierID(adhipma.txtsuppliername.Text);
                txtacctstatus.Text = Classes.Suppliers.getSupplierStatus(adhipma.txtsuppliername.Text);
                txtacctbalance.Text = Classes.Suppliers.getSupplierBalance(adhipma.txtsuppliername.Text);
                txtmvmtdate.Text = Classes.Suppliers.getSupplierLastMovementDate(adhipma.txtsuppliername.Text);

                HOForms.TransactionPayment.isdone = false;
                adhipma.Dispose();
            }
        }

        private void SupplierAccountsDevEx_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            dateTimePicker1.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            //var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateTimePicker2.Text = lastDay.ToShortDateString();

            datefrompurch.Text = date.ToShortDateString();
            datetopurch.Text = lastDay.ToShortDateString();

            datefrompay.Text = date.ToShortDateString();
            datetopay.Text = lastDay.ToShortDateString();

            expdatefrom.Text = date.ToShortDateString();
            expdateto.Text = lastDay.ToShortDateString();
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", searchLookUpEdit1, "SupplierName", "SupplierName");
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string lastcheque = Database.getSingleQuery("SELECT CheckNo FROM CheckVoucher WHERE SequenceNumber=(SELECT MAX(SequenceNumber) FROM CheckVoucher)", "CheckNo");
            supplierid = txtacctid.Text;
            suppliername = txtclientname.Text;//searchLookUpEdit1.Text;
            HOFormsDevEx.SupplierPaymentDevEx adhipma = new HOFormsDevEx.SupplierPaymentDevEx();
            adhipma.Show();
            adhipma.txtsupplierid.Text = supplierid;
            adhipma.txtsuppliername.Text = suppliername;
            adhipma.txtlastchecknum.Text = lastcheque;
            if (HOFormsDevEx.SupplierPaymentDevEx.isdone == true)// (HOForms.TransactionPayment.isdone == true)
            {
                //searchLookUpEdit1.Text = "";
                txtacctid.Text = "";
                txtacctbalance.Text = "";
                txtacctstatus.Text = "";
                txtmvmtdate.Text = "";
                txtacctid.Text = Classes.Suppliers.getSupplierID(adhipma.txtsuppliername.Text);
                txtacctstatus.Text = Classes.Suppliers.getSupplierStatus(adhipma.txtsuppliername.Text);
                txtacctbalance.Text = Classes.Suppliers.getSupplierBalance(adhipma.txtsuppliername.Text);
                txtmvmtdate.Text = Classes.Suppliers.getSupplierLastMovementDate(adhipma.txtsuppliername.Text);

                //HOForms.TransactionPayment.isdone = false;
                HOFormsDevEx.SupplierPaymentDevEx.isdone = false;
                adhipma.Dispose();
            }
        }

       
       
        
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            loadLedger();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            loadPurchases();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            loadPayments();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            loadExpenses();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStripLedger.Show(gridControl2, e.Location);
        }

        private void showTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ticketref = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TicketReference").ToString();
            string ticketreferenceno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNumber").ToString();
            string postingdate = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PostingDate").ToString();
            string transactiondate = Convert.ToDateTime(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TransactionDate").ToString()).ToShortDateString();
            string referenceNumber = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNumber").ToString(); //referenceKey InvoiceNo
            //string ticketbrcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TicketReference").ToString().Substring(8, 3).Trim();
            //string ticketno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TicketReference").ToString().Substring(11, 5).Trim();
            //string ticketrefno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "TicketReference").ToString().Substring(16, 5).Trim();
            Accounting.ViewTicketDetails viewtick = new Accounting.ViewTicketDetails();

            Database.display("SELECT * FROM view_AccountingTicketReports WHERE TicketNumber='" + ticketref + "' AND TicketDate='" + postingdate + "' and ReferenceNumber='" + ticketreferenceno + "'  ORDER BY TicketNumber ", viewtick.gridControl1, viewtick.gridView1);
            //Database.GridMasterDetail("TicketMaster", "TicketDetails", "BranchCode='" + Accounting.GLSummary.branchcode + "'", "AccountCode='" + Accounting.GLSummary.accountcode + "' and TicketDate = '" + Accounting.GLSummary.postingdate + "' and BranchCode='" + Accounting.GLSummary.branchcode + "'", "TicketNumber", "TicketNumber", "TicketMasterDetails", gridControl1);
            viewtick.ShowDialog(this);
        }

        private void gridControl4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStripExpenses.Show(gridControl4, e.Location);
        }

        private void toolStripMenuItemExpensesShowDetails_Click(object sender, EventArgs e)
        {
            //EXPENSE MASTER AND SUPPLIER LEDGER
            //show ledger
            //must used gridview to display then rightclick the gridview to display tickets
            //string ticketrefno = Database.getSingleQuery("SupplierLedger", "InvoiceNo='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "InvoiceNo").ToString() + "' AND ReferenceNumber='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "TRN_SEQ_NO").ToString() + "'", "TicketReference");

        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuTabPurchases.Show(gridControl1, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Accounting.ViewTicketDetails viewtick = new Accounting.ViewTicketDetails();
            Database.display("SELECT * FROM view_AccountingTicketReports WHERE ReferenceKey='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceNo").ToString() + "' and ReferenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNumber").ToString() + "' And TicketDate='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceDate").ToString() + "' ", viewtick.gridControl1, viewtick.gridView1);
            viewtick.ShowDialog(this);
        }

        private void gridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)   
                contextMenuTabPayments.Show(gridControl3, e.Location);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Accounting.ViewTicketDetails viewtick = new Accounting.ViewTicketDetails();
            if (gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "VoucherType").ToString()=="CHECK")
            {
                referenceKey = Database.getSingleQuery("CheckVoucher","SupplierID='"+txtacctid.Text+"' " +
                    "AND ReferenceNumber='"+ gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ReferenceNumber").ToString() + "' ","CheckNo");

                Database.display("SELECT * FROM view_AccountingTicketReports " +
                "WHERE ReferenceNumber='" + gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ReferenceNumber").ToString() + "' " +
                "AND ReferenceKey='" + referenceKey + "'" +
                "And TicketDate='" + Convert.ToDateTime(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "DatePaid").ToString()).ToShortDateString() + "' ", viewtick.gridControl1, viewtick.gridView1);
            }
            else
            {
                Database.display("SELECT * FROM view_AccountingTicketReports " +
                "WHERE ReferenceNumber='" + gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ReferenceNumber").ToString() + "' " +
                "And TicketDate='" + Convert.ToDateTime(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "DatePaid").ToString()).ToShortDateString() + "' ", viewtick.gridControl1, viewtick.gridView1);
            }
            
            
            viewtick.ShowDialog(this);
        }

        private void gridView4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuTabExpenses.Show(gridControl4, e.Location);
        }

        private void gridView4_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "isErrorCorrect"));
            if (check)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierAddPaymentDevEx suppadd = new SupplierAddPaymentDevEx();
            suppadd.txtactualcost.Properties.ReadOnly = false;
            suppadd.txtbalance.Properties.ReadOnly = false;
            suppadd.btnSave.Visible = false;
            suppadd.btnupdate.Visible = true;
            suppadd.groupControl1.Text = txtacctid.Text;// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            suppadd.txtshipno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            suppadd.txtinvoiceno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceNo").ToString();
            suppadd.txtinvoicedate.Text = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceDate").ToString()).ToShortDateString();
            suppadd.txtactualcost.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ActualCost").ToString();
            suppadd.txtbalance.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ActualCost").ToString();
            suppadd.ShowDialog(this);
            if(SupplierAddPaymentDevEx.isdone == true)
            {
                SupplierAddPaymentDevEx.isdone = false;
                suppadd.Dispose();
                simpleButton2.PerformClick();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Accounting.ViewTicketDetails viewtick = new Accounting.ViewTicketDetails();
            Database.display("SELECT * FROM view_AccountingTicketReports WHERE ReferenceKey='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "InvoiceNo").ToString() + "' and ReferenceNumber='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ReferenceNumber").ToString() + "' And TicketDate='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ExpenseDate").ToString() + "' ", viewtick.gridControl1, viewtick.gridView1);
            viewtick.ShowDialog(this);
        }

        private void showSpecificTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.ViewTicketDetails viewtick = new Accounting.ViewTicketDetails();
            Database.display("SELECT * FROM view_AccountingTicketReports WHERE ReferenceKey='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "InvoiceNo").ToString() + "' and ReferenceNumber='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ReferenceNumber").ToString() + "' And TicketDate='" + gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ExpenseDate").ToString() + "' ", viewtick.gridControl1, viewtick.gridView1);
            viewtick.ShowDialog(this);
        }
    }
}