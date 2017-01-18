using KolbenService.Database.Entities;
using KolbenService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class StockService : ServiceBase<Stock>, IStockService
    {
        public StockService(KolbenContext context) : base(context)
        {

        }

        protected override Task<Stock> Includes(Stock entity, params Expression<Func<Stock, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
