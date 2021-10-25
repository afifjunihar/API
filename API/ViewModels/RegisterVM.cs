﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.ViewModels
{
	public class RegisterVM
	{
		public string NIK { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public DateTime BirthDate { get; set; }
		public string Password { get; set; }
		public string Degree { get; set; }
		public string GPA { get; set; }
		public string UniversityId { get; set; }

	}
}
