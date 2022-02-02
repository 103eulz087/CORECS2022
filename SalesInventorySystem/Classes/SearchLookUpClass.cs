using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    public class SearchLookUpClass
    {

       
        public static Object getSingleValue(SearchLookUpEdit searchLookUpEdit1, string fieldname,SqlConnection con)
        {
            object value;
            GridView view = searchLookUpEdit1.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            string fieldName = fieldname; // or other field name
            value = view.GetRowCellValue(rowHandle, fieldName);
            return value;
        }
        public static Object getSingleValue(SearchLookUpEdit searchLookUpEdit1,  string fieldname)
        {
            object value;
            GridView view = searchLookUpEdit1.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            string fieldName = fieldname; // or other field name
            value = view.GetRowCellValue(rowHandle, fieldName);
            return value;
        }
        //AddBranchOrder
        //GridView view = searchLookUpEdit1.Properties.View;
        //int rowHandle = view.FocusedRowHandle;
        ////string fieldName = "Name"; // or other field name
        //object value = view.GetRowCellValue(rowHandle, "SeqNo");
        //object valueAvailable = view.GetRowCellValue(rowHandle, "Available");
        //txtseqno.Text = value.ToString();
        //    txtweight.Text = valueAvailable.ToString();
        //    txtweight.Focus();
    }
}
