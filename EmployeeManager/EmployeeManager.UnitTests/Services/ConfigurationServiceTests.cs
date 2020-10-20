using EmployeeManager.Binaries.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.UnitTests.Services
{
    [TestClass]
    public class ConfigurationServiceTests
    {
        private IConfigurationService configurationService;
        public ConfigurationServiceTests()
        {
            configurationService = new ConfigurationService();
        }

        [TestMethod]
        public void GetConfigurationValueString_SupplyValidKey_ReturnsAppSettingsValue()
        {
            ConfigurationManager.AppSettings["UnitTestKey"] = "MyKey";

            string result = configurationService.GetConfigurationValueString("UnitTestKey");

            Assert.AreEqual("MyKey", result);
        }

        [TestMethod]
        public void GetConfigurationValueString_SupplyInvalidKey_ReturnsBlankValue()
        {
            string result = configurationService.GetConfigurationValueString("DemoKey");

            Assert.AreEqual("", result);
        }
    }
}
