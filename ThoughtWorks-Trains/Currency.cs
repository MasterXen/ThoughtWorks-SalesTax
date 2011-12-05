namespace ThoughtWorks_SalesTax {
    internal class Currency {
        public decimal Amount { get; set; }

        public Symbol Symbol { get; set; }

        /// <summary>
        ///   Formatted dollar amount rounded to two decimal places
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Amount.ToString("F2");
        }
    }

    public enum Symbol {
        USD,
        EURO,
        YEN
    }
}