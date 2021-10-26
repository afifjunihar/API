using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employee;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employee = employeeRepository;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = employee.Register(registerVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = "NIK sudah tersedia" });
            }
            else if (result == 2)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = "Email sudah terdaftar" });
            }
            else if (result == 3)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = "Nomor telepon sudah terdaftar" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Registrasi Berhasil" });
            }

        }

        [Route("Profile")]
        [HttpGet]
        public ActionResult GetProfile()
        {
            var result = employee.GetProfile();
            return Ok(new { status = HttpStatusCode.OK, result, message = "Berhasil menampilkan data register" });
        }

        [Route("Profile/{NIK}")]
        [HttpGet]
        public ActionResult GetProfile(string NIK)
        {
            try
            {
                var result = employee.GetProfile(NIK);
                return Ok(new { status = HttpStatusCode.OK, result, message = $"Berhasil menampilkan data register dengan NIK : {NIK}" });
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
        }
    }
}
