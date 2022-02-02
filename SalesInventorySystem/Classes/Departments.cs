using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class Departments
    {

        public static String getDeptCode(string deptname)
        {
            return Database.getSingleQuery("Departments", "DeptName='" + deptname + "'", "DeptID");//Database.getSingleData("Branches", "BranchName", branchname, "BranchCode");
        }

        public static String getDeptName(string deptcode)
        {
            return Database.getSingleQuery("Departments", "BranchCode='" + deptcode + "'", "DeptName");
        }
    }
}
