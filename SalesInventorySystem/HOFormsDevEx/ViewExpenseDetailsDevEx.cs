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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewExpenseDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ViewExpenseDetailsDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!HelperFunction.ConfirmDialog(
                        "Are you sure you want to approve this expense?",
                        "Approve Expense"))
            {
                return;
            }

            try
            {
                ApproveExpense();
                isdone = true;
                this.Close();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message, "Approval Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Approved this Expense?", "Approve Expense");
            if (confirm)
            {
                Database.ExecuteQuery($"UPDATE dbo.ExpenseSummary SET Status='CANCELLED',UpdatedBy='{Login.userid}',DateTimeUpdated='{DateTime.Now.ToString()}' " +
               $"WHERE ReferenceNumber='{txtrefno.Text}' " +
               $"AND InvoiceNo='{txtinvoiceno.Text}' ", "Successfully Updated");
                isdone = true;
                this.Close();
            }
            else
            { return; }
        }

        private void ApproveExpense()
        {
            if (string.IsNullOrWhiteSpace(txtrefno.Text) ||
                string.IsNullOrWhiteSpace(txtsuppid.Text) ||
                string.IsNullOrWhiteSpace(txtinvoiceno.Text))
            {
                throw new ApplicationException("Missing required expense information.");
            }

            using (SqlConnection con = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_ApproveExpense", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.Add("@parmrefno", SqlDbType.VarChar, 10)
                    .Value = txtrefno.Text.Trim();

                cmd.Parameters.Add("@parmsupplierid", SqlDbType.VarChar, 100)
                    .Value = txtsuppid.Text.Trim();

                cmd.Parameters.Add("@parminvoiceno", SqlDbType.VarChar, 150)
                    .Value = txtinvoiceno.Text.Trim();

                cmd.Parameters.Add("@parmuser", SqlDbType.VarChar, 50)
                    .Value = Login.Fullname;

                con.Open();
                cmd.ExecuteNonQuery();
                BigAlert.Show("SUCCESS", "EXPENSE Entry Successfully Posted!..",MessageBoxIcon.Information);
            }
        }


        //void updateExpense()
        //{
        //    try
        //    {
        //        SqlConnection con = Database.getConnection();
        //        con.Open();
        //        string query = "sp_UpdateExpenseApproved";
        //        SqlCommand com = new SqlCommand(query, con);
        //        com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
        //        com.Parameters.AddWithValue("@parmsupplierid", txtsuppid.Text);
        //        com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
        //        com.Parameters.AddWithValue("@parmuser", Login.Fullname);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = query;
        //        com.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}

        private void ViewExpenseDetailsDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}