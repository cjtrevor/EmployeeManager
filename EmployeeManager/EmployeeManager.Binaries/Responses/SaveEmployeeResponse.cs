using EmployeeManager.Binaries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Responses
{
    public class SaveEmployeeResponse : ResponseBase
    {
        public SaveEmployeeResponse()
        {
            base.Success = false;
        }
        public Employee data { get; set; }
    }
}
