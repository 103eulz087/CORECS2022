using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class ViewCheckVoucher : Form
    {
        public static string vouchid,paidto,checkno,checkdate,pariculars,amount,dateadded,preparedby;

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vouchid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            paidto = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PaidTo").ToString();
            checkno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckNo").ToString();
            checkdate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckDate").ToString();
            pariculars = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Particulars").ToString();
            amount = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString();
            preparedby = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PreparedBy").ToString();
            Accounting.ViewCheckVoucherDetails vouchdet = new ViewCheckVoucherDetails();
            vouchdet.Show();
            Database.display("SELECT * FROM view_VoucherDetails WHERE VoucherID='" + vouchid + "'", vouchdet.gridControl1, vouchdet.gridView1);
            vouchdet.gridView1.Columns["VoucherID"].Visible = false;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "isErrorCorrect"));
            if (check)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void errorCorrecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isErrorCorrect").ToString()) == true)
            {
                XtraMessageBox.Show("Entry Already Corrected!");
                return;
            }
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Cancel this Cheque? if yes All Ticket Entries in this Transaction Voucher will automatically create reversal entries..", "Cancelled Cheque");
            if(confirm)
            {
                CancelledCheque();
                XtraMessageBox.Show("Payment Successfully Posted");
            }
            else
            {
                return;
            }
        }

        void CancelledCheque()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
               
                string query = "sp_CancelledCheques";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmvoucherid", id);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
               
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

        private void liquidateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.LiquidationDevExFrm easd = new HOFormsDevEx.LiquidationDevExFrm();
            easd.Show();
            easd.txtvoucherid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            easd.txtcheckname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PaidTo").ToString();
            easd.txtcheckamount.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString();
            easd.txtparticulars.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Particulars").ToString();
            easd.txtvendor.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            easd.txtcheckno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckNo").ToString();
            easd.txtcheckdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckDate").ToString();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        public ViewCheckVoucher()
        {
            InitializeComponent();
        }

        private void ViewCheckVoucher_Load(object sender, EventArgs e)
        {
            //Classes.Utilities.setDate(datefrom.Text, dateto.Text);
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            datefrom.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateto.Text = lastDay.ToShortDateString();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                Database.display("SELECT * FROM view_Voucher WHERE DateAdded BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' ", gridControl1, gridView1);
            }
            else
            {
                Database.display("SELECT * FROM view_Voucher WHERE DateAdded BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' AND isErrorCorrect='0' ", gridControl1, gridView1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            vouchid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            paidto = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PaidTo").ToString();
            checkno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckNo").ToString();
            checkdate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckDate").ToString();
            pariculars = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Particulars").ToString();
            amount = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString();
            preparedby = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PreparedBy").ToString();
            Accounting.ViewCheckVoucherDetails vouchdet = new ViewCheckVoucherDetails();
            vouchdet.Show();
            Database.display("SELECT * FROM view_VoucherDetails WHERE VoucherID='" + ViewCheckVoucher.vouchid + "'", vouchdet.gridControl1, vouchdet.gridView1);
            vouchdet.gridView1.Columns["VoucherID"].Visible = false;
        }

        private void reprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
    }
}
