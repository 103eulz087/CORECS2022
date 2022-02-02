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
    public partial class AddCompanyDevFrm : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname;
        public AddCompanyDevFrm()
        {
            InitializeComponent();
        }

        private void AddCompanyDevFrm_Load(object sender, EventArgs e)
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

        private void btnnew_Click(object sender, EventArgs e)
        {
            enablefields();
            display();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        void enablefields()
        {
            txtdeptid.Enabled = true;
            txtdeptname.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT CompanyID FROM CompanyProfile WHERE CompanyID='" + txtdeptid.Text.Trim() + "' AND CompanyName='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Deparmtnets Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO CompanyProfile VALUES('" + txtdeptid.Text + "','" + txtdeptname.Text + "','','')", "Successfully Added");
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
            Database.ExecuteQuery("UPDATE CompanyProfile SET CompanyID='" + txtdeptid.Text + "',CompanyName='" + txtdeptname.Text + "' WHERE CompanyID='" + deptid + "' AND CompanyName='" + deptname + "' ", "Successfully Updated!");
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
                Database.ExecuteQuery("DELETE FROM CompanyProfile WHERE CompanyID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CompanyID").ToString() + "' AND CompanyName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CompanyName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CompanyID").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CompanyName").ToString();
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
            Database.display("SELECT BranchCode,CompanyName FROM CompanyProfile", gridControl1, gridView1);
        }
    }
}