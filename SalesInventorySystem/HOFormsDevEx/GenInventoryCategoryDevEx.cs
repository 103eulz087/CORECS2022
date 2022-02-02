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
    public partial class GenInventoryCategoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname;
        public GenInventoryCategoryDevEx()
        {
            InitializeComponent();
        }

        private void GenInventoryCategoryDevEx_Load(object sender, EventArgs e)
        {
            disablefields();
           
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM GenInventoryCategory", gridControl1, gridView1);
        }
        void clear()
        {
            txtdeptid.Text = "";
            txtdeptname.Text = "";
        }

        void disablefields()
        {
            txtdeptid.Enabled = false;
            txtdeptname.Enabled = false;
        }
        void enablefields()
        {
            txtdeptid.Enabled = true;
            txtdeptname.Enabled = true;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            if (Main.program == "SALESANDINVENTORY")
            {
                Database.display("SELECT * FROM GenInventoryCategory", gridControl1, gridView1);
            }
            else
            {
                Database.display("SELECT * FROM GenInventoryCategory", gridControl1, gridView1,Database.getCustomizeConnection());
            }
            enablefields();
            
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT Category FROM GenInventoryCategory WHERE Category='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Category Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO GenInventoryCategory VALUES('" + txtdeptname.Text + "','1')", "Successfully Added");
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

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CategoryID").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Category").ToString();
            txtdeptid.Text = deptid;
            txtdeptname.Text = deptname;
            enablefields();
          
            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Category");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM GenInventoryCategory WHERE CategoryID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CategoryID").ToString() + "' ", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
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

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE GenInventoryCategory SET Category='" + txtdeptname.Text + "' WHERE CategoryID='" + deptid + "' ", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }
        
    }
}