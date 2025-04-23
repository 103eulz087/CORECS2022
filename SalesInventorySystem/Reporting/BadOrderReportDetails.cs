using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Reporting
{
    public partial class BadOrderReportDetails : Form
    {
        public static string brcode, batchid,reportype,dateencode;
        public static bool isdone = false;
        public BadOrderReportDetails()
        {
            InitializeComponent();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            printSTS();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\BadOrderDetails\\";
            string filename = "BAD_ORDER_" + brcode + "_" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            HelperFunction.exporttoexcel(gridView1, filename, filepath);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/BadOrder");
        }

        private void BadOrderReportDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            isdone = true;
        }

        void printSTS()
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOutRep'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.BadOrderDetails xct = new DevExReportTemplate.BadOrderDetails();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOutRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;
            xct.xrLabel1.Text = reportype+ " REPORT";

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

            string remarks = Database.getSingleQuery($"SELECT TOP(1) Remarks FROM dbo.StockoutSummary WHERE BatchID='{batchid}' ", "Remarks");
            xct.xrremarks.Text = remarks;
            xct.xrbranchname.Text = brcode;
            xct.xrdatefrom.Text = dateencode;
            //xct.xrdatefrom.Text = dateFrom.Text;//getDateRequest().Substring(0, 10);
            //xct.xrdateto.Text = dateTo.Text;//getDateRequest().Substring(0, 10);


            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
    }
}
