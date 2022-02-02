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

namespace SalesInventorySystem.Accounting
{
    public partial class AcctViewTicketDetails : Form
    {
        public AcctViewTicketDetails()
        {
            InitializeComponent();
        }

        private void AcctViewTicketDetails_Load(object sender, EventArgs e)
        {
            //display();
        }
        void display()
        {
            Database.displayLocalGrid("SELECT * FROM TempTicketDetails WHERE TicketNumber='" + Accounting.ViewTicket.ticketrefno + "'", dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            //if (Convert.ToBoolean(Login.isChecker)==true)
            //{
            //    Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "', CheckerStatus='Approved' WHERE TicketNumber='" + dataGridView1.Rows[cord].Cells[3].Value.ToString() + "' ", "Successfully Checked!");
            //}
            //if (Convert.ToBoolean(Login.isglobalApprover) == true)
            //{
            //    Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "', ApproverStatus='Approved' WHERE TicketNumber='" + dataGridView1.Rows[cord].Cells[3].Value.ToString() + "' ", "Successfully Approved!");
            //}
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "', ApproverStatus='Approved' WHERE TicketNumber='" + dataGridView1.Rows[cord].Cells[3].Value.ToString() + "' AND TicketDate='" + dataGridView1.Rows[cord].Cells["TicketDate"].Value.ToString() + "' AND BranchCode='" + dataGridView1.Rows[cord].Cells["BranchCode"].Value.ToString() + "' ", "Successfully Approved!");

            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "usp_UpdateApprovedTickets";
            SqlCommand com = new SqlCommand(query, con); 
            com.Parameters.AddWithValue("@Branch", dataGridView1.Rows[cord].Cells[0].Value.ToString());
            com.Parameters.AddWithValue("@TicketDate", dataGridView1.Rows[cord].Cells[1].Value.ToString());
            //com.Parameters.AddWithValue("@Supplementary", dataGridView1.Rows[cord].Cells[2].Value.ToString());
            com.Parameters.AddWithValue("@TicketNumber", dataGridView1.Rows[cord].Cells[3].Value.ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            XtraMessageBox.Show("Ticket Succesfully Updated!");
            con.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "',CheckerStatus='DisApproved' WHERE TicketNumber='" + dataGridView1.Rows[cord].Cells[3].Value.ToString() + "' AND TicketDate='" + dataGridView1.Rows[cord].Cells["TicketDate"].Value.ToString() + "' AND BranchCode='" + dataGridView1.Rows[cord].Cells["BranchCode"].Value.ToString() + "' ", "Successfully DisApproved!");
            this.Dispose();
            this.Close();
        }
    }
}
