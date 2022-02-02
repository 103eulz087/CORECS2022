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
    public partial class ForwardingTruckDetails : DevExpress.XtraEditors.XtraForm
    {
        public ForwardingTruckDetails()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ForwardingTruckDetails_Load(object sender, EventArgs e)
        {
            disablefields();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
        void clear()
        {
            txtbodyno.Text = "";
            txtbodytype.Text = "";
            txtchassisno.Text = "";
            txtcrno.Text = "";
            txtcylinders.Text = "";
            txtdatereg.Text = "";
            txtdenomination.Text = "";
            txtengineno.Text = "";
            txtfuel.Text = "";
            txtgrosswt.Text = "";
            txtmake.Text = "";
            txtmvfileno.Text = "";
            txtnetcapacity.Text = "";
            txtnetwt.Text = "";
            txtpiston.Text = "";
            txtplateno.Text = "";
            txtseries.Text = "";
            txtshippingwt.Text = "";
            txtyrmodel.Text = "";
        }

        void disablefields()
        {
            txtbodyno.Enabled = false;
            txtbodytype.Enabled = false;
            txtchassisno.Enabled = false;
            txtcrno.Enabled = false;
            txtcylinders.Enabled = false;
            txtdatereg.Enabled = false;
            txtdenomination.Enabled = false;
            txtengineno.Enabled = false;
            txtfuel.Enabled = false;
            txtgrosswt.Enabled = false;
            txtmake.Enabled = false;
            txtmvfileno.Enabled = false;
            txtnetcapacity.Enabled = false;
            txtnetwt.Enabled = false;
            txtpiston.Enabled = false;
            txtplateno.Enabled = false;
            txtseries.Enabled = false;
            txtshippingwt.Enabled = false;
            txtyrmodel.Enabled = false;
        }
        void enablefields()
        {
            txtbodyno.Enabled = true;
            txtbodytype.Enabled = true;
            txtchassisno.Enabled = true;
            txtcrno.Enabled = true;
            txtcylinders.Enabled = true;
            txtdatereg.Enabled = true;
            txtdenomination.Enabled = true;
            txtengineno.Enabled = true;
            txtfuel.Enabled = true;
            txtgrosswt.Enabled = true;
            txtmake.Enabled = true;
            txtmvfileno.Enabled = true;
            txtnetcapacity.Enabled = true;
            txtnetwt.Enabled = true;
            txtpiston.Enabled = true;
            txtplateno.Enabled = true;
            txtseries.Enabled = true;
            txtshippingwt.Enabled = true;
            txtyrmodel.Enabled = true;
        }
        void display()
        {
            Database.display("SELECT * FROM view_TruckDetails", gridControlMain, gridViewMain, Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
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
            bool ok = Database.checkifExist("SELECT * FROM view_TruckDetails WHERE PlateNo='" + txtplateno.Text.Trim() + "'", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in TruckDetails Table.. Please use Edit Function");
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
                string query = "sp_InsertCR";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmplateno", txtplateno.Text);
                com.Parameters.AddWithValue("@parmmvfileno", txtmvfileno.Text);
                com.Parameters.AddWithValue("@parmengineno", txtengineno.Text);
                com.Parameters.AddWithValue("@parmchassisno", txtchassisno.Text);
                com.Parameters.AddWithValue("@parmdenomination", txtdenomination.Text);
                com.Parameters.AddWithValue("@parmpiston", txtpiston.Text);
                com.Parameters.AddWithValue("@parmcylinders", txtcylinders.Text);
                com.Parameters.AddWithValue("@parmfuel", txtfuel.Text);
                com.Parameters.AddWithValue("@parmmake", txtmake.Text);
                com.Parameters.AddWithValue("@parmseries", txtseries.Text);
                com.Parameters.AddWithValue("@parmbodytype", txtbodytype.Text);
                com.Parameters.AddWithValue("@parmbodyno", txtbodyno.Text);
                com.Parameters.AddWithValue("@parmyearmodel", txtyrmodel);
                com.Parameters.AddWithValue("@parmgrosswt", txtgrosswt);
                com.Parameters.AddWithValue("@parmnetwt", txtnetwt);
                com.Parameters.AddWithValue("@parmshippingwt", txtshippingwt);
                com.Parameters.AddWithValue("@parmnetcapacity", txtnetcapacity);
                com.Parameters.AddWithValue("@parmcrno", txtcrno);
                com.Parameters.AddWithValue("@parmdatereg", txtdatereg);
                com.Parameters.AddWithValue("@parmcondition", condition);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
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

        private void gridControlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlMain, e.Location);
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
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete Trucks");
            if (ok)
            {
                //ExecuteSP("Delete");
                Database.ExecuteLocalQuery("DELETE FROM TruckDetails WHERE PlateNo='" + gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "PlateNo").ToString() + "'", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtbodyno.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "BodyNo").ToString();
            txtbodytype.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "BodyType").ToString();
            txtchassisno.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "ChassisNo").ToString();
            txtcrno.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "CRNo").ToString();
            txtcylinders.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "NumCylinders").ToString();
            txtdatereg.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "DateRegistered").ToString();
            txtdenomination.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "Denomination").ToString();
            txtengineno.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "EngineNo").ToString();
            txtfuel.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "Fuel").ToString();
            txtgrosswt.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "GrossWt").ToString();
            txtmake.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "Make").ToString();
            txtmvfileno.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "MVFileNo").ToString();
            txtnetcapacity.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "NetCapacity").ToString();
            txtnetwt.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "NetWt").ToString();
            txtpiston.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "PistonDisplacement").ToString();
            txtplateno.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "PlateNo").ToString();
            txtseries.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "Series").ToString();
            txtshippingwt.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "ShippingWt").ToString();
            txtyrmodel.Text = gridViewMain.GetRowCellValue(gridViewMain.FocusedRowHandle, "YearModel").ToString();
            enablefields();
            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void gridViewMain_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }
    }
}