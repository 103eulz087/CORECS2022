using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace SalesInventorySystem
{
    public partial class POForDelivery : DevExpress.XtraEditors.XtraForm
    {
        public POForDelivery()
        {
            InitializeComponent();
        }

        private void POForDelivery_Load(object sender, EventArgs e)
        {
            Database.display("select * FROM PurchaseOrderSummary WHERE Status = 'FOR DELIVERY'", gridControl1, gridView1);
        }

        private String getDeliveryNum()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string str = "";
            string query = "SELECT ID FROM DeliverIDGenerator";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    str = reader["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.StackTrace.ToString());
            }
            finally
            {
                con.Close();
            }
            return str;
        }

        private void save()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            double sum = 0.0, totalkilo = 0.0;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                string query = "sp_AddPurchaseOrder";
                SqlCommand com = new SqlCommand(query, con);
                // com.Parameters.AddWithValue("@idinc", gridView1.RowCount);
                com.Parameters.AddWithValue("@ponum", gridView1.GetRowCellValue(i, "PONumber"));
                com.Parameters.AddWithValue("@brcode", gridView1.GetRowCellValue(i, "BranchCode"));
                com.Parameters.AddWithValue("@prodcode", gridView1.GetRowCellValue(i, "ProductCode"));
                com.Parameters.AddWithValue("@prodname", gridView1.GetRowCellValue(i, "ProductName"));
                com.Parameters.AddWithValue("@unitprice", gridView1.GetRowCellValue(i, "UnitPrice"));
                com.Parameters.AddWithValue("@totalkg", gridView1.GetRowCellValue(i, "TotalKilos"));
                com.Parameters.AddWithValue("@totalamount", gridView1.GetRowCellValue(i, "TotalAmount"));
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                sum += Convert.ToDouble(gridView1.GetRowCellValue(i, "TotalAmount"));
                totalkilo += Convert.ToDouble(gridView1.GetRowCellValue(i, "TotalKilos"));
            }
            XtraMessageBox.Show("Successfully Added");
            Database.ExecuteQuery("INSERT INTO PurchaseOrderSummary VALUES('" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber") + "','" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode") + "','" + totalkilo + "','" + sum + "','OPEN','" + DateTime.Now.ToString() + "','EULZ')", "Done");
            Database.ExecuteQuery("UPDATE PONumberGenerator SET ID = ID+1", "Done");
            con.Close();
            this.Close();
        }

        private void generateDeliveryReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}