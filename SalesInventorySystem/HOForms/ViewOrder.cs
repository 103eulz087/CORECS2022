using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class ViewOrder : Form
    {
        public static string shipmentno, refno,tabtype="",invoiceno,actualcost,suppliername,supplierid,invoicedate;
        
        public ViewOrder()
        {
            InitializeComponent();
        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {
            txtdatefrom.Text = DateTime.Now.ToShortDateString();
            txtdateto.Text = DateTime.Now.ToShortDateString();
            filtertab();
            loadColumns();
        }
        void loadColumns()
        {
            Database.displayComboBoxItems("SELECT * FROM view_ShipmentOrderColumns ORDER BY column_name ASC", "column_name", txtcols);
        }
        private void filtertab()
        {
            if (tabControl1.SelectedTab.Equals(forapproval))
            {
                Database.display("SELECT ShipmentNo,OrderDate,TargetDate,SupplierID,SupplierName,TotalOrder,TotalQty,TotalCost,OrderedBy,Remarks,Status FROM ShipmentOrder WHERE Status='FOR APPROVAL' and NonTrade='0'", gridControl2, gridView2);
                //Database.display("SELECT * FROM POSUMMARY WHERE Status='FOR APPROVAL' ", gridControl2, gridView2);
            }
            else if (tabControl1.SelectedTab.Equals(fordelivery))
            {
                Database.display("SELECT * FROM ShipmentOrder WHERE Status='FOR DELIVERY' and NonTrade='0'", gridControl1, gridView1);
            }
            else if (tabControl1.SelectedTab.Equals(delivered))
            {
                Database.display("SELECT * FROM ShipmentOrder WHERE Status='DELIVERED' AND OrderDate >= '"+txtdatefrom.Text+ "' AND OrderDate <= '" + txtdateto.Text+ "' and Status='Delivered' and NonTrade='0'", gridControl3, gridView3);
            }
            else if (tabControl1.SelectedTab.Equals(cancelled))
            {
                Database.display("SELECT * FROM ShipmentOrder WHERE Status='CANCELLED' and NonTrade='0'", gridControl4, gridView4);
            }
            else if (tabControl1.SelectedTab.Equals(paid))
            {
                Database.display("SELECT * FROM ShipmentOrder WHERE Status='PAID' and NonTrade='0'", gridControl5, gridView5);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            tabtype = "";
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            ViewOrderDetails voerord = new ViewOrderDetails();
            voerord.FormClosed += new FormClosedEventHandler(voerord_FormClosed);
            voerord.Show();
            //if (Convert.ToBoolean(Login.isglobalOfficer) != true || Convert.ToBoolean(Login.isglobalApprover) != true)
            //{
            //    button1.Enabled = false;
            //    button2.Enabled = false;
            //}
            //if(ViewOrder.tabtype == "Delivered")
            //{
            //    groupBox1.Visible = false;
            //}

            //Database.displayLocalGrid("SELECT ShipmentNo,ProductCode,Description,Quantity,Metrics,CostKg,TotalProductCost,ButcheryCost,FreightCost FROM OrderDetails WHERE ShipmentNo='" + ViewOrder.shipmentno + "' UNION ALL (Select ShipmentNo,ProductCode,Description,Quantity,Metrics,CostKg,TotalProductCost,ButcheryCost,FreightCost FROM (SELECT '' AS ShipmentNo,'' AS ProductCode,'' AS Description,SUM(Quantity) as Quantity,'' AS Metrics,SUM(CostKg) as CostKg,SUM(TotalProductCost) as TotalProductCost,SUM(ButcheryCost) as ButcheryCost,SUM(FreightCost) as FreightCost FROM OrderDetails WHERE ShipmentNo='" + ViewOrder.shipmentno + "') as dt  )", dataGridView1);
            //DataGridViewSettings.gridFooter(dataGridView1);
            //dataGridView1.Focus();
        }

        void voerord_FormClosed(object sender, FormClosedEventArgs e)
        {
            filtertab();
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            shipmentno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ShipmentNo").ToString();
            ViewDeliveredShipment viewdlship = new ViewDeliveredShipment();
            viewdlship.FormClosed += new FormClosedEventHandler(viewdlship_FormClosed);
            viewdlship.Show();
        }

        void viewdlship_FormClosed(object sender, FormClosedEventArgs e)
        {
            filtertab();
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tabtype = "";
                shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
                ViewOrderDetails voerord = new ViewOrderDetails();
                voerord.FormClosed += new FormClosedEventHandler(voerord_FormClosed);
                voerord.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                Database.display("SELECT * FROM view_ShipmentOrderDeliveredTab WHERE Status='DELIVERED'  ", gridControl3, gridView3);
            else  
                Database.display("SELECT * FROM view_ShipmentOrderDeliveredTab WHERE OrderDate >='" + txtdatefrom.Text+"' AND OrderDate <= '"+txtdateto.Text+"' AND Status='DELIVERED'", gridControl3, gridView3);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtdatefrom.Enabled = false;
                txtdateto.Enabled = false;
            }else
            {
                txtdatefrom.Enabled = true;
                txtdateto.Enabled = true;
            }
                
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuDeliveredG3.Show(gridControl1, e.Location);
            }
        }

        private void nONEToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.EditShipmentOrder edtnsh = new HOForms.EditShipmentOrder();
            edtnsh.Show();
            Database.display("SELECT * FROM ShipmentOrder WHERE ShipmentNo='" + shipmentno + "'", edtnsh.gridControl1, edtnsh.gridView1);
            Database.display("SELECT * FROM OrderDetails WHERE ShipmentNo='" + shipmentno + "'", edtnsh.gridControl2, edtnsh.gridView2);

            edtnsh.gridView2.Columns["Quantity"].Summary.Clear();
            edtnsh.gridView2.Columns["TotalProductCost"].Summary.Clear();
            edtnsh.gridView2.Columns["ButcheryCost"].Summary.Clear();
            edtnsh.gridView2.Columns["FreightCost"].Summary.Clear();

            edtnsh.gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}");
            edtnsh.gridView2.Columns["TotalProductCost"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalProductCost", "{0}");
            edtnsh.gridView2.Columns["ButcheryCost"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ButcheryCost", "{0}");
            edtnsh.gridView2.Columns["FreightCost"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FreightCost", "{0}");
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                shipmentno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ShipmentNo").ToString();
                ViewDeliveredShipment viewdlship = new ViewDeliveredShipment();
                viewdlship.FormClosed += new FormClosedEventHandler(viewdlship_FormClosed);
                viewdlship.Show();
            }
        }

       

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuForApprovalG2.Show(gridControl2, e.Location);
            }
        }

        private void showRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabtype = "";
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            ViewOrderDetails voerord = new ViewOrderDetails();
            voerord.FormClosed += new FormClosedEventHandler(voerord_FormClosed);
            voerord.Show();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shipmentno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ShipmentNo").ToString();
            ViewDeliveredShipment viewdlship = new ViewDeliveredShipment();
            viewdlship.FormClosed += new FormClosedEventHandler(viewdlship_FormClosed);
            viewdlship.Show();
        }

        private void addPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //supplierid = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SupplierID").ToString();
            //shipmentno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ShipmentNo").ToString();
            //invoiceno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "InvoiceNo").ToString();
            //actualcost = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ActualCost").ToString();
            //suppliername = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SupplierName").ToString();
            //invoicedate = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "InvoiceDate").ToString();
            //HOForms.AddShipmentPayment addship = new HOForms.AddShipmentPayment();
            //addship.Show();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            tabtype = "Delivered";
            shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            ViewOrderDetails voerord = new ViewOrderDetails();
            voerord.Show();
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuForDeliveryG1.Show(gridControl3, e.Location);
            }
        }
    }
}
