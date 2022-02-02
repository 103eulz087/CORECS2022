using DevExpress.XtraReports.UI;
using SalesInventorySystem.DevExReportTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class ViewOrderDetails : Form
    {
        public ViewOrderDetails()
        {
            InitializeComponent();
        }

        private void ViewOrderDetails_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Login.isglobalOfficer) != true || Convert.ToBoolean(Login.isglobalApprover) != true)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                btnprint.Enabled = true;
            }
            if (ViewOrder.tabtype == "Delivered")
            {
                //groupBox1.Visible = false;
                button1.Enabled = false;
                button2.Enabled = false;
                btnprint.Enabled = true;
            }

            Database.display("SELECT ShipmentNo,ProductCode,Description,FORMAT(Quantity,'N', 'en-us') AS Quantity,Metrics,FORMAT(CostKg,'N', 'en-us') AS Cost,FORMAT(TotalProductCost,'N', 'en-us') AS TotalProductCost,FORMAT(ButcheryCost,'N', 'en-us') AS ButcheryCost,FORMAT(FreightCost,'N', 'en-us') AS FreightCost FROM OrderDetails WHERE ShipmentNo='" + ViewOrder.shipmentno + "' ", gridControl1, gridView1);
            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
            gridView1.Columns["TotalProductCost"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalProductCost", "{0:n2}");
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE ShipmentOrder SET ApprovedBy='" + Login.Fullname + "',ApprovedDate='" + DateTime.Now.ToString() + "',Status = 'FOR DELIVERY' WHERE ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "'", "Approved");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE ShipmentOrder Set Status='CANCELLED',ApprovedDate='" + DateTime.Now.ToShortDateString() + "',ApprovedBy='" + Login.isglobalUserID + "' WHERE ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "'", "DisApproved!");
            this.Close();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderRep'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");
            var rowz = Database.getMultipleQuery("ShipmentOrder", "ShipmentNo = '" + ViewOrder.shipmentno + "'"
                , "SupplierName,Remarks,OrderedBy,ApprovedBy,TargetDate,Branch");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            string custname = rowz["SupplierName"].ToString();  

            string address = Database.getSingleQuery("Supplier", "SupplierName = '" + custname + "'", "Address");
            string contactno = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerContactNo");

            string remarks = rowz["Remarks"].ToString();  
            string orderedby = rowz["OrderedBy"].ToString();  
            string approvedby = rowz["ApprovedBy"].ToString();  
            string devdate = rowz["TargetDate"].ToString();  
            string branch = rowz["Branch"].ToString();  

            DevExReportTemplate.PurchaseOrderRepSupplier xct = new DevExReportTemplate.PurchaseOrderRepSupplier();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='PurchaseOrderRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xraddress1.Text = "";
            xct.xraddress2.Text = "";
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.lbldate.Text = DateTime.Now.ToShortDateString();
            xct.lblorderno.Text = ViewOrder.shipmentno;
            xct.lblcustname.Text = custname;

            xct.xrdevdate.Text = Convert.ToDateTime(devdate).ToShortDateString();
            xct.xrlblremarks.Text = remarks;
            xct.xrorderedby.Text = orderedby;
            xct.approvedby.Text = approvedby;
            xct.xrbranch.Text = branch;

            this.gridView1.Columns["ShipmentNo"].Visible = false;
            this.gridView1.Columns["ProductCode"].Visible = false;
         //   this.gridView1.Columns["TotalProductCost"].Visible = false;
            this.gridView1.Columns["ButcheryCost"].Visible = false;
            //  this.gridView1.Columns["Dispatched"].Visible = false;
            this.gridView1.Columns["FreightCost"].Visible = false;
           
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
    }
}
