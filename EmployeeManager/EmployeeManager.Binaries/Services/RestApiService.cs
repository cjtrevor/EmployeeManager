using EmployeeManager.Binaries.Models;
using EmployeeManager.Binaries.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Binaries.Services
{
    public class RestApiService : IRestApiService
    {
        private IConfigurationService _configurationService;
        private HttpClient ApiClient { get; set; }
        public RestApiService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;

            Initialize();
        }
        private void Initialize()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configurationService.GetConfigurationValueString("apiAuth"));
            ApiClient.BaseAddress = new Uri(_configurationService.GetConfigurationValueString("api"));
        }

        public async Task<GetEmployeeResponse> GetEmployees(int PageNumber)
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync("/public-api/users?page=" + PageNumber))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    GetEmployeeResponse result = JsonConvert.DeserializeObject<GetEmployeeResponse>(content);
                    result.Success = true;
                    return result;
                }

                return new GetEmployeeResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<SaveEmployeeResponse> AddEmployee(Employee employee)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiClient.PostAsync("/public-api/users", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    SaveEmployeeResponse result = JsonConvert.DeserializeObject<SaveEmployeeResponse>(responseContent);
                    result.Success = true;
                    return result;
                }

                return new SaveEmployeeResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<SaveEmployeeResponse> UpdateEmployee(Employee employee)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiClient.PutAsync("/public-api/users/" + employee.id, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    SaveEmployeeResponse result = JsonConvert.DeserializeObject<SaveEmployeeResponse>(responseContent);
                    result.Success = true;
                    return result;
                }

                return new SaveEmployeeResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<RemoveEmployeeResponse> RemoveEmployee(int employeeID)
        {
            using (HttpResponseMessage response = await ApiClient.DeleteAsync("/public-api/users/" + employeeID))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new RemoveEmployeeResponse() { Success = true };
                }

                return new RemoveEmployeeResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<GetEmployeeResponse> SearchEmployees(int PageNumber, string Name, string Gender, string Status)
        {
            string uri = GetSearchUri(PageNumber, Name, Gender, Status);
            using (HttpResponseMessage response = await ApiClient.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    GetEmployeeResponse result = JsonConvert.DeserializeObject<GetEmployeeResponse>(content);
                    result.Success = true;
                    return result;
                }

                return new GetEmployeeResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }
        private string GetSearchUri(int PageNumber, string Name, string Gender, string Status)
        {
            string uri = $"/public-api/users?";

            if (PageNumber > 0)
                uri += $"page={PageNumber}&";
            if (!string.IsNullOrEmpty(Name))
                uri += $"name={Name}&";
            if (Gender != "No Filter")
                uri += $"gender={Gender}&";
            if (Status != "No Filter")
                uri += $"status={Status}&";

            return uri.Substring(0, uri.Length - 1);
        }
    }
}
