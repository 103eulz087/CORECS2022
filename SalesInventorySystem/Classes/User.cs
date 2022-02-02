using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    class User
    {

        public static String getUserID(string userid)
        {
            string username = "";
            username = Database.getSingleQuery("Users", "UserID='" + userid + "'", "UserID");
            return username;
        }

        public static String getUserName(string userid)
        {
            string username = "";
            username = Database.getSingleQuery("Users", "UserID='" + userid + "'", "Username");
            return username;
        }

        public static String getUserBranch(string id)
        {
            string branch = "";
            branch = Database.getSingleQuery("Users", "UserID='" + id + "'", "AssignedBranch");
            return branch;
        }

    }
}
