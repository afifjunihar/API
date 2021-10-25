using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models
{
	[Table("TB_T_Account")]
	public class Account
	{
		[Key]
		public string NIK { get; set; }
		public string Password { get; set; }
		public virtual Employee Employee { get; set; }
		public virtual Profiling Profiling { get; set; }
	}
}
