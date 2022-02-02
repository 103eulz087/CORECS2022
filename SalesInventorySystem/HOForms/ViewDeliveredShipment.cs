using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class ViewDeliveredShipment : Form
    {
        public ViewDeliveredShipment()
        {
            InitializeComponent();
        }


        private void ViewDeliveredShipment_Load(object sender, EventArgs e)
        {
            //txtrefno.Text = IDGenerator.getReferenceNumber();

            txtrefno.Text = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            button1.Focus();
            display();
        }

        //private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    Database.ExecuteQuery("UPDATE ShipmentOrder set Status='PAID',DatePaid='" + DateTime.Now.ToString() + "' WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "' ", "Confirmed");
        //    this.Close();

        //    bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Close Order");
        //    if (ok)
        //    {
        //        add();
        //        this.Close();
        //    }
        //    return;

        //}

        private void display()
        {
            Database.display("SELECT * FROM OrderDetails WHERE ShipmentNo='" + ViewOrder.shipmentno + "'", gridControl2, gridView2);
        }

        private void add()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_ConfirmShipmentOrder";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmshipmentNo", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString());
            com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
            com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace.ToString());
            }
            finally
            {
                con.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Close Order");
            if (ok)
            {
                add();
                this.Close();
            }
            return;
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button1.PerformClick();
            }
        }

        private void ViewDeliveredShipment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                button1.PerformClick();
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl2, e.Location);
            }
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            btnrecalc.Visible = true;
        }

        private void btnrecalc_Click(object sender, EventArgs e)
        {   
            double orderdetailsqty = 0.0,totalprodcost=0.0,totalqty=0.0,totalcost=0.0,totalbutcherycost=0.0,totalfreightcost=0.0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                orderdetailsqty = Convert.ToDouble(gridView2.GetRowCellValue(i, "Quantity").ToString());
                totalprodcost = orderdetailsqty * Convert.ToDouble(gridView2.GetRowCellValue(i, "CostKg").ToString());
                totalqty += orderdetailsqty;
                totalcost += Convert.ToDouble(gridView2.GetRowCellValue(i, "TotalProductCost").ToString());
                totalbutcherycost += Convert.ToDouble(gridView2.GetRowCellValue(i, "ButcheryCost").ToString());
                totalfreightcost += Convert.ToDouble(gridView2.GetRowCellValue(i, "FreightCost").ToString());
                Database.ExecuteQuery("Update OrderDetails SET CostKg='" + gridView2.GetRowCellValue(i, "CostKg").ToString() + "', Quantity='" + gridView2.GetRowCellValue(i, "Quantity").ToString() + "',TotalProductCost='" + totalprodcost + "',ButcheryCost='" + gridView2.GetRowCellValue(i, "ButcheryCost").ToString() + "',FreightCost='" + gridView2.GetRowCellValue(i, "FreightCost").ToString() + "' WHERE SequenceNumber='" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "'");
            }
            Database.ExecuteQuery("Update ShipmentOrder SET TotalQty='" + totalqty + "',TotalCost='" + totalcost + "',TotalButcheryCost='"+totalbutcherycost+"',TotalFreightCost='"+totalfreightcost+"' WHERE ShipmentNo='" + ViewOrder.shipmentno + "' ");
            recalculate();
            this.Dispose();
        }

        void recalculate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spu_recalculateInvtoryCost";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranch", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmshipmentno", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            try
            {
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Successfully Updated!");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void showInventoryItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            string prodcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductCode").ToString();
            HOFormsDevEx.ViewDeliveredShipDetailsDevEx viewship = new HOFormsDevEx.ViewDeliveredShipDetailsDevEx();
            viewship.Show();
            Database.display("SELECT * FROM Inventory WHERE ShipmentNo='" + shipmentno + "' and isSource='1' AND Branch='" + Login.assignedBranch + "' AND Product='"+ prodcode + "' ", viewship.gridControl2, viewship.gridView2);
        }
    }
}
