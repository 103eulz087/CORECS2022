using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public class GlobalVariables
    {
        public static string ponumber;
        public static string strApplicationName = "SPIRE: Sales and Inventory Management System";
        public static string computerName = Environment.MachineName.ToString();
        public static OleDbConnection connection = new OleDbConnection();
        public static OleDbCommand command = new OleDbCommand();
        //public static OleDbDataAdapter dataAdapter;
        public static OleDbDataReader dataReader;
        public static DataSet dataset = new DataSet();
       // public static DataSet ds_TABLES_obj = new ds_TABLES();

        public static SqlConnection con = new SqlConnection();
        public static SqlCommand com = new SqlCommand();
        public static SqlDataReader reader;
        

        //public static long lngFileLength=0;
        //public static byte[] byteImage;

        //public static int[] intUserFeatures;
        public static ArrayList arrayList = new ArrayList();

        public static string strPictureDestination = Application.StartupPath + @"\PICTURES";

        //public static string strAccountNo;
        public static int intUserID;
        public static string strUserName;
        public static string strFullName;
        public static string strPassword;
        public static int intStatus;
        public static int intDeleted;
        public static int intActive;
        public static int intAdmin;
        //public static DateTime dtLastPasswordChangeDate;
        //public static DateTime dtLastLoginDate;
        //public static string strLastLoginWorkStation;

        public static bool blLoginStatus;

        public static int intNoOfCheques;
    }
    public static class GlobalConfig
    {
        public static string Version { get; private set; }
        public static string Token { get; private set; }
        public static string VersionCheckerUrl { get; private set; }

        // Call this once at startup (e.g., in Program.cs or MainForm)
        public static void Load(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("Version="))
                {
                    Version = line.Split('=')[1];
                }
                if (line.StartsWith("VersionCheckerUrl="))
                {
                    VersionCheckerUrl = line.Split('=')[1];
                }
                Match match = Regex.Match(line, @"https:\/\/.*?\/([A-Za-z0-9]+)\/");
                if (match.Success)
                {
                    Token = match.Groups[1].Value;
                }
            }
        }
    }

}
