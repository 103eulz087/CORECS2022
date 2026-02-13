using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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


        public static object GetSingleValueRepositoryItem(
            RepositoryItemSearchLookUpEdit repoItem,
            string fieldName)
        {
            if (repoItem == null)
                throw new ArgumentNullException(nameof(repoItem));

            // The repository item itself exposes the view
            GridView view = repoItem.View as GridView;
            if (view == null)
                return null;

            // Validate column exists
            var col = view.Columns[fieldName];
            if (col == null)
                return null;

            int rowHandle = view.FocusedRowHandle;
            if (!view.IsDataRow(rowHandle))
                return null;

            return view.GetRowCellValue(rowHandle, col);
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
