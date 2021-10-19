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
            string NIK = employee.NIK;
            
            var resultAll = employeeRepository.Get();
            if (employeeRepository.Get(NIK) != null)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, NIK sudah terdata di database" });
            }
            else 
            {
                foreach (var item in resultAll)
                {
                    if (item.email == employee.email)
                    {
                        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, email sudah terdata di database" });
                    }
                    else if (item.Phone == employee.Phone)
                    {
                        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, nomor telepon sudah terdata di database" });
                    }
                }
                return Ok(); 
            }
            //if (employeeRepository.Get(employee.NIK) != null)
            //{
            //    return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, NIK sudah terdata di database" });
            //}            
            //else
            //{
            //    if (result.email == employee.email)
            //    {
            //        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, email sudah terdata di database" });
            //    }
            //    else if (result.Phone == employee.Phone)
            //    {
            //        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, nomor telepon sudah terdata di database" });
            //    }
            //    employeeRepository.Insert(employee);
            //    return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil diinput kedalam database", employee });
            //}
            //if (employeeRepository.Get(NIK) == null)
            //{
            //    return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            //}
            //else
            //{
            //    var result = employeeRepository.Get(NIK);
            //    return Ok();
            //}

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
                var result = employeeRepository.Get(NIK);
                return Ok(result);
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
            try
            {
                employeeRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });

            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
        }

        [HttpPatch]
        public ActionResult Patch(Employee employee)
        {
            try
            {
                employeeRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });

            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
        }
    }
}
