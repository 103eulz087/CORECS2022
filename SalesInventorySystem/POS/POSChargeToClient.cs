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
    public partial class POSChargeToClient : Form
    {
        public static string paymentstat = "";
        public static bool transactiondone = false;
        object custkey = null;
        public static string customercode = "",invoiceNum="";
        public POSChargeToClient()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                btnClose.PerformClick();
            }
            
            return functionReturnValue;
        }
        private void cmdConfirmPayment_Click(object sender, EventArgs e)
        {
             
            
        }

        private void POSChargeToClient_Load(object sender, EventArgs e)
        {
            //lbltranscode.Text = PointOfSale._transcode;
            Database.displaySearchlookupEdit("SELECT CustomerKey,CustomerName FROM Customers", txtcust, "CustomerName", "CustomerName");
        }


        private void execute()
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_ChargeTransaction";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbrcode", Login.assignedBranch); 
                com.Parameters.AddWithValue("@parmorderno", txtorderno.Text); 
                com.Parameters.AddWithValue("@parminvoice", txtinvoiceno.Text); 
                com.Parameters.AddWithValue("@parmcustid", custkey.ToString());
                com.Parameters.AddWithValue("@parmtotalamount", txtamount.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
                com.Parameters.AddWithValue("@transby", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmmachineused", Environment.MachineName);
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

        private void txtcust_EditValueChanged(object sender, EventArgs e)
        {
            custkey=SearchLookUpClass.getSingleValue(txtcust, "CustomerKey");
            customercode = custkey.ToString();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcust.Text) || String.IsNullOrEmpty(txtamount.Text) || String.IsNullOrEmpty(txtinvoiceno.Text))
            {
                XtraMessageBox.Show("Customer Name OR Amount must not Empty");
                return;
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are all Details Correct?", "Confirm Transaction");
                if(confirm)
                {
                    invoiceNum = txtinvoiceno.Text;
                    execute();
                    transactiondone = true;
                    this.Close();
                }
                else
                {
                    return;
                }
               
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            transactiondone = false;
            this.Close();
        }
    }
}
