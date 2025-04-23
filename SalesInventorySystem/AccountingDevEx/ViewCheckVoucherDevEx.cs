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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class ViewCheckVoucherDevEx : DevExpress.XtraEditors.XtraForm
    { 
        public static string supplierid,refno,vouchid, paidto, checkno, checkdate, pariculars, amount, vouchertype,dateadded, preparedby;

        public ViewCheckVoucherDevEx()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Database.display("SELECT * " +
                    "FROM view_Voucher " +
                    "WHERE DateAdded BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' " +
                    "", gridControl1, gridView1);
            }
            else
            {
                Database.display("SELECT * " +
                    "FROM view_Voucher " +
                    "WHERE DateAdded BETWEEN '" + datefrom.Text + "' and '" + dateto.Text + "' " +
                    "AND isErrorCorrect='0' ", gridControl1, gridView1);
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        void viewVoucherDetails()
        {
            refno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNumber").ToString();
            supplierid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            vouchid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            paidto = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PaidTo").ToString();
            checkno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckNo").ToString();
            checkdate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckDate").ToString();
            pariculars = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Particulars").ToString();
            amount = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString();
            vouchertype = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherType").ToString();
            preparedby = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PreparedBy").ToString();

            AccountingDevEx.ViewCheckVoucherDetailsDevEx vouch = new ViewCheckVoucherDetailsDevEx();
            Database.display("SELECT * " +
                "FROM view_VoucherDetails " +
                "WHERE VoucherID='" + vouchid + "' " +
                "and SupplierID='" + supplierid + "' ", vouch.gridControl1, vouch.gridView1);
            vouch.gridView1.Columns["VoucherID"].Visible = false;
            //Database.display($"SELECT * FROM view_VoucherDetails WHERE ReferenceNumber='{refno}'", vouch.gridControl1, vouch.gridView1);
            //Classes.DevXGridViewSettings.ShowFooterCountTotal(vouch.gridView1, "VoucherID");
            Classes.DevXGridViewSettings.ShowFooterTotal(vouch.gridView1, "Amount");
            vouch.ShowDialog(this);
        }

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewVoucherDetails();
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
                com.Parameters.AddWithValue("@parmsupplierid", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString());
                com.Parameters.AddWithValue("@parmcheckno", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckNo").ToString());
                com.Parameters.AddWithValue("@parmreferenceno", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNumber").ToString());
                com.Parameters.AddWithValue("@parmvoucherid", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString());
                com.Parameters.AddWithValue("@parmvouchertype", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherType").ToString());
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

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
        private void errorCorrecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isErrorCorrect").ToString()) == true)
            {
                XtraMessageBox.Show("Entry Already Corrected!");
                return;
            }
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Cancel this Cheque? if yes All Ticket Entries in this Transaction Voucher will automatically create reversal entries..", "Cancelled Cheque");
            if (confirm)
            {
                CancelledCheque();
                XtraMessageBox.Show("Payment Successfully Posted");
            }
            else
            {
                return;
            }
        }

        private void liquidateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string a = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
            string b = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            bool check = false,checkIfLiquidated=false;
            check=Database.checkifExist("SELECT TOP(1) isnull(isLiquidation,0) " +
                "FROM CheckVoucher " +
                "WHERE VoucherID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString() + "' " +
                "AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "'" +
                "AND isLiquidation=1 ");
            checkIfLiquidated = Database.checkifExist("SELECT TOP(1) isnull(isLiquidation,0) " +
                "FROM CheckVoucher " +
                "WHERE VoucherID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString() + "' " +
                "AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "'" +
                "AND OfficialReceiptNo='LIQUIDATED' ");
            if(checkIfLiquidated)
            {
                XtraMessageBox.Show("You Already Liquidate this Voucher!...");
                return;
            }
            if (check == true && checkIfLiquidated == false)
            {
                HOFormsDevEx.LiquidationDevExFrm easd = new HOFormsDevEx.LiquidationDevExFrm();
                easd.txtvoucherid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VoucherID").ToString();
                easd.txtcheckname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PaidTo").ToString();
                easd.txtcheckamount.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString();
                easd.txtparticulars.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Particulars").ToString();
                easd.txtvendor.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
                easd.txtcheckno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckNo").ToString();
                easd.txtcheckdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CheckDate").ToString();
                easd.ShowDialog(this);
            }
            else
            {
                XtraMessageBox.Show("This CheckVoucher is not for Liquidation!...");
            }
            
        }

        private void ViewCheckVoucherDevEx_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            datefrom.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateto.Text = lastDay.ToShortDateString();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            viewVoucherDetails();
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

      
    }
}