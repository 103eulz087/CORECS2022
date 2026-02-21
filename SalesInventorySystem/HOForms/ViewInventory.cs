using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem
{
    public partial class ViewInventory : DevExpress.XtraEditors.XtraForm
    {
        public static string shipmentNo, itemNo,barcode,opt;
       
        public ViewInventory()
        {
            InitializeComponent();
        }

        private void ViewInventory_Load(object sender, EventArgs e)
        {
            display(); //dapat naa cya sa display sa grid view para mo display ang gridfooter summary

            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["SupplierName"],DevExpress.Data.ColumnSortOrder.Ascending),
                 new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 2);

            //GridGroupSummaryItem ite = new GridGroupSummaryItem();
            //ite.FieldName = "TipWeight";
            //ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ite.ShowInGroupColumnFooter = gridView1.Columns["TipWeight"];
            //gridView1.GroupSummary.Add(ite);

            GridGroupSummaryItem ite1 = new GridGroupSummaryItem();
            ite1.FieldName = "Quantity";
            ite1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite1.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite1);

           
                //gridView1.Columns["TipWeight"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TipWeight", "{0:n2}");
                gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");

      
           
            //txtshipment.Focus();
        }

      

        private void display()
        {
            //string query = "SELECT * FROM view_BigBlueInventoryDetails";
            //HelperFunction.ShowWaitAndDisplay(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
            //gridView1.Focus();

            Database.display("SELECT * FROM view_BigBlueInventoryDetails", gridControl1, gridView1);
            gridView1.Columns[0].Visible = false;
           
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
                contextMenuStrip1.Items[0].Visible = false;
                contextMenuStrip1.Items[1].Visible = false;
                contextMenuStrip1.Items[4].Visible = false;
                //if (Convert.ToBoolean(Login.isglobalWarehouseOfficer) == true && Convert.ToBoolean(Login.isglobalAdmin) == false)
                //{
                //    contextMenuStrip1.Items[4].Visible = false;
                //}
            }
        }

        private void processToPrimalCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shipmentNo = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            itemNo = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString();
            //AddPrimalCuts addpcuts = new AddPrimalCuts();
            //addpcuts.FormClosed += new FormClosedEventHandler(addpcuts_FormClosed);
            //addpcuts.Show();
        }

        void addpcuts_FormClosed(object sender, FormClosedEventArgs e)
        {
            display();
        }

        private void showPrimalCutItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            barcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            ViewPrimalCutItems bpricut = new ViewPrimalCutItems();
            bpricut.ShowDialog(this);
        }

        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "Status")
            //{
            //    if (e.CellValue == "CUT")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //}

            //if (e.Column.FieldName == "Available")
            //{
            //    if (Convert.ToDouble(e.CellValue) <= 0)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}


            GridView view = (GridView) sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsStock"));
            if (!check)
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "Available")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            //if (e.Column.FieldName == "IsStock")
            //{
            //    if (Convert.ToBoolean(e.CellValue) == false)
            //    {
            //        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
            //else if (e.Column.FieldName == "Available")
            //{
            //    if (Convert.ToDouble(e.CellValue) <= 0)
            //    {

            //        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Strikeout);
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

       

     
        private void reprintBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime petsa = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString());
                DateTime petsaExpired = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ExpiryDate").ToString());
                string newdate = petsa.ToShortDateString();
                string dateexpired = petsaExpired.ToShortDateString();
                Barcode.CarcassBarcodePrinting bprint = new Barcode.CarcassBarcodePrinting();
                bprint.lblshipmentno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
                bprint.lblpalletnum.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PalletNo").ToString();
                //bprint.xrBarCode2.Text = lblshipmentno.Text + "1" + txtweight.Text.Remove(2, 1) + ctr.ToString();
                bprint.xrBarCode2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
                bprint.lblmanufdate.Text = newdate;
                bprint.lblxpirydate.Text = dateexpired;//petsa.AddYears(1).ToShortDateString();
                bprint.lblprodtype.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
                bprint.lblseqnum.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            //    bprint.lbltipweight.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TipWeight").ToString();
                bprint.lblactualweight.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quantity").ToString();
                ReportPrintTool report = new ReportPrintTool(bprint);
                report.Print();
                report.Dispose();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

       

        private void transferToCommisaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber");// IDGenerator.getTransferedNumber();
            Database.ExecuteQuery("INSERT INTO InventoryTransferred VALUES('888','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString() + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString() + "','" + Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString()).ToShortDateString() + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quantity").ToString() + "','" + DateTime.Now.ToShortDateString() + "','1','" + id + "','','"+Login.Fullname+"','BigBllue','Commissary')");
            Database.ExecuteQuery("UPDATE Inventory SET IsWarehouse='1' WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'", "Items Successfully Transferred To Commissary!");   
        }

       

       
    }
}