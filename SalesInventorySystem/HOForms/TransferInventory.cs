using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;

namespace SalesInventorySystem.HOForms
{
    public partial class TransferInventory : Form
    {

        DataTable table;
        string prodnum, desc, qty,seqnum,barcode,datereceived;

        public TransferInventory()
        {
            InitializeComponent();
        }

        void loadGridview2()
        {
            table = new DataTable();
            table.Columns.Add("#");
            table.Columns.Add("Product");
            table.Columns.Add("DateReceived");
            table.Columns.Add("Barcode");
            table.Columns.Add("Description");
            table.Columns.Add("Quantity");
            gridControl2.DataSource = table;
        }

        void display()
        {
            
            string isBigblue = "0";
            if (txtfrom.Text == "BigBlue")
            {
                isBigblue = "0";
            }
            else
            {
                isBigblue = "1";
                btnExecuteTransfer.Visible = true;
            }
            Database.display("SELECT * FROM view_TransferInventory WHERE IsWarehouse='" + isBigblue + "' And Branch='"+Login.assignedBranch+"'", gridControl1, gridView1);
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                        }, 1);
            //  view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Available";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Available"];
            gridView1.GroupSummary.Add(ite);
            
        }

        private void txtfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridControl2.BeginUpdate();
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            display();
            gridControl2.EndUpdate();
          
        }

        void addNew()
        {
            //Database.display("Select SequenceNumber,Product,Description,Barcode,Quantity,Available,DateReceived FROM TempInventory WHERE BatchCode='" + txtbatchcode.Text + "' and isStock=1", gridControl1, gridView1);
      
            gridControl2.BeginUpdate();
            seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            prodnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            barcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quantity").ToString();
            qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            datereceived = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString();
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            DataRow newRow = table.NewRow();
            newRow["#"] = seqnum;
            newRow["Product"] = prodnum;
            newRow["DateReceived"] = datereceived;
            newRow["Barcode"] = barcode;
            newRow["Description"] = desc;
            newRow["Quantity"] = qty;
            table.Rows.Add(newRow);
            
            gridControl2.DataSource = table;

            gridView2.Columns["Quantity"].Summary.Clear();
            //gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}");
            gridControl2.EndUpdate();

            GridView view = gridControl2.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
           // new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView2.Columns["Quantity"];
            gridView2.GroupSummary.Add(ite);

            gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }
        void add()
        {
            gridControl2.BeginUpdate();
            seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            prodnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            barcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            datereceived = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString();
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            DataRow newRow = table.NewRow();
            newRow["#"] = seqnum;
            newRow["Product"] = prodnum;
            newRow["DateReceived"] = datereceived;
            newRow["Barcode"] = barcode;
            newRow["Description"] = desc;
            newRow["Quantity"] = qty;
            table.Rows.Add(newRow);

            gridControl2.DataSource = table;

            gridView2.Columns["Quantity"].Summary.Clear();
            //gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}");
            gridControl2.EndUpdate();

            GridView view = gridControl2.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
           // new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView2.Columns["Quantity"];
            gridView2.GroupSummary.Add(ite);

            gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }

        void addCom()
        {
            gridControl2.BeginUpdate();
         
            seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            prodnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            barcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();

            datereceived = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateReceived").ToString();
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            DataRow newRow = table.NewRow();
            newRow["#"] = seqnum;
            newRow["Product"] = prodnum;
            newRow["DateReceived"] = Convert.ToDateTime(datereceived).ToShortDateString();
            newRow["Barcode"] = barcode;
            newRow["Description"] = desc;
            
            newRow["Quantity"] = qty;
            table.Rows.Add(newRow);
          
            gridControl2.DataSource = table;
            gridView2.BestFitColumns();
            gridView2.Columns["Quantity"].Summary.Clear();
          // gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}"); //duplicate footer
            gridControl2.EndUpdate();

            GridView view = gridControl2.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
           // new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView2.Columns["Quantity"];
            gridView2.GroupSummary.Add(ite);

            gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }

        private void TransferInventory_Load(object sender, EventArgs e)
        {
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber");// IDGenerator.getTransferedNumber();
            loadGridview2();
            //table.Columns.Add("#");
            //table.Columns.Add("Product");
            //table.Columns.Add("DateReceived");
            //table.Columns.Add("Barcode");
            //table.Columns.Add("Description");
            //table.Columns.Add("Quantity");
            //Database.display("Select SequenceNumber,Product,Description,Barcode,Quantity,Available,DateReceived FROM TempInventory WHERE BatchCode='" + txtbatchcode.Text + "' and isStock=1", transinv.gridControl1, transinv.gridView1);
            
            displayProductCategory();
        }

        void displayProductCategory()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtsort);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl2.BeginUpdate();
            //if (txtfrom.Text=="BigBlue")
            //{
            //    add();
            //}
            //else
            //{
            //    addCom();
            //}
            addNew();
            gridControl2.EndUpdate();
            gridView1.DeleteSelectedRows();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            // gridView2.Columns[0].Visible = false;
            gridView2.Columns["#"].Visible = false;
            if (gridView2.RowCount <= 0)
            {
                XtraMessageBox.Show("No Data to print");
                return;
            }
            BigBlueTemplate2();
            //if (txtfrom.Text == "BigBlue")
            //{
            //    BigBlueTemplate();
            //}
            //else
            //{
                
            //    CommissaryTemplate();
            //}

        }
        void BigBlueTemplate2()
        {

            gridView2 = gridControl2.FocusedView as GridView;
            gridView2.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(gridView2.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            gridView2.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView2.Columns["Quantity"];
            gridView2.GroupSummary.Add(ite);
             
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString(); 

            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();
            xct.Landscape = false;
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);


            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = "Transfer to BigBlue";

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl2));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);


            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        void BigBlueTemplate()
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.CustomerRequest xct = new DevExReportTemplate.CustomerRequest();
 
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrdateneeded.Text = DateTime.Now.ToShortDateString();
            xct.xrrequestedby.Text = Login.Fullname;
            xct.xrdaterequest.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
            xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl2));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void CommissaryTemplate()
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.StorageReceivingForm xct = new DevExReportTemplate.StorageReceivingForm();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now); 

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl2));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void btnExecuteTransfer_Click(object sender, EventArgs e)
        {
            GridView view = gridControl2.FocusedView as GridView;
            view.SortInfo.Clear(); 

            if (gridView2.RowCount <= 0)
            {
                XtraMessageBox.Show("No Data to print");
                return;
            }
            else
            {
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("UPDATE Inventory SET IsWarehouse='0',QtyBigBlue=Available,Available=0 WHERE SequenceNumber='" + gridView2.GetRowCellValue(i, "#").ToString() + "'");
                    Database.ExecuteQuery("INSERT INTO InventoryTransferred VALUES('888','" + gridView2.GetRowCellValue(i, "Product").ToString() + "','" + gridView2.GetRowCellValue(i, "Description").ToString() + "','" + Convert.ToDateTime(gridView2.GetRowCellValue(i, "DateReceived").ToString()).ToShortDateString() + "','" + gridView2.GetRowCellValue(i, "Barcode").ToString() + "','" + gridView2.GetRowCellValue(i, "Quantity").ToString() + "','" + DateTime.Now.ToShortDateString() + "','0','" + textEdit1.Text + "','','" + Login.Fullname + "','Commissary','BigBlue')");
                }
                XtraMessageBox.Show("Success!");
                this.Dispose();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount <= 0)
            {
                XtraMessageBox.Show("No Data to print");
                return;
            }
            else
            {
                for (int i=0;i<=gridView2.RowCount-1;i++)
                {
                    Database.ExecuteQuery("UPDATE Inventory SET IsWarehouse='1' WHERE SequenceNumber='" + gridView2.GetRowCellValue(i, "SequenceNumber") + "'");
                }
                XtraMessageBox.Show("Success!");
                this.Dispose();
            }
        }

        private void txtsort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtfrom.Text == "")
            {
                return;
            }
            else
            {
                string isBigblue = "0";
                if (txtfrom.Text == "BigBlue")
                {
                    isBigblue = "0";
                }
                else
                {
                    isBigblue = "1";
                }
                Database.display("SELECT * FROM view_TransferInventory WHERE IsWarehouse='" + isBigblue + "' and SUBSTRING(Product,1,2) = '" + Classes.Product.getProductCategoryCode(txtsort.Text) + "' And Branch='888'", gridControl1, gridView1);
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl2, e.Location);
            }
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //seqnum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "#").ToString();
            //prodnum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Product").ToString();
            //desc = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Description").ToString();
            //barcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Barcode").ToString();
            //qty = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Quantity").ToString();
            ////qty = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Available").ToString();
            //datereceived = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DateReceived").ToString();
            
            //DataRow newRow = table.NewRow();
            //newRow["SequenceNumber"] = seqnum;
            //newRow["Product"] = prodnum;
            //newRow["Description"] = desc;
            //newRow["Barcode"] = barcode;
            //newRow["Quantity"] = qty;
            //newRow["Available"] = qty;
            //newRow["DateReceived"] = datereceived;
            //table.Rows.Add(newRow);

            ////Database.display("Select SequenceNumber,Product,Description,Barcode,Quantity,Available,DateReceived FROM TempInventory WHERE BatchCode='" + txtbatchcode.Text + "' and isStock=1", gridControl1, gridView1);
            //gridControl1.DataSource = table;

            gridView2.DeleteSelectedRows();
            //newRow["#"] = seqnum;
            //newRow["Product"] = prodnum;
            //newRow["DateReceived"] = datereceived;
            //newRow["Barcode"] = barcode;
            //newRow["Description"] = desc;
            //newRow["Quantity"] = qty;

        }
    }
}
