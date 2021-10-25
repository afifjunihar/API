//using API.Models;
//using API.Repository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeesController : ControllerBase
//    {
//        private readonly EmployeeRepository employeeRepository;
//        public EmployeesController(EmployeeRepository employeeRepository)
//        {
//            this.employeeRepository = employeeRepository;
//        }

//        [HttpPost]
//        public ActionResult Post(Employee employee)
//        {
//            int result = employeeRepository.Insert(employee);
//            switch (result)
//            {
//                case 0:
//                    return Ok(new
//                    {
//                        status = HttpStatusCode.OK,
//                        message = $"Berhasil menambah data {employee.NIK}"
//                    });
//                case 1:
//                    return Ok(new
//                    {
//                        status = HttpStatusCode.BadRequest,
//                        message = "Gagal menambahkan data, NIK sudah terdaftar"
//                    });
//                case 2:
//                    return Ok(new
//                    {
//                        status = HttpStatusCode.BadRequest,
//                        message = "Gagal menambahkan data, email sudah terdaftar"
//                    });
//                case 3:
//                    return Ok(new
//                    {
//                        status = HttpStatusCode.BadRequest,
//                        message = "Gagal menambahkan data, nomor telpon sudah terdaftar"
//                    });
//            }
//            return Ok();
//        }


//        [HttpGet]
//        public ActionResult Get()
//        {
//            var result = employeeRepository.Get();
//            if (result.Count() == 0)
//            {
//                return Ok(new { status = HttpStatusCode.NoContent, message = "Database tidak memiliki data alias kosong" });
//            }
//            else
//            {
//                return Ok(new { status = HttpStatusCode.OK, result, message = "Data ditemukan" });
//            }
//        }

//        [HttpGet("{NIK}")]
//        public ActionResult Get(string NIK)
//        {
//            if (employeeRepository.Get(NIK) == null)
//            {
//                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
//            }
//            else
//            {
//                var result = employeeRepository.Get(NIK);
//                return Ok(result);
//            }
//        }

//        [HttpDelete("{NIK}")]
//        public ActionResult Delete(string NIK)
//        {
//            if (employeeRepository.Get(NIK) == null)
//            {
//                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
//            }
//            else
//            {
//                employeeRepository.Delete(NIK);
//                Console.WriteLine();
//                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil Menghapus data {NIK}" });
//            }
//        }

//        [HttpPut]
//        public ActionResult Update(Employee employee)
//        {
//            try
//            {
//                employeeRepository.Update(employee);
//                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });

//            }
//            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
//            {
//                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
//            }
//        }

//        [HttpPatch]
//        public ActionResult Patch(Employee employee)
//        {
//            try
//            {
//                employeeRepository.Update(employee);
//                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });

//            }
//            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
//            {
//                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
//            }
//        }
//    }
//}
