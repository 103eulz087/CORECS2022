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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.Reporting
{
    public partial class CounterReceiptDevEx : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        string invdate, invoiceno, amount;
        public CounterReceiptDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            add();
            gridView1.DeleteSelectedRows();
        }
        void display()
        {
            gridControl1.BeginUpdate();
            Database.display("Select CAST(TransactionDate as date) as InvoiceDate,Reference as InvoiceNo,Amount FROM TransactionChargeSales WHERE CustomerID='"+txtcustomers.Text+"' AND CAST(TransactionDate as date) between '" + txtfrom.Text + "' and '" + txtto.Text + "'", gridControl1, gridView1);
            gridControl1.EndUpdate();

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            display();
        }

        private void CounterReceiptDevEx_Load(object sender, EventArgs e)
        {
            loadGridview2();
            populate();
        }
        void populate()
        {
            Database.displaySearchlookupEdit("SELECT CustomerID,CustomerName FROM Customers", txtcustomers, "CustomerID", "CustomerID");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DevExReportTemplate.CounterReceipt xct = new DevExReportTemplate.CounterReceipt();
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

            string custname = Database.getSingleQuery("Customers", " CustomerID='" + txtcustomers.Text + "'", "CustomerName");
            string address = Database.getSingleQuery("Customers", " CustomerID='" + txtcustomers.Text + "'", "CustomerAddress");

            xct.xrcustname.Text = custname;
            xct.xraddress.Text = address;
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrPreparedBy.Text = Login.Fullname;
           
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl2));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void loadGridview2()
        {
            table = new DataTable();
            table.Columns.Add("InvoiceDate");
            table.Columns.Add("InvoiceNo");
            table.Columns.Add("Amount");
            gridControl2.DataSource = table;
        }
        void add()
        {
            gridControl2.BeginUpdate();
            invdate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceDate").ToString();
            invoiceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceNo").ToString();
            amount = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString();
           
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            DataRow newRow = table.NewRow();
            newRow["InvoiceDate"] = invdate;
            newRow["InvoiceNo"] = invoiceno;
            newRow["Amount"] = amount;
            table.Rows.Add(newRow);

            gridControl2.DataSource = table;

            gridView2.Columns["Amount"].Summary.Clear();
            gridControl2.EndUpdate();


            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Amount";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView2.Columns["Amount"];
            gridView2.GroupSummary.Add(ite);

            gridView2.Columns["Amount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:n2}");
        }
    }
}