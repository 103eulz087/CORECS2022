using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class UploadBatchInventory : Form
    {
        public static bool isdone = false;
        bool iserror = false;
        public UploadBatchInventory()
        {
            InitializeComponent();
        }

        private void UploadBatchInventory_Load(object sender, EventArgs e)
        {
            //updatePallet();
            string shipno = "";
            if (ViewShipmentDashboard.shipmentno == "")
            {
                shipno = "";
            }
            else
            {
                shipno = ViewShipmentDashboard.shipmentno;
            }
            txtshipmentno.Text = shipno;
            //txtrefno.Text = IDGenerator.getReferenceNumber();
            txtrefno.Text = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            txtdestination.Text = "BigBlue";
            //display();
        }

        void display()
        {
            SqlConnection con;
            if (ViewShipmentDashboard.connectionused == "local")
            {
                UploadBatchItems();
                XtraMessageBox.Show("Successfully Transfer to Production!...");
            }
            else
            {
                con = Database.getConnection();
            }
            //sa cloud nani cya nga display
            Database.display("SELECT * FROM TempInventoryBatchUpload WHERE ShipmentNo='"+txtshipmentno.Text+"' ORDER BY Product,SequenceNumber", gridControl1, gridView1);
        }

        void addInventoryNewEulz()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            int ctr = gridView1.RowCount - 1;
            try
            {
                string query = "sp_ImportCarcassInventoryNewEulz";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmreceivedby", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmdestination", txtdestination.Text.Trim());
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
                finalupdate();
                isdone = true;
                ////checkBatchUpload();
                //bool mark = iserror;
                //if (iserror == true)
                //{
                //    display();
                //    return;
                //}
                //else
                //{
                   
                //}
                //this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void updatePallet() //wla sa ni cya gamita
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_UpdatePalletNo";
                SqlCommand com = new SqlCommand(query, con);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Pallet Number Automatically Generated");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            } 
        }
        void UploadBatchItems()
        {
            SqlConnection con = Database.getLocalConnection();
            con.Open();
            int ctr = gridView1.RowCount - 1;
            try
            {
                    string query = "sp_UploadShipmentReceived";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.CommandTimeout = 3600;
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
        void addInventoryOLD()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            int ctr = gridView1.RowCount - 1;
            try
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    string str = gridView1.GetRowCellValue(i, "Product").ToString();
                    string query = "sp_ImportCarcassInventory";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                    com.Parameters.AddWithValue("@parmfreightcost", txtfreightcost.Text);
                    com.Parameters.AddWithValue("@barcode", gridView1.GetRowCellValue(i, "Barcode").ToString());
                    com.Parameters.AddWithValue("@parmproductcode", gridView1.GetRowCellValue(i, "Product").ToString());
                    com.Parameters.AddWithValue("@parmproddesc", gridView1.GetRowCellValue(i, "Description").ToString());
                    com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                    com.Parameters.AddWithValue("@parmpalletno", gridView1.GetRowCellValue(i, "PalletNo").ToString());
                    com.Parameters.AddWithValue("@parmtipweight", gridView1.GetRowCellValue(i, "TipWeight").ToString());
                    com.Parameters.AddWithValue("@parmactualweight", gridView1.GetRowCellValue(i, "Quantity").ToString());
                    com.Parameters.AddWithValue("@parmreferenceCode", gridView1.GetRowCellValue(i, "ReferenceCode").ToString());
                    com.Parameters.AddWithValue("@parmreceivedby", Login.isglobalUserID);
                    com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                    com.Parameters.AddWithValue("@parmdestination", txtdestination.Text.Trim());
                    com.Parameters.AddWithValue("@ctr", ctr);
                    com.Parameters.AddWithValue("@parmcost", gridView1.GetRowCellValue(i, "Cost").ToString());
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                }
                
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

        void finalupdate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spu_postInventoryCarcass";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmbranch", Login.assignedBranch);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parminvoicedate", txtinvoicedate.Text);
                com.Parameters.AddWithValue("@parmduedate", txtduedate.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                //string query = "spu_postInventory";
                //SqlCommand com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                //com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                ////com.Parameters.AddWithValue("@parmfreightcost", txtfreightcost.Text);
                //com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                //com.Parameters.AddWithValue("@parmbranch", Login.assignedBranch);
                ////com.Parameters.AddWithValue("@parmdevtype", "FULL");
                //com.CommandType = CommandType.StoredProcedure;
                //com.CommandText = query;
                //com.ExecuteNonQuery();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                txtfreightcost.Enabled = true;
            else
                txtfreightcost.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                Thread.Sleep(100);
                // Report progress.
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBarControl1.EditValue = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //display();
            Database.display("SELECT * FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' ORDER BY Product,SequenceNumber", gridControl1, gridView1);

            XtraMessageBox.Show("Successfully Added!");
            this.Dispose();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        void checkBatchUpload()
        {
            SqlConnection con = Database.getLocalConnection();
            try
            {
                con.Open();
                string query = "sp_CheckBatchUploadedItems";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.Add("@parmiserror", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();

                iserror = Convert.ToBoolean(com.Parameters["@parmiserror"].Value.ToString());
                //bool mark = true;
                //mark = iserror;
                //string mar2k = "";
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

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            display();
        }
    }
}
