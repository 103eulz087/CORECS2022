using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    class BarcodeSettings
    {
        //CARCASS
        public static String getBarcodeShipmentNo(string barcode)
        {
            return barcode.Substring(0, 5).Trim(); //(10000) 10 10005 50123 0001
        }

        public static String getBarcodeProductCategoryCode(string barcode)
        {
            return barcode.Substring(5, 2).Trim();//(10000) (10) 10005 50123 0001
        }

        public static String getParentBarcodeProductCode(string barcode)
        {
            return barcode.Substring(7, 5).Trim();//10000 10 (10005) 50123 0001 //1008010 10055 1060
        }

        public static String getParentBarcodeQuantity(string barcode)
        {
            string ones, tens, finalqty;
            ones = barcode.Substring(12, 2).Trim();
            tens = barcode.Substring(14, 3).Trim(); //1008010100551060
            finalqty = ones + '.' + tens;
            return finalqty.Trim();
        }

        public static String getParentBarcodeSequenceNumber(string barcode)
        {
            return barcode.Substring(17, 4).Trim();//10000 10 (10005) 50123 0001
        }

        //OTHER PRODUCTS
        public static String getBarcodeProductCode(string barcode)
        {
            return barcode.Substring(0, 2).Trim();
        }

        public static String getBarcodePrimalProductCode(string barcode)
        {
            return barcode.Substring(2, 5).Trim();
        }

        public static String getBarcodeQuantity(string barcode)
        {
            string ones, tens, finalqty;
            ones = barcode.Substring(7, 2).Trim();
            tens = barcode.Substring(9, 3).Trim();
            finalqty = ones + '.' + tens;
            return finalqty.Trim();
        }
    }
}
