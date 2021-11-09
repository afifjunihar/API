
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employee;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository EmployeeRepository, IConfiguration configuration) : base(EmployeeRepository) 
        { 
            this.employee = EmployeeRepository;
            this._configuration = configuration;
        }


        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = employee.Register(registerVM);
            if (result == 1)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "NIK sudah terdaftar" });
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Email sudah terdaftar" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Nomor telepon sudah terdaftar" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Registrasi Berhasil" });
            }

        }

        [Route("Register")]
        [HttpPut]
        public ActionResult UpdateEmployee(RegisterVM registerVM)
        {
            var result = employee.UpdateEmployee(registerVM);
            if (result == 1)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "NIK sudah terdaftar" });
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Email sudah terdaftar" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Nomor telepon sudah terdaftar" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Registrasi Berhasil" });
            }

        }

        [Authorize(Roles = "Director, Manager")]
        [Route("Profile")]
        [HttpGet]
        public ActionResult GetProfileInfo()
        { 
            return Ok(employee.GetProfile());
        }

        //[Authorize(Roles = "Employee, Manager, Director")]
        [Route("Profile/{nik}")]
        [HttpGet]
        public ActionResult GetProfileInfo(string nik)
        {
            return Ok(employee.GetProfileBy(nik));
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var result = employee.Login(loginVM);
            if (result == 1)
            {
                return NotFound("Email Belum Terdaftar");
            }
            else if (result == 2)
            {
                return BadRequest("Password Salah");
            }
            // Implement JWT
            var data = new LoginDataVM
            {
                email = loginVM.Email,
                role = employee.GetRole(loginVM)
            };
            var claims = new List<Claim>
            {
                new Claim("email", data.email)
            };
            foreach (var item in data.role)
            {
                claims.Add(new Claim("roles", item.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                );
            var idToken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idToken.ToString()));
            return Ok(new { status = HttpStatusCode.OK, idToken, message = "Login Sukses" });
        }

        [HttpGet]
        [Route("TestCORS")]
        public string funtion()
        {
            return "Test CORS berhasil";
        }

        [Authorize]
        [HttpGet]
        [Route("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        [Authorize(Roles = "Director")]
        [HttpPost]
        [Route("SignManager")]
        public ActionResult SignManager(AccountRole role)
        {
            var result = employee.SignManager(role);
            return Ok(new { status = HttpStatusCode.OK, message = $"berhasil mengatur NIK {role.NIK} sebagai manager " });
        }

        [Route("Gender")]
        [HttpGet]
        public ActionResult CountGender()
        {
            //int[] gender = { employee.CountFemale(), employee.CountMale() };
            List<Int32> genderCount = new List<Int32>();
            genderCount.Add(employee.CountMale());
            genderCount.Add(employee.CountFemale());
            return Ok(new { x = genderCount });
        }
    }
}