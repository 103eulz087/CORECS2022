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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmReports : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmReports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            extract();
        }

        void extract()
        {
            gridControlCheckin.BeginUpdate();
            gridViewCheckin.Columns.Clear();
            gridControlCheckin.DataSource = null;

            if(txtreporttype.Text == "IN-HOUSE GUEST REPORT")
            {
                Database.display("SELECT * FROM view_InHouseGuest WHERE DateAdded between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "'", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
            }
            else if(txtreporttype.Text == "POLICE REPORT")
            {
                Database.display("SELECT * FROM view_PoliceReport WHERE DateAdded between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "'", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
            }
            else if (txtreporttype.Text == "DAILY CASH COLLECTION REPORT")
            {
                Database.display("SELECT SequenceKey as TransID,BookingID,GuestID,RoomNumber,Amount,DatePaid as DateCollected,Incharge as CollectedBy FROM view_DailyCollectionReports WHERE DatePaid between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' ", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
            }
            else if (txtreporttype.Text == "ROOM SALES REPORT")
            {
                Database.display("SELECT SequenceKey as TransID,BookingID,RoomNumber,Amount,RoomType,RoomRate,DatePaid as SalesDate,AddedBy as Incharge FROM view_RoomSalesReport WHERE DatePaid between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and AddedBy='"+Login.Fullname+"'", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
            }
            else if (txtreporttype.Text == "ROOM STATUS REPORT")
            {
                Database.display("SELECT RoomNumber,RoomType,RoomStatus FROM Rooms ", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
            }
            else if (txtreporttype.Text == "HOUSEKEEPING REPORT")
            {
                Database.display("SELECT * FROM RoomCleanedReport WHERE DateCleaned between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "'", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
            }
            else
            {
                XtraMessageBox.Show("Please Select Report Category!!");
            }
            gridControlCheckin.EndUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='HotelManagement'", "Heading");
            //string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='HotelManagement'", "ImageWidth");
            //string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='HotelManagement'", "ImageHeight");
            //string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='HotelManagement'", "Caption1");
            //string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='HotelManagement'", "Caption2");
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            if (txtreporttype.Text == "IN-HOUSE GUEST REPORT")
            {
                
                HotelReportingFrms.InHouseGuestSheet asdk = new HotelReportingFrms.InHouseGuestSheet();
                SalesInventorySystem.Classes.Utilities.GetImageDevEx(asdk.xrPictureBox1, "ReportHeaderSettings", "ReportName='HotelManagement'", "ImageLogo");
                asdk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
               
                asdk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControlCheckin));
                asdk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(asdk);
                report.ShowRibbonPreviewDialog();
            }
            else if (txtreporttype.Text == "POLICE REPORT")
            {
                HotelReportingFrms.PoliceReportSheet asdk = new HotelReportingFrms.PoliceReportSheet();
                SalesInventorySystem.Classes.Utilities.GetImageDevEx(asdk.xrPictureBox1, "ReportHeaderSettings", "ReportName='HotelManagement'", "ImageLogo");
                asdk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));

                asdk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControlCheckin));
                asdk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(asdk);
                report.ShowRibbonPreviewDialog();
            }
            else if (txtreporttype.Text == "DAILY CASH COLLECTION REPORT")
            {
                HotelReportingFrms.PoliceReportSheet asdk = new HotelReportingFrms.PoliceReportSheet();
                SalesInventorySystem.Classes.Utilities.GetImageDevEx(asdk.xrPictureBox1, "ReportHeaderSettings", "ReportName='HotelManagement'", "ImageLogo");
                asdk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                asdk.xrheading.Text = "DAILY CASH COLLECTION REPORT";

                asdk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControlCheckin));
                asdk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(asdk);
                report.ShowRibbonPreviewDialog();
            }
            else if (txtreporttype.Text == "ROOM SALES REPORT")
            {
                HotelReportingFrms.PoliceReportSheet asdk = new HotelReportingFrms.PoliceReportSheet();
                SalesInventorySystem.Classes.Utilities.GetImageDevEx(asdk.xrPictureBox1, "ReportHeaderSettings", "ReportName='HotelManagement'", "ImageLogo");
                asdk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                asdk.xrheading.Text = "ROOM SALES REPORT";

                asdk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControlCheckin));
                asdk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(asdk);
                report.ShowRibbonPreviewDialog();
            }
            else if (txtreporttype.Text == "ROOM STATUS REPORT")
            {
                HotelReportingFrms.PoliceReportSheet asdk = new HotelReportingFrms.PoliceReportSheet();
                SalesInventorySystem.Classes.Utilities.GetImageDevEx(asdk.xrPictureBox1, "ReportHeaderSettings", "ReportName='HotelManagement'", "ImageLogo");
                asdk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                asdk.xrheading.Text = "ROOM STATUS REPORT";

                asdk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControlCheckin));
                asdk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(asdk);
                report.ShowRibbonPreviewDialog();
            }
            else if (txtreporttype.Text == "HOUSEKEEPING REPORT")
            {
                HotelReportingFrms.PoliceReportSheet asdk = new HotelReportingFrms.PoliceReportSheet();
                SalesInventorySystem.Classes.Utilities.GetImageDevEx(asdk.xrPictureBox1, "ReportHeaderSettings", "ReportName='ShipmentReport'", "ImageLogo");
                asdk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                asdk.xrheading.Text = "HOUSEKEEPING REPORT";

                asdk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControlCheckin));
                asdk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(asdk);
                report.ShowRibbonPreviewDialog();
            }
        }

        private void txtdatefrom_EditValueChanged(object sender, EventArgs e)
        {
            SalesInventorySystem.Classes.Utilities.setMinimumDateDevEx(txtdatefrom.Text, txtdateto);
        }
    }
}