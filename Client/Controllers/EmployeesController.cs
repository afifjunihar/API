using Client.Base.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Repositories.Data;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
