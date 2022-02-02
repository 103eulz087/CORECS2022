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
    public partial class ViewTicket : Form
    {
        public static string ticketrefno = String.Empty;
        public static string ticketnum = String.Empty;
        public ViewTicket()
        {
            InitializeComponent();
        }

        private void ViewTicket_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(forupdating);
            tabControl1.TabPages.Remove(forchecking);
            tabControl1.TabPages.Remove(forapproval);
            tabControl1.TabPages.Remove(awaitingforapproval);
           
            if (Convert.ToBoolean(Login.isMaker) == true)
            {
                tabControl1.TabPages.Insert(2,forupdating);
                tabControl1.TabPages.Insert(3, awaitingforapproval);
            }
            if (Convert.ToBoolean(Login.isChecker) == true)
            {
                tabControl1.TabPages.Insert(0,forchecking);
            }
            if (Convert.ToBoolean(Login.isglobalApprover) == true)
            {
                tabControl1.TabPages.Insert(1, forapproval);
            }
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
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster Where Checker='" + Login.isglobalUserID + "' AND CheckerStatus='FOR APPROVAL' ", dataGridView1);
            }
            else if (tabControl1.SelectedTab.Equals(forapproval))
            {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE CheckerStatus='Approved' AND ApproverStatus='FOR APPROVAL' AND Approver='"+Login.isglobalUserID + "'", dataGridView2);
            }
            else if (tabControl1.SelectedTab.Equals(forupdating))
            {
                Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE ApproverStatus='Approved' AND Status='Pending' and Maker='" + Login.isglobalUserID + "' ", dataGridView3);
            }
            else if (tabControl1.SelectedTab.Equals(approvedtickets))
            {
                if (Convert.ToBoolean(Login.isMaker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE Status='Approved'", dataGridView4);
                }
                else if (Convert.ToBoolean(Login.isChecker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE CheckerStatus='Approved'", dataGridView4);
                }
                else if (Convert.ToBoolean(Login.isChecker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE ApproverStatus='Approved'", dataGridView4);
                }
            }
            else if (tabControl1.SelectedTab.Equals(disapprovedtickets))
            {
                if (Convert.ToBoolean(Login.isMaker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE Status='DisApproved'", dataGridView5);
                }
                else if (Convert.ToBoolean(Login.isChecker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE CheckerStatus='DisApproved' AND Checker='"+checker+"'", dataGridView5);
                }
                else if (Convert.ToBoolean(Login.isChecker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE ApproverStatus='DisApproved'", dataGridView5);
                }
            }
            else if (tabControl1.SelectedTab.Equals(awaitingforapproval))
            {
                if (Convert.ToBoolean(Login.isMaker) == true)
                {
                    Database.displayLocalGrid("SELECT TicketDate,SupplementaryNumber,BranchCode,TicketNumber,Origin,Maker,DateOutMaker,Checker,DateOutChecker,CheckerStatus,Approver,ApproverStatus,DateOutApprover,ParticularsInText FROM TempTicketMaster WHERE Status='Pending' ",dataGridView6);
                }
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }
        
        private void approvedTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord=dataGridView1.CurrentCellAddress.Y;
            ticketnum = dataGridView1.Rows[cord].Cells[3].Value.ToString();
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "',CheckerStatus='Approved' WHERE TicketNumber='" + dataGridView1.Rows[cord].Cells[3].Value + "' ", "Successfully Checked!");
            filtertab();
        }

        private void disApprovedTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            ticketnum = dataGridView1.Rows[cord].Cells[3].Value.ToString();
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutChecker='" + DateTime.Now.ToString() + "',CheckerStatus='DisApproved',Status='DisApproved' WHERE TicketNumber='" + dataGridView1.Rows[cord].Cells[3].Value + "' ", "Successfully DisApproved!");
            filtertab();
        }

        private void dataGridView2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(dataGridView2, e.Location);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int cord = dataGridView2.CurrentCellAddress.Y;
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "',ApproverStatus='Approved' WHERE TicketNumber='" + dataGridView2.Rows[cord].Cells[3].Value + "' ", "Successfully Updated!");
            filtertab();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int cord = dataGridView2.CurrentCellAddress.Y;
            Database.ExecuteQuery("UPDATE TempTicketMaster SET DateOutApprover='" + DateTime.Now.ToString() + "',ApproverStatus='DisApproved',Status='DisApproved' WHERE TicketNumber='" + dataGridView2.Rows[cord].Cells[3].Value + "' ", "Successfully Updated!");
            filtertab();
        }

        private void dataGridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip3.Show(dataGridView3, e.Location);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int cord = dataGridView3.CurrentCellAddress.Y;
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "usp_UpdateApprovedTickets";
            SqlCommand com = new SqlCommand(query,con);
            com.Parameters.AddWithValue("@Branch",dataGridView3.Rows[cord].Cells[2].Value.ToString());
            com.Parameters.AddWithValue("@TicketDate",dataGridView3.Rows[cord].Cells[0].Value.ToString());
            com.Parameters.AddWithValue("@Supplementary",dataGridView3.Rows[cord].Cells[1].Value.ToString());
            com.Parameters.AddWithValue("@TicketNumber", dataGridView3.Rows[cord].Cells[3].Value.ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            XtraMessageBox.Show("Ticket Succesfully Updated!");
            filtertab();
            con.Close();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            int cord = dataGridView3.CurrentCellAddress.Y;
            Database.ExecuteQuery("UPDATE TempTicketMaster SET Status='Pending' WHERE TicketNumber='" + dataGridView3.Rows[cord].Cells[3].Value + "' ", "Successfully Updated!");
       
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void viewTicketDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord=dataGridView1.CurrentCellAddress.Y;
            ticketrefno=dataGridView1.Rows[cord].Cells[3].Value.ToString();
            Accounting.AcctViewTicketDetails viewtickdet = new AcctViewTicketDetails();
            viewtickdet.FormClosed += new FormClosedEventHandler(viewtickdet_FormClosed);
            viewtickdet.ShowDialog(this);
        }

        void viewtickdet_FormClosed(object sender, FormClosedEventArgs e)
        {
            filtertab();
        }

        private void viewTicketDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int cord = dataGridView2.CurrentCellAddress.Y;
            ticketrefno = dataGridView2.Rows[cord].Cells[3].Value.ToString();
            Accounting.AcctViewTicketDetails viewtickdet = new AcctViewTicketDetails();
            viewtickdet.FormClosed += new FormClosedEventHandler(viewtickdet_FormClosed);
            viewtickdet.ShowDialog(this);
        }

        private void viewTicketDetailsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int cord = dataGridView3.CurrentCellAddress.Y;
            ticketrefno = dataGridView3.Rows[cord].Cells[3].Value.ToString();
            Accounting.AcctViewTicketDetails viewtickdet = new AcctViewTicketDetails();
            viewtickdet.FormClosed += new FormClosedEventHandler(viewtickdet_FormClosed);
            viewtickdet.ShowDialog(this);
        }
    }
}
