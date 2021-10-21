using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_Profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [ForeignKey("EducationId")]
        public int EducationId { get; set; }

        public Education Education { get; set; }
        public Account Account { get; set; }
    }
}
