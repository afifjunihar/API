using API.Context;
using API.HashingPassword;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public object GetFullName(LoginVM loginVM) 
        {
            var checkEmail = eContext.Employees.Where(p => p.Email == loginVM.Email).FirstOrDefault();
            var Fullname = checkEmail.FirstName + " " + checkEmail.LastName;
            return Fullname;
        }

        public string[] GetUserData(LoginVM loginVM) 
        {
            var getData = eContext.Employees.Where(p => p.Email == loginVM.Email).FirstOrDefault();
            var NIK = getData.NIK;
            var getRole = eContext.AccountRoles.Where(x => x.NIK == NIK).ToList();

            List<string> result = new List<string>();
            foreach (var x in getRole) 
            {
                result.Add(eContext.Roles.Where(y => y.RoleId == x.RoleId).First().RoleName);
            }
            return result.ToArray();
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

        public int SignManager(RegisterVM registervm)
        {
            var cNik = eContext.Employees.Find(registervm.NIK);
            if (cNik != null) 
            {
                var RoleManager = new AccountRole
                {
                    NIK = registervm.NIK,
                    RoleId = 2
                };
                eContext.AccountRoles.Add(RoleManager);
                eContext.SaveChanges();
                return 0;
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
                var employee = new Employee
                {

                    NIK = entity.NIK,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Phone = entity.Phone,
                    Salary = entity.Salary,
                    BirthDate = entity.BirthDate,
                    Email = entity.Email,
                    Gender = (Gender)entity.Gender,
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

                if (entity.RoleId == 0)
                {
                    entity.RoleId = 1;
                }

                var empRole = new AccountRole
                {
                    NIK = entity.NIK,
                    RoleId = entity.RoleId
                };
                eContext.AccountRoles.Add(empRole);
                eContext.Employees.Add(employee);
                var result = eContext.SaveChanges();
                return result;
            }
        }

        public dynamic GetProfile() 
        {
           var listEmployee = eContext.Employees.ToList();
           var listProfiling = eContext.Profilings.ToList();
           var listEducations = eContext.Educations.ToList();
           var listUniversities = eContext.Universities.ToList();
           var listAccountRole = eContext.AccountRoles.ToList();
           var listRole = eContext.Roles.ToList();

            var getData = from a in listEmployee
                          join b in listProfiling on a.NIK equals b.NIK into table1

                          from b in table1.ToList()
                          join d in listEducations on b.EducationId equals d.EducationId into table2

                          from d in table2.ToList()
                          join f in listUniversities on d.UniversityId equals f.UniversityId into table3

                          from f in table3.ToList()
                          join g in listAccountRole on a.NIK equals g.NIK into table4

                          from h in table4.ToList()
                          join i in listRole on h.RoleId equals i.RoleId into table5
                          from j in table5

                          select new
                          {
                              a.NIK,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Phone,
                              a.BirthDate,
                              a.Salary,
                              a.Email,
                              d.Degree,
                              d.Gpa,
                              f.Name,
                              j.RoleName
                          };
            int hitungData = getData.Count();
            if (hitungData == 0)
            {
                string checkData = "Tidak ditemukan Data pada Database";
                return checkData;
            }
            else
            {
                return getData;
            }   
        }

        public object GetProfile(string NIK)
        {
            var listEmployee = eContext.Employees.ToList();
            var listProfiling = eContext.Profilings.ToList();
            var listEducations = eContext.Educations.ToList();
            var listUniversities = eContext.Universities.ToList();
            var listAccountRole = eContext.AccountRoles.ToList();
            var listRole = eContext.Roles.ToList();

            var getData = from a in listEmployee
                          where a.NIK == NIK
                          join b in listProfiling on a.NIK equals b.NIK into table1

                          from b in table1.ToList()
                          join d in listEducations on b.EducationId equals d.EducationId into table2

                          from d in table2.ToList()
                          join f in listUniversities on d.UniversityId equals f.UniversityId into table3

                          from f in table3.ToList()
                          join g in listAccountRole on a.NIK equals g.NIK into table4

                          from h in table4.ToList()
                          join i in listRole on h.RoleId equals i.RoleId into table5

                          from j in table5                          
                          select new
                          {
                              a.NIK,
                              Fullname = a.FirstName + " " + a.LastName,
                              a.Phone,
                              a.BirthDate,
                              a.Salary,
                              a.Email,
                              d.Degree,
                              d.Gpa,
                              f.Name,
                              j.RoleName
                          };
            return getData.FirstOrDefault();
        }

        public override int Delete(string NIK)
        {
            var delete = eContext.Employees.Find(NIK);
            var findProfiling = eContext.Profilings.Find(NIK);
            var findEdu = eContext.Educations.Find(findProfiling.EducationId);

            //foreach (var x in eContext.AccountRoles)
            //{
            //    if (x.NIK == NIK)
            //    {
            //        var fingAccRole = eContext.AccountRoles.Where(p => p.NIK == NIK).FirstOrDefault();
            //        var accrole = eContext.AccountRoles.Find(fingAccRole.AccountRoleId);
            //        eContext.AccountRoles.Remove(accrole);
            //        break;
            //    }
            //}

            eContext.Employees.Remove(delete);
            eContext.Educations.Remove(findEdu);
            var result = eContext.SaveChanges();
            return result;
        }
    }
}
