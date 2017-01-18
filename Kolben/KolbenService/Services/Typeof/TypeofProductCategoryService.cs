using System;
using System.Linq.Expressions;
using KolbenService.Database.Entities;
using KolbenService.Database.Entities.Typeof;
using System.Threading.Tasks;

namespace KolbenService.Services
{
    public class TypeofProductCategoryService : ServiceBase<TypeofProductCategory>
    {
        public TypeofProductCategoryService(KolbenContext context) : base(context)
        {

        }

        protected override Task<TypeofProductCategory> Includes(TypeofProductCategory entity, params Expression<Func<TypeofProductCategory, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
