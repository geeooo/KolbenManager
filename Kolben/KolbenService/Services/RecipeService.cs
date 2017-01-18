using KolbenService.Database.Entities;
using KolbenService.Enums;
using KolbenService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services
{
    public class RecipeService : ServiceBase<Recipe>, IRecipeService
    {
        public RecipeService(KolbenContext context) : base(context)
        {

        }

        protected override async Task<Recipe> Includes(Recipe entity, params Expression<Func<Recipe, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(List<RecipeProduct>))
                {
                    entity.RecipeProducts = await KolbenServiceUnit.RecipeProductService.FindBy(rp => rp.IdRecipe == entity.Id, rp => rp.Product);
                    continue;
                }               
            }
            return entity;
        }
    }
}
