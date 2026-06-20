using System;

namespace SalesInventorySystem.Classes
{
    /// <summary>
    /// Represents a single stock out item with all required information for insertion
    /// </summary>
    public class StockOutItem
    {
        public string BatchID { get; set; }
        public string BranchCode { get; set; }
        public string DateOut { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsVat { get; set; }
        public string EncodedBy { get; set; }
        public string DateEncoded { get; set; }

        /// <summary>
        /// Calculates the total cost based on quantity and unit cost
        /// </summary>
        public void CalculateTotalCost()
        {
            TotalCost = Quantity * Cost;
        }

        /// <summary>
        /// Validates that all required fields are properly populated
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(BatchID) &&
                   !string.IsNullOrWhiteSpace(BranchCode) &&
                   !string.IsNullOrWhiteSpace(DateOut) &&
                   !string.IsNullOrWhiteSpace(ProductCode) &&
                   !string.IsNullOrWhiteSpace(Description) &&
                   !string.IsNullOrWhiteSpace(Barcode) &&
                   Quantity > 0 &&
                   Cost >= 0 &&
                   TotalCost >= 0 &&
                   !string.IsNullOrWhiteSpace(EncodedBy) &&
                   !string.IsNullOrWhiteSpace(DateEncoded);
        }
    }
}
