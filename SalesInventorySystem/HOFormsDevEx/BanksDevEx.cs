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
    public partial class BanksDevEx : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname;
        public BanksDevEx()
        {
            InitializeComponent();
        }

        private void BanksDevEx_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
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
            enablefields();
            display();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT BankCode FROM Banks WHERE BankCode='" + txtdeptid.Text.Trim() + "' AND BankName='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Banks Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Banks VALUES('" + txtdeptid.Text + "','" + txtdeptname.Text + "')", "Successfully Added");
                clear();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;

                disablefields();
                display();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Banks SET BankCode='" + txtdeptid.Text + "',BankName='" + txtdeptname.Text + "' WHERE BankCode='" + deptid + "' AND BankName='" + deptname + "' ", "Successfully Updated!");
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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Banks");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Banks WHERE BankCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BankCode").ToString() + "' AND BankName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BankName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BankCode").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BankName").ToString();
            txtdeptid.Text = deptid;
            txtdeptname.Text = deptname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        void display()
        {
            Database.display("SELECT * FROM Banks", gridControl1, gridView1);
        }
    }
}