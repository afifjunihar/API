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
			// Insert to to employee
			var empResult = new Employee 
			{ 
				NIK = entity.NIK , 
				FirstName = entity.FirstName, 
				LastName = entity.LastName, 
				BirthDate = entity.BirthDate , 
				Phone = entity.Phone,
				Acc	= new Account
				{
					NIK = entity.NIK,
					Password = entity.Password,
					Profiling = new Profiling
					{
						NIK = entity.NIK,
						Education = new Education
						{
							Degree = entity.Degree,
							GPA = entity.GPA,
							University = new University
							{
								Name = entity.UniversityId
							}
						}
					}
				}
			};
			eContext.Employees.Add(empResult);
			var result = eContext.SaveChanges(); 
			return result;
		}

		//public IEnumerable<RegisterVM> Gets()
		//{
		//	return RegisterVM.ToList();
		//}
	}
}
