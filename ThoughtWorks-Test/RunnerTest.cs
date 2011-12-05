using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThoughtWorks_SalesTax;

namespace ThoughtWorks_Test {
    ///<summary>
    ///  This is a test class for RunnerTest and is intended
    ///  to contain all RunnerTest Unit Tests
    ///</summary>
    [TestClass]
    public class RunnerTest {
        private const string MISSING_ORDER_DIRECTORY = "missing_directory/";
        private const string BLANK_INPUT_DIRECTORY = "empty_input/";

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        ///<summary>
        ///  Test that invalid directories throw an exception
        ///</summary>
        [TestMethod]
        [DeploymentItem("ThoughtWorks_SalesTax.exe")]
        [ExpectedException(typeof (DirectoryNotFoundException))]
        public void ReadInvalidInputDirectory() {
            Runner.ReadOrders(MISSING_ORDER_DIRECTORY);
        }

        ///<summary>
        ///  Test that blank directories throw an exception
        ///</summary>
        [TestMethod, Ignore]
        [DeploymentItem("ThoughtWorks_SalesTax.exe")]
        [ExpectedException(typeof (IOException))]
        public void ReadBlankInputDirectory() {
            Runner.ReadOrders(BLANK_INPUT_DIRECTORY);
        }
    }
}