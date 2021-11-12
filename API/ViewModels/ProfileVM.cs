using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ProfileVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string gpa { get; set; }
        public string name { get; set; }
        public string roleName { get; set; }
    }
}
