using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class RecipeProductService : ServiceBase<RecipeProduct>
    {
        public RecipeProductService(KolbenContext context) : base(context) { }

        protected override async Task<RecipeProduct> Includes(RecipeProduct entity, params Expression<Func<RecipeProduct, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(Product))
                {
                    entity.Product = await KolbenServiceUnit.ProductService.GetSingle(entity.IdProduct, p => p.Stocks);
                    continue;
                }
            }
            return entity;
        }
    }
}
