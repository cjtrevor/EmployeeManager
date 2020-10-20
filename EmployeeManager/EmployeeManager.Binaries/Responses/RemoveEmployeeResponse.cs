using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Responses
{
    public class RemoveEmployeeResponse:ResponseBase
    {
        public RemoveEmployeeResponse()
        {
            base.Success = false;
        }
    }
}
