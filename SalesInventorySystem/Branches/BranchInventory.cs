using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem
{
    public partial class BranchInventory : DevExpress.XtraEditors.XtraForm
    {
        public BranchInventory()
        {
            InitializeComponent();
        }

        private void BranchInventory_Load(object sender, EventArgs e)
        {
           // Database.display("SELECT * FROM view_BranchInventory", gridControl1, gridView1);
            display();
        }

        private void display()
        {
            gridControl1.BeginUpdate();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            try
            {   
                if (radioButton1.Checked == true) //DETAILED
                {
                    Database.display("SELECT * FROM view_BranchInventoryDetails WHERE BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1);
                    
                }
                else if (radioButton2.Checked == true) //SUMMARY
                {
                    Database.display("SELECT * FROM view_BranchInventory WHERE BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1);
                }
                // Database.GridMasterDetail("view_BranchInventory", "view_BranchInventoryDetails", "Branch='" + Login.assignedBranch + "'", "Branch='" + Login.assignedBranch + "'", "Product", "Product", "InventoryBreakdownDetails", gridControl1);
            }
            catch (SqlException sex2)
            {
                XtraMessageBox.Show(sex2.Message.ToString());
            }
            finally
            {
                gridControl1.EndUpdate();
            }
            //HelperFunction.getTotalSum("Available");
            //gridView1.Columns["Available"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Available", "{0:n2}");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Available")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (radioButton2.Checked == true)
            {
                if (e.RowHandle >= 0)
                {
                    string available = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Available"]);
                    string reorderlevel = view.GetRowCellDisplayText(e.RowHandle, view.Columns["ReOrderLevel"]);
                    if (Convert.ToDouble(available) < Convert.ToDouble(reorderlevel))
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                    }
                }
            }
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("UPDATE Inventory SET isWarehouse=1,Branch='888' WHERE Barcode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "' AND Branch='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString() + "'", "Successfully Updated");
            //display();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filename = "BRANCHINVENTORY_" + DateTime.Now.ToShortDateString().Replace(@"/", "-");
            HelperFunction.exporttoexcel(gridView1, filename);
        }
    }
}