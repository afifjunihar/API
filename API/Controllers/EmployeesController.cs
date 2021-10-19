using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if(employeeRepository.Get(employee.NIK) != null)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, NIK sudah terdata di database" });
            }
            else
            {
                employeeRepository.Insert(employee);
                return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil diinput kedalam database", employee });
            }
            
        }


        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get();
            if (result.Count() == 0)
            {
                return Ok(new {status = HttpStatusCode.NoContent, message = "Database tidak memiliki data alias kosong" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data ditemukan" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            if (employeeRepository.Get(NIK) == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
            else
            {
                return Ok(employeeRepository.Get(NIK));
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            if (employeeRepository.Get(NIK) == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
            else
            {
                employeeRepository.Delete(NIK);
                Console.WriteLine();
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil Menghapus data {NIK}" });
            }
        }

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            if (employee.NIK == employeeRepository.Get(employee.NIK).NIK )
            {
                employeeRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
        }
    }
}
