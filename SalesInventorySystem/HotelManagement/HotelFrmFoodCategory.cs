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
    public partial class HotelFrmFoodCategory : DevExpress.XtraEditors.XtraForm
    {
        string category="";
        public HotelFrmFoodCategory()
        {
            InitializeComponent();
        }

        private void HotelFrmFoodCategory_Load(object sender, EventArgs e)
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
          
        }

        void disablefields()
        {
            txtcharges.Enabled = false;
            
        }
        void enablefields()
        {
            txtcharges.Enabled = true;
        }
        void display()
        {
            Database.display("SELECT * FROM FoodCategory", gridControl1, gridView1,Database.getCustomizeConnection());
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

            bool ok = Database.checkifExist("SELECT * FROM FoodCategory WHERE CategoryName='" + txtcharges.Text.Trim() + "'",Database.getCustomizeConnection());
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in FoodCategory Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO FoodCategory VALUES('" + txtcharges.Text + "')", "Successfully Added", Database.getCustomizeConnection());
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
            Database.ExecuteQuery("UPDATE FoodCategory SET CategoryName='" + txtcharges.Text + "' WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Updated!", Database.getCustomizeConnection());
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
            category = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CategoryName").ToString();
            txtcharges.Text = category;
           
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Food Category");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM FoodCategory WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ", "Successfully Deleted",Database.getCustomizeConnection());
                display();
            }
            else
            {
                return;
            }
        }
    }
}