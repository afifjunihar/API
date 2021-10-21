﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_University")]
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        public string Name { get; set; }
        public int EducationId { get; set; }
        public ICollection<Education> Education { get; set; }
    }
}
