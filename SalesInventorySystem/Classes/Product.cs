using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    class Product
    {
        public static String getProductCategoryCode(String ProductName)
        {
            string str = "";
            str = Database.getSingleQuery("ProductCategory", "Description='" + ProductName + "'", "ProductCategoryID");
            return str;
        }
        public static String getProductCategoryCodeByPcode(String productcode)
        {
            string str = "";
            str = Database.getSingleQuery("Products", "ProductCode='" + productcode + "' and BranchCode='"+Login.assignedBranch+"'", "ProductCategoryCode");
            return str;
        }
        public static String getProductCategoryName(String id)
        {
            string str = "";
            str = Database.getSingleQuery("ProductCategory", "ProductCategoryID='" + id + "'", "Description");
            return str;
        }
        public static String getProductCode(String ProductName,String ProductCategoryCode)
        {
            string str;
            // str = Database.getSingleQuery("Products", "Description='" + ProductName + "' AND ProductCategoryCode='" + getProductCategoryCode(ProductCategoryCode) + "'", "ProductCode");
            str = Database.getSingleQuery("Products", "Description='" + ProductName + "' AND ProductCategoryCode='" + ProductCategoryCode + "' and BranchCode='"+Login.assignedBranch+"'", "ProductCode");
            return str;
        }
        public static String getProductName(String id, String ProductCategoryCode)
        {
            string str;
            str = Database.getSingleQuery("Products", "ProductCode='" + id + "' AND ProductCategoryCode='" + getProductCategoryCode(ProductCategoryCode) + "' and BranchCode='"+Login.assignedBranch+"'", "Description");
            return str;
        }
        public static void displayProductCategoryComboBoxItems(System.Windows.Forms.ComboBox box)
        {
            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", box);
        }
        public static void displayProductComboBoxItems(System.Windows.Forms.ComboBox box, String ProductCategoryName, string branchcode)
        {
            Database.displayComboBoxItems("SELECT Description FROM Products WHERE BranchCode='" + branchcode + "' AND ProductCategoryCode='" + getProductCategoryCode(ProductCategoryName) + "' ORDER BY Description", "Description", box);
        }
        public static void displayProductComboBoxItems(System.Windows.Forms.ComboBox box, String ProductCategoryCode, String isProductstat, string branchcode)
        {
            Database.displayComboBoxItems("SELECT Description FROM Products WHERE BranchCode='" + branchcode + "' AND isPrimalCut='" + isProductstat + "' AND ProductCategoryCode='" + getProductCategoryCode(ProductCategoryCode) + "' ORDER BY Description", "Description", box);
        }
    }
}
