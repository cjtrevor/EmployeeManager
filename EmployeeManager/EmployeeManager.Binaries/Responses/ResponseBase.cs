using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Responses
{
    public class ResponseBase
    {
        public bool Success { get; set; }
        public string FailureReason { get; set; }
    }
}
