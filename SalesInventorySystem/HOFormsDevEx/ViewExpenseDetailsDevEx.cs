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
                XtraMessageBox.Show("Reference No., Supplier, and Invoice No. are required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = Database.getConnection())
                using (var cmd = new SqlCommand("dbo.sp_ApproveExpense", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;

                    // NOTE: sp_ApproveExpense receives @parmsupplierid (the SupplierID,
                    // not SupplierKey). The SP resolves SupplierKey internally.
                    cmd.Parameters.Add("@parmrefno", SqlDbType.VarChar, 10).Value = txtrefno.Text.Trim();
                    cmd.Parameters.Add("@parmsupplierid", SqlDbType.VarChar, 100).Value = txtsuppid.Text.Trim();
                    cmd.Parameters.Add("@parminvoiceno", SqlDbType.VarChar, 150).Value = txtinvoiceno.Text.Trim();
                    cmd.Parameters.Add("@parmuser", SqlDbType.VarChar, 50).Value = Login.Fullname;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                BigAlert.Show("SUCCESS", "Expense approved and tickets generated.", MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(
                    $"Approval failed ({ex.Number}): {ex.Message}",
                    "Approve Expense Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ViewExpenseDetailsDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}