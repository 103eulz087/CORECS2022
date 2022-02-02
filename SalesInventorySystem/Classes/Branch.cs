using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    class Branch
    {
        public static String getBranchCode(string branchname)
        {
            return Database.getSingleQuery("Branches", "BranchName='" + branchname + "'", "BranchCode");//Database.getSingleData("Branches", "BranchName", branchname, "BranchCode");
        }

        public static String getBranchName(string branchcode)
        {
            //String getSingleData(string tablename, string col, string value,string returnval)
            //return Database.getSingleData("Branches", "BranchCode", branchcode, "BranchName");
            return Database.getSingleQuery("Branches", "BranchCode='" + branchcode + "'", "BranchName");
        }

        public static String getBranchAddress(string branchcode)
        {
            return Database.getSingleData("Branches", "BranchCode", branchcode, "Address");
        }

        public static String getBranchEmailAddress(string branchcode)
        {
            return Database.getSingleData("Branches", "BranchCode", branchcode, "EmailAddress");
        }
        public static void displayBranchComboBoxItems(System.Windows.Forms.ComboBox box)
        {
            Database.displayComboBoxItems("SELECT * FROM Branches", "BranchCode", box);
        }
        public static void displayBranchNameComboBoxItems(System.Windows.Forms.ComboBox box)
        {
            Database.displayComboBoxItems("SELECT * FROM Branches", "BranchName", box);
        }

    }
}
