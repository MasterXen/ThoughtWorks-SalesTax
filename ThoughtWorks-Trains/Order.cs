using System.Collections.Generic;
using System.Text;

namespace ThoughtWorks_SalesTax {
    internal class Order {
        private readonly List<LineItem> mLineItems = new List<LineItem>();
        private readonly Currency mSalesTax = new Currency();

        /// <summary>
        ///   Number of line items currently in this order
        /// </summary>
        public int LineItemsCount {
            get { return mLineItems.Count; }
        }

        public Currency TotalTaxes {
            get { return mSalesTax; }
        }

        public bool AddLineItem(string pLineItem) {
            var oLineItem = new LineItem(pLineItem);
            mLineItems.Add(oLineItem);

            IncrementTotalSalesTax(oLineItem);
            return true;
        }

        public void IncrementTotalSalesTax(LineItem pLineItem) {
            mSalesTax.Amount += pLineItem.Tax.Amount;
        }

        public string PrintInvoice() {
            var oStringBuffer = new StringBuilder();
            var oTotal = new Currency();

            foreach (var oLineItem in mLineItems) {
                oStringBuffer.AppendLine(oLineItem.ToString());
                oTotal.Amount += oLineItem.DisplayPrice.Amount;
            }

            oStringBuffer.AppendLine("Sales Taxes: " + mSalesTax);
            oStringBuffer.AppendLine("Total: " + oTotal).AppendLine();

            return oStringBuffer.ToString();
        }
    }
}