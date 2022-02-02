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
    public partial class GenInventoryItems : DevExpress.XtraEditors.XtraForm
    {
        string itemcode, itemname;
        public GenInventoryItems()
        {
            InitializeComponent();
        }

        private void GenInventoryItems_Load(object sender, EventArgs e)
        {
            display();
            populateCategory();
        }

        void populateCategory()
        {
            Database.displaySearchlookupEdit("select CategoryID,Category FROM GenInventoryCategory", searchLookUpEdit1, "Category", "Category");
        }

        String getCategoryCode()
        {
            string code = "";
            code = Database.getSingleQuery("GenInventoryCategory", "Category='" + searchLookUpEdit1.Text + "'", "CategoryID");
            return code;
        }
        String getCategoryName()
        {
            string name = "";
            name = Database.getSingleQuery("GenInventoryCategory", "Category='" + searchLookUpEdit1.Text + "'", "Category");
            return name;
        }

        int getLastProductCode()
        {
            int str=0;
            str = Database.getLastID("GenInventoryItems", "ItemCategory='" + getCategoryName() + "'", "ItemCode");
            return str;
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            int newid = 0;
            string createID = getCategoryCode() + "0000";

            if (getLastProductCode() != 0)
            {
                newid = getLastProductCode() + 1;
            }
            else
            {
                newid = Convert.ToInt32(createID);
            }
            txtprodcode.Text = newid.ToString();
            txtprodcode.ReadOnly = true;
            txtdesc.Focus();
        }

        void clear()
        {
            searchLookUpEdit1.Text = "";
            txtprodcode.Text = "";
            txtdesc.Text = "";
        }

        void disablefields()
        {
            searchLookUpEdit1.Enabled = false;
            txtprodcode.Enabled = false;
            txtdesc.Enabled = false;
        }
        void enablefields()
        {
            searchLookUpEdit1.Enabled = true;
            txtprodcode.Enabled = true;
            txtdesc.Enabled = true;
        }
        void display()
        {
            Database.display("SELECT * FROM GenInventoryItems", gridControl1, gridView1);
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            enablefields();
            display();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT ItemCode FROM GenInventoryItems WHERE ItemCode='" + txtprodcode.Text.Trim() + "' AND Description='" + txtdesc.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Inventory Items Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO GenInventoryItems VALUES('" + txtprodcode.Text + "','" + txtdesc.Text + "','"+searchLookUpEdit1.Text+"')", "Successfully Added");
                clear();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;

                disablefields();
                display();
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Inventory Items");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM GenInventoryItems WHERE ItemCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode").ToString() + "' AND Description='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE GenInventoryItems SET Description='" + txtdesc.Text.Trim() + "',ItemCategory='" + searchLookUpEdit1.Text + "' WHERE ItemCode='" + itemcode + "' AND Description='" + itemname + "' ", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itemcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemCode").ToString();
            itemname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            txtprodcode.Text = itemcode;
            txtdesc.Text = itemname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}