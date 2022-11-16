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
    public partial class BadOrderReport : DevExpress.XtraEditors.XtraForm
    {
        object brcode = null;
        public BadOrderReport()
        {
            InitializeComponent();
        }
        void displayBranches()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch, "BranchName", "BranchName");
        }

        void display()
        {
            Database.display($"SELECT * FROM view_StockOut " +
                $"WHERE DateAdded between '{dateFrom.Text}' " +
                $"AND '{dateTo.Text}' " +
                $"AND BranchCode='{brcode.ToString()}' ", gridControl1, gridView1);
        }
        private void BadOrderReport_Load(object sender, EventArgs e)
        {
            displayBranches();
        }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            brcode = SearchLookUpClass.getSingleValue(txtbranch, "BranchCode");
            //updateChanged();
        }

        //void updateChanged()
        //{

        //    Database.displaySearchlookupEdit($"SELECT BatchID FROM dbo.StockoutSummary WHERE DateAdded between '{dateFrom.Text}' and '{dateTo.Text}' AND BranchCode='{brcode.ToString()}'", txtbatchid, "BatchID", "BatchID");
        //}

        private void btnadd_Click(object sender, EventArgs e)
        {
            display();
        }
        //void printSTS()
        //{
        //    var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

        //    string companyname = row["Heading"].ToString();
        //    string imagewidth = row["ImageWidth"].ToString();
        //    string imageheight = row["ImageHeight"].ToString();
        //    string caption1 = row["Caption1"].ToString();
        //    string caption2 = row["Caption2"].ToString();

        //    DevExReportTemplate.BadOrder xct = new DevExReportTemplate.BadOrder();

        //    Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
        //    xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
        //    xct.xrcompanyname.Text = companyname;
        //    xct.xrcaption1.Text = caption1;
        //    xct.xrcaption2.Text = caption2;

        //    xct.Landscape = false;
        //    xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
        //    xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

        //    string remarks = Database.getSingleQuery($"SELECT TOP(1) Remarks FROM dbo.StockoutSummary WHERE BatchID='{txtbatchid.Text}' ", "Remarks");
        //    xct.xrremarks.Text = remarks;
        //    xct.xrbranchname.Text = txtbranch.Text;
        //    xct.xrdatefrom.Text = dateFrom.Text;//getDateRequest().Substring(0, 10);
        //    xct.xrdateto.Text = dateTo.Text;//getDateRequest().Substring(0, 10);
            

        //    xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
        //    xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
        //    ReportPrintTool report = new ReportPrintTool(xct);
        //    report.ShowRibbonPreviewDialog();
        //}
        private void btnprint_Click(object sender, EventArgs e)
        {
            //printSTS();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\BadOrder\\";
            string filename = "BAD_ORDER_" + txtbranch.Text +"_"+ DateTime.Now.ToShortDateString().Replace(@"\", "-");
            HelperFunction.exporttoexcel(gridView1, filename, filepath);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/BadOrder");
        }

        private void dateTo_EditValueChanged(object sender, EventArgs e)
        {
          //  updateChanged();
        }

        private void dateFrom_EditValueChanged(object sender, EventArgs e)
        {
          //  updateChanged();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BadOrderReportDetails rep = new BadOrderReportDetails();
            Database.display($"SELECT * FROM [dbo].[view_StockOutDetails] WHERE ID='{gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BatchID").ToString()}' ", rep.gridControl1, rep.gridView1);
            BadOrderReportDetails.brcode = txtbranch.Text;
            BadOrderReportDetails.dateencode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateAdded").ToString();
            BadOrderReportDetails.reportype = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Category").ToString();
            BadOrderReportDetails.batchid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BatchID").ToString();
            rep.ShowDialog(this);
            if(BadOrderReportDetails.isdone==true)
            {
                rep.Dispose();
            }
        }
    }
}