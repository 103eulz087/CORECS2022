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
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewDeliveredShipDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public ViewDeliveredShipDetailsDevEx()
        {
            InitializeComponent();
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
            btnupdate.Visible = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            double totalqty = 0.0;
            for (int i = 0; i <= gridView2.RowCount - 1; i++)
            {
                totalqty += Convert.ToDouble(gridView2.GetRowCellValue(i, "Quantity").ToString());
                Database.ExecuteQuery("UPDATE Inventory SET Quantity='" + gridView2.GetRowCellValue(i, "Quantity").ToString() + "',Available='" + gridView2.GetRowCellValue(i, "Quantity").ToString() + "' WHERE SequenceNumber='" + gridView2.GetRowCellValue(i, "SequenceNumber").ToString() + "' ");
            }
            recalculate();
            XtraMessageBox.Show("SUCCESSFULLY UPDATED!");
        }

        void recalculate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spu_recalculateInvtoryCost";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmshipmentno", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        void display()
        {
            Database.display("SELECT * FROM Inventory WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "' and isSource='1' AND Branch='" + Login.assignedBranch + "' AND Product='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Product").ToString() + "' ", gridControl2, gridView2); 
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM Inventory WHERE SequenceNumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SequenceNumber").ToString() + "' ", "Successfully Deleted");
            display();
        }
    }
}