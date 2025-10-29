using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.SalesModel
{
    public class ZReadingDto
    {
        //public long Id { get; set; }                     // bigint
        public long TenantID { get; set; }               // bigint
        public string POSID { get; set; }                // varchar(30)
        public string UserID { get; set; }               // varchar(30)
        public string CounterNo { get; set; }            // varchar(50)
        public decimal BeginningBalance { get; set; }    // money
        public decimal Debit { get; set; }               // money
        public decimal Credit { get; set; }              // money
        public decimal EndingBalance { get; set; }       // money
        public string BeginningSINo { get; set; }        // varchar(20)
        public string EndingSINo { get; set; }           // varchar(20)
        public string BeginningReturnTransNo { get; set; } // varchar(20)
        public string EndingReturnTransNo { get; set; }    // varchar(20)
        public int SoldItems { get; set; }
        public int CancelledItems { get; set; }
        public int VoidItems { get; set; }
        public int VatItems { get; set; }
        public int DiscountItems { get; set; }
        public int SCDiscItems { get; set; }
        public int PWDDiscItems { get; set; }
        public int RegDiscItems { get; set; }
        public decimal TotalCashSales { get; set; }      // money
        public decimal TotalCreditSales { get; set; }    // money
        public decimal TotalSales { get; set; }          // money
        public decimal TotalCancelledSales { get; set; } // money
        public decimal TotalVoidSales { get; set; }      // money
        public decimal TotalReturnedSales { get; set; }  // money
        public decimal TotalDiscount { get; set; }       // money
        public decimal TotalSCDiscount { get; set; }     // money
        public decimal TotalPWDDiscount { get; set; }    // money
        public decimal TotalRegDiscount { get; set; }    // money
        public decimal TotalVatSales { get; set; }       // money
        public decimal VatExemptSale { get; set; }       // money
        public decimal VatableSale { get; set; }         // money
        public decimal VatInput { get; set; }            // money
        public decimal ZeroRatedSale { get; set; }       // money
        public decimal VatAdjustment { get; set; }       // money
        public decimal TotalNetSales { get; set; }       // decimal(18,2)
        public DateTime DateAdded { get; set; }          // date
        public TimeSpan TimeAdded { get; set; }          // time
        public DateTime DateTimeAdded { get; set; }      // datetime
        public DateTime DateUpdated { get; set; }        // date
        public TimeSpan TimeUpdated { get; set; }        // time
        public DateTime DateTimeUpdated { get; set; }    // datetime
    }
}
