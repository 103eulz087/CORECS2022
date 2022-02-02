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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AccountReconDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AccountReconDevEx()
        {
            InitializeComponent();
        }

        private void AccountReconDevEx_Load(object sender, EventArgs e)
        {
            populateCOA();
        }


        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT * FROM view_GLAccounts", searchLookUpEdit1, "AccountCode", "AccountCode");
        }

        void display()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_BankReconDisplay";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdatefrom", dateFrom.Text);
                com.Parameters.AddWithValue("@parmdateto", dateTo.Text);
                com.Parameters.AddWithValue("@parmacctcode", searchLookUpEdit1.Text);

                //com.Parameters.AddWithValue("@parmbal", txtbalance.Text).Direction=ParameterDirection.Output;
                //com.Parameters.AddWithValue("@parmdepintran", txtdepositintransit.Text).Direction = ParameterDirection.Output;
                //com.Parameters.AddWithValue("@parmoutstandingcheck", txtoutstandingchecks.Text).Direction = ParameterDirection.Output;
                //com.Parameters.AddWithValue("@parmunclearedcheck", txtunclearedchecks.Text).Direction = ParameterDirection.Output;
                //com.Parameters.AddWithValue("@parmglbalance", txtglbalance.Text).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmbal", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmdepintran", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmoutstandingcheck", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmunclearedcheck", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmglbalance", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //txtdepositintransit.Text=com.Parameters["@parmdepintran"].Value.ToString();
                //txtoutstandingchecks.Text=com.Parameters["@parmbal"].Value.ToString();
                //txtunclearedchecks.Text=com.Parameters["@parmbal"].Value.ToString();
                //txtglbalance.Text=com.Parameters["@parmbal"].Value.ToString();


                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                gridView1.Columns["SequenceNumber"].Visible = false;

                txtbalance.Text = com.Parameters["@parmbal"].Value.ToString();
                txtdepositintransit.Text = com.Parameters["@parmdepintran"].Value.ToString();
                txtoutstandingchecks.Text = com.Parameters["@parmoutstandingcheck"].Value.ToString();
                txtunclearedchecks.Text = com.Parameters["@parmunclearedcheck"].Value.ToString();
                txtglbalance.Text = com.Parameters["@parmglbalance"].Value.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            display();
            //refreshTotals();
            //gridView1.Columns["CheckBankDebits"].Summary.Clear();
            //gridView1.Columns["CheckBankDebits"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CheckBankDebits", "{0}");
            //gridView1.Columns["DepositBankCredits"].Summary.Clear();
            //gridView1.Columns["DepositBankCredits"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DepositBankCredits", "{0}");
        }

        //void refreshTotals()
        //{
        //    double uncleareddepdeb = 0.0, unclearedcheckdeb = 0.0, cleareddepdeb = 0.0, clearedcheckdeb = 0.0;
        //    for (int i=0;i<=gridView1.RowCount-1;i++)
        //    {
        //        if(gridView1.GetRowCellValue(i, "Status").ToString()=="Uncleared")
        //        {
        //            unclearedcheckdeb += Convert.ToDouble(gridView1.GetRowCellValue(i, "CheckBankDebits").ToString());
        //            uncleareddepdeb += Convert.ToDouble(gridView1.GetRowCellValue(i, "DepositBankCredits").ToString());
        //        }
        //        if (gridView1.GetRowCellValue(i, "Status").ToString() == "Cleared")
        //        {
        //            clearedcheckdeb += Convert.ToDouble(gridView1.GetRowCellValue(i, "CheckBankDebits").ToString());
        //            cleareddepdeb += Convert.ToDouble(gridView1.GetRowCellValue(i, "DepositBankCredits").ToString());
        //        }
        //    }
        //    unclearedcred.Text = unclearedcheckdeb.ToString();
        //    uncleareddeb.Text = uncleareddepdeb.ToString();
        //    clearedcred.Text = clearedcheckdeb.ToString();
        //    cleareddeb.Text = cleareddepdeb.ToString();
        //}

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void clearedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("UPDATE AccountRecon SET Status='Cleared' WHERE SequenceNumber='"+gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"SequenceNumber").ToString()+"'");
            Database.ExecuteQuery("Update TicketMaster SET Status='Cleared' WHERE TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' ");
            display();
            //refreshTotals();
        }

        private void unclearedToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   Database.ExecuteQuery("UPDATE AccountRecon SET Status='Uncleared' WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'");
            Database.ExecuteQuery("Update TicketMaster SET Status='Uncleared' WHERE TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' ");
            display();
            //refreshTotals(); 
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //GridView view = (GridView)sender;
            //string status = view.GetRowCellValue(e.RowHandle, "Status").ToString();
            //if (status == "Cleared")
            //{
            //    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
            //    e.Appearance.ForeColor = Color.Red;
            //}

            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string status = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Status"]);
                if (status == "Cleared")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font,FontStyle.Regular);
                    e.Appearance.ForeColor = Color.Red;
                    //e.Appearance.BackColor = Color.Salmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                }
            }

        }

        private void txtcheckdate_Popup(object sender, EventArgs e)
        {
            DateEdit edit = sender as DateEdit;
            PopupDateEditForm form = (edit as IPopupControl).PopupWindow as PopupDateEditForm;
            form.Calendar.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.YearInfo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {
                string filepath = "C:\\MyFiles\\";
                string filename = "BankRecon_" + dateFrom.Text.Replace('/', '-') + '-' + dateTo.Text.Replace('/', '-') + ".xls";
                string file = filepath + filename;
                gridView1.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }

        private void viewTicketDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.ViewTicketMasterDetails asds = new Accounting.ViewTicketMasterDetails();
            //Database.display("Select * FROM TicketMaster WHERE TicketNumber='"+gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"TicketNumber").ToString()+"'",asds.d)
            Database.GridMasterDetail("TicketMaster", "TicketDetails", "TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "'", "TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "'", "TicketNumber", "TicketNumber", "TicketMasterDetails", asds.gridControl1);
            asds.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lockBankRecon();
        }

        void lockBankRecon()
        {
            //Database.display("SELECT SequenceNumber,Status,CheckBankDebits,DepositBankCredits,DateProcessed,Reference,AccountDescription as Description FROM AccountRecon WHERE AccountCode='"+searchLookUpEdit1.Text+ "' AND DateProcessed >= '"+dateFrom.Text+"' AND DateProcessed <= '"+dateTo.Text+"' ", gridControl1, gridView1);
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_LockBankRecon";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdatefrom", dateFrom.Text);
                com.Parameters.AddWithValue("@parmdateto", dateTo.Text);
                com.Parameters.AddWithValue("@parmacctcode", searchLookUpEdit1.Text);

                com.Parameters.Add("@parmbal", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmdepintran", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmoutstandingcheck", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmunclearedcheck", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmglbalance", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
          
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                gridView1.Columns["SequenceNumber"].Visible = false;

                txtbalance.Text = com.Parameters["@parmbal"].Value.ToString();
                txtdepositintransit.Text = com.Parameters["@parmdepintran"].Value.ToString();
                txtoutstandingchecks.Text = com.Parameters["@parmoutstandingcheck"].Value.ToString();
                txtunclearedchecks.Text = com.Parameters["@parmunclearedcheck"].Value.ToString();
                txtglbalance.Text = com.Parameters["@parmglbalance"].Value.ToString();
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

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}