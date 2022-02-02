using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class CarcassReports : Form
    {
        public CarcassReports()
        {
            InitializeComponent();
        }

        private void CarcassReports_Load(object sender, EventArgs e)
        {
            //loadShipment();
            //display();
        }

        void loadShipment()
        {
            //Database.displayComboBoxItems("SELECT * FROM ShipmentOrder", "ShipmentNo", comboBox1);
            Database.displayComboBoxItems("SELECT DISTINCT(ShipmentNo) FROM POSUMMARY", "ShipmentNo", comboBox1);
        }

        void display()
        {
            if(radgroup.Checked==true)
            {
                gridControl1.BeginUpdate();
                gridView1.GroupSummary.Clear();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                Database.display("SELECT DateReceived,Description,SUM(Quantity) as Quantity,Cost,FORMAT((Quantity*Cost), 'N', 'en-us') as TotalCost " +
                    "FROm TempInventoryBatchUpload " +
                    "WHERE ShipmentNo='" + BatchProcessMasterDevEx.shipmentno + "' " +
                    "and Branch='"+Login.assignedBranch+ "' " +
                    "and isSource=1 " +
                    "GROUP BY DateReceived,Description,Cost,Quantity", gridControl1, gridView1);
                GridView viewz = gridControl1.FocusedView as GridView;
                viewz.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(viewz.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
                viewz.ExpandAllGroups();

                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Quantity");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalCost");
                gridControl1.EndUpdate();
            }
            if (raddetailed.Checked == true)
            {
                
                gridControl1.BeginUpdate();
                gridView1.GroupSummary.Clear();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                Database.display("SELECT DateReceived,Barcode,PalletNo,Description,TipWeight,Quantity AS ActualWeight,ROUND((TipWeight-Quantity),2) AS Variance,Cost FROm TempInventoryBatchUpload WHERE ShipmentNo='" + BatchProcessMasterDevEx.shipmentno + "' and Branch='"+Login.assignedBranch+"' and isSource=1 ORDER BY Description,PalletNo,Cost", gridControl1, gridView1); 
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending),
                new GridColumnSortInfo(view.Columns["PalletNo"],DevExpress.Data.ColumnSortOrder.Ascending)

                },2);
                view.ExpandAllGroups();

                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TipWeight");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "ActualWeight");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Variance");

                gridView1.BestFitColumns();
                gridControl1.EndUpdate();
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='ShipmentReport'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            string suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + BatchProcessMasterDevEx.supplierid + "'", "SupplierName");
            DevExReportTemplate.ShipmentReport xct = new DevExReportTemplate.ShipmentReport();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='ShipmentReport'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            
            //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 15, 100);

            xct.lblshipment.Text = BatchProcessMasterDevEx.shipmentno;
            xct.xrsuppliername.Text = suppliername;
            xct.xrinvoiceno.Text = BatchProcessMasterDevEx.invoinceno;
            xct.preparedby.Text = Login.Fullname;
           
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void raddetailed_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }
    }
}
