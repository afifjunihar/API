using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeAPI.Models
{
	[Table("TB_M_Education")]
	public class Education
	{
		[Key]
		public int Id { get; set; }
		public string Degree { get; set; }
		public string GPA { get; set; }
		public virtual ICollection<Profiling> Profiling { get; set; }
		public virtual University University { get; set; }
		//public string University_Id { get; set; }
	}
}
