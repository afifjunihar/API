﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Models
{
	[Table("TB_M_University")]
	public class University
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Education> Education { get; set; }
	}
}
