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
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> Get()
        {
            throw new NotImplementedException();
        }

        public Entity Get(Key Key)
        {
            throw new NotImplementedException();
        }

        public int Insert(Entity Entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Entity Entity)
        {
            throw new NotImplementedException();
        }
    }
}
