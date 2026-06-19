using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    public class EJournalGenerator
    {
        #region Models

        public class EJournalHeader
        {
            public string BranchCode { get; set; }
            public string ReferenceNo { get; set; }
            public int CashierTransNo { get; set; }
            public string CustomerNo { get; set; }
            public string Invoice { get; set; }
            public DateTime TransDate { get; set; }
            public string PreparedBy { get; set; }
            public string CashierFullName { get; set; }
            public string MachineUsed { get; set; }

            public decimal SubTotal { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TotalVATSale { get; set; }
            public decimal TotalVATExemptSale { get; set; }
            public decimal TotalVatableSale { get; set; }
            public decimal AmountTendered { get; set; }
            public decimal AmountChange { get; set; }
            public string PaymentType { get; set; }
            public string Status { get; set; }

            public string DiscountType { get; set; }
            public decimal DiscountAmount { get; set; }
            public string DiscName { get; set; }
            public string DiscIDNo { get; set; }
            public decimal DiscountPercentage { get; set; }
            public decimal VatAdjustment { get; set; }
            public decimal VatExemptAdj { get; set; }

            public decimal TotalPerItemDiscount { get; set; }
            public decimal TotalVatItems { get; set; }
            public decimal NetOfVatInNonDiscountedItems { get; set; }
            public decimal NetOfVatInDiscountedItems { get; set; }
        }

        public class EJournalItem
        {
            public string ReferenceNo { get; set; }
            public int SequenceNumber { get; set; }
            public string Description { get; set; }
            public decimal QtySold { get; set; }
            public decimal SellingPrice { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal DiscountTotal { get; set; }
            public bool IsVat { get; set; }
            public DateTime DateOrder { get; set; }
        }

        public class DiscountComputation
        {
            public decimal AmountDue { get; set; }
            public decimal LessVat { get; set; }
            public decimal NetOfVat { get; set; }
            public decimal LessDiscount { get; set; }
            public decimal NetDiscount { get; set; }
            public decimal AddVat { get; set; }
            public decimal FinalTotal { get; set; }
        }

        #endregion

        #region Public Entry Method

        public void GenerateEJournal(DateTime transDate, string branchCode, string machineUsed, string cashier)
        {
            string footerText = File.ReadAllText(Path.Combine(Application.StartupPath, "FOOTER.txt"));

            List<EJournalHeader> headers;
            List<EJournalItem> items;

            GetEJournalData(transDate, branchCode, machineUsed, cashier, out headers, out items);

            if (headers == null || headers.Count == 0)
            {
                MessageBox.Show("No SOLD transactions found for the selected date / branch / machine / cashier.");
                return;
            }

            Dictionary<string, List<EJournalItem>> itemsByReceipt = items
                .GroupBy(x => x.ReferenceNo)
                .ToDictionary(g => g.Key, g => g.OrderBy(i => i.SequenceNumber).ToList());

            StringBuilder sb = new StringBuilder();

            // ESC/POS opener - drawer kick / initialization codes
            sb.Append((char)27);
            sb.Append((char)112);
            sb.Append((char)0);
            sb.Append((char)25);

            for (int i = 0; i < headers.Count; i++)
            {
                EJournalHeader header = headers[i];
                List<EJournalItem> receiptItems = new List<EJournalItem>();

                if (itemsByReceipt.ContainsKey(header.ReferenceNo))
                    receiptItems = itemsByReceipt[header.ReferenceNo];

                sb.Append(BuildReceiptText(header, receiptItems, footerText));
            }

            string fileName = headers[headers.Count - 1].CashierTransNo.ToString() + "_E-JOURNAL.txt";

            SaveJournalFile(transDate, cashier, fileName, sb.ToString());
        }

        #endregion

        #region Database Load

        private void GetEJournalData(
            DateTime transDate,
            string branchCode,
            string machineUsed,
            string cashier,
            out List<EJournalHeader> headers,
            out List<EJournalItem> items)
        {
            headers = new List<EJournalHeader>();
            items = new List<EJournalItem>();

            using (SqlConnection con = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand("dbo.usp_GetEJournalData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("@TransDate", SqlDbType.Date).Value = transDate.Date;
                cmd.Parameters.Add("@BranchCode", SqlDbType.Char, 3).Value = branchCode;
                cmd.Parameters.Add("@MachineUsed", SqlDbType.VarChar, 100).Value = machineUsed;
                cmd.Parameters.Add("@Cashier", SqlDbType.VarChar, 50).Value = cashier;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Result set 1 - header/summary
                    while (reader.Read())
                    {
                        EJournalHeader header = new EJournalHeader();
                        header.BranchCode = reader["BranchCode"].ToString();
                        header.ReferenceNo = reader["ReferenceNo"].ToString();
                        header.CashierTransNo = ToInt(reader["CashierTransNo"]);
                        header.CustomerNo = reader["CustomerNo"].ToString();
                        header.Invoice = reader["Invoice"].ToString();
                        header.TransDate = ToDateTime(reader["TransDate"]);
                        header.PreparedBy = reader["PreparedBy"].ToString();
                        header.CashierFullName = reader["CashierFullName"].ToString();
                        header.MachineUsed = reader["MachineUsed"].ToString();

                        header.SubTotal = ToDecimal(reader["SubTotal"]);
                        header.TotalAmount = ToDecimal(reader["TotalAmount"]);
                        header.TotalVATSale = ToDecimal(reader["TotalVATSale"]);
                        header.TotalVATExemptSale = ToDecimal(reader["TotalVATExemptSale"]);
                        header.TotalVatableSale = ToDecimal(reader["TotalVatableSale"]);
                        header.AmountTendered = ToDecimal(reader["AmountTendered"]);
                        header.AmountChange = ToDecimal(reader["AmountChange"]);
                        header.PaymentType = reader["PaymentType"].ToString();
                        header.Status = reader["Status"].ToString();

                        header.DiscountType = reader["DiscountType"].ToString();
                        header.DiscountAmount = ToDecimal(reader["DiscountAmount"]);
                        header.DiscName = reader["DiscName"].ToString();
                        header.DiscIDNo = reader["DiscIDNo"].ToString();
                        header.DiscountPercentage = NormalizeDiscountPercentage(ToDecimal(reader["DiscountPercentage"]));
                        header.VatAdjustment = ToDecimal(reader["VatAdjustment"]);
                        header.VatExemptAdj = ToDecimal(reader["VatExemptAdj"]);

                        header.TotalPerItemDiscount = ToDecimal(reader["TotalPerItemDiscount"]);
                        header.TotalVatItems = ToDecimal(reader["TotalVatItems"]);
                        header.NetOfVatInNonDiscountedItems = ToDecimal(reader["NetOfVatInNonDiscountedItems"]);
                        header.NetOfVatInDiscountedItems = ToDecimal(reader["NetOfVatInDiscountedItems"]);

                        headers.Add(header);
                    }

                    // Result set 2 - item details
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            EJournalItem item = new EJournalItem();
                            item.ReferenceNo = reader["ReferenceNo"].ToString();
                            item.SequenceNumber = ToInt(reader["SequenceNumber"]);
                            item.Description = reader["Description"].ToString();
                            item.QtySold = ToDecimal(reader["QtySold"]);
                            item.SellingPrice = ToDecimal(reader["SellingPrice"]);
                            item.TotalAmount = ToDecimal(reader["TotalAmount"]);
                            item.DiscountTotal = ToDecimal(reader["DiscountTotal"]);
                            item.IsVat = ToBool(reader["isVat"]);
                            item.DateOrder = ToDateTime(reader["DateOrder"]);

                            items.Add(item);
                        }
                    }
                }
            }
        }

        #endregion

        #region Receipt Builder

        private string BuildReceiptText(EJournalHeader header, List<EJournalItem> items, string footerText)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Classes.ReceiptSetup.doHeaderB2(header.BranchCode, header.MachineUsed));
            sb.Append(Classes.ReceiptSetup.doTitle("SALES INVOICE"));
            sb.Append(Classes.ReceiptSetup.doHeaderDetailsX(
                header.CashierFullName,
                header.ReferenceNo,
                " ",
                null,
                null,
                null,
                null,
                header.TransDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ""
            ));

            sb.Append(HelperFunction.createDottedLine());
            sb.Append(Environment.NewLine);

            decimal receiptVatItems = 0m;

            for (int i = 0; i < items.Count; i++)
            {
                EJournalItem item = items[i];

                if (item.QtySold <= 0)
                    continue;

                string addV = item.IsVat ? "V" : "";
                string addD = item.DiscountTotal > 0 ? "   - (Less: Discount)" : "";

                if (item.IsVat)
                    receiptVatItems += item.TotalAmount;

                sb.Append(HelperFunction.PrintLeftText(item.Description));
                sb.Append(Environment.NewLine);

                decimal cleanBalance = item.TotalAmount + item.DiscountTotal;

                string leftText = "   - " + item.QtySold.ToString("0.###") + " @ " + item.SellingPrice.ToString("0.00");
                string rightText = " " + HelperFunction.convertToNumericFormat(cleanBalance) + addV;

                sb.Append(HelperFunction.PrintLeftRigthText(leftText, rightText));
                sb.Append(Environment.NewLine);

                if (item.DiscountTotal > 0)
                {
                    sb.Append(HelperFunction.PrintLeftRigthText(addD, "(" + item.DiscountTotal.ToString("0.00") + ")"));
                    sb.Append(Environment.NewLine);
                }

                bool hasDiscount = !string.IsNullOrWhiteSpace(header.DiscountType);
                if (hasDiscount && header.DiscountType.Trim().ToUpper() == "REGULAR")
                {
                    sb.Append(HelperFunction.PrintLeftText("   - (Less: Discount " + FormatPercent(header.DiscountPercentage) + ")"));
                    sb.Append(Environment.NewLine);
                }
            }

            sb.Append(HelperFunction.PrinttoRight("----------"));
            sb.Append(Environment.NewLine);

            decimal globalAmountDue = header.SubTotal;

            sb.Append(HelperFunction.PrintLeftRigthText("TOTAL DUE:", header.SubTotal.ToString("0.00")));
            sb.Append(Environment.NewLine);
            sb.Append(HelperFunction.PrinttoRight("=========="));
            sb.Append(Environment.NewLine);

            DiscountComputation dc = ComputeDiscountTotals(header, receiptVatItems);

            if (header.DiscountAmount > 0)
            {
                AppendDiscountSection(sb, header, dc);
                globalAmountDue = dc.AmountDue;
            }

            decimal newAmountTender = GetRoundedTenderAmount(globalAmountDue);
            decimal newChange = Math.Round(newAmountTender - globalAmountDue, 2);

            if (header.PaymentType != null && header.PaymentType.Equals("Credit", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(HelperFunction.PrintLeftRigthText("TENDERED:", newAmountTender.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.Append(HelperFunction.PrintLeftRigthText("TENDERED:", newAmountTender.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("CHANGE  :", newChange.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }

            sb.Append(HelperFunction.PrintLeftRigthText("VATable Sales", header.TotalVatableSale.ToString("0.00")));
            sb.Append(Environment.NewLine);
            sb.Append(HelperFunction.PrintLeftRigthText("VAT Amount", header.TotalVATSale.ToString("0.00")));
            sb.Append(Environment.NewLine);
            sb.Append(HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", header.TotalVATExemptSale.ToString("0.00")));
            sb.Append(Environment.NewLine);
            sb.Append(HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00"));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            if (header.PaymentType != null && header.PaymentType.Equals("Credit", StringComparison.OrdinalIgnoreCase))
            {
                string cardno = "";
                string cardtype = "";
                string cardrefno = "";

                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card"));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Card Type: " + cardtype));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Reference No.: " + cardrefno));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);
            }

            sb.Append(HelperFunction.PrintCenterText(footerText));
            sb.Append(Environment.NewLine);
            sb.Append(HelperFunction.LastPagePaper());

            return sb.ToString();
        }

        #endregion

        #region Discount Logic

        private DiscountComputation ComputeDiscountTotals(EJournalHeader header, decimal receiptVatItems)
        {
            DiscountComputation result = new DiscountComputation();

            decimal amountDue = header.SubTotal;
            decimal lessVat = 0m;
            decimal netOfVat = 0m;
            decimal lessDisc = 0m;
            decimal netDisc = 0m;
            decimal addVat = 0m;
            decimal finalTotal = header.SubTotal;

            if (header.DiscountAmount <= 0)
            {
                result.AmountDue = amountDue;
                result.LessVat = lessVat;
                result.NetOfVat = netOfVat;
                result.LessDiscount = lessDisc;
                result.NetDiscount = netDisc;
                result.AddVat = addVat;
                result.FinalTotal = finalTotal;
                return result;
            }

            string dtype = "";
            if (header.DiscountType != null)
                dtype = header.DiscountType.Trim().ToUpper();

            if (dtype == "SENIOR" || dtype == "PWD")
            {
                netOfVat = Math.Round(header.NetOfVatInDiscountedItems, 2);
                lessVat = Math.Round(netOfVat * 0.12m, 2);
                lessDisc = Math.Round(netOfVat * header.DiscountPercentage, 2);
                netDisc = Math.Round(netOfVat - lessDisc, 2);
                addVat = Math.Round(netDisc * 0.12m, 2);
                finalTotal = Math.Round(netDisc + addVat, 2);

                decimal vatAdj = Math.Round(lessDisc * 0.12m, 2);
                amountDue = Math.Round((header.SubTotal - header.DiscountAmount) - vatAdj, 2);
            }
            else if (dtype == "REGULAR")
            {
                netOfVat = Math.Round(receiptVatItems / 1.12m, 2);
                lessVat = Math.Round(netOfVat * 0.12m, 2);
                lessDisc = Math.Round(netOfVat * header.DiscountPercentage, 2);
                netDisc = Math.Round(netOfVat - lessDisc, 2);
                addVat = Math.Round(netDisc * 0.12m, 2);
                finalTotal = Math.Round(netDisc + addVat, 2);

                decimal vatAdj = Math.Round(lessDisc * 0.12m, 2);
                amountDue = Math.Round((header.SubTotal - header.DiscountAmount) - vatAdj, 2);
            }

            result.AmountDue = amountDue;
            result.LessVat = lessVat;
            result.NetOfVat = netOfVat;
            result.LessDiscount = lessDisc;
            result.NetDiscount = netDisc;
            result.AddVat = addVat;
            result.FinalTotal = finalTotal;

            return result;
        }

        private void AppendDiscountSection(StringBuilder sb, EJournalHeader header, DiscountComputation dc)
        {
            sb.Append(HelperFunction.createDottedLine());
            sb.Append(Environment.NewLine);

            string dtype = "";
            if (header.DiscountType != null)
                dtype = header.DiscountType.Trim().ToUpper();

            if (dtype == "SENIOR")
            {
                sb.Append(HelperFunction.PrintLeftText("SENIOR DISCOUNT"));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("OSCA SC/ID: " + header.DiscIDNo));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Name: " + header.DiscName));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Discount Amount:", header.DiscountAmount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Signature: _______________"));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);

                sb.Append(HelperFunction.PrintLeftRigthText("Less VAT:", dc.LessVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Net of VAT:", dc.NetOfVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Less SC Discount:", dc.LessDiscount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Net SC Discount:", dc.NetDiscount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Add VAT:", dc.AddVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Total:", dc.FinalTotal.ToString("0.00")));
                sb.Append(Environment.NewLine);

                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("AMOUNT DUE:", dc.AmountDue.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            else if (dtype == "PWD")
            {
                sb.Append(HelperFunction.PrintLeftText("PWD DISCOUNT"));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("PWD ID: " + header.DiscIDNo));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Name: " + header.DiscName));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Discount Amount:", header.DiscountAmount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftText("Signature: _______________"));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);

                sb.Append(HelperFunction.PrintLeftRigthText("Less VAT:", dc.LessVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Net of VAT:", dc.NetOfVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Less PWD Discount:", dc.LessDiscount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Net PWD Discount:", dc.NetDiscount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Add VAT:", dc.AddVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Total:", dc.FinalTotal.ToString("0.00")));
                sb.Append(Environment.NewLine);

                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("AMOUNT DUE:", dc.AmountDue.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            else if (dtype == "REGULAR")
            {
                sb.Append(HelperFunction.PrintLeftText("REGULAR DISCOUNT"));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Discount Amount:", header.DiscountAmount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);

                sb.Append(HelperFunction.PrintLeftRigthText("Less VAT:", dc.LessVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Net of VAT:", dc.NetOfVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Less Reg Discount:", dc.LessDiscount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Net Reg Discount:", dc.NetDiscount.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Add VAT:", dc.AddVat.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("Total:", dc.FinalTotal.ToString("0.00")));
                sb.Append(Environment.NewLine);

                sb.Append(HelperFunction.createDottedLine());
                sb.Append(Environment.NewLine);
                sb.Append(HelperFunction.PrintLeftRigthText("AMOUNT DUE:", dc.AmountDue.ToString("0.00")));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
        }

        #endregion

        #region File Save

        private void SaveJournalFile(DateTime transDate, string cashier, string fileName, string content)
        {
            string folderPath = Path.Combine(
                @"C:\ProgramFilesTest\DailySales",
                transDate.ToString("yyyyMMdd"),
                cashier,
                "TransactionJournalSummary"
            );

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fullPath = Path.Combine(folderPath, fileName);

            // Overwrite existing file to avoid duplicate appended content
            File.WriteAllText(fullPath, content, Encoding.UTF8);
        }

        #endregion

        #region Helpers

        private decimal ToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0m;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;

            return 0m;
        }

        private int ToInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            int result;
            if (int.TryParse(value.ToString(), out result))
                return result;

            decimal d;
            if (decimal.TryParse(value.ToString(), out d))
                return Convert.ToInt32(d);

            return 0;
        }

        private bool ToBool(object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            bool result;
            if (bool.TryParse(value.ToString(), out result))
                return result;

            int number;
            if (int.TryParse(value.ToString(), out number))
                return number == 1;

            return false;
        }

        private DateTime ToDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
                return DateTime.MinValue;

            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return DateTime.MinValue;
        }

        private decimal NormalizeDiscountPercentage(decimal discountPercentage)
        {
            if (discountPercentage > 1m)
                return discountPercentage / 100m;

            return discountPercentage;
        }

        private string FormatPercent(decimal rate)
        {
            return (rate * 100m).ToString("0.##") + "%";
        }

        private decimal GetRoundedTenderAmount(decimal amount)
        {
            decimal[] denominations = new decimal[] { 20m, 50m, 100m, 200m, 500m, 1000m };
            decimal best = decimal.MaxValue;

            for (int i = 0; i < denominations.Length; i++)
            {
                decimal d = denominations[i];
                decimal rounded = Math.Ceiling(amount / d) * d;

                if (rounded < best)
                    best = rounded;
            }

            if (best == decimal.MaxValue)
                return amount;

            return best;
        }

        #endregion
    }
}