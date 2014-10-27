using System.Data;
using System.Threading.Tasks;
using MicroOrm.Pocos.SqlGenerator;

namespace MvcApplication2.DataBase
{
    public class Repository : DataRepository<MvcApplication2.Models.Task>
    {
        public Repository(IDbConnection connection, ISqlGenerator<MvcApplication2.Models.Task> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }

        //public override bool Insert(Task instance)
        //{
        //    var newId = Connection.Query<decimal>(SqlGenerator.GetInsert(), instance).Single();
        //    var inserted = newId > 0;

        //    if (inserted)
        //    {
        //        instance.Id = (int)newId;
        //    }

        //    return inserted;
        //}
    }
}
