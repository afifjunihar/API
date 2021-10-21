using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, NIK sudah terdata di Database" });
            }
            else
            {
                foreach (var item in resultAll)
                {
                    if (item.Email == employee.Email)
                    {
                        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, email sudah terdata di Database" });
                    }
                    else if (item.Phone == employee.Phone)
                    {
                        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, nomor telepon sudah terdata di Database" });
                    }
                    else if (item.Phone == employee.Phone && item.Email == employee.Email)
                    {
                        return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, No. Telefon dan Email sudah terdata di Database" });
                    }
                }
                employeeRepository.Insert(employee);
                return Ok(new { StatusCode = HttpStatusCode.OK, message = "Data Berhasil dimasukan ke Dabase" });
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get();
            if (result.Count() == 0)
            {
                return Ok(new { status = HttpStatusCode.NoContent, message = "Database tidak memiliki data alias kosong" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data ditemukan" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var Message = employeeRepository.Get(NIK);
            if (employeeRepository.Get(NIK) == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data Berhasil ditemukan", Message });
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

     
        [HttpPatch("{NIK}")]
        public ActionResult UpdatePatch(string NIK, Employee employee) 
        {
            if (NIK != employeeRepository.Get(employee.NIK).NIK || employeeRepository.Get(employee.NIK) == null)
            {
                return BadRequest(new { status = HttpStatusCode.OK, message = "Data Gagal Diubah" });
            }
            else 
            {

                if (employee.FirstName == null)
                {
                    employee.FirstName = employeeRepository.Get(employee.NIK).FirstName;
                }
                if (employee.LastName == null)
                {
                    employee.LastName = employeeRepository.Get(employee.NIK).LastName;
                }
                if (employee.Email == null)
                {
                    employee.Email = employeeRepository.Get(employee.NIK).Email;
                }
                else 
                {
                    foreach (var item in employeeRepository.Get())
                    {
                        if (item.Email == employee.Email)
                        {
                            return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, email sudah terdata di Database" });
                        }
                        else if (item.Phone == employee.Phone)
                        {
                            return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, nomor telepon sudah terdata di Database" });
                        }
                    }
                }


                if (employee.Salary == 0)
                {
                    employee.Salary = employeeRepository.Get(employee.NIK).Salary;
                }
                if (employee.Phone == null)
                {
                    employee.Phone = employeeRepository.Get(employee.NIK).Phone;
                }
                else 
                {
                    foreach (var item in employeeRepository.Get())
                    {
                        if (item.Phone == employee.Phone)
                        {
                            return Ok(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan, nomor telepon sudah terdata di Database" });
                        }
                    }
                }
                if (employee.BirthDate == DateTime.MinValue)
                {
                    employee.BirthDate = employeeRepository.Get(employee.NIK).BirthDate;
                }

                employeeRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, message = "berhasil mengubah data" });
            }
        }
    }
}

