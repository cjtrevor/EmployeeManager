using EmployeeManager.Binaries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Responses
{
    public class GetEmployeeResponse : ResponseBase
    {
        public GetEmployeeResponse()
        {
            base.Success = false;
        }
        public ResponseMeta meta { get; set; }
        public List<Employee> data { get; set; }
    }
}
