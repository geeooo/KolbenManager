using KolbenService.Database.Entities.Typeof;
using KolbenService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KolbenService.Services.Typeof
{
    public class TypeofAccountingAccountService : ServiceBase<TypeofAccountingAccount>
    {
        public TypeofAccountingAccountService(KolbenContext context) : base(context)
        {

        }

        protected override Task<TypeofAccountingAccount> Includes(TypeofAccountingAccount entity, params Expression<Func<TypeofAccountingAccount, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
