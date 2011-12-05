using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ThoughtWorks_SalesTax {
    internal class LineItem {
        private const string AT = " at";
        private const string IMPORTED_KEYWORD = "imported";

        // Regex named groups
        private const string REGEX_GROUP_COUNT = "COUNT";
        private const string REGEX_GROUP_COST = "COST";
        private const string REGEX_GROUP_DESCRIPTION = "DESCRIPTION";

        // Configuration keys
        private const string CONFIG_KEY_INVOICE_TEMPLATE = "InvoiceLineItemTemplate";
        private const string CONFIG_KEY_IMPORT_TAX = "ImportTax";
        private const string CONFIG_KEY_SALES_TAX = "SalesTax";
        private const string CONFIG_KEY_TAX_ROUNDING = "TaxRoundToNearest";

        /// <summary>
        ///   Line item seperator
        /// </summary>
        public Regex mLineItemSeperator = new Regex(@"(?<COUNT>\d+)\s(?<DESCRIPTION>.*)\s(?<COST>\d+\.\d{2})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public LineItem(string pLineItem) {
            Price = new Currency();
            Tax = new Currency();

            ConvertToLineItem(pLineItem);
            CalculateSalesTax();
            CalculateImportTax();
        }

        public int Count { get; set; }

        public string Description { get; set; }

        public Currency Price { get; private set; }

        public Currency Tax { get; private set; }

        /// <summary>
        ///   Includes base item price plus all applicable taxes
        /// </summary>
        public Currency DisplayPrice {
            get {
                var oDisplayCurrency = new Currency();
                oDisplayCurrency.Amount = Price.Amount + Tax.Amount;
                return oDisplayCurrency;
            }
        }

        private void CalculateImportTax() {
            if (!Description.Contains(IMPORTED_KEYWORD)) {
                return;
            }

            var oImportTax = SalesTaxHelper.GetConfigurationValue<decimal>(CONFIG_KEY_IMPORT_TAX);
            var oTaxRoundingAmount = SalesTaxHelper.GetConfigurationValue<decimal>(CONFIG_KEY_TAX_ROUNDING);
            if (!oImportTax.HasValue || !oTaxRoundingAmount.HasValue) {
                throw new ConfigurationErrorsException("No value found for import tax");
            }

            Tax.Amount += SalesTaxHelper.RoundUpToNearest(Price.Amount*oImportTax.Value, oTaxRoundingAmount.Value);
        }

        private void CalculateSalesTax() {
            if (SalesTaxHelper.ItemIsTaxExempt(Description)) {
                return;
            }

            var oSalesTax = SalesTaxHelper.GetConfigurationValue<decimal>(CONFIG_KEY_SALES_TAX);
            var oTaxRoundingAmount = SalesTaxHelper.GetConfigurationValue<decimal>(CONFIG_KEY_TAX_ROUNDING);
            if (!oSalesTax.HasValue || !oTaxRoundingAmount.HasValue) {
                throw new ConfigurationErrorsException("No value found for sales tax");
            }

            Tax.Amount += SalesTaxHelper.RoundUpToNearest(Price.Amount*oSalesTax.Value, oTaxRoundingAmount.Value);
        }

        private void ConvertToLineItem(string pLineItem) {
            var oItemDetails = mLineItemSeperator.Match(pLineItem);

            Count = Convert.ToInt32(oItemDetails.Groups[REGEX_GROUP_COUNT].Value);
            Price.Amount = Convert.ToDecimal(oItemDetails.Groups[REGEX_GROUP_COST].Value)*Count;

            // Remove all occurences of the word "at"
            var oDescription = oItemDetails.Groups[REGEX_GROUP_DESCRIPTION].Value;
            Description = oDescription.EndsWith(AT) ? oDescription.Substring(0, oDescription.Length - AT.Length) : oDescription;
        }

        public override string ToString() {
            return String.Format(SalesTaxHelper.GetConfigurationValue(CONFIG_KEY_INVOICE_TEMPLATE), Count, Description, DisplayPrice);
        }
    }
}