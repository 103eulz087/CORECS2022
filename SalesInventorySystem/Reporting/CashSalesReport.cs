using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace SalesInventorySystem
{
    public partial class CashSalesReport : DevExpress.XtraEditors.XtraForm
    {
        string reptype = "";
        //ReportDocument cry = new ReportDocument();
        public CashSalesReport()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
           
            if (radioButton1.Checked == true)
            {
                reptype = "Summary";
               
            }
            else if (radioButton2.Checked == true)
            {
                reptype = "Detailed";
                
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_CashSalesReport";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@brcode", txtbranch.Text);
            com.Parameters.AddWithValue("@datefrom", datefrom.Text);
            com.Parameters.AddWithValue("@dateto", dateto.Text);
            com.Parameters.AddWithValue("@reportype", reptype);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
           // gridControl1.BeginUpdate();
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
           // gridControl1.EndUpdate();
            con.Close();
            ////string filePath = Application.StartupPath + "\\Reporting\\CashSalesReportDetailed.rpt";
            //////cry.Load(@"C:\Users\eulz\Documents\Visual Studio 2008\Projects\SalesInventorySystem\SalesInventorySystem\Reporting\PurchaseOrders.rpt");
            ////cry.Load(filePath);
            //SqlConnection con = Database.getConnection();
            //con.Open();
            ////SqlDataAdapter adapter = new SqlDataAdapter("spr_CashSalesReport", con);
            ////adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            ////adapter.SelectCommand.Parameters.AddWithValue("@brcode", comboBoxEdit1.Text);
            ////adapter.SelectCommand.Parameters.AddWithValue("@datefrom", dateEdit1.Text);
            ////adapter.SelectCommand.Parameters.AddWithValue("@dateto", dateEdit2.Text);
            //////adapter.SelectCommand.Parameters.AddWithValue("@reptype", reptype);
            //////adapter.SelectCommand.Parameters.AddWithValue("@unitprice","");
            //////adapter.SelectCommand.Parameters.AddWithValue("@totalkilo", "");
            //////adapter.SelectCommand.Parameters.AddWithValue("@totalamount","");
            //////adapter.SelectCommand.Parameters.AddWithValue("@daterequested", "");
            ////DataTable table = new DataTable();
            ////adapter.Fill(table);
            ////cry.SetDataSource(table);
            ////crystalReportViewer1.ReportSource = cry;
            //con.Close();
        }

        private void CashSalesReport_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            Database.display("SELECT ShipmentNo,Barcode,PalletNo,TipWeight,Available,ROUND((TipWeight-Available),2) AS Variance FROm TempInventory", gridControl1, gridView1);
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["PalletNo"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 2);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "TipWeight";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter=gridView1.Columns["TipWeight"];
           
            gridView1.GroupSummary.Add(ite);

            GridGroupSummaryItem ite1 = new GridGroupSummaryItem();
            ite1.FieldName = "Available";
            ite1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
         
            ite1.ShowInGroupColumnFooter = gridView1.Columns["Available"];
            gridView1.GroupSummary.Add(ite1);
         
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //CashSalesReportFrm xct = new CashSalesReportFrm();
            //xct.Landscape = false;
            //xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            //xct.xrreporttype.Text = reptype;
            //xct.xrdatefrom.Text = datefrom.Text;
            //xct.xrdateto.Text = dateto.Text;
            //xct.xrbranch.Text = Database.getSingleQuery("Branches", "BranchCode='" + txtbranch.Text + "'", "BranchName");
            ////xct.Font = new System.Drawing.Font("Arial Narrow", 8);
            //xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            //xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            //ReportPrintTool report = new ReportPrintTool(xct);
            //report.ShowRibbonPreviewDialog();
        }
    }
}