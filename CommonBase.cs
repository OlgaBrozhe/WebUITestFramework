// <author>Olga Brozhe.</author>
//<summary>Base class.</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WebUITesting
{
    [TestClass]
    public abstract class CommonBase
    {
        internal static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static TestContext Context;

        protected static string Prefix => GetSettingValue("prefix");

        protected static string Environment => GetSettingValue("environment");

        protected static string Domain => GetSettingValue("domain");

        protected static string TopLevelDomain => GetSettingValue("topleveldomain");

        protected static string Username => GetSettingValue("username");

        protected static string Password => GetSettingValue("password");

        protected static int Delay => int.Parse(GetSettingValue("delay"));

        /// <summary>
        /// Gets the specific setting value from runsettings file.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>The setting value.</returns>
        public static string GetSettingValue(string setting)
        {
            if (Context.Properties[setting] == null)
            {
                Log.Error($"Setting {setting} was not found in the runsettings file.");
                throw new Exception($"Setting {setting} was not found in the runsettings file.");
            }
            else
            {
                return Context.Properties[setting].ToString();
            }
        }

        /// <summary>
        /// Initializes the test context.
        /// </summary>
        /// <param name="testcontext">The test context.</param>
        [AssemblyInitialize]
        public static void ClassInit(TestContext testcontext)
        {
            Context = testcontext;
        }
    }
}