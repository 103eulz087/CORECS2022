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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class DebitMemoDevEx : DevExpress.XtraEditors.XtraForm
    {
        public DebitMemoDevEx()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Database.display("SELECT PONumber,QtyDelivered as Qty,ProductName as Description,SellingPrice as UnitCost,FORMAT(TotalAmount , 'N', 'en-us') as Amount FROM view_CreditMemoDetails WHERE Variance > 0 and PONumber='" + txtpono.Text + "' ", gridControl4, gridView4);
                //Database.display("SELECT * FROM view_CreditMemoDetails WHERE PONumber='" + txtpono.Text + "' AND isCreditMemo='1'", gridControl4, gridView4);

                button3.PerformClick();
                DevExReportTemplate.DebitMemo xct = new DevExReportTemplate.DebitMemo();
                xct.Landscape = false;

                string custname = Database.getSingleQuery("PurchaseOrderSummary", " PONumber='" + txtpono.Text + "'", "Customer");
                string refno = Database.getSingleQuery("DeliverySummary", " PONumber='" + txtpono.Text + "'", "ReferenceNumber");
                string invoiceno = Database.getSingleQuery("DeliverySummary", " PONumber='" + txtpono.Text + "'", "InvoiceNo");
                //string refno = Database.getSingleQuery("DeliveryDetails", " PONumber='" + txtpono.Text + "'", "ReferenceNumber");
                string desc = Database.getSingleQuery("ClientChargeSalesSummary", " PONumber='" + txtpono.Text + "'", "Description");
                xct.xrLabel4.Text = desc;
                xct.xrcustname.Text = custname;
                xct.xrinvoiceno.Text = invoiceno;
                xct.xrdate.Text = DateTime.Now.ToShortDateString();
                xct.xrPreparedBy.Text = Login.Fullname;

                xct.xrtitle.Text = "Debit Memo";

                xct.xrPreparedBy.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControl4));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='DebitMemo'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth = row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString();
                string caption1 = row["Caption1"].ToString();
                string caption2 = row["Caption2"].ToString();

                button3.PerformClick();
                DevExReportTemplate.DebitMemo xct = new DevExReportTemplate.DebitMemo();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='DebitMemo'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4;

                string custname = Database.getSingleQuery("PurchaseOrderSummary", " PONumber='" + txtpono.Text + "'", "Customer");
                string refno = Database.getSingleQuery("DeliverySummary", " PONumber='" + txtpono.Text + "'", "ReferenceNumber");
                string invoiceno = Database.getSingleQuery("DeliverySummary", " PONumber='" + txtpono.Text + "'", "InvoiceNo");
                string desc = Database.getSingleQuery("ClientChargeSalesSummary", " PONumber='" + txtpono.Text + "'", "Description");
                xct.xrLabel4.Text = desc;
                xct.xrcustname.Text = custname;
                xct.xrinvoiceno.Text = invoiceno;
                xct.xrdate.Text = DateTime.Now.ToShortDateString();
                xct.xrPreparedBy.Text = Login.Fullname;

                xct.xrtitle.Text = "Debit Memo";

                xct.xrPreparedBy.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControl4));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}