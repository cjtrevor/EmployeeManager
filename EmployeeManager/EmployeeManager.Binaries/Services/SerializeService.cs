using EmployeeManager.Binaries.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public class SerializeService : ISerializeService
    {
        public string SerializeEmployeeListToCSV(List<Employee> ListToSerialize)
        {
            if (ListToSerialize.Count == 0)
                return "";

            return ListToSerialize.ToCsv();
        }
    }
}
