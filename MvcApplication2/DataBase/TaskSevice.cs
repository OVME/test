using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MicroOrm.Pocos.SqlGenerator;
using MvcApplication2.Models;

namespace MvcApplication2.DataBase
{
    public class TaskSevice : IService<Task>
    {
        private readonly IDataRepository<Task> _repo;

        public TaskSevice()
        {
            ISqlGenerator<Task> isg = new SqlGenerator<Task>();
            IDbConnection idbc =
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _repo = new Repository(idbc, isg);
        }

        public IEnumerable<Task> GetAll()
        {
            return _repo.GetAll();
        }

        public IEnumerable<Task> GetWhere(object filters)
        {
            return _repo.GetWhere(filters);
        }

        public Task GetFirst(object filters)
        {
            return _repo.GetFirst(filters);
        }

        public void Insert(Task instance)
        {
            var res = _repo.Insert(instance);
            if(!res) throw new Exception("Element wasn't insert");
        }

        public void Delete(object key)
        {
            var res = _repo.Delete(key);
            //if (!res) throw new Exception("Element wasn't delete");
        }

        public void Update(Task instance)
        {
            var res = _repo.Update(instance);
            if (!res) throw new Exception("Element wasn't update");
        }

        public IEnumerable<Task> Get(int offset, int count)
        {
            return _repo.GetAll().OrderBy(p => p.Id).Skip(offset).Take(count);
        }

        public IEnumerable<Task> GetRoot()
        {
            return _repo.GetAll().Where(p => p.Task_Id == null);
        }

        public IEnumerable<Task> GetChildren(int id)
        {
            return _repo.GetWhere(new {Task_Id = id});
        }

        public void DeleteChildren(int id)
        {
            foreach (Task child in GetChildren(id))
            {
                DeleteChildren(child.Id);
            }
            _repo.Delete(new { Id = id });
        }
    }
}
