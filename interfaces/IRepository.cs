using System.Collections.Generic;

namespace cadastro_series.interfaces
{
    public interface IRepository<T>
    {
         List<T> List();
         T ReturnById(int id);
         void Insert(T item);
         void Update(int id, T item);
         void Delete(int id);
         int NextId();
    }
}