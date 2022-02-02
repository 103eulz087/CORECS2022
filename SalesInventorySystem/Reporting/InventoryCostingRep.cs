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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.Reporting
{
    public partial class InventoryCostingRep : DevExpress.XtraEditors.XtraForm
    {
        object suppkey;
        string suppliername = "";
        public InventoryCostingRep()
        {
            InitializeComponent();
        }

        private void InventoryCostingRep_Load(object sender, EventArgs e)
        {
            displaySupplier();
        }
        void displaySupplier()
        {
            Database.displaySearchlookupEdit("select SupplierKey,SupplierID,SupplierName FROM Supplier", searchLookUpEdit1, "SupplierName", "SupplierName");
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            suppkey = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "SupplierKey");
            Database.display("SELECT ProductCode,Barcode,ProductName,Cost,AvailableQty" +
                " FROM func_viewPurchaseOrder('" + Login.assignedBranch + "','" + suppkey + "')", gridControl2, gridView2);
        }
        void print()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "Caption2");

            DevExReportTemplate.InventoryCostSupplierRep xct = new DevExReportTemplate.InventoryCostSupplierRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='PurchaseOrderShipmentRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            
            suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + suppkey + "'", "SupplierName");

            xct.xrsupplierid.Text = Reporting.PurchaseOrderRepDevEx.supplierid;
            xct.xrsuppliername.Text = suppliername;

            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
 
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl2));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            print();
        }
        void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\MyFiles\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }
        private void btnexporttoexcel_Click(object sender, EventArgs e)
        {
            string filename = "INVENTORYCOST_"+ suppliername +"_"+ DateTime.Now.ToString("yyyyMMdd");
            exporttoexcel(gridView2, filename);
        }
    }
}