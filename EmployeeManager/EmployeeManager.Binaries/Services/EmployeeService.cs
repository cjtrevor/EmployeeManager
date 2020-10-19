using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IRestApiService _restApiService;
        public EmployeeService(IRestApiService restApiService)
        {
            _restApiService = restApiService;
        }
        public async Task<SaveUserResponse> AddEmployee(Employee employee)
        {
            return await _restApiService.AddEmployee(employee);
        }

        public async Task<GetUserResponse> GetEmployees(int PageNumber)
        {
            return await _restApiService.GetEmployees(PageNumber);
        }

        public async Task<RemoveUserResponse> RemoveEmployee(int employeeID)
        {
            return await _restApiService.RemoveEmployee(employeeID);
        }

        public async Task<GetUserResponse> SearchEmployees(int PageNumber, string Name, string Gender, string Status)
        {
            return await _restApiService.SearchEmployees(PageNumber, Name, Gender, Status);
        }
        public async Task<SaveUserResponse> UpdateEmployee(Employee employee)
        {
            return await _restApiService.UpdateEmployee(employee);
        }
    }
}
