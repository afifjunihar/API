using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class EmployeeVM
    {
        public string NIK { get; set; } 
        public string FirstName { get; set; }      
        public string LastName { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }      
        public string Gender { get; set; }
    }
}
