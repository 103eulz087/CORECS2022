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
    public partial class ViewTicketDetails : Form
    {
        public ViewTicketDetails()
        {
            InitializeComponent();
        }

        private void ViewTicketDetails_Load(object sender, EventArgs e)
        {
            //string ticketnumber = "";
            //ticketnumber = Database.getSingleQuery("TicketDetails", "AccountCode='" + Accounting.GLSummary.accountcode + "' and TicketDate = '" + Accounting.GLSummary.postingdate + "' and BranchCode='" + Accounting.GLSummary.branchcode + "'", "TicketNumber");
            //Database.display("SELECT * FROM TicketMaster WHERE TicketNumber = '" + ticketnumber + "' ", gridControl1, gridView1);
            //try
            //{
            //    Database.GridMasterDetail("TicketMaster", "TicketDetails", "BranchCode='" + Accounting.GLSummary.branchcode + "'", "AccountCode='" + Accounting.GLSummary.accountcode + "' and TicketDate = '" + Accounting.GLSummary.postingdate + "' and BranchCode='" + Accounting.GLSummary.branchcode + "'", "TicketNumber", "TicketNumber", "TicketMasterDetails", gridControl1);
            //}
            //catch (SqlException ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
         }
    }
}
