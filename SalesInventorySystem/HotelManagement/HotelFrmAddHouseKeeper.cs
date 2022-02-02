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
    public partial class HotelFrmAddHouseKeeper : DevExpress.XtraEditors.XtraForm
    {
        string name = "";
        public HotelFrmAddHouseKeeper()
        {
            InitializeComponent();
        }

        private void HotelFrmAddHouseKeeper_Load(object sender, EventArgs e)
        {
            disablefields();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }
        void clear()
        {
            txthousekepper.Text = "";
           
        }

        void disablefields()
        {
            txthousekepper.Enabled = false;
          
        }
        void enablefields()
        {
            txthousekepper.Enabled = true;

        }
        void display()
        {
            Database.display("SELECT * FROM RoomAttendant", gridControl1, gridView1, Database.getCustomizeConnection());
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
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
            bool isExist = Database.checkifExist("SELECT * FROM RoomAttendant WHERE HouseKeeper='" + txthousekepper.Text + "'", Database.getCustomizeConnection());
            if (String.IsNullOrEmpty(txthousekepper.Text))
            {
                XtraMessageBox.Show("Please input fields");
                return;
            }
            else if(isExist)
            {
                XtraMessageBox.Show("HouseKeeper Name Already Exist");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO RoomAttendant VALUES('" + txthousekepper.Text + "')", "Successfully Added!", Database.getCustomizeConnection());
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Rooms SET RoomAttendant='" + txthousekepper.Text + "' WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Updated!", Database.getCustomizeConnection());
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Attendant");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM RoomAttendant WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Deleted", Database.getCustomizeConnection());
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
            name = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "HouseKeeper").ToString();

            txthousekepper.Text = Name;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}