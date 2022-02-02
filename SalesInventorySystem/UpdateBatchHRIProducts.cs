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

namespace SalesInventorySystem
{
    public partial class UpdateBatchHRIProducts : DevExpress.XtraEditors.XtraForm
    {
        object value = null;
        public UpdateBatchHRIProducts()
        {
            InitializeComponent();
        }

        private void UpdateBatchHRIProducts_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='888' ORDER BY ProductCode ASC", txtprod, "Description", "Description");

        }

        private void txtprod_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtprod.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            value = view.GetRowCellValue(rowHandle, "ProductCode");
            txtsprice.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE CustomerProductSetting SET SpecialPriceAmount='" + txtsprice.Text + "' WHERE ProductCode='" + value + "'", "Successfully Updated!...");
            txtsprice.Text = "";
            txtprod.Focus();
        }
    }
}