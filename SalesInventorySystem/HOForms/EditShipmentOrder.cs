using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class EditShipmentOrder : Form
    {
        public EditShipmentOrder()
        {
            InitializeComponent();
        }

        private void EditShipmentOrder_Load(object sender, EventArgs e)
        {
            
        }

       void displayInvoiceSummary()
        {
           // Database.display
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        void display()
        {
            Database.display("SELECT * FROM OrderDetails WHERE ShipmentNo='" + ViewOrder.shipmentno + "'", gridControl2, gridView2);
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(gridControl2, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
        }

        private void deleteThisItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM OrderDetails WHERE SequenceNumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SequenceNumber").ToString() + "'", "Successfully Deleted");
            display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            savechanges();
            this.Dispose();
        }

        void savechanges()
        {
            int totalitem = 0;
            double totalqty = 0.0, cost=0.0, totalcost = 0.0, totalbutcherycost = 0.0, totalfreightcost = 0.0;//, totalactualcost = 0.0, totalactualkilos = 0.0;
            //double qty = 0.0, costkg = 0.0;
            totalitem = gridView2.RowCount;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                Database.ExecuteQuery("UPDATE OrderDetails SET Quantity='" + gridView2.GetRowCellValue(i, "Quantity").ToString() + "',CostKg='" + gridView2.GetRowCellValue(i, "CostKg").ToString() + "',ButcheryCost='" + gridView2.GetRowCellValue(i, "ButcheryCost").ToString() + "',FreightCost='" + gridView2.GetRowCellValue(i, "FreightCost").ToString() + "' WHERE SequenceNumber='" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "' ");
                Database.ExecuteQuery("UPDATE OrderDetails SET TotalProductCost=(Quantity*CostKg) WHERE SequenceNumber='" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "'");
            }
            display();
            for (int ii=0;ii<=gridView2.RowCount-1;ii++)
            {
                //qty = Convert.ToDouble(gridView2.GetRowCellValue(i, "Quantity").ToString());

                cost = Convert.ToDouble(gridView2.GetRowCellValue(ii, "Quantity").ToString()) * Convert.ToDouble(gridView2.GetRowCellValue(ii, "CostKg").ToString());
                totalqty += Convert.ToDouble(gridView2.GetRowCellValue(ii, "Quantity").ToString());
                totalcost += Convert.ToDouble(gridView2.GetRowCellValue(ii, "TotalProductCost").ToString());
                totalbutcherycost += Convert.ToDouble(gridView2.GetRowCellValue(ii, "ButcheryCost").ToString());
                totalfreightcost += Convert.ToDouble(gridView2.GetRowCellValue(ii, "FreightCost").ToString());
            }
            Database.ExecuteQuery("UPDATE ShipmentOrder SET TotalOrder='"+ totalitem+"',TotalQty='"+totalqty+"',TotalCost='"+totalcost+"',TotalButcheryCost='"+ totalbutcherycost + "',TotalFreightCost='"+ totalfreightcost + "' WHERE ShipmentNo='"+ViewOrder.shipmentno+"' ","Successfully Updated");
        }
    }
}
