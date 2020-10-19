using EmployeeManager.Binaries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Responses
{
    public class SaveUserResponse: ResponseBase
    {
        public Employee data { get; set; }
    }
}
