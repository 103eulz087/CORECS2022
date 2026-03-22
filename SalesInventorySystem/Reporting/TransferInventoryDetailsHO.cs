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
    public partial class TransferInventoryDetailsHO : DevExpress.XtraEditors.XtraForm
    {
        public TransferInventoryDetailsHO()
        {
            InitializeComponent();
        }
        void CommissaryTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption2");

            DevExReportTemplate.StorageReceivingForm xct = new DevExReportTemplate.StorageReceivingForm();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);

            gridView1.Columns["SequenceNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["PalletNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["Status"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["Barcode"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["ShipmentNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["CreatedAt"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CommissaryTemplate();
        }
    }
}