using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Branches
{
    public partial class ViewBranchRequestDetails : Form
    {
        public ViewBranchRequestDetails()
        {
            InitializeComponent();
        }

        private void ViewBranchRequestDetails_Load(object sender, EventArgs e)
        {

        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "Qty")
            {
                e.Appearance.BackColor = Color.Yellow;
                // e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "Dispatched")
            {
                e.Appearance.BackColor = Color.LightSalmon;
               // e.Appearance.ForeColor = Color.Red;
            } 
            if (e.Column.FieldName == "Received")
            {
                e.Appearance.BackColor = Color.LightBlue;
               // e.Appearance.ForeColor = Color.Red;
            }
        }
    }
}
