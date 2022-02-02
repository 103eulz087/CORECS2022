using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Accounting
{
    public partial class GLSummary : Form
    {
        public static string accountcode = "",postingdate="",branchcode="";
        bool isasofdate = true;
        bool isperbranch = true;
        public GLSummary()
        {
            InitializeComponent();
        }

        private void GLSummary_Load(object sender, EventArgs e)
        {
            populateComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                execute();
                //populateRows();
                //lbldebit.Text = computeTotalDebits().ToString();
                //lblcredit.Text = computeTotalCredits().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_GLSummary";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
            com.Parameters.AddWithValue("@parmcutoffdate", txtcutoffdate.Text);
            com.Parameters.AddWithValue("@parmdate", txtdate.Text);
            com.Parameters.AddWithValue("@parmisasofdate", isasofdate);
            com.Parameters.AddWithValue("@parmisperbranch", isperbranch);
            com.Parameters.AddWithValue("@parmreporttype", txtreporttype.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 3600;
            com.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }

        void populateComboBox()
        {

            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbrcode,"BranchCode","BranchCode");
        }

        void populateRows()
        {
            try
            {
                Database.display("SELECT * FROM view_GLSummary WHERE BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + txtcutoffdate.Text + "' AND SupplementaryNumber='0' ORDER BY AccountCode ASC", gridControl1, gridView1);
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        Double computeTotalDebits()
        {
            double totaldebits = 0.0;
            totaldebits = Database.getTotalSummation2("view_GLSummary", "BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + txtcutoffdate.Text + "' AND SupplementaryNumber='0' AND AccountType='D'", "Debits");
            return totaldebits;
        }

        Double computeTotalCredits()
        {
            double totalcredits = 0.0;
            totalcredits = Database.getTotalSummation2("view_GLSummary", "BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + txtcutoffdate.Text + "' AND SupplementaryNumber='0' AND AccountType='D'", "Credits");
            return totalcredits;
        }

        private void txtbrcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblbranchname.Text = Branch.getBranchName(txtbrcode.Text);
        }

        private void txtbrcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtbrcode.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            object value = view.GetRowCellValue(rowHandle, "BranchName");
            lblbranchname.Text = value.ToString();
        }

        private void chckperbranch_CheckedChanged(object sender, EventArgs e)
        {
            if(chckperbranch.Checked==true)
            {
                txtbrcode.Enabled = true;
                isperbranch = true;
            }
            else
            {
                txtbrcode.Enabled = false;
                isperbranch = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                isasofdate = true;
                txtcutoffdate.Enabled = false;
            }
            else
            {
                isasofdate = false;
                txtcutoffdate.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string filepath = "C:\\MyFiles\\";
            Classes.Utilities.createDirectoryFolder(filepath);
            string filename = txtreporttype.Text + "_" + txtcutoffdate.Text.Replace('/','-')+'-'+txtdate.Text.Replace('/', '-') + ".xls";
            string file = filepath + filename;
            gridView1.ExportToXls(file);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void showTicketEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            postingdate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PostingDate").ToString();
            accountcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountCode").ToString();
            branchcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            Accounting.ViewTicketDetails viewticket = new ViewTicketDetails();
            viewticket.ShowDialog(this);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}
