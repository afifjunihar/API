using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {

        }

        public int Register(RegisterVM registerVM)
        {
            context.Employees.Add();
            context.Accounts.Add();
            context.Educations.Add();
            var resullt = context.SaveChanges();
            return resullt;
        }

    }
}
