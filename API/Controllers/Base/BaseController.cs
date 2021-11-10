using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.Base
{

    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            //if (result.Count() == 0)
            //{
            //    return Ok(new { status = HttpStatusCode.NoContent, message = "Database tidak memiliki data alias kosong" });
            //}
            //else
            //{
            //    return Ok(new { status = HttpStatusCode.OK, result, message = "Data ditemukan" });
            //}
            return Ok(result) ;
        }

        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {
            int result = repository.Insert(entity);
            switch (result)
            {
                case 2:
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = $"Berhasil menambah data"
                    });
                case 0:
                    return Ok(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Gagal menambahkan data, key sudah terdaftar"
                    });
            }
            return Ok();
        }



        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            if (repository.Get(key) == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
            else
            {
                var result = repository.Get(key);
                return Ok(result);
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            if (repository.Get(key) == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
            else
            {
                repository.Delete(key);
                Console.WriteLine();
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil Menghapus data" });
            }
        }

        //[HttpPut]
        //public ActionResult Update(Employee employee)
        //{
        //    try
        //    {
        //        employeeRepository.Update(employee);
        //        return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });

        //    }
        //    catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
        //    }
        //}

        //[HttpPatch]
        //public ActionResult Patch(Employee employee)
        //{
        //    try
        //    {
        //        employeeRepository.Update(employee);
        //        return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil mengubah data {employee.NIK}" });

        //    }
        //    catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        //    {
        //        return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
        //    }
        //}

    }
}