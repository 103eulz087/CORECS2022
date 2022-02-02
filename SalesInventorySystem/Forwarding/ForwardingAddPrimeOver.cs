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
using System.Data.SqlClient;

namespace SalesInventorySystem.Forwarding
{
    public partial class ForwardingAddPrimeOver : DevExpress.XtraEditors.XtraForm
    {
        public ForwardingAddPrimeOver()
        {
            InitializeComponent();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txttripid.Text = IDGenerator.getIDbySP("sp_GetTripID","tripnumber",Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding")); //IDGenerator.getShipmentNumber().ToString();
            enablefields();
            display();
            populate();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        void populate()
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("Select CustomerID,CustomerName FROM Customers", txtconsignee, "CustomerID", "CustomerID");
            Database.displaySearchlookupEdit("Select * FROM Staff Where Designation='Driver'", txtdriver, "StaffName", "StaffName", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            Database.displaySearchlookupEdit("Select * FROM Staff Where Designation='Helper'", txthelper, "StaffName", "StaffName", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            Database.displaySearchlookupEdit("Select PlateNo,Make,Series,BodyType FROM TruckDetails ", txtplateno, "PlateNo", "PlateNo", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
        }

        private void ForwardingAddPrimeOver_Load(object sender, EventArgs e)
        {
            disablefields();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
            txtbranch.Text = "";
            txtconsignee.Text = "";
            txtcontainerno.Text = "";
            txtdatestart.Text = "";
            txtdriver.Text = "";
            txthelper.Text = "";
            txtlocation.Text = "";
            txtlocationto.Text = "";
            txtplateno.Text = "";
            txtpulloutdate.Text = "";
            txtrate.Text = "";
            txttripid.Text = "";
        }

        void disablefields()
        {
            txtbranch.Enabled = false;
            txtconsignee.Enabled = false;
            txtcontainerno.Enabled = false;
            txtdatestart.Enabled = false;
            txtdriver.Enabled = false;
            txthelper.Enabled = false;
            txtlocation.Enabled = false;
            txtlocationto.Enabled = false;
            txtplateno.Enabled = false;
            txtpulloutdate.Enabled = false;
            txtrate.Enabled = false;
            txttripid.Enabled = false;
        }
        void enablefields()
        {
            txtbranch.Enabled = true;
            txtconsignee.Enabled = true;
            txtcontainerno.Enabled = true;
            txtdatestart.Enabled = true;
            txtdriver.Enabled = true;
            txthelper.Enabled = true;
            txtlocation.Enabled = true;
            txtlocationto.Enabled = true;
            txtplateno.Enabled = true;
            txtpulloutdate.Enabled = true;
            txtrate.Enabled = true;
        }

        void display()
        {
            Database.display("SELECT * FROM view_Monitoring WHERE Status='PENDING'", gridControl1, gridView6, Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT * FROM Monitoring WHERE TripID='" + txttripid.Text.Trim() + "'", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Monitoring Table.. Please use Edit Function");
                return;
            }
            else
            {
                ExecuteSP("Add");
                display();
                clear();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;

                disablefields();
            }
        }

        void ExecuteSP(string condition)
        {
            SqlConnection con = null;
            try
            {
                con = Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding");
                con.Open();
                string query = "sp_InsertPrimeOver";
                SqlCommand com = new SqlCommand(query,con);
                com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                com.Parameters.AddWithValue("@parmconsignee", txtconsignee.Text);
                com.Parameters.AddWithValue("@parmcontainerno", txtcontainerno.Text);
                com.Parameters.AddWithValue("@parmdatestart", txtdatestart.Text);
                com.Parameters.AddWithValue("@parmdriver", txtdriver.Text);
                com.Parameters.AddWithValue("@parmhelper", txthelper.Text);
                com.Parameters.AddWithValue("@parmlocationfrom", txtlocation.Text);
                com.Parameters.AddWithValue("@parmlocationto", txtlocationto.Text);
                com.Parameters.AddWithValue("@parmplateno", txtplateno.Text);
                com.Parameters.AddWithValue("@parmpulloutdate", txtpulloutdate.Text);
                com.Parameters.AddWithValue("@parmrate", txtrate.Text);
                com.Parameters.AddWithValue("@parmtripid", txttripid.Text);
                com.Parameters.AddWithValue("@parmcondition", condition);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            ExecuteSP("Update");
            display();
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Monitoring");
            if (ok)
            {
                ExecuteSP("Delete");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtbranch.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "BranchCode").ToString();
            txtconsignee.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "Consignee").ToString();
            txtcontainerno.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "ContainerNo").ToString();
            txtdatestart.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "DateAdded").ToString();
            txtdriver.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "Driver").ToString();
            txthelper.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "Helper").ToString();
            txtlocation.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "LocationFrom").ToString();
            txtlocationto.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "LocationTo").ToString();
            txtplateno.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "PlateNo").ToString();
            txtpulloutdate.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "DatePullOut").ToString();
            txtrate.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "Rate").ToString();
            txttripid.Text = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "TripID").ToString();
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }
    }
}