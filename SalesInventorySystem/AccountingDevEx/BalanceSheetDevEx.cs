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

//-------------------------------------------------------------------------------
//ASSETS
//-------------------------------------------------------------------------------
//FUNDS & SUBSRIPTION RECEIVABLE
//1010201	    Petty Cash Fund
//1010202	    Change Fund
//10508	        Security Deposit
//10203	          Accounts Receivables - Subscriptions

//AR TRADE & AR OTHERS
//10201	          Accounts Receivables - Trade
//10202	          Accounts Receivables - Others

//ADVANCES TO EMPLOYEES
//10204	          Accounts Receivables -Advances to Officers/Employees
//10206	          Accounts Receivables -Advances Others

//PPE
//10602	          Software
//10603	          Store and Office Equipment
//10605	          Furniture and Fixture
//10606	          Transportation Equipment
//10607	          Leasehold Improvement

//10701	          Accum Depn  -Software
//10702	          Accum Depn - Store and Office Equipment
//10704	          Accum Depn - Furniture and Fixture
//10705	          Accum Depn - Transportation Equipment
//10706	          Accum Depn - Leasehold Improvement
//-------------------------------------------------------------------------------

//-------------------------------------------------------------------------------
//LIABILITIES
//AP TRADE
//20101	          Accounts Payable - Trade

//ACCRUED EXPENSES & AP OTHERS
//20102	          AP - Others
//20210	          Accrued Expense Payable

//MORTGAGE & LOANS PAYABLE
//20211	          Loans Payable
//20215	          MORTGAGE PAYABLE
//-------------------------------------------------------------------------------
namespace SalesInventorySystem.AccountingDevEx
{
    public partial class BalanceSheetDevEx : DevExpress.XtraEditors.XtraForm
    {
        public BalanceSheetDevEx()
        {
            InitializeComponent();
        }
        void executeSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_BalanceSheet";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmdate",txtdatefrom.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            gridControl1.DataSource = table;
            con.Close();
        }
        private void btnextract_Click(object sender, EventArgs e)
        {
            executeSP();
        }

        private void showReconDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountingDevEx.BalanceSheetAccountDetails viewticket = new AccountingDevEx.BalanceSheetAccountDetails();
            viewticket.Show();
            Database.display("select BranchCode" +
                ",TicketDate" +
                ",TicketNumber" +
                ",ReferenceKey" +
                ",Supplier" +
                ",Particulars" +
                ",Debit" +
                ",Credit " +
                "FROM view_AccountingTicketReports " +
                "where AccountCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountCode").ToString() + "'" +
                " order by TicketDate ", viewticket.gridControl1, viewticket.gridView1);
            
        }
        void rowstyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["AccountCode"]);
                if (category == "1" || category == "2" || category == "3")
                {
                    e.Appearance.BackColor = Color.LightBlue;
                    e.Appearance.BackColor2 = Color.LightCyan;
                    e.HighPriority = true;
                }
                if (category == "199" || category == "299" || category == "399")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.BackColor2 = Color.LightSeaGreen;
                    e.HighPriority = true;
                }
            }
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            rowstyle(sender, e);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }
    }
}