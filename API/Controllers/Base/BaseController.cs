﻿using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (result.Count() == 0)
            {
                return Ok(new { status = HttpStatusCode.NoContent, message = "Database tidak memiliki data alias kosong" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data ditemukan" });
            }
        }

        [HttpPost]
        public ActionResult<Entity> Post(Entity Entity)
        {
            try
            {
               int result = repository.Insert(Entity);
               return Ok(new{status = HttpStatusCode.OK, message = $"Berhasil menambah data"});
    
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Gagal menambahkan data, Primary Key sudah terdaftar"
                });
            }
           
        }


        [HttpGet("{Key}")]
        public ActionResult<Entity> Get(Key Key)
        {
            try
            {
                var result = repository.Get(Key);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditemukan" });
            }
            catch (System.ArgumentNullException)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            try
            {
                repository.Delete(key);
                Console.WriteLine();
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil Menghapus data" });
            }
            catch (System.ArgumentNullException)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data tidak ditemukan" });
            }
        }

        [HttpPut]
        public ActionResult<Entity> Put(Entity entity)
        {
            try
            {
                repository.Update(entity);
                return Ok(new { status = HttpStatusCode.OK, message = "Berhasil mengubah data" });
            }
            catch (Exception)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
        }

    }


}
