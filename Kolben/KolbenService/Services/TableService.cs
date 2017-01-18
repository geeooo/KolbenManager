using KolbenService.Database.Entities;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class TableService : ServiceBase<Table>
    {
        public TableService(KolbenContext context) : base(context)
        {
        }

        protected override Task<Table> Includes(Table entity, params Expression<Func<Table, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
