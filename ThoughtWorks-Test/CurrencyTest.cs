using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks_SalesTax;

namespace ThoughtWorks_Test {
    ///<summary>
    ///  This is a test class for CurrencyTest and is intended
    ///  to contain all CurrencyTest Unit Tests
    ///</summary>
    [TestClass]
    public class CurrencyTest {
        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        ///<summary>
        ///  Testing zero value amount
        ///</summary>
        [TestMethod]
        public void ToStringRoundingTest() {
            Currency oTarget = new Currency();
            oTarget.Amount = new decimal(13.388830000);
// ReSharper disable ConvertToConstant.Local
            string oExpected = "13.39";
// ReSharper restore ConvertToConstant.Local
            string oActual = oTarget.ToString();
            Assert.AreEqual(oExpected, oActual);
        }

        ///<summary>
        ///  Testing zero value amount
        ///</summary>
        [TestMethod]
        public void ToStringTestForZero() {
            Currency oTarget = new Currency();
            oTarget.Amount = 0;
// ReSharper disable ConvertToConstant.Local
            string oExpected = "0.00";
// ReSharper restore ConvertToConstant.Local
            string oActual = oTarget.ToString();
            Assert.AreEqual(oExpected, oActual);
        }

        ///<summary>
        ///  Testing zero value amount
        ///</summary>
        [TestMethod]
        public void ToStringTestForNegative() {
            Currency oTarget = new Currency();
            oTarget.Amount = new decimal(-13.59);
// ReSharper disable ConvertToConstant.Local
            string oExpected = "-13.59";
// ReSharper restore ConvertToConstant.Local
            string oActual = oTarget.ToString();
            Assert.AreEqual(oExpected, oActual);
        }
    }
}