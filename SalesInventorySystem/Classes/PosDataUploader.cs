using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class PosDataUploader
    {
        //private readonly string _localConnString = "Server=localhost;Database=ITCOREPOSFORDEMO;Trusted_Connection=True;";
        //private readonly string _cloudConnString = "Server=ITCORE-APPS.COM,1882;Database=CORECSUPDATER;User Id=yourUser;Password=yourPassword;";

            //DYNAMICALLY UPLOAD
        public async Task UploadTableToCloudAsync(string tableName, string dateColumnName, DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            int maxRetries = 3;
            int currentTry = 0;

            while (currentTry < maxRetries)
            {
                try
                {
                    currentTry++;
                    if (currentTry > 1)
                        statusText?.Report($"Network drop detected. Retrying {tableName} ({currentTry}/{maxRetries})...");

                    using (SqlConnection localConn = Database.getConnection())
                    using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
                    {
                        await localConn.OpenAsync();
                        await cloudConn.OpenAsync();

                        // 1. DYNAMIC COUNT QUERY
                        int totalRows = 0;
                        string countQuery = $@"SELECT COUNT(*) FROM [dbo].[{tableName}] 
                                      WHERE BranchCode = @BranchCode 
                                      AND {dateColumnName} >= @StartDate AND {dateColumnName} < @EndDate 
                                      AND MachineUsed = @MachineName;";

                        using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                        {
                            countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                            countCmd.Parameters.AddWithValue("@StartDate", startDate);
                            countCmd.Parameters.AddWithValue("@EndDate", endDate);
                            countCmd.Parameters.AddWithValue("@MachineName", machineName);
                            totalRows = (int)await countCmd.ExecuteScalarAsync();
                        }

                        if (totalRows == 0)
                        {
                            statusText?.Report($"No data in {tableName} to upload.");
                            progress?.Report(100);
                            return;
                        }

                        // 2. EXECUTE CLOUD TRANSACTION
                        using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                        {
                            // A. Dynamic Remote Delete
                            string deleteQuery = $@"DELETE FROM [dbo].[{tableName}] 
                                            WHERE BranchCode = @BranchCode AND {dateColumnName} >= @StartDate 
                                            AND {dateColumnName} < @EndDate AND MachineUsed = @MachineName;";

                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                            {
                                deleteCmd.CommandTimeout = 120;
                                deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                                deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                                deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                                deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                                await deleteCmd.ExecuteNonQueryAsync();
                            }

                            // B. Dynamic Local Select
                            string selectQuery = $@"SELECT * FROM [dbo].[{tableName}] 
                                            WHERE BranchCode = @BranchCode AND {dateColumnName} >= @StartDate 
                                            AND {dateColumnName} < @EndDate AND MachineUsed = @MachineName;";

                            using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                            {
                                selectCmd.CommandTimeout = 120;
                                selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                                selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                                selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                                selectCmd.Parameters.AddWithValue("@MachineName", machineName);

                                using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                                {
                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                                    {
                                        bulkCopy.DestinationTableName = $"[dbo].[{tableName}]";
                                        bulkCopy.BatchSize = 1000;
                                        bulkCopy.BulkCopyTimeout = 300;

                                        bulkCopy.NotifyAfter = 50;
                                        bulkCopy.SqlRowsCopied += (sender, e) =>
                                        {
                                            int percent = (int)((e.RowsCopied * 100.0) / totalRows);
                                            progress?.Report(percent);
                                            statusText?.Report($"Uploading {tableName}: {e.RowsCopied} of {totalRows} records...");
                                        };

                                        await bulkCopy.WriteToServerAsync(reader);
                                    }
                                }
                            }
                            cloudTx.Commit();
                        }

                        // 3. Dynamic Local Update (Marking isUpload = 1)
                        string updateLocalQuery = $@"UPDATE [dbo].[{tableName}] SET isUpload = 1 
                                             WHERE BranchCode = @BranchCode AND {dateColumnName} >= @StartDate 
                                             AND {dateColumnName} < @EndDate AND MachineUsed = @MachineName;";
                        using (SqlCommand updateCmd = new SqlCommand(updateLocalQuery, localConn))
                        {
                            updateCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                            updateCmd.Parameters.AddWithValue("@StartDate", startDate);
                            updateCmd.Parameters.AddWithValue("@EndDate", endDate);
                            updateCmd.Parameters.AddWithValue("@MachineName", machineName);
                            await updateCmd.ExecuteNonQueryAsync();
                        }

                        progress?.Report(100);
                        statusText?.Report($"{tableName} Uploaded Successfully!");
                        return; // Break retry loop
                    }
                }
                catch (SqlException ex)
                {
                    if (currentTry >= maxRetries)
                        throw new Exception($"Failed to upload {tableName} after {maxRetries} attempts. Details: {ex.Message}");

                    await Task.Delay(2000);
                }
            }
        }
        public async Task UploadBatchTempInventoryAsync(string shipmentno, IProgress<int> progress, IProgress<string> statusText)
        {
            //DateTime startDate = transactionDate.Date;
            //DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection(@"Enzo\ConnSettingsLocal"))
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[TempInventoryBatchUpload] 
                                  WHERE ShipmentNo = @ShipmentNo;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    //countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@ShipmentNo", shipmentno);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[TempInventoryBatchUpload] WHERE ShipmentNo = @ShipmentNo;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        //deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@ShipmentNo", shipmentno);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[TempInventoryBatchUpload] WHERE ShipmentNo = @ShipmentNo;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        //selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@ShipmentNo", shipmentno);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[TempInventoryBatchUpload]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Inventory Uploaded Successfully!");
            }
        }
        public async Task UploadBatchSalesSummaryAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[BatchSalesSummary] 
                                  WHERE BranchCode = @BranchCode 
                                  AND Transdate >= @StartDate AND Transdate < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[BatchSalesSummary] WHERE BranchCode = @BranchCode AND TransDate >= @StartDate AND TransDate < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[BatchSalesSummary] WHERE BranchCode = @BranchCode AND Transdate >= @StartDate AND Transdate < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[BatchSalesSummary]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch Sales Summary Uploaded Successfully!");
            }
        }

        public async Task UploadBatchSalesDetailsAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[BatchSalesDetails] 
                                  WHERE BranchCode = @BranchCode 
                                  AND DateOrder >= @StartDate AND DateOrder < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[BatchSalesDetails] WHERE BranchCode = @BranchCode AND DateOrder >= @StartDate AND DateOrder < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[BatchSalesDetails] WHERE BranchCode = @BranchCode AND DateOrder >= @StartDate AND DateOrder < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[BatchSalesDetails]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch Sales Details Uploaded Successfully!");
            }
        }

        public async Task UploadBatchSalesTransactionSummaryAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[SalesTransactionSummary] 
                                  WHERE BranchCode = @BranchCode 
                                  AND DateOpen >= @StartDate AND DateOpen < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[SalesTransactionSummary] WHERE BranchCode = @BranchCode AND DateOpen >= @StartDate AND DateOpen < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[SalesTransactionSummary] WHERE BranchCode = @BranchCode AND DateOpen >= @StartDate AND DateOpen < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[SalesTransactionSummary]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch Sales Transaction Summary Uploaded Successfully!");
            }
        }

        public async Task UploadBatchZReadingTransactionsAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[POSZReadingTransactions] 
                                  WHERE BranchCode = @BranchCode 
                                  AND DateExecute >= @StartDate AND DateExecute < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[POSZReadingTransactions] WHERE BranchCode = @BranchCode AND DateExecute >= @StartDate AND DateExecute < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[POSZReadingTransactions] WHERE BranchCode = @BranchCode AND DateExecute >= @StartDate AND DateExecute < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[POSZReadingTransactions]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch POSZReading Transactions Uploaded Successfully!");
            }
        }

        public async Task UploadBatchSalesDiscountAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[SalesDiscount] 
                                  WHERE BranchCode = @BranchCode 
                                  AND DateExecute >= @StartDate AND DateExecute < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[SalesDiscount] WHERE BranchCode = @BranchCode AND DateExecute >= @StartDate AND DateExecute < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[SalesDiscount] WHERE BranchCode = @BranchCode AND DateExecute >= @StartDate AND DateExecute < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[SalesDiscount]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch POSZReading Transactions Uploaded Successfully!");
            }
        }

        public async Task UploadBatchPOSSalesSummaryAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[POSSalesSummary] 
                                  WHERE BranchCode = @BranchCode 
                                  AND DateOrder >= @StartDate AND DateOrder < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[POSSalesSummary] WHERE BranchCode = @BranchCode AND DateOrder >= @StartDate AND DateOrder < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[POSSalesSummary] WHERE BranchCode = @BranchCode AND DateOrder >= @StartDate AND DateOrder < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[POSSalesSummary]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch POSSalesSummary Transactions Uploaded Successfully!");
            }
        }

        public async Task UploadPOSCreditCardTransactionAsync(DateTime transactionDate, string branchCode, string machineName, IProgress<int> progress, IProgress<string> statusText)
        {
            DateTime startDate = transactionDate.Date;
            DateTime endDate = startDate.AddDays(1);

            using (SqlConnection localConn = Database.getConnection())
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            {
                await localConn.OpenAsync();
                await cloudConn.OpenAsync();

                // --- NEW: GET TOTAL ROWS FIRST ---
                int totalRows = 0;
                string countQuery = @"SELECT COUNT(*) FROM [dbo].[POSCreditCardTransactions] 
                                  WHERE BranchCode = @BranchCode 
                                  AND DateAdded >= @StartDate AND DateAdded < @EndDate 
                                  AND MachineUsed = @MachineName;";
                using (SqlCommand countCmd = new SqlCommand(countQuery, localConn))
                {
                    countCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                    countCmd.Parameters.AddWithValue("@StartDate", startDate);
                    countCmd.Parameters.AddWithValue("@EndDate", endDate);
                    countCmd.Parameters.AddWithValue("@MachineName", machineName);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No data to upload for this date.");
                    progress?.Report(100);
                    return; // Exit early!
                }
                // ---------------------------------

                using (SqlTransaction cloudTx = cloudConn.BeginTransaction())
                {
                    // 1. Remote Delete (Same as before)
                    string deleteQuery = "DELETE FROM [dbo].[POSCreditCardTransactions] WHERE BranchCode = @BranchCode AND DateAdded >= @StartDate AND DateAdded < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cloudConn, cloudTx))
                    {
                        // (Add parameters here like before...)
                        deleteCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        deleteCmd.Parameters.AddWithValue("@StartDate", startDate);
                        deleteCmd.Parameters.AddWithValue("@EndDate", endDate);
                        deleteCmd.Parameters.AddWithValue("@MachineName", machineName);
                        await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // 2. Local Select
                    string selectQuery = "SELECT * FROM [dbo].[POSCreditCardTransactions] WHERE BranchCode = @BranchCode AND DateAdded >= @StartDate AND DateAdded < @EndDate AND MachineUsed = @MachineName;";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, localConn))
                    {
                        // (Add parameters here like before...)
                        selectCmd.Parameters.AddWithValue("@BranchCode", branchCode);
                        selectCmd.Parameters.AddWithValue("@StartDate", startDate);
                        selectCmd.Parameters.AddWithValue("@EndDate", endDate);
                        selectCmd.Parameters.AddWithValue("@MachineName", machineName);
                        using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cloudConn, SqlBulkCopyOptions.Default, cloudTx))
                            {
                                bulkCopy.DestinationTableName = "[dbo].[POSCreditCardTransactions]";
                                bulkCopy.BatchSize = 1000;

                                // --- NEW: PROGRESS TRACKING ---
                                bulkCopy.NotifyAfter = 50; // Fire event every 50 rows
                                bulkCopy.SqlRowsCopied += (sender, e) =>
                                {
                                    // Calculate percentage (0 to 100)
                                    int percent = (int)((e.RowsCopied * 100.0) / totalRows);

                                    // Send the numbers safely back to the UI thread
                                    progress?.Report(percent);
                                    statusText?.Report($"Uploading {e.RowsCopied} of {totalRows} records...");
                                };
                                // ------------------------------

                                await bulkCopy.WriteToServerAsync(reader);
                            }
                        }
                    }
                    cloudTx.Commit();
                }

                // 3. Update Local Status (Same as before)
                // UPDATE [dbo].[BatchSalesSummary] SET isUpload = 1 ...

                // Send final completion status
                progress?.Report(100);
                statusText?.Report("Batch Credit Card Transactions Uploaded Successfully!");
            }
        }
    }
}
