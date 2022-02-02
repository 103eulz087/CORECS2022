using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace SalesInventorySystem.Classes
{
    class DBupdater
    {
        static RegistryKey regkey;
        static string constring;
        public static SqlConnection getConnection()
        {
            regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettingsUpdater");
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

        public static void ExecuteQuery(string query, string msg)
        {
            SqlConnection con = getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            MessageBox.Show(msg);
            con.Close();
        }

        //********RYAN VIAJEDOR*****************************************************************************
        public static int getCTRVersion(string tableName, string id, string cond) //, string condition
        {
            int i = 0;
            SqlConnection con = getConnection();
            con.Open();
            string query = "SELECT CAST(" + id + " as int) AS CC FROM " + tableName + " WHERE Company='" + cond + "'"; //" WHERE " + condition + 
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        //i = reader.GetInt32(1);
                        i = Convert.ToInt32(reader["CC"].ToString());
                        // i = int.Parse(reader["CC"].ToString());
                        //  i = reader.GetInt32(reader.GetOrdinal("CC"));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
                // throw new Exception(ex.StackTrace.ToString());
            }
            finally
            {
                con.Close();
            }
            return i;
        }
        //********/RYAN VIAJEDOR****************************************************************************
    }
}
