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
    public partial class HotelFrmRooms : DevExpress.XtraEditors.XtraForm
    {
        public static string roomnum = "",type="",desc="";
        public HotelFrmRooms()
        {
            InitializeComponent();
        }

        private void HotelFrmRooms_Load(object sender, EventArgs e)
        {
            disablefields();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }
        void clear()
        {
            txtdesc.Text = "";
            txtroomnum.Text = "";
            txttype.Text = "";
        }

        void disablefields()
        {
            txtdesc.Enabled = false;
            txtroomnum.Enabled = false;
            txttype.Enabled = false;
        }
        void enablefields()
        {
            txtdesc.Enabled = true;
            txtroomnum.Enabled = true;
            txttype.Enabled = true;
        }

        void display()
        {
            Database.display("SELECT * FROM Rooms", gridControl1, gridView1, Database.getCustomizeConnection());
            Database.displaySearchlookupEdit("Select Category FROM RoomCategory", txttype, "Category", "Category", Database.getCustomizeConnection());
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void showRoomAvailabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roomnum= gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
            HotelFrmRoomAvailability rmavailability = new HotelFrmRoomAvailability();
            rmavailability.Show();
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
            add();
            clear();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;

            disablefields();
            display();
        }

        void add()
        {
            if (String.IsNullOrEmpty(txtdesc.Text) || String.IsNullOrEmpty(txtroomnum.Text) || String.IsNullOrEmpty(txttype.Text))
            {
                XtraMessageBox.Show("Please input fields");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Rooms VALUES('" + txtroomnum.Text + "','" + txtdesc.Text + "','" + txttype.Text + "','Available','"+DateTime.Now.ToShortDateString()+"','')", "Successfully Added!", Database.getCustomizeConnection());
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Rooms SET RoomNumber='" + txtroomnum.Text + "',RoomDescription='" + txtdesc.Text + "',RoomType='" + txttype.Text + "',DateUpdated='"+DateTime.Now.ToShortDateString()+"'  WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Updated!", Database.getCustomizeConnection());
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Rooms");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Rooms WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Deleted", Database.getCustomizeConnection());
                display();
            }
            else
            {
                return;
            }
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

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            roomnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
            desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomDescription").ToString();
            type = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomType").ToString();

            txtdesc.Text = desc;
            txtroomnum.Text = roomnum;
            txttype.Text = type;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}