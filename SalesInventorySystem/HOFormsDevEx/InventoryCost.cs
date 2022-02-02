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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryCost : DevExpress.XtraEditors.XtraForm
    {
        object supplierkey;
        public InventoryCost()
        {
            InitializeComponent();
        }

        private void InventoryCost_Load(object sender, EventArgs e)
        {
            populateSupplier();
            //display();
        }
        void populateSupplier()
        {
            Database.displaySearchlookupEdit("select SupplierKey,SupplierName FROM Supplier", txtsuppliers);
        }
        void display()
        {
            Database.display("SELECT * FROM dbo.func_viewInventoryCost('"+Login.assignedBranch+ "') WHERE SupplierID='" + supplierkey + "'", gridControl1, gridView1);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtsuppliers.Text))
            {
                XtraMessageBox.Show("Please select Supplier..");
            }
            else
            {
                HOFormsDevEx.InventoryCostSearchProduct orderko = new InventoryCostSearchProduct();
                orderko.txtsupplierid.Text = supplierkey.ToString();
                orderko.ShowDialog(this);
                if (HOFormsDevEx.InventoryCostSearchProduct.isdone == true)
                {
                    HOFormsDevEx.InventoryCostSearchProduct.isdone = false;
                    orderko.Dispose();
                    display();
                }
            }
        }

        private void txtsuppliers_EditValueChanged(object sender, EventArgs e)
        {
            supplierkey=SearchLookUpClass.getSingleValue(txtsuppliers,"SupplierKey");
            Database.display("SELECT * FROM dbo.func_viewInventoryCost('" + Login.assignedBranch + "') WHERE SupplierID='"+ supplierkey + "'", gridControl1, gridView1);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Delete this Item?..", "Confirm Delete");
            if(confirm)
            {
                Database.ExecuteQuery("DELETE FROM InventoryCost " +
              "WHERE SupplierID= " + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "  " +
              "AND ProductCode= " + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString() + "  ", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
          
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.InventoryCostEdit invcostedit = new InventoryCostEdit();
            invcostedit.txtsuppkey.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"SupplierID").ToString();
            invcostedit.txtsuppname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"SupplierName").ToString();
            invcostedit.txtprodcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ProductCode").ToString();
            invcostedit.txtprodname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ProductName").ToString();
            invcostedit.txtcost.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Cost").ToString();
            invcostedit.ShowDialog(this);
            if(HOFormsDevEx.InventoryCostEdit.isdone==true)
            {
                HOFormsDevEx.InventoryCostEdit.isdone = false;
                invcostedit.Dispose();
                display();
            }
        }

        private void chckall_CheckedChanged(object sender, EventArgs e)
        {
            if(chckall.Checked==true)
            {
                Database.display("SELECT * FROM dbo.func_viewInventoryCost('" + Login.assignedBranch + "') ", gridControl1, gridView1);
            }
        }
    }
}