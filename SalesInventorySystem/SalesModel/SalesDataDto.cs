using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.SalesModel
{
    public class SalesDataDto
    {
        public long TenantID { get; set; }
        public string POSID { get; set; }
        public string OrderNo { get; set; }
        public string UserID { get; set; }
        public string CustomerName { get; set; }
        public int TotalItem { get; set; }
        public int TotalItemSold { get; set; }
        public int TotalItemCancelled { get; set; }
        public int TotalItemVoid { get; set; }
        public int TotalItemReturned { get; set; }
        public int TotalItemDiscount { get; set; }
        public int TotalVatableItems { get; set; }
        public int TotalNonVatableItems { get; set; }
        public decimal TotalSoldAmount { get; set; }
        public decimal TotalCancelledAmount { get; set; }
        public decimal TotalVoidAmount { get; set; }
        public decimal TotalReturnedAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalCharge { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalVATSale { get; set; }
        public decimal TotalVATExemptSale { get; set; }
        public decimal TotalVatableSale { get; set; }
        public decimal TotalZeroRatedSale { get; set; }
        public char PaymentType { get; set; }
        public decimal AmountTendered { get; set; }
        public decimal AmountChange { get; set; }
        public bool isFloat { get; set; }
        public bool isHold { get; set; }
        public bool isVoid { get; set; }
        public char Status { get; set; }
        public char DiscountType { get; set; }
        public string SeniorControlNo { get; set; }
        public string SeniorName { get; set; }
        public decimal SeniorDiscount { get; set; }
        public string PwdIDNo { get; set; }
        public string PwdName { get; set; }
        public decimal PwdDiscountAmount { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public TimeSpan TimeUpdated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }

}
