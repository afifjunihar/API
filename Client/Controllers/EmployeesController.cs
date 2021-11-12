using Client.Base.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Repositories.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetProfile()
        {
            var result = await repository.GetProfile();
            return Json(result);
        }

        public JsonResult Register(RegisterVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }

        public ActionResult<RegisterVM> Daftar(RegisterVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("login", "home");
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
            return RedirectToAction("datatable", "home");
        }
        [Authorize]
        public IActionResult TesLogin()
        {
            return View();
        }


    }
}
