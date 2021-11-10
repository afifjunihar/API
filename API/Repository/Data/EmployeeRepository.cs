using API.Context;
using API.HashPassword;
using API.Models;
using API.ViewModel;
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
                    Gender = registerVM.Gender,
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
                        },
                    }
                };

                if (registerVM.Role_Id == 0)
                {
                    registerVM.Role_Id = 1;
                }

                var empRole = new AccountRole
                {
                    NIK = registerVM.NIK,
                    Role_Id = registerVM.Role_Id
                };
                context.Employees.Add(empResult);
                context.AccountRoles.Add(empRole);
                var result = context.SaveChanges();
                return result;
            }
        }

        public int UpdateEmployee(RegisterVM registerVM)
        {
            var checkNIK = context.Employees.Find(registerVM.NIK);
            var checkNIK2 = context.Employees.Where(p => p.NIK == registerVM.NIK);
            var checkEmail = context.Employees.Where(p => p.Email == registerVM.Email).Count();
            var checkPhone = context.Employees.Where(p => p.Phone == registerVM.Phone).Count();
            if (checkEmail > 1)
            {
                return 2;
            }
            else if (checkPhone > 1)
            {
                return 3;
            }
            else
            {
                context.Entry(checkNIK).State = EntityState.Detached;
                string hashPassword = Hashing.HashPassword(registerVM.Password);
                var empResult = new Employee
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Phone = registerVM.Phone,
                    Salary = registerVM.Salary,
                    Gender = registerVM.Gender,
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
                        },
                    }
                };

                if (registerVM.Role_Id == 0)
                {
                    registerVM.Role_Id = 1;
                }

                context.Entry(empResult).State = EntityState.Modified;
                var result = context.SaveChanges();
                return 4;
            }
        }

        public dynamic GetProfile()
        {
            var employeeData = context.Employees.ToList();
            var profilingData = context.Profilings.ToList();
            var educationData = context.Educations.ToList();
            var universityData = context.Universities.ToList();
            var accountRoleData = context.AccountRoles.ToList();
            var roleData = context.Roles.ToList();

            var profile = from e in employeeData
                          join p in profilingData on e.NIK equals p.NIK into table1

                          from p in table1.ToList()
                          join ed in educationData on p.Education_Id equals ed.Id into table2

                          from ed in table2.ToList()
                          join uni in universityData on ed.University_Id equals uni.Id into table3

                          from uni in table3.ToList() 
                          join ar in accountRoleData on e.NIK equals ar.NIK into table4

                          from ar in table4.ToList()
                          join r in roleData on ar.Role_Id equals r.Role_Id into table5

                          from r in table5

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
                              University = uni.Name,
                              Role = r.Role_Name
                          };

            if (profile.Count() == 0 )
            {
                string result = "Tidak ditemukan data pada database";
                return result;
            }
            else
            {
                return profile;
            }
        }

        public IEnumerable GetProfileBy(string NIK)
        {
            var employeeData = context.Employees.ToList();
            var profilingData = context.Profilings.ToList();
            var educationData = context.Educations.ToList();
            var universityData = context.Universities.ToList();
            var accountRoleData = context.AccountRoles.ToList();
            var roleData = context.Roles.ToList();

            var profile = from e in employeeData
                          join p in profilingData on e.NIK equals p.NIK into table1
                          from p in table1.ToList()
                          join ed in educationData on p.Education_Id equals ed.Id into table2
                          from ed in table2.ToList()
                          join uni in universityData on ed.University_Id equals uni.Id into table3

                          from uni in table3.ToList()
                          join ar in accountRoleData on e.NIK equals ar.NIK into table4

                          from ar in table4.ToList()
                          join r in roleData on ar.Role_Id equals r.Role_Id into table5

                          from r in table5
                          where e.NIK == NIK
                          select new
                          {
                              Role = r.Role_Name,
                              NIK = e.NIK,
                              Firstname = e.FirstName,
                              Lastname = e.LastName,
                              Phone = e.Phone,
                              Birthdate = e.BirthDate,
                              Salary = e.Salary,
                              Email = e.Email,
                              Degree = ed.Degree,
                              GPA = ed.GPA,
                              University = uni.Name,
                              Gender =  e.Gender
                          };

            return profile;
        }
        public int Login(LoginVM loginVM)
        {
            var checkEmail = context.Employees.Where(p => p.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                var dataLogin = checkEmail.NIK;
                var dataPassword = context.Accounts.Find(dataLogin).Password;
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
        public override int Delete(string NIK)
        {
            var delete = context.Employees.Find(NIK);
            var findProfiling = context.Profilings.Find(NIK);
            var findEdu = context.Educations.Find(findProfiling.Education_Id);

            int roleNumbers = context.AccountRoles.Where(p => p.NIK == NIK).Count();
            for (int i = 0; i < roleNumbers; i++)
            {
                var findRoles = context.AccountRoles.Where(p => p.NIK == NIK).FirstOrDefault();
                context.AccountRoles.Remove(findRoles);
                var _result = context.SaveChanges();
            }

            context.Employees.Remove(delete);
            context.Educations.Remove(findEdu);
            var result = context.SaveChanges();
            return result;
        }

        public int SignManager(AccountRole accountRole)
        {
            accountRole.Role_Id = 2;
            context.AccountRoles.Add(accountRole);
            var result = context.SaveChanges();
            return result;
        }

        public string[] GetRole(LoginVM loginVM)
        {
            var dataExist = context.Employees.Where(fn => fn.Email == loginVM.Email).FirstOrDefault();
            var userNIK = dataExist.NIK;
            var userRole = context.AccountRoles.Where(fn => fn.NIK == userNIK).ToList();
            List<string> result = new List<string>();
            foreach (var item in userRole)
            {
                result.Add(context.Roles.Where(fn => fn.Role_Id == item.Role_Id).First().Role_Name);
            }

            return result.ToArray();
        }

        public int CountMale()
        {
            var employeeData = context.Employees.ToList();
            var genderMale = (from e in employeeData
                             where e.Gender == Gender.Male
                             select e.NIK).Count();
            return genderMale;
        }
        public int CountFemale()
        {
            var employeeData = context.Employees.ToList();
            var genderFemale = (from f in employeeData
                                where f.Gender == Gender.Female
                                select f.NIK).Count();
            return genderFemale;
        }

        public IEnumerable SalaryChart()
        {
            var employeeData = context.Employees.ToList();

            return employeeData;
        }

        public IEnumerable ByDegree()
		{
			var degree = from edu in context.Educations
							 group edu by edu.Degree into degreeChart
							 select new
							 {
								 Degree = degreeChart.Key,
								 Count = degreeChart.Count()
							 };
			return degree.ToList();
		}
    }
}