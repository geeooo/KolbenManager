using KolbenService.Database.Entities;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using KolbenService.Entities;

namespace KolbenService.Services
{
    public class AccountingAccountEntryService : ServiceBase<AccountingAccountEntry>
    {
        public AccountingAccountEntryService(KolbenContext context) : base(context)
        {
        }

        protected override async Task<AccountingAccountEntry> Includes(AccountingAccountEntry entity, params Expression<Func<AccountingAccountEntry, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(List<AccountingAccountEntryDetail>))
                {
                    entity.AccountingAccountEntryDetails = await KolbenServiceUnit.AccountingAccountEntryDetailService.FindBy(aaed => aaed.IdAccountingAccountEntry == entity.Id, aaed => aaed.TypeofTVA);
                    continue;
                }

            }
            return entity;
        }
    }
}
