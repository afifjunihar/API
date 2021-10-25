using EmployeeAPI.Base;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.Data;
using EmployeeAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EmployeeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
	{
		private readonly EmployeeRepository employee;
		public EmployeesController(EmployeeRepository employeeRepository) : base (employeeRepository)
		{
			this.employee = employeeRepository;
		}
		[Route("Register")]
		[HttpPost]
		public ActionResult Register(RegisterVM entity)
		{
			var result = employee.Register(entity);
			if (result > 0)
			{
				return Ok("Berhasil Registrasi");
			}
			return BadRequest();
		}

		//[Route("Register")]
		//[HttpGet]
		//public ActionResult Gets()
		//{
		//	var result = employee.Gets();
		//	if (result != null)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest();
		//}
	}
}
