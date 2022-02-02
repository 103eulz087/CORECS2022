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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryCostSearchProduct : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public InventoryCostSearchProduct()
        {
            InitializeComponent();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            extract();
        }

        void extract()
        {
            Database.display("SELECT ProductCategoryCode as CategoryCode,ProductCode" +
                ",Description" +
                ",Barcode" +
                ",'0' as Cost " +
                "FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                "AND ProductCode not in (SELECT ProductCode " +
                                        "FROM InventoryCost " +
                                        "WHERE SupplierID='"+txtsupplierid.Text+"') " +
                "AND (Description like '%" + txtsearchprod.Text.Trim() + "%' OR Barcode like '%" + txtsearchprod.Text.Trim() + "%') ", gridControl1, gridView1);
        }
        void save()
        {
            try
            {
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.Clear();

                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (int rowHandle in selectedRows)
                {
                   
                    string productcatcode = gridView1.GetRowCellValue(rowHandle, "CategoryCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string productcode = gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                     
                    if (rowHandle >= 0)
                    {

                        Database.ExecuteQuery("INSERT INTO InventoryCost (SupplierID" +
                            ",ProductCategoryCode" +
                            ",ProductCode" +
                            ",CostKg) " +
                            "VALUES ('" + txtsupplierid.Text + "'" +
                            ",'" + productcatcode + "'" +
                            ",'" + productcode + "'" +
                            ",'" + cost + "') ");
                        //Database.ExecuteQuery("insert into InventoryBigBlue values ('888',' ',' ','" + txtbatchcode.Text + "','" + DateTime.Now.ToShortDateString() + "','" + productcode + "','" + description + "','" + barcode + "','" + quantity + "','" + quantity + "','0','" + quantity + "',0,1,0,1,'" + txtbatchcode.Text + "','" + DateTime.Now.ToShortDateString() + "',0,0,0);");
                        //Database.ExecuteQuery("insert into InventoryTransferred values ('888', '" + productcode + "', '" + description + "', '" + DateTime.Now.ToShortDateString() + "', '" + barcode + "', '" + quantity + "', '" + DateTime.Now.ToShortDateString() + "', 1, '" + txtbatchcode.Text + "', 'auto', '" + Login.Fullname + "', 'Commissary', 'BigBlue', ' ', ' ')");
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

        private void txtsearchprod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnsubmit.PerformClick();
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
           
            if (e.Column.FieldName == "Cost")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }
    }
}