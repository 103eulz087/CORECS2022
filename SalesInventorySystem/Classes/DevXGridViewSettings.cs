using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace SalesInventorySystem.Classes
{
    class DevXGridViewSettings
    {
        public static GridView gridStrikeout(RowCellStyleEventArgs e,String col,String value)
        {
            GridView view = new GridView();
            //bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, col));
            if (e.Column.FieldName == col)
            {
                if (Convert.ToString(e.CellValue) == value)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                }
            }
            return view;
        }

       

        private static GridView rowcellstyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e,string col,Color color)
        {
            
            GridView view = (GridView)sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, col));
            if (!check)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = color;
            }
            return view;
        }
        public static GridView rowstyle(object sender, RowStyleEventArgs e,string viewcol,string viewvalue)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns[viewcol]);
                if (category.Equals(viewvalue))
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.HighPriority = true;
                }
            }
            return View;
        }

        private static GridView showeditor(object sender, CancelEventArgs e,string col)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != col)
                e.Cancel = true;
            return view;

        }

        public static GridView setGridFormat(object sender)
        {
            
            GridView view = sender as GridView;
            foreach (GridColumn col in view.Columns)
            {

                if (col.ColumnType == typeof(DateTime))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "MM/dd/yyyy";
                }
                else
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n2";
                }
            }
            return view;
        }

        public static GridView getTotalSummation(object sender,params string[] summaryvalues)
        {
            GridView view = sender as GridView;
            
            foreach (GridColumn col in view.Columns)
            {
                //foreach (String str in values)
                //{
                //    view.Columns[str].Visible = false;
                //}

                if (col.ColumnType == typeof(DateTime))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "MM/dd/yyyy";
                }
                else
                {
                    foreach (string str in summaryvalues)
                    {
                        if (str == col.FieldName)
                        {
                            col.Summary.Clear();
                            col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, str, "{0:n2}");
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            col.DisplayFormat.FormatString = "n2";
                        }
                    }
                }
            }
            return view;
        }
        public static GridView getTotalSummation(object sender, string[] hidevalues,params string[] summaryvalues)
        {
            GridView view = sender as GridView;

            foreach (GridColumn col in view.Columns)
            {
                foreach (String str2 in hidevalues)
                {
                    view.Columns[str2].Visible = false;
                }

                if (col.ColumnType == typeof(DateTime))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "MM/dd/yyyy";
                }
                else
                {
                    foreach (string str in summaryvalues)
                    {
                        if (str == col.FieldName)
                        {
                            col.Summary.Clear();
                            col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, str, "{0:n2}");
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            col.DisplayFormat.FormatString = "n2";
                        }
                    }
                }
            }
            return view;
        }

        public static void ShowFooterTotal(GridView view,string col)
        {
          
            GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
            ite11.FieldName = col;
            ite11.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite11.ShowInGroupColumnFooter = view.Columns[col];
            view.GroupSummary.Add(ite11);
            view.Columns[col].Summary.Add(DevExpress.Data.SummaryItemType.Sum, col, "{0:n2}");
            //return view;
        }

        public static void ShowFooterCountTotal(GridView view, string col)
        {

            GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
            ite11.FieldName = col;
            ite11.SummaryType = DevExpress.Data.SummaryItemType.Count;
            ite11.ShowInGroupColumnFooter = view.Columns[col];
            view.GroupSummary.Add(ite11);
            view.Columns[col].Summary.Add(DevExpress.Data.SummaryItemType.Count, col, "{0}");
            //return view;
        }
        //private static GridView rowstyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e,params string[] str,string highlightedcol)
        //{
        //    GridView view = sender as GridView;
        //    if (e.RowHandle >= 0)
        //    {
        //        string status = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Status"]);
        //        if (status == "NO INVENTORY")
        //        {
        //            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        //            e.Appearance.BackColor = Color.Salmon;
        //            e.Appearance.BackColor2 = Color.SeaShell;
        //        }
        //        if (status == "FAILED")
        //        {
        //            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        //            e.Appearance.BackColor = Color.Blue;
        //            e.Appearance.BackColor2 = Color.LightBlue;
        //        }
        //    }
        //    return view;
        //}
    }
}
