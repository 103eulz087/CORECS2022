using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SalesInventorySystem.Classes;
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
            gridView3.OptionsView.AllowCellMerge = true;
            

            gridView3.OptionsView.ShowGroupPanel = false;
            gridView3.OptionsView.ShowIndicator = false;
            gridView3.OptionsView.RowAutoHeight = true;
            gridView3.OptionsView.ColumnAutoWidth = false;

            gridView3.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView3.BestFitColumns();


        }

        private void ConversionReports_Load(object sender, EventArgs e)
        {
            datefrom.Text = DateTime.Today.ToShortDateString();
            dateto.Text = DateTime.Today.ToShortDateString();

            if (Login.assignedBranch == "888")
            {
                loadBranch();
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
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches with(nolock) ", txtbrcode, "BranchCode", "BranchCode");
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
                    Database.display("SELECT * FROM view_ConversionSummary WHERE BranchCode='" + Login.assignedBranch + "' AND CAST(DateConverted as date) BETWEEN '" + datefrom.Text + "' and  '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='0' ", gridControl1, gridView1);
                    gridView1.Focus();
                }
                else if (tabControl1.SelectedTab.Equals(APPROVED))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE BranchCode='" + Login.assignedBranch + "' AND CAST(DateConverted as date) BETWEEN '" + datefrom.Text + "' and  '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='1' ", gridControl2, gridView2);
                    gridView1.Focus();
                }
                else if (tabControl1.SelectedTab.Equals(SUMMARY))
                {
                    Database.display("SELECT * FROM view_ConversionReport WHERE BranchCode='" + Login.assignedBranch + "' AND CAST(DateConverted as date) BETWEEN '" + datefrom.Text + "' and  '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='1' ", gridControl3, gridView3);
                    //gridView3.Focus();
                    gridView3.BeginSort();
                    gridView3.ClearSorting();

                    gridView3.Columns["ConID"].SortIndex = 0;
                    gridView3.Columns["ConID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                    gridView3.EndSort();
                }
            }
            else
            {
                if (tabControl1.SelectedTab.Equals(FORAPPROVAL))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE BranchCode='" + Login.assignedBranch + "' AND CAST(DateConverted as date) BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='0' ", gridControl1, gridView1);
                    gridView2.Focus();
                }
                else if (tabControl1.SelectedTab.Equals(APPROVED))
                {
                    Database.display("SELECT * FROM view_ConversionSummary WHERE BranchCode='" + Login.assignedBranch + "' AND CAST(DateConverted as date) BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='1' ", gridControl2, gridView2);
                    gridView2.Focus();
                }
                else if (tabControl1.SelectedTab.Equals(SUMMARY))
                {
                    Database.display("SELECT * FROM view_ConversionReport WHERE BranchCode='" + Login.assignedBranch + "' AND CAST(DateConverted as date) BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' and isErrorCorrect ='0' and isConfirm='1' ", gridControl3, gridView3);
                    // Sort by ConID so identical values are adjacent
                    gridView3.BeginSort();
                    gridView3.ClearSorting();
                    gridView3.Columns["ConID"].SortIndex = 0;
                    gridView3.Columns["ConID"].SortOrder = ColumnSortOrder.Ascending;
                    //gridView3.Columns["Product"].SortIndex = 1;   // optional: keep products ordered

                    gridView3.EndSort();

                    // Enable cell merge

                    gridView3.OptionsView.AllowCellMerge = true;

                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView3.Columns)
                        col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

                    gridView3.Columns["ConID"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    

                }
            }
        }
        private void tabfilter()
        {
            
        }


        private void errorCorrectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirm = BigAlert.Show(
                       "CANCEL CONVERSION",
                       "Are you sure you want to CANCEL this CONVERSION Process?",
                       MessageBoxIcon.Warning,
                       MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                roolback();
                display();
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
                //XtraMessageBox.Show("Converted Operation Successfully Executed!");
                BigAlert.Show(
                         "CONVERSION CANCELLED",
                         "This Conversion has now been CANCELED",
                         MessageBoxIcon.Information);
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
                BigAlert.Show(
                        "ROLLBACK SUCCESS",
                        "This Conversion is Successfully CANCELLED",
                        MessageBoxIcon.Information);
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
            if (sure) { confirm(); display(); }
              
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
                XtraMessageBox.Show("Comversion Successfully Executed!");
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
                display();
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

        private void viewConversionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contype = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ConversionType").ToString();
            conid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ConID").ToString();
            Reporting.ConversionReportDetails conrep = new ConversionReportDetails();
            Database.display("SELECT * FROM view_ConversionDetails WHERE ConID='" + conid + "'", conrep.gridControl1, conrep.gridView1);
            conrep.ShowDialog(this);
        }

        string _lastConID = null;
        bool _useAltColor = false;

        private void gridView3_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            if (!gridView3.IsDataRow(e.RowHandle)) return;

            var isSource = gridView3.GetRowCellValue(e.RowHandle, "SourceProductCode") != null;
            if (isSource)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }


            if (!gridView3.IsDataRow(e.RowHandle))
                return;

            string currentConID =
                gridView3.GetRowCellValue(e.RowHandle, "ConID")?.ToString();

            if (_lastConID == null || currentConID != _lastConID)
            {
                _useAltColor = !_useAltColor;
                _lastConID = currentConID;
            }

            if (_useAltColor)
            {
                e.Appearance.BackColor = Color.FromArgb(245, 248, 255); // subtle blue
                e.Appearance.BackColor2 = Color.White;
                //e.HighPriority = true;
            }


        }

        private void gridView3_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {


            GridView view = sender as GridView;
            if (view == null) return;
            if (!view.IsDataRow(e.RowHandle)) return;

            // Current ConID
            string conId = Convert.ToString(view.GetRowCellValue(e.RowHandle, "ConID"));

            // Previous/Next row ConID (to detect first/last row of the ConID block)
            string prevConId = null;
            string nextConId = null;

            int prevHandle = view.GetPrevVisibleRow(e.RowHandle);
            if (prevHandle >= 0 && view.IsDataRow(prevHandle))
                prevConId = Convert.ToString(view.GetRowCellValue(prevHandle, "ConID"));

            int nextHandle = view.GetNextVisibleRow(e.RowHandle);
            if (nextHandle >= 0 && view.IsDataRow(nextHandle))
                nextConId = Convert.ToString(view.GetRowCellValue(nextHandle, "ConID"));

            bool isFirstRowOfConId = (prevHandle < 0) || (conId != prevConId);
            bool isLastRowOfConId = (nextHandle < 0) || (conId != nextConId);

            // Determine first & last visible columns (for left/right borders)
            var firstCol = view.VisibleColumns.Count > 0 ? view.VisibleColumns[0] : null;
            var lastCol = view.VisibleColumns.Count > 0 ? view.VisibleColumns[view.VisibleColumns.Count - 1] : null;

            bool isFirstVisibleColumn = (firstCol != null && e.Column == firstCol);
            bool isLastVisibleColumn = (lastCol != null && e.Column == lastCol);

            // Border style
            using (Pen pen = new Pen(Color.DimGray, 2))
            {
                // TOP border (draw once per row, but only for first row of ConID)
                if (isFirstRowOfConId)
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);
                }

                // BOTTOM border (only for last row of ConID)
                if (isLastRowOfConId)
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
                }

                // LEFT border (only on the first visible column)
                if (isFirstVisibleColumn)
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom);
                }

                // RIGHT border (only on the last visible column)
                if (isLastVisibleColumn)
                {
                    e.Graphics.DrawLine(pen, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
                }
            }

        }

        private void gridView3_CellMerge(object sender, CellMergeEventArgs e)
        {


           
            // Same ConID: allow DevExpress default merge behavior (value equality)
            // Important: do NOT set e.Handled=true here


        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            
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
