using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class POSDownloadUpdates
    {
        public async Task DownloadProductUpdatesAsync(IProgress<int> progress, IProgress<string> statusText)
        {
            // Note: We swapped the roles. Cloud is now the Source, Local is Destination.
            using (SqlConnection cloudConn = Database.getConnection(@"AAITCRE\ConnSettingsServer"))
            using (SqlConnection localConn = Database.getConnection())
            {
                await cloudConn.OpenAsync();
                await localConn.OpenAsync();

                // 1. GET TOTAL ROWS TO DOWNLOAD (For the progress bar)
                // Ideally, your cloud table has a "LastModifiedDate" so you only pull NEW changes.
                // For this example, let's assume we pull everything modified in the last 3 days.
                DateTime cutoffDate = DateTime.Now.AddDays(-3);
                int totalRows = 0;

                string countQuery = "SELECT COUNT(*) FROM [dbo].[Products] WHERE BranchCode='888'";
                using (SqlCommand countCmd = new SqlCommand(countQuery, cloudConn))
                {
                    //countCmd.Parameters.AddWithValue("@Cutoff", cutoffDate);
                    totalRows = (int)await countCmd.ExecuteScalarAsync();
                }

                if (totalRows == 0)
                {
                    statusText?.Report("No new product updates found.");
                    progress?.Report(100);
                    return;
                }

                // We must use a local transaction so our #Temp table stays alive 
                // for both the BulkCopy and the Merge step.
                using (SqlTransaction localTx = localConn.BeginTransaction())
                {
                    try
                    {
                        // 2. CREATE LOCAL TEMP TABLE
                        string createTempTable = @"
                    CREATE TABLE #TempProducts (
                        [ProductCode] VARCHAR(20),
                        [Barcode] VARCHAR(35),
                        [Description] VARCHAR(75),
                        [Cost] DECIMAL(12,2),
                        [SellingPrice] DECIMAL(12,2)
                    );";
                        using (SqlCommand createCmd = new SqlCommand(createTempTable, localConn, localTx))
                        {
                            await createCmd.ExecuteNonQueryAsync();
                        }

                        // 3. READ FROM CLOUD
                        string selectCloud = "SELECT [ProductCode], [Barcode], [Description], [Cost], [SellingPrice] FROM [dbo].[Products] WHERE LastModified >= @Cutoff;";
                        using (SqlCommand selectCmd = new SqlCommand(selectCloud, cloudConn))
                        {
                            selectCmd.Parameters.AddWithValue("@Cutoff", cutoffDate);
                            using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                            {
                                // 4. BULK COPY INTO LOCAL TEMP TABLE
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(localConn, SqlBulkCopyOptions.Default, localTx))
                                {
                                    bulkCopy.DestinationTableName = "#TempProducts";
                                    bulkCopy.BatchSize = 1000;
                                    bulkCopy.NotifyAfter = 50;
                                    bulkCopy.SqlRowsCopied += (sender, e) =>
                                    {
                                        int percent = (int)((e.RowsCopied * 100.0) / totalRows);
                                        progress?.Report(percent);
                                        statusText?.Report($"Downloading {e.RowsCopied} of {totalRows} product updates...");
                                    };

                                    await bulkCopy.WriteToServerAsync(reader);
                                }
                            }
                        }

                        // 5. MERGE TEMP TABLE INTO REAL TABLE
                        statusText?.Report("Applying updates to local database...");
                        string mergeQuery = @"
                    MERGE INTO [dbo].[Products] AS Target
                    USING #TempProducts AS Source
                    ON Target.ProductCode = Source.ProductCode
                    
                    WHEN MATCHED THEN 
                        -- Item exists! Update the price and details.
                        UPDATE SET 
                            Target.Barcode = Source.Barcode,
                            Target.Description = Source.Description,
                            Target.Cost = Source.Cost,
                            Target.SellingPrice = Source.SellingPrice

                    WHEN NOT MATCHED BY TARGET THEN 
                        -- Item does not exist locally! Insert it.
                        INSERT ([ProductCode], [Barcode], [Description], [Cost], [SellingPrice])
                        VALUES (Source.ProductCode, Source.Barcode, Source.Description, Source.Cost, Source.SellingPrice);
                    ";

                        using (SqlCommand mergeCmd = new SqlCommand(mergeQuery, localConn, localTx))
                        {
                            // For massive tables, you might want to increase the command timeout
                            mergeCmd.CommandTimeout = 120;
                            await mergeCmd.ExecuteNonQueryAsync();
                        }

                        // Commit the transaction. The #Temp table is automatically destroyed by SQL Server.
                        localTx.Commit();

                        progress?.Report(100);
                        statusText?.Report("Product database is fully up to date!");
                    }
                    catch
                    {
                        localTx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
