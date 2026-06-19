using SalesInventorySystem;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public static class GlobalCache
{
    // The property is publicly readable, but only settable from within this class
    public static string CompanyName { get; private set; } = "";

    // Call this method ONCE when your application starts
    public static void InitializeCompanyData()
    {
        try
        {
            // Replace with your actual Database connection method
            using (SqlConnection con = Database.getConnection())
            {
                con.Open();

                // Fetch the company name (adjust the table and column names to match your DB)
                string query = "SELECT TOP 1 CompanyName FROM CompanyProfile";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    object result = com.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        CompanyName = result.ToString();
                    }
                    else
                    {
                        CompanyName = "DEFAULT_COMPANY";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to load company data: " + ex.Message);
            CompanyName = "DEFAULT_COMPANY";
        }
    }
}