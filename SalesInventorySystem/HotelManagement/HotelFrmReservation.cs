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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmReservation : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmReservation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcontactno.Text) || String.IsNullOrEmpty(txtnameofguest.Text) || String.IsNullOrEmpty(txtnumdays.Text) || String.IsNullOrEmpty(txtreservdate.Text) || String.IsNullOrEmpty(txtroomnum.Text))
            {
                XtraMessageBox.Show("Please filled out the form correctly!");
                return;
            }
            else
            {
                execute();
                XtraMessageBox.Show("Done!..");
                this.Dispose();   
            }
            
        }

        void execute()
        {
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {

                string query = "sp_ReserveRoom";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@reserveid", txtreserveid.Text);
                com.Parameters.AddWithValue("@roomnum", txtroomnum.Text);
                com.Parameters.AddWithValue("@guestname",txtnameofguest.Text);
                com.Parameters.AddWithValue("@contactno", txtcontactno.Text);
                com.Parameters.AddWithValue("@numdays", txtnumdays.Text);
                com.Parameters.AddWithValue("@reservationdate", txtreservdate.Text);
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

        private void txtnumdays_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
            
        }

        private void HotelFrmReservation_Load(object sender, EventArgs e)
        {
            txtreserveid.Text = IDGenerator.getReservationID().ToString();
        }
    }
}