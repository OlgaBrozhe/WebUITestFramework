// <author>Olga Brozhe.</author>
// <summary>Helper for actions like trim.</summary>

using System.Collections.Generic;
using System.Linq;

namespace TestWebUI.Helpers
{
    public class CommonHelper
    {
        /// <summary>
        /// Gets the currency from the price.
        /// </summary>
        /// <param name="price">Price with the currency.</param>
        /// <returns>Currency.</returns>
        public static string GetCurrencyFromPrice(string price)
        {
            var currency = string.Empty;
            List<string> allcharacters = new List<string>();
            allcharacters.AddRange(price.Select(c => c.ToString()));
            foreach (var c in allcharacters)
            {
                bool success = int.TryParse(c, out int number);
                if (!success && c != "-" && c != "." && c != ",")
                {
                    currency += c;
                }
            }

            return currency;
        }

        /// <summary>
        /// Gets the amount from price, sign wise.
        /// </summary>
        /// <param name="price">Price with the currency.</param>
        /// <returns>Amount, positive or negative.</returns>
        public static float GetAmountFromPrice(string price)
        {
            var amountString = string.Empty;
            List<string> allcharacters = new List<string>();
            allcharacters.AddRange(price.Select(c => c.ToString()));
            foreach (var c in allcharacters)
            {
                bool success = int.TryParse(c, out int number);
                if (success || c == "-" || c == "." || c == ",")
                {
                    amountString += c;
                }
            }

            var amount = float.Parse(amountString);
            return amount;
        }
    }
}
