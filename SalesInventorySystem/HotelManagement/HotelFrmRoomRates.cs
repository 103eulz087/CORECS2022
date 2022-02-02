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
    public partial class HotelFrmRoomRates : DevExpress.XtraEditors.XtraForm
    {
        string type = "", price = "",rate="";
        public HotelFrmRoomRates()
        {
            InitializeComponent();
        }

        private void HotelFrmRoomRates_Load(object sender, EventArgs e)
        {
            disablefields();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            populateDisplay();
        }
        void clear()
        {
            txtroomcategory.Text = "";
            txtroomprice.Text = "";
            txtroomrate.Text = "";
        }

        void disablefields()
        {
            txtroomcategory.Enabled = false;
            txtroomprice.Enabled = false;
            txtroomrate.Enabled = false;
        }
        void enablefields()
        {
            txtroomcategory.Enabled = true;
            txtroomprice.Enabled = true;
            txtroomrate.Enabled = true;
        }

        void populateDisplay()
        {
            Database.displaySearchlookupEdit("SELECT * FROM RoomCategory", txtroomcategory, "Category", "Category", Database.getCustomizeConnection());
            Database.displaySearchlookupEdit("SELECT * FROM Rates", txtroomrate, "Rates", "Rates", Database.getCustomizeConnection());
            Database.display("SELECT * FROM RoomRates", gridControl1, gridView1, Database.getCustomizeConnection());
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add();
            populateDisplay();
        }

        void add()
        {
            if (String.IsNullOrEmpty(txtroomprice.Text) || String.IsNullOrEmpty(txtroomcategory.Text) || String.IsNullOrEmpty(txtroomrate.Text))
            {
                XtraMessageBox.Show("Please input fields");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO RoomRates VALUES('" + txtroomcategory.Text + "','" + txtroomprice.Text + "','" + txtroomrate.Text + "')", "Successfully Added!",Database.getCustomizeConnection());

            }
            populateDisplay();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            enablefields();
            populateDisplay();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE RoomRates SET RoomType='" + txtroomcategory.Text + "',RoomPrice='"+txtroomprice.Text+"',RoomRate='"+txtroomrate.Text+"'  WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Updated!",Database.getCustomizeConnection());
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            populateDisplay();
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            add();
            clear();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;

            disablefields();
            populateDisplay();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete RoomCategory");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM RoomRates WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Deleted",Database.getCustomizeConnection());
                clear();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;

                disablefields();
                populateDisplay();
            }
            else
            {
                return;
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomType").ToString();
            price = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomPrice").ToString();
            rate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomRate").ToString();
            txtroomcategory.Text = type;
            txtroomprice.Text = price;
            txtroomrate.Text = rate;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}