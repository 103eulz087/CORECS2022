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

namespace SalesInventorySystem.POS
{
    public partial class POSUploadChecker : DevExpress.XtraEditors.XtraForm
    {
        public POSUploadChecker()
        {
            InitializeComponent();
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        void display()
        {
            progressBarControl1.Position = 20;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_POSUploadChecker";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmdatefrom", dateEdit1.Text); 
                com.Parameters.AddWithValue("@parmmachine", Environment.MachineName);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                com.ExecuteNonQuery();

                progressBarControl1.Position = 40;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
                progressBarControl1.Position = 100;
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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Status").ToString() != "SUCCESS")
                {
                    contextMenuStrip1.Show(gridControl1, e.Location);
                }
            }
        }


        //async void Reupload(string TableName)
        //{
        //    // Reset your progress bar (assuming you have a control named progressBar1)
        //    progressBar1.Value = 0;
        //    progressBar1.Maximum = 100; // We use 0-100 percentage

        //    // 1. Create the Progress handlers. 
        //    // These run ON THE UI THREAD whenever the background thread calls .Report()
        //    IProgress<int> progressHandler = new Progress<int>(p =>
        //    {
        //        progressBar1.Value = Math.Min(p, 100);
        //    });

        //    IProgress<string> statusHandler = new Progress<string>(msg =>
        //    {
        //        lblProgress.Text = msg;
        //    });

        //    try
        //    {

        //        string branchCode = Login.assignedBranch;
        //        string machineName = Environment.MachineName;
        //        string salesDate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransactionDate").ToString();
        //        DateTime transDate = Convert.ToDateTime(salesDate);
        //        PosDataUploader uploader = new PosDataUploader();

        //        statusHandler.Report("Starting upload process...");

        //        // 2. Pass the handlers into the background task!
        //        if (TableName == "BatchSalesSummary") {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting Sales Summary upload...");
        //                await uploader.UploadBatchSalesSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
        //            });
        //        }
        //        else if (TableName == "BatchSalesDetails")
        //        {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting Sales Details upload...");
        //                await uploader.UploadBatchSalesDetailsAsync(transDate, branchCode, machineName, progressHandler, statusHandler);

        //            });
        //        }
        //        else if(TableName == "SalesTransactionSummary(XREAD)")
        //        {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting Sales Transaction Summary upload...");
        //                await uploader.UploadBatchSalesTransactionSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
        //            });
        //        }
        //        else if (TableName == "POSZReadingTransactions(ZREAD)")
        //        {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting POSZReading Transaction upload...");
        //                await uploader.UploadBatchZReadingTransactionsAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
        //            });
        //        }
        //        else if (TableName == "SalesDiscount")
        //        {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting Sales Discount upload...");
        //                await uploader.UploadBatchSalesDiscountAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
        //            });
        //        }
        //        else if (TableName == "POSSalesSummary(GroupSales)")
        //        {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting POSSales Summary upload...");
        //                await uploader.UploadBatchPOSSalesSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
        //            });
        //        }
        //        else if (TableName == "POSCreditCardTransactions")
        //        {
        //            await Task.Run(async () =>
        //            {
        //                statusHandler.Report("Starting Credit Card Transaction upload...");
        //                await uploader.UploadPOSCreditCardTransactionAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
        //            });
        //        }

        //        MessageBox.Show("Successfully uploaded seamlessly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblProgress.Text = "Error during upload.";
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {

        //    }
        //}

        async void Reupload(string selectedUIString)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

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
                string branchCode = Login.assignedBranch;
                string machineName = Environment.MachineName;
                string salesDate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransactionDate").ToString();
                DateTime transDate = Convert.ToDateTime(salesDate);

                PosDataUploader uploader = new PosDataUploader();

                // 1. Map the UI text to the actual Database Table and Date Column
                string dbTableName = "";
                string dbDateColumn = "";
                string displayMessage = "";

                switch (selectedUIString)
                {
                    case "BatchSalesSummary":
                        dbTableName = "BatchSalesSummary";
                        dbDateColumn = "Transdate";
                        displayMessage = "Sales Summary";
                        break;
                    case "BatchSalesDetails":
                        dbTableName = "BatchSalesDetails";
                        dbDateColumn = "DateOrder";
                        displayMessage = "Sales Details";
                        break;
                    case "SalesTransactionSummary(XREAD)":
                        dbTableName = "SalesTransactionSummary";
                        dbDateColumn = "DateOpen";
                        displayMessage = "Sales Transaction Summary";
                        break;
                    case "POSZReadingTransactions(ZREAD)":
                        dbTableName = "POSZReadingTransactions";
                        dbDateColumn = "DateExecute";
                        displayMessage = "POSZReading Transaction";
                        break;
                    case "SalesDiscount":
                        dbTableName = "SalesDiscount";
                        dbDateColumn = "DateExecute";
                        displayMessage = "Sales Discount";
                        break;
                    case "POSSalesSummary(GroupSales)":
                        dbTableName = "POSSalesSummary";
                        dbDateColumn = "DateOrder";
                        displayMessage = "POSSales Summary";
                        break;
                    case "POSCreditCardTransactions":
                        dbTableName = "POSCreditCardTransactions";
                        dbDateColumn = "DateAdded";
                        displayMessage = "Credit Card Transaction";
                        break;
                    default:
                        MessageBox.Show("Unknown table selected.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Stop execution if nothing matches
                }

                statusHandler.Report($"Starting {displayMessage} upload...");

                // 2. Pass variables into a SINGLE background task
                await Task.Run(async () =>
                {
                    await uploader.UploadTableToCloudAsync(dbTableName, dbDateColumn, transDate, branchCode, machineName, progressHandler, statusHandler);
                });

                MessageBox.Show($"{displayMessage} successfully uploaded seamlessly!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblProgress.Text = "Error during upload.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable any buttons if you disabled them at the top!
            }
        }
        private void reuploadThisTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tablename = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TableName").ToString();
            //string mark = dateEdit1.Text;
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToShortDateString() + "')");
            //wla pa na endofday
            bool isOpen = Database.checkifExist($"SELECT TOP(1) BranchCode FROM dbo.POSEODMonitoring WHERE TransactionDate='{transdate}' " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}' " +
                $"AND isEndOfDay=0 ");
            if (isOpen && dateEdit1.Text == DateTime.Now.ToShortDateString())
            {
                XtraMessageBox.Show("This Transaction is still open you cannot reupload this.");
                return;
            }
            else
            {
                Reupload(tablename);
                //Database.ExecuteQuery("EXEC sp_REUploadPOSSalesSummary '" + dateEdit1.Text + "','" + Login.assignedBranch + "','" + tablename + "','" + Environment.MachineName + "' ", "SP Successfully Executed!!...");
                display();
            }  
        }

        private void POSUploadChecker_Load(object sender, EventArgs e)
        {
            progressBarControl1.Position = 0;
            dateEdit1.Text = DateTime.Today.ToShortDateString();
            //display();
        }


        public async Task<DataTable> GetUploadStatusWithoutLinkedServerAsync(string branchCode, DateTime dateFrom, string machineName)
        {
            // 1. Setup the DataTable structure exactly like your old #uploadtables
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("BranchCode", typeof(string));
            dtStatus.Columns.Add("MachineUsed", typeof(string));
            dtStatus.Columns.Add("TransactionDate", typeof(DateTime));
            dtStatus.Columns.Add("TableName", typeof(string));
            dtStatus.Columns.Add("LocalCtr", typeof(int));
            dtStatus.Columns.Add("CloudCtr", typeof(int));
            dtStatus.Columns.Add("Status", typeof(string));

            // 2. We use ONE ultra-fast SQL script that gets all 8 counts at once.
            // Notice we use >= @Start and < @End to use your indexes properly!
            // SELECT 'SalesIN(ManualEntry)', ISNULL(COUNT(*),0) FROM SalesIN WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND SalesDate >= @Start AND SalesDate < @End UNION ALL

            string countScript = @"
            DECLARE @Start DATETIME = @parmdatefrom;
            DECLARE @End DATETIME = DATEADD(DAY, 1, @parmdatefrom);

            SELECT 'BatchSalesSummary' AS TableName, ISNULL(COUNT(*),0) AS Ctr FROM BatchSalesSummary WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND TransDate >= @Start AND TransDate < @End UNION ALL
            SELECT 'BatchSalesDetails', ISNULL(COUNT(*),0) FROM BatchSalesDetails WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND DateOrder >= @Start AND DateOrder < @End UNION ALL
            SELECT 'SalesTransactionSummary(XREAD)', ISNULL(COUNT(*),0) FROM SalesTransactionSummary WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND DateOpen >= @Start AND DateOpen < @End UNION ALL
            SELECT 'POSZReadingTransactions(ZREAD)', ISNULL(COUNT(*),0) FROM POSZReadingTransactions WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND DateExecute >= @Start AND DateExecute < @End UNION ALL
            SELECT 'SalesDiscount', ISNULL(COUNT(*),0) FROM SalesDiscount WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND DateExecute >= @Start AND DateExecute < @End UNION ALL
            SELECT 'POSSalesSummary(GroupSales)', ISNULL(COUNT(*),0) FROM POSSalesSummary WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND DateOrder >= @Start AND DateOrder < @End UNION ALL
            SELECT 'POSCreditCardTransactions', ISNULL(COUNT(*),0) FROM POSCreditCardTransactions WITH(NOLOCK) WHERE BranchCode = @parmbranchcode AND MachineUsed = @parmmachine AND DateAdded >= @Start AND DateAdded < @End;
        ";

            // Dictionaries to hold our results in memory
            Dictionary<string, int> localCounts = new Dictionary<string, int>();
            Dictionary<string, int> cloudCounts = new Dictionary<string, int>();

            // 3. GET LOCAL COUNTS
            using (SqlConnection localConn = Database.getConnection())
            using (SqlCommand cmdLocal = new SqlCommand(countScript, localConn))
            {
                cmdLocal.Parameters.AddWithValue("@parmbranchcode", branchCode);
                cmdLocal.Parameters.AddWithValue("@parmmachine", machineName);
                cmdLocal.Parameters.AddWithValue("@parmdatefrom", dateFrom.Date);

                await localConn.OpenAsync();
                using (SqlDataReader reader = await cmdLocal.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        localCounts.Add(reader["TableName"].ToString(), Convert.ToInt32(reader["Ctr"]));
                    }
                }
            }

            // 4. GET CLOUD COUNTS (Independent connection, NO MSDTC!)
            try
            {
                using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
                using (SqlCommand cmdCloud = new SqlCommand(countScript, cloudConn))
                {
                    cmdCloud.Parameters.AddWithValue("@parmbranchcode", branchCode);
                    cmdCloud.Parameters.AddWithValue("@parmmachine", machineName);
                    cmdCloud.Parameters.AddWithValue("@parmdatefrom", dateFrom.Date);

                    // Setting a timeout just in case the internet is completely down
                    cmdCloud.CommandTimeout = 15;

                    await cloudConn.OpenAsync();
                    using (SqlDataReader reader = await cmdCloud.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cloudCounts.Add(reader["TableName"].ToString(), Convert.ToInt32(reader["Ctr"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If the cloud is offline, we fill it with -1 so the user knows it failed
                ex.Message.ToString();
                foreach (var key in localCounts.Keys)
                {
                    cloudCounts[key] = -1;
                }
            }

            // 5. MERGE RESULTS INTO THE DATATABLE
            foreach (var local in localCounts)
            {
                string tableName = local.Key;
                int localCtr = local.Value;

                // Safely get the cloud count (defaults to 0 if something went wrong)
                int cloudCtr = cloudCounts.ContainsKey(tableName) ? cloudCounts[tableName] : 0;

                string status = (localCtr == cloudCtr && cloudCtr != -1) ? "SUCCESS" : "NEED TO REUPLOAD";
                if (cloudCtr == -1) status = "CLOUD OFFLINE";

                // Add the row to the DataTable
                dtStatus.Rows.Add(branchCode, machineName, dateFrom.Date, tableName, localCtr, cloudCtr, status);
            }

            return dtStatus;
        }


        async void generateAndUploadPending()
        {
            // 1. Check if the Grid has data
            if (gridControl1.DataSource is DataTable dtStatus && dtStatus.Rows.Count > 0)
            {
                try
                {
                    // Lock the UI
                    btnGenerate.Enabled = false;
                     
                    progressBar1.Value = 0;

                    // Setup our progress handlers (same as we built before)
                    IProgress<int> progressHandler = new Progress<int>(p =>
                    {
                        progressBar1.Value = Math.Min(p, 100);
                    });
                    IProgress<string> statusHandler = new Progress<string>(msg =>
                    {
                        lblProgress.Text = msg;
                    });

                    PosDataUploader uploader = new PosDataUploader();
                    bool performedUpload = false;

                    // Grab the parameters from the first row (since they are the same for all)
                    string branchCode = dtStatus.Rows[0]["BranchCode"].ToString();
                    DateTime transDate = Convert.ToDateTime(dtStatus.Rows[0]["TransactionDate"]);
                    string machineName = dtStatus.Rows[0]["MachineUsed"].ToString();

                    // 2. Loop through the underlying DataTable
                    foreach (DataRow row in dtStatus.Rows)
                    {
                        string status = row["Status"].ToString();
                        string tableName = row["TableName"].ToString();

                        // 3. ONLY process tables that need it
                        if (status == "NEED TO REUPLOAD")
                        {
                            performedUpload = true;
                            statusHandler.Report($"Preparing to upload {tableName}...");

                            // Offload the heavy lifting to the background thread
                            await Task.Run(async () =>
                            {
                                // 4. Use a Switch statement to call the correct method
                                switch (tableName)
                                {
                                    case "BatchSalesSummary":
                                        await uploader.UploadBatchSalesSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    case "BatchSalesDetails":
                                        // You will create this method based on the Summary template
                                        await uploader.UploadBatchSalesDetailsAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    case "SalesTransactionSummary(XREAD)":
                                        await uploader.UploadBatchSalesTransactionSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    case "POSZReadingTransactions(ZREAD)":
                                        await uploader.UploadBatchZReadingTransactionsAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    case "SalesDiscount":
                                        await uploader.UploadBatchSalesDiscountAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    case "POSSalesSummary(GroupSales)":
                                        await uploader.UploadBatchPOSSalesSummaryAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    case "POSCreditCardTransactions":
                                        await uploader.UploadPOSCreditCardTransactionAsync(transDate, branchCode, machineName, progressHandler, statusHandler);
                                        break;

                                    // Add cases for the rest of your tables here...
                                    // case "POSZReadingTransactions(ZREAD)":
                                    // case "SalesDiscount":
                                    // case "POSSalesSummary(GroupSales)":
                                    // case "SalesIN(ManualEntry)":
                                    // case "POSCreditCardTransactions":

                                    default:
                                        statusHandler.Report($"No upload method configured for {tableName}");
                                        break;
                                }
                            });

                            // Reset progress bar for the next table
                            progressHandler.Report(0);
                        }
                    }

                    // 5. Wrap up
                    if (performedUpload)
                    {
                        statusHandler.Report("All pending uploads completed successfully!");
                        MessageBox.Show("Sync complete!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optional: Automatically click the "Check Status" button 
                        // to refresh the DevExpress grid and show all Green Checkmarks!
                        //display();
                    }
                    else
                    {
                        statusHandler.Report("Everything is already up to date.");
                        MessageBox.Show("No pending data to upload. Everything is already synced.", "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    lblProgress.Text = "Upload process encountered an error.";
                    MessageBox.Show("Upload failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Unlock the UI
                    btnGenerate.Enabled = true;
                    //btnCheckStatus.Enabled = true;
                    progressBar1.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Please check the status first before uploading.", "Action Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            
            progressBarControl1.Position = 0;
            btnGenerate.Enabled = false;
            //display();
            try
            {
                btnGenerate.Enabled = false;
                lblProgress.Text = "Checking cloud sync status...";

                // Grab the values from your UI
                string branchCode = Login.assignedBranch;
                DateTime transDate = Convert.ToDateTime(dateEdit1.Text);
                string machineName = Environment.MachineName;

                PosDataUploader uploader = new PosDataUploader();

                // Call the async method we just made
                DataTable dtResults = await GetUploadStatusWithoutLinkedServerAsync(branchCode, transDate, machineName);
               
                gridControl1.DataSource = dtResults;

                gridView1.BestFitColumns();

                generateAndUploadPending();
                // Clean up the UI

                lblProgress.Text = "Status check complete.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to check status: " + ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblProgress.Text = "Error checking status.";
            }
            finally
            {
                btnGenerate.Enabled = true;
            }
            btnGenerate.Enabled = true ;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            // Check if we are formatting the "Status" column and not a group row/header
            if (e.Column.FieldName == "Status" && e.ListSourceRowIndex != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string statusValue = e.Value?.ToString();

                if (statusValue == "SUCCESS")
                {
                    e.DisplayText = "✔ SYNCED";
                }
                else if (statusValue == "NEED TO REUPLOAD")
                {
                    e.DisplayText = "✖ PENDING";
                }
                else if (statusValue == "CLOUD OFFLINE")
                {
                    e.DisplayText = "⚠ OFFLINE";
                }
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // Only apply this style to the Status column
            if (e.Column.FieldName == "Status")
            {
                // Grab the raw underlying value from the row to check its status
                string statusValue = gridView1.GetRowCellValue(e.RowHandle, "Status")?.ToString();

                // Make the font Bold
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Bold);

                // Apply colors based on the raw value
                if (statusValue == "SUCCESS")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.Green;
                }
                else if (statusValue == "NEED TO REUPLOAD")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                }
                else if (statusValue == "CLOUD OFFLINE")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
                }
            }
        }
    }
}