using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    class CompanyProfile
    {

        public static String getCompanyName(string name)
        {
            return Database.getSingleQuery("CompanyName", "CompanyName='" + name + "'", "BranchCode");//Database.getSingleData("Branches", "BranchName", branchname, "BranchCode");
        }

        public static String getBranchName(string branchcode)
        {
            //String getSingleData(string tablename, string col, string value,string returnval)
            return Database.getSingleData("Branches", "BranchCode", branchcode, "BranchName");
        }
    }
}
