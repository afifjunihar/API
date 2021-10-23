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
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        public UniversitiesController(UniversityRepository UniversityRepository) : base(UniversityRepository) { }
    }
}