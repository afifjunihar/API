using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountRole
    {
        [Key]
        public int AccountRoleId { get; set; }
        [ForeignKey("RoleId")]
        [JsonIgnore]
        public virtual int RoleId { get; set; }
        [ForeignKey("NIK")]
        [JsonIgnore]
        public virtual string NIK { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }


    }
}
