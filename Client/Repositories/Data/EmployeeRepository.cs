using System;
using API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Base.Urls;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {

        }
    }
}
