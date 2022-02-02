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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace SalesInventorySystem.Reporting
{
    public partial class BatchProcessMasterDevEx : DevExpress.XtraEditors.XtraForm
    {
        object value = null;
        public static string shipmentno = "",suppliername="",invoinceno="", supplierid = "";
        public BatchProcessMasterDevEx()
        {
            InitializeComponent();
        }

        private void BatchProcessMasterDevEx_Load(object sender, EventArgs e)
        {
            loadSupplier();
        }

        void loadSupplier()
        {
            Database.displaySearchlookupEdit("Select SupplierID,SupplierName FROM Supplier ORDER BY SupplierName ASC", txtsupp, "SupplierName", "SupplierName");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display();
        }

        private void txtsupp_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtsupp.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            value = view.GetRowCellValue(rowHandle, "SupplierID");
        }

        void display()
        {
            Database.display("SELECT ShipmentNo,SupplierID,InvoiceDate,InvoiceNo " +
                "FROM APACCOUNTS " +
                "WHERE SupplierID='" + value + "' " +
                "and InvoiceDate >='" + txtdate.Text + "' " +
                "and InvoiceDate <= '" + txtdateto.Text + "'", gridControl1, gridView1);
        }

        void displayShipmentReports(string shipno)
        {
            CarcassReports carrep = new CarcassReports();
            if (carrep.radgroup.Checked == true)
            {
                carrep.gridControl1.BeginUpdate();
                carrep.gridView1.GroupSummary.Clear();
                carrep.gridView1.Columns.Clear();
                carrep.gridControl1.DataSource = null;
                Database.display("SELECT DateReceived,Description,SUM(Quantity) as Quantity,Cost FROm TempInventoryBatchUpload WHERE ShipmentNo='" + BatchProcessMasterDevEx.shipmentno + "' and Branch='" + Login.assignedBranch + "' GROUP BY DateReceived,Description,Cost", carrep.gridControl1, carrep.gridView1);
                 GridView viewz = gridControl1.FocusedView as GridView;
                viewz.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(viewz.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
                viewz.ExpandAllGroups();
                
                GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
                ite11.FieldName = "Quantity";
                ite11.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                ite11.ShowInGroupColumnFooter = carrep.gridView1.Columns["Quantity"];
                carrep.gridView1.GroupSummary.Add(ite11);

                carrep.gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
                carrep.gridControl1.EndUpdate();
            }
            if (carrep.raddetailed.Checked == true)
            {
                carrep.gridControl1.BeginUpdate();
                carrep.gridView1.GroupSummary.Clear();
                carrep.gridView1.Columns.Clear();
                carrep.gridControl1.DataSource = null;
                Database.display("SELECT DateReceived,Barcode,PalletNo,Description,Quantity,Cost FROm TempInventoryBatchUpload WHERE ShipmentNo='" + shipno + "' and Branch='" + Login.assignedBranch + "' ORDER BY Description,PalletNo,Cost", carrep.gridControl1, carrep.gridView1);
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending),
                new GridColumnSortInfo(view.Columns["PalletNo"],DevExpress.Data.ColumnSortOrder.Ascending)
                },2);
                view.ExpandAllGroups();

                Classes.DevXGridViewSettings.ShowFooterTotal(carrep.gridView1, "Quantity");
                carrep.gridView1.BestFitColumns();
                carrep.gridControl1.EndUpdate();
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            supplierid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            invoinceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceNo").ToString();
          
            CarcassReports carrep = new CarcassReports();
            carrep.Show();
            if (carrep.radgroup.Checked == true)
            {
                carrep.gridControl1.BeginUpdate();
                carrep.gridView1.GroupSummary.Clear();
                carrep.gridView1.Columns.Clear();
                carrep.gridControl1.DataSource = null;
                Database.display("SELECT DateReceived,Description,SUM(Quantity) as Quantity,Cost FROm TempInventoryBatchUpload WHERE ShipmentNo='" + BatchProcessMasterDevEx.shipmentno + "' and Branch='" + Login.assignedBranch + "' and isSource=1 GROUP BY DateReceived,Description,Cost", carrep.gridControl1, carrep.gridView1);
                GridView viewz = carrep.gridControl1.FocusedView as GridView;
                viewz.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(viewz.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
                viewz.ExpandAllGroups();

                Classes.DevXGridViewSettings.ShowFooterTotal(carrep.gridView1, "Quantity");
                carrep.gridControl1.EndUpdate();
            }
            if (carrep.raddetailed.Checked == true)
            {
                carrep.gridControl1.BeginUpdate();
                carrep.gridView1.GroupSummary.Clear();
                carrep.gridView1.Columns.Clear();
                carrep.gridControl1.DataSource = null;
                Database.display("SELECT DateReceived,Barcode,PalletNo,Description,Quantity,FORMAT(Cost, 'N', 'en-us') as Cost,FORMAT((Quantity*Cost), 'N', 'en-us')  as TotalCost " +
                    "FROm TempInventoryBatchUpload " +
                    "WHERE ShipmentNo='" + shipmentno + "' " +
                    "and Branch='" + Login.assignedBranch + "' " +
                    "and isSource=1 ORDER BY Description,PalletNo,Cost", carrep.gridControl1, carrep.gridView1);
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending),
                new GridColumnSortInfo(view.Columns["PalletNo"],DevExpress.Data.ColumnSortOrder.Ascending)

                }, 2);
                view.ExpandAllGroups();

                Classes.DevXGridViewSettings.ShowFooterTotal(carrep.gridView1, "Quantity");
                Classes.DevXGridViewSettings.ShowFooterTotal(carrep.gridView1, "TotalCost");
                
                carrep.gridView1.BestFitColumns();
                carrep.gridControl1.EndUpdate();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}