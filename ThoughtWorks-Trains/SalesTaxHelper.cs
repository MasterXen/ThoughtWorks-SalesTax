using System;
using System.Configuration;
using System.Linq;

namespace ThoughtWorks_SalesTax {
    internal class SalesTaxHelper {
        // Configuration keys
        public const string CONFIG_KEY_TAX_EXEMPT_ITEMS = "SalesTaxExemptItems";

        internal static T? GetConfigurationValue<T>(string pConfigurationKey) where T : struct {
            T? oReturnValue = null;
            if (ConfigurationManager.AppSettings[pConfigurationKey] != null) {
                oReturnValue = (T) Convert.ChangeType(ConfigurationManager.AppSettings[pConfigurationKey], typeof (T));
            }
            return oReturnValue;
        }

        internal static string GetConfigurationValue(string pConfigurationKey) {
            return ConfigurationManager.AppSettings[pConfigurationKey] != null ? ConfigurationManager.AppSettings.Get(pConfigurationKey) : String.Empty;
        }

        internal static bool ItemIsTaxExempt(string pItemDescription) {
            var oCommaDelimitedExemptList = GetConfigurationValue(CONFIG_KEY_TAX_EXEMPT_ITEMS);
            if (String.IsNullOrEmpty(oCommaDelimitedExemptList)) {
                return false;
            }

            var oExemptItems = oCommaDelimitedExemptList.Split(',');
            return oExemptItems.Any(pExemptItem => pItemDescription.ToLower().Contains(pExemptItem));
        }

        internal static decimal RoundUpToNearest(decimal pTaxableAmount, decimal pRoundToNearest) {
            return (Math.Ceiling(pTaxableAmount/pRoundToNearest))*pRoundToNearest;
        }
    }
}