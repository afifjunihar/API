﻿
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
    //public class DetailsViewModel
    //{
    //    public Employee Employee { get; set; }
    //    public Account Account { get; set; }
    //    public Profiling Profiling { get; set; }
    //    public Education Education { get; set; }
    //    public University University { get; set; }
    //}
   
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public readonly EmployeeRepository employee;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository) 
        {
            this.employee = employeeRepository;
        }

        [HttpPost]
        [Route("Registration")]
        public ActionResult Register(RegisterVM register)
        {
            try
            {
                var result = employee.Register(register);
                if (result != 0)
                {
                    return Ok("Berhasil Registrasi");
                }
                else if (result == 0)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Gagal menambahkan data, Primary Key tidak boleh kosong"
                    });
                }
                else 
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest
                    });
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Gagal menambahkan data, Primary Key sudah terdaftar"
                });
            }


        }

        [HttpGet]
        [Route("Registration/Profile")]
        public ActionResult<Employee> GetProfile() 
        {
            try
            {
            var result = employee.GetProfile();    
            return Ok(new { status = HttpStatusCode.OK, message = "Data Berhasil Ditemukan", result});  

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
        public ActionResult Register(string NIK)
        {
            try
            {

                var result = employee.GetProfile(NIK);
                return Ok(new { status = HttpStatusCode.OK, message = "Data Berhasil Ditemukan", result });
   
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
        
    }
 }





   
