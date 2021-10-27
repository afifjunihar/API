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
        public int Account_role_id { get; set; }
        public string NIK { get; set; }
        public int Role_Id { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        [ForeignKey("Role_Id")]
        public virtual Role Role { get; set; }
    }
}
