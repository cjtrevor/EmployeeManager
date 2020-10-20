using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.UnitTests.Services
{
    [TestClass]
    public class SerializerServiceTests
    {
        private ISerializeService serializeService;

        public SerializerServiceTests()
        {
            serializeService = new SerializeService();
        }
        [TestMethod]
        public void SerializeEmployeeListToCSV_SupplyListWithValues_ReturnsCSVString()
        {
            List<Employee> Employees = new List<Employee>()
            {
                new Employee(){id = 1, name = "Peter Parker", email = "peterp@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now },
                new Employee(){id = 2, name = "Bruce Banner", email = "bruceb@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now },
                new Employee(){id = 3, name = "Charles Xavier", email = "charlesx@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now }
            };

            string result = serializeService.SerializeEmployeeListToCSV(Employees);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void SerializeEmployeeListToCSV_SupplyEmptyList_ReturnsEmptyString()
        {
            List<Employee> Employees = new List<Employee>();

            string result = serializeService.SerializeEmployeeListToCSV(Employees);
            Assert.IsTrue(result.Length == 0);
        }
    }
}
