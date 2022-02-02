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
    public partial class DepartmentsDevEx : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname;
        public DepartmentsDevEx()
        {
            InitializeComponent();
        }

        private void DepartmentsDevEx_Load(object sender, EventArgs e)
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

        void display()
        {
            Database.display("SELECT * FROM Departments", gridControl1, gridView1);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            bool ok = Database.checkifExist("SELECT DeptID FROM Departments WHERE DeptID='" + txtdeptid.Text.Trim() + "' AND DeptName='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Deparmtnets Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Departments VALUES('" + txtdeptid.Text + "','" + txtdeptname.Text + "')", "Successfully Added");
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
            Database.ExecuteQuery("UPDATE Departments SET DeptID='" + txtdeptid.Text + "',DeptName='" + txtdeptname.Text + "' WHERE DeptID='"+ deptid + "' AND DeptName='"+deptname+"' ", "Successfully Updated!");
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
                Database.ExecuteQuery("DELETE FROM Departments WHERE DeptID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeptID").ToString() + "' AND DeptName='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeptName").ToString()+"'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeptID").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeptName").ToString();
            txtdeptid.Text = deptid;
            txtdeptname.Text = deptname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}