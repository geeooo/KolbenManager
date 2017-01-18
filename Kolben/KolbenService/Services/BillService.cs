using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class BillService : ServiceBase<Bill>
    {
        public BillService(KolbenContext context) : base(context)
        {
        }

        protected override Task<Bill> Includes(Bill entity, params Expression<Func<Bill, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
