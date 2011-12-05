using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks_SalesTax;

namespace ThoughtWorks_Test {
    ///<summary>
    ///  This is a test class for SalesTaxHelperTest and is intended
    ///  to contain all SalesTaxHelperTest Unit Tests
    ///</summary>
    [TestClass]
    public class SalesTaxHelperTest {
        public static readonly decimal mZero = new decimal(0);
        public static readonly decimal mBarelyPositive = new decimal(0.04);
        public static readonly decimal mBigDecimal = new decimal(10.473883);

        // Rounding setup
        public readonly decimal mRoundToNearestFiveTenth = new decimal(0.05);
        public readonly decimal mRoundToNearestOneTenth = new decimal(0.01);

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DeploymentItem("app.config")]
        public void VerifyConfigurationSettingsExist() {
            string oSalesTax = ConfigurationManager.AppSettings.Get("SalesTax");
            Assert.IsFalse(String.IsNullOrEmpty(oSalesTax), "No App.Config found.");
        }

        ///<summary>
        ///  A test for ItemIsTaxExempt
        ///</summary>
        [TestMethod]
        [DeploymentItem("app.config")]
        public void ItemIsTaxExemptTest() {
            var oExemptItems = new[] {"stomach pills", "dark chocolate bar", "picture book", "imported white chocolates"};
            var oNonExemptItems = new[] {"bagels", "headphones", "soap"};

            foreach (var oExemptItem in oExemptItems) {
                Assert.IsTrue(SalesTaxHelper.ItemIsTaxExempt(oExemptItem), oExemptItem + " was not exempted");
            }
            foreach (var oNonExemptItem in oNonExemptItems) {
                Assert.IsFalse(SalesTaxHelper.ItemIsTaxExempt(oNonExemptItem), oNonExemptItem + " was incorrectly exempted");
            }
        }

        ///<summary>
        ///  Round up a zero value
        ///</summary>
        [TestMethod]
        public void TestRoundUpForZero() {
            Assert.AreEqual(new decimal(0), SalesTaxHelper.RoundUpToNearest(new decimal(0), mRoundToNearestFiveTenth));
        }

        ///<summary>
        ///  Round up a negative value
        ///</summary>
        [TestMethod]
        public void TestRoundUpForNegative() {
            Assert.AreEqual(new decimal(-10.30), SalesTaxHelper.RoundUpToNearest(new decimal(-10.34), mRoundToNearestFiveTenth));
        }

        ///<summary>
        ///  Simplified test, does not imply mathematical accuracy due to rounding
        ///</summary>
        [TestMethod]
        public void TestRoundUpToNearest() {
            Assert.AreEqual(new decimal(0.05), SalesTaxHelper.RoundUpToNearest(mBarelyPositive, mRoundToNearestFiveTenth));

            Assert.AreEqual(new decimal(0.04), SalesTaxHelper.RoundUpToNearest(mBarelyPositive, mRoundToNearestOneTenth));

            Assert.AreEqual(new decimal(10.5), SalesTaxHelper.RoundUpToNearest(mBigDecimal, mRoundToNearestFiveTenth));

            Assert.AreEqual(new decimal(10.48), SalesTaxHelper.RoundUpToNearest(mBigDecimal, mRoundToNearestOneTenth));
        }
    }
}