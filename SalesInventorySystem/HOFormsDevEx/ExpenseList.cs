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
    public partial class ExpenseList : DevExpress.XtraEditors.XtraForm
    {
        string deptid,deptname;
        public ExpenseList()
        {
            InitializeComponent();
        }

        private void ExpenseList_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
           
            txtdeptname.Text = "";
        }

        void disablefields()
        {
            
            txtdeptname.Enabled = false;
        }
        void enablefields()
        {
          
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
            Database.display("SELECT * FROM ExpensesList", gridControl1, gridView1);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT ExpenseName FROM ExpensesList WHERE ExpenseName='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in ExpensesList Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO ExpensesList VALUES('" + txtdeptname.Text + "')", "Successfully Added");
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete ExpenseList");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM ExpensesList WHERE ExpenseID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ExpenseID").ToString() + "' AND ExpenseName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ExpenseName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

      

        private void editItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ExpenseID").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ExpenseName").ToString();

            txtdeptname.Text = deptname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE ExpensesList SET ExpenseName='" + txtdeptname.Text + "' WHERE ExpenseID='" + deptid + "' AND ExpenseName='" + deptname + "' ", "Successfully Updated!");
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