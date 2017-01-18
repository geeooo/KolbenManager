using System;
using System.Linq.Expressions;
using KolbenService.Database.Entities.Typeof;
using System.Threading.Tasks;
using KolbenService.Database.Entities;
using KolbenService.Enums;

namespace KolbenService.Services
{
    public class TypeofTVAService : ServiceBase<TypeofTVA>
    {
        public TypeofTVAService(KolbenContext context) : base(context)
        {

        }

        public override async Task<int> Add(TypeofTVA entity)
        {
            var idTypeofTVA = await base.Add(entity);

            #region AccountingAccount
            var typeofAccountingAccount = await KolbenServiceUnit.TypeofAccountingAccountService.GetSingle(toaa => toaa.TypeofAccountingAccountCategory == TypeofAccountingAccountCategory.TVA);
            var tvaAccountingAccount = new AccountingAccount()
            {
                AccountNumber = "00001",
                Name = entity.Name,
                IdTypeofAccountingAccount = typeofAccountingAccount.Id,
                EditionDisabled = true,
                SuppressionDisabled = true,
                IdTypeofTVA = idTypeofTVA            
            };
            await KolbenServiceUnit.AccountingAccountService.Add(tvaAccountingAccount);
            #endregion

            return idTypeofTVA;
        }

        protected override Task<TypeofTVA> Includes(TypeofTVA entity, params Expression<Func<TypeofTVA, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
