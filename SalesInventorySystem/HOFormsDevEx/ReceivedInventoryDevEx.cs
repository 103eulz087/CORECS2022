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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReceivedInventoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string inventorysource = "";
        public ReceivedInventoryDevEx()
        {
            InitializeComponent();
        }

        private void ReceivedInventoryDevEx_Load(object sender, EventArgs e)
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
                Database.display("SELECT * FROM view_ReceivedShipment WHERE Status='FOR DELIVERY' ORDER BY ShipmentNo", gridControl2, gridView2);
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl2, e.Location);
        }

        private void manualInventoryEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           
        }

        private void localConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inventorysource = "LOCAL";
            HOForms.UPLOADINVENTORY uploadbatch = new HOForms.UPLOADINVENTORY();
            string shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            var rows = Database.getMultipleQuery("POSUMMARY", "ShipmentNo='" + shipmentno + "'", "SupplierID,BranchCode");
            string SupplierID = rows["SupplierID"].ToString();
            string BranchCode = rows["BranchCode"].ToString();
            HOForms.UPLOADINVENTORY.branch = BranchCode;//gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            HOForms.UPLOADINVENTORY.shipmentno = shipmentno;// gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.UPLOADINVENTORY.supplierid = SupplierID;// gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString();

            uploadbatch.ShowDialog(this);
            if (HOForms.UPLOADINVENTORY.isdone == true)
            {
                filtertab();
                HOForms.UPLOADINVENTORY.isdone = false;
                uploadbatch.Dispose();
            }
        }

        private void liveConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inventorysource = "LIVE";
            HOForms.UPLOADINVENTORY uploadbatch = new HOForms.UPLOADINVENTORY();

            HOForms.UPLOADINVENTORY.branch = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            HOForms.UPLOADINVENTORY.shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.UPLOADINVENTORY.supplierid = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString();

            uploadbatch.ShowDialog(this);
            if (HOForms.UPLOADINVENTORY.isdone == true)
            {
                filtertab();
                HOForms.UPLOADINVENTORY.isdone = false;
                uploadbatch.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                Database.display("SELECT * FROM view_ReceivedShipment WHERE Status='DELIVERED'  ", gridControl1, gridView1);
            else
                Database.display("SELECT * FROM view_ReceivedShipment WHERE OrderDate >='" + txtdatefrom.Text + "' AND OrderDate <= '" + txtdateto.Text + "' AND Status='DELIVERED'", gridControl1, gridView1);

        }

        private void fromLocalConnectionWithoutPOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inventorysource = "LOCALWITHOUTPO";
            HOForms.UPLOADINVENTORY uploadbatch = new HOForms.UPLOADINVENTORY();
            uploadbatch.txtshipmentno.Enabled = true;
            uploadbatch.txtshipmentno.ReadOnly = false;
            uploadbatch.ShowDialog(this);
            if (HOForms.UPLOADINVENTORY.isdone == true)
            {
                filtertab();
                HOForms.UPLOADINVENTORY.isdone = false;
                uploadbatch.Dispose();
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            string shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.ViewShipmentDashBoardDetails voerord = new HOForms.ViewShipmentDashBoardDetails();
            Database.display("SELECT ProductCategory,OrderCode,Description,Quantity FROM view_PODETAILS WHERE ShipmentNo='" + shipmentno + "'", voerord.gridControl1, voerord.gridView1);
            voerord.ShowDialog(this);
        }

        private void singleModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string shipmentno;
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            AddInventoryDevEx addinv = new AddInventoryDevEx();
            addinv.txtshipmentno.Text = shipmentno;
            Database.display("SELECT ProductCategory,OrderCode,Barcode,Description,Quantity FROM view_PODETAILS WHERE ShipmentNo='" + shipmentno + "'", addinv.gridControl2, addinv.gridView2);
            addinv.groupControl3.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierName").ToString();
            addinv.ShowDialog(this);
            if (AddInventoryDevEx.isdone == true)
            {
                filtertab();
                AddInventoryDevEx.isdone = false;
                addinv.Dispose();
            }
        }

        private void batchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string shipmentno;
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            AddInventoryDevExBatchMode addinv = new AddInventoryDevExBatchMode();
            addinv.txtshipmentno.Text = shipmentno;
            Database.display("SELECT ProductCategory,OrderCode,Description,Quantity,Cost FROM view_PODETAILS WHERE ShipmentNo='" + shipmentno + "'", addinv.gridControl1, addinv.gridView1);
            //addinv.gridView1.Columns["Cost"].Visible = false;
            addinv.ShowDialog(this);
            if (AddInventoryDevExBatchMode.isdone == true)
            {
                filtertab();
                AddInventoryDevExBatchMode.isdone = false;
                addinv.Dispose();
            }
        }
    }
}