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
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ClientAccountsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string custid, custname,custkey;
        Classes.ClientAccount ca = new Classes.ClientAccount();
        public ClientAccountsDevEx()
        {
            InitializeComponent();
        }

        private void ClientAccountsDevEx_Load(object sender, EventArgs e)
        {
            loadCustomers();
        }
        void loadCustomers()
        {
            Database.displaySearchlookupEdit("SELECT CustomerID,CustomerName FROM Customers ORDER BY CustomerName", searchLookUpEdit1, "CustomerName", "CustomerName");
        }

        void retrieveAccountDetails(string acctid)
        {
            ca.loadAccountDetails(acctid);
            txtacctid.Text = acctid;
            txtacctname.Text = ca.AccountName;
            txtacctstatus.Text = ca.AccountStatus;
            txtacctbalance.Text = ca.AccountBalance;
            txtmvmtdate.Text = ca.LastMovementDate;
        }

        void loadLedger()
        {
            Database.display("SELECT CAST(TransactionDate as date) as TransactionDate" +
                    //",CAST(TransactionDate as date) as TransactionDate" +
                    //",TransCode" +
                    ",Particulars" +
                    ",Debit" +
                    ",Credit " +
                    //",InvoiceNo " +
                "FROM ClientLedger " +
                "WHERE AccountID='" + txtacctid.Text + "' " +
                "AND CAST(TransactionDate as date) >= '" + datefromledge.Text + "' " +
                "and CAST(TransactionDate as date)<= '" + datetoledge.Text + "' ", gridControl2, gridView2);
        }

        double getCustBalance()
        {
            double balance = 0.0;
            balance = Database.getTotalSummation2("TransactionChargeSales", "CustomerKey='" + Classes.ClientAccount.getClientKey(txtacctid.Text) + "' AND PayStatus <> 'FULLYPAID' ", "Balance");
            return Math.Round(balance,2);
        }

        void loadTransactionSummary()
        {
           
            Database.display("SELECT " +
                "CAST(TransactionDate as date) as TransactionDate" +
                ",ReferenceNo" +
                ",InvoiceNo" +
                ",TotalAmount" +
                ",PayStatus" +
                ",AmountPaid" +
                ",Balance  " +
                ",EWTAmount  " +
                ",DiscountAmount  " +
                ",OffsetAmount  " +
                "FROM TransactionChargeSales " +
                "WHERE CustomerKey='" + txtacctid.Text + "' " +
                "AND TransactionDate between '" + dateFromTransSum.Text + "' AND '" + dateToTransSum.Text + "' ", gridControl1, gridView1);
        }
        void loadTransactionPayment()
        {
            Database.display("SELECT DatePaid" +
                ",ReferenceNo" +
                ",TransactedBy" +
                ",AmountPaid" +
                " FROM TransactionPayment " +
                "WHERE CustomerKey='" + txtacctid.Text + "' " +
                "AND CAST(DatePaid as date) >= '" + datepymntfrom.Text + "' and CAST(DatePaid as date)<= '" + datepymntto.Text + "' " +
                "ORDER BY SEQ_NO", gridControl3, gridView3);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            custid = txtacctid.Text;
            custname = txtacctname.Text;
            HOFormsDevEx.ClientPaymentsDevEx clientpayment = new HOFormsDevEx.ClientPaymentsDevEx();
            clientpayment.ShowDialog(this);
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            txtacctid.Text = Classes.ClientAccount.getClientID(searchLookUpEdit1.Text);
            txtacctname.Text = Classes.ClientAccount.getClientName(searchLookUpEdit1.Text);
            txtacctstatus.Text = Classes.ClientAccount.getClientStatus(searchLookUpEdit1.Text);
            txtacctbalance.Text = getCustBalance().ToString();// Classes.ClientAccount.getClientBalance(searchLookUpEdit1.Text);
            txtmvmtdate.Text = Classes.ClientAccount.getClientLastMovementDate(searchLookUpEdit1.Text);
            custkey = Classes.ClientAccount.getClientKey(txtacctid.Text);
            //loadLedger();
            loadTransactionSummary();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadTransactionPayment();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            loadLedger();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double getTotalPayments = 0.0, getTotalCredit = 0.0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                if (gridView2.GetRowCellValue(i, "TransCode").ToString() == "PYMT")
                {
                    getTotalPayments += Convert.ToDouble(gridView2.GetRowCellValue(i, "Credit").ToString());
                }

            }
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                if (gridView2.GetRowCellValue(i, "TransCode").ToString() == "CHRG")
                {
                    getTotalCredit += Convert.ToDouble(gridView2.GetRowCellValue(i, "Debit").ToString());
                }

            }
            double minpay = 0.0;

            string creditlimit = Database.getSingleQuery("Customers", "CustomerID='" + txtacctid.Text + "'", "CustomerCreditLimit");
            DevExReportTemplate.StatementOfAccount xct = new DevExReportTemplate.StatementOfAccount();
            xct.Landscape = false;

            xct.xrname.Text = txtacctname.Text;
            xct.xraddress.Text = Customers.getCustAddress(txtacctid.Text);
            xct.xraccountid1.Text = txtacctid.Text;
            xct.xrpaymentdate.Text = DateTime.Now.ToShortDateString();
            xct.xrcreditlimit.Text = creditlimit;
            xct.xramounttopay.Text = String.Format("{0:0,0.00}", getTotalCredit);
            minpay = Convert.ToDouble(txtacctbalance.Text) / 2;
            //xct.xrminimumpayment.Text = txtacctbalance.Text;//minpay.ToString();
            //Database.display("SELECT CAST(PostingDate as date) as PostingDate,CAST(TransactionDate as date) as TransactionDate,TransCode,Description,Debit,Credit,ReferenceNumber FROM ClientLedger WHERE AccountID='" + txtacctid.Text + "' AND CAST(TransactionDate as date) >= '" + datefromledge.Text + "' and CAST(TransactionDate as date)<= '" + datetoledge.Text + "' ORDER BY SequenceNumber ASC", gridControl2, gridView2);

            gridView2.Columns["PostingDate"].Visible = false;
            gridView2.Columns["Description"].Visible = false;

            Point p = new Point(50, 0);
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControl2, p));

            xct.Bands[BandKind.PageHeader].Font = new System.Drawing.Font("Tahoma", 9);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void loadTransactionDetails()
        {
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            custid = txtacctid.Text;
            custname = txtacctname.Text;
            HOFormsDevEx.ClientPaymentsDevEx clientpayment = new HOFormsDevEx.ClientPaymentsDevEx();
            clientpayment.ShowDialog(this);
        }

        private void btnTransactionSummary_Click(object sender, EventArgs e)
        {
            loadTransactionSummary();
        }

        private void btnTransactionPayment_Click(object sender, EventArgs e)
        {
            loadTransactionPayment();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            loadLedger();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            double getTotalPayments = 0.0, getTotalCredit = 0.0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                if (gridView2.GetRowCellValue(i, "TransCode").ToString() == "PYMT")
                {
                    getTotalPayments += Convert.ToDouble(gridView2.GetRowCellValue(i, "Credit").ToString());
                }

            }
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                if (gridView2.GetRowCellValue(i, "TransCode").ToString() == "CHRG")
                {
                    getTotalCredit += Convert.ToDouble(gridView2.GetRowCellValue(i, "Debit").ToString());
                }

            }
            double minpay = 0.0;

            string creditlimit = Database.getSingleQuery("Customers", "CustomerID='" + txtacctid.Text + "'", "CustomerCreditLimit");
             
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StatementOfAccount'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");
            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.StatementOfAccount xct = new DevExReportTemplate.StatementOfAccount();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StatementOfAccount'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
           
            xct.Landscape = false;

            xct.xrname.Text = txtacctname.Text;
            xct.xraddress.Text = Customers.getCustAddress(txtacctid.Text);
            xct.xraccountid.Text = txtacctid.Text;
            xct.xrBarCode1.Text = txtacctid.Text;
            xct.xrpaymentdate.Text = DateTime.Now.ToShortDateString();
            xct.xrcreditlimit.Text = creditlimit;
            xct.xramounttopay.Text = String.Format("{0:0,0.00}", getTotalCredit);
            minpay = Convert.ToDouble(txtacctbalance.Text) / 2;
            
            gridView2.Columns["PostingDate"].Visible = false;
            gridView2.Columns["Description"].Visible = false;

            Point p = new Point(50, 0);
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControl2, p));

            xct.Bands[BandKind.PageHeader].Font = new System.Drawing.Font("Tahoma", 9);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
    }
}