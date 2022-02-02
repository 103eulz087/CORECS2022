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
    public partial class HotelFrmCheckin : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmCheckin()
        {
            InitializeComponent();
        }

        private void txtid_EditValueChanged(object sender, EventArgs e)
        {
            string firstname = Database.getSingleQuery("GuestInfo", "GuestID='" + txtid.Text + "'", "FirstName", Database.getCustomizeConnection());
            string mname = Database.getSingleQuery("GuestInfo", "GuestID='" + txtid.Text + "'", "MiddleName", Database.getCustomizeConnection());
            string lname = Database.getSingleQuery("GuestInfo", "GuestID='" + txtid.Text + "'", "LastName", Database.getCustomizeConnection());
            string contactno = Database.getSingleQuery("GuestInfo", "GuestID='" + txtid.Text + "'", "ContactNo", Database.getCustomizeConnection());
            string fullname = firstname + ' ' + mname + ' ' + lname;
            txtname.Text = fullname;
            txtcontactno.Text = contactno;
        }

        private void txtpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtpaytype.Text=="FULLYPAID")
            {
                labelControl11.Visible = false;
                txtpartialamount.Visible = false;
            }
            else
            {
                labelControl11.Visible = true;
                txtpartialamount.Visible = true;
                txtpartialamount.Focus();
            }
        }

        private void txtratetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string price = Database.getSingleQuery("RoomRates", "RoomRate='" + txtratetype.Text + "'", "RoomPrice",Database.getCustomizeConnection());
            txtroomrate.Text = price;
            double totalamount = 0.0;
            double numdays = 0.0;
            DateTime startdate = Convert.ToDateTime(txtcheckindate.Text);
            DateTime enddate = Convert.ToDateTime(txtcheckoutdate.Text);
            numdays = (enddate - startdate).TotalDays;
            totalamount = Convert.ToDouble(txtroomrate.Text) * numdays;
            txttotalamount.Text = totalamount.ToString();
        }

        private void radcash_CheckedChanged(object sender, EventArgs e)
        {
            if(radcash.Checked==true)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

        private void radcredit_CheckedChanged(object sender, EventArgs e)
        {
            if (radcash.Checked == true)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            execute();
            XtraMessageBox.Show("Successfully Added");
            this.Dispose();
        }

      
        void execute()
        {
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string paymentmethod = "";
                if (radcash.Checked == true)
                { paymentmethod = "CASH"; }
                else
                { paymentmethod = "CREDIT"; }

                string query = "sp_checkin";
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@bookingid", txtbookingno.Text);
                com.Parameters.AddWithValue("@resid", txtresid.Text);
                com.Parameters.AddWithValue("@guestid", txtid.Text);
                com.Parameters.AddWithValue("@guestname", txtname.Text);
                com.Parameters.AddWithValue("@guestcontact", txtcontactno.Text);
                com.Parameters.AddWithValue("@parmcheckindate", txtcheckindate.Text);
                com.Parameters.AddWithValue("@parmcheckoutdate", txtcheckoutdate.Text);
                com.Parameters.AddWithValue("@parmroomnum", txtroomnum.Text);
                com.Parameters.AddWithValue("@parmpaymentmethod", paymentmethod);
                com.Parameters.AddWithValue("@parmpaymenttype", txtpaytype.Text);
                com.Parameters.AddWithValue("@parminitialpayment", txtpartialamount.Text);
                com.Parameters.AddWithValue("@parmroomrate", txtroomrate.Text);
                com.Parameters.AddWithValue("@parmroomtype", txtroomtype.Text);
                com.Parameters.AddWithValue("@parmratetype", txtratetype.Text);
                com.Parameters.AddWithValue("@parmnoofpersons", txtnoofpersons.Text);

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

        private void HotelFrmCheckin_Load(object sender, EventArgs e)
        {
            txtbookingno.Text = IDGenerator.getBookingNo().ToString();
        }

        private void txtnoofpersons_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txtcheckindate_EditValueChanged(object sender, EventArgs e)
        {
            SalesInventorySystem.Classes.Utilities.setMinimumDateDevEx(txtcheckindate.Text, txtcheckoutdate);
        }
    }
}