using Newtonsoft.Json;
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
        [Required]
        public string Password { get; set; }


        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        [ForeignKey("NIK")]
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
