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
            int result = repository.Insert(Entity);
            switch (result)
            {
                case 0:
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        message = $"Berhasil menambah data"
                    });
                case 1:
                    return Ok(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Gagal menambahkan data, key sudah terdaftar"
                    });
            }
            return Ok();
        }


        [HttpGet("{Key}")]
        public ActionResult<Entity> Get(Key Key)
        {
            if (repository.Get(Key) == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tidak ditemukan" });
            }
            else
            {
                var result = repository.Get(Key);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditemukan" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
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
