using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeAPI.Models
{
	[Table("TB_T_Profiling")]
	public class Profiling
	{
		[Key]
		public string NIK { get; set; }
		public virtual Account Account { get; set; }
		public virtual Education Education { get; set; }
	}
}
