﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int Gender { get;set;}
        public int RoleId { get; set; }
        public string Password { get; set; }

        public int EducationId { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; } 

        public int UniversityId { get; set; }
        public string Name { get; set; }
    }
}