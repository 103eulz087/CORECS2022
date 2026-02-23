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
                statusText?.Report("Batch POSZReading Transactions Uploaded Successfully!");
            }
        }
    }
}
