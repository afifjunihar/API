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
         public class LoginsController : BaseController<LoginVM, LoginRepository, string>
        {
            private readonly LoginRepository login;
            public LoginsController(LoginRepository repository) : base(repository)
            {
                this.login = repository;
            }

            public IActionResult Index()
            {
                return View();
            }
            public IActionResult Login()
            {
                return View();
            }

            public async Task<IActionResult> SignIn(LoginVM entity)
            {
                    var jwtToken = await login.Auth(entity);
                    var token = jwtToken.Token;

                    if (token == null)
                    {
                        return RedirectToAction("Index");
                    }

                    HttpContext.Session.SetString("JWToken", token);
                    //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
                    //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");
                    return RedirectToAction("Index", "Home");
            }

            [Authorize]
            [HttpGet("Logout")]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }



    }

}