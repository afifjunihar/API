
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public EmployeesController(EmployeeRepository EmployeeRepository) : base(EmployeeRepository) { }

        [HttpPost]
        [Route("Registration")]
        public ActionResult registration(RegisterVM register) 
        {
            return Ok(new { status = HttpStatusCode.OK, register, message = "Data Berhasil Dimasukan" });

        }
    }
}
