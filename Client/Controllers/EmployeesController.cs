using API.Models;
using API.ViewModels;
using Client.Controllers.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employee;
        public EmployeesController(EmployeeRepository repository) : base(repository) 
        {
            this.employee = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<JsonResult> GetProfileAll() 
        {
            var result = await employee.GetProfile();
            return Json(result);
        }

        public async Task<JsonResult> GetProfile(string id)
        {
            var result = await employee.Profile(id);
            return Json(result);
        }

        public async Task<JsonResult> GetEmployeeAll()
        {
            var result = await employee.GetEmployeeAll();
            return Json(result);
        }

        public async Task<JsonResult> GetEmployee(string id)
        {
            var result = await employee.GetEmployee(id);
            return Json(result);
        }

        public async Task<JsonResult> ChartDegree()
        {
            var result = await employee.GetDataChartDegree();
            return Json(result);
        }

        public async Task<JsonResult> ChartGender()
        {
            var result = await employee.GetDataChartGender();
            return Json(result);
        }
        public async Task<JsonResult> ChartSalary()
        {
            var result = await employee.GetDataChartSalary();
            return Json(result);
        }


        public JsonResult Register(RegisterVM entity)
        {
            var result = employee.Post(entity);
            return Json(result);
        }

        public JsonResult Update(RegisterVM entity)
        {
            var result = employee.Put(entity);
            return Json(result);
        }

        public JsonResult Delete(string id)
        {
            var result = employee.Delete(id);
            return Json(result);
        }

       // [ValidateAntiForgeryToken]
       // [HttpPost("Auth/")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await employee.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("Login", "Employees");
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
            return RedirectToAction("Datatables","Home");
        }

    }
}

 

