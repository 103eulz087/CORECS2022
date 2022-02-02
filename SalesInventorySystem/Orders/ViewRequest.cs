using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SalesInventorySystem
{
    public partial class ViewRequest : DevExpress.XtraEditors.XtraForm
    {
        public static string devno,brcode,pono,refno,refno1,stat1;
        public ViewRequest()
        {
            InitializeComponent();
        }

        private void ViewRequest_Load(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void tabFilter()
        {
            if (tabControl1.SelectedTab.Equals(tabPage1)) //FOR RECEIVING
            {
                Database.display("SELECT * FROM view_DeliverySummary WHERE Status='FOR DELIVERY' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1); //display for receiving
            }
            else if (tabControl1.SelectedTab.Equals(tabPage2)) //MY REQUEST
            {
                Database.display("SELECT * FROM view_POSummary WHERE BranchCode='" + Login.assignedBranch + "' AND DateAdded >= '"+datefrommyreq.Text+ "' and  DateAdded <= '" + datetomyreq.Text + "' ", gridControl2, gridView2); //display to show the status of your request
            }
            else if (tabControl1.SelectedTab.Equals(tabPage3)) //DELIVERED
            {
                //Database.display("SELECT * FROM ReceiveOrderSummary WHERE BranchCode='" + Login.assignedBranch + "' AND DateReceived >= '" + datefromdeliv.Text + "' and  DateReceived <= '" + datetodeliv.Text + "'", gridControl3, gridView3); //display for receiving
                Database.display("SELECT * FROM view_ReceivedBranchOrder WHERE BranchCode='" + Login.assignedBranch + "' AND DateReceived >= '" + datefromdeliv.Text + "' and  DateReceived <= '" + datetodeliv.Text + "'", gridControl3, gridView3); //display for receiving
            }
            else if (tabControl1.SelectedTab.Equals(tabPage4)) //REJECTED
            {
                Database.display("SELECT * FROM view_POSummary WHERE BranchCode='" + Login.assignedBranch + "' AND Status='REJECTED' AND DateApproved >= '" + datefromrej.Text + "' and  DateApproved <= '" + datetorej.Text + "'", gridControl4, gridView4); //display for receiving
            }
            else if (tabControl1.SelectedTab.Equals(tabPage5)) //FOR APPROVAL
            {
                Database.display("SELECT * FROM view_POSummary WHERE BranchCode='" + Login.assignedBranch + "' AND Status='FOR APPROVAL' AND DateAdded >= '" + datefromforapprv.Text + "' and  DateAdded <= '" + datetoforapprv.Text + "'", gridControl5, gridView5); //display for receiving
            }
        }

        void displayAll()
        {
            Database.display("SELECT * FROM DeliverySummary WHERE Status='FOR DELIVERY' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1); //display for receiving
            Database.display("SELECT * FROM view_POSummary WHERE BranchCode='" + Login.assignedBranch + "'", gridControl2, gridView2); //display to show the status of your request
            Database.display("SELECT * FROM view_ReceivedBranchOrder WHERE BranchCode='" + Login.assignedBranch + "'", gridControl3, gridView3); //display for receiving
            Database.display("SELECT * FROM view_POSummary WHERE BranchCode='" + Login.assignedBranch + "' AND Status='REJECTED'", gridControl4, gridView4); //display for receiving
            Database.display("SELECT * FROM view_POSummary WHERE BranchCode='" + Login.assignedBranch + "' AND Status='FOR APPROVAL'", gridControl5, gridView5); //display for receiving
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) //FOR RECEIVING
        {
            devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            refno1 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            stat1 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Status").ToString();
            brcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
          
            AddBranchInventoryFrm addbrnchfrm = new AddBranchInventoryFrm();
            //addbrnchfrm.FormClosed += new FormClosedEventHandler(Addbrnchfrm_FormClosed);
            addbrnchfrm.Show();
            addbrnchfrm.txtdevno.Text = devno;
            addbrnchfrm.txtpono.Text = refno1;
            Database.display("SELECT * FROM view_BranchOrderForReceiving WHERE DeliveryNo='" + devno + "'", addbrnchfrm.gridControl2, addbrnchfrm.gridView2);
            Database.display("SELECT ProductCode,Barcode,ProductName,Qty,SellingPrice,DateReceived,ReceivedBy FROM ReceivedOrderDetails WHERE DeliveryNo='" + devno + "'", addbrnchfrm.gridControl1, addbrnchfrm.gridView1);
            if(AddBranchInventoryFrm.isDone == true)
            {
                Database.display("SELECT * FROM view_DeliverySummary WHERE Status='FOR DELIVERY' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1); //display for receiving
                AddBranchInventoryFrm.isDone = false;
                addbrnchfrm.Dispose();
            }
          
        }

        //private void Addbrnchfrm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Database.display("SELECT * FROM view_DeliverySummary WHERE Status='FOR DELIVERY' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1); //display for receiving
        //}

        void porcv_FormClosed(object sender, FormClosedEventArgs e)
        {
            tabFilter();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            pono = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
           
            POShowOrderDetails poshow = new POShowOrderDetails();
            poshow.FormClosed += new FormClosedEventHandler(poshow_FormClosed);
            poshow.Show();
        }

        void poshow_FormClosed(object sender, FormClosedEventArgs e)
        {
           // displayAll();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void receivedInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            //devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            //AddBranchInventoryFrm addbrnchfrm = new AddBranchInventoryFrm();
            //addbrnchfrm.FormClosed += new FormClosedEventHandler(addbrnchfrm_FormClosed);
            //addbrnchfrm.Show();
            devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            refno1 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            stat1 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Status").ToString();
            brcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();

            AddBranchInventoryFrm addbrnchfrm = new AddBranchInventoryFrm();
            //addbrnchfrm.FormClosed += new FormClosedEventHandler(Addbrnchfrm_FormClosed);
            addbrnchfrm.Show();
            addbrnchfrm.txtdevno.Text = devno;
            addbrnchfrm.txtpono.Text = refno1;
            Database.display("SELECT * FROM view_BranchOrderForReceiving WHERE DeliveryNo='" + devno + "'", addbrnchfrm.gridControl2, addbrnchfrm.gridView2);
            Database.display("SELECT ProductCode,Barcode,ProductName,Qty,SellingPrice,DateReceived,ReceivedBy FROM ReceivedOrderDetails WHERE DeliveryNo='" + devno + "'", addbrnchfrm.gridControl1, addbrnchfrm.gridView1);
            if (AddBranchInventoryFrm.isDone == true)
            {
                Database.display("SELECT * FROM view_DeliverySummary WHERE Status='FOR DELIVERY' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1); //display for receiving
                AddBranchInventoryFrm.isDone = false;
                addbrnchfrm.Dispose();
            }
        }

        void addbrnchfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            displayAll();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
                devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
                AddBranchInventoryFrm addbrnchfrm = new AddBranchInventoryFrm();
                addbrnchfrm.FormClosed += new FormClosedEventHandler(addbrnchfrm_FormClosed);
                addbrnchfrm.Show();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabFilter();
        }


        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            pono = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "PONumber").ToString();
            Branches.ViewBranchRequestDetails viewbranchreq = new Branches.ViewBranchRequestDetails();
            viewbranchreq.Show();
            Database.display("SELECT * FROM view_PurchaseOrderDetails WHERE PONumber='" + pono + "' AND BranchCode='" + Login.assignedBranch + "'", viewbranchreq.gridControl2, viewbranchreq.gridView2);

            viewbranchreq.gridView2.Columns["Qty"].Summary.Clear();
            viewbranchreq.gridView2.Columns["Dispatched"].Summary.Clear();
            viewbranchreq.gridView2.Columns["Received"].Summary.Clear();

            viewbranchreq.gridView2.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0}");
            viewbranchreq.gridView2.Columns["Dispatched"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Dispatched", "{0}");
            viewbranchreq.gridView2.Columns["Received"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Received", "{0}");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datefromforapprv_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datetoforapprv_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datefrommyreq_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datetomyreq_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datefromdeliv_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datetodeliv_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datefromrej_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void datetorej_ValueChanged(object sender, EventArgs e)
        {
            tabFilter();
        }

        private void gridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(gridControl3, e.Location);
                // contextMenuStrip1.Items[2].Visible = false;
            }
        }

        private void viewTicketJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.ViewJournalTickets viewjrount = new HOForms.ViewJournalTickets();
            viewjrount.Show();
        }

        private void rejectInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            Database.ExecuteQuery("UPDATE DeliverySummary SET Status='REJECTED' WHERE DeliveryNo='"+devno+"' AND PONumber='"+pono+"' ");
            Database.ExecuteQuery("UPDATE DeliveryDetails SET Status='REJECTED' WHERE DeliveryNo='" + devno + "' AND PONumber='" + pono + "' ");
            Database.ExecuteQuery("UPDATE DeliverySubDetails SET Status='REJECTED' WHERE DeliveryNo='" + devno + "' AND PONumber='" + pono + "' ","Successfully Rejected!");
        }
    }
}