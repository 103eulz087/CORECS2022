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
using System.Data.SqlClient;

namespace SalesInventorySystem.Reporting.BIR
{
    public partial class SalesDetailsComparative : DevExpress.XtraEditors.XtraForm
    {
        public SalesDetailsComparative()
        {
            InitializeComponent();
        }

        private void SalesDetailsComparative_Load(object sender, EventArgs e)
        {
            populateBranch();
        }
        void populateBranch()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM dbo.Branches ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");
        }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,MachineUsed FROM dbo.POSInfoDetails WHERE BranchCode='" + txtbranch.Text + "'", txtmachine, "MachineUsed", "MachineUsed");
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            extract();
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatExemptSale");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatExemptSale2");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatExemptDiff");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatableSale");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatableSale2");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatableSaleDiff");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatInput");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatInput2");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "VatInputDiff");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalNetSales");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalNetSales2");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalNetSalesDiff"); 

        }
        void extract()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl1.BeginUpdate();
            try
            {
                string query = "spr_SalesDetailsComparativeAB";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                com.Parameters.AddWithValue("@parmbrcode", txtbranch.Text);
                com.Parameters.AddWithValue("@parmmachinename", txtmachine.Text);
                com.Parameters.AddWithValue("@datefrom", txtsalesdatefrom.Text);
                com.Parameters.AddWithValue("@dateto", txtsalesdateto.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                Classes.DevXGridViewSettings.setGridFormat(gridView1);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControl1.EndUpdate();
            }
            con.Close();
        }

        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\";
            string filename = txtbranch.Text + "_" + txtmachine.Text + "_" + txtsalesdatefrom.Text.Replace("/", "-") +"_"+ txtsalesdateto.Text.Replace("/", "-") + "_SALEDETAILSCOMP_AB.xls";
            string file = filepath + filename;
            gridControl1.ExportToXls(file);
            XtraMessageBox.Show("Export Success");
        }
    }
}