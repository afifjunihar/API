
using API.Models;
using API.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        public EmployeesController(EmployeeRepository EmployeeRepository) : base(EmployeeRepository) { }
    }
}
