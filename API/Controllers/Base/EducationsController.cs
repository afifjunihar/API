using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : BaseController<Education, EducationRepository, int>
    {
        public EducationsController(EducationRepository EducationRepository) : base(EducationRepository) { }
    }
}