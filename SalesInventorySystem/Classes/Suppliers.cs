
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    class Suppliers
    {
        string acctid,  acctbalance, acctstatus;
        string acctmovementdate;

        //PROPERTIES
        public string SupplierID
        {
            get { return acctid; }
            set { acctid = value; }
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
        public static String getSupplierKey(string custname)
        {
            string custid = "";
            custid = Database.getSingleQuery("Supplier", "SupplierName='" + custname + "'", "SupplierKey");
            return custid;
        }

        public static String getSupplierID(string custname)
        {
            string custid = "";
            custid = Database.getSingleQuery("Supplier", "SupplierName='" + custname + "'", "SupplierID");
            return custid;
        }

        public static String getSupplierName(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("Supplier", "SupplierKey='" + custid + "'", "SupplierName");
            return custname;
        }

        public static String getSupplierStatus(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("SupplierAccounts", "SupplierID='" + getSupplierID(custid) + "'", "AccountStatus");
            return custname;
        }

        public static String getSupplierBalance(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("SupplierAccounts", "SupplierID='" + getSupplierID(custid) + "'", "AccountBalance");
            return custname;
        }

        public static String getSupplierLastMovementDate(string custid)
        {
            string custname = "";
            custname = Database.getSingleQuery("SupplierAccounts", "SupplierID='" + getSupplierID(custid) + "'", "LastMovementDate");
            return custname;
        }
    }
}
