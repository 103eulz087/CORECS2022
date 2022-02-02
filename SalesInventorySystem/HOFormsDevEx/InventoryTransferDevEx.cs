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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryTransferDevEx : DevExpress.XtraEditors.XtraForm
    {
        string dispatchno = "";
        public InventoryTransferDevEx()
        {
            InitializeComponent();
        }

        private void InventoryTransferDevEx_Load(object sender, EventArgs e)
        {
            txtbatchno.Enabled = false;
        }

        void display()
        {
            string source="",destination="";
            if(radioButton1.Checked==true) //commmissary to warehouse
            {
               // transto = "1";
                source = "Commissary";
                destination = "BigBlue";
            }else
            {
                //transto = "0";
                source = "BigBlue";
                destination = "Commissary";
            }
            if (checkBox1.Checked == true)
            {
                Database.display("SELECT * FROM view_InventoryTransferred WHERE DateTransferred='" + dateEdit1.Text + "' AND Source='" + source + "' AND Destination='" + destination + "' and BatchNumber='" + txtbatchno.Text + "'", gridControl1, gridView1);
                dispatchno = Database.getSingleQuery("view_InventoryTransferred", " DateTransferred='" + dateEdit1.Text + "' AND Source='" + source + "' AND Destination='" + destination + "' and BatchNumber='" + txtbatchno.Text + "'", "DispatchNo");
            }
            else
            {
                Database.display("SELECT * FROM view_InventoryTransferred WHERE DateTransferred='" + dateEdit1.Text + "' AND Source='" + source + "' AND Destination='" + destination + "'  ", gridControl1, gridView1);
                dispatchno = Database.getSingleQuery("view_InventoryTransferred", " DateTransferred='" + dateEdit1.Text + "' AND Source='" + source + "' AND Destination='" + destination + "' ", "DispatchNo");
            }
           
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
           // new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite);

            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked == false && radioButton2.Checked==false)
            {
                XtraMessageBox.Show("Please select report type");
                return;
            }
            else if (checkBox1.Checked == true && String.IsNullOrEmpty(txtbatchno.Text))
            {
                XtraMessageBox.Show("BatchNumber is Enable and must not Empty!..");
                return;
            }
            else
            {
                display();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                txtbatchno.Enabled = true;
            else
                txtbatchno.Enabled = false;
        }
        void BigBlueTemplate()
        {

            gridView1.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(gridView1.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            gridView1.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite);

            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();
            xct.Landscape = false;
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

            xct.xrdispatchno.Text = dispatchno;
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = "Transfer to BigBlue";

            gridView1.Columns["Branch"].Visible = false;
            gridView1.Columns["DateReceived"].Visible = true;
            gridView1.Columns["DateTransferred"].Visible = false;
            gridView1.Columns["isWarehouse"].Visible = false;
            gridView1.Columns["BatchNumber"].Visible = false;
            gridView1.Columns["DispatchNo"].Visible = false;
            gridView1.Columns["ProcessedBy"].Visible = false;
            gridView1.Columns["Source"].Visible = false;
            gridView1.Columns["Destination"].Visible = false;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);


            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            BigBlueTemplate();
        }
    }
}