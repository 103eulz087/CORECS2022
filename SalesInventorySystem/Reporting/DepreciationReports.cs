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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace SalesInventorySystem.Reporting
{
    public partial class DepreciationReports : DevExpress.XtraEditors.XtraForm
    {
        public DepreciationReports()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_DepreciationReports";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmdate", datefrom.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Accumulated";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Accumulated"];
            gridView1.GroupSummary.Add(ite);

            GridGroupSummaryItem ite2 = new GridGroupSummaryItem();
            ite2.FieldName = "BookValue";
            ite2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite2.ShowInGroupColumnFooter = gridView1.Columns["BookValue"];
            gridView1.GroupSummary.Add(ite2);

            Classes.DevXGridViewSettings.setGridFormat(gridView1);
            gridView1.BestFitColumns();
            gridView1.Columns["Accumulated"].Summary.Clear();
            gridView1.Columns["BookValue"].Summary.Clear();
            gridView1.Columns["Accumulated"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Accumulated", "{0}");
            gridView1.Columns["BookValue"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BookValue", "{0}");
            gridView1.ExpandAllGroups();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}