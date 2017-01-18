using KolbenService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using KolbenService.Enums;

namespace KolbenService.Services
{
    public class PurchaseService : ServiceBase<Purchase>
    {
        public PurchaseService(KolbenContext context) : base(context)
        {
        }

        protected override async Task<Purchase> Includes(Purchase entity, params Expression<Func<Purchase, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(AccountingAccount))
                {
                    entity.ProviderAccountingAccount = await KolbenServiceUnit.AccountingAccountService.GetSingle(entity.IdProviderAccountingAccount, aa => aa.TypeofAccountingAccount, aa => aa.AccountingAccountEntries);
                    entity.DebitAccountingAccount = await KolbenServiceUnit.AccountingAccountService.GetSingle(entity.IdDebitAccountingAccount, aa => aa.TypeofAccountingAccount, aa => aa.AccountingAccountEntries);
                    continue;
                }

                if (include.Body.Type == typeof(List<PurchaseDetail>))
                {
                    entity.PurchaseDetails = await KolbenServiceUnit.PurchaseDetailService.FindBy(p => p.IdPurchase == entity.Id, p => p.Product, p => p.TypeOfTVA);
                    continue;
                }

            }

            return entity;
        }
    }
}
