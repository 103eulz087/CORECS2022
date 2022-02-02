using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSChargeToRoom : Form
    {
        public static bool transactiondone = false;
        public POSChargeToRoom()
        {
            InitializeComponent();
        }

        private void POSChargeToRoom_Load(object sender, EventArgs e)
        {
            txtInvoiceNumber.Text = IDGenerator.getIDNumber("BatchSalesSummary", "Invoice", 10000).ToString();
            txtTotalOnScreen.Text = PointOfSale.totalamountstr;
            lblorderno.Text = PointOfSale.refno;
            lbltransactionno.Text = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");// IDGenerator.getReferenceNumber();
            txtClientName.Text = PointOfSale.custname;
            Database.displaySearchlookupEdit("SELECT RoomNumber,RoomType,GuestName FROM CheckinGuest WHERE isDone=0", txtservices, "RoomNumber", "RoomNumber", Database.getCustomizeConnection());

        }

        private void cmdConfirmPayment_Click(object sender, EventArgs e)
        {
            try
            {
                string creditlimit = Database.getSingleQuery("Customers", "CustomerID='" + PointOfSale.custcode + "'", "CustomerCreditLimit");
                string getacctbalance = Database.getSingleQuery("ClientAccounts", "AccountID='" + PointOfSale.custcode + "'", "AccountBalance");
                double overalltotal = Convert.ToDouble(getacctbalance) + Convert.ToDouble(txtTotalOnScreen.Text);
                if (overalltotal > Convert.ToDouble(creditlimit))
                {
                    Classes.Utilities.displayMessage("You are now exceed in your Credit Limit", MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    execute();
                    transactiondone = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void execute()
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_ChargeTransactionHotel";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtransno", lbltransactionno.Text);
                com.Parameters.AddWithValue("@parmorderno", lblorderno.Text);
                com.Parameters.AddWithValue("@parmreferenceno", "");
                com.Parameters.AddWithValue("@parmamountpayable", txtTotalOnScreen.Text);
                com.Parameters.AddWithValue("@parmamounttender", "");
                com.Parameters.AddWithValue("@parmamountchange", "");
                com.Parameters.AddWithValue("@parminvoice", txtInvoiceNumber.Text);
                com.Parameters.AddWithValue("@parmpaymenttype", "Charge");
                com.Parameters.AddWithValue("@parmcustid", txtguestid.Text);
                com.Parameters.AddWithValue("@parmremarks", txtRemarks.Text);
                com.Parameters.AddWithValue("@transby", Login.isglobalUserID);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
