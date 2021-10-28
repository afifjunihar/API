﻿
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = employee.Register(registerVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, message = "NIK sudah terdaftar" });
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
        [Route("Register")]
        [HttpGet]
        public ActionResult GetProfileInfo()
        { 
            return Ok(employee.GetProfile());
        }

        [Route("Register/{nik}")]
        [HttpGet]
        public ActionResult GetProfileInfo(string nik)
        {
            return Ok(employee.GetProfileBy(nik));
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM login)
        {
            var checkLogin = employee.Login(login);
            if (checkLogin == 0)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Selamat Datang" });
            }
            else if (checkLogin == 1)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email yang Anda Masukan Tidak Terdaftar"
                });
            }
            else if (checkLogin == 2)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Password yang Anda Masukan Salah"
                });
            }
            else
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Terjadi Error"
                });
            }
        }

        [HttpGet]
        [Route("TestCORS")]
        public string funtion()
        {
            return "Test CORS berhasil";
        }
    }
}