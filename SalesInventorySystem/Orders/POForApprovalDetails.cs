using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Data.SqlClient;
using SalesInventorySystem.DevExReportTemplate;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem
{
    public partial class POForApprovalDetails : DevExpress.XtraEditors.XtraForm
    {
        public static string refernceno;
        public static bool isdone = false;
        public POForApprovalDetails()
        {
            InitializeComponent();
        }

        private void POForApprovalDetails_Load(object sender, EventArgs e)
        {
            //richTextBox1.Focus();
        

            if (POForApproval.menu == "approvedrequest")
            {
                btnadd.Visible = false;
                simpleButton9.Visible = false;
            }
            else
            {
                btnadd.Visible = true;
                simpleButton9.Visible = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            //Database.ExecuteQuery("UPDATE PurchaseOrderDetails SET Status='FOR DELIVERY' WHERE PONumber='" + POForApproval.refno + "'");
            Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='FOR DELIVERY' WHERE PONumber='" + POForApproval.refno + "'", "Success");
            this.Close();
        }

       
        private void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                XtraMessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }

            // Open the Preview window.
            grid.ShowPrintPreview();
        }

        private void PrintGrid(GridControl grid)
        {
            // Check whether the GridControl can be printed.
            if (!grid.IsPrintingAvailable)
            {
                XtraMessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }

            // Print.
            grid.Print();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime dt = DateTime.Now;
            refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            Database.ExecuteQuery("UPDATE PurchaseOrderDetails SET Status='APPROVED' WHERE PONumber='" + POForApproval.refno + "'");
            Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='APPROVED',ApprovedBy='" + Login.isglobalUserID + "',DateApproved='" +String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + POForApproval.refno + "'", "Success");
            this.Close();
            //POPreview prvew = new POPreview();
            //prvew.Show();
        }

      

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime dt = DateTime.Now;
             bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Reject this Transaction?", "Rejected!!");
             if (ok)
             {
                 refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
                 Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='REJECTED',ApprovedBy='" + Login.isglobalUserID + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + POForApproval.refno + "'", "Success");
                 this.Close();
             }
        }

      
        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
                contextMenuStrip1.Items[0].Visible = false;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            //string custname = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + POForApproval.refno + "'", "Customer");
            //string notes = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + POForApproval.refno + "'", "Notes");
            //string address = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerAddress");
            //string contactno = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerContactNo");
            //string requestedby = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + POForApproval.refno + "'", "RequestedBy");


            var row = Database.getMultipleQuery("PurchaseOrderSummary", "PONumber = '" + POForApproval.refno + "'", "Customer,Notes,RequestedBy,ApprovedBy,EffectivityDate");
            string custname = row["Customer"].ToString();
            var cust = Database.getMultipleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerAddress,CustomerContactNo");


            string notes = row["Notes"].ToString();
            string address = cust["CustomerAddress"].ToString();
            string contactno = cust["CustomerContactNo"].ToString();
            string requestedby = row["RequestedBy"].ToString();
            string approvedby = row["ApprovedBy"].ToString();
            string devdate = row["EffectivityDate"].ToString();


            PurchaseOrderRep xct = new PurchaseOrderRep();
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.lbldate.Text = DateTime.Now.ToShortDateString();
            xct.lblorderno.Text = POForApproval.refno;
            xct.lblcustname.Text = custname;
            //xct.lbladdress.Text = address;
            //xct.lblcontact.Text = contactno;
            xct.lblnote.Text = notes;
            xct.xrapprovedby.Text = requestedby;

            gridView1.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:n2}");
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
             //GridView view = sender as GridView;
             //if (view.GridControl.IsPrinting) // && e.Column.FieldName == "ReferenceNumber" && e.Column.FieldName == "DateRequested" && e.Column.FieldName == "Status")
             //{
             //    if (e.Column.FieldName == "ReferenceNumber")
             //    {
             //        e.Column.Visible = false;
             //    }
             //}
        }

        private void btnapprove_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                refernceno = POForApproval.refno;//gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString(); 
                Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='APPROVED',Remarks='"+richTextBox1.Text.Trim()+"',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                isdone = true;
                this.Close();
            }
        }

        private void btnreject_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Reject this Transaction?", "Rejected!!");
                if (ok)
                {
                    //refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString(); 
                    Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='REJECTED',Remarks='"+richTextBox1.Text.Trim()+"',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                    this.Close();
                }
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            //string custname = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Customer");
            //string notes = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Notes");
            //string address = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerAddress");
            //string contactno = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerContactNo");
            //string requestedby = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "RequestedBy");
            //string approvedby = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "ApprovedBy");
            //string devdate = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "EffectivityDate");


            var row = Database.getMultipleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Customer,Notes,RequestedBy,ApprovedBy,EffectivityDate");
            string custname = row["Customer"].ToString();
            var cust = Database.getMultipleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerAddress,CustomerContactNo");


            string notes = row["Notes"].ToString();
            string address = cust["CustomerAddress"].ToString();
            string contactno = cust["CustomerContactNo"].ToString();
            string requestedby = row["RequestedBy"].ToString();
            string approvedby = row["ApprovedBy"].ToString();
            string devdate = row["EffectivityDate"].ToString();


            PurchaseOrderRep xct = new PurchaseOrderRep();
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.lbldate.Text = DateTime.Now.ToShortDateString();
            xct.lblorderno.Text = txtpono.Text;
            xct.lblcustname.Text = custname;
            xct.xraddress.Text = address;
            xct.xrdevdate.Text = Convert.ToDateTime(devdate).ToShortDateString();
            //xct.lbladdress.Text = address;
            //xct.lblcontact.Text = contactno;
            xct.lblnote.Text = notes;
            xct.xrapprovedby.Text = approvedby;
            xct.xrrequestedby.Text = requestedby;

            this.gridView1.Columns["BranchCode"].Visible = false;
            this.gridView1.Columns["PONumber"].Visible = false;
            this.gridView1.Columns["Category"].Visible = false;
            this.gridView1.Columns["ProductCode"].Visible = false;
          //  this.gridView1.Columns["Dispatched"].Visible = false;
            this.gridView1.Columns["Received"].Visible = false;
            this.gridView1.Columns["DateRequested"].Visible = false;
            this.gridView1.Columns["EffectivityDate"].Visible = false;
            this.gridView1.Columns["Status"].Visible = false;
            this.gridView1.Columns["Customer"].Visible = false;
            this.gridView1.Columns["SeqNo"].Visible = false;
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void cancelThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Delete this Item?", "Delete Item");
            double qty=0.0;
            qty = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Qty").ToString());
            if (confirm)
            {
                Database.ExecuteQuery("DELETE FROM PurchaseOrderDetails Where SeqNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SeqNo").ToString() + "' and PONumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString() + "'");
                Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Qty=Qty-'" + qty + "' Where PONumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString() + "'", "Item Successfully Rejected!");
                //Database.display("SELECT * FROM PurchaseOrderDetails WHERE PONumber = '" + GlobalVariables.ponumber + "'", gridControl1, gridView1);
                gridView1.DeleteSelectedRows();
            }
        }

        private void printSTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printSts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printSts();

            
        }

        void printSts()
        {
            
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            gridView1.FocusedRowHandle = 2;
            DevExReportTemplate.StockOrderRep xct = new DevExReportTemplate.StockOrderRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
           
            var rowPO = Database.getMultipleQuery("PurchaseOrderSummary", "PONumber='" + txtpono.Text + "' ", "RequestedBy,BranchCode,EffectivityDate");
            string requestedby = rowPO["RequestedBy"].ToString();
            string brcode = rowPO["BranchCode"].ToString();
            string effecitivitydate = rowPO["EffectivityDate"].ToString();

            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + brcode + "'", "BranchName");

            xct.xrbranchname.Text = branchname;
            xct.xrdate.Text = Convert.ToDateTime(effecitivitydate).ToShortDateString();//dt.ToShortDateString();
            xct.xrrequestedby.Text = requestedby;//POForApproval.requestedBy;
            xct.xrpono.Text = txtpono.Text;

            //this.gridView1.Columns["BranchCode"].Visible = false;
            this.gridView1.Columns["PONumber"].Visible = false;
            this.gridView1.Columns["Category"].Visible = false;
            this.gridView1.Columns["ProductCode"].Visible = false;
            this.gridView1.Columns["Units"].Visible = false;
            //this.gridView1.Columns["DateRequested"].Visible = false;
            //this.gridView1.Columns["EffectivityDate"].Visible = false;
            //this.gridView1.Columns["Status"].Visible = false;
            this.gridView1.Columns["SeqNo"].Visible = false;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 9);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void addButton()
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                refernceno = POForApproval.refno;//gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString(); 
                Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='APPROVED',Remarks='" + richTextBox1.Text.Trim() + "',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                isdone = true;
                this.Close();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            addButton();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Reject this Transaction?", "Rejected!!");
                if (ok)
                {
                    //refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString(); 
                    Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET Status='REJECTED',Remarks='" + richTextBox1.Text.Trim() + "',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                    this.Close();
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            printSts();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            //string custname = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Customer");
            //string notes = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Notes");
            //string address = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerAddress");
            //string contactno = Database.getSingleQuery("Customers", "CustomerName = '" + custname + "'", "CustomerContactNo");
            //string requestedby = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "RequestedBy");
            //string approvedby = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "ApprovedBy");
            //string devdate = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "EffectivityDate");

            var row = Database.getMultipleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Customer,Notes,RequestedBy,ApprovedBy,EffectivityDate");
            string custkey = row["Customer"].ToString(); //this is the customer key
            var cust = Database.getMultipleQuery("Customers", "CustomerKey = '" + custkey + "'", "CustomerName,CustomerAddress,CustomerContactNo");

            string custname = cust["CustomerName"].ToString();
            string notes = row["Notes"].ToString();
            string address = cust["CustomerAddress"].ToString();
            string contactno = cust["CustomerContactNo"].ToString();
            string requestedby = row["RequestedBy"].ToString();
            string approvedby = row["ApprovedBy"].ToString();
            string devdate = row["EffectivityDate"].ToString();


            var rowz = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = rowz["Heading"].ToString();
            string imagewidth = rowz["ImageWidth"].ToString();
            string imageheight = rowz["ImageHeight"].ToString();
            string caption1 = rowz["Caption1"].ToString();
            string caption2 = rowz["Caption2"].ToString();

            PurchaseOrderRep xct = new PurchaseOrderRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));

            xct.xrcompanyname.Text = companyname;
            xct.xraddress1.Text = caption1;
            xct.xraddress2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.lbldate.Text = DateTime.Now.ToShortDateString();
            xct.lblorderno.Text = txtpono.Text;
            xct.lblcustname.Text = custname;
            xct.xraddress.Text = address;
            xct.xrdevdate.Text = Convert.ToDateTime(devdate).ToShortDateString();
            //xct.lbladdress.Text = address;
            //xct.lblcontact.Text = contactno;
            xct.lblnote.Text = notes;
            xct.xrapprovedby.Text = approvedby;
            xct.xrrequestedby.Text = requestedby;

            //this.gridView1.Columns["BranchCode"].Visible = false;
            this.gridView1.Columns["PONumber"].Visible = false;
            this.gridView1.Columns["Category"].Visible = false;
            this.gridView1.Columns["ProductCode"].Visible = false;
            //  this.gridView1.Columns["Dispatched"].Visible = false;
            //this.gridView1.Columns["Received"].Visible = false;
            //this.gridView1.Columns["DateRequested"].Visible = false;
            //this.gridView1.Columns["EffectivityDate"].Visible = false;
            //this.gridView1.Columns["Status"].Visible = false;
            //this.gridView1.Columns["Customer"].Visible = false;
            this.gridView1.Columns["SeqNo"].Visible = false;
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}