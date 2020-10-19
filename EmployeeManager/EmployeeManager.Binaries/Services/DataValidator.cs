using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public class DataValidator : IDataValidator
    {
        public bool isValidEmail(string Email)
        {
            return new EmailAddressAttribute().IsValid(Email);
        }

        public bool isValidSearchCriteria(string Gender, string Status)
        {
            return !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(Status);
        }
    }
}
