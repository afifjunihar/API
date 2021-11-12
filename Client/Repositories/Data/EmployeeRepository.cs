using System;
using API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Base.Urls;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using API.ViewModel;
using System.Net;
using System.Text;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address Address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.Address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<RegisterVM>> GetProfile()
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "Profile/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode Register(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(Address.link + request + "Register", content).Result;
            return result.StatusCode;
        }
        //get jwtoken from login
        public async Task<JWTokenVM> Auth(LoginVM login)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }
    }
}
