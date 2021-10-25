﻿using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext) { this.context = myContext; }
        public int Register(RegisterVM entity)
        {
            //register employee
            var register = new Employee
            {
                NIK = entity.NIK,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Phone = entity.Phone,
                Email = entity.Email,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                Salary = entity.Salary,
                Account = new Account
                {
                    NIK = entity.NIK,
                    Password = entity.Password,
                    Profiling = new Profiling
                    {
                        NIK = entity.NIK,
                        Education = new Education
                        {
                            Degree = entity.Degree,
                            GPA = entity.GPA,
                            University_Id = entity.UniversityId
                        }
                    }
                }

            };
            context.Employees.Add(register);
            int result = context.SaveChanges();
            return result;
        }

        public IEnumerable GetProfile()
        {
            var employeeData = context.Employees.ToList();
            var profilingData = context.Profilings.ToList();
            var educationData = context.Educations.ToList();
            var universityData = context.Universities.ToList();

            var profile = from e in employeeData
                          join p in profilingData on e.NIK equals p.NIK into table1
                          from p in table1.ToList()
                          join ed in educationData on p.Education_Id equals ed.Id into table2
                          from ed in table2.ToList()
                          join uni in universityData on ed.University_Id equals uni.Id into table3
                          from uni in table3
                          select new
                          {
                              NIK = e.NIK,
                              Fullname = e.FirstName + " " + e.LastName,
                              Phone = e.Phone,
                              Birthdate = e.BirthDate,
                              Salary = e.Salary,
                              Email = e.Email,
                              Degree = ed.Degree,
                              GPA = ed.GPA,
                              University = uni.Name
                          };

            return profile;
        }

    }
}