using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class Locations
    {
        public static String getLocationID(string deptname)
        {
            return Database.getSingleQuery("Location", "LocationName='" + deptname + "'", "LocationID");//Database.getSingleData("Branches", "BranchName", branchname, "BranchCode");
        }

        public static String getLocationName(string deptcode)
        {
            return Database.getSingleQuery("Location", "LocationID='" + deptcode + "'", "LocationName");
        }
    }
}
