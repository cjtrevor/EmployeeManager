using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public interface IRestApiService
    {
        Task<GetUserResponse> GetEmployees(int PageNumber);

        Task<SaveUserResponse> AddEmployee(Employee employee);
        Task<SaveUserResponse> UpdateEmployee(Employee employee);
        Task<RemoveUserResponse> RemoveEmployee(int employeeID);
        Task<GetUserResponse> SearchEmployees(int PageNumber, string Name, string Gender, string Status);
    }
}