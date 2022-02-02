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
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class CustomerProductSettingsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public CustomerProductSettingsDevEx()
        {
            InitializeComponent();
        }

        private void CustomerProductSettingsDevEx_Load(object sender, EventArgs e)
        {
            populateCustomer();
            populateProductCategory();
            display();
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["CustName"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 2);
        }

        void populateCustomer()
        {
            Database.displaySearchlookupEdit("select distinct CustomerID,CustomerName FROM Customers ORDER BY CustomerName", txtcustomers);
        }

        void populateProductCategory()
        {
            Database.displayDevComboBoxItems("Select * FROM ProductCategory", "Description", txtprodcat);
        }
        void display()
        {
            // Database.displayLocalGrid("SELECT * FROM view_InventoryCost", dataGridView1);
            Database.display("SELECT * FROM view_custprodsettings ORDER BY Custname", gridControl1, gridView1);
            //gridView1.Columns["ID"].Visible = false;
        }
        String getCustKey()
        {
            string str = "";
            str = Database.getSingleData("Customers", "CustomerName", txtcustomers.Text, "CustomerKey");
            return str;
        } String getCustID()
        {
            string str = "";
            str = Database.getSingleData("Customers", "CustomerName", txtcustomers.Text, "CustomerID");
            return str;
        }
        String getProductCategoryCode()
        {
            string str = "";
            str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text + "'", "ProductCategoryID");
            return str;
        }
        String getPrimalProductCode()
        {
            string str = "";
            //str = Database.getSingleData("Products", "Description", txtproduct.Text, "ProductCode");
            str = Database.getSingleQuery("Products", "Description='" + txtproducts.Text + "' AND ProductCategoryCode='" + getProductCategoryCode() + "' AND BranchCode='888'", "ProductCode");
            return str;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtprodcat.Text == "" || txtcustomers.Text == "" || txtproducts.Text == "" || txtcostkg.Text == "")
            {
                XtraMessageBox.Show("Please input all the forms correctly");
                return;
            }
            else if (Convert.ToDouble(txtcostkg.Text) < 0)
            {
                XtraMessageBox.Show("Cost/Kg must Greater than Negative Value!");
                return;
            }
            else
            {
                bool isexist = Database.checkifExist("SELECT CustID FROM CustomerProductSetting WHERE CustID='" + getCustID() + "' AND ProductCode='" + getPrimalProductCode() + "'");
                if (isexist)
                {
                    XtraMessageBox.Show("Supplier and Product Code are AlreadyExist!");
                    return;
                }
                else
                {
                    Database.ExecuteQuery("INSERT INTO CustomerProductSetting VALUES('"+ getCustKey() + "'" +
                        ",'" + getCustID() + "'" +
                        ",'"+txtcustomers.Text+"'" +
                        ",'" + getPrimalProductCode() + "'" +
                        ",'" + txtproducts.Text + "'" +
                        ",'" + txtcostkg.Text + "'" +
                        ",'"+txtremarks.Text+"')", "Successfully Added!");
                    display();
                    clear();
                }
            }
        }
        void clear()
        {
            txtcostkg.Text = "";
            txtprodcat.Text = "";
            txtproducts.Text = "";
            txtcustomers.Text = "";
            txtremarks.Text = "";
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("select Description FROM Products WHERE ProductCategoryCode='" + getProductCategoryCode() + "' and BranchCode='888' ORDER BY ProductCode", txtproducts, "Description", "Description");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsBehavior.ReadOnly = false;
            btnupdate.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE CustomerProductSetting Set SpecialPriceAmount='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SpecialPriceAmount").ToString() + "',Remarks='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Remarks").ToString() + "' WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'", "Successfully Update");
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            btnupdate.Enabled = false;
            clear();
            display();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM CustomerProductSetting " +
                "WHERE CustomerKey='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerKey").ToString() + "' " +
                "AND ProductCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString() + "'", "Successfully Deleted");
            clear();
            display();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "SpecialPriceAmount")
                e.Cancel = false;
            if (view.FocusedColumn.FieldName == "Remarks")
                e.Cancel = false;
        }
    }
}