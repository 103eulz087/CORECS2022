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
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem
{
    public partial class ViewShipmentDashboard : DevExpress.XtraEditors.XtraForm
    {
        public static string shipmentno,connectionused,tabtype="";
        public ViewShipmentDashboard()
        {
            InitializeComponent();
        }

        private void ViewShipmentDashboard_Load(object sender, EventArgs e)
        {
            display();
        }

        private void display()
        {
            filtertab();
        }

        private void filtertab()
        {
            if (tabControl1.SelectedTab.Equals(NewShipment))
            {
                Database.display("SELECT * FROM view_ShipmentOrder WHERE Status='FOR DELIVERY'", gridControl2, gridView2);
                //Database.display("SELECT * FROM POSUMMARY WHERE Status='FOR DELIVERY'", gridControl2, gridView2);
            }
            else if (tabControl1.SelectedTab.Equals(ExistingShipment))
            {
               // Database.display("SELECT * FROM view_ShipmentOrder WHERE Status='DELIVERED'", gridControl1, gridView1);
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
                AddInventory addinv = new AddInventory();
               // addinv.FormClosed += new FormClosedEventHandler(addinv_FormClosed);
                addinv.Show();
                if (AddInventory.isdone == true)
                {
                    filtertab();
                    AddInventory.isdone = false;
                    addinv.Dispose();
                }
            }
        }
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            //tabtype = "Delivered";
            //shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            //ViewOrderDetails voerord = new ViewOrderDetails();
            //voerord.Show();
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.ViewShipmentDashBoardDetails voerord = new HOForms.ViewShipmentDashBoardDetails();
            
            voerord.Show();
        }

        //void addinv_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //   // Database.display("SELECT * FROM view_ShipmentOrder WHERE Status='FOR DELIVERY'", gridControl2, gridView2);
        //    filtertab();
        //}

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

        private void uploadBatchInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

     

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
               // contextMenuStrip1.Items[0].Visible = false;
                contextMenuStrip1.Show(gridControl2, e.Location);
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
              
                contextMenuStrip2.Show(gridControl1, e.Location);
            }
        }

        private void printPalleteNumberSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Print Pallete Number");
            if (ok)
            {
                Barcode.PalletPrinting bprint = new Barcode.PalletPrinting();
                string shipmentno = bprint.lblshipmentno.Text.Trim();
                string palletno = bprint.lblpalletno.Text.Trim();
                string totalkilos = bprint.lbltotalkilos.Text.Trim();
                bprint.xrBarCode2.Text = shipmentno + "10" + totalkilos.Remove(2, 1);
                ReportPrintTool report = new ReportPrintTool(bprint);
                report.Print();
            }
            else
            {
                return;
            }
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectionused = "local";
            shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.UploadBatchInventory uploadbatch = new HOForms.UploadBatchInventory();
            //uploadbatch.FormClosed += new FormClosedEventHandler(uploadbatch_FormClosed);
            uploadbatch.ShowDialog(this);
        }

        private void manualInventoryEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            //shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            AddInventory addinv = new AddInventory();
            //addinv.FormClosed += new FormClosedEventHandler(addinv_FormClosed);
            addinv.ShowDialog(this);
            if (AddInventory.isdone == true)
            {
                filtertab();
                AddInventory.isdone = false;
                addinv.Dispose();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.ViewShipmentDashBoardDetails voerord = new HOForms.ViewShipmentDashBoardDetails();
            voerord.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                Database.display("SELECT * FROM ShipmentOrder WHERE Status='DELIVERED'  ", gridControl1, gridView1);
            else
                Database.display("SELECT * FROM ShipmentOrder WHERE OrderDate >='" + txtdatefrom.Text + "' AND OrderDate <= '" + txtdateto.Text + "' AND Status='DELIVERED'", gridControl1, gridView1);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtdatefrom.Enabled = false;
                txtdateto.Enabled = false;
            }
            else
            {
                txtdatefrom.Enabled = true;
                txtdateto.Enabled = true;
            }
        }

        private void localConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = gridView2.RowCount;
            if (gridView2.RowCount <= 0)
            {
                shipmentno = "";
            }
            else
            {
                shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            }
            connectionused = "local";
            HOForms.UploadBatchInventory uploadbatch = new HOForms.UploadBatchInventory();
            uploadbatch.ShowDialog(this);
            if (HOForms.UploadBatchInventory.isdone == true)
            {
                filtertab();
                HOForms.UploadBatchInventory.isdone = false;
                uploadbatch.Dispose();
            }
       
        }

        private void liveConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = gridView2.RowCount;
            if (gridView2.RowCount <= 0)
            {
                shipmentno = "";
            }
            else
            {
                shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            }
            connectionused = "live";
            HOForms.UploadBatchInventory uploadbatch = new HOForms.UploadBatchInventory();
           // uploadbatch.FormClosed += new FormClosedEventHandler(uploadbatch_FormClosed);
            uploadbatch.ShowDialog(this);
            if (HOForms.UploadBatchInventory.isdone == true)
            {
                filtertab();
                HOForms.UploadBatchInventory.isdone = false;
                uploadbatch.Dispose();
            }
        }

        
    }
}