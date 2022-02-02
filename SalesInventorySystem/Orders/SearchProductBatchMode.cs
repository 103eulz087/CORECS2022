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

namespace SalesInventorySystem.Orders
{
    public partial class SearchProductBatchMode : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public SearchProductBatchMode()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == (Keys.F | Keys.Control))
            {
                txtsearchprod.Focus();
            }
            return functionReturnValue;
        }
        private void SearchProductBatchMode_Load(object sender, EventArgs e)
        {
            txtsearchprod.Focus();
            populateRepositoryMetrics();
        }
        void populateRepositoryMetrics()
        {
            Database.displayRepositoryComboBoxItems("SELECT * FROM Metrics", "Metrics", repoMetrics);
        }
        void extract()
        {
            Database.display("SELECT ProductCode,LongDescription as Description,Barcode,'0' as Quantity,' ' as Units,' ' as Remarks " +
                "FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                "AND (Description like '%" + txtsearchprod.Text.Trim() + "%' OR Barcode like '%" + txtsearchprod.Text.Trim() + "%') ", gridControl1, gridView1);
        }
        void save()
        {
            try
            {
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.Clear();

                int[] selectedRows = gridView1.GetSelectedRows();
                int ctr = 0;
                ctr = Database.getLastID("TransferOrderDetails", "PONumber='" + txtpono.Text + "' ", "SeqNo");

                foreach (int rowHandle in selectedRows)
                {
                    ctr += 1;
                    string productcode = gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string description = gridView1.GetRowCellValue(rowHandle, "Description").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString(); 
                    string quantity = gridView1.GetRowCellValue(rowHandle, "Quantity").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string units = gridView1.GetRowCellValue(rowHandle, "Units").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string remarks = gridView1.GetRowCellValue(rowHandle, "Remarks").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();

                    if (rowHandle >= 0)
                    {
                        bool checkifexists = Database.checkifExist($"SELECT TOP(1) ProductCode FROM TransferOrderDetails WHERE ProductCode='{productcode}' AND PONumber='{txtpono.Text}'");
                        if(checkifexists)
                        {
                            XtraMessageBox.Show("One of the products you request are already added..please check!..");
                            return;
                        }
                        else
                        {
                            Database.ExecuteQuery("INSERT INTO TransferOrderDetails (PONumber" +
                           ",SeqNo" +
                           ",ProductCode" +
                           ",ProductName" +
                           ",Qty" +
                           ",Units" +
                           ",isValid" +
                           ",Remarks) " +
                           "VALUES ('" + txtpono.Text + "'" +
                           ",'" + ctr + "'" +
                           ",'" + productcode + "'" +
                           ",'" + description + "'" +
                           ",'" + quantity + "'" +
                           ",'" + units + "'" +
                           ",'1'" +
                           ",'" + remarks + "') ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to save this Request?", "Confirm Request Order");
            if (confirm)
            {
                save();
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            extract();
        }

        private void txtsearchprod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnsubmit.PerformClick();
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Units")
                e.RepositoryItem = repoMetrics;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
            if (e.Column.FieldName == "Units")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            if (e.Column.FieldName == "Remarks")
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.BackColor2 = Color.Yellow;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Quantity" && view.FocusedColumn.FieldName != "Units" && view.FocusedColumn.FieldName != "Remarks")
                e.Cancel = true;

        }
    }
}