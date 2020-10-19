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
        private HttpClient ApiClient { get; set; }
        public RestApiService()
        {
            Initialize();
        }
        private void Initialize()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["apiAuth"]);
            ApiClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]);
        }

        public async Task<GetUserResponse> GetEmployees(int PageNumber)
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync("/public-api/users"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    GetUserResponse result = JsonConvert.DeserializeObject<GetUserResponse>(content);
                    result.Success = true;
                    return result;
                }

                return new GetUserResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<SaveUserResponse> AddEmployee(Employee employee)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiClient.PostAsync("/public-api/users", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    SaveUserResponse result = JsonConvert.DeserializeObject<SaveUserResponse>(responseContent);
                    result.Success = true;
                    return result;
                }

                return new SaveUserResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<SaveUserResponse> UpdateEmployee(Employee employee)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiClient.PutAsync("/public-api/users/" + employee.id, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    SaveUserResponse result = JsonConvert.DeserializeObject<SaveUserResponse>(responseContent);
                    result.Success = true;
                    return result;
                }

                return new SaveUserResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<RemoveUserResponse> RemoveEmployee(int employeeID)
        {
            using (HttpResponseMessage response = await ApiClient.DeleteAsync("/public-api/users/" + employeeID))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new RemoveUserResponse() { Success = true };
                }

                return new RemoveUserResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }

        public async Task<GetUserResponse> SearchEmployees(int PageNumber, string Name, string Gender, string Status)
        {
            string uri = $"/public-api/users?";
            
            if (!string.IsNullOrEmpty(Name))
                uri += $"name={Name}&";
            if (Gender != "No Filter")
                uri += $"gender={Gender}&";
            if (Status != "No Filter")
                uri += $"status={Status}&";

            uri = uri.Substring(0, uri.Length - 1);

            using (HttpResponseMessage response = await ApiClient.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    GetUserResponse result = JsonConvert.DeserializeObject<GetUserResponse>(content);
                    result.Success = true;
                    return result;
                }

                return new GetUserResponse() { Success = false, FailureReason = response.ReasonPhrase };
            }
        }
    }
}
