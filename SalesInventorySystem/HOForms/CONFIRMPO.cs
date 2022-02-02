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

namespace SalesInventorySystem.HOForms
{
    public partial class CONFIRMPO : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone=false;
        public CONFIRMPO()
        {
            InitializeComponent();
        }

        void ExecuteSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SP_CONFIRMPO";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplier.Text);
                com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                com.Parameters.AddWithValue("@parmordertype", txtordertype.Text);
                com.Parameters.AddWithValue("@parminvoicedate", txtinvoicedate.Text);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parmamountpayable", txtamountpayable.Text);
                com.Parameters.AddWithValue("@parmorderedby", Login.isglobalUserID);
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

        void saveAndConfirm()
        {
            ExecuteSP();
            XtraMessageBox.Show("Successfully Updated!..");
            isdone = true;
            this.Close();
        }

       
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Confirm this Transaction", "Confirm Transaction");
            if (confirm)
            {
                saveAndConfirm();
            }
            else
            {
                return;
            }
        }

        private void CONFIRMPO_Load(object sender, EventArgs e)
        {

        }
    }
}