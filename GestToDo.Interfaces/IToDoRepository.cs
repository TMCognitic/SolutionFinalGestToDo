using System;
using System.Collections.Generic;
using System.Text;

namespace GestToDo.Interfaces
{
    public interface IToDoRepository<TEntity>
    {
        IEnumerable<TEntity> Get(int userId);
        TEntity Get(int userId, int id);
        TEntity Insert(TEntity entity);
        bool Update(int id, TEntity entity);
        bool Delete(int userId, int id);
    }
}
