using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
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

namespace SalesInventorySystem.POSDevEx
{
    public partial class POSSalesCollectionSummary : Form
    {
        public POSSalesCollectionSummary()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();

            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["BranchName"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
            gridView1.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "CashRemitted";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;

            ite.DisplayFormat = "{0:n0}";
            ite.ShowInGroupColumnFooter = view.Columns["CashRemitted"];
           
            gridView1.GroupSummary.Add(ite);
            gridView1.Focus();
            
            Classes.DevXGridViewSettings.getTotalSummation(gridView1, "CashRemitted","Shortage","Overage");
        }

        void display()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_POSCashierSalesCollection";
            SqlCommand com = new SqlCommand(query,con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
             
            com.Parameters.AddWithValue("@datefrom", txtsalesdatefrom.Text);
            com.Parameters.AddWithValue("@dateto", txtsalesdateto.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            Classes.DevXGridViewSettings.setGridFormat(gridView1);
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            display();

            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["BranchName"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
            gridView1.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "BranchName";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["BranchName"];
            gridView1.GroupSummary.Add(ite);
            gridView1.Focus();

            Classes.DevXGridViewSettings.getTotalSummation(gridView1, "CashRemitted", "Shortage", "Overage");
        }
        void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\ITCOREFiles\\SalesCollection";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/SalesCollection//");
            }
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            //string dfrm = txtsalesdatefrom.Text.Replace(@"\", "-");
            //string dtto = txtsalesdateto.Text.Replace(@"\", "-");
            string filename = "SALES_COLLECTION_" + txtsalesdatefrom.Text.Replace(@"/", "-")+"_"+ txtsalesdateto.Text.Replace(@"/", "-");
            exporttoexcel(gridView1, filename);
        }
    }
}
