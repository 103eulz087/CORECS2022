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
    public partial class BranchDevEx : DevExpress.XtraEditors.XtraForm
    {
        string brcode, brname;
        public BranchDevEx()
        {
            InitializeComponent();
        }

        private void BranchDevEx_Load_1(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
            txtbrcode.Text = "";
            txtbrname.Text = "";
            txtaddress.Text = "";
            txtmanager.Text = "";
            txtcashier.Text = "";
        }

        void disablefields()
        {
            txtbrcode.Enabled = false;
            txtbrname.Enabled = false;
            txtaddress.Enabled = false;
            txtmanager.Enabled = false;
            txtcashier.Enabled = false;
        }
        void enablefields()
        {
            txtbrcode.Enabled = true;
            txtbrname.Enabled = true;
            txtaddress.Enabled = true;
            txtmanager.Enabled = true;
            txtcashier.Enabled = true;
        }
        void display()
        {
            Database.display("SELECT * FROM Branches", gridControl1, gridView1);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT BranchCode FROM Branches WHERE BranchCode='" + txtbrcode.Text.Trim() + "' AND BranchName='" + txtbrname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Deparmtnets Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Branches VALUES('" + txtbrcode.Text + "','" + txtbrname.Text + "','" + txtaddress.Text + "','" + txtmanager.Text + "','" + txtcashier.Text + "')", "Successfully Added");
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
            Database.ExecuteQuery("UPDATE Branches SET BranchCode='" + txtbrcode.Text + "',BranchName='" + txtbrname.Text + "',Address='"+txtaddress.Text+"',SignatoryManager='"+txtmanager.Text+"',SignatoryCashier='"+txtcashier.Text+"' " +
                "WHERE BranchCode='" + brcode + "' AND BranchName='" + brname + "' ", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Department");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Branches WHERE BranchCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString() + "' AND BranchName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtbrcode.Enabled = true;
            txtbrname.Enabled = true;
            txtcashier.Enabled = true;
            txtmanager.Enabled = true;
            txtaddress.Enabled = true;

            display();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            brname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchName").ToString();
            txtbrcode.Text = brcode;
            txtbrname.Text = brname;
            txtaddress.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Address").ToString(); 
            txtmanager.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SignatoryManager").ToString(); 
            txtcashier.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SignatoryCashier").ToString(); 
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

    }
}