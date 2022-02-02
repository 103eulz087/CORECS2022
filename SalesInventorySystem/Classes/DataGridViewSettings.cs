using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    class DataGridViewSettings
    {

        public static void gridFooter(DataGridView view)
        {
            view.Rows[view.RowCount - 1].DefaultCellStyle.BackColor = Color.Red;
            view.Rows[view.RowCount - 1].DefaultCellStyle.ForeColor = Color.White;
            view.Rows[view.RowCount - 1].DefaultCellStyle.Font = new Font("Segoe UI", 10.25f,FontStyle.Bold);
        }

        public static void gridDefaultSettings(DataGridView view)
        {
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            view.BorderStyle = BorderStyle.None;
            view.BackgroundColor = Color.White;
            view.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            view.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            view.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            view.DefaultCellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Regular);
            view.MultiSelect = false;
            view.ReadOnly = true;
            view.RowHeadersVisible = false;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
