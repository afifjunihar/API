using EmployeeAPI.Context;
using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository.Data
{
	public class UniversityRepositories : GeneralRepository<MyContext, University, int>
	{
		public UniversityRepositories(MyContext myContext) : base(myContext)
		{

		}
	}
}
