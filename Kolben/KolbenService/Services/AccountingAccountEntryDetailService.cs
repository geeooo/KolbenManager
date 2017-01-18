using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using KolbenService.Database.Entities.Typeof;
using KolbenService.Entities;

namespace KolbenService.Services
{
    public class AccountingAccountEntryDetailService : ServiceBase<AccountingAccountEntryDetail>
    {
        public AccountingAccountEntryDetailService(KolbenContext context) : base(context)
        {
        }

        protected override async Task<AccountingAccountEntryDetail> Includes(AccountingAccountEntryDetail entity, params Expression<Func<AccountingAccountEntryDetail, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(TypeofTVA))
                {
                    entity.TypeofTVA = await KolbenServiceUnit.TypeofTVAService.GetSingle(entity.IdTypeofTVA);
                    continue;
                }
            }
            return entity;
        }
    }
}
