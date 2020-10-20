using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public interface IEmployeeService
    {
        Task<GetEmployeeResponse> GetEmployees(int PageNumber);
        Task<SaveEmployeeResponse> AddEmployee(Employee employee);
        Task<SaveEmployeeResponse> UpdateEmployee(Employee employee);
        Task<RemoveEmployeeResponse> RemoveEmployee(int employeeID); 
        Task<GetEmployeeResponse> SearchEmployees(int PageNumber, string Name, string Gender, string Status);
    }
}
