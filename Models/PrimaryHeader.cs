// <author>Olga Brozhe.</author>
// <summary>Primary header methods.</summary>

namespace WebUITesting.Models
{
    using System;
    using System.Diagnostics;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using WebUITesting.Pages;

    internal class PrimaryHeader : ModelBase
    {
        private IWebElement ShoppingCartIcon => Driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));

        private IWebElement ShoppingCartIconCounter => Driver.FindElement(By.XPath("//a[@class='shopping_cart_link']"));

        private WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

        /// <summary>
        /// Clicks shopping cart icon.
        /// </summary>
        /// <returns>ShoppingCartPage.</returns>
        internal ShoppingCartPage ClickShoppingCart()
        {
            Log.Info($"PrimaryHeader.{new StackFrame(0).GetMethod().Name}.");
            ShoppingCartIcon.Click();
            Log.Info("Shopping Cart icon was clicked.");
            return new ShoppingCartPage(Driver);
        }

        /// <summary>
        /// Gets shopping cart items counter from the shopping cart icon badge.
        /// </summary>
        /// <returns>Shopping cart items count.</returns>
        internal int GetShoppingCartCounter()
        {
            Log.Info($"PrimaryHeader.{new StackFrame(0).GetMethod().Name}.");
            var countText = string.Empty;
            try
            {
                Driver.FindElement(By.XPath("//div[@id='shopping_cart_container']/a[count(*) > 0]")); // Checks if badge with counter is displayed.
                countText = ShoppingCartIconCounter.Text;
                Log.Info($"Shopping Cart has {countText} items.");
            }
            catch (NoSuchElementException)
            {
                countText = "0";
                Log.Info("Shopping Cart is empty.");
            }

            int count = int.Parse(countText);
            return count;
        }

    }
}