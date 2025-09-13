using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    class ClientAccount:GlobalVariables
    {
        //FIELDS
        string acctid, acctname,acctbalance, acctstatus;
        string acctmovementdate;

        //PROPERTIES
        public string AccountID
        {
            get { return acctid; }
            set { acctid = value; }
        }

        public string AccountName
        {
            get { return acctname; }
            set { acctname = value; }
        }

        public string AccountBalance
        {
            get { return acctbalance; }
            set { acctbalance = value; }
        }

        public string AccountStatus
        {
            get { return acctstatus; }
            set { acctstatus = value; }
        }

        public string LastMovementDate
        {
            get { return acctmovementdate; }
            set { acctmovementdate = value; }
        }

        //public DateTime LastMovementDate
        //{
        //    get { return acctmovementdate; }
        //    set { acctmovementdate = value; }
        //}
        public static void loadClientAccounts(ComboBox box)
        {
            Database.GetRecord("SELECT AccountID,AccountName FROM ClientAccounts");
            Classes.ListItems listobj = new ListItems();
            box.Items.Clear();
            while (reader.Read())
            {
                //ListItems(string listname, int listid,string listidchar)
                listobj = new ListItems(reader["AccountName"].ToString(),reader["AccountKey"].ToString());
                box.Items.Add(listobj);
            }
            reader.Close();
        }

        public void loadAccountDetails(string acctid)
        {
            Database.GetRecord("SELECT * FROM ClientAccounts WHERE AccountKey='" + acctid+"'");
            if (reader.Read())
            {
                acctid = reader["AccountKey"].ToString();
                acctname = reader["AccountName"].ToString();
                acctbalance = reader["AccountBalance"].ToString();
                acctstatus = reader["AccountStatus"].ToString();
                acctmovementdate = reader["LastMovementDate"].ToString();
                //acctmovementdate = DateTime.Parse(reader["LastMovementDate"].ToString());
            }
            reader.Close();
        }
        public static String getClientKey(string custid)
        {
            string custkey = "";
            custkey = Database.getSingleQuery("ClientAccounts", "AccountID='" + custid + "'", "AccountKey");
            return custkey;
        }
        public static String getClientID(string custname)
        {
            string custid = "";
            custid = Database.getSingleQuery("ClientAccounts", "AccountName='" + custname + "'", "AccountKey");
            return custid;
        }

        public static String getClientName(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("ClientAccounts", "AccountID='" + getClientID(custid) + "'", "AccountName");
            return custname;
        }

        public static String getClientStatus(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("ClientAccounts", "AccountID='" + getClientID(custid) + "'", "AccountStatus");
            return custname;
        }

        public static String getClientBalance(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("ClientAccounts", "AccountID='" + getClientID(custid) + "'", "AccountBalance");
            return custname;
        }
        public static String getClientCashWalletBalance(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("ClientAccounts", "AccountID='" + getClientID(custid) + "'", "CashWalletBalance");
            return custname;
        }

        public static String getClientLastMovementDate(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("ClientAccounts", "AccountID='" + getClientID(custid) + "'", "LastMovementDate");
            return custname;
        }
    }
}
