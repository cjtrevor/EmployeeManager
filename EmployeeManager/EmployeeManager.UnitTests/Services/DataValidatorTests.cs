using System;
using EmployeeManager.Binaries.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeManager.UnitTests.Services
{
    [TestClass]
    public class DataValidatorTests
    {
        private IDataValidator validator;

        public DataValidatorTests()
        {
            validator = new DataValidator();
        }

        [TestMethod]
        public void isValidEmail_ValidEmailSupplied_ReturnsTrue()
        {
            bool Valid = validator.isValidEmail("abc@def.com");
            Assert.IsTrue(Valid);
        }

        [TestMethod]
        public void isValidEmail_InvalidEmailSupplied_ReturnsFalse()
        {
            bool Valid = validator.isValidEmail("abcdef.com");
            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void isValidSearchCriteria_EmptyGenderSupplied_ReturnsFalse()
        {
            bool Valid = validator.isValidSearchCriteria("", "Active");
            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void isValidSearchCriteria_EmptyStatusSupplied_ReturnsFalse()
        {
            bool Valid = validator.isValidSearchCriteria("Male", "");
            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void isValidSearchCriteria_EmptyGenderAndStatusSupplied_ReturnsFalse()
        {
            bool Valid = validator.isValidSearchCriteria("", "");
            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void isValidSearchCriteria_GenderAndStatusSupplied_ReturnsTrue()
        {
            bool Valid = validator.isValidSearchCriteria("Male", "Active");
            Assert.IsTrue(Valid);
        }
    }
}
