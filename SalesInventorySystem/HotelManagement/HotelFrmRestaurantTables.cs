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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmRestaurantTables : DevExpress.XtraEditors.XtraForm
    {
        string charge, rate;
        public HotelFrmRestaurantTables()
        {
            InitializeComponent();
        }

        private void HotelFrmRestaurantTables_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
            txtcharges.Text = "";
            txtrate.Text = "";
        }

        void disablefields()
        {
            txtcharges.Enabled = false;
            txtrate.Enabled = false;
        }
        void enablefields()
        {
            txtcharges.Enabled = true;
            txtrate.Enabled = true;
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
            bool ok = Database.checkifExist("SELECT * FROM RestaurantTable WHERE TableNo='" + txtcharges.Text.Trim() + "'",Database.getCustomizeConnection());
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in RestaurantTable Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO RestaurantTable VALUES('" + txtcharges.Text + "','" + txtrate.Text + "','Available','"+DateTime.Now.ToShortDateString()+"','','"+Login.Fullname+"','')", "Successfully Added", Database.getCustomizeConnection());
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
            Database.ExecuteQuery("UPDATE RestaurantTable SET TableNo='" + txtcharges.Text + "',TableName='" + txtrate.Text + "',DateUpdated='"+ DateTime.Now.ToShortDateString() + "',UpdatedBy='"+ Login.Fullname + "' WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Updated!", Database.getCustomizeConnection());
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

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            charge = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TableNo").ToString();
            rate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TableName").ToString();
            txtcharges.Text = charge;
            txtrate.Text = rate;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Restaurant Table");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM RestaurantTable WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Deleted", Database.getCustomizeConnection());
                display();
            }
            else
            {
                return;
            }
        }

        void display()
        {
            Database.display("SELECT * FROM RestaurantTable", gridControl1, gridView1, Database.getCustomizeConnection());
        }

    }
}