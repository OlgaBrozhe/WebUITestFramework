// <author>Olga Brozhe.</author>
//<summary>Base class for pages.</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace WebUITesting.Pages
{
    [TestClass]
    public abstract class PageBase : CommonBase
    {
        private static IWebDriver driver;

        /// <summary>
        /// Gets/sets the web driver with delay.
        /// </summary>
        public static IWebDriver Driver
        {
            get
            {
                Thread.Sleep(Delay);
                return driver;
            }

            set => driver = value;
        }

        /// <summary>
        /// Gets web driver depending on browser, maximizes browser window.
        /// </summary>
        /// <returns>Web driver.</returns>
        public static IWebDriver GetDriver()
        {
            var browser = Context.Properties["browser"].ToString();

            switch (browser)
            {
                case "Firefox":
                    Driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    Driver = new ChromeDriver();
                    break;
                default:
                    throw new Exception("Unknown browser.");
            }

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
            return Driver;
        }

        /// <summary>
        /// Navigates to a page by URL.
        /// </summary>
        /// <param name="url">Page URL.</param>
        internal void NavTo(string url)
        {
            _ = url ?? throw new ArgumentNullException(nameof(url));
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Gets/sets the LoginPage.
        /// </summary>
        internal LoginPage LoginPage { get; set; }

        /// <summary>
        /// Gets/sets the InventoryPage.
        /// </summary>
        internal InventoryPage InventoryPage { get; set; }

        /// <summary>
        /// Gets/sets the ShoppingCartPage.
        /// </summary>
        internal ShoppingCartPage ShoppingCartPage { get; set; }
    }
}