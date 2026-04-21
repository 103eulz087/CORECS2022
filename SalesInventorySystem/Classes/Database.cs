using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using DevExpress.XtraEditors;
using System.Configuration;
using Microsoft.Win32;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraEditors.Repository;
using Npgsql;
using System.Threading.Tasks;
using SalesInventorySystem.Classes;
using System.Collections.Concurrent;

namespace SalesInventorySystem
{
    class Database:GlobalVariables
    {
        static RegistryKey regkey;
        static string constring;
        static string pgConString;// = "Host=192.168.3.79;Username=HPGame;Password=18E095D40E2;Database=EptBadger_RESTORE";
        //static string constringLocal = "Data Source=127.0.0.1;Initial Catalog=SalesAndInventory;UserID=sa;Password=p@$$w0rd;";
        public Database()
        {

        }

        public static NpgsqlConnection getPgConnection()
        {
            // Accessing the registry for the Postgres string
            regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsPostgres");

            // It's safer to provide a fallback value in case the registry key doesn't exist
            pgConString = regkey.GetValue("dbconn")?.ToString() ?? pgConString;

            NpgsqlConnection con;
            try
            {
                con = new NpgsqlConnection(pgConString);
            }
            catch (NpgsqlException ex)
            {
                // Log the error or handle it
                return null;
            }
            return con;
        }
        //public static int getCTRVersionAs(String name)
        //{
        //    int num1 = 0;

        //    SqlDataReader sqlDataReader=null;
        //    try
        //    {
        //        SqlConnection connection = Database.getConnection(@"AAITCRE\ConnSettingsUpdater");
        //        connection.Open();
        //        sqlDataReader = new SqlCommand($"SELECT TOP 1 CAST(Versions as int) AS CC FROM UploaderLoopUps WHERE Company='{ name }';", connection).ExecuteReader();

        //        if (sqlDataReader != null)
        //        {
        //            while (sqlDataReader.Read())
        //                num1 = Convert.ToInt32(sqlDataReader["CC"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        num1 = 0;// (int)MessageBox.Show(ex.StackTrace.ToString());
        //    }
        //    finally
        //    {
        //        sqlDataReader.Close();
        //        connection.Close();
        //    }

        //    //exitProg:
        //    return num1;
        //}
        //public static String getConnectionServerName()
        //{
        //    regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsMain");
        //    return constring = regkey.GetValue("servername").ToString();
        //}

        //public static String getConnectionString(string regkeypath)
        //{
        //    regkey = Registry.CurrentUser.CreateSubKey(regkeypath);
        //    return constring = regkey.GetValue("dbconn").ToString();
        //}
        // Cache the server name in memory so we only read the Registry once!
        private static string _cachedServerName = null;

        public static string getConnectionServerName()
        {
            // 1. Check if we already loaded it into memory
            if (string.IsNullOrEmpty(_cachedServerName))
            {
                // 2. Open safely in READ-ONLY mode, wrapped in a 'using' block to prevent OS memory leaks
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AAITCRE\ConnSettingsMain", false))
                {
                    if (key != null)
                    {
                        object regValue = key.GetValue("servername");
                        if (regValue != null)
                        {
                            _cachedServerName = regValue.ToString();
                        }
                    }
                }
            }

            // 3. Return the cached string (or a blank string if the registry key is completely missing)
            return _cachedServerName ?? "";
        }

        public static string getConnectionString(string regkeypath)
        {
            string connectionString = "";

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(regkeypath, false))
                {
                    if (key != null)
                    {
                        object regValue = key.GetValue("dbconn");

                        // Safely check for null before converting to string!
                        if (regValue != null)
                        {
                            connectionString = regValue.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Fail safely if the registry path is malformed
            }

            return connectionString;
        }
        //FOR CASHIER ONLY
        public static void RunLocalDatabaseMigrations()
        {
            try
            {
                using (SqlConnection con = getConnection()) // Connects to the cashier's local DB
                {
                    con.Open();

                    // You can safely stack as many CREATEs and ALTERs in here as you want!
                //    --1.Create the new table if it doesn't exist
                //IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NewCloudTable]') AND type in (N'U'))
                //BEGIN
                //    CREATE TABLE[dbo].[NewCloudTable](
                //        [ID][int] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
                //        [DataValue] [varchar] (50) NULL
                //    )
                //END

                //-- 2. Add the audit columns to DeliveryDetails if they are missing
                    string migrationScript = @"
            
                        IF COL_LENGTH('dbo.POSType', 'isAutoSystemDeduct') IS NULL
                        BEGIN
                            ALTER TABLE dbo.POSType ADD isAutoSystemDeduct BIT NULL DEFAULT 0 WITH VALUES;
                        END
                    ";

                    using (SqlCommand com = new SqlCommand(migrationScript, con))
                    {
                        com.CommandTimeout = 120; // 2 minutes
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show("Warning: Local database migration failed. " + ex.Message, "Sync Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //public static SqlConnection getConnection()
        //{
        //    //regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettings");
        //    regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsMain");
        //    constring = regkey.GetValue("dbconn").ToString();
        //    SqlConnection con;
        //    try
        //    {
        //        con = new SqlConnection(constring);
        //    }
        //    catch (SqlException sex)
        //    {
        //        sex.StackTrace.ToString();
        //        return null;
        //    }
        //    return con;
        //}
        private static string _cachedConnectionString = null;

        public static SqlConnection getConnection()
        {
            // 1. Only read from the Registry if we haven't loaded it yet!
            if (string.IsNullOrEmpty(_cachedConnectionString))
            {
                // Open in READ-ONLY mode, wrapped in a using block to release OS resources
                using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"AAITCRE\ConnSettingsMain", false))
                {
                    if (regkey != null)
                    {
                        object regValue = regkey.GetValue("dbconn");

                        if (regValue != null)
                        {
                            _cachedConnectionString = regValue.ToString();
                        }
                    }
                }

                // 2. Fail Fast: If the string is still empty, the computer isn't set up right!
                if (string.IsNullOrEmpty(_cachedConnectionString))
                {
                    // Throwing an explicit exception here makes debugging 100x easier
                    throw new InvalidOperationException("CRITICAL ERROR: Database connection string is missing from the Windows Registry.");
                }
            }

            // 3. Create and return the connection using the blazing-fast memory cache
            return new SqlConnection(_cachedConnectionString);
        }


        //public static SqlConnection getConnection(string regkeyname)
        //{
        //    regkey = Registry.CurrentUser.CreateSubKey(regkeyname);
        //    constring = regkey.GetValue("dbconn").ToString();
        //    SqlConnection con;
        //    try
        //    {
        //        con = new SqlConnection(constring);
        //        //if (con.State == ConnectionState.Closed)
        //        //{
        //        //    con.Close();
        //        //    goto outer;
        //        //}
        //    }
        //    catch (SqlException sex)
        //    {
        //        sex.StackTrace.ToString();
        //        return null;
        //    }
        //    //outer:
        //    return con;
        //}
        //public static SqlConnection getConnectionUpdater()
        //{
        //    regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsUpdater");
        //    constring = regkey.GetValue("dbconn").ToString();
        //    SqlConnection con;
        //    try
        //    {
        //        con = new SqlConnection(constring);
        //    }
        //    catch (SqlException sex)
        //    {
        //        sex.StackTrace.ToString();
        //        return null;
        //    }
        //    return con;
        //}
        // A thread-safe memory cache to store multiple connection strings
        private static readonly ConcurrentDictionary<string, string> _connectionCache = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static SqlConnection getConnection(string regkeyname)
        {
            // 1. Try to get the connection string from memory cache first (Blazing fast!)
            if (!_connectionCache.TryGetValue(regkeyname, out string cachedString))
            {
                // 2. If it is NOT in memory, read the Registry (Safely in READ-ONLY mode)
                using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(regkeyname, false))
                {
                    if (regkey != null)
                    {
                        object regValue = regkey.GetValue("dbconn");
                        if (regValue != null)
                        {
                            cachedString = regValue.ToString();

                            // 3. Save it to the cache so we never have to read the registry for this key again!
                            _connectionCache.TryAdd(regkeyname, cachedString);
                        }
                    }
                }

                // 4. Fail Fast: If it's still null, the registry key is missing or blank
                if (string.IsNullOrEmpty(cachedString))
                {
                    throw new InvalidOperationException($"CRITICAL ERROR: Connection string missing in Registry: {regkeyname}");
                }
            }

            // 5. Return the new connection using the safely cached string
            return new SqlConnection(cachedString);
        }

        public static SqlConnection getConnectionUpdater()
        {
            // DRY Principle (Don't Repeat Yourself): 
            // Just pass the hardcoded string into your optimized master method!
            return getConnection(@"AAITCRE\ConnSettingsUpdater");
        }
        public static SqlConnection getCustomConnection(string constring)
        {
           
            SqlConnection con;
            try
            {
                con = new SqlConnection(constring);
            }
            catch (SqlException sex)
            {
                sex.StackTrace.ToString();
                return null;
            }
            return con;
        }public static SqlConnection getCustomizeConnection()
        {
            regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettingsHRM");
            constring = regkey.GetValue("dbconn").ToString();
            SqlConnection con;
            try
            {
                con = new SqlConnection(constring);
            }
            catch (SqlException sex)
            {
                sex.StackTrace.ToString();
                return null;
            }
            return con;
        }
        public static SqlConnection getCustomizeConnection(string registryconsettings)
        {
            regkey = Registry.CurrentUser.CreateSubKey(registryconsettings);
            constring = regkey.GetValue("dbconn").ToString();
            SqlConnection con;
            try
            {
                con = new SqlConnection(constring);
            }
            catch (SqlException sex)
            {
                sex.StackTrace.ToString();
                return null;
            }
            return con;
        }

        public static string getConnectionStringProd()
        {
            string str = "";
            regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettings");
            str = regkey.GetValue("dbconn").ToString();
            return str;
        }

        public static SqlConnection getStandAloneConnection()
        {
            regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettings2");
            constring = regkey.GetValue("dbconn").ToString();
            SqlConnection con;
            try
            {
                con = new SqlConnection(constring);
            }
            catch (SqlException sex)
            {
                sex.StackTrace.ToString();
                return null;
            }
           
            return con;
        }

        public static SqlConnection getLocalConnection()
        {
            regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettingsLocal");
            constring = regkey.GetValue("dbconn").ToString();
            SqlConnection con;
            try
            {
                con = new SqlConnection(constring);
            }
            catch (SqlException sex)
            {
                sex.StackTrace.ToString();
                return null;
            }
            return con;
        }

        public static string getDBName()
        {
            string dbname = "";
            regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettings");
            dbname = regkey.GetValue("dbname").ToString();
            return dbname;
        }


        public static void addData(string tableName, string datas)
        {
            SqlConnection con = getConnection();
            con.Open();
            string query = "INSERT INTO "+tableName+" VALUES ("+datas+") ";
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }

        public static void ExecuteCommandQuery(string query)
        {
            SqlConnection con = getConnection();
            SqlTransaction transaction;
            con.Open();
            transaction = con.BeginTransaction();
            try
            {
                SqlCommand com = new SqlCommand(query, con,transaction);
                com.ExecuteNonQuery();
                transaction.Commit();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
        }
        public static void ExecuteCommandQuery(string query,string msg)
        {
            SqlConnection con = getConnection();
            SqlTransaction transaction;
            con.Open();
            transaction = con.BeginTransaction();
            try
            {
                SqlCommand com = new SqlCommand(query, con, transaction);
                com.ExecuteNonQuery();
                transaction.Commit();
                XtraMessageBox.Show(msg);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                transaction.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        public static void ExecuteQuery(string query)
        {
            SqlConnection con = getConnection();
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
            }
           catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static void ExecuteQueryCommand(string query)
        {
            SqlConnection con = getConnection();
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand(query, con);
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
        //public static void ExecuteQuery(string query, string msg)
        //{
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandTimeout = 3600;
        //    com.ExecuteNonQuery();
        //    XtraMessageBox.Show(msg);
        //    con.Close();
        //}
        public static void ExecuteQuery(string query, string msg)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.CommandTimeout = 3600; // 1-Hour Timeout
                        com.ExecuteNonQuery();
                    }
                }

                // Only show the popup if the query was successful AND a message was provided
                if (!string.IsNullOrEmpty(msg))
                {
                    BigAlert.Show("SUCESS",msg+ ": Process was executed succesfully",MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                BigAlert.Show("Execution Failed:", ex.Message + "Database Error", MessageBoxIcon.Error);
            }
        }
        public static async Task ExecuteQueryAsync(string query, string msg)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    // Opens the connection in the background
                    await con.OpenAsync();

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // 1 Hour timeout, but it won't freeze the screen anymore!
                        com.CommandTimeout = 3600;

                        // Executes the heavy query in the background
                        await com.ExecuteNonQueryAsync();
                    }
                }

                // Because we await, it safely returns to the UI thread to show the box!
                if (!string.IsNullOrEmpty(msg))
                {
                    //XtraMessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BigAlert.Show("SUCESS", msg + ": Process was executed succesfully", MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                //XtraMessageBox.Show("Execution Failed: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BigAlert.Show("Execution Failed:", ex.Message + "Database Error", MessageBoxIcon.Error);
            }
        }
        public static void ExecuteQuery(string query, string msg,SqlConnection con)
        {
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            XtraMessageBox.Show(msg);
            con.Close();
        }
        public static void ExecuteQuery2(string query, SqlConnection con)
        {
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.CommandTimeout = 3600;
            com.ExecuteNonQuery();
            con.Close();
        }

        
        public static void ExecuteLocalQuery(string query, SqlConnection con)
        {
          //  SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }

        //public static bool checkifExist(string query)
        //{
        //    bool result = false;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    //try
        //    //{
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            result = true;
        //        }
        //        else
        //        {
        //            result = false;
        //        }
        //    reader.Close();
        //    //}
        //    //catch(SqlException ex)
        //    //{
        //    //    XtraMessageBox.Show(ex.Message.ToString());
        //    //}
        //    //finally
        //    //{
        //        con.Close();
        //    //}

        //    return result;
        //}
        public static bool checkifExist(string query)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            // Instantly returns true if data exists, false if it doesn't
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // If the query fails or network drops, safely assume it doesn't exist
                return false;
            }
        }
        public static bool checkifExist(string query,SqlConnection con)
        {
            bool result = false;
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            reader.Close();
            con.Close();
            return result;
        }
        public static String getSingleQueryCustom(string query,string col,string col2)
        {
            string str = "",str1="";
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    str = reader[col].ToString();
                    str1 = reader[col2].ToString();
                }
                reader.Close();
            }
            con.Close();
            return str;
        }
        //public static String getSingleQuery(string query,string returnval)
        //{
        //    string str = "";
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                str = reader[returnval].ToString();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch(SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //    return str;
        //}
        public static string getSingleQuery(string query, string returnval)
        {
            string str = "";
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            // Loops through and grabs the specified column safely
                            while (reader.Read())
                            {
                                // The ?.ToString() prevents crashes if the database cell is NULL
                                str = reader[returnval]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Silent fail: Let the UI handle the empty string rather than throwing a popup
            }
            return str;
        }
        //public static String getSingleQuery(string tablename, string condition, string returnval)
        //{
        //    string str = "";
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "SELECT TOP(1) " + returnval + " FROM " + tablename + " WHERE " + condition + " ";
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                str = reader[returnval].ToString();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //    return str;
        //}
        public static string getSingleQuery(string tablename, string condition, string returnval)
        {
            string str = "";

            try
            {
                // 'using' blocks automatically close connections and destroy memory safely
                using (SqlConnection con = getConnection())
                {
                    con.Open();

                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // ExecuteScalar is heavily optimized for grabbing exactly ONE value
                        object result = com.ExecuteScalar();

                        // Ensure we don't crash on NULL database values
                        if (result != null && result != DBNull.Value)
                        {
                            str = result.ToString();
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // SILENT FAIL: Let it return an empty string. 
                // The calling method (like your button click) should be the one to show an error if it gets "".
            }

            return str;
        }
        public static String getSingleQuery(string tablename, string condition, string returnval,SqlConnection con)
        {
            string str = "";
            //SqlConnection con = getConnection();
            con.Open();
            try
            {
                string query = "SELECT TOP(1) * FROM " + tablename + " WHERE " + condition + " ";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        str = reader[returnval].ToString();
                    }
                    reader.Close();
                }
            }
            catch(SqlException ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
           
            return str;
        }
        public static async Task<string> getSingleQueryAsync(string tablename, string condition, string returnval)
        {
            string str = "";

            try
            {
                using (SqlConnection con = getConnection())
                {
                    await con.OpenAsync(); // Non-blocking connection

                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = await com.ExecuteScalarAsync(); // Non-blocking query

                        if (result != null && result != DBNull.Value)
                        {
                            str = result.ToString();
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Silently catch network blips
            }

            return str;
        }

        //public static Dictionary<string, object> getMultipleQuery(string tablename, string condition, string returnval) // ID, Name
        //{

        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) " + returnval + " FROM " + tablename + " WHERE " + condition + " ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();

        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            //str = reader[returnval].ToString();
        //            dic = ToDictionary(reader);
        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return dic;
        //}
        //public static Dictionary<string, object> getMultipleQuery(string query, string returnval) // ID, Name
        //{

        //    SqlConnection con = getConnection();
        //    con.Open();
        //    //string query = "SELECT TOP 1 " + returnval + " FROM " + tablename + " WHERE " + condition + " ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();

        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            //str = reader[returnval].ToString();
        //            dic = ToDictionary(reader);
        //            // Replace null or whitespace values with empty string
        //            foreach (var key in dic.Keys.ToList())
        //            {
        //                var value = dic[key];
        //                dic[key] = string.IsNullOrWhiteSpace(value?.ToString()) ? "" : value;
        //            }

        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return dic;
        //}
        public static Dictionary<string, object> getMultipleQuery(string tablename, string condition, string returnval)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        // Changed 'while' to 'if' since we are only grabbing TOP(1)
                        if (reader.Read())
                        {
                            dic = ToDictionary(reader);
                            CleanDictionary(dic); // Apply your null cleanup!
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Fail safely and return an empty dictionary
            }

            return dic;
        }

        // Note: I kept 'returnval' in the signature so it doesn't break your existing code, 
        // but it is not doing anything since 'query' is already fully formed.
        public static Dictionary<string, object> getMultipleQuery(string query, string returnval = "")
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dic = ToDictionary(reader);
                            CleanDictionary(dic); // Apply your null cleanup!
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Fail safely
            }

            return dic;
        }

        // ------------------------------------------------------------------
        // HELPER METHOD: Keeps your code DRY (Don't Repeat Yourself)
        // ------------------------------------------------------------------
        private static void CleanDictionary(Dictionary<string, object> dic)
        {
            foreach (var key in dic.Keys.ToList())
            {
                var value = dic[key];
                // Extra safe: checks for SQL DBNull as well as standard C# nulls
                if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    dic[key] = "";
                }
            }
        }

        public static async Task<Dictionary<string, object>> getMultipleQueryAsync(string tablename, string condition, string returnval)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                using (SqlConnection con = getConnection())
                {
                    await con.OpenAsync();
                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataReader reader = await com.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dic = ToDictionary(reader);
                            CleanDictionary(dic);
                        }
                    }
                }
            }
            catch (SqlException) { }

            return dic;
        }

        public static async Task<Dictionary<string, object>> getMultipleQueryAsync(string query, string returnval = "")
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            try
            {
                using (SqlConnection con = getConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataReader reader = await com.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            dic = ToDictionary(reader);
                            CleanDictionary(dic);
                        }
                    }
                }
            }
            catch (SqlException) { }

            return dic;
        }

        //public static Dictionary<string, object> ToDictionary(System.Data.SqlClient.SqlDataReader row)
        //{
        //    string nameStr="";
        //    lock (nameStr)
        //    {
        //        var dic = new Dictionary<string, object>();
        //        for (int i = 0; i < row.FieldCount; i++)
        //        {
        //            nameStr = row.GetName(i);
        //            dic[nameStr] = (object)row[nameStr];
        //        }
        //        return dic;
        //    }
        //}
        public static Dictionary<string, object> ToDictionary(System.Data.SqlClient.SqlDataReader row)
        {
            // Adding OrdinalIgnoreCase makes reading from this dictionary later 100x easier!
            var dic = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < row.FieldCount; i++)
            {
                string columnName = row.GetName(i);

                // Grab the value directly by INDEX (i). This is significantly faster!
                object value = row[i];

                // Handle SQL NULLs directly at the source
                if (value == DBNull.Value)
                {
                    dic[columnName] = "";
                }
                else
                {
                    dic[columnName] = value;
                }
            }

            return dic;
        }


        //public static Double getSingleAmountQuery(string tablename, string condition, string returnval)
        //{
        //    double str = 0.0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) * FROM " + tablename + " WHERE " + condition + " ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            str = Convert.ToDouble(reader[returnval]);
        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return str;
        //}

        //public static String getSingleQueryWithNull(string tablename,string id, string condition, string returnval)
        //{
        //    string str = "";
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) " + id+" FROM " + tablename + " WHERE " + condition + " ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    try
        //    {
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                str = reader[returnval].ToString();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}

        //public static String getSingleData(string tablename,string col,string value)
        //{
        //    string str = "";
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "SELECT TOP(1) * FROM " + tablename + " WHERE " + col + " = '" + value + "' ";
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                str = reader[col].ToString();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch(SqlException ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}

        //public static String getSingleData(string tablename, string col, string value,string returnval)
        //{
        //    string str = "";
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "SELECT TOP(1) * FROM " + tablename + " WHERE " + col + " = '" + value + "' ";
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                str = reader[returnval].ToString();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}
        // 1. The Double / Amount Lookup
        public static double getSingleAmountQuery(string tablename, string condition, string returnval)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    // FIXED: Only selects the column we actually need!
                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToDouble(result) : 0.0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0.0; // Fail safely
            }
        }

        // 2. The Null-Safe String Lookup (Fixed the 'id' vs 'returnval' bug!)
        public static string getSingleQueryWithNull(string tablename, string id, string condition, string returnval)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    // FIXED: We select 'returnval' directly so ExecuteScalar grabs the right text.
                    // (Note: If 'id' was supposed to be the column, just swap {returnval} with {id} here)
                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? result.ToString() : "";
                    }
                }
            }
            catch (Exception)
            {
                // Fail silently and safely return a blank string
                return "";
            }
        }

        // 3. Single Data Lookup (Returns the same column you searched for)
        public static string getSingleData(string tablename, string col, string value)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    // FIXED: Replaced * with {col}
                    string query = $"SELECT TOP(1) {col} FROM {tablename} WHERE {col} = '{value}'";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? result.ToString() : "";
                    }
                }
            }
            catch (SqlException)
            {
                return "";
            }
        }

        // 4. Single Data Lookup (Searches one column, returns another)
        public static string getSingleData(string tablename, string col, string value, string returnval)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    // FIXED: Replaced * with {returnval}
                    string query = $"SELECT TOP(1) {returnval} FROM {tablename} WHERE {col} = '{value}'";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? result.ToString() : "";
                    }
                }
            }
            catch (SqlException)
            {
                return "";
            }
        }
        //public static String getSingleResultSet(string query)
        //{
        //    string str = "";
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        SqlCommand com = new SqlCommand(query, con);
        //        com.CommandTimeout = 0;
        //        SqlDataReader reader = com.ExecuteReader();
        //        if(reader.Read())
        //        {
        //            str = reader[0].ToString();
        //        }
        //        reader.Close();
        //    }
        //    catch (SqlException ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return str;
        //}
        public static string getSingleResultSet(string query)
        {
            string str = "";
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // Removed the infinite 0 timeout. 
                        // Set to 3600 (1 hour) if this is a heavy report, otherwise 30-60 is safer!
                        com.CommandTimeout = 3600;

                        // ExecuteScalar instantly grabs the exact value of reader[0] and nothing else
                        object result = com.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            str = result.ToString();
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Fail silently. The UI should handle what to do if it receives an empty string.
            }

            return str;
        }
        public static string getSingleResultSet(string query, Dictionary<string, object> parameters)
        {
            string result = string.Empty;

            // Replace "GetConnectionString()" with however you currently get your DB connection string
            using (SqlConnection conn = getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Attach all parameters dynamically
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    try
                    {
                        conn.Open();
                        object obj = cmd.ExecuteScalar();
                        if (obj != null)
                        {
                            result = obj.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle or log your database errors here
                        throw new Exception("Database Error: " + ex.Message);
                    }
                }
            }
            return result;
        }
        public static async Task<string> getSingleResultSetAsync(string query)
        {
            string str = "";
            try
            {
                using (SqlConnection con = getConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.CommandTimeout = 3600;

                        object result = await com.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            str = result.ToString();
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Fail silently
            }

            return str;
        }

        //public static int getCountData(string query,string value)
        //{
        //    int ctr = 0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //   // string query = "SELECT COUNT(" + id + ") AS Counter FROM " + tablename + " WHERE " + condition + " ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            ctr = Convert.ToInt32(reader[value].ToString());
        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return ctr;
        //}
        //public static int getCountData(string tablename, string condition,string id)
        //{
        //    int ctr = 0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "SELECT TOP(1) COUNT(" + id + ") AS Counter FROM " + tablename + " WHERE " + condition + " ";
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                ctr = Convert.ToInt32(reader["Counter"].ToString());
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return ctr;
        //}

        //public static int getCountData(string tablename, string col, string value,string id)
        //{
        //    int ctr=0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) COUNT(" + id+") AS Counter FROM " + tablename + " WHERE " + col + " = '" + value + "' ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            ctr = Convert.ToInt32(reader["Counter"].ToString());
        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return ctr;
        //}

        //public static int getCountData(string tablename, string col, string value, string id,string col2,string val2)
        //{
        //    int ctr = 0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) COUNT(" + id + ") AS Counter FROM " + tablename + " WHERE " + col + " = '" + value + "' AND "+col2+" = "+val2+" ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            ctr = Convert.ToInt32(reader["Counter"].ToString());
        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return ctr;
        //}
        // OVERLOAD 1: Raw Query
        // Note: We keep the 'value' parameter so we don't break your existing code, 
        // but ExecuteScalar makes it obsolete since it grabs the first column automatically!
        public static int getCountData(string query, string value)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0; // Safe fail
            }
        }

        // OVERLOAD 2: Condition String
        public static int getCountData(string tablename, string condition, string id)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    // Note: TOP(1) is technically not needed with COUNT(), but harmless to leave in.
                    string query = $"SELECT COUNT({id}) FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }
        }

        
       
        //public static double getTotalSummation(string tablename, string condition,string id)
        //{
        //    double ctr = 0.0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "SELECT TOP(1) ISNULL(" + id + ",0) AS Totals FROM (SELECT SUM(" + id + ") AS Totals FROM " + tablename + " WHERE " + condition + ") ";
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataReader reader = com.ExecuteReader();
        //        if (!reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                ctr = Convert.ToDouble(reader["Totals"].ToString());
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return ctr;
        //}


        public static double getTotalSummation2(string tablename, string condition, string columnName)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    // Simplified query: No need for nested SELECTs!
                    string query = $"SELECT ISNULL(SUM({columnName}), 0) FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // ExecuteScalar grabs the exact single value instantly
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0; // Fail safely
            }
        }
        // Exactly the same, just returning a Decimal for currency!
        public static decimal getTotalSummation2Dec(string tablename, string condition, string columnName)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    string query = $"SELECT ISNULL(SUM({columnName}), 0) FROM {tablename} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }
        }

        //public static int getLastID(string tableName,string id)
        //{
        //    int i = 0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) ISNULL(MAX(CAST(" + id + " as int)),0) AS CC FROM " + tableName;
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        { 
        //            i = Convert.ToInt32(reader["CC"].ToString());
        //        }
        //        reader.Close();
        //    }

        //    con.Close();
        //    return i;
        //}

        //public static int getLastID(string tableName,string condition, string id)
        //{
        //    int i = 0;
        //    SqlConnection con = getConnection();
        //    con.Open();
        //    string query = "SELECT TOP(1) isnull(MAX(CAST(" + id + " as int)),0) AS CC FROM " + tableName + " WHERE "+condition+"";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            //i = reader.GetInt32(1);
        //            i = Convert.ToInt32(reader["CC"].ToString());
        //            // i = int.Parse(reader["CC"].ToString());
        //            //  i = reader.GetInt32(reader.GetOrdinal("CC"));
        //        }
        //        reader.Close();
        //    }
        //    con.Close();
        //    return i;
        //}
        // Version 1: No Condition
        public static int getLastID(string tableName, string idColumnName)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    string query = $"SELECT ISNULL(MAX(CAST({idColumnName} AS INT)), 0) FROM {tableName}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }
        }

        // Version 2: With Condition
        public static int getLastID(string tableName, string condition, string idColumnName)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    string query = $"SELECT ISNULL(MAX(CAST({idColumnName} AS INT)), 0) FROM {tableName} WHERE {condition}";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        object result = com.ExecuteScalar();
                        return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (SqlException)
            {
                return 0;
            }
        }
        public static int getLastID(string tableName, string condition, string id,SqlConnection con)
        {
            int i = 0;
            con.Open();
            string query = "SELECT TOP(1) isnull(MAX(CAST(" + id + " as int)),0) AS CC FROM " + tableName + " WHERE " + condition + "";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    i = Convert.ToInt32(reader["CC"].ToString());
                }
                reader.Close();
            }
            con.Close();
            return i;
        }

        public static int getBeginningID(string tableName, string condition, string id)
        {
            int i = 0;
            SqlConnection con = getConnection();
            con.Open();
            string query = "SELECT TOP(1) isnull(MIN(CAST(" + id + " as int)),0) AS CC FROM " + tableName + " WHERE " + condition + "";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    //i = reader.GetInt32(1);
                    i = Convert.ToInt32(reader["CC"].ToString());
                }
                reader.Close();
            }
            con.Close();
            return i;
        }

        public static String getLastDate(string tableName, string condition, string id)
        {
            string lastdate="";
            SqlConnection con = getConnection();
            con.Open();
            string query = "SELECT TOP(1) MAX(" + id + " ) AS CC FROM " + tableName + " WHERE " + condition + "";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    lastdate = reader["CC"].ToString();
                }
                reader.Close();
            }
            con.Close();
            return lastdate;
        }

        public static String getLastRecord(string tableName, string condition, string id)
        {
            string lastdate = "";
            SqlConnection con = getConnection();
            con.Open();
            string query = "SELECT TOP(1) LAST(" + id + " ) AS CC FROM " + tableName + " WHERE " + condition + "";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    lastdate = reader["CC"].ToString();
                }
                reader.Close();
            }
            con.Close();
            return lastdate;
        }

        public static String getMaxRecord(string tableName, string condition, string id)
        {
            string lastdate = "";
            SqlConnection con = getConnection();
            con.Open();
            string query = "SELECT TOP(1) MAX(" + id + " ) AS CC FROM " + tableName + " WHERE " + condition + "";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    lastdate = reader["CC"].ToString();
                }
                reader.Close();
            }
            con.Close();
            return lastdate;
        }

        //public static void display(string query, GridControl cont, GridView view)
        //{
        //    SqlConnection con = getConnection();
        //    con.Open();
        //  //  cont.BeginUpdate();
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataAdapter adapter = new SqlDataAdapter(com);
        //    DataTable table = new DataTable();
        //    try
        //    {
        //        com.CommandTimeout = 0;
        //        view.Columns.Clear();
        //        cont.DataSource = null;
        //        adapter.Fill(table);
        //        //  table.Columns.Add("OvertimeType");
        //        cont.DataSource = table;
        //        view.BestFitColumns();
        //    }
        //    catch (SqlException ee)
        //    {
        //        XtraMessageBox.Show(ee.ToString());
        //    }
        //    finally
        //    {
        //     //   cont.EndUpdate();
        //        con.Close();
        //    }
        //}
        public static void display(string query, GridControl cont, GridView view)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // 1 Hour timeout. High enough for heavy reports, but prevents infinite freezes.
                        com.CommandTimeout = 3600;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // Freeze the UI drawing engine (Prevents flickering and speeds up load times)
                            cont.BeginUpdate();

                            view.Columns.Clear();
                            cont.DataSource = null; // Clean slate
                            cont.DataSource = table;
                            view.BestFitColumns();
                        }
                    }
                }
            }
            catch (SqlException ee)
            {
                XtraMessageBox.Show("Failed to load grid data: " + ee.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ALWAYS resume drawing, even if the query crashes!
                if (cont != null)
                {
                    cont.EndUpdate();
                }
            }
        }
        public static async Task displayAsync(string query, GridControl cont, GridView view)
        {
            try
            {
                DataTable table = new DataTable();
                // 1. Do all the heavy database lifting in the BACKGROUND
                using (SqlConnection con = getConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.CommandTimeout = 3600; // 1-Hour Timeout

                        using (SqlDataReader reader = await com.ExecuteReaderAsync())
                        {
                            // This seamlessly loads the DataTable without blocking the UI
                            table.Load(reader);
                        }
                    }
                }

                // 2. Now that the data is ready, update the DevExpress UI safely
                cont.BeginUpdate();

                view.Columns.Clear();
                cont.DataSource = null;
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (SqlException ee)
            {
                XtraMessageBox.Show("Failed to load grid data: " + ee.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cont != null)
                {
                    cont.EndUpdate();
                }
            }
        }
        public static void displayPg(string query, GridControl cont, GridView view)
        {
            // 1. Use your PostgreSQL connection method
            NpgsqlConnection con = getPgConnection();

            // 2. Use Npgsql classes instead of Sql classes
            NpgsqlCommand com = new NpgsqlCommand(query, con);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(com);
            DataTable table = new DataTable();

            try
            {
                con.Open();

                // CommandTimeout 0 means wait indefinitely (use with caution)
                com.CommandTimeout = 0;

                view.Columns.Clear();
                cont.DataSource = null;

                // Fill the DataTable from Postgres
                adapter.Fill(table);

                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (NpgsqlException ee) // Catch PostgreSQL specific errors
            {
                XtraMessageBox.Show("Postgres Error: " + ee.Message);
            }
            catch (Exception ex) // Catch general errors
            {
                XtraMessageBox.Show("General Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                // Properly dispose of objects to free memory
                com.Dispose();
                adapter.Dispose();
            }
        }

        public static void display(string query, GridControl cont, GridView view,SqlConnection con)
        {
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                //  table.Columns.Add("OvertimeType");
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void display(string query, GridControl cont, CardView view)
        {
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                //  table.Columns.Add("OvertimeType");
                cont.DataSource = table;
                //view.BestFitColumns();
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void displayOrdinaryGrid(string query,DataGridView view)
        {
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                //  table.Columns.Add("OvertimeType");
                view.DataSource = table;
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void GridMasterDetail(string query1, string query2,string table1,string table2, string col1, string col2, string fkeyname, GridControl grid,string eulz)
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query1, con);
                SqlDataAdapter adapter2 = new SqlDataAdapter(query2, con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

                //Set up a master-detail relationship between the DataTables
                DataColumn keycolumn = ds.Tables[table1].Columns[col1];
                DataColumn foreigncolumn = ds.Tables[table2].Columns[col2];
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn);
                
                //Bind the grid control to the data source
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();

                GridView view = new GridView(grid);
                view.BestFitColumns();
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ColumnAutoWidth = true;
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
                view.OptionsView.ShowFooter = true;
                grid.LevelTree.Nodes.Add(fkeyname, view);
            }
            catch(SqlException e)
            {
                XtraMessageBox.Show(e.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void GridMasterDetailMysql(string query1, string query2,string table1,string table2, string col1, string col2, string fkeyname, GridControl grid,string eulz)
        {
            try
            {
                string constringLocal = "SERVER=abacos.com.ph;DATABASE=abacos_lucky7;UID=abacos_livetrends;PASSWORD=6969rd//;";
                MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(constringLocal);
                con.Open();

                MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(query1, con);
                MySql.Data.MySqlClient.MySqlDataAdapter adapter2 = new MySql.Data.MySqlClient.MySqlDataAdapter(query2, con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

                //Set up a master-detail relationship between the DataTables
                DataColumn keycolumn = ds.Tables[table1].Columns[col1];
                DataColumn foreigncolumn = ds.Tables[table2].Columns[col2];
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn);
                
                //Bind the grid control to the data source
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();

                GridView view = new GridView(grid);
                view.BestFitColumns();
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ColumnAutoWidth = true;
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
                view.OptionsView.ShowFooter = true;
                grid.LevelTree.Nodes.Add(fkeyname, view);
            }
            catch(SqlException e)
            {
                XtraMessageBox.Show(e.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static void GridMasterDetail(string query1, string query2, string table1, string table2, string col1, string col2,string col3,string col4, string fkeyname, GridControl grid,GridView viewDetails, string eulz)
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query1, con);
                SqlDataAdapter adapter2 = new SqlDataAdapter(query2, con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

              
                DataColumn[] keycolumn = { ds.Tables[table1].Columns[col1], ds.Tables[table1].Columns[col2] };
                DataColumn[] foreigncolumn = { ds.Tables[table2].Columns[col3], ds.Tables[table2].Columns[col4] };
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn,false);

             
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();


                //GridView view = new GridView(grid);
                viewDetails.ExpandAllGroups();
                viewDetails.OptionsView.ShowGroupPanel = false;
                //view.OptionsView.ColumnAutoWidth = true;
                viewDetails.OptionsView.RowAutoHeight = true;
                viewDetails.OptionsBehavior.ReadOnly = true;
                viewDetails.OptionsBehavior.Editable = false;
                viewDetails.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
                viewDetails.OptionsView.ShowFooter = true;
                viewDetails.BestFitColumns();
                grid.LevelTree.Nodes.Add(fkeyname, viewDetails);
            }
            catch (SqlException e)
            {
                XtraMessageBox.Show(e.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static void GridMasterDetail(string table1, string table2, string col1, string col2, string fkeyname, GridControl grid)
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + table1 + "", con);
                SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT * FROM " + table2 + "", con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

                //Set up a master-detail relationship between the DataTables
                DataColumn keycolumn = ds.Tables[table1].Columns[col1];
                DataColumn foreigncolumn = ds.Tables[table2].Columns[col2];
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn);
                //Bind the grid control to the data source
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();

                GridView view = new GridView(grid);
                view.BestFitColumns();
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
                //view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
                view.OptionsView.ShowFooter = true;
                grid.LevelTree.Nodes.Add(fkeyname, view);
            }
            catch (SqlException e)
            {
                XtraMessageBox.Show(e.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void GridMasterDetailMysql(string table1, string table2, string col1, string col2, string fkeyname, GridControl grid)
        {
            try
            {
                string constringLocal = "SERVER=abacos.com.ph;DATABASE=abacos_lucky7;UID=abacos_livetrends;PASSWORD=6969rd//;";
                MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(constringLocal);
                con.Open();

                MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT * FROM " + table1 + "", con);
                MySql.Data.MySqlClient.MySqlDataAdapter adapter2 = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT * FROM " + table2 + "", con);

                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

                //Set up a master-detail relationship between the DataTables
                DataColumn keycolumn = ds.Tables[table1].Columns[col1];
                DataColumn foreigncolumn = ds.Tables[table2].Columns[col2];
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn);
                //Bind the grid control to the data source
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();

                GridView view = new GridView(grid);
                view.BestFitColumns();
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
                //view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
                view.OptionsView.ShowFooter = true;
                grid.LevelTree.Nodes.Add(fkeyname, view);
            }
            catch (SqlException e)
            {
                XtraMessageBox.Show(e.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void GridMasterDetail(string table1, string table2,string condition1,string condition2, string col1, string col2, string fkeyname, GridControl grid)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + table1 + " WHERE " + condition1 + "", con);
                SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT * FROM " + table2 + " WHERE " + condition2 + "", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

                //Set up a master-detail relationship between the DataTables
                DataColumn keycolumn = ds.Tables[table1].Columns[col1];
                DataColumn foreigncolumn = ds.Tables[table2].Columns[col2];
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn);
                //Bind the grid control to the data source
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();

                GridView view = new GridView(grid);
                view.BestFitColumns();
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
                //view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
                view.OptionsView.ShowFooter = true;
                grid.LevelTree.Nodes.Add(fkeyname, view);
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public static void GridMasterDetailMysql(string table1, string table2,string condition1,string condition2, string col1, string col2, string fkeyname, GridControl grid)
        {
            string constringLocal = "SERVER=abacos.com.ph;DATABASE=abacos_lucky7;UID=abacos_livetrends;PASSWORD=6969rd//;";
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(constringLocal);
            con.Open();
            try
            {
                MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT * FROM " + table1 + " WHERE " + condition1 + "", con);
                MySql.Data.MySqlClient.MySqlDataAdapter adapter2 = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT * FROM " + table2 + " WHERE " + condition2 + "", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, table1);
                adapter2.Fill(ds, table2);

                //Set up a master-detail relationship between the DataTables
                DataColumn keycolumn = ds.Tables[table1].Columns[col1];
                DataColumn foreigncolumn = ds.Tables[table2].Columns[col2];
                ds.Relations.Add(fkeyname, keycolumn, foreigncolumn);
                //Bind the grid control to the data source
                grid.DataSource = ds.Tables[table1];
                grid.ForceInitialize();

                GridView view = new GridView(grid);
                view.BestFitColumns();
                view.ExpandAllGroups();
                view.OptionsView.ShowGroupPanel = false;
                //view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
                view.OptionsView.ShowFooter = true;
                grid.LevelTree.Nodes.Add(fkeyname, view);
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

       

        public static void displayFromSP(string query, GridControl cont, GridView view)
        {
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                //  table.Columns.Add("OvertimeType");
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }


        public static void displayLocalGrid(string query, DataGridView dview)
        {
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                //  table.Columns.Add("OvertimeType");
                dview.DataSource = table;
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static void displayLocalGrid(string query, DataGridView dview,SqlConnection con)
        {
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                //  table.Columns.Add("OvertimeType");
                dview.DataSource = table;
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }
       
        public static void displayDevComboBoxItems(string query, string col, ComboBoxEdit box)
        {
            box.Properties.Items.Clear();
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Properties.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
             
                con.Close();
            }
        }

        public static void displayRepositoryComboBoxItems(string query, string col,RepositoryItemComboBox box)
        {
            box.Items.Clear();
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {

                con.Close();
            }
        }
        public static void displayDevComboBoxItems(string query, string col, ComboBoxEdit box,SqlConnection con)
        {
            box.Properties.Items.Clear();
           // SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Properties.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static void displayComboBoxItems(string query, string col, System.Windows.Forms.ComboBox box)
        {
            box.Items.Clear();
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString().Trim());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
               
                con.Close();
            }
        }
        public static void displayComboBoxItems(string query, string col, System.Windows.Forms.ComboBox box,SqlConnection con)
        {
            box.Items.Clear();
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString().Trim());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
           
                con.Close();
            }
        }
        public static void displayListBoxItems(string query, string col, System.Windows.Forms.ListBox box)
        {
            box.Items.Clear();
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
              
                con.Close();
            }
        }
        public static void displayListBoxItems(string query, string col, System.Windows.Forms.ListBox box,SqlConnection con)
        {
            box.Items.Clear();
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
            
                con.Close();
            }
        }
        public static void displayCheckedListBoxItems(string query, string col, System.Windows.Forms.CheckedListBox box, SqlConnection con)
        {
            box.Items.Clear();
            //SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
               
                con.Close();
            }
        }
        public static void displayCheckedListBoxItemsDevEx(string query, string col, CheckedListBoxControl box)
        {
            box.Items.Clear();
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
               
                con.Close();
            }
        }
        public static void displayCheckedListBoxItemsDevEx(string query, string col, CheckedListBoxControl box, SqlConnection con)
        {
            box.Items.Clear();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        box.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
               
                con.Close();
            }
        }
        public static void displayListViewItems(string query, string col, System.Windows.Forms.ListView view, SqlConnection con)
        {
            view.Items.Clear();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        view.Items.Add(reader[col].ToString());
                    }
                    reader.Close();
                }
            }
            catch (SqlException sex)
            {
                XtraMessageBox.Show(sex.StackTrace.ToString());
            }
            finally
            {
                
                con.Close();
            }
        }

        //public static void displaySearchlookupEdit(string query, SearchLookUpEdit searchEdit)
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    // string query = "SELECT * FROM PrimalCuts";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataAdapter adapter = new SqlDataAdapter(com);
        //    DataTable table = new DataTable();
        //    adapter.Fill(table);
        //    searchEdit.Properties.DataSource = table;
        //    con.Close();
        //}
        //public static void displaySearchlookupEdit(string query,SearchLookUpEdit searchEdit,string displaymember,string valuemember)
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    try
        //    {
        //        SqlCommand com = new SqlCommand(query, con);
        //        SqlDataAdapter adapter = new SqlDataAdapter(com);
        //        DataTable table = new DataTable();

        //        searchEdit.Properties.View.Columns.Clear();
        //        adapter.Fill(table);
        //        searchEdit.Properties.DataSource = null;
        //        searchEdit.Properties.DataSource = table;
        //        searchEdit.Properties.DisplayMember = displaymember;
        //        searchEdit.Properties.ValueMember = valuemember;
        //    }
        //    catch(SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        public static void displaySearchlookupEdit(string query, SearchLookUpEdit searchEdit)
        {
            try
            {
                using (SqlConnection con = getConnection()) // Uses your new optimized connection!
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        searchEdit.Properties.DataSource = table;
                    }
                }
            }
            catch (SqlException)
            {
                // Fail silently so the UI just shows an empty dropdown instead of crashing the form
            }
        }

        public static void displaySearchlookupEdit(string query, SearchLookUpEdit searchEdit, string displaymember, string valuemember)
        {
            try
            {
                using (SqlConnection con = getConnection())
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // UI binding logic
                        searchEdit.Properties.View.Columns.Clear();
                        searchEdit.Properties.DataSource = null; // Forces a clean DevExpress refresh
                        searchEdit.Properties.DataSource = table;
                        searchEdit.Properties.DisplayMember = displaymember;
                        searchEdit.Properties.ValueMember = valuemember;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Formatted cleanly for DevExpress
                XtraMessageBox.Show("Failed to load dropdown data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static async Task displaySearchlookupEditAsync(string query, SearchLookUpEdit searchEdit, string displaymember, string valuemember)
        {
            try
            {
                DataTable table = new DataTable();

                // 1. Fetch the data in the background (UI stays completely unfrozen)
                using (SqlConnection con = getConnection())
                {
                    await con.OpenAsync();
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // We use a DataReader for Async because SqlDataAdapter doesn't fully support true Async operations
                        using (SqlDataReader reader = await com.ExecuteReaderAsync())
                        {
                            table.Load(reader);
                        }
                    }
                }

                // 2. Bind it to the DevExpress control safely on the UI thread
                searchEdit.Properties.View.Columns.Clear();
                searchEdit.Properties.DataSource = null;
                searchEdit.Properties.DataSource = table;
                searchEdit.Properties.DisplayMember = displaymember;
                searchEdit.Properties.ValueMember = valuemember;
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show("Failed to load dropdown data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void displaySearchlookupEdit(string query, SearchLookUpEdit searchEdit, string displaymember, string valuemember,SqlConnection con)
        {
           // SqlConnection con = Database.getConnection();
            con.Open();
            // string query = "SELECT * FROM PrimalCuts";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            searchEdit.Properties.DataSource = table;
            searchEdit.Properties.DisplayMember = displaymember;
            searchEdit.Properties.ValueMember = valuemember;
            con.Close();
        }
        public static void displayRepositorySearchlookupEdit(string query, RepositoryItemSearchLookUpEdit searchEdit, string displaymember, string valuemember)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                searchEdit.DataSource = table;
                searchEdit.DisplayMember = displaymember;
                searchEdit.ValueMember = valuemember;
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
        
        public static void displayGridlookupEdit(string query, GridLookUpEdit gridlook, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
          // string query = "SELECT * FROM PrimalCuts";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                gridlook.Properties.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace.ToString());
            }

            finally
            {
                con.Close();
            }
            //gridLookUpEdit1.Properties.DataSource = table;
        }
        public static void displayCheckListBox(string query, GridLookUpEdit gridlook, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            // string query = "SELECT * FROM PrimalCuts";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            try
            {
                adapter.Fill(table);
                gridlook.Properties.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace.ToString());
            }

            finally
            {
                con.Close();
            }
            //gridLookUpEdit1.Properties.DataSource = table;
        }
        /***************************************************************************/
        public static void setConnectionState()//must remove
        {
            if (con.State == ConnectionState.Open)
            {
                connection.Close();
            }
            con.ConnectionString = getConnectionStringProd();
        }

        public static SqlConnection OpenConnection()//must remove
        {
            setConnectionState();
            try
            {
                con.Open();
            }
            catch (SqlException ex)
            {
                Classes.Utilities.displayMessage(ex.Message, MessageBoxIcon.Error);
            }
            return con;
        }

        public static SqlDataReader GetRecord(string sql) //must remove
        {
            try
            {
                com.Connection = OpenConnection();
                com.CommandText = sql;
                reader = com.ExecuteReader();
               
            }
            catch (SqlException ex)
            {
                Classes.Utilities.displayMessage(ex.Message, MessageBoxIcon.Error);
            }
            return reader;
        }
    }
}
