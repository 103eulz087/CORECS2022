using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.HotelManagement.Classes
{
    class HotelIDGenerator
    {
        public static int getGuestIDNumber()
        {
            int id = Database.getLastID("GuestInfo", "GuestID <> ''", "GuestID", Database.getCustomizeConnection());
            if (id != 0)
                return ++id;
            else
                return 10000;
        }
    }
}
