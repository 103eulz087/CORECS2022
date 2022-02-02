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
    public partial class AccountPayablesDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AccountPayablesDetailsDevEx()
        {
            InitializeComponent();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            string status = view.GetRowCellValue(e.RowHandle, "PaymentStatus").ToString();
            double balance = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, "Balance"));
            if (status == "FULLYPAID")
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Green;
            }
            if (e.Column.FieldName == "Balance")
            {
                if (Convert.ToDouble(e.CellValue) > 0)
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void AccountPayablesDetailsDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}