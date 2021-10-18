using API.Models;
using API.Repository;
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
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            employeeRepository.Insert(employee);

            return Ok();
        }


        [HttpGet]
        public ActionResult Get()
        {
            return Ok(employeeRepository.Get());
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            return Ok(employeeRepository.Get(NIK));
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            employeeRepository.Delete(NIK);
            Console.WriteLine();
            return Ok($"Berhasil Menghapus data {NIK}");
        }

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            employeeRepository.Update(employee);
            return Ok("Data berhasil diubah");
        }
    }
}
