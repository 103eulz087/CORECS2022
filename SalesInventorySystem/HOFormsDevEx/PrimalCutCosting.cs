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
    public partial class PrimalCutCosting : DevExpress.XtraEditors.XtraForm
    {
        public PrimalCutCosting()
        {
            InitializeComponent();
        }

        private void PrimalCutCosting_Load(object sender, EventArgs e)
        {
            populate();
        }

        void populate()
        {
            Database.displaySearchlookupEdit("SELECT ShipmentNo,SupplierName FROM view_POSUMMARYREP ORDER BY ShipmentNo DESC", txtshipmentno,"ShipmentNo","ShipmentNo");
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            bool isExists = false;
            isExists = Database.checkifExist($"SELECT 1 FROM dbo.TempCosting WHERE ShipmentNo='{txtshipmentno.Text}'");
            if(isExists)
            {
                Database.display($"SELECT ItemCode as ProductCode,Parts as Description,CostPerKg as Cost FROM dbo.TempCosting WHERE ShipmentNo='{txtshipmentno.Text}'", gridControl1, gridView1);
            }
            else
            {
                XtraMessageBox.Show("This Shipment Number has not been defined for costing yet.");
                Database.display($"SELECT * FROM dbo.view_PrimalCutPartsForCosting", gridControl1, gridView1);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool isExists = false;
            isExists = Database.checkifExist($"SELECT 1 FROM dbo.TempCosting WHERE ShipmentNo='{txtshipmentno.Text}'");
            if(isExists)
            {
                if(gridView1.RowCount > 0)
                {
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        string costperkg = gridView1.GetRowCellValue(i, "Cost").ToString();
                        string productcode = gridView1.GetRowCellValue(i, "ProductCode").ToString();
                       
                        Database.ExecuteQuery($"UPDATE dbo.TempCosting SET CostPerKg='{costperkg}' WHERE ShipmentNo='{txtshipmentno.Text}' and ItemCode='{productcode}'");
                    }
                    XtraMessageBox.Show("Successfully Updated.");
                }
            }
            else
            {
                if (gridView1.RowCount > 0)
                {
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        string costperkg = gridView1.GetRowCellValue(i, "Cost").ToString();
                        string productcode = gridView1.GetRowCellValue(i, "ProductCode").ToString();
                        string desc = gridView1.GetRowCellValue(i, "Description").ToString();
                        Database.ExecuteQuery($"INSERT INTO dbo.TempCosting(ShipmentNo,ItemCode,Parts,CostPerKg) VALUES ('{txtshipmentno.Text}','{productcode}','{desc}','{costperkg}')  ");
                    }
                    XtraMessageBox.Show("Successfully inserted.");
                }
            }
            Database.ExecuteQuery("UPDATE a SET a.Cost=b.CostperKg " +
                "FROM dbo.Inventory a " +
                "INNER JOIN dbo.TempCosting b " +
                "On a.Product = b.ItemCode " +
                "and a.ShipmentNo = b.ShipmentNo " +
                $"WHERE a.Branch = '{Login.assignedBranch}' and a.isWarehouse = 0 ", "All Items of this ShipmentNo are Successfully Executed.");
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
           
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowCellStyle_1(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Cost")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
        }

        private void gridView1_ShowingEditor_1(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Cost")
                e.Cancel = true;
        }
    }
}