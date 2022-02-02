using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace SalesInventorySystem
{
    public partial class PreviewReceipt : DevExpress.XtraEditors.XtraForm
    {
        ReportDocument cry = new ReportDocument();
        public PreviewReceipt()
        {
            InitializeComponent();
        }

        private void PreviewReceipt_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\Reporting\\CashSalesReceipt.rpt";
            //cry.Load(@"C:\Users\eulz\Documents\Visual Studio 2008\Projects\SalesInventorySystem\SalesInventorySystem\Reporting\PurchaseOrders.rpt");
            cry.Load(filePath);
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("spr_CashSales", con);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@invoiceno", POForApprovalDetails.refernceno);
            //adapter.SelectCommand.Parameters.AddWithValue("@brcode", "");
            //adapter.SelectCommand.Parameters.AddWithValue("@pcode", "");
            //adapter.SelectCommand.Parameters.AddWithValue("@pname", "");
            //adapter.SelectCommand.Parameters.AddWithValue("@unitprice","");
            //adapter.SelectCommand.Parameters.AddWithValue("@totalkilo", "");
            //adapter.SelectCommand.Parameters.AddWithValue("@totalamount","");
            //adapter.SelectCommand.Parameters.AddWithValue("@daterequested", "");
            DataTable table = new DataTable();
            adapter.Fill(table);
            cry.SetDataSource(table);
            crystalReportViewer1.ReportSource = cry;
            con.Close();
        }
    }
}