using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class BillRecipeService : ServiceBase<BillRecipe>
    {
        public BillRecipeService(KolbenContext context) : base(context)
        {
        }

        protected override Task<BillRecipe> Includes(BillRecipe entity, params Expression<Func<BillRecipe, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
