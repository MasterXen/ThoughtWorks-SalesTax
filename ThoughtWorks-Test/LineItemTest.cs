using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks_SalesTax;

namespace ThoughtWorks_Test {
    ///<summary>
    ///  This is a test class for LineItemTest and is intended
    ///  to contain all LineItemTest Unit Tests
    ///</summary>
    [TestClass]
    public class LineItemTest {
        private const string MULTIPLE_COUNT_LINE_ITEM_AS_INPUT_STRING = "14 imported box of chocolates at 10.85";
        private const string SINGLE_COUNT_LINE_ITEM_AS_INPUT_STRING = "1 imported box of chocolates at 10.85";

        private const string SINGLE_COUNT_DOMESTIC_LINE_ITEM_AS_INPUT_STRING = "1 magazine at 4.67";
        private const string MULTIPLE_COUNT_DOMESTIC_LINE_ITEM_AS_INPUT_STRING = "3 scented candles at 5.05";

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        ///<summary>
        ///  A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest() {
            var oSingleCountLineItem = new LineItem(SINGLE_COUNT_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual("1 imported box of chocolates : 11.40", oSingleCountLineItem.ToString());

            var oMultiCountLineItem = new LineItem(MULTIPLE_COUNT_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual("14 imported box of chocolates : 159.50", oMultiCountLineItem.ToString());
        }

        ///<summary>
        ///  A test for ConvertToLineItem
        ///</summary>
        [TestMethod]
        [DeploymentItem("ThoughtWorks_SalesTax.exe")]
        public void ConvertToLineItemTest() {
            var oSingleCountLineItem = new LineItem(SINGLE_COUNT_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(1, oSingleCountLineItem.Count);
            Assert.AreEqual("imported box of chocolates", oSingleCountLineItem.Description);
            Assert.AreEqual(new decimal(10.85), oSingleCountLineItem.Price.Amount);

            var oMultiCountLineItem = new LineItem(MULTIPLE_COUNT_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(14, oMultiCountLineItem.Count);
            Assert.AreEqual("imported box of chocolates", oMultiCountLineItem.Description);
            Assert.AreEqual(new decimal(14 * 10.85), oMultiCountLineItem.Price.Amount);
        }

        ///<summary>
        ///  A test for CalculateSalesTax
        ///</summary>
        [TestMethod]
        [DeploymentItem("ThoughtWorks_SalesTax.exe")]
        public void CalculateSalesTaxTest() {
            var oSingleCountLineItem = new LineItem(SINGLE_COUNT_DOMESTIC_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(new decimal(0.50), oSingleCountLineItem.Tax.Amount);

            var oMultiCountLineItem = new LineItem(MULTIPLE_COUNT_DOMESTIC_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(new decimal(1.55), oMultiCountLineItem.Tax.Amount);
        }

        ///<summary>
        ///  A test for CalculateImportTax
        ///</summary>
        [TestMethod]
        [DeploymentItem("ThoughtWorks_SalesTax.exe")]
        public void CalculateImportTaxTest() {
            var oSingleCountLineItem = new LineItem(SINGLE_COUNT_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(new decimal(0.55), oSingleCountLineItem.Tax.Amount);

            var oMultiCountLineItem = new LineItem(MULTIPLE_COUNT_LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(new decimal(7.60), oMultiCountLineItem.Tax.Amount);
        }
    }
}