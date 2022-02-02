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
    public partial class HotelCheckoutComputation : DevExpress.XtraEditors.XtraForm
    {
        string option = "";
        public HotelCheckoutComputation()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HotelCheckoutComputation_Load(object sender, EventArgs e)
        {
            display();
            SalesInventorySystem.Classes.Utilities.setMinimumDateDevEx(txtcheckindate.Text, txtcheckoutdate);
            //DateTime oDate = Convert.ToDateTime(txtcheckindate.Text);
            //txtcheckoutdate.Properties.MinValue = oDate;
            
        }

        void display()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "sp_CheckoutCompute";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbookingid", txtbookingno.Text);
                com.Parameters.AddWithValue("@parmguestid", HotelFrmMainDashBoard.id);
                com.Parameters.AddWithValue("@parmckout", txtcheckoutdate.Text);
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
                com.Parameters.Add("@parmaddcharge", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmdiscount", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmgrandtotalamount", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.AddWithValue("@parmoption", option);
                com.Parameters.Add("@parmtotalorder", SqlDbType.Money).Direction = ParameterDirection.Output;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
             
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
                txttaxcharge.Text = com.Parameters["@parmtaxcharge"].Value.ToString();
                txtnumdays.Text = com.Parameters["@parmnumdays"].Value.ToString();
                txttotalamount.Text = com.Parameters["@parmtotalamount"].Value.ToString();
                txtinitialpayment.Text = com.Parameters["@parminitialpayment"].Value.ToString();
                txtbalance.Text = com.Parameters["@parmbalance"].Value.ToString();
                txtrefund.Text = com.Parameters["@parmrefund"].Value.ToString();
                txtaddcharge.Text = com.Parameters["@parmaddcharge"].Value.ToString();
                txtdiscount.Text = com.Parameters["@parmdiscount"].Value.ToString();
                txttotalorder.Text = com.Parameters["@parmtotalorder"].Value.ToString();

                double grandtotal = 0.0,vatablesale=0.0,vat=0.0;
                grandtotal = Convert.ToDouble(txtbalance.Text) + Convert.ToDouble(txtaddcharge.Text) + Convert.ToDouble(txttaxcharge.Text) + Convert.ToDouble(txttotalorder.Text);
                vatablesale = Math.Round(grandtotal / 1.12,2);
                vat = Math.Round(vatablesale * 0.12,2);
                lblgrandtotal.Text =String.Format("{0:0.00}", grandtotal); //grandtotal.ToString();
                lblvatablesale.Text = vatablesale.ToString();
                lblvatsale.Text = vat.ToString();
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
            option = "Recalculate";
            btnsavechanges.Enabled = true;
            display();
        }


        void updateChanges()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "sp_SaveChanges";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmguestid", HotelFrmMainDashBoard.id);
                com.Parameters.AddWithValue("@parmroomnumber", txtroomnum.Text);
                com.Parameters.AddWithValue("@parmcheckoutdate", txtcheckoutdate.Text);
                com.Parameters.AddWithValue("@parmnumdays", txtnumdays.Text);
                com.Parameters.AddWithValue("@parmtotalamount", txttotalamount.Text);
                com.Parameters.AddWithValue("@parmbalance", txtbalance.Text);
                com.Parameters.AddWithValue("@parmrefund", txtrefund.Text);
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
        private void btnsavechanges_Click(object sender, EventArgs e)
        {
            
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to this action?", "Recalculate");
            if (ok)
            {
                updateChanges();
                XtraMessageBox.Show("Successfully Updated!");
            }
            else
            {
                return;
            }
            display();
            btnsavechanges.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkOut();
            XtraMessageBox.Show("Successfully Checkout!");
            this.Dispose();
        }
        void checkOut()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "sp_checkout";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbookingid", txtbookingno.Text);
                com.Parameters.AddWithValue("@guestid", HotelFrmMainDashBoard.id);
                com.Parameters.AddWithValue("@roomnum", txtroomnum.Text);
                com.Parameters.AddWithValue("@roombalance", txtbalance.Text);
                com.Parameters.AddWithValue("@roomtype", txtroomtype.Text);
                com.Parameters.AddWithValue("@roomrate", txtroomrate.Text);
                com.Parameters.AddWithValue("@chargebalance", txtaddcharge.Text);
                com.Parameters.AddWithValue("@taxbalance", txttaxcharge.Text);
                com.Parameters.AddWithValue("@grandtotal", lblgrandtotal.Text);
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
    }
}