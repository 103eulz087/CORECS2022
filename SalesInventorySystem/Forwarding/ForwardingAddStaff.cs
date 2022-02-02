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

namespace SalesInventorySystem.Forwarding
{
    public partial class ForwardingAddStaff : DevExpress.XtraEditors.XtraForm
    {
        string staffname, designation;
        public ForwardingAddStaff()
        {
            InitializeComponent();
        }

        private void ForwardingAddStaff_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
            txtstaffname.Text = "";
            txtdesignation.Text = "";
        }

        void disablefields()
        {
            txtstaffname.Enabled = false;
            txtdesignation.Enabled = false;
        }
        void enablefields()
        {
            txtstaffname.Enabled = true;
            txtdesignation.Enabled = true;
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
            bool ok = Database.checkifExist("SELECT * FROM Staff WHERE StaffName='" + txtstaffname.Text.Trim() + "' AND Designation='" + txtdesignation.Text.Trim() + "'", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Staff Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Staff VALUES('" + txtstaffname.Text + "','" + txtdesignation.Text + "')", "Successfully Added", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
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
            Database.ExecuteQuery("UPDATE Staff SET StaffName='" + txtstaffname.Text + "',Designation='" + txtdesignation.Text + "' WHERE StaffName='" + staffname + "' AND Designation='" + designation + "' ", "Successfully Updated!", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Staff");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Staff WHERE StaffName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StaffName").ToString() + "' AND Designation='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Designation").ToString() + "'", "Successfully Deleted", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staffname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "StaffName").ToString();
            designation = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Designation").ToString();
            txtstaffname.Text = staffname;
            txtdesignation.Text = designation;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        void display()
        {
            Database.display("SELECT * FROM Staff", gridControl1, gridView1,Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
        }
    }
}