using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public interface IDataValidator
    {
        bool isValidEmail(string Email);
        bool isValidSearchCriteria(string Gender, string Status);
    }
}
