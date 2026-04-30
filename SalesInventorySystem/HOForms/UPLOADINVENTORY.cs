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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HOForms
{
    public partial class UPLOADINVENTORY : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public static string supplierid = "", shipmentno = "", branch="",invsourcestat="";
        public UPLOADINVENTORY()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        void loaddata()
        {
            SqlConnection con;
            if (HOFormsDevEx.ReceivedInventoryDevEx.inventorysource == "LOCAL")
            {
                invsourcestat = "LOCAL";
                //UploadBatchItems();
                UploadInventory();
            }
            else if (HOFormsDevEx.ReceivedInventoryDevEx.inventorysource == "LIVE")
            {
                invsourcestat = "LIVE";
                con = Database.getConnection();
            }
            else //if (HOForms.VIEWPO.inventorysource == "LOCALWITHOUTPO")
            {
                invsourcestat = "LOCALWITHOUTPO";
                UploadBatchItemsWithoutPO();
            }
            //sa cloud nani cya nga display
            Database.display("SELECT * FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' ORDER BY Product,SequenceNumber", gridControl1, gridView1);
        }

        private void UPLOADINVENTORY_Load(object sender, EventArgs e)
        {
            txtshipmentno.Text = shipmentno;
        }

        void upload()
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Upload!...");
                return;
            }
            else
            {
                finalupdate();
                XtraMessageBox.Show("Successfully Uploaded!...");
                isdone = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            
        }
        void UploadBatchItemsWithoutPO()
        {
            SqlConnection con = Database.getLocalConnection();
            con.Open();
            try
            {
                string query = "sp_UploadShipmentReceivedWithoutPO";
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

        void UploadBatchItems()
        {
            SqlConnection con = Database.getLocalConnection();
            con.Open();
            try
            {
                string query = "sp_UploadShipmentReceived";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
                com.ExecuteNonQuery();
                displayUploadedItems();
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

        void displayUploadedItems()
        {
            Database.display("SELECT * FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "'", gridControl1, gridView1);
        }

        void finalupdate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SP_UPLOADINVENTORY";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", shipmentno);
                com.Parameters.AddWithValue("@parmbranch", branch);
                com.Parameters.AddWithValue("@parmsupplierid", supplierid);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parmuploadby", Login.Fullname);
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
        async void UploadInventory()
        {
            // Reset your progress bar (assuming you have a control named progressBar1)
            progressBar1.Value = 0;
            progressBar1.Maximum = 100; // We use 0-100 percentage

            // 1. Create the Progress handlers. 
            // These run ON THE UI THREAD whenever the background thread calls .Report()
            IProgress<int> progressHandler = new Progress<int>(p =>
            {
                progressBar1.Value = Math.Min(p, 100);
            });

            IProgress<string> statusHandler = new Progress<string>(msg =>
            {
                lblProgress.Text = msg;
            });

            try
            {
                //DateTime transDate = DateTime.Today;
                string branchCode = Login.assignedBranch;
                string machineName = Environment.MachineName;

                PosDataUploader uploader = new PosDataUploader();

                statusHandler.Report("Starting upload process...");

                // 2. Pass the handlers into the background task!
                await Task.Run(async () =>
                {

                    ///////////////////////////////////////// NEW
                    statusHandler.Report("Starting Inventory upload...");
                    await uploader.UploadBatchTempInventoryAsync(txtshipmentno.Text, progressHandler, statusHandler);

                    //statusHandler.Report("Starting Sales Details upload...");
                    //await uploader.UploadTableToCloudAsync("BatchSalesDetails", "DateOrder", transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting Sales Transaction upload...");
                    //await uploader.UploadTableToCloudAsync("SalesTransactionSummary", "DateOpen", transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting POSZReading Transaction upload...");
                    //await uploader.UploadTableToCloudAsync("POSZReadingTransactions", "DateExecute", transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting Sales Discount upload...");
                    //await uploader.UploadTableToCloudAsync("SalesDiscount", "DateExecute", transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting POSSales Summary upload...");
                    //await uploader.UploadTableToCloudAsync("POSSalesSummary", "DateOrder", transDate, branchCode, machineName, progressHandler, statusHandler);

                    //statusHandler.Report("Starting CreditCard Transaction upload...");
                    //await uploader.UploadTableToCloudAsync("POSCreditCardTransactions", "DateAdded", transDate, branchCode, machineName, progressHandler, statusHandler);

                   
                });

                MessageBox.Show("All end-of-day data uploaded seamlessly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblProgress.Text = "Error during upload.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }
        private void btndone_Click(object sender, EventArgs e)
        {
            loaddata();
            //UploadInventory();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (invsourcestat != "LOCALWITHOUTPO")
            {
                upload();
            }
            else
            {
                //for (int i = 0; i <= gridView1.RowCount - 1; i++)
                //{
                //    Database.ExecuteQuery("INSERT INTO Inventory VALUES ('" + gridView1.GetRowCellValue(i, "Branch").ToString() + "','" + gridView1.GetRowCellValue(i, "ShipmentNo").ToString() + "','" + gridView1.GetRowCellValue(i, "PalletNo").ToString() + "','" + gridView1.GetRowCellValue(i, "BatchCode").ToString() + "','" + gridView1.GetRowCellValue(i, "DateReceived").ToString() + "','" + gridView1.GetRowCellValue(i, "Product").ToString() + "','" + gridView1.GetRowCellValue(i, "Description").ToString() + "','" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "','" + gridView1.GetRowCellValue(i, "TipWeight").ToString() + "','" + gridView1.GetRowCellValue(i, "Quantity").ToString() + "','" + gridView1.GetRowCellValue(i, "Cost").ToString() + "','" + gridView1.GetRowCellValue(i, "Available").ToString() + "',0,'" + gridView1.GetRowCellValue(i, "IsStock").ToString() + "','" + gridView1.GetRowCellValue(i, "IsVat").ToString() + "','" + gridView1.GetRowCellValue(i, "IsWarehouse").ToString() + "','" + gridView1.GetRowCellValue(i, "ReferenceCode").ToString() + "','" + gridView1.GetRowCellValue(i, "LastMovementDate").ToString() + "',0,1,0)");
                //}
                XtraMessageBox.Show("Inventory Without PO Successfully Uploaded!...");
                this.Dispose();
            }
            
        }
    }
}