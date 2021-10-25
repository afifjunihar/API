
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employee;
        public EmployeesController(EmployeeRepository EmployeeRepository) : base(EmployeeRepository) { this.employee = EmployeeRepository; }

        [Route("Register")]
        [HttpPost]
        public ActionResult Regis(RegisterVM register)
        {
            var result = employee.Register(register);
            return Ok(result);
        }
        [Route("Register")]
        [HttpGet]
        public ActionResult GetInfo()
        {
            return Ok(employee.GetProfile());
        }
    }
}