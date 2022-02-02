using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Reporting
{
    public partial class BatchProcessReports : Form
    {
      // double selectedsum=0.0;
        DataTable table;
        string total1, total2;
        public BatchProcessReports()
        {
            InitializeComponent();
        }

        private void BatchProcessReports_Load(object sender, EventArgs e)
        {
            //var column1 = gridView1.Columns["Quantity"];
            //var column2 = gridView2.Columns["Quantity"];
            //column1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            //column2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            // displayBatchBox();
            populateprodcat();
            //loadBatchCode();
            //display();
            //gridView1.Columns["TipWeight"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TipWeight", "{0:n2}");
            //gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
            //gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
            //display();
            //gridView1.Columns["Quantity"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //gridView1.Columns["Quantity"].SummaryItem.FieldName = "Quantity";
            //gridView1.Columns["Quantity"].SummaryItem.DisplayFormat = "{0:n2}";
            comboBox1.Text = BatchProcessMasterDevEx.shipmentno;

            //display();

            //displayCarcass();
            //displayOtherItems();
            
        }

        void populateprodcat()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtprodcat);
        }

        void loadBatchCode()
        {
            Database.displayComboBoxItems("SELECT DISTINCT(BatchCode) FROM Inventory ORDER BY BatchCode ASC", "BatchCode", comboBox1);
        }

        void loadgridview1()
        {
            table = new DataTable();

            table.Columns.Add("TipWeight");
            table.Columns.Add("Quantity");
           
            //table.Columns.Add("Cost");
            //table.Columns.Add("Available"); //UnitPrice
            //gridControl2.DataSource = table;
            gridControl1.DataSource = table;

        }

        void loadgridview2()
        {
            table = new DataTable();

            table.Columns.Add("TipWeight");
            table.Columns.Add("Quantity");

            //table.Columns.Add("Cost");
            //table.Columns.Add("Available"); //UnitPrice
            //gridControl2.DataSource = table;
            gridControl2.DataSource = table;

        }

      
        Double calculateCutLoss()
        {
            double cutloss = 0.0,mark1=0.0,mark2=0.0;
            mark1 = Math.Round(getSummary(gridView2), 2);
            mark2 = Math.Round(getSummary(gridView1), 2);
            cutloss = Math.Round(getSummary(gridView2),2) - Math.Round(getSummary(gridView1),2);
            return cutloss;
        }

        Double calculateCutLossUpdated()
        {
            double cutloss = 0.0;
            double totalcarcass = 0.0,totalconverteditems=0.0;
            for(int i=0;i<=gridView1.RowCount-1;i++) //GET TOTAL SUMMARY OF CARCASS
            {
                totalcarcass += Convert.ToDouble(gridView1.GetRowCellValue(i, "Quantity").ToString());
            }
            for (int j = 0; j <= gridView1.RowCount - 1; j++) //GET TOTAL SUMMARY OF CONVERTED ITEMS
            {
                totalconverteditems += Convert.ToDouble(gridView2.GetRowCellValue(j, "Quantity").ToString());
            }
            cutloss = totalconverteditems - totalcarcass; //CARCASS-CONVERTED
            return cutloss;
        }

        void displayBatchBox()
        {
            Database.displayComboBoxItems("SELECT distinct BatchCode FROM Inventory order by BatchCode ASC", "BatchCode", comboBox1);
            Database.displayComboBoxItems("SELECT distinct BatchCode FROM Inventory order by BatchCode ASC", "BatchCode", comboBox2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl1.BeginUpdate();
            gridControl2.BeginUpdate();
            displayCarcass();
            displayOtherItems();
            
            //display();
            
            //txtcutloss.Text = calculateCutLossUpdated().ToString();
         //   txtcutloss.Text = calculateCutLoss().ToString();
            label5.Text = getProcessedBy();
            gridControl2.EndUpdate();
            gridControl1.EndUpdate();
        }

        void display()
        {
            //gridView1.ClearSelection();
            //gridView2.ClearSelection();
            //gridControl1.DataSource = null;
            //gridControl2.DataSource = null;
            Database.display("SELECT  * FROM view_BatchProcessReports WHERE BatchCode='" + comboBox1.Text + "' AND isSource='1' and BatchCode='0' ", gridControl1, gridView1);
            Database.display("SELECT  * FROM view_BatchProcessReports WHERE BatchCode='" + comboBox1.Text + "' AND isSource='0' and BatchCode='0' ", gridControl2, gridView2);
            gridView1.BestFitColumns();
            gridView2.BestFitColumns();
           
        }

        void displayCarcass()
        {
            // Database.display("SELECT * FROM view_BatchProcessReports WHERE BatchCode BETWEEN '" + comboBox1.Text + "' AND '" + comboBox2.Text + "' AND ReferenceCode = ''", gridControl1, gridView1);
            Database.display("SELECT * FROM view_BatchProcessReports WHERE BatchCode = '" + comboBox1.Text + "' AND  isSource='1'", gridControl1, gridView1);
            gridView1.BestFitColumns();

            gridView1.Columns["TipWeight"].Summary.Clear();
            gridView1.Columns["Quantity"].Summary.Clear();
            gridView1.Columns["TipWeight"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TipWeight", "{0}");
            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}");
            var a = gridView1.Columns["Quantity"].SummaryItem.SummaryValue;
            total1 = a.ToString();
        }

        void displayOtherItems()
        {
            gridControl2.BeginUpdate();

           
            // Database.display("SELECT * FROM view_BatchProcessReports WHERE BatchCode BETWEEN'" + comboBox1.Text + "' AND '" + comboBox2.Text + "'  AND ReferenceCode <> ''", gridControl2, gridView2);
            Database.display("SELECT BatchCode,Barcode,PalletNo,DateReceived,Description,TipWeight,Quantity,Product FROM TempInventory WHERE BatchCode = '" + comboBox1.Text + "' AND isSource='0'", gridControl2, gridView2);
            gridView2.BestFitColumns();

            gridView2.Columns["Quantity"].Summary.Clear();
            gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}");
            var a = gridView2.Columns["Quantity"].SummaryItem.SummaryValue;
            total2 = a.ToString();
            double tots = 0.0;
            // tots = Convert.ToDouble(total1) - Convert.ToDouble(total2);//calculateCutLoss().ToString();
            tots = Convert.ToDouble(total2) - Convert.ToDouble(total1);//calculateCutLoss().ToString();
            txtcutloss.Text = tots.ToString();
            label5.Text = getProcessedBy();

            GridView view = gridControl2.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                        }, 1);
          //  view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView2.Columns["Quantity"];
            gridView2.GroupSummary.Add(ite);

            gridControl2.EndUpdate();

           
            // gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayCarcass();
            displayOtherItems();

            txtcutloss.Text = calculateCutLoss().ToString();
            label5.Text = getProcessedBy();
        }

        String getProcessedBy()
        {
            string names="";
            names = Database.getSingleQuery("InventoryLedger", "BatchCode='" + comboBox1.Text + "'", "ProcessedBy");
            return names;
        }

        double getSummary(GridView view)
        {
           
            double sum = 0.0;
            for (int i = 0; i <= view.RowCount - 1; i++)
            {
                sum += Convert.ToDouble(view.GetRowCellValue(i, "Quantity").ToString());
            }
            return sum;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
           
        }

        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            //if (e.Column != gridView1.Columns["BatchCode"]) return;
            // int value1 = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle1, e.Column));
            // int value2 = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle2, e.Column));
            //if(Math.Sign(value1) == Math.Sign(value2)) 
            //{
            //      e.Merge = true;
            //      e.Handled = true;
            //}
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridView1.OptionsView.AllowCellMerge = !gridView1.OptionsView.AllowCellMerge;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT DISTINCT(BatchCode) FROM Inventory WHERE SUBSTRING(Product,1,2)='"+Classes.Product.getProductCategoryCode(txtprodcat.Text)+"' ORDER BY BatchCode ASC", "BatchCode", comboBox1);
            Database.displayComboBoxItems("SELECT DISTINCT(Description) FROM Inventory WHERE BatchCode <> 0 and SUBSTRING(Product,1,2)='" + Classes.Product.getProductCategoryCode(txtprodcat.Text) + "' and isSource=1 ORDER BY Description ASC", "Description", txtproductname);

        }

        private void txtproductname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = Classes.Product.getProductCategoryCode(txtprodcat.Text);
            string mark = Classes.Product.getProductCode(txtproductname.Text, a);
            //Database.displayComboBoxItems("SELECT DISTINCT(BatchCode) FROM Inventory WHERE SUBSTRING(Product,1,2)='"+Classes.Product.getProductCategoryCode(txtprodcat.Text)+"' ORDER BY BatchCode ASC", "BatchCode", comboBox1);
            Database.displayComboBoxItems("SELECT DISTINCT(BatchCode) FROM Inventory WHERE Product='" + mark + "' ORDER BY BatchCode ASC", "BatchCode", comboBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GridControl[] grids = new GridControl[] { gridControl1, gridControl2 };



            PrintingSystem ps = new PrintingSystem();

            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();

            compositeLink.PrintingSystem = ps;



            foreach (GridControl grid in grids)
            {

                PrintableComponentLink link = new PrintableComponentLink();

                link.Component = grid;

                compositeLink.Links.Add(link);

            }


            string filepath = "C:\\MyFiles\\";
            string filename = txtproductname.Text+"_"+comboBox1.Text + ".xls";
            string file = filepath + filename;
            compositeLink.CreateDocument();
            compositeLink.ExportToXls(file);
            //compositeLink.ShowPreview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gridView2.ExpandAllGroups();
           // txtcutloss.Text = calculateCutLoss().ToString();
        }

        //private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
        //    var column1 = gridView1.Columns["Quantity"];
        //    switch(e.Action)
        //    {
        //        case CollectionChangeAction.Add:
        //            selectedsum += (double)gridView1.GetRowCellValue(e.ControllerRow, column1);
        //            break;
        //        case CollectionChangeAction.Remove:
        //            selectedsum -= (double)gridView1.GetRowCellValue(e.ControllerRow, column1);
        //            break;
        //        case CollectionChangeAction.Refresh:
        //            selectedsum = 0;
        //            foreach (var rowHandle in gridView1.GetSelectedRows())
        //                selectedsum += (double)gridView1.GetRowCellValue(rowHandle, column1);
        //            break;
        //    }
        //    gridView1.UpdateTotalSummary();
        //}

        //private void gridView1_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        //{
        //    var item = e.Item as GridColumnSummaryItem;
        //    if (item == null || item.FieldName != "Quantity")
        //        return;
        //    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
        //        e.TotalValue = selectedsum;
        //}
    }
}
