using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_Employee")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }

        public Employee Employee { get; set; }
        public Profiling Profiling { get; set; }
    }
}
