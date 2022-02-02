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
    public partial class HotelFrmRoomCategory : DevExpress.XtraEditors.XtraForm
    {
        string catid = "",catname="";
        public HotelFrmRoomCategory()
        {
            InitializeComponent();
        }

        private void HotelFrmRoomCategory_Load(object sender, EventArgs e)
        {
            disablefields();
            display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
            txtroomcategory.Text = "";
        }

        void disablefields()
        {
            txtroomcategory.Enabled = false;
        }
        void enablefields()
        {
            txtroomcategory.Enabled = true;
        }

        void display()
        {
            Database.display("SELECT * FROM RoomCategory", gridControl1, gridView1, Database.getCustomizeConnection());
        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {
            Database.ExecuteQuery("INSERT INTO RoomCategory VALUES( '" + txtroomcategory.Text + "')", "Successfully Added!", Database.getCustomizeConnection());
            clear();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;

            disablefields();
            display();
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
            Database.ExecuteQuery("UPDATE RoomCategory SET Category='" + txtroomcategory.Text + "'  WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Updated!",Database.getCustomizeConnection());
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete RoomCategory");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM RoomCategory WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Deleted",Database.getCustomizeConnection());
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            catid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString();
            catname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Category").ToString();
            txtroomcategory.Text = catname;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}