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

namespace SalesInventorySystem.HOForms
{
    public partial class ReturnInventory : Form
    {
        public ReturnInventory()
        {
            InitializeComponent();
        }

        private void ReturnInventory_Load(object sender, EventArgs e)
        {
            displayBatchBox();
        }

        void displayBatchBox()
        {
            Database.displayComboBoxItems("SELECT distinct BatchCode FROM Inventory ORDER BY BatchCode", "BatchCode", comboBox1);
        }

        void display()
        {
            Database.display("SELECT * FROM Inventory WHERE BatchCode='"+comboBox1.Text+"' and isSource='0' ",gridControl2,gridView2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl2, e.Location);
            }
        }

        private void returnInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Return Inventory?", "Return Inventory");
            if (ok)
            {
                ReturnInventoryItems();
                XtraMessageBox.Show("Success");
                display();
            }
            else
            {
                return;
            }
        }

        void ReturnInventoryItems()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string barcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Barcode").ToString();
                string batchcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BatchCode").ToString();
                string sequencenum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SequenceNumber").ToString();
                string shipnum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
                String query = "ReturnInventory";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbarcode", barcode);
                com.Parameters.AddWithValue("@parmbatchcode", batchcode);
                com.Parameters.AddWithValue("@parmshipnum", shipnum);
                com.Parameters.AddWithValue("@parmseqnum", sequencenum);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
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
    }
}
