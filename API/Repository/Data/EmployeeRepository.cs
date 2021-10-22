using EmployeeAPI.Context;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.Interface;
using EmployeeAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeAPI.Repository.Data
{

	public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
	{
		private readonly MyContext eContext;
		public EmployeeRepository(MyContext myContext) : base(myContext)
		{
			this.eContext = myContext;
		}
		public int Register(RegisterVM entity)
		{
			var empResult = entity;
			return 0;
		}
	}
}
