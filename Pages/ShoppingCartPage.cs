// <author>Olga Brozhe.</author>
// <summary>Home Page methods.</summary>

namespace WebUITesting.Pages
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    internal class ShoppingCartPage : PageBase
    {
        private static string itemNameShoppingCart;

        /// <summary>
        /// Initializes an instance of <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="driver">Web driver.</param>
        public ShoppingCartPage(IWebDriver driver)
        {
            Driver = driver;
            Assert.AreEqual(ExpectedUrl, Driver.Url);
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(CheckOutBtn));
        }

        protected string ExpectedUrl => $"https://{Prefix}.{Environment}.{TopLevelDomain}/cart.html";

        private IWebElement CheckOutBtn => Driver.FindElement(By.Id("checkout"));

        private IWebElement ItemName => Driver.FindElement(By.XPath($"//div[text()='{itemNameShoppingCart}']"));

        private IWebElement ItemPriceByName => Driver.FindElement(By.XPath($"//div[text()='{itemNameShoppingCart}']/../..//div[@class = 'inventory_item_price']"));

        private IWebElement ItemRemoveBtnByName => Driver.FindElement(By.XPath($"//div[text()='{itemNameShoppingCart}']/../..//button[contains(@id,'remove')]"));

        private WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

        /// <summary>
        /// Gets item price.
        /// </summary>
        /// <param name="itemName">Item name.</param>
        /// <returns>Item price.</returns>
        internal string GetItemPrice(string itemName)
        {
            Log.Info($"ShoppingCartPage.{new StackFrame(0).GetMethod().Name}.");
            itemNameShoppingCart = itemName;
            var price = ItemPriceByName.Text;
            Log.Info($"Item price is {price}.");
            return price;
        }

        /// <summary>
        /// Checks if specific item is in the cart.
        /// </summary>
        /// <param name="itemName">Item name.</param>
        /// <returns>True, if item is in the cart, false - if not.</returns>
        internal bool IsItemInShoppingCart(string itemName)
        {
            Log.Info($"ShoppingCartPage.{new StackFrame(0).GetMethod().Name}.");
            itemNameShoppingCart = itemName;
            bool result = false;
            if (ItemName.Displayed)
            {
                result = true;
                Log.Info($"Item '{itemName}' is in the cart.");
            }
            else
            {
                Log.Info($"Item '{itemName}' is not in the cart.");
            }

            return result;
        }

        /// <summary>
        /// Clicks Remove button on a specific item.
        /// </summary>
        /// <param name="">Item name.</param>
        internal void ClickRemoveBtn(string itemName)
        {
            Log.Info($"ShoppingCartPage.{new StackFrame(0).GetMethod().Name}.");
            itemNameShoppingCart = itemName;
            ItemRemoveBtnByName.Click();
            Log.Info($"'{itemName}' Remove button was clicked.");
        }

    }
}