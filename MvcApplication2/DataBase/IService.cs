using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication2.DataBase
{
    public interface IService<TEntity> where TEntity : new()
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetWhere(object filters);
        TEntity GetFirst(object filters);
        void Insert(TEntity instance);
        void Delete(object key);
        void Update(TEntity instance);
        IEnumerable<TEntity> Get(int offset, int count);
        IEnumerable<TEntity> GetRoot();
        IEnumerable<TEntity> GetChildren(int id);
        void DeleteChildren(int id);
    }
}
