using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public virtual int Delete(Key Key)
        {
            var delete = entities.Find(Key);
            entities.Remove(delete);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key Key)
        {
            var entity = entities.Find(Key);
            myContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual int Insert(Entity Entity)
        {
            entities.Add(Entity);
            return myContext.SaveChanges();

            //            if (myContext.Find(key) != null)
            //            {
            //                return 1;
            //            }
            //            else if (myContext.Employees.Where(e => e.Email == Entity.Email).FirstOrDefault() != null)
            //            {
            //                return 2;
            //            }
            //            else if (myContext.Employees.Where(e => e.Phone == Entity.Phone).FirstOrDefault() != null)
            //            {
            //                return 3;
            //            }
            //            else
            //            {
            //                myContext.Employees.Add(employee);
            //                myContext.SaveChanges();
            //                return 0;
            //            }

        }

        public int Update(Entity Entity)
        {
            myContext.Entry(Entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }

        //        public int Update(Entity Entity)
        //        {
        //            myContext.Entry(Entity).State = EntityState.Modified;
        //            var result = myContext.SaveChanges();
        //            return result;
        //        }


    }
}
