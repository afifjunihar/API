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

        public int Delete(Key Key)
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

        public int Insert(Entity Entity)
        {
             entities.Add(Entity);
             return myContext.SaveChanges();           
        }

        public int Update(Entity Entity)
        {
            myContext.Entry(Entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
