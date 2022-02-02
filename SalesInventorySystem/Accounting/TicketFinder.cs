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

namespace SalesInventorySystem.Accounting
{
    public partial class TicketFinder : DevExpress.XtraEditors.XtraForm
    {
        public TicketFinder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Database.GridMasterDetail("EulzTracing", "TicketDetails","BranchCode <> ''", "TicketNumber in(Select TicketNumber FROM EulzTracing)", "TicketNumber","TicketNumber", "TicketMasterDetails", gridControl1);
           //GridMasterDetailMysql(string query1, string query2, string table1, string table2, string col1, string col2, string fkeyname, GridControl grid, string eulz)
           Database.GridMasterDetailMysql("SELECT * FROM testview","SELECT userid,gameid,transactionno,combination,straight,rumble,draw,datedraw,datetrn FROM tblgamebets WHERE userid IN (Select userid FROM testview)","testview","tblgamebets","userid","userid","UserDetails",gridControl1,"");

        }
    }
}