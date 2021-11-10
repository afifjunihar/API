using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Base.Controllers;
using API.ViewModel;

namespace Client.Controllers
{
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

        public async Task<JsonResult> GetProfile()
        {
            var result = await employee.GetProfile();
            return Json(result);
        }

        public async Task<JsonResult> Profile(string id)
        {
            var result = await employee.Profile(id);
            return Json(result);
        }

        public JsonResult Register(RegisterVM entity)
        {
            var result = employee.Register(entity);
            return Json(result);
        }
    }
}
