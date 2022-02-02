using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    class Customers
    {
        public static String getCustKey(string cust)
        {
            string custid = "";
            custid = Database.getSingleQuery("Customers", "CustomerName='" + cust + "'", "CustomerKey");
            return custid;
        }
        public static String getCustID(string custname)
        {
            string custid = "";
            custid = Database.getSingleQuery("Customers", "CustomerName='" + custname + "'", "CustomerID");
            return custid;
        }

        public static String getCustAccountID(string custname)
        {
            string custid = "";
            custid = Database.getSingleQuery("ClientAccounts", "AccountName='" + custname + "'", "AccountID");
            return custid;
        }

        public static String getCustName(int id)
        {
            string custname = "";
            custname = Database.getSingleQuery("Customers", "CustomerID='" + id + "'", "CustomerName");
            return custname;
        }

        public static String getCustBranch(string custid)
        {
            string custbranch = "";
            custbranch = Database.getSingleQuery("Customers", "CustomerID='" + custid + "'", "BranchCode");
            return custbranch;
        }

        public static String getCustAddress(string id)
        {
            string custname = "";
            custname = Database.getSingleQuery("Customers", "CustomerID='" + id + "'", "CustomerAddress");
            return custname;
        }

        public static void populateCustomer(System.Windows.Forms.ComboBox box)
        {
            Database.displayComboBoxItems("Customers", "CustomerName", box);
        }
    }
}
