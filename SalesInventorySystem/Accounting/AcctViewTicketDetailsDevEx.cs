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

namespace SalesInventorySystem.Accounting
{
    public partial class AcctViewTicketDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public AcctViewTicketDetailsDevEx()
        {
            InitializeComponent();
        }

        private void btnapproved_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "', ApproverStatus='Approved' WHERE TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' AND TicketDate='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketDate").ToString() + "' AND BranchCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString() + "' ", "Successfully Approved!");

            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "usp_UpdateApprovedTickets";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@Branch", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString());
            com.Parameters.AddWithValue("@TicketDate", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketDate").ToString());
            //com.Parameters.AddWithValue("@Supplementary", dataGridView1.Rows[cord].Cells[2].Value.ToString());
            com.Parameters.AddWithValue("@TicketNumber", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            XtraMessageBox.Show("Ticket Succesfully Updated!");
            con.Close();
            isdone = true;
            this.Close();
        }

        private void btndisapproved_Click(object sender, EventArgs e)
        {

            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "',CheckerStatus='DisApproved' WHERE TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' AND TicketDate='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketDate").ToString() + "' AND BranchCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString() + "' ", "Successfully DisApproved!");
            isdone = true;
            this.Close();
        }
    }
}