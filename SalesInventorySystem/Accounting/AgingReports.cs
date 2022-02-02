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
using DevExpress.XtraGrid;

namespace SalesInventorySystem.Accounting
{
    public partial class AgingReports : DevExpress.XtraEditors.XtraForm
    {
        public AgingReports()
        {
            InitializeComponent();
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_Aging";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmdatefrom", datefrom.Text);
            com.Parameters.AddWithValue("@parmdateto", dateto.Text);
            com.Parameters.AddWithValue("@parmtype", txtagingtype.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            con.Close();
        }

     

        private void btnextract_Click(object sender, EventArgs e)
        {
            try
            {
                execute();
                Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "InvoiceNo");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "0 to 30");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "31 to 60");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "61 to 90");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "91 to 120");
                //GridGroupSummaryItem ite = new GridGroupSummaryItem();
                //ite.FieldName = "0 to 30";
                //ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //ite.ShowInGroupColumnFooter = gridView1.Columns["0 to 30"];
                //gridView1.GroupSummary.Add(ite);

                //GridGroupSummaryItem ite1 = new GridGroupSummaryItem();
                //ite1.FieldName = "31 to 60";
                //ite1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //ite1.ShowInGroupColumnFooter = gridView1.Columns["31 to 60"];
                //gridView1.GroupSummary.Add(ite1);

                //GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
                //ite11.FieldName = "61 to 90";
                //ite11.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //ite11.ShowInGroupColumnFooter = gridView1.Columns["61 to 90"];
                //gridView1.GroupSummary.Add(ite11);

                //GridGroupSummaryItem ite1111 = new GridGroupSummaryItem();
                //ite1111.FieldName = "91 to 120";
                //ite1111.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //ite1111.ShowInGroupColumnFooter = gridView1.Columns["91 to 120"];
                //gridView1.GroupSummary.Add(ite1111);

                //gridView1.Columns["0 to 30"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "0 to 30", "{0:n2}");
                //gridView1.Columns["31 to 60"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "31 to 60", "{0:n2}");
                //gridView1.Columns["61 to 90"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "61 to 90", "{0:n2}");
                //gridView1.Columns["91 to 120"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "91 to 120", "{0:n2}");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\";
            Classes.Utilities.createDirectoryFolder(filepath);
            string filename = "AGINGREPORTS" + "_" + txtagingtype.Text + '-' + datefrom.Text.Replace('/', '-') + ".xls";
            string file = filepath + filename;
            gridView1.ExportToXls(file);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
        }
    }
}