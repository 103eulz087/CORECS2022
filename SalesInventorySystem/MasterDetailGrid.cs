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
using MySql.Data.MySqlClient;

namespace SalesInventorySystem
{
    public partial class MasterDetailGrid : DevExpress.XtraEditors.XtraForm
    {
       // static string constringLocal = "Data Source=abacos.com.ph;Initial Catalog=SalesAndInventory;UserID=sa;Password=p@$$w0rd;";
        string constringLocal = "SERVER=abacos.com.ph;DATABASE=abacos_lucky7;UID=abacos_livetrends;PASSWORD=6969rd//;";
        public MasterDetailGrid()
        {
            InitializeComponent();
        }

        private void MasterDetailGrid_Load(object sender, EventArgs e)
        {
            //Database.GridMasterDetailMysql("testview", "tblgamebets", "userid<>''", "userid IN (Select userid FROM testview) ", "userid", "userid", "UserDetails", gridControl1);
            Database.GridMasterDetailMysql("SELECT * FROM testview"
                , "SELECT userid,gameid,transactionno,combination,straight,rumble,draw,datedraw,datetrn " +
                            "FROM tblgamebets " +
                            "WHERE userid IN (Select userid FROM testview)"
                , "testview"
                , "tblgamebets"
                , "userid"
                , "userid"
                , "TicketHistory"
                , gridControl1, "");

            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "NumberOfTickets");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalAmountBet");

            //MySqlConnection con = new MySqlConnection(constringLocal);
            //con.Open();
            ////  cont.BeginUpdate();
            //string query = "SELECT * FROM tblgamesummary LIMIT 1";
            //MySqlCommand com = new MySqlCommand(query, con);
            //MySqlDataAdapter adapter = new MySqlDataAdapter(com);
            //DataTable table = new DataTable();
            //try
            //{
            //    com.CommandTimeout = 180;
            //    gridView1.Columns.Clear();
            //    gridControl1.DataSource = null;
            //    adapter.Fill(table);
            //    //  table.Columns.Add("OvertimeType");
            //    gridControl1.DataSource = table;
            //    gridView1.BestFitColumns();
            //}
            //catch (MySqlException ee)
            //{
            //    XtraMessageBox.Show(ee.ToString());
            //}
            //finally
            //{
            //    //   cont.EndUpdate();
            //    con.Close();
            //}

        }

    }
}