using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SalesInventorySystem.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReceivedTransferBranchInventory : XtraForm
    {
        public ReceivedTransferBranchInventory()
        {
            InitializeComponent();
        }

        private void btnforrcvng_Click(object sender, EventArgs e)
        {
            //Database.display("SELECT * FROM view_ForReceivingSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and Status='FOR DELIVERY' and EffectivityDate between '" + txtdatefromforrcvng.Text + "' and '" + txtdatetoforrcvng.Text + "'  ORDER BY PONumber DESC", gridControlForReceiving, gridViewForReceiving);
            string query = "SELECT * FROM view_ForReceivingBranchInventoryTransfer WHERE DestBranchCode='" + Login.assignedBranch + "' and Status='FOR DELIVERY' and CAST(DateAdded as date) between '" + txtdatefromforrcvng.Text + "' and  '" + txtdatetoforrcvng.Text + "'  ORDER BY TransferNo DESC ";
            HelperFunction.ShowWaitAndDisplay(query, gridControlForReceiving, gridViewForReceiving, "Please wait", "Populating data into the database...");
            gridViewForReceiving.Focus();
        }

        private void gridControlForReceiving_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripForReceiving.Show(gridControlForReceiving, e.Location);
        }
        void display()
        {
            if (tabMain.SelectedTabPage.Equals(tabForReceiving))
            {
                //Database.display("SELECT * FROM view_ForReceivingSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and Status='FOR DELIVERY' and EffectivityDate between '" + txtdatefromforrcvng.Text + "' and '" + txtdatetoforrcvng.Text + "' ORDER BY PONumber DESC", gridControlForReceiving, gridViewForReceiving);
                string query = "SELECT * FROM dbo.view_ForReceivingBranchInventoryTransfer WHERE DestBranchCode='" + Login.assignedBranch + "' and Status='PENDING' ORDER BY TransferNo DESC ";
                HelperFunction.ShowWaitAndDisplay(query, gridControlForReceiving, gridViewForReceiving, "Please wait", "Populating data into the database...");
                gridViewForReceiving.Focus();
            }
            else if (tabMain.SelectedTabPage.Equals(tabReceived))
            {
                //Database.display("SELECT * FROM view_ForReceivingSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and Status='FOR DELIVERY' and EffectivityDate between '" + txtdatefromforrcvng.Text + "' and '" + txtdatetoforrcvng.Text + "' ORDER BY PONumber DESC", gridControlForReceiving, gridViewForReceiving);
                string query = "SELECT * FROM dbo.view_ForReceivingBranchInventoryTransfer WHERE DestBranchCode='" + Login.assignedBranch + "' and Status='DELIVERED' ORDER BY TransferNo DESC ";
                HelperFunction.ShowWaitAndDisplay(query, gridControlMyReq, gridViewMyReq, "Please wait", "Populating data into the database...");
                gridViewForReceiving.Focus();
            }
        }
        private void showForReceivingItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pono;
            pono = gridViewForReceiving.GetRowCellValue(gridViewForReceiving.FocusedRowHandle, "TransferNo").ToString();
            HOFormsDevEx.ReceivedTransferInventoryBatchMode askdh = new HOFormsDevEx.ReceivedTransferInventoryBatchMode();

            askdh.txtshipmentno.Text = pono;
            string query = "SELECT ProductNo,ProductName,BarcodeNo,Cost,QtyDelivered,QtyDelivered as ActualQty FROM TransferInventoryDetails with(nolock) WHERE TransferNo='" + pono + "'  ";
            HelperFunction.ShowWaitAndDisplay(query, askdh.gridControlRcvd, askdh.gridViewRcvd, "Please wait", "Populating data into the database...");

            //askdh.gridView1.Focus();
            askdh.ShowDialog(this);
            if (HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone == true)
            {
                //display();
                HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone = false;
                askdh.Dispose();
            }
        }

        private void btnMyReq_Click(object sender, EventArgs e)
        {
            //Database.display("SELECT * FROM view_ForReceivingSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and Status='FOR DELIVERY' and EffectivityDate between '" + txtdatefromforrcvng.Text + "' and '" + txtdatetoforrcvng.Text + "'  ORDER BY PONumber DESC", gridControlForReceiving, gridViewForReceiving);
            string query = "SELECT * FROM view_ForReceivingBranchInventoryTransfer WHERE DestBranchCode='" + Login.assignedBranch + "' and Status='DELIVERED' and CAST(DateAdded as date) between '" + txtdatefromforrcvng.Text + "' and  '" + txtdatetoforrcvng.Text + "'  ORDER BY TransferNo DESC ";
            HelperFunction.ShowWaitAndDisplay(query, gridControlMyReq, gridViewMyReq, "Please wait", "Populating data into the database...");
            gridViewForReceiving.Focus();
        }

        private void gridControlMyReq_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripReceived.Show(gridControlMyReq, e.Location);
        }

        //private void toolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    string pono;
        //    pono = gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "TransferNo").ToString();
        //    HOFormsDevEx.ReceivedTransferInventoryBatchMode askdh = new HOFormsDevEx.ReceivedTransferInventoryBatchMode();

        //    askdh.txtshipmentno.Text = pono;
        //    //string query = "SELECT * FROM TransferInventoryDetails with(nolock) WHERE TransferNo='" + pono + "'  ";
        //    string query = $"SELECT * FROM dbo.funcview_ReceivedReturnedTransferInventory('{pono}')";
        //    HelperFunction.ShowWaitAndDisplay(query, askdh.gridControlRcvd, askdh.gridViewRcvd, "Please wait", "Populating data into the database...");
        //    askdh.groupControl1.Visible = false;
        //    //askdh.gridView1.Focus();

        //    askdh.ShowDialog(this);
        //    if (HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone == true)
        //    {
        //        display();
        //        HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone = false;
        //        askdh.Dispose();
        //    }
        //}
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string pono = gridViewMyReq.GetRowCellValue(
                gridViewMyReq.FocusedRowHandle, "TransferNo"
            ).ToString();

            HOFormsDevEx.ReceivedTransferInventoryBatchMode askdh =
                new HOFormsDevEx.ReceivedTransferInventoryBatchMode();

            // ✅ Create Context Menu
            ContextMenuStrip cms = new ContextMenuStrip();

            // ✅ Create Menu Item
            ToolStripMenuItem printItem = new ToolStripMenuItem("Print Report");

            // ✅ Attach Event (you said you’ll handle it)
            printItem.Click += (s, args) =>
            {
                // You can call your print method here later
                MessageBox.Show("Print Report clicked for " + pono);
            };

            // ✅ Add item to menu
            cms.Items.Add(printItem);

            // ✅ Assign to form (right-click anywhere on dialog)
            askdh.ContextMenuStrip = cms;
            printItem.Click += PrintReport_Click;
            //// ✅ Pass value if needed
            //askdh.transferNo = pono; // optional if your form has property
            askdh.txtshipmentno.Text = pono;
            //string query = "SELECT * FROM TransferInventoryDetails with(nolock) WHERE TransferNo='" + pono + "'  ";
            string query = $"SELECT * FROM dbo.funcview_ReceivedReturnedTransferInventory('{pono}')";
            HelperFunction.ShowWaitAndDisplay(query, askdh.gridControlRcvd, askdh.gridViewRcvd, "Please wait", "Populating data into the database...");
            askdh.groupControl1.Visible = false;
            //askdh.gridView1.Focus();

            askdh.ShowDialog(this);
            if (HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone == true)
            {
                display();
                HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone = false;
                askdh.Dispose();
            }
        }
        private void PrintReport_Click(object sender, EventArgs e)
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='ShipmentReport'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            //HEADER MASTER
            string pono = gridViewMyReq.GetRowCellValue(
                gridViewMyReq.FocusedRowHandle, "TransferNo"
            ).ToString();

            // Fix: getMultipleQuery returns Dictionary<string, object>, not string
            var supplierRow = Database.getMultipleQuery(
                "SELECT a.TransferNo,a.DateReceived,a.ReceivedBy,a.Remarks,a.SupplierName,b.Description as Category " +
                "FROM dbo.ReceiveTransferInventorySummary a " +
                "INNER JOIN dbo.ReceiveCategoryMaster b " +
                "ON a.ReceiveCategory=b.CategoryCode " +
                $"WHERE a.TransferNo='{pono}' ",
                "TransferNo,DateReceived,ReceivedBy,Remarks,SupplierName,Category"
            );

            string suppliername = supplierRow.ContainsKey("SupplierName") ? supplierRow["SupplierName"].ToString() : string.Empty;
            string transactionumber = supplierRow.ContainsKey("TransferNo") ? supplierRow["TransferNo"].ToString() : string.Empty;
            string returncategory = supplierRow.ContainsKey("Category") ? supplierRow["Category"].ToString() : string.Empty;
            string dateofreturn = supplierRow.ContainsKey("DateReceived") ? supplierRow["DateReceived"].ToString() : string.Empty;
            string remarks = supplierRow.ContainsKey("Remarks") ? supplierRow["Remarks"].ToString() : string.Empty;

            DevExReportTemplate.ShipmentReport xct = new DevExReportTemplate.ShipmentReport();


            HOFormsDevEx.ReceivedTransferInventoryBatchMode askdh =
                new HOFormsDevEx.ReceivedTransferInventoryBatchMode();
            string query = $"SELECT * FROM dbo.funcview_ReceivedReturnedTransferInventory('{pono}')";
            HelperFunction.ShowWaitAndDisplay(query, askdh.gridControlRcvd, askdh.gridViewRcvd, "Please wait", "Populating data into the database...");

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='ShipmentReport'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;

            //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 15, 100);

            xct.lblshipment.Text = BatchProcessMasterDevEx.shipmentno;
            xct.xrsuppliername.Text = suppliername;
            xct.xrinvoiceno.Text = BatchProcessMasterDevEx.invoinceno; 
            xct.preparedby.Text = Login.Fullname;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(askdh.gridControlRcvd));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);

            //gridView1.Columns["Cost"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

    }
}
