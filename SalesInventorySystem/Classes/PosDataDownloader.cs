using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class PosDataDownloader
    {
        private readonly string _cloudConnString = "YOUR_CLOUD_CONNECTION_STRING";
        private readonly string _localConnString = "YOUR_LOCAL_CONNECTION_STRING";

        // =================================================================================
        // PUBLIC METHODS - These are beautifully simple because the helper does the work!
        // =================================================================================

        public async Task SyncAllReferenceDataAsync(string branchCode, IProgress<int> progress, IProgress<string> status)
        {
            // 1. Branches (Global - No Branch Filter)
            await SyncTableAsync("Branches", new[] { "BranchCode" }, new[] {
            "BranchCode", "BranchName", "Address", "SignatoryManager", "SignatoryCashier"
        }, null, progress, status);

            // 2. Customers (Filtered by Branch)
            await SyncTableAsync("Customers", new[] { "CustomerKey" }, new[] {
            "CustomerKey", "CustomerID", "CustomerName", "CustomerEmail", "CustomerContactNo",
            "CustomerAddress", "CustomerBirthDate", "CustomerCreditLimit", "BranchCode",
            "Term", "isActive", "DateAdded", "AddedBy", "UpdatedBy", "AccountOfficer", "TinNo"
        }, branchCode, progress, status);

            // 3. ProductCategory (Global)
            await SyncTableAsync("ProductCategory", new[] { "ProductCategoryID" }, new[] {
            "ProductCategoryID", "Description", "isVat", "PrinterID", "Port"
        }, null, progress, status);

            // 4. POSInfoDetails (Filtered by Branch)
            await SyncTableAsync("POSInfoDetails", new[] { "BranchCode", "MachineUsed" }, new[] {
            "BranchCode", "BusinessName", "BusinessAddress", "TINNo", "MachineUsed",
            "AccreditationNo", "DateIssued", "SerialNo", "RegTransactionNo", "DateApplication",
            "PermitNumber", "MINNo", "DatePermitStart", "DatePermitEnd"
        }, branchCode, progress, status);

            // 5. ProductType (Global)
            await SyncTableAsync("ProductType", new[] { "TypeCode" }, new[] {
            "TypeCode", "TypeDescription"
        }, null, progress, status);

            // 6. Users (Global)
            await SyncTableAsync("Users", new[] { "UserID" }, new[] {
            "UserID", "FullName", "Designation", "EmailAddress", "Password", "AssignedBranch",
            "isAdmin", "isGlobalOfficer", "isBranchOfficer", "isWarehouseOfficer", "isCashier",
            "isMaker", "isChecker", "isApprover", "isAccounting", "DateRegister",
            "LastUpdated", "CashEndLimit", "CashInLimit", "ReceivableLimit", "GLAccount"
        }, null, progress, status);

            // 7. UserMenuAccess (Global)
            await SyncTableAsync("UserMenuAccess", new[] { "UserID" }, new[] {
            "UserID", "isAdmin", "isSales", "isInventory", "isAccounting", "isHotel",
            "isPayroll", "isReporting", "isForwarding", "isClientDataSheet"
        }, null, progress, status);

            // 8. PRODUCTS (Filtered by Branch, Composite Primary Key)
            // We add this right here! Notice the primary keys are BranchCode AND ProductCode
            await SyncTableAsync("Products", new[] { "BranchCode", "ProductCode" }, new[] {
            "BranchCode", "ProductCode", "Description", "LongDescription",
            "LandingCost", "SellingPrice", "ProductCategoryCode",
            "Price1", "Price2", "Price3", "Price4",
            "isRegular", "isPrice1", "isPrice2", "isPrice3", "isPrice4", "isDiscount",
            "haveBarcode", "Barcode", "ReOrderLevel", "isVat", "ProdType"
        }, branchCode, progress, status);

            status?.Report("All reference data and products successfully synchronized!");
            progress?.Report(100);
        }

        // =================================================================================
        // THE ENGINE - This handles ALL tables dynamically!
        // =================================================================================
        private async Task SyncTableAsync(string tableName, string[] primaryKeys, string[] allColumns, string filterBranchCode, IProgress<int> progress, IProgress<string> status)
        {
            status?.Report($"Synchronizing {tableName}...");

            using (SqlConnection cloudConn = new SqlConnection(_cloudConnString))
            using (SqlConnection localConn = new SqlConnection(_localConnString))
            {
                await cloudConn.OpenAsync();
                await localConn.OpenAsync();

                using (SqlTransaction localTx = localConn.BeginTransaction())
                {
                    try
                    {
                        // 1. CREATE TEMP TABLE INSTANTLY
                        string tempTable = $"#Temp_{tableName}";
                        string createTempTable = $"SELECT TOP 0 * INTO {tempTable} FROM [dbo].[{tableName}];";
                        using (SqlCommand createCmd = new SqlCommand(createTempTable, localConn, localTx))
                        {
                            await createCmd.ExecuteNonQueryAsync();
                        }

                        // 2. BUILD CLOUD SELECT QUERY
                        string selectCloud = $"SELECT {string.Join(", ", allColumns.Select(c => $"[{c}]"))} FROM [dbo].[{tableName}]";
                        if (!string.IsNullOrEmpty(filterBranchCode))
                        {
                            // Assume the column is named BranchCode if we are filtering
                            selectCloud += " WHERE BranchCode = @BranchCode";
                        }

                        // 3. DOWNLOAD & BULK COPY
                        using (SqlCommand selectCmd = new SqlCommand(selectCloud, cloudConn))
                        {
                            if (!string.IsNullOrEmpty(filterBranchCode))
                                selectCmd.Parameters.AddWithValue("@BranchCode", filterBranchCode);

                            selectCmd.CommandTimeout = 120;

                            using (SqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(localConn, SqlBulkCopyOptions.Default, localTx))
                                {
                                    bulkCopy.DestinationTableName = tempTable;
                                    bulkCopy.BatchSize = 5000;
                                    bulkCopy.BulkCopyTimeout = 120;
                                    await bulkCopy.WriteToServerAsync(reader);
                                }
                            }
                        }

                        // 4. DYNAMICALLY BUILD THE MERGE STATEMENT
                        string mergeOn = string.Join(" AND ", primaryKeys.Select(pk => $"Target.[{pk}] = Source.[{pk}]"));

                        // Exclude Primary Keys from the UPDATE SET clause
                        var updateCols = allColumns.Where(c => !primaryKeys.Contains(c)).ToList();
                        string updateSet = updateCols.Any()
                            ? "WHEN MATCHED THEN UPDATE SET " + string.Join(", ", updateCols.Select(c => $"Target.[{c}] = Source.[{c}]"))
                            : ""; // If table ONLY has primary keys, don't update anything

                        string insertCols = string.Join(", ", allColumns.Select(c => $"[{c}]"));
                        string insertVals = string.Join(", ", allColumns.Select(c => $"Source.[{c}]"));

                        string deleteFilter = string.IsNullOrEmpty(filterBranchCode)
                            ? ""
                            : "AND Target.BranchCode = @BranchCode";

                        string mergeQuery = $@"
                        MERGE INTO [dbo].[{tableName}] AS Target
                        USING {tempTable} AS Source
                        ON {mergeOn}
                        
                        {updateSet}
                        
                        WHEN NOT MATCHED BY TARGET THEN 
                            INSERT ({insertCols}) VALUES ({insertVals})
                        
                        WHEN NOT MATCHED BY SOURCE {deleteFilter} THEN 
                            DELETE;
                    ";

                        // 5. EXECUTE MERGE
                        using (SqlCommand mergeCmd = new SqlCommand(mergeQuery, localConn, localTx))
                        {
                            if (!string.IsNullOrEmpty(filterBranchCode))
                                mergeCmd.Parameters.AddWithValue("@BranchCode", filterBranchCode);

                            mergeCmd.CommandTimeout = 120;
                            await mergeCmd.ExecuteNonQueryAsync();
                        }

                        localTx.Commit();
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
