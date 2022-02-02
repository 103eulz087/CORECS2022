using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.Reporting
{
    public partial class StocksOrder : Form
    {
        public StocksOrder()
        {
            InitializeComponent();
        }

        private void StocksOrder_Load(object sender, EventArgs e)
        {
            //display();
        }

        void display()
        {
            Database.display("SELECT Description,ProductName as Item,Qty as Requested,Remarks FROM view_StockOrderReport WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' AND PONumber='" + comboBox2.Text + "'", gridControl1, gridView1);
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending) 
                }, 1);
            gridView1.ExpandAllGroups();
            // gridView1.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:n2}");
            loadData();
        }

        void loadData()
        {
            Branch.displayBranchNameComboBoxItems(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM PurchaseOrderSummary WHERE BranchCode='"+Branch.getBranchCode(comboBox1.Text)+"'","PONumber",comboBox2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        void loadGrid()
        {
            //Database.display("SELECT Description,ProductName as Item,Qty as Requested,Dispatched,Received,Remarks FROM view_StockOrderReport WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' AND PONumber='" + comboBox2.Text + "'", gridControl1, gridView1);
            display();
        }

        String getRequestedBy()
        {
            string str = "";
            str = Database.getSingleQuery("PurchaseOrderSummary", "PONumber='" + comboBox2.Text + "'", "RequestedBy");
            return str;
        }

        String getDateRequest()
        {
            string str = "";
            str = Database.getSingleQuery("PurchaseOrderSummary", "PONumber='" + comboBox2.Text + "'", "EffectivityDate");
            return str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        void printSTS()
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.StockOrderRep xct = new DevExReportTemplate.StockOrderRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + txtbranch.Text + "'", "BranchName");
            string branchaddress = Database.getSingleQuery("Branches", "BranchCode='" + txtbranch.Text + "'", "Address");
            string dateprocessed = Database.getSingleQuery("DeliverySummary", "PONumber='" + txtpono.Text + "'", "DateAdded");

            xct.xrbranchname.Text = branchname;// comboBox1.Text;
            xct.xrdate.Text = txteffectivitydate.Text;//getDateRequest().Substring(0, 10);
            xct.xrdateprocessed.Text = Convert.ToDateTime(dateprocessed).ToShortDateString();
            xct.xrrequestedby.Text = txtrequestedby.Text;// getRequestedBy();
            xct.xrpono.Text = txtpono.Text;//comboBox2.Text;
            xct.xrbranchaddress.Text = branchaddress;
            xct.xrpreparedby.Text = txtpreparedby.Text;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            if(gridView1.RowCount == 0)
            {
                MessageBox.Show("Nothing To Print!...");
            }
            else
            {
                printSTS();
            }
           
        }
    }
}
