using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    class ListItems
    {
        //private int listIDInt;
        private string listIDChar,listName;

        public ListItems(string listname, string listidchar)
        {
          //  listIDInt = listid;
            listName = listname;
            listIDChar = listidchar;
        }

        public ListItems()
        {
           // listIDInt = 0;
            listName = string.Empty;
            listIDChar = string.Empty;
        }

        //public int ListID
        //{
        //    get { return listIDInt; }
        //    set { listIDInt = value; }
        //}

        public string ListIDChar
        {
            get { return listIDChar; }
            set { listIDChar = value; }
        }

        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }

        public override string ToString()
        {
            return listName;
        }
    }
}
