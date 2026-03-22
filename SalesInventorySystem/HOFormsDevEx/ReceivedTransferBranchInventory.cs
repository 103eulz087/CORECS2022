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
    public partial class ReceivedTransferBranchInventory : Form
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

            askdh.gridView1.Focus();
            askdh.ShowDialog(this);
            if (HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone == true)
            {
                display();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string pono;
            pono = gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "TransferNo").ToString();
            HOFormsDevEx.ReceivedTransferInventoryBatchMode askdh = new HOFormsDevEx.ReceivedTransferInventoryBatchMode();

            askdh.txtshipmentno.Text = pono;
            string query = "SELECT * FROM TransferInventoryDetails with(nolock) WHERE TransferNo='" + pono + "'  ";
            HelperFunction.ShowWaitAndDisplay(query, askdh.gridControlRcvd, askdh.gridViewRcvd, "Please wait", "Populating data into the database...");
            askdh.groupControl1.Visible = false;
            askdh.gridView1.Focus();
            askdh.ShowDialog(this);
            if (HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone == true)
            {
                display();
                HOFormsDevEx.ReceivedTransferInventoryBatchMode.isdone = false;
                askdh.Dispose();
            }
        }
    }
}
