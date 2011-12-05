using System;
using System.IO;

namespace ThoughtWorks_SalesTax {
    public class Runner {
        // Configuration keys
        public const string CONFIG_KEY_INPUT_DIRECTORY = "OrderDirectory";
        public const string CONFIG_KEY_FILE_SEARCH_PATTERN = "OrderFileSearchPattern";

        private static void Main() {
            ReadOrders(SalesTaxHelper.GetConfigurationValue(CONFIG_KEY_INPUT_DIRECTORY));
            PauseBeforeExit();
        }

        /// <summary>
        ///   Process order files in input directory
        /// </summary>
        /// <param name = "pOrderDirectory"></param>
        public static void ReadOrders(string pOrderDirectory) {
            // This exception is already thrown by <code>Directory.GetFiles()</code> but caught earlier here to allow 
            // the option of throwing an app-specific exception
            if (!Directory.Exists(pOrderDirectory)) {
                throw new DirectoryNotFoundException("Unable to find input directory for orders: " + pOrderDirectory);
            }

            // Process the list of files found in the directory.
            string[] oOrderFilenames = Directory.GetFiles(pOrderDirectory, SalesTaxHelper.GetConfigurationValue(CONFIG_KEY_FILE_SEARCH_PATTERN));

            if (oOrderFilenames.Length < 1) {
                throw new IOException("No orders found in input directory");
            }

            foreach (var oOrderFile in oOrderFilenames) {
                var oOrderProcessor = new Order();

                var oOrderLineItems = File.ReadAllLines(oOrderFile);
                foreach (var oLineItem in oOrderLineItems) {
                    oOrderProcessor.AddLineItem(oLineItem);
                }

                Console.WriteLine(oOrderProcessor.PrintInvoice());
            }

            Console.WriteLine("======================================");
            Console.WriteLine("PROCESSED ALL INPUT FILES IN DIRECTORY");
        }

        /// <summary>
        ///   Pause execution to prevent program exit
        /// </summary>
        private static void PauseBeforeExit() {
            Console.ReadLine();
        }
    }
}