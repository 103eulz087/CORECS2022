using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace SalesInventorySystem.Reporting
{
    public partial class AcctTicketReports : Form
    {
        //public static string debitvalue="", creditvalue = "";
        public AcctTicketReports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        void populateComboBox()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcode,"BranchCode","BranchCode");
        }

        void populateRows()
        {
            try
            { 
                if(chcktickets.Checked==true)
                {
                    //Database.display("SELECT * FROM view_AccountingTicketReports WHERE BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text+ "' AND EnteredBy='"+Login.Fullname+"' ORDER BY TicketNumber", gridControl1, gridView1);
                    string query = "SELECT * FROM view_AccountingTicketReports WHERE BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text + "' AND EnteredBy='" + Login.Fullname + "' ORDER BY TicketNumber ";
                    HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                    gridView1.Focus();
                }
                else
                {
                    //Database.display("SELECT * FROM view_AccountingTicketReports WHERE BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text+ "' ORDER BY TicketNumber", gridControl1, gridView1);
                    string query = "SELECT * FROM view_AccountingTicketReports WHERE BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text + "' ORDER BY TicketNumber";
                    HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
                    gridView1.Focus();
                }
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
          //  Database.displayLocalGrid("SELECT TicketDetails.TicketNumber, TicketMaster.Mnemonic, TicketDetails.AccountCode, ChartOfAccounts.Description, TicketDetails.Debit, TicketDetails.Credit, TicketMaster.Particulars, TicketMaster.EnteredBy, TicketMaster.CheckedBy, TicketMaster.ApprovedBy FROM TicketDetails INNER JOIN TicketMaster ON TicketDetails.TicketNumber=TicketMaster.TicketNumber INNER JOIN ChartOfAccounts ON TicketDetails.AccountCode=ChartOfAccounts.AccountCode WHERE TicketDetails.BranchCode='" + txtbrcode.Text + "' AND TicketDetails.TicketDate='" + txtdate.Text+ "' ORDER BY TicketDetails.TicketNumber ASC, TicketDetails.Debit ASC", dataGridView1);
        }

        Double computeTotalDebits()
        {
            double totaldebits = 0.0;
            totaldebits = Database.getTotalSummation2("view_AccountingTicketReports", "BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text+ "'", "Debit");
            //totaldebits =  Database.getTotalSummation("SELECT SUM(Debits) FROM FROM view_AccountingTicketReports WHERE BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text+ "'");
            return totaldebits;
        }

        Double computeTotalCredits()
        {
            double totalcredits = 0.0;
            totalcredits = Database.getTotalSummation2("view_AccountingTicketReports", "BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text+ "'", "Credit");
            //totaldebits =  Database.getTotalSummation("SELECT SUM(Debits) FROM FROM view_AccountingTicketReports WHERE BranchCode='" + txtbrcode.Text + "' AND TicketDate='" + txtdate.Text+ "'");
            return totalcredits;
        }

        private void AcctTicketReports_Load(object sender, EventArgs e)
        {
            populateComboBox();
        }

    
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblcredit_Click(object sender, EventArgs e)
        {

        }

        private void lbldebit_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblbranchname_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Convert.ToBoolean(Login.isglobalAdmin) == true)
            {
                if (e.Button == MouseButtons.Right)
                    contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void updateTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Accounting.UpdateTIckets updtick = new Accounting.UpdateTIckets();
            Database.display("SELECT * FROM TicketMaster WHERE TicketDate='" + txtdate.Text+ "' and BranchCode='" + txtbrcode.Text + "' AND TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' ", updtick.gridControlMaster, updtick.gridViewMaster);
            Database.display("SELECT TicketDate,BranchCode,ReferenceKey,TicketNumber,AccountCode,Debit,Credit,Debit as OrigDebit,Credit as OrigCredit, AccountCode as OrigAcctCode FROM TicketDetails WHERE TicketDate='" + txtdate.Text+ "' and BranchCode='" + txtbrcode.Text + "' AND TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' ", updtick.gridControlDetails, updtick.gridViewDetails);
            updtick.ShowDialog(this);
        }

        private void txtbrcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtbrcode.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            object value = view.GetRowCellValue(rowHandle, "BranchName");
            lblbranchname.Text = value.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                populateRows();
                lbldebit.Text = computeTotalDebits().ToString();
                lblcredit.Text = computeTotalCredits().ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
