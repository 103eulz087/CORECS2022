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
    public partial class MerchantsDevEx : DevExpress.XtraEditors.XtraForm
    {
        string deptid, deptname;
        public MerchantsDevEx()
        {
            InitializeComponent();
        }

        private void MerchantsDevEx_Load(object sender, EventArgs e)
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
            txtterm.Text = "";
            txtlimit.Text = "";
        }

        void disablefields()
        { 
            txtdeptname.Enabled = false;
            txtterm.Enabled = false;
            txtlimit.Enabled = false;
        }
        void display()
        {
            Database.display("SELECT * FROM Merchants", gridControl1, gridView1);
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
            bool ok = Database.checkifExist("SELECT MerchantName FROM Merchants WHERE MerchantName='" + txtdeptname.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Merchants Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery($"INSERT INTO Merchants VALUES('{txtdeptname.Text}','{txtterm.Text}','{txtlimit.Text}','{Login.isglobalUserID}','{DateTime.Now.ToString()}','','')", "Successfully Added");
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
            Database.ExecuteQuery("UPDATE Merchants SET MerchantName='" + txtdeptname.Text + "',Term='"+txtterm.Text+"',Limit='"+txtlimit.Text+"',UpdatedBy='"+Login.isglobalUserID+"',DateUpdated='"+ DateTime.Now.ToString() + "' WHERE MerchantID='" + deptid + "'  ", "Successfully Updated!");
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Merchant");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Merchants WHERE MerchantID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MerchantID").ToString() + "' ", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deptid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MerchantID").ToString();
            deptname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MerchantName").ToString();
          
            txtdeptname.Text = deptname;
            txtlimit.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Limit").ToString();  
            txtterm.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Term").ToString(); 
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        void enablefields()
        {
           
            txtdeptname.Enabled = true;
            txtterm.Enabled = true;
            txtlimit.Enabled = true;
        }

    }
}