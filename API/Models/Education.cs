using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_T_Education")]
    public class Education
    {
       [Key]
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        [ForeignKey("UniversityId")]
        public int UniversityId { get; set; }

        public  University University { get; set; }
        public  string NIK { get; set; }
        public  ICollection<Profiling> Profiling { get; set; }
    }
}
