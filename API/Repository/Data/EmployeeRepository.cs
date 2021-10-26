using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.HashingPassword;


namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
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
                        Password = Hashing.HashPassword(registerVM.Password),
                        Profiling = new Profiling
                        {
                            NIK = registerVM.NIK,
                            Education = new Education
                            {
                                Degree = registerVM.Degree,
                                GPA = registerVM.GPA,
                                UniversityId = registerVM.UniversityId
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
            var profile = (from Employee in context.Employees
                           join Account in context.Accounts on Employee.NIK equals Account.NIK
                           join Profiling in context.Profilings on Account.NIK equals Profiling.NIK
                           join Education in context.Educations on Profiling.EducationId equals Education.Id
                           join Universitas in context.Universities on Education.UniversityId equals Universitas.Id
                           select new
                           {
                               NIK = Employee.NIK,
                               Fullname = Employee.FirstName + " " + Employee.LastName,
                               Phone = Employee.Phone,
                               BirthDate = Employee.BirthDate,
                               Salary = Employee.Salary,
                               Email = Employee.Email,
                               Degree = Education.Degree,
                               GPA = Education.GPA,
                               Nama_Universitas = Universitas.Name
                           });
            return profile;
        }

        public Object GetProfile(string NIK)
        {
            var profile = (from Employee in context.Employees
                           join Account in context.Accounts on Employee.NIK equals Account.NIK
                           join Profiling in context.Profilings on Account.NIK equals Profiling.NIK
                           join Education in context.Educations on Profiling.EducationId equals Education.Id
                           join Universitas in context.Universities on Education.UniversityId equals Universitas.Id
                           where Employee.NIK == NIK
                           select new
                           {
                               NIK = Employee.NIK,
                               Fullname = Employee.FirstName + " " + Employee.LastName,
                               Phone = Employee.Phone,
                               BirthDate = Employee.BirthDate,
                               Salary = Employee.Salary,
                               Email = Employee.Email,
                               Degree = Education.Degree,
                               GPA = Education.GPA,
                               Nama_Universitas = Universitas.Name
                           });
            var result = profile.First();
            return result;
        }

    }
}
