using API.Context;
using API.HashingPassword;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext eContext;
        public EmployeeRepository(MyContext myContext) : base(myContext) 
        {
            this.eContext = myContext;
        }

        public object getFullName(LoginVM loginVM) 
        {
            var checkEmail = eContext.Employees.Where(p => p.Email == loginVM.Email).FirstOrDefault();
            var Fullname = checkEmail.FirstName + " " + checkEmail.LastName;
            return Fullname;
        }

        public int Login(LoginVM loginVM) 
        {
            var checkEmail = eContext.Employees.Where(p => p.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                var dataLogin = checkEmail.NIK;
                var dataPassword = eContext.Accounts.Find(dataLogin).Password;
                var verify = Hashing.ValidatePassword(loginVM.Password, dataPassword);

                if (verify)
                {
                    return 0;
                }
                else
                {
                    return 2;
                }            
            }
            else 
            {
                return 1;
            }
        }

        public int Register(RegisterVM entity) 
        {
            var checkEmail = eContext.Employees.Where(p => p.Email == entity.Email).FirstOrDefault();
            var checkPhone = eContext.Employees.Where(p => p.Phone == entity.Phone).FirstOrDefault();
            var checkNik = eContext.Employees.Find(entity.NIK);
            if (entity.NIK == string.Empty)
            {
                return 0;
            }  
            else if (checkNik != null)
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
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(entity.Password);
                var employee = new Employee
                {

                    NIK = entity.NIK,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Phone = entity.Phone,
                    Salary = entity.Salary,
                    BirthDate = entity.BirthDate,
                    Email = entity.Email,
                    Gender = (Gender)entity.gender,
                    Account = new Account
                    {
                        NIK = entity.NIK,
                        Password = Hashing.HashPassword(entity.Password),
                        Profiling = new Profiling
                        {
                            NIK = entity.NIK,
                            Education = new Education
                            {
                                Degree = entity.Degree,
                                Gpa = entity.Gpa,
                                UniversityId = entity.UniversityId
                            }
                        }
                    }
                };
                eContext.Employees.Add(employee);
                var result = eContext.SaveChanges();
                return result;
            }
        }

        public IEnumerable GetProfile() 
        {
           var listEmployee = eContext.Employees.ToList();
           var listProfiling = eContext.Profilings.ToList();
           var listEducations = eContext.Educations.ToList();
           var listUniversities = eContext.Universities.ToList();

            var getData = from a in listEmployee

                          join b in listProfiling on a.NIK equals b.NIK into table1
                          from b in table1.ToList()

                          join d in listEducations on b.EducationId equals d.EducationId into table2
                          from d in table2.ToList()

                          join f in listUniversities on d.UniversityId equals f.UniversityId into table3
                          from f in table3
                          select new
                          {
                              NIK = a.NIK,
                              Fullname = a.FirstName + " " + a.LastName,
                              Phone = a.Phone,
                              BirthDate = a.BirthDate,
                              Salary = a.Salary,
                              Email = a.Email,
                              Degree = d.Degree,
                              GPA = d.Gpa,
                              University = f.Name
                          };
            return getData;
        }

        public object GetProfile(string NIK)
        {
            var listEmployee = eContext.Employees.ToList();
            var listProfiling = eContext.Profilings.ToList();
            var listEducations = eContext.Educations.ToList();
            var listUniversities = eContext.Universities.ToList();

            var getData = from a in listEmployee
                          where a.NIK == NIK
                          join b in listProfiling on a.NIK equals b.NIK into table1                         

                          from b in table1.ToList()
                          join d in listEducations on b.EducationId equals d.EducationId into table2

                          from d in table2.ToList()
                          join f in listUniversities on d.UniversityId equals f.UniversityId into table3

                          from f in table3
                          select new
                          {
                              NIK = a.NIK,
                              Fullname = a.FirstName + " " + a.LastName,
                              Phone = a.Phone,
                              BirthDate = a.BirthDate,
                              Salary = a.Salary,
                              Email = a.Email,
                              Degree = d.Degree,
                              GPA = d.Gpa,
                              University = f.Name
                          };
            return getData.FirstOrDefault();
        }

        //public Employee GetProfile(string NIK) 
        //{
        //    var dataEmployee = eContext.Employees.Find(NIK);
        //    var dataProfiling = eContext.Profilings.Find(NIK);
        //    var dataEducation = eContext.Educations.Find(dataProfiling.EducationId);
        //    var dataUniversity = eContext.Universities.Find(dataEducation.UniversityId);

        //    var getData = new
        //                  {
        //                      NIK = NIK,
        //                      Fullname = dataEmployee.FirstName + " " + dataEmployee.LastName,
        //                      Phone = dataEmployee.Phone,
        //                      BirthDate = dataEmployee.BirthDate,
        //                      Salary = dataEmployee.Salary,
        //                      Email = dataEmployee.Email,
        //                      Degree = dataEducation.Degree,
        //                      GPA = dataEducation.Gpa,
        //                      University = dataUniversity.Name
        //                  };
        //    return getData;
        //}
    }
}
