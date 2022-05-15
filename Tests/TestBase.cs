﻿// <author>Olga Brozhe.</author>
//<summary>Test Base class.</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using WebUITesting.Pages;

namespace TestWebUI.Tests
{
    [TestClass]
    public abstract class TestBase : PageBase
    {
        internal WebDriverWait Wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

        public TestContext TestContext { get; set; }

        /// <summary>
        /// Logins to the environment specified in runsettings file.
        /// </summary>
        internal void LoginToEnvironment()
        {
            Log.Info($"Test init: {TestContext.TestName}.");
            Log.Info("Login to the Environment.");
            Driver = GetDriver();
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            LoginPage = new LoginPage(Driver);
            LoginPage.Login();
        }

        /// <summary>
        /// Cleanup.
        /// </summary>
        public void DriverDispose()
        {
            var testMethodName = TestContext.TestName;
            Log.Info($"Test cleanup: {testMethodName}.");
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                Log.Info($"Test {testMethodName} failed, see the test log for the details.");
            }

            Driver.Dispose();
        }
    }
}
