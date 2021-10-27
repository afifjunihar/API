﻿using API.Context;
using API.HashPassword;
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

        public override int Insert(Employee employee)
        {
            if (context.Employees.Find(employee.NIK) != null)
            {
                return 0;
            }
            else if (context.Employees.Where(e => e.Email == employee.Email).FirstOrDefault() != null)
            {
                return 2;
            }
            else if (context.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault() != null)
            {
                return 3;
            }
            else
            {
                var check = 
                context.Employees.Add(employee);
                return 1;
            }
        }
        public int Register(RegisterVM registerVM)
        {
            var checkEmail = context.Employees.Where(p => p.Email == registerVM.Email).FirstOrDefault();
            var checkPhone = context.Employees.Where(p => p.Phone == registerVM.Phone).FirstOrDefault();
            var checkNik = context.Employees.Find(registerVM.NIK);
            if (checkNik != null)
            {
                return 1;
            }
            else if (checkEmail != null)
            {
                return 2;
            }
            else if (checkPhone != null)
            {
                return 3;
            }
            else
            {
                string hashPassword = Hashing.HashPassword(registerVM.Password);
                var empResult = new Employee
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Phone = registerVM.Phone,
                    Salary = registerVM.Salary,
                    Email = registerVM.Email,
                    Account = new Account
                    {
                        NIK = registerVM.NIK,
                        Password = hashPassword,
                        Profiling = new Profiling
                        {
                            NIK = registerVM.NIK,
                            Education = new Education
                            {
                                Degree = registerVM.Degree,
                                GPA = registerVM.GPA,
                                University_Id = registerVM.UniversityId
                            }
                        }
                    }
                };
                context.Employees.Add(empResult);
                var result = context.SaveChanges();
                return result;
            }
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

        public IEnumerable GetProfileBy(string NIK)
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
                          where e.NIK == NIK
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