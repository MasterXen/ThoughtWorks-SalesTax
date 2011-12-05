using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks_SalesTax;

namespace ThoughtWorks_Test {
    ///<summary>
    ///  This is a test class for OrderTest and is intended
    ///  to contain all OrderTest Unit Tests
    ///</summary>
    [TestClass]
    public class OrderTest {
        private const string LINE_ITEM_AS_INPUT_STRING = "14 imported chocolates at 30.85";
        private readonly LineItem mLineItem;

        public OrderTest() {
            mLineItem = new LineItem(LINE_ITEM_AS_INPUT_STRING);
        }

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        ///<summary>
        ///  A test for AddLineItem
        ///</summary>
        [TestMethod]
        public void AddLineItemTest() {
            Order oOrder = new Order();
            Assert.AreEqual(0, oOrder.LineItemsCount);

            oOrder.AddLineItem(LINE_ITEM_AS_INPUT_STRING);
            Assert.AreEqual(1, oOrder.LineItemsCount);
        }

        ///<summary>
        ///  A test for IncrementTotalSalesTax
        ///</summary>
        [TestMethod]
        [DeploymentItem("app.config")]
        public void IncrementTotalSalesTaxTest() {
            var oOrder = new Order();
            oOrder.IncrementTotalSalesTax(mLineItem);

            Assert.AreEqual(new decimal(21.60), oOrder.TotalTaxes.Amount);
        }

        ///<summary>
        ///  A test for PrintInvoice
        ///</summary>
        [TestMethod]
        public void PrintInvoiceTest() {
            var oOrder = new Order();
            oOrder.AddLineItem(LINE_ITEM_AS_INPUT_STRING);

            Assert.AreEqual("14 imported chocolates : 453.50\r\nSales Taxes: 21.60\r\nTotal: 453.50\r\n\r\n", oOrder.PrintInvoice());
        }
    }
}