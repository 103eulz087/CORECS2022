using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem
{
    public partial class ViewPrimalCutsInventory : DevExpress.XtraEditors.XtraForm
    {
        public ViewPrimalCutsInventory()
        {
            InitializeComponent();
        }

        private void ViewProductsInventory_Load(object sender, EventArgs e)
        {
            display();
        }

        private void display()
        {
            
            Database.display("SELECT * FROM view_PrimalCutInventory ", gridControl1, gridView1);
            gridView1.Columns[0].Visible = false;

            if(Convert.ToBoolean(Login.isglobalAdmin) == true)
            {

                gridView1.Columns["Cost"].Visible = true;
            }
            else
            {
                gridView1.Columns["Cost"].Visible = false;
            }

            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
           // new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending) 
            }, 2);
            //view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite);

            GridGroupSummaryItem ite1 = new GridGroupSummaryItem();
            ite1.FieldName = "Available";
            ite1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite1.ShowInGroupColumnFooter = gridView1.Columns["Available"];
            gridView1.GroupSummary.Add(ite1);

            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
            gridView1.Columns["Available"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Available", "{0:n2}");

        }

       

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsWarehouse"));
            bool checkStock = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsStock"));
            if (!check)
            {
                //e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Blue;
            }
            if (!checkStock)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "Available")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            //if (e.Column.FieldName == "Available")
            //{
            //    if (Convert.ToDouble(e.CellValue) <= 0)
            //    {
            //        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
            //if (e.Column.FieldName == "IsWarehouse")
            //{
            //    if (Convert.ToBoolean(e.CellValue) == false)
            //    {
            //        e.Appearance.BackColor = Color.Blue;
            //    }
            // }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
                if (Convert.ToBoolean(Login.isglobalWarehouseOfficer) == true && Convert.ToBoolean(Login.isglobalAdmin) == false)
                {
                    contextMenuStrip1.Items[0].Visible = false;
                }
            }
        }

        private void bigBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); // IDGenerator.getTransferedNumber();
            Database.ExecuteQuery("INSERT INTO InventoryTransferred VALUES ('888','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString() + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString() + "','" + Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString()).ToShortDateString() + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString() + "','" + DateTime.Now.ToShortDateString() + "','0','" + id + "','','"+Login.Fullname+ "','Commissary','BigBlue')");
            Database.ExecuteQuery("UPDATE Inventory SET IsWarehouse='0' WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'", "Items Successfully Transferred To BigBlue!");
            //display();
            //return;
        }

        private void expandAllGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void expandAllGroupsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gridView1.ExpandAllGroups();
        }

        private void hideGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            gridView1.GroupRowCollapsing +=  new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gridView1_GroupRowCollapsing);
        }

        void gridView1_GroupRowCollapsing(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = false;
        }

        private void reprintBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime petsa = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString());
                string newdate = petsa.ToShortDateString();
                Barcode.CarcassBarcodePrinting bprint = new Barcode.CarcassBarcodePrinting();
                bprint.lblshipmentno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
                //bprint.lblpalletnum.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PalletNo").ToString();
                //bprint.xrBarCode2.Text = lblshipmentno.Text + "1" + txtweight.Text.Remove(2, 1) + ctr.ToString();
                bprint.xrBarCode2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
                bprint.lblmanufdate.Text = newdate;
                bprint.lblxpirydate.Text = petsa.AddYears(1).ToShortDateString();
                bprint.lblprodtype.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
                bprint.lblseqnum.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
                bprint.lblactualweight.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
                ReportPrintTool report = new ReportPrintTool(bprint);
                report.Print();
                report.Dispose();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void refreshDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}