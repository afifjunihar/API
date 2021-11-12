
using Client.Base.Urls;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using System.Net;
using System.Text;
using Client.ViewModels;

namespace Client.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<ProfileVM>> GetProfile() 
        {

            List<ProfileVM> entities1 = new List<ProfileVM>();
            using (var response = await httpClient.GetAsync(request + "Registration/Profile/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities1 = JsonConvert.DeserializeObject<List<ProfileVM>>(apiResponse);
            }
            return entities1;
        }

        public async Task<List<EmployeeVM>> GetEmployeeAll()
        {
            List<EmployeeVM> entities2 = new List<EmployeeVM>();
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities2 = JsonConvert.DeserializeObject<List<EmployeeVM>>(apiResponse);
            }
            return entities2;
        }

        public async Task<List<CountDegreeVM>> GetDataChartDegree()
        {
            List<CountDegreeVM> entities2 = new List<CountDegreeVM>();
            using (var response = await httpClient.GetAsync(request + "ChartDegree"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities2 = JsonConvert.DeserializeObject<List<CountDegreeVM>>(apiResponse);
            }
            return entities2;
        }

        public async Task<List<CountGenderVM>> GetDataChartGender()
        {
            List<CountGenderVM> entity = new List<CountGenderVM>();
            using (var response = await httpClient.GetAsync(request + "ChartGender/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<List<CountGenderVM>>(apiResponse);
            }
            return entity;
        }

        public async Task<List<CountSalaryVM>> GetDataChartSalary()
        {
            List<CountSalaryVM> entity = new List<CountSalaryVM>();
            using (var response = await httpClient.GetAsync(request + "ChartSalary/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<List<CountSalaryVM>>(apiResponse);
            }
            return entity;
        }
        public async Task<ProfileVM> Profile(string id)
        {
            ProfileVM entity = null;
            using (var response = await httpClient.GetAsync(request + "Registration/Profile/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ProfileVM>(apiResponse);
            }

            return entity;
        }

        public async Task<EmployeeVM> GetEmployee(string id)
        {
            EmployeeVM entity = null;
            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<EmployeeVM>(apiResponse);
            }
            return entity;
        }        

        public HttpStatusCode Post(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");           
           var result = httpClient.PostAsync(address.link + request + "Registration/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Put(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(address.link + request + "Update/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Delete(string id)
        {
            var result = httpClient.DeleteAsync(request + "Delete/" + id).Result;
            return result.StatusCode;
        }

        public async Task<JWTokenVM> Auth(LoginVM login)
        {
            JWTokenVM token = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(address.link + request + "Login/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;          
            
        }

    }
}
