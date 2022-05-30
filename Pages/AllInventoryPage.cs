// <author>Olga Brozhe.</author>
// <summary>Inventory Page methods.</summary>

namespace WebUITesting.Pages
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    internal class AllInventoryPage : PageBase
    {
        private static string itemNameInventoryPage;

        /// <summary>
        /// Initializes an instance of <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="driver">Web driver.</param>
        public AllInventoryPage(IWebDriver driver)
        {
            Driver = driver;
            Assert.AreEqual(ExpectedUrl, Driver.Url);
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(FirstAddToCartBtn));
        }

        protected string ExpectedUrl => $"https://{Prefix}.{Environment}.{TopLevelDomain}/inventory.html";

        private IWebElement FirstAddToCartBtn => Driver.FindElement(By.XPath("//button[contains(@id,'add-to-cart')]"));

        private IWebElement ItemPrice => Driver.FindElement(By.XPath($"//div[text()='{itemNameInventoryPage}']/../../following-sibling::div/div[@class = 'inventory_item_price']"));

        private IWebElement ItemBtn => Driver.FindElement(By.XPath($"//div[text()='{itemNameInventoryPage}']/../../..//button"));

        private WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

        /// <summary>
        /// Gets item price.
        /// </summary>
        /// <param name="itemName">Item name.</param>
        /// <returns>Item price.</returns>
        internal string GetItemPrice(string itemName)
        {
            Log.Info($"AllInventoryPage.{new StackFrame(0).GetMethod().Name}.");
            itemNameInventoryPage = itemName;
            var price = ItemPrice.Text;
            Log.Info($"Item price is {price}.");
            return price;
        }

        /// <summary>
        /// Clicks Add To Cart button on a specific item.
        /// </summary>
        /// <param name="">Item name.</param>
        internal void ClickItemBtn(string itemName)
        {
            Log.Info($"ShoppingCartPage.{new StackFrame(0).GetMethod().Name}.");
            itemNameInventoryPage = itemName;
            ItemBtn.Click();
            Log.Info($"Item '{itemName}' button was clicked.");
        }

        /// <summary>
        /// Checks if a specific item button is 'Add To Cart' or 'Remove'.
        /// </summary>
        /// <param name="itemName">Item Name.</param>
        /// <returns>Button value (text).</returns>
        internal string WhichBtnDisplayedAddOrRemove(string itemName)
        {
            Log.Info($"ShoppingCartPage.{new StackFrame(0).GetMethod().Name}.");
            itemNameInventoryPage = itemName;
            var buttonId = ItemBtn.GetAttribute("id");
            var buttonValue = "Add To Cart";
            if (buttonId.Contains("remove"))
            {
                buttonValue = "Remove";
            }

            Log.Info($"Item '{itemName}' button is '{buttonValue}'.");
            return buttonValue;
        }
    }
}