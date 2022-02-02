using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace SalesInventorySystem
{
    public class POSTransaction
    {
        
        /*CLOSED POS TRANSACTION*/
        public static string getBeginningDate(string transcode)
        {
            string datefrom;
            datefrom = Database.getSingleQuery("SalesTransactionSummary", "AccountCode='" + transcode + "' And BranchCode='"+Login.assignedBranch+"' and IsOpen='1'", "TransactionBegin");
            return datefrom;
        }

        public static string getEndDate(string transcode)
        {
            string datefrom;
            datefrom = Database.getSingleQuery("SalesTransactionSummary", "AccountCode='" + transcode + "' And BranchCode='" + Login.assignedBranch + "' and IsOpen='1'", "DateClosed");
            return datefrom;
        }

        public static double getBeginningCash(string transcode)
        {
            double cash;
            cash = Convert.ToDouble(Database.getSingleQuery("SalesTransactionSummary", "AccountCode='" + transcode + "' And BranchCode='" + Login.assignedBranch + "' and IsOpen='1'", "BeginningCash"));
            return cash;
        }

        public static int getTotalSoldItems(string transcode)
        {
            int cash=0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isConfirmed='1' AND isCancelled='0' AND isVoid='0' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalCancelledItems(string transcode)
        {
            int cash=0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isCancelled='1'  And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalVoidItems(string transcode)
        {
            int cash=0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isVoid='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalReturnedItems(string transcode)
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isErrorCorrect='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalCancelledTransactions(string transcode)
        {
            double cash = 0.0;  
            cash = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isCancelled='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static double getTotalVoidTransactions(string transcode)
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isVoid='1'  And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static double getTotalReturnedTransactions(string transcode)
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "' AND isErrorCorrect='1'  And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static int getNoOfDiscountItems(string transcode)
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND DiscountRate > 0 AND isVoid='0' and isCancelled='0' And IsConfirmed='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalDiscount(string transcode)
        {
            double cash = 0.0;  
            cash = Database.getTotalSummation2("BatchSalesSummary", "TransactionCode='" + transcode + "' AND isVoid='0' And Status='SOLD'   And BranchCode='" + Login.assignedBranch + "'", "TotalDiscount");
            return cash;
        }
        public static int getNoOfChargeItems(string transcode)
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND ChargeRate > 0 AND isVoid='0' and isCancelled='0' And IsConfirmed='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalCharge(string transcode)
        {
            double cash = 0.0;  
            cash = Database.getTotalSummation2("BatchSalesSummary", "TransactionCode='" + transcode + "' AND isVoid='0' And Status='SOLD' And BranchCode='" + Login.assignedBranch + "'", "TotalCharge");
            return cash;
        }
        public static int getNoOfVATItems(string transcode)
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TransactionCode='" + transcode + "' AND TaxRate > 0 AND isVoid='0' and isCancelled='0'  And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalTax(string transcode)
        {
            double cash = 0.0;  
            cash = Database.getTotalSummation2("BatchSalesSummary", "TransactionCode='" + transcode + "' AND isVoid='0' And Status='SOLD' And BranchCode='" + Login.assignedBranch + "'", "TotalTax");
            return cash;
        }
        public static double getTotalSales(string transcode)
        {
            double cash;
            cash = Database.getTotalSummation2("BatchSalesSummary", "TransactionCode='" + transcode + "'AND isFloat='0' AND isHold='0' AND isVoid='0'  And BranchCode='" + Login.assignedBranch + "' and PaymentType='Cash'", "TotalAmount");
            return cash;
        }
        public static double getTotalCreditSales(string transcode)
        {
            double cash;
            cash = Database.getTotalSummation2("BatchSalesSummary", "TransactionCode='" + transcode + "'AND isFloat='0' AND isHold='0' AND isVoid='0'  And BranchCode='" + Login.assignedBranch + "' and PaymentType='Credit'", "TotalAmount");
            return cash;
        }

        public static int getTransactionCount(string transcode)
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesSummary", "TransactionCode='" + transcode + "' AND BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }

        public static int getBeginningSINumber(string transcode)
        {
            int cash = 0;
            cash = Database.getBeginningID("BatchSalesSummary", "TransactionCode='" + transcode + "' AND BranchCode='" + Login.assignedBranch + "'", "ReferenceNo");
            return cash;
        }

        public static int getLastOrNo(string transcode)
        {
            int cash = 0;
            cash = Database.getLastID("BatchSalesSummary", "TransactionCode='" + transcode + "' AND BranchCode='" + Login.assignedBranch + "'", "ReferenceNo");
            return cash;
        }

        public static int getBeginningInvoice(string transcode)
        {
            int cash = 0;
            cash = Database.getBeginningID("BatchSalesSummary", "TransactionCode='" + transcode + "' AND BranchCode='" + Login.assignedBranch + "'", "Invoice");
            return cash;
        }

        public static int getLastTransactionNo(string transcode)
        {
            int cash = 0;
            cash = Database.getLastID("BatchSalesSummary", "TransactionCode='" + transcode + "' AND BranchCode='" + Login.assignedBranch + "'", "TransactionCode");
            return cash;
        }

        public static double getVatableSales(string transcode)
        {
            double vatsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "'AND isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return vatsales;
        }

        public static double getVatExemptSales(string transcode)
        {
            double vatsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "'AND isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='0' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return vatsales;
        }

        public static double getNetSalesOfVat(string transcode)
        {
            double vatsales = 0.0,netsales=0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "'AND isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            netsales = vatsales / 1.12;
            return netsales;
        }

        public static double getVatAmount(string transcode)
        {
            double vatsales = 0.0, netsales = 0.0;
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "TransactionCode='" + transcode + "'AND isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            netsales = (vatsales / 1.12) * .12;
            return netsales;
        }

        public static void ClosedTransaction()
        {
            

        }

        public static double getTotalBeginningBalance()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' AND DateOpen='"+DateTime.Now.ToShortDateString()+"' and MachineUsed='"+Environment.MachineName+"'", "TotalCashSales");
            return cash;
        }

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
            cash = Convert.ToDouble(Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' and IsOpen='1'", "BeginningCash"));
            return cash;
        }

        public static int getTotalSoldItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "isConfirmed='1' AND isCancelled='0' AND isVoid='0' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalCancelledItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "isCancelled='1'  And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalVoidItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "isVoid='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static int getTotalReturnedItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "isErrorCorrect='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }



        public static double getTotalCancelledTransactions()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesDetails", "isCancelled='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static double getTotalVoidTransactions()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesDetails", "isVoid='1'  And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static double getTotalReturnedTransactions()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesDetails", "isErrorCorrect='1'  And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            return cash;
        }
        public static int getNoOfDiscountItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "DiscountRate > 0 AND isVoid='0' and isCancelled='0' And IsConfirmed='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalDiscount()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesSummary", "isVoid='0' And Status='SOLD'   And BranchCode='" + Login.assignedBranch + "'", "TotalDiscount");
            return cash;
        }
        public static int getNoOfChargeItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "ChargeRate > 0 AND isVoid='0' and isCancelled='0' And IsConfirmed='1' And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalCharge()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesSummary", "isVoid='0' And Status='SOLD' And BranchCode='" + Login.assignedBranch + "'", "TotalCharge");
            return cash;
        }
        public static int getNoOfVATItems()
        {
            int cash = 0;
            cash = Convert.ToInt32(Database.getCountData("BatchSalesDetails", "TaxRate > 0 AND isVoid='0' and isCancelled='0'  And BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }
        public static double getTotalTax()
        {
            double cash = 0.0;
            cash = Database.getTotalSummation2("BatchSalesSummary", "isVoid='0' And Status='SOLD' And BranchCode='" + Login.assignedBranch + "'", "TotalTax");
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
            cash = Convert.ToInt32(Database.getCountData("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "TransactionCode"));
            return cash;
        }

        public static int getBeginningORNo()
        {
            int cash = 0;
            cash = Database.getBeginningID("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo");
            return cash;
        }

        public static int getLastOrNo()
        {
            int cash = 0;
            cash = Database.getLastID("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo");
            return cash;
        }

        public static int getBeginningInvoice()
        {
            int cash = 0;
            cash = Database.getBeginningID("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "Invoice");
            return cash;
        }

        public static int getLastTransactionNo()
        {
            int cash = 0;
            cash = Database.getLastID("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "TransactionCode");
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
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='0' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
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
            vatsales = Database.getTotalSummation2("BatchSalesDetails", "isConfirmed='1' AND isCancelled='0' AND isVoid='0' and isErrorCorrect='0' AND isVat='1' And BranchCode='" + Login.assignedBranch + "'", "TotalAmount");
            netsales = (vatsales / 1.12) * .12;
            return netsales;
        }

    }
}
