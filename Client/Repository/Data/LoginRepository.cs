using API.ViewModels;
using Client.Base.Urls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class LoginRepository : GeneralRepository<LoginVM, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public LoginRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
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