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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class JFCInventoryPerBoxDevEx : DevExpress.XtraEditors.XtraForm
    {
        public JFCInventoryPerBoxDevEx()
        {
            InitializeComponent();
        }

        void rowstyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string val = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Available"]);
                if (Convert.ToDouble(val)<=0)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.HighPriority = true;
                }
            }
        }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            rowstyle(sender,e);
        }

        private void JFCInventoryPerBoxDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}