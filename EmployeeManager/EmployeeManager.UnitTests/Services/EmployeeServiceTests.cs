using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using EmployeeManager.Binaries.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.UnitTests.Services
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private readonly IEmployeeService employeeService;
        private readonly Mock<IRestApiService> restApiMock = new Mock<IRestApiService>();
        public EmployeeServiceTests()
        {
            employeeService = new EmployeeService(restApiMock.Object);
        }

        [TestMethod]
        public async Task GetEmployees_WhenSuppliedWithAPageNumber_ShouldReturnAllEmployeesForThatPage()
        {
            int PageNumber = 1;
            GetEmployeeResponse response = new GetEmployeeResponse()
            {
                data = new List<Employee>()
                {
                     new Employee(){id = 1, name = "Peter Parker", email = "peterp@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now },
                     new Employee(){id = 2, name = "Bruce Banner", email = "bruceb@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now },
                     new Employee(){id = 3, name = "Charles Xavier", email = "charlesx@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now }
                },
                Success = true
            };

            restApiMock.Setup(x => x.GetEmployees(PageNumber)).ReturnsAsync(response);

            var result = await employeeService.GetEmployees(PageNumber);

            Assert.AreEqual(3, result.data.Count);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task AddEmployee_WhenSuppliedWithANewEmployeeObject_ShouldReturnTheObjectWithANewID()
        {
            Employee Source = new Employee()
            {
                name = "Peter Parker",
                email = "peterp@marvel.com",
                gender = "Male",
                status = "Active",
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            SaveEmployeeResponse response = new SaveEmployeeResponse()
            {
                data = new Employee() { id = 1, name = "Peter Parker", email = "peterp@marvel.com", gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now },
                Success = true
            };

            restApiMock.Setup(x => x.AddEmployee(Source)).ReturnsAsync(response);

            var result = await employeeService.AddEmployee(Source);

            Assert.AreEqual(1, result.data.id);
            Assert.AreEqual(Source.name, result.data.name);
            Assert.AreEqual(Source.email, result.data.email);
            Assert.AreEqual(Source.gender, result.data.gender);
            Assert.AreEqual(Source.status, result.data.status);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task UpdateEmployee_WhenSuppliedWithAnExistingEmployeeObject_ShouldReturnTheUpdatedObject()
        {
            Employee Source = new Employee()
            {
                id = 1,
                name = "Peter Parker",
                email = "peterparker@marvel.com",
                gender = "Male",
                status = "Active",
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            SaveEmployeeResponse response = new SaveEmployeeResponse()
            {
                data = new Employee() { id = 1, name = "Peter Parker", email = "peterparker@marvel.com", gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now },
                Success = true
            };

            restApiMock.Setup(x => x.UpdateEmployee(Source)).ReturnsAsync(response);

            var result = await employeeService.UpdateEmployee(Source);

            Assert.AreEqual(1, result.data.id);
            Assert.AreEqual(Source.name, result.data.name);
            Assert.AreEqual(Source.email, result.data.email);
            Assert.AreEqual(Source.gender, result.data.gender);
            Assert.AreEqual(Source.status, result.data.status);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task RemoveEmployee_WhenSuppliedWithAnExistingEmployeeObject_ShouldReturnARemovedObjectResponse()
        {
            int EmployeeID = 1234;

            RemoveEmployeeResponse response = new RemoveEmployeeResponse()
            {
                Success = true
            };

            restApiMock.Setup(x => x.RemoveEmployee(EmployeeID)).ReturnsAsync(response);

            var result = await employeeService.RemoveEmployee(EmployeeID);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task SearchEmployess_WhenSuppliedWithValidCriteria_ShouldReturnAllEmployeesForThatSearch()
        {
            int PageNumber = 0;
            string Name = "Peter";
            string Gender = "Male";
            string Status = "Active";

            GetEmployeeResponse response = new GetEmployeeResponse()
            {
                data = new List<Employee>()
                {
                     new Employee(){id = 1, name = "Peter Parker", email = "peterp@marvel.com",gender = "Male", status = "Active", created_at = DateTime.Now, updated_at = DateTime.Now }
                },
                Success = true
            };

            restApiMock.Setup(x => x.SearchEmployees(PageNumber, Name, Gender, Status)).ReturnsAsync(response);

            var result = await employeeService.SearchEmployees(PageNumber, Name, Gender, Status);

            foreach (var emp in result.data)
            {
                Assert.IsTrue(emp.name.Contains(Name));
                Assert.AreEqual(Gender, emp.gender);
                Assert.AreEqual(Status, emp.status);
            }
        }
    }
}
