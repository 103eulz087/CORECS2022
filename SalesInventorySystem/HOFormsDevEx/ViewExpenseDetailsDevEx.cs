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
    public partial class ViewExpenseDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ViewExpenseDetailsDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Approved this Expense?", "Approve Expense");
            if (confirm)
            {
                updateExpense();
                isdone = true;
                this.Close();
            }
            else
            { return; }
            
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

        void updateExpense()
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_UpdateExpenseApproved";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsuppid.Text);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
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

        private void ViewExpenseDetailsDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}