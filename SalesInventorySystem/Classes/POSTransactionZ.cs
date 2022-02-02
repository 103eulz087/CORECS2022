using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class POSTransactionZ
    {
        /*CLOSED POS TRANSACTION*/
        public static string getBeginningDate()
        {
            string datefrom;
            datefrom = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' and IsOpen='1'", "TransactionBegin");
            return datefrom;
        }

        public static string getEndDate()
        {
            string datefrom;
            datefrom = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' and IsOpen='1'", "DateClosed");
            return datefrom;
        }

        public static double getBeginningCash()
        {
            double cash;
            cash = Convert.ToDouble(Database.getSingleQuery("SalesTransactionSummary", " BranchCode='" + Login.assignedBranch + "' and IsOpen='1'", "BeginningCash"));
            return cash;
        }

        public static int getTotalSoldItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " isConfirmed='1' AND isCancelled='0' AND isVoid='0' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalCancelledItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " isCancelled='1'  And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalVoidItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " isVoid='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalReturnedItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " isErrorCorrect='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalCancelledTransactions()
        {
            double cash = 0.0;  // public static double getTotalSummation(string tablename, string col, string value,string id)
            cash = Database.getTotalSummation2("BatchSalesDetails", " isCancelled='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static double getTotalVoidTransactions()
        //public static string getTotalVoidTransactions()
        {
            double cash = 0.0;// public static double getTotalSummation(string tablename, string col, string value,string id)
            cash = Database.getTotalSummation2("BatchSalesDetails", " isVoid='1'  And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static double getTotalReturnedTransactions()
        //public static string getTotalVoidTransactions()
        {
            double cash = 0.0;// public static double getTotalSummation(string tablename, string col, string value,string id)
            cash = Database.getTotalSummation2("BatchSalesDetails", " isErrorCorrect='1'  And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static int getNoOfDiscountItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " DiscountRate > 0 AND isVoid='0' and isCancelled='0' And IsConfirmed='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalDiscount()
        {
            double cash = 0.0;  // public static double getTotalSummation(string tablename, string col, string value,string id)
            cash = Database.getTotalSummation2("BatchSalesSummary", " isVoid='0' And Status='SOLD'   And BranchCode='" + Login.assignedBranch + "'", "TotalDiscount");
            return cash;
        }
        public static int getNoOfChargeItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " ChargeRate > 0 AND isVoid='0' and isCancelled='0' And IsConfirmed='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalCharge()
        {
            double cash = 0.0;  // public static double getTotalSummation(string tablename, string col, string value,string id)
            cash = Database.getTotalSummation2("BatchSalesSummary", " isVoid='0' And Status='SOLD' And BranchCode='" + Login.assignedBranch + "'", "TotalCharge");
            return cash;
        }
        public static int getNoOfVATItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", " TaxRate > 0 AND isVoid='0' and isCancelled='0'  And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalTax()
        {
            double cash = 0.0;  // public static double getTotalSummation(string tablename, string col, string value,string id)
            cash = Database.getTotalSummation2("BatchSalesSummary", " isVoid='0' And Status='SOLD' And BranchCode='" + Login.assignedBranch + "'", "TotalTax");
            return cash;
        }
        public static double getTotalSales()
        {
            double cash;
            cash = Database.getTotalSummation2("BatchSalesSummary", "isFloat='0' AND isHold='0' AND isVoid='0'  And BranchCode='" + Login.assignedBranch + "' and PaymentType='Cash'", "TotalAmount");
            return cash;
        }
        public static double getTotalCreditSales()
        {
            double cash;
            cash = Database.getTotalSummation2("BatchSalesSummary", "isFloat='0' AND isHold='0' AND isVoid='0'  And BranchCode='" + Login.assignedBranch + "' and PaymentType='Credit'", "TotalAmount");
            return cash;
        }

        public static int getTransactionCount()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesSummary", " BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }

        //public static int getBeginningORNo()
        //{
        //    int cash = 0;
        //    cash = Database.getBeginningID("BatchSalesSummary", " BranchCode='" + Login.assignedBranch + "'", "ReferenceNo");
        //    return cash;
        //}

        public static int getLastOrNo()
        {
            int cash = 0;
            cash = Database.getLastID("BatchSalesSummary", " BranchCode='" + Login.assignedBranch + "'", "ReferenceNo");
            return cash;
        }

        //public static int getBeginningInvoice()
        //{
        //    int cash = 0;
        //    cash = Database.getBeginningID("BatchSalesSummary", " BranchCode='" + Login.assignedBranch + "'", "Invoice");
        //    return cash;
        //}

        public static int getLastTransactionNo()
        {
            int cash = 0;
            cash = Database.getLastID("BatchSalesSummary", " BranchCode='" + Login.assignedBranch + "'", "TransactionCode");
            return cash;
        }

        public static double getVatableSales()
        {
            double vatsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return vatsales;
        }

        public static double getVatExemptSales()
        {
            double vatsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", " isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='0' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return vatsales;
        }

        public static double getNetSalesOfVat()
        {
            double vatsales = 0.0, netsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            netsales = vatsales / 1.12;
            return netsales;
        }

        public static double getVatAmount()
        {
            double vatsales = 0.0, netsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", " isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            netsales = (vatsales / 1.12) * .12;
            return netsales;
        }
    }
}
