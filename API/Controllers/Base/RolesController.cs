using API.Models;
using API.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository RoleRepository) : base(RoleRepository) { }
    }
}
