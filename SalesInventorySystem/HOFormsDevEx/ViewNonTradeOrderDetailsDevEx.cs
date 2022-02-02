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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewNonTradeOrderDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ViewNonTradeOrderDetailsDevEx()
        {
            InitializeComponent();
        }

        private void ViewNonTradeOrderDetailsDevEx_Load(object sender, EventArgs e)
        {
            if(Convert.ToBoolean(Login.isglobalApprover) == true)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
            //////////////////////////////////////////////////////////////////////////
            if(ViewNonTradeOrdersDevEx.tabtype == "Approved")
            {
                groupBox1.Visible = false;
            }
            populateGlCodes();
        }

        void populateGlCodes()
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts ", searchLookUpEdit1, "AccountCode", "Description");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts ", searchLookUpEdit2, "AccountCode", "Description");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(searchLookUpEdit1.Text) || String.IsNullOrEmpty(searchLookUpEdit2.Text))
            {
                XtraMessageBox.Show("Please Input GL Account Codes");
                return;
            }
            else
            {
                save();
                XtraMessageBox.Show("Successfully Approved");
                isdone = true;
                this.Dispose();
            }
           
        }

        void save()
        {
            string id = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spu_postNonTradeOrder";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno",HOFormsDevEx.ViewNonTradeOrdersDevEx.shipmentno);
                com.Parameters.AddWithValue("@parmrefno", id);
                com.Parameters.AddWithValue("@parmuser",Login.Fullname);
                com.Parameters.AddWithValue("@parmbranch",Login.assignedBranch);
                com.Parameters.AddWithValue("@parmdebitacctcode",searchLookUpEdit1.Text);
                com.Parameters.AddWithValue("@parmcreditacctcode", searchLookUpEdit2.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE ShipmentOrder Set Status='CANCELLED',ApprovedDate='" + DateTime.Now.ToShortDateString() + "',ApprovedBy='" + Login.Fullname + "' WHERE ShipmentNo='" + ViewNonTradeOrdersDevEx.shipmentno + "'", "DisApproved!");
            isdone = true;
            this.Close();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string debitdesc = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + searchLookUpEdit1.Text + "'", "Description");
            labeldebit.Text = debitdesc;
           
        }

        private void searchLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            string creditdesc = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + searchLookUpEdit2.Text + "'", "Description");
            labelcredit.Text = creditdesc;
        }
    }
}