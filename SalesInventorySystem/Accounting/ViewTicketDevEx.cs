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
    public partial class ViewTicketDevEx : DevExpress.XtraEditors.XtraForm
    {
        public ViewTicketDevEx()
        {
            InitializeComponent();
        }

        private void ViewTicketDevEx_Load(object sender, EventArgs e)
        {
            //tabControl1.TabPages[0].Hide();
            //tabControl1.TabPages[2].Hide();
            tabControl1.TabPages.Remove(forchecking);
            tabControl1.TabPages.Remove(forupdating);
            filtertab();
        }
        String getUserID(string name)
        {
            string str = "";
            str = Database.getSingleData("Users", "Checker", Login.Fullname, "UserID");
            return str;
        }
        void filtertab()
        {
            string checker = Database.getSingleQuery("TempTicketMaster", "Checker='" + Login.Fullname + "'", "Checker");
           
            string maker = Database.getSingleQuery("TempTicketMaster", "Maker='" + Login.Fullname + "'", "Maker");
            if (tabControl1.SelectedTab.Equals(forchecking))
            {
            }
            else if (tabControl1.SelectedTab.Equals(forapproval))
            {
                Database.display("SELECT TicketDate,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,ParticularsInText FROM TempTicketMaster WHERE ApproverStatus='FOR APPROVAL' ", gridControlforapproval,gridViewforapproval);
            }
            else if (tabControl1.SelectedTab.Equals(forupdating))
            {
            }
            else if (tabControl1.SelectedTab.Equals(approvedtickets))
            {
               Database.display("SELECT TicketDate,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,ParticularsInText,Status FROM TempTicketMaster WHERE ApproverStatus='Approved' and Status <> 'DisApproved' and TicketDate Between '" + datefromrej.Text+"' and '"+datetorej.Text+"'", gridControlapprovedtickets, gridViewapprovedtickets);

            }
            else if (tabControl1.SelectedTab.Equals(disapprovedtickets))
            {
                Database.display("SELECT TicketDate,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,ParticularsInText FROM TempTicketMaster WHERE ApproverStatus='DisApproved' and TicketDate Between '" + datefromdisapproved.Text + "' and '" + datetodisapproved.Text + "'", gridControldisapprovedtickets, gridViewdisapprovedtickets);

            }
        }

        private void gridcontrolforchecking_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripForChecking.Show(gridcontrolforchecking, e.Location);
            }
        }

        private void approvedTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "',CheckerStatus='Approved' WHERE TicketNumber='" + gridViewforchecking.GetRowCellValue(gridViewforchecking.FocusedRowHandle, "TicketNumber").ToString() + "' ", "Successfully Checked!");
            filtertab();
        }

        private void disApprovedTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "',CheckerStatus='DisApproved',Status='DisApproved' WHERE TicketNumber='" + gridViewforchecking.GetRowCellValue(gridViewforchecking.FocusedRowHandle, "TicketNumber").ToString() + "' ", "Successfully DisApproved!");
            filtertab();
        }

        private void viewTicketDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.AcctViewTicketDetails viewtickdet = new AcctViewTicketDetails();
            viewtickdet.FormClosed += Viewtickdet_FormClosed;
            Database.displayLocalGrid("SELECT * FROM TempTicketDetails WHERE TicketNumber='" + gridViewforchecking.GetRowCellValue(gridViewforchecking.FocusedRowHandle,"TicketNumber").ToString() + "'", viewtickdet.dataGridView1);
            viewtickdet.ShowDialog(this);
           
        }

        private void Viewtickdet_FormClosed(object sender, FormClosedEventArgs e)
        {
            filtertab();
        }

        private void gridControlforapproval_MouseUp(object sender, MouseEventArgs e)
        {
            string approver = Database.getSingleQuery("Approvers", "UserID <> ''", "UserID");
            if (Login.isglobalUserID == approver)
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStripForApproval.Show(gridControlforapproval, e.Location);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "',ApproverStatus='Approved' WHERE TicketNumber='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketNumber").ToString() + "' AND TicketDate='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketDate").ToString() + "' AND BranchCode='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "BranchCode").ToString() + "' ", "Successfully Updated!");
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "usp_UpdateApprovedTickets";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@Branch", gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "BranchCode").ToString());
            com.Parameters.AddWithValue("@TicketDate", gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketDate").ToString());
            com.Parameters.AddWithValue("@TicketNumber", gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketNumber").ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
            XtraMessageBox.Show("Ticket Succesfully Updated!");
            filtertab();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "',ApproverStatus='DisApproved',Status='DisApproved' WHERE TicketNumber='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketNumber").ToString() + "'  AND TicketDate='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketDate").ToString() + "' AND BranchCode='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "BranchCode").ToString() + "' ", "Successfully Updated!");
            filtertab();
        }

        private void viewTicketDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Accounting.AcctViewTicketDetails viewtickdet = new AcctViewTicketDetails();
            //viewtickdet.FormClosed += Viewtickdet_FormClosed1;
            //Database.displayLocalGrid("SELECT * FROM TempTicketDetails WHERE TicketNumber='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketNumber").ToString() + "' AND TicketDate='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketDate").ToString() + "' AND BranchCode='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "BranchCode").ToString() + "'", viewtickdet.dataGridView1);
            //viewtickdet.ShowDialog(this);

            Accounting.AcctViewTicketDetailsDevEx acctview = new AcctViewTicketDetailsDevEx();
            Database.display("SELECT * FROM TempTicketDetails WHERE TicketNumber='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketNumber").ToString() + "' AND TicketDate='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "TicketDate").ToString() + "' AND BranchCode='" + gridViewforapproval.GetRowCellValue(gridViewforapproval.FocusedRowHandle, "BranchCode").ToString() + "'",acctview.gridControl1, acctview.gridView1);
            acctview.ShowDialog(this);
            if (AcctViewTicketDetailsDevEx.isdone == true)
            {
                AcctViewTicketDetailsDevEx.isdone = false;
                acctview.Dispose();
                filtertab();
            }
        }

        private void Viewtickdet_FormClosed1(object sender, FormClosedEventArgs e)
        {
            filtertab();
        }

        private void gridControlforupdating_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripForUpdating.Show(gridControlforupdating, e.Location);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "usp_UpdateApprovedTickets";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@Branch", gridViewforupdating.GetRowCellValue(gridViewforupdating.FocusedRowHandle, "BranchCode").ToString());
            com.Parameters.AddWithValue("@TicketDate", gridViewforupdating.GetRowCellValue(gridViewforupdating.FocusedRowHandle, "TicketDate").ToString());
            com.Parameters.AddWithValue("@TicketNumber", gridViewforupdating.GetRowCellValue(gridViewforupdating.FocusedRowHandle, "TicketNumber").ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            XtraMessageBox.Show("Ticket Succesfully Updated!");
            filtertab();
            con.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            filtertab();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtertab();
        }

        private void gridControlapprovedtickets_MouseUp(object sender, MouseEventArgs e)
        {
            if(gridViewapprovedtickets.GetRowCellValue(gridViewapprovedtickets.FocusedRowHandle,"Status").ToString() == "PENDING")
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(gridControlapprovedtickets, e.Location);
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "usp_UpdateApprovedTickets";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@Branch", gridViewapprovedtickets.GetRowCellValue(gridViewapprovedtickets.FocusedRowHandle, "BranchCode").ToString());
            com.Parameters.AddWithValue("@TicketDate", gridViewapprovedtickets.GetRowCellValue(gridViewapprovedtickets.FocusedRowHandle, "TicketDate").ToString());
            com.Parameters.AddWithValue("@TicketNumber", gridViewapprovedtickets.GetRowCellValue(gridViewapprovedtickets.FocusedRowHandle, "TicketNumber").ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
            XtraMessageBox.Show("Ticket Succesfully Updated!");
            filtertab();
        }
    }
}