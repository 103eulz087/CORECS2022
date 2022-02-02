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
    public partial class HotelFrmTransferRoom : DevExpress.XtraEditors.XtraForm
    {
        double numdays=0.0;
        double balance = 0.0, rate = 0.0,refund=0.0,variance=0.0;
        public HotelFrmTransferRoom()
        {
            InitializeComponent();
        }

        private void gridViewOrders_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridViewAvailable_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            

        }
        void display()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "sp_GetTransferRoom";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbookingid", HotelFrmMainDashBoard.bookingid);
                com.Parameters.Add("@parmreservationid", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmguestid", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmcheckindate", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmcheckoutdate", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmguestname", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmguestcontactno", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmroomnumber", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmpaymentstatus", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmroomtype", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmratetype", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmroomrate", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmmethodofpayment", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtaxcharge", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnumdays", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalamount", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parminitialpayment", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmbalance", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmrefund", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofperson", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                txtbookingid.Text = HotelFrmMainDashBoard.bookingid;
                txtreservationid.Text = com.Parameters["@parmreservationid"].Value.ToString();
                txtguestid.Text = com.Parameters["@parmguestid"].Value.ToString();
                txtcheckindate.Text = com.Parameters["@parmcheckindate"].Value.ToString();
                txtcheckoutdate.Text = com.Parameters["@parmcheckoutdate"].Value.ToString();
                txtroomnum.Text = com.Parameters["@parmroomnumber"].Value.ToString();
                txtguestname.Text = com.Parameters["@parmguestname"].Value.ToString();
                txtguestcontactno.Text = com.Parameters["@parmguestcontactno"].Value.ToString();
                txtpaymentstatus.Text = com.Parameters["@parmpaymentstatus"].Value.ToString();
                txtroomtype.Text = com.Parameters["@parmroomtype"].Value.ToString();
                txtratetype.Text = com.Parameters["@parmratetype"].Value.ToString();
                txtroomrate.Text = com.Parameters["@parmroomrate"].Value.ToString();
                txtmethodofpayment.Text = com.Parameters["@parmmethodofpayment"].Value.ToString();
                txtnumdays.Text = com.Parameters["@parmnumdays"].Value.ToString();
                txttotalamount.Text = com.Parameters["@parmtotalamount"].Value.ToString();
                txtinitialpayment.Text = com.Parameters["@parminitialpayment"].Value.ToString();
                txtbalance.Text = com.Parameters["@parmbalance"].Value.ToString();
                txtrefund.Text = com.Parameters["@parmrefund"].Value.ToString();
                txtnoofperson.Text = com.Parameters["@parmnoofperson"].Value.ToString();
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

        private void HotelFrmTransferRoom_Load(object sender, EventArgs e)
        {
            txtdatetransfer.Text = DateTime.Now.ToShortDateString();
            display();
        }

        private void gridViewAvailable_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            numdays = 0.0;
            balance = 0.0;
            refund = 0.0;
            DateTime checkoutdate = Convert.ToDateTime(txtcheckoutdate.Text);
            DateTime transferdate = Convert.ToDateTime(txtdatetransfer.Text);
            numdays = (checkoutdate - transferdate).TotalDays;
            newroomnum.Text = gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomNumber").ToString();
            string roomtype = Database.getSingleQuery("Rooms", "RoomNumber='" + gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomNumber").ToString() + "'", "RoomType", Database.getCustomizeConnection());
            newroomtype.Text = roomtype;// gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomType").ToString();
            string roomprice = Database.getSingleQuery("RoomRates", "RoomType='" + roomtype + "'", "RoomPrice", Database.getCustomizeConnection());
            string roomrate = Database.getSingleQuery("RoomRates", "RoomType='" + roomtype + "'", "RoomRate", Database.getCustomizeConnection());
            newroomratepday.Text = roomprice;
            newratetype.Text = roomrate;

            rate = Convert.ToDouble(roomprice) * numdays;

            variance = rate - (Convert.ToDouble(txtinitialpayment.Text));
            if (variance < 0)
            {
                refund = variance;
            }
            else
            {
                balance = variance;
            }
            // balance = rate - (Convert.ToDouble(txtinitialpayment.Text));
            newnumdays.Text = numdays.ToString();
            newtotalamount.Text = rate.ToString();
            newbalance.Text = balance.ToString();
            newrefund.Text = refund.ToString();
        }

        private void txtdatetransfer_EditValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(newratetype.Text) || String.IsNullOrEmpty(newroomnum.Text) || String.IsNullOrEmpty(newroomratepday.Text) || String.IsNullOrEmpty(newroomtype.Text))
            {
                return;
            }
            else
            {
                numdays = 0.0;
                balance = 0.0;
                refund = 0.0;
                string roomprice = Database.getSingleQuery("RoomRates", "RoomType='" + newroomtype.Text + "'", "RoomPrice",Database.getCustomizeConnection());
                DateTime checkoutdate = Convert.ToDateTime(txtcheckoutdate.Text);
                DateTime transferdate = Convert.ToDateTime(txtdatetransfer.Text);
                numdays = (checkoutdate - transferdate).TotalDays;
                rate = Convert.ToDouble(roomprice) * numdays;
                variance = rate - (Convert.ToDouble(txtinitialpayment.Text));
                if (variance < 0)
                {
                    refund = variance;
                }
                else
                {
                    balance = variance;
                }
                newnumdays.Text = numdays.ToString();
                newtotalamount.Text = rate.ToString();
                newbalance.Text = balance.ToString();
                newrefund.Text = refund.ToString();
            }
        }
        void executeTransfer()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "spu_TransferRoom";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbookingid", txtbookingid.Text);
                com.Parameters.AddWithValue("@parmguestid", HotelFrmMainDashBoard.id);
                com.Parameters.AddWithValue("@parmcheckindate", txtdatetransfer.Text);
                com.Parameters.AddWithValue("@parmcheckoutdate", txtcheckoutdate.Text);
                com.Parameters.AddWithValue("@parmguestname", txtguestname.Text);
                com.Parameters.AddWithValue("@parmguestcontactno", txtguestcontactno.Text);
                com.Parameters.AddWithValue("@parmroomnumber", txtroomnum.Text);
                com.Parameters.AddWithValue("@parmpaymentstatus", txtpaymentstatus.Text);
                com.Parameters.AddWithValue("@parmroomtype", newroomtype.Text);
                com.Parameters.AddWithValue("@parmratetype", newratetype.Text);
                com.Parameters.AddWithValue("@parmroomrate", newroomratepday.Text);
                com.Parameters.AddWithValue("@parmmethodofpayment", txtmethodofpayment.Text);
                com.Parameters.AddWithValue("@parmnumdays", numdays);
                com.Parameters.AddWithValue("@parmtotalamount", rate);
                com.Parameters.AddWithValue("@parminitialpayment", txtinitialpayment.Text);
                com.Parameters.AddWithValue("@parmbalance", newbalance.Text);
                com.Parameters.AddWithValue("@parmrefund", newrefund.Text);
                com.Parameters.AddWithValue("@parmnoofpersons", txtnoofperson.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
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
        private void button1_Click(object sender, EventArgs e)
        {
            executeTransfer();
            XtraMessageBox.Show("Successfully Transfer");
        }
    }
}