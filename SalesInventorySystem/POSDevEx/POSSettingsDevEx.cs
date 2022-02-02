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

namespace SalesInventorySystem.POSDevEx
{
    public partial class POSSettingsDevEx : DevExpress.XtraEditors.XtraForm
    {
        string brcode, posname;
        public POSSettingsDevEx()
        {
            InitializeComponent();
        }

        private void POSSettingsDevEx_Load(object sender, EventArgs e)
        {
            populateItems();
            display();
        }
        void display()
        {
            Database.display("SELECT * FROM POSSettings", gridControl1, gridView1);
        }
        void clear()
        {
            txtbrcode.Text = "";
            txtposname.Text = "";
            txtcompname.Text = "";
            txtaddress1.Text = "";
            txtaddress2.Text = "";
            txtbirpermitno.Text = "";
            txtminno.Text = "";
            txtserialno.Text = "";
            txttinno.Text = "";
        }

        void disablefields()
        {
            txtbrcode.Enabled = false;
            txtposname.Enabled = false;
            txtcompname.Enabled = false;
            txtaddress1.Enabled = false;
            txtaddress2.Enabled = false;
            txtbirpermitno.Enabled = false;
            txtminno.Enabled = false;
            txtserialno.Enabled = false;
            txttinno.Enabled = false;
        }
        void enablefields()
        {
            txtbrcode.Enabled = true;
            txtposname.Enabled = true;
            txtcompname.Enabled = true;
            txtaddress1.Enabled = true;
            txtaddress2.Enabled = true;
            txtbirpermitno.Enabled = true;
            txtminno.Enabled = true;
            txtserialno.Enabled = true;
            txttinno.Enabled = true;
        }
        void populateItems()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
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
            bool ok = Database.checkifExist("SELECT BranchCode FROM POSSettings WHERE BranchCode='" + txtbrcode.Text.Trim() + "' AND POSName='" + txtposname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO POSSettings VALUES('" + txtbrcode.Text + "','" + txtposname.Text + "','" + txtcompname.Text + "','" + txtaddress1.Text + "','" + txtaddress2.Text + "','" + txttinno.Text + "','" + txtminno.Text + "','" + txtbirpermitno.Text + "','" + txtserialno.Text + "')", "Successfully Added");
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
            Database.ExecuteQuery("UPDATE POSSettings SET POSName='" + txtposname.Text + "',CompanyName='" + txtcompname.Text + "',Address1='" + txtaddress1.Text + "',Address2='" + txtaddress2.Text + "',TinNo='" + txttinno.Text + "',MinNo='" + txtminno.Text + "',BirPermitNo='" + txtbirpermitno.Text + "',SerialNo='" + txtserialno.Text + "' WHERE BranchCode='" + brcode + "' AND POSName='" + posname + "' ", "Successfully Updated!");
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete POS Settings");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM POSSettings WHERE BranchCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString() + "' AND POSName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "POSName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            posname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "POSName").ToString();
           
            txtbrcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            txtposname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "POSName").ToString();
            txtcompname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CompanyName").ToString();
            txtaddress1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Address1").ToString();
            txtaddress2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Address2").ToString();
            txtbirpermitno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BirPermitNo").ToString();
            txtminno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MinNo").ToString();
            txtserialno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SerialNo").ToString();
            txttinno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TinNo").ToString();

            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}