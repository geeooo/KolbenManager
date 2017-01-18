using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class BillProductService : ServiceBase<BillProduct>
    {
        public BillProductService(KolbenContext context) : base(context)
        {
        }

        protected override Task<BillProduct> Includes(BillProduct entity, params Expression<Func<BillProduct, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
