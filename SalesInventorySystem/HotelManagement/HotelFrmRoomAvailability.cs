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
using DevExpress.XtraGrid.Views.Grid;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmRoomAvailability : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmRoomAvailability()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT * FROM RoomAvailability WHERE RoomDate >= '" + datefromforapproval.Text + "' and RoomDate <= '" + datetoforapproval.Text + "' and RoomNumber='" + HotelFrmRooms.roomnum + "'",Database.getCustomizeConnection());
            if(!ok)
            {
                bool confirm = HelperFunction.ConfirmDialog("This Room is currently No Schedule on this Date.. Are you want to Create Schedule in this Room?","Create Schedule");
                if(confirm)
                {
                    createSchedule();
                    XtraMessageBox.Show("You have succesfully create an Schedule on this Room Number!");
                    this.Dispose();
                }
                else
                {
                    return;
                }
            }
            else
            {
                getQeury();
            }
           
        }

        void createSchedule()
        {
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {

                string query = "sp_createCalendarDays";
                SqlCommand com = new SqlCommand(query, con);
                DataTable table = new DataTable();
                com.Parameters.AddWithValue("@datefrom", datefromforapproval.Text);
                com.Parameters.AddWithValue("@dateto", datetoforapproval.Text);
                com.Parameters.AddWithValue("@roomnum", HotelFrmRooms.roomnum);
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

        void getQeury()
        {
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                
                string query = "sp_roomAvailability";
                SqlCommand com = new SqlCommand(query, con);
               
                com.Parameters.AddWithValue("@datefrom", datefromforapproval.Text);
                com.Parameters.AddWithValue("@dateto", datetoforapproval.Text);
                com.Parameters.AddWithValue("@roomnum", HotelFrmRooms.roomnum);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gridControl1.DataSource = table;
               
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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void showRoomAvailabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomStatus").ToString() != "Available")
            {
                XtraMessageBox.Show("Only Available Rooms can be reserved!");
                return;
            }
            else
            {
                HotelFrmReservation reserve = new HotelFrmReservation();
                reserve.FormClosed += Reserve_FormClosed;
                reserve.Show();
                reserve.txtroomnum.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
                reserve.txtreservdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomDate").ToString();
            }
            //reserve.txtnameofguest.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
            //reserve.txtcontactno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
            //reserve.txtnumdays.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
            //reserve.txtreservdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString();
        }

        private void Reserve_FormClosed(object sender, FormClosedEventArgs e)
        {
            getQeury();
            //button1.PerformClick();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string status = view.GetRowCellDisplayText(e.RowHandle, view.Columns["RoomStatus"]).ToString();
                if (status == "Reserved")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
               
            }
        }

        private void HotelFrmRoomAvailability_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            datefromforapproval.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            datetoforapproval.Text = lastDay.ToShortDateString();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("Update RoomAvailability SET RoomStatus='Available' WHERE RoomNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomNumber").ToString() + "' and RoomDate='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomDate").ToString() + "' and RoomStatus='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RoomStatus").ToString() + "' ","Successfully Clear!!",Database.getCustomizeConnection());
            button1.PerformClick();
        }
    }
}