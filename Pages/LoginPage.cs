// <author>Olga Brozhe.</author>
// <summary>Login Page methods.</summary>

namespace WebUITesting.Pages
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    internal class LoginPage : PageBase
    {
        /// <summary>
        /// Initializes an instance of <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="driver">Web driver.</param>
        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected string ExpectedUrl => $"https://{Prefix}.{Environment}.{TopLevelDomain}/";

        private IWebElement UsernameInput => Driver.FindElement(By.Id("user-name"));

        private IWebElement PasswordInput => Driver.FindElement(By.Id("password"));

        private IWebElement LoginBtn => Driver.FindElement(By.Id("login-button"));

        private WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

        /// <summary>
        /// Login.
        /// </summary>
        /// <returns>InventoryPage.</returns>
        internal InventoryPage Login()
        {
            Log.Info($"LoginPage.{new StackFrame(0).GetMethod().Name}.");
            var username = Username;
            var password = Password;
            NavTo(ExpectedUrl);
            Assert.AreEqual(ExpectedUrl, Driver.Url);
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(LoginBtn));
            EnterUsername(username);
            EnterPassword(password);
            InventoryPage = ClickLoginBtn();
            Log.Info("Login completed.");
            return new InventoryPage(Driver);
        }

        /// <summary>
        /// Enters username.
        /// </summary>
        /// <param name="username">User name.</param>
        internal void EnterUsername(string username)
        {
            Log.Info($"LoginPage.{new StackFrame(0).GetMethod().Name}.");
            UsernameInput.SendKeys(username);
            Log.Info("Username was inserted.");
        }

        /// <summary>
        /// Enters password.
        /// </summary>
        /// <param name="password">Password.</param>
        internal void EnterPassword(string password)
        {
            Log.Info($"LoginPage.{new StackFrame(0).GetMethod().Name}.");
            PasswordInput.SendKeys(password);
            Log.Info("Password was inserted.");
        }

        /// <summary>
        /// Clicks Login button.
        /// </summary>
        /// <returns>InventoryPage.</returns>
        internal InventoryPage ClickLoginBtn()
        {
            Log.Info($"LoginPage.{new StackFrame(0).GetMethod().Name}.");
            LoginBtn.Click();
            Log.Info("Login button was clicked.");
            return new InventoryPage(Driver);
        }
    }
}