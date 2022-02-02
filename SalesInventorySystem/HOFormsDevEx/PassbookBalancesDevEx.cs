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
    public partial class PassbookBalancesDevEx : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname,bankname;
        public PassbookBalancesDevEx()
        {
            InitializeComponent();
        }

        private void PassbookBalancesDevEx_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            Database.displaySearchlookupEdit("Select * FROM Banks", txtbank, "BankName", "BankName");
        }
        void clear()
        {

            txtbank.Text = "";
            txtdate.Text = "";
            txtbalance.Text = "";
        }

        void disablefields()
        {
            txtbank.Enabled = false;
            txtdate.Enabled = false;
            txtbalance.Enabled = false;
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
            txtbank.Enabled = true;
            txtdate.Enabled = true;
            txtbalance.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT PassbookDate FROM PassbookBalances WHERE PassbookDate='" + txtdate.Text.Trim() + "' AND EndingBalance='" + txtbalance.Text.Trim() + "' AND BankName='" + txtbank.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in PassbookBalances Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO PassbookBalances VALUES('"+txtbank.Text+"','" + txtdate.Text + "','" + txtbalance.Text + "')", "Successfully Added");
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
            Database.ExecuteQuery("UPDATE PassbookBalances SET BankName='"+txtbank.Text+"',PassbookDate='" + txtdate.Text + "',EndingBalance='" + txtbalance.Text + "' WHERE PassbookDate='" + deptid + "' AND EndingBalance='" + deptname + "' AND BankName='"+bankname+"'", "Successfully Updated!");
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete PassbookBalances");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM PassbookBalances WHERE PassbookDate='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PassbookDate").ToString() + "' AND EndingBalance='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EndingBalance").ToString() + "' AND BankName='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BankName").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PassbookDate").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EndingBalance").ToString();
            bankname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BankName").ToString();
            txtdate.Text = deptid;
            txtbalance.Text = deptname;
            txtbank.Text = bankname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        void display()
        {
            Database.display("SELECT * FROM PassbookBalances", gridControl1, gridView1);
        }


    }
}