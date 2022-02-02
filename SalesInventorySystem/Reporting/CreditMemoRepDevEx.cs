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
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.Reporting
{
    public partial class CreditMemoRepDevEx : DevExpress.XtraEditors.XtraForm
    {
        public CreditMemoRepDevEx()
        {
            InitializeComponent();
        }

        private void CreditMemoRepDevEx_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            dateFrom.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateTo.Text = lastDay.ToShortDateString();
            populate();
        }


        void populate()
        {
            Database.displaySearchlookupEdit("Select Distinct PONumber FROM DeliveryDetails WHERE (isCreditMemo=1 OR isReturned=1)", searchLookUpEdit1, "PONumber", "PONumber");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            if (reweigh.Checked == true)
                Database.display("Select * FROM CreditMemo WHERE CAST(DateAdded as date) between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' AND PONumber='" + searchLookUpEdit1.Text + "' ", gridControl1, gridView1);
            else
                Database.display("Select ProductName" +
                    ",QtyDelivered" +
                    ",SellingPrice" +
                    ",(QtyDelivered*SellingPrice) as TotalAmount " +
                    "FROM ReturnedOrderDetails " +
                    "WHERE PONumber='" + searchLookUpEdit1.Text + "'  ", gridControl1, gridView1);
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //Database.display("Select * FROM CreditMemo WHERE CAST(DateAdded as date) between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' AND PONumber='"+searchLookUpEdit1.Text+"' ", gridControl1, gridView1);
        }

       
        void print()
        {
            var rowz = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CreditMemo'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = rowz["Heading"].ToString();
            string imagewidth = rowz["ImageWidth"].ToString();
            string imageheight = rowz["ImageHeight"].ToString();
            string caption1 = rowz["Caption1"].ToString();
            string caption2 = rowz["Caption2"].ToString();

            DevExReportTemplate.Creditmemo xct = new DevExReportTemplate.Creditmemo();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CreditMemo'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;
            xct.Landscape = false;

            string custkey = Database.getSingleQuery("PurchaseOrderSummary", " PONumber='" + searchLookUpEdit1.Text + "'", "Customer");
            string custname = Database.getSingleQuery("Customers", " CustomerKey='" + custkey + "'", "CustomerName");

            var row = Database.getMultipleQuery("DeliverySummary", "PONumber='" + searchLookUpEdit1.Text + "' ", "ReferenceNumber,InvoiceNo");
            string refno = row["ReferenceNumber"].ToString();
            string invoiceno = row["InvoiceNo"].ToString();
            xct.xrcustname.Text = custname;
            xct.xrinvoiceno.Text = invoiceno;
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrPreparedBy.Text = Login.Fullname;

            if(reweigh.Checked==true)
            {
                gridView1.Columns["PONumber"].Visible = false;
                gridView1.Columns["ProductCode"].Visible = false; 
                gridView1.Columns["TicketRefNo"].Visible = false;
                gridView1.Columns["DateAdded"].Visible = false;
                gridView1.Columns["ExecuteBy"].Visible = false;
            }
            //else
            //{
            //    gridView1.Columns["DeliveryNo"].Visible = false;
            //    gridView1.Columns["ReferenceNumber"].Visible = false;
            //    gridView1.Columns["ProductNo"].Visible = false;
            //    gridView1.Columns["BarcodeNo"].Visible = false;
            //    gridView1.Columns["PONumber"].Visible = false;
            //    gridView1.Columns["BranchCode"].Visible = false;
            //    gridView1.Columns["Cost"].Visible = false;
            //    gridView1.Columns["ActualQty"].Visible = false;
            //    gridView1.Columns["Variance"].Visible = false;
            //    gridView1.Columns["ReferenceCode"].Visible = false;
            //    gridView1.Columns["Status"].Visible = false;
            //    gridView1.Columns["DateProcessed"].Visible = false;
            //    gridView1.Columns["isVat"].Visible = false;
            //    gridView1.Columns["ProcessedBy"].Visible = false;
            //    gridView1.Columns["SequenceNo"].Visible = false;
            //}
            

            xct.xrPreparedBy.Text = Login.Fullname;
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        void printButton()
        {
            bool isfilterbypo = Database.checkifExist("Select top 1 PONumber FROM CreditMemo WHERE PONumber='" + searchLookUpEdit1.Text + "'");
            if (isfilterbypo)
                print();
            else
                return;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            printButton();
        }
    }
}