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

namespace SalesInventorySystem.POS
{
    public partial class POSInventoryMapping : DevExpress.XtraEditors.XtraForm
    {
        object ppcode, pdesc,cpcode,cpdesc;
        public POSInventoryMapping()
        {
            InitializeComponent();
        }

        private void txtchildproduct_EditValueChanged(object sender, EventArgs e)
        {
            cpcode = SearchLookUpClass.getSingleValue(txtchildproduct, "ProductCode");
            cpdesc = SearchLookUpClass.getSingleValue(txtchildproduct, "Description");
        }

        private void POSInventoryMapping_Load(object sender, EventArgs e)
        {
            populateProducts();
            display();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtparentproduct.Text) || String.IsNullOrEmpty(txtchildproduct.Text) || String.IsNullOrEmpty(txtqty.Text))
            {
                XtraMessageBox.Show("Must Not Empty Fields...");
                return;
            }
            else
            {
                Database.ExecuteQuery($"INSERT INTO InventoryMapping VALUES('{ppcode}','{pdesc}','{cpcode}','{cpdesc}','{txtqty.Text}',' ','')", "Successfully Added!!..");
                display();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            cancelLine();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }
        void cancelLine()
        {
            Database.ExecuteQuery($"DELETE FROM InventoryMapping WHERE ParentProductCode='{gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ParentProductCode").ToString()}' " +
               $"and ChildProductCode='{gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ChildProductCode").ToString()}' " +
               $"and Quantity='{gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Quantity").ToString()}'", "Successfully Added!!..");
            display();
        }
        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelLine();
        }

        void display()
        {
            Database.display("SELECT * FROM InventoryMapping",gridControl1,gridView2);
        }

        void populateProducts()
        {
            Database.displaySearchlookupEdit("SELECT a.ProductCode,a.Description,b.Description as Category " +
                "FROM Products as a " +
                "INNER JOIN ProductCategory as b " +
                "ON a.ProductCategoryCode=b.ProductCategoryID " +
                "WHERE a.BranchCode='" + Login.assignedBranch + "' ", txtparentproduct, "Description", "Description");
            Database.displaySearchlookupEdit("SELECT a.ProductCode,a.Description,b.Description as Category " +
               "FROM Products as a " +
               "INNER JOIN ProductCategory as b " +
               "ON a.ProductCategoryCode=b.ProductCategoryID " +
               "WHERE a.BranchCode='" + Login.assignedBranch + "' ", txtchildproduct, "Description", "Description");
        }

        private void txtparentproduct_EditValueChanged(object sender, EventArgs e)
        {
            ppcode = SearchLookUpClass.getSingleValue(txtparentproduct, "ProductCode");
            pdesc = SearchLookUpClass.getSingleValue(txtparentproduct, "Description");
        }
    }
}