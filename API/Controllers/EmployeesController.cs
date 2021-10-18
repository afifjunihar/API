using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeRepository employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            employeeRepository.Insert(employee);
            return Ok();
        }

        //public ActionResult Update(Employee employee)
        //{
        //    employeeRepository.Update(employee);
        //    return Ok();
        //}

        //public ActionResult Delete(Employee NIK)
        //{

        //    employeeRepository.Delete(Convert.ToString(NIK));
        //    return Ok();
        //}

        //public ActionResult Get()
        //{
        //    return Get();
        //}
    }

}
