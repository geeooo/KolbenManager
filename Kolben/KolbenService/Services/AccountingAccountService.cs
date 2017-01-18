using KolbenService.Database.Entities;
using KolbenService.Database.Entities.Typeof;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Services
{
    public class AccountingAccountService : ServiceBase<AccountingAccount>
    {
        public AccountingAccountService(KolbenContext context) : base(context)
        {

        }

        protected async override Task<AccountingAccount> Includes(AccountingAccount entity, params Expression<Func<AccountingAccount, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(TypeofAccountingAccount))
                {
                    entity.TypeofAccountingAccount = await KolbenServiceUnit.TypeofAccountingAccountService.GetSingle(entity.IdTypeofAccountingAccount);
                    continue;
                }

                if (include.Body.Type == typeof(List<AccountingAccountEntry>))
                {
                    entity.AccountingAccountEntries = await KolbenServiceUnit.AccountingAccountEntryService.FindBy(aae => aae.IdAccountingAccount == entity.Id, aae => aae.AccountingAccountEntryDetails);
                    continue;
                }
            }
            return entity;
        }
    }
}
