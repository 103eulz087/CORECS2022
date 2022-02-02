using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace SalesInventorySystem.Reporting
{
    public partial class ConversionReports : Form
    {
        public static string conid,contype="";
        public ConversionReports()
        {
            InitializeComponent();
        }

        private void ConversionReports_Load(object sender, EventArgs e)
        {
            
            loadBranch();
            if (Login.assignedBranch == "888")
            {
                labelControl1.Visible = true;
                txtbrcode.Visible = true;
            }
            else
            {
                labelControl1.Visible = false;
                txtbrcode.Visible = false;
            }
            //displayConversionForToday();
        }

        void loadBranch()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        void displayConversionForToday()
        {
            Database.display("SELECT * FROM view_ConversionSummary WHERE DateConverted = '" + DateTime.Now.ToShortDateString() + "' and BranchCode='" + Login.assignedBranch + "' and isErrorCorrect ='0'", gridControl1, gridView1);
        }

        void display()
        {
            if (Login.assignedBranch == "888")
            {
                //Database.display("SELECT * FROM view_ConversionSummary WHERE DateConverted >= '" + datefrom.Text + "' and DateConverted <= '" + dateto.Text + "' and isErrorCorrect ='0' and BranchCode='" + txtbrcode.Text + "'", gridControl1,gridView1);
                if (tabControl1.SelectedTab.Equals(FORAPPROVAL))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE DateConverted >= '" + datefrom.Text + "' and DateConverted <= '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='0' and BranchCode='" + txtbrcode.Text + "'", gridControl1, gridView1);
                    gridView1.Focus();
                }
                else if (tabControl1.SelectedTab.Equals(APPROVED))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE DateConverted >= '" + datefrom.Text + "' and DateConverted <= '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='1' and BranchCode='" + txtbrcode.Text + "'", gridControl2, gridView2);
                    gridView1.Focus();
                }
            }
            else
            {
                if (tabControl1.SelectedTab.Equals(FORAPPROVAL))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE DateConverted >= '" + datefrom.Text + "' and DateConverted <= '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='0' and BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1);
                    gridView2.Focus();
                }
                else if (tabControl1.SelectedTab.Equals(APPROVED))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE DateConverted >= '" + datefrom.Text + "' and DateConverted <= '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='1' and BranchCode='" + Login.assignedBranch + "'", gridControl2, gridView2);
                    gridView2.Focus();
                }
            }
        }
        private void tabfilter()
        {
            
        }


        private void errorCorrectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Discard this operation?", "Discard Converted Items");
            if (ok)
            {
                roolback();
            }
            else
            {
                return;
            }
        }

        void roolback()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ReturnConvertedItems";
                SqlCommand com = new SqlCommand(query, con);
                //string mark = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
                com.Parameters.AddWithValue("@parmbranchcode", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString());
                com.Parameters.AddWithValue("@parmconid", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ConID").ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Converted Operation Successfully Executed!");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ReturnConvertedItems";
                SqlCommand com = new SqlCommand(query, con);
                //string mark = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
                com.Parameters.AddWithValue("@parmbranchcode",gridView2.GetRowCellValue(gridView2.FocusedRowHandle,"BranchCode").ToString());
                com.Parameters.AddWithValue("@parmconid", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ConID").ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Converted Operation Successfully Executed!");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
                //if(Login.assignedBranch != "888")
                //{
                //    contextMenuStrip1.Items[1].Visible = false;
                //}
            }
        }

        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sure = HelperFunction.ConfirmDialog("Are you sure?", "Confirm Conversion");
            if (sure)
                confirm();
            else
                return;
        }

        void confirm()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ConfirmConversion";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString());
                com.Parameters.AddWithValue("@parmconid", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ConID").ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Converted Operation Successfully Executed!");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(gridControl2, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this operation?", "Error Correct Converted Items");
            if (ok)
            {
                execute();
            }
            else
            {
                return;
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            contype = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ConversionType").ToString();
            conid = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ConID").ToString();
            Reporting.ConversionReportDetails conrep = new ConversionReportDetails();
            Database.display("SELECT * FROM view_ConversionDetails WHERE ConID='" + Reporting.ConversionReports.conid + "'", conrep.gridControl1, conrep.gridView1);
            conrep.ShowDialog(this);
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view.FocusedColumn.FieldName != "Cost")
            //    e.Cancel = true;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            contype = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ConversionType").ToString();
            conid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ConID").ToString();
            Reporting.ConversionReportDetails conrep = new ConversionReportDetails();
            Database.display("SELECT * FROM view_ConversionDetails WHERE ConID='" + conid + "'", conrep.gridControl1, conrep.gridView1);
            conrep.ShowDialog(this);
        }

        //private void dataGridView1_DoubleClick(object sender, EventArgs e)
        //{
        //    int cord = dataGridView1.CurrentCellAddress.Y;
        //    conid = dataGridView1.Rows[cord].Cells["ConID"].Value.ToString();
        //    Reporting.ConversionReportDetails conrep = new ConversionReportDetails();
        //    conrep.ShowDialog(this);
        //}

        //int cord = dataGridView1.CurrentCellAddress.Y;
        //conid = dataGridView1.Rows[cord].Cells["ConID"].Value.ToString();
        //    Reporting.ConversionReportDetails conrep = new ConversionReportDetails();
        //conrep.ShowDialog(this);

        //private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.ColumnIndex == 4)
        //    {
        //        e.CellStyle.BackColor = Color.LightGreen;
        //    }
        //    if (e.ColumnIndex == 5)
        //    {
        //        e.CellStyle.BackColor = Color.LightBlue;
        //    }
        //    if (e.ColumnIndex == 6)
        //    {
        //        e.CellStyle.BackColor = Color.Red;
        //    }
        //}
    }
}
