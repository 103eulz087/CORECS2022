using System;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraEditors;

namespace SalesInventorySystem.Classes
{
    /// <summary>
    /// Service class for handling stock out operations
    /// </summary>
    public class StockOutService
    {
        /// <summary>
        /// Inserts a stock out item into the database using parameterized queries for security
        /// </summary>
        public static bool InsertStockOutItem(StockOutItem item, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (item == null)
            {
                errorMessage = "Stock out item cannot be null";
                return false;
            }

            if (!item.IsValid())
            {
                errorMessage = "Stock out item contains invalid data";
                return false;
            }

            SqlConnection con = Database.getConnection();
            con.Open();

            try
            {
                string query = @"INSERT INTO dbo.StockOutDetails 
                    (ID, BranchCode, DateReceived, ProductCode, Description, Barcode, 
                     Quantity, Cost, TotalCost, isVat, isDone, DateEncode, EncodeBy) 
                    VALUES 
                    (@batchID, @branchCode, @dateOut, @productCode, @description, 
                     @barcode, @quantity, @cost, @totalCost, @isVat, 0, @dateEncoded, @encodedBy)";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    // Add parameters - this prevents SQL injection
                    com.Parameters.AddWithValue("@batchID", item.BatchID);
                    com.Parameters.AddWithValue("@branchCode", item.BranchCode);
                    com.Parameters.AddWithValue("@dateOut", item.DateOut);
                    com.Parameters.AddWithValue("@productCode", item.ProductCode);
                    com.Parameters.AddWithValue("@description", item.Description);
                    com.Parameters.AddWithValue("@barcode", item.Barcode);
                    com.Parameters.AddWithValue("@quantity", item.Quantity);
                    com.Parameters.AddWithValue("@cost", item.Cost);
                    com.Parameters.AddWithValue("@totalCost", item.TotalCost);
                    com.Parameters.AddWithValue("@isVat", item.IsVat);
                    com.Parameters.AddWithValue("@dateEncoded", item.DateEncoded);
                    com.Parameters.AddWithValue("@encodedBy", item.EncodedBy);

                    com.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                errorMessage = "Database error: " + ex.Message;
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Deletes a stock out detail by batch ID, branch code, and product code
        /// </summary>
        public static bool DeleteStockOutItem(string batchID, string branchCode, string productCode, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(batchID) || string.IsNullOrWhiteSpace(branchCode) || string.IsNullOrWhiteSpace(productCode))
            {
                errorMessage = "Batch ID, Branch Code, and Product Code are required";
                return false;
            }

            SqlConnection con = Database.getConnection();
            con.Open();

            try
            {
                string query = @"DELETE FROM dbo.StockOutDetails 
                    WHERE BranchCode = @branchCode 
                    AND ID = @batchID 
                    AND ProductCode = @productCode";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@branchCode", branchCode);
                    com.Parameters.AddWithValue("@batchID", batchID);
                    com.Parameters.AddWithValue("@productCode", productCode);

                    com.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException ex)
            {
                errorMessage = "Database error: " + ex.Message;
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
