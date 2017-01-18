using KolbenService.Database.Entities;
using KolbenService.Enums;
using KolbenService.Services;
using KolbenService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using KolbenService.Database.Entities.Typeof;

namespace KolbenService.Services
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public ProductService(KolbenContext context) : base(context)
        {

        }

        protected override async Task<Product> Includes(Product entity, params Expression<Func<Product, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(List<Stock>))
                {
                    entity.Stocks = await KolbenServiceUnit.StockService.FindBy(s => s.IdProduct == entity.Id);
                    continue;
                }

                if (include.Body.Type == typeof(TypeofProductCategory))
                {
                    entity.TypeofProductCategory = await KolbenServiceUnit.TypeofProductCategoryService.GetSingle(entity.IdTypeofProductCategory);
                    continue;
                }
            }
            return entity;
        }
    }
}
