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
    public partial class LiquidationList : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname;
        public LiquidationList()
        {
            InitializeComponent();
        }

        private void LiquidationList_Load(object sender, EventArgs e)
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

            txtdeptname.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT LiquidationName FROM LiquidationList WHERE LiquidationName='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in LiquidationList Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO LiquidationList VALUES('" + txtdeptname.Text + "')", "Successfully Added");
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete LiquidationList");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM LiquidationList WHERE LiquidationID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LiquidationID").ToString() + "' AND LiquidationName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LiquidationName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LiquidationID").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LiquidationName").ToString();

            txtdeptname.Text = deptname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE LiquidationList SET LiquidationName='" + txtdeptname.Text + "' WHERE LiquidationID='" + deptid + "' AND LiquidationName='" + deptname + "' ", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM LiquidationList", gridControl1, gridView1);
        }

    }
}