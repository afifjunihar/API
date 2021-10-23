using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        IEnumerable<Entity> Get();
        Entity Get(Key Key);
        int Insert(Entity Entity);
        int Update(Entity Entity);
        int Delete(Key Key);

    }
}