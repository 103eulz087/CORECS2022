using DevExpress.XtraEditors;
using SalesInventorySystem.Orders;
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

namespace SalesInventorySystem.HOForms
{
    public partial class ReturnCustomerOrder : Form
    {
        public static bool isdone = false;
        bool isvatable = false;
        public ReturnCustomerOrder()
        {
            InitializeComponent();
        }

        private void ReturnCustomerOrder_Load(object sender, EventArgs e)
        {
            //txtpono.Text = Orders.viewBranchOrderDetails.pono;
            //txtdevno.Text = Orders.viewBranchOrderDetails.devno;
            //txtrefno.Text = Orders.viewBranchOrderDetails.refno;
            //txtprodno.Text = Orders.viewBranchOrderDetails.prodno;
            //txtdesc.Text = Orders.viewBranchOrderDetails.prodname;
            //txtqty.Text = Orders.viewBranchOrderDetails.qty;
            //txtcost.Text = Orders.viewBranchOrderDetails.cost;
            ////txtnewqty.Text = Orders.viewBranchOrderDetails.qty;
            //txtreturnedqty.Text= Orders.viewBranchOrderDetails.qty;
            if (Convert.ToBoolean(Orders.viewBranchOrderDetails.isvat) == true)
            {
                checkBox1.Checked = true;
                isvatable = true;
            } else
            {
                checkBox1.Checked = false;
                isvatable = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool checkCM = Database.checkifExist("Select isCreditMemo FROM DeliveryDetails WHERE isCreditMemo=1 AND PONumber='" + txtpono.Text + "' and ProductNo='" + txtprodno.Text + "'");
            bool isInvoiceUpdated = false;
            isInvoiceUpdated = Database.checkifExist("Select isInvoiceUpdate FROM DeliverySummary WHERE PONumber='" + txtpono.Text + "' and isInvoiceUpdate=1");
            if (isInvoiceUpdated == false)
            {
                XtraMessageBox.Show("Invoice Number must be updated first..please go to Print Delivery Receipt Option!...");
                return;
            }
            if (checkCM==true)
            {
                XtraMessageBox.Show("This item is already executed as CreditMemo!");
                return;
            }
            if (txtreturnedqty.Text=="" || txtreturnedqty.Text == null)
            {
                XtraMessageBox.Show("Returned Qty must not Empty");
                return;
            }
            if (Convert.ToDouble(txtreturnedqty.Text) > Convert.ToDouble(txtqty.Text))
            {
                XtraMessageBox.Show("Returned Qty must not greater than actualqty");
                return;
            }
            else
            {
                returnInventory();
                XtraMessageBox.Show("Successfully Returned");
                isdone = true;
                this.Close();
            }

        }

        void returnInventory()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string str = "", qty = "";
                if (radreturnpartial.Checked == true)
                {
                    str = "PARTIAL";
                    qty = txtnewqty.Text;
                }
                else
                {
                    str = "FULL";
                    qty = txtqty.Text;
                }
                string query = "sp_ReturnDeliveredOrder"; //sp_ReturnDeliveredOrder //sp_ReturnDelivery
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmpono", txtpono.Text);
                com.Parameters.AddWithValue("@parmprodno", txtprodno.Text);
                com.Parameters.AddWithValue("@parmbranchcode", viewBranchOrderDetails.brcode); //before 888
                com.Parameters.AddWithValue("@parmreturnedqty", txtreturnedqty.Text);
                com.Parameters.AddWithValue("@parmcost", txtcost.Text);
                com.Parameters.AddWithValue("@parmisvat", isvatable);
                com.Parameters.AddWithValue("@parmtype", str); //@parmtype == full
                com.Parameters.AddWithValue("@parmreturntype", "SELECTED"); //@parmtype == full @parmuser
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.Parameters.AddWithValue("@parmseqno", txtseqno.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtbarcode.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
               

            }
            catch (SqlException ex)
            {
                //throw new Exception(ex.StackTrace.ToString());
                XtraMessageBox.Show(ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radreturnfull_CheckedChanged(object sender, EventArgs e)
        {
            if (radreturnfull.Checked == true)
            {
                txtreturnedqty.Text = txtqty.Text;
                txtreturnedqty.ReadOnly = true;
           } else
            {
                txtreturnedqty.Text = "";
                txtreturnedqty.ReadOnly = false;
            }

        }

        private void radreturnpartial_CheckedChanged(object sender, EventArgs e)
        {
            if (radreturnpartial.Checked == true)
            {
                txtreturnedqty.Text = "";
                txtreturnedqty.ReadOnly = false;
            }
            else
            {
                txtreturnedqty.Text = txtqty.Text;
                txtreturnedqty.ReadOnly = true;
            }
        }
    }
}
