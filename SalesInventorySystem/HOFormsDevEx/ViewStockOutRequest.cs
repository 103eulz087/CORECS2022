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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewStockOutRequest : Form
    {
        public ViewStockOutRequest()
        {
            InitializeComponent();
        }

        private void btnForApprovalSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM [dbo].[view_StockOutSummary] WHERE Status='FOR APPROVAL' and DateAdded >= '" + datefromsts.Text + "' and DateAdded <= '" + datetosts.Text + "'and BranchCode='" + Login.assignedBranch + "' ", gridControlSTS, gridViewSTS);

        }

        private void btnApprovedSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM [dbo].[view_StockOutSummary] WHERE Status='APPROVED' and DateAdded >= '" + datefromsts.Text + "' and DateAdded <= '" + datetosts.Text + "'and BranchCode='" + Login.assignedBranch + "' ", gridControlapprvdsts, gridViewapprvdsts);

        }

        private void btnRejectedSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM [dbo].[view_StockOutSummary] WHERE Status='REJECTED' and DateAdded >= '" + datefromsts.Text + "' and DateAdded <= '" + datetosts.Text + "'and BranchCode='" + Login.assignedBranch + "' ", gridControlrjctdsts, gridViewrjctdsts);

        }

        void display()
        {

        }

        private void approveThisRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Approved this Request?", "Approved Request");
            if (confirm)
            {
                doFIFO();
                Database.ExecuteQuery($"UPDATE dbo.StockOutSummary SET Status='APPROVED' WHERE BatchID='{gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "BatchID").ToString()}'");
            }
            else
            { return; }
              
        }

        private void rejectThisRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Reject this Request?", "Reject Request");
            if (confirm)
                Database.ExecuteQuery($"UPDATE dbo.StockOutSummary SET Status='REJECTED' WHERE BatchID='{gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "BatchID").ToString()}'");
            else
                return;
        }


        void doFIFO()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                //string sp = "sp_FiFoMapping";
                //SqlCommand com = new SqlCommand(sp, con);
                //com.Parameters.AddWithValue("@parmtransdate", txtdatein.Text);
                //com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                //com.Parameters.AddWithValue("@parmprodcode", pcode);
                //com.Parameters.AddWithValue("@parmqty", txtqty.Text);
                //com.Parameters.AddWithValue("@parmoption", "1");
                string sp = "sp_FiFoWithOptions";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmorderno", gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "BatchID").ToString());
                com.Parameters.AddWithValue("@parmtransdate", gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "DateAdded").ToString());
                com.Parameters.AddWithValue("@parmbranchcode", gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "BranchCode").ToString());
                com.Parameters.AddWithValue("@parmprodcode", "");
                com.Parameters.AddWithValue("@parmqty", "0");
                com.Parameters.AddWithValue("@parmoption", "2");
                com.Parameters.AddWithValue("@parmsellingprice", "0");
                com.Parameters.AddWithValue("@parmcost", "0");
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
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

      

    }
}
