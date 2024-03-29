﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace SalesInventorySystem.Reporting.BIR
{
    public partial class POSSalesReportSummary : DevExpress.XtraEditors.XtraForm
    {
        string reportyype = "";
        public POSSalesReportSummary()
        {
            InitializeComponent();
        }
        public POSSalesReportSummary(string rep)
        {
            this.reportyype = rep;
            InitializeComponent();
        }

        void extract()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query;
            if (reportyype == "A")
            {
                query = "spr_POSSalesReportSummary";
            }
            else
            {
                query = "spr_POSSalesReportSummary2";
            }
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmdatefrom", dateEdit1.Text);
            com.Parameters.AddWithValue("@parmdateto", dateEdit2.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            con.Close();

        }
        private void btnExtract_Click(object sender, EventArgs e)
        {
            extract();
            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "POSName");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalVatExemptSales");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalVatSales");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalSalesNetOfVat");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalVat");
        }

        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\BIRReports\\";
            string filename = "POSSalesReportSummary_" + DateTime.Now.ToShortDateString().Replace(@"/", "-");
            HelperFunction.exporttoexcel(gridView1, filename, filepath);
        }
        void extractESales(Reporting.BIR.E_SalesReports erep)
        {
            //= new E_SalesReports();
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_ESalesDetails";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString());
            com.Parameters.AddWithValue("@parmdatefrom", dateEdit1.Text);
            com.Parameters.AddWithValue("@parmdateto", dateEdit2.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 180;
            com.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            erep.gridView1.Columns.Clear();
            erep.gridControl1.DataSource = null;
            adapter.Fill(table);
            erep.gridControl1.DataSource = table;
            erep.gridView1.BestFitColumns();
            con.Close();

        }
        private void showESalesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporting.BIR.E_SalesReports erep = new E_SalesReports();

            //Database.display("exec spr_ESalesDetails '" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString() + "','" + dateEdit1.Text + "','" + dateEdit1.Text + "' ", erep.gridControl1, erep.gridView1);
            //erep.ShowDialog(this);
            erep.Show();
            extractESales(erep);
            Classes.DevXGridViewSettings.ShowFooterCountTotal(erep.gridView1, "BranchCode");
            Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "TotalVatExemptSales");
            Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "TotalVatSales");
            Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "TotalSalesNetOfVat");
            Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "TotalVat");
            Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "TotalSales");
            //Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "AccumulatedSales");
            Classes.DevXGridViewSettings.ShowFooterTotal(erep.gridView1, "NumberOfTransactions");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
             
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show( gridControl1, e.Location);
        }
    }
}