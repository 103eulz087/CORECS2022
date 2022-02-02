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
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;

namespace SalesInventorySystem
{
    public partial class POShowOrderDetails : DevExpress.XtraEditors.XtraForm
    {
        public POShowOrderDetails()
        {
            InitializeComponent();
        }

        private void POShowOrderDetails_Load(object sender, EventArgs e)
        {
            //gridView2.AddNewRow();
            display();
            gridView2.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:n2}");
            //double total = Database.getTotalSummation("view_POVariance", " WHERE PONumber='" + ViewRequest.pono + "' ", "Qty");
            //gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Qty", total);
        }

        private void display()
        {
            Database.display("SELECT * FROM view_POVariance WHERE PONumber='" + ViewRequest.pono + "'", gridControl2, gridView2);
            
           // double total=0.0;
           // string totalkilos = "";
            
           //// total = Database.getTotalSummation("view_POVariance"," WHERE PONumber='"+ViewRequest.pono+"' ","Qty");
           // for (int i = 0; i <= gridView2.RowCount; i++)
           // {
           //     totalkilos = gridView2.GetRowCellValue(i, "Qty").ToString();
           //     total += Convert.ToDouble(totalkilos); 
           // }
            //DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            //row[4] = "0";
            
        }

        //private void display()
        //{
        //    string query = "SELECT * FROM view_POVariance WHERE PONumber='" + ViewRequest.pono + "'";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataAdapter adapter = new SqlDataAdapter(com);
        //    DataTable table = new DataTable();
        //    try
        //    {
        //        adapter.Fill(table);
        //        string totalkilos = "";
        //        double total = 0.0;
        //        gridControl2.DataSource = table;
        //        gridView2.BestFitColumns();
        //        gridView2.AddNewRow();
        //        for (int i = 0; i <= gridView2.RowCount; i++)
        //        {
        //            totalkilos = gridView2.GetRowCellValue(i, "Qty").ToString();
        //            total += Convert.ToDouble(totalkilos);

        //        }
        //        gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Qty", 2222);
                
        //    }
        //    catch (Exception ee)
        //    {
        //        throw new Exception(ee.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

    }
}