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
    public partial class Custodian : DevExpress.XtraEditors.XtraForm
    {
        string id, name;
        public Custodian()
        {
            InitializeComponent();
        }

        private void Custodian_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }

        void clear()
        {
            txtcustodian.Text = "";
        }

        void disablefields()
        {
            txtcustodian.Enabled = false;
        }
        void enablefields()
        {
            txtcustodian.Enabled = true;
        }
        void display()
        {
            Database.display("SELECT * FROM Custodian", gridControl1, gridView1);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT Custodian FROM Custodian WHERE Custodian='" + txtcustodian.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Custodian Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Custodian VALUES('" + txtcustodian.Text + "')", "Successfully Added");
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Custodian");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Custodian WHERE ID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString() + "' AND Custodian='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Custodian").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString();
            name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Custodian").ToString();
            txtcustodian.Text = name;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
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

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Custodian SET Custodian='" + txtcustodian.Text + "' WHERE ID='" + id + "' AND Custodian='" + name + "' ", "Successfully Updated!");
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