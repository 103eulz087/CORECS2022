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
    public partial class PurchaseOrderRepDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public PurchaseOrderRepDetailsDevEx()
        {
            InitializeComponent();
        }


        void print()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "Caption2");

            DevExReportTemplate.PurchaseOrderShipmentRep xct = new DevExReportTemplate.PurchaseOrderShipmentRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            //   DateTime dt = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString());
            //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 10);
            string suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + Reporting.PurchaseOrderRepDevEx.supplierid + "'", "SupplierName");

            xct.xrsupplierid.Text = Reporting.PurchaseOrderRepDevEx.supplierid;
            xct.xrsuppliername.Text = suppliername;
            xct.xrpono.Text = Reporting.PurchaseOrderRepDevEx.pono;
            xct.xrdate.Text = Convert.ToDateTime(Reporting.PurchaseOrderRepDevEx.dateorder).ToShortDateString();
            xct.xrpreparedby.Text = Reporting.PurchaseOrderRepDevEx.preparedby;
            xct.xrapprovedby.Text = Reporting.PurchaseOrderRepDevEx.approvedby;

            gridView1.Columns["ShipmentNo"].Visible = false;
            gridView1.Columns["SupplierID"].Visible = false;
            gridView1.Columns["OrderType"].Visible = false;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
    
        private void PurchaseOrderRepDetailsDevEx_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            print();
        }
    }
}