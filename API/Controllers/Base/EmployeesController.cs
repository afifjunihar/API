
using API.Models;
using API.ViewModels;
using API.Repository;
using API.Repository.Data;
using API.ViewModels;
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

   
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public readonly EmployeeRepository employee;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository) 
        {
            this.employee = employeeRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]
        public ActionResult Register(RegisterVM register)
        {
            try
            {
                var result = employee.Register(register);
           
                if (result == 0)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Gagal menambahkan data, Primary Key tidak boleh kosong"
                    });
                }
                else if (result == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "NIK sudah tersedia" });
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
                    return Ok("Berhasil Registrasi");

                }
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Gagal menambahkan data, Cek bagian Register => Employees Controller"
                });
            }


        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(RegisterVM register)
        {
 
                var result = employee.Update(register);

                if (result == 0)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Gagal menambahkan data, Primary Key tidak boleh kosong"
                    });
                }
                else if (result == 1)
                {
                    return Ok("Berhasil Update Data");
                }
                else 
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal diubah" });
                };
            
           


        }

        [HttpGet]
        [Route("Registration/Profile")]
       // [Authorize(Roles = "Manajer,Director")]
        public ActionResult<Employee> GetProfile() 
        {
            try
            {
                var result = employee.GetProfile();               
                return Ok(result);  
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Maaf Terjadi Error, Mohon Cek Line 72, Employee Controller"
                });
            }
        }

        
        [HttpGet]
        [Route("Registration/Profile/{NIK}")]
        //[Authorize(Roles = "Employee,Manajer,Director")]
        public ActionResult Register(string NIK)
        {
            try
            {
                var result = employee.GetProfile(NIK);
                return Ok( result);
   
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Gagal menambahkan data, Primary Key tidak terdaftar"
                });
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM login)
        {
            var checkLogin = employee.Login(login);

            if (checkLogin == 0)
            {

                var getUserRole = employee.GetUserData(login);
                var getFullName = employee.GetFullName(login);

                var data = new LoginDataVM
                {
                    Email = login.Email,
                    Roles = getUserRole
                };

                var claims = new List<Claim>
                {
                    new Claim("email", login.Email)
                };

                foreach (var x in data.Roles)
                {
                    claims.Add(new Claim("roles", x.ToString()));
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
                return Ok(new JWTokenVM { message = $"Login Berhasil !", Token = idToken });
            }

            //else if (checkLogin == 1)
            //{
            //    return BadRequest(new
            //    {
            //        status = HttpStatusCode.BadRequest,
            //        message = "Email yang Anda Masukan Tidak Terdaftar"
            //    });
            //}
            //else if (checkLogin == 2)
            //{
            //    return BadRequest(new
            //    {
            //        status = HttpStatusCode.BadRequest,
            //        message = "Password yang Anda Masukan Salah"
            //    });
            //}
            //else 
            //{
            //    return NotFound(new
            //    {
            //        status = HttpStatusCode.NotFound,
            //        message = "Terjadi Error mohon chek line 120 EmployeesRepository"
            //    });
            //}
            else
            {
                return BadRequest(new JWTokenVM { message = $"Login ggl !", Token = null });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("TestCORS")]
        public ActionResult CORS()
        {
            return Ok("Test CORS Berhasil");
        }

        [HttpDelete]
        [Route("Delete/{NIK}")]
        public ActionResult DeleteAkun(string NIK)
        {
            var result = employee.Delete(NIK);
            if (result == 0)
            {
                return Ok("Data Berhasil di Hapus");

            }
            else 
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Gagal menghapus data, Primary Key tidak terdaftar"
                });
            }
        }

        [HttpPost]
        [Route("SignManager")]
        [Authorize(Roles = "Director")]
        public ActionResult SignManager(RegisterVM registerVM) 
        {
            var test = employee.SignManager(registerVM);
            if (test == 0)
            {
                return Ok(new { message = "Selamat atas Pengangkatannya sebagai Manager" });
            }
            else if (test == 1)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "NIK yang Anda Masukan Tidak Terdaftar"
                });
            }
            else 
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Terjadi Error mohon chek line 120 EmployeesRepository"
                });
            }
        }

        [Route("ChartGender")]
        [HttpGet]
        public ActionResult CountGender()
        {
            List<object> genderCount = new List<object>
            {
                new { Gender = "Female", Count = employee.CountFemale() },
                new { Gender = "Male", Count = employee.CountMale() }
            };
            return Ok(genderCount);
        }


        [Route("ChartDegree")]
        [HttpGet]
        public ActionResult Chart()
        {
            var result = employee.ByDegree();
            return Ok(result);
        }

        [Route("ChartSalary")]
        [HttpGet]
        public ActionResult ChartSalary()
        {
            List<object> CountSalary = new List<object>
            {
                new { Range = "< 5000000", count = employee.CheckSalary()[0]},
                new { Range = "5000000 - 10000000", count = employee.CheckSalary()[1] },
                new { Range = "> 10000000", count = employee.CheckSalary()[2] }
            };
            return Ok(CountSalary);
        }


    }
 }





   
