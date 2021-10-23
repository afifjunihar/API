//using API.Context;
//using API.Models;
//using API.Repository.Interface;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;

//namespace API.Repository
//{
//    public class EmployeeRepository : IEmployeeRepository
//    {
//        private readonly MyContext context;
//        public EmployeeRepository(MyContext context)
//        {
//            this.context = context;
//        }
//        public int Delete(string NIK)
//        {
//            var entity = context.Employees.Find(NIK);
//            context.Remove(entity);
//            var result = context.SaveChanges();
//            return result;
//        }

//        public IEnumerable<Employee> Get()
//        {
//            return context.Employees.ToList();
//        }

//        public Employee Get(string NIK)
//        {
//            var entity = context.Employees.Find(NIK);
//            return entity;

//        }

//        public int Insert(Employee employee)
//        {
//            if (context.Employees.Find(employee.NIK) != null)
//            {
//                return 1;
//            }
//            else if (context.Employees.Where(e => e.Email == employee.Email).FirstOrDefault() != null)
//            {
//                return 2;
//            }
//            else if (context.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault() != null)
//            {
//                return 3;
//            }
//            else
//            {
//                context.Employees.Add(employee);
//                context.SaveChanges();
//                return 0;
//            }

//        }

//        public int Update(Employee employee)
//        {
//            context.Entry(employee).State = EntityState.Modified;
//            var result = context.SaveChanges();
//            return result;
//        }
//    }
//}
