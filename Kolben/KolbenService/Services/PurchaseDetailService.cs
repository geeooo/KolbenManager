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
    public class PurchaseDetailService : ServiceBase<PurchaseDetail>
    {
        public PurchaseDetailService(KolbenContext context) : base(context)
        {
        }

        public override async Task<int> Add(PurchaseDetail entity)
        {
            var idPurchaseDetail = await base.Add(entity);

            var typeofTVA = await KolbenServiceUnit.TypeofTVAService.GetSingle(entity.IdTypeofTVA);

            // Compute price
            var unitPrice = 0m;
            var kgPrice = 0m;
            var tvaValue = (100 - typeofTVA.Value) / 100;

            if (entity.UnitQuantity > 0)
            {
                unitPrice = Math.Round((entity.Price * tvaValue) / entity.UnitQuantity, 3);
            }

            if (entity.KGQuantity > 0)
            {
                kgPrice = Math.Round((entity.Price * tvaValue) / entity.KGQuantity, 3);
            }

            #region Stock
            // Check if the stock already exist
            var stock = await KolbenServiceUnit.StockService.GetSingle(s => s.IdProduct == entity.IdProduct && ((s.UnitPrice > 0 && s.UnitPrice == unitPrice) || (s.KgPrice > 0 && s.KgPrice == kgPrice)));
            if (stock != null)
            {
                stock.UnitQuantity += entity.UnitQuantity;
                stock.KgQuantity += entity.KGQuantity;

                await KolbenServiceUnit.StockService.Update(stock);
            }
            else
            {
                stock = new Stock
                {
                    IdProduct = entity.IdProduct,
                    KgPrice = kgPrice,
                    UnitPrice = unitPrice,
                    KgQuantity = entity.KGQuantity,
                    UnitQuantity = entity.UnitQuantity
                };

                await KolbenServiceUnit.StockService.Add(stock);
            }
            #endregion

            #region AccountingAccountEntry
            var purchase = await KolbenServiceUnit.PurchaseService.GetSingle(entity.IdPurchase, p => p.ProviderAccountingAccount);
            if (purchase != null)
            {
                #region Provider AccountingAccount
                if (purchase.IdProviderAccountingAccount > 0)
                {
                    //Double check if the accounting account entry exist
                    var accountingAccountEntry = purchase.ProviderAccountingAccount.AccountingAccountEntries.FirstOrDefault(aae => aae.IdPurchase == purchase.Id);
                    if (accountingAccountEntry == null)
                    {
                        //If accountingAccount entry does not exist -> Creation
                        accountingAccountEntry = new AccountingAccountEntry()
                        {
                            IdPurchase = purchase.Id,
                            AccountingAccountEntryOperation = Enums.AccountingAccountEntryOperation.Debit,
                            Label =  purchase.Label,
                            IdAccountingAccount = purchase.ProviderAccountingAccount.Id,
                            Date = purchase.PurchaseDate,
                        };
                        await KolbenServiceUnit.AccountingAccountEntryService.Add(accountingAccountEntry);
                    }

                    //Add AccountingAccountEntry detail
                    var accountingAccountEntryDetail = new AccountingAccountEntryDetail()
                    {
                        IdAccountingAccountEntry = accountingAccountEntry.Id,
                        IdTypeofTVA = entity.IdTypeofTVA,
                        Amount = entity.Price * tvaValue,
                    };
                    await KolbenServiceUnit.AccountingAccountEntryDetailService.Add(accountingAccountEntryDetail);
                }
                #endregion

                #region Debit AccoutingAccount
                if (purchase.DebitAccountingAccount != null)
                {
                    //Double check if the accounting account entry exist
                    var accountingAccountEntry = purchase.DebitAccountingAccount.AccountingAccountEntries.FirstOrDefault(aae => aae.IdPurchase == purchase.Id);
                    if (accountingAccountEntry == null)
                    {
                        //If accountingAccount entry does not exist -> Creation
                        accountingAccountEntry = new AccountingAccountEntry()
                        {
                            IdPurchase = purchase.Id,
                            AccountingAccountEntryOperation = Enums.AccountingAccountEntryOperation.Debit,
                            Label = purchase.Label,
                            IdAccountingAccount = purchase.DebitAccountingAccount.Id,
                            Date = purchase.PurchaseDate,
                        };
                        await KolbenServiceUnit.AccountingAccountEntryService.Add(accountingAccountEntry);
                    }

                    //Add AccountingAccountEntry detail
                    var accountingAccountEntryDetail = new AccountingAccountEntryDetail()
                    {
                        IdAccountingAccountEntry = accountingAccountEntry.Id,
                        IdTypeofTVA = entity.IdTypeofTVA,
                        Amount = entity.Price * tvaValue,
                    };
                    await KolbenServiceUnit.AccountingAccountEntryDetailService.Add(accountingAccountEntryDetail);
                }
                #endregion

                #region TVA AccountingAccount
                if (entity.IdTypeofTVA > 0)
                {
                    //Get TVA accounting account
                    var tvaAccountingAccount = await KolbenServiceUnit.AccountingAccountService.GetSingle(aa => aa.IdTypeofTVA > 0 && aa.IdTypeofTVA == entity.IdTypeofTVA, aa => aa.AccountingAccountEntries);

                    //Double check if the accounting account entry exist
                    var accountingAccountEntry = tvaAccountingAccount.AccountingAccountEntries.FirstOrDefault(aae => aae.IdPurchase == purchase.Id);
                    if (accountingAccountEntry == null)
                    {
                        //If accountingAccount entry does not exist -> Creation
                        accountingAccountEntry = new AccountingAccountEntry()
                        {
                            IdPurchase = purchase.Id,
                            AccountingAccountEntryOperation = Enums.AccountingAccountEntryOperation.Debit,
                            Label = purchase.Label,
                            IdAccountingAccount = tvaAccountingAccount.Id,
                            Date = purchase.PurchaseDate,
                        };
                        await KolbenServiceUnit.AccountingAccountEntryService.Add(accountingAccountEntry);
                    }

                    //Add AccountingAccountEntry detail
                    var accountingAccountEntryDetail = new AccountingAccountEntryDetail()
                    {
                        IdAccountingAccountEntry = accountingAccountEntry.Id,
                        IdTypeofTVA = entity.IdTypeofTVA,
                        Amount = entity.Price - (entity.Price * tvaValue),
                    };
                    await KolbenServiceUnit.AccountingAccountEntryDetailService.Add(accountingAccountEntryDetail);
                }
                #endregion
            }

            #endregion

            return idPurchaseDetail;
        }

        public override async Task Delete(int id)
        {
            var puchaseDetail = await KolbenServiceUnit.PurchaseDetailService.GetSingle(id, p => p.TypeOfTVA);

            // Compute price
            var unitPrice = 0m;
            var kgPrice = 0m;
            var tvaValue = (100 - puchaseDetail.TypeOfTVA.Value) / 100;

            if (puchaseDetail.UnitQuantity > 0)
            {
                unitPrice = Math.Round((puchaseDetail.Price * tvaValue) / puchaseDetail.UnitQuantity, 3);
            }

            if (puchaseDetail.KGQuantity > 0)
            {
                kgPrice = Math.Round((puchaseDetail.Price * tvaValue) / puchaseDetail.KGQuantity, 3);
            }

            #region Stock
            // Check if the stock already exist
            var stock = await KolbenServiceUnit.StockService.GetSingle(s => s.IdProduct == puchaseDetail.IdProduct && ((s.UnitPrice > 0 && s.UnitPrice == unitPrice) || (s.KgPrice > 0 && s.KgPrice == kgPrice)));
            if (stock != null)
            {
                stock.UnitQuantity -= puchaseDetail.UnitQuantity;
                stock.KgQuantity -= puchaseDetail.KGQuantity;

                await KolbenServiceUnit.StockService.Update(stock);
            }
            #endregion

            await base.Delete(id);
        }

        public override async Task Delete(PurchaseDetail entity)
        {           
            // Compute price
            var unitPrice = 0m;
            var kgPrice = 0m;
            var tvaValue = (100 - entity.TypeOfTVA.Value) / 100;

            if (entity.UnitQuantity > 0)
            {
                unitPrice = Math.Round((entity.Price * tvaValue) / entity.UnitQuantity, 3);
            }

            if (entity.KGQuantity > 0)
            {
                kgPrice = Math.Round((entity.Price * tvaValue) / entity.KGQuantity, 3);
            }

            #region Stock
            // Check if the stock already exist
            var stock = await KolbenServiceUnit.StockService.GetSingle(s => s.IdProduct == entity.IdProduct && ((s.UnitPrice > 0 && s.UnitPrice == unitPrice) || (s.KgPrice > 0 && s.KgPrice == kgPrice)));
            if (stock != null)
            {
                stock.UnitQuantity -= entity.UnitQuantity;
                stock.KgQuantity -= entity.KGQuantity;

                await KolbenServiceUnit.StockService.Update(stock);
            }
            #endregion

            await base.Delete(entity);
        }

        protected override async Task<PurchaseDetail> Includes(PurchaseDetail entity, params Expression<Func<PurchaseDetail, object>>[] includes)
        {
            foreach (var include in includes)
            {
                if (include.Body.Type == typeof(TypeofTVA))
                {
                    entity.TypeOfTVA = await KolbenServiceUnit.TypeofTVAService.GetSingle(entity.IdTypeofTVA);
                    continue;
                }

                if (include.Body.Type == typeof(Product))
                {
                    entity.Product = await KolbenServiceUnit.ProductService.GetSingle(p => p.Id == entity.IdProduct);
                    continue;
                }
            }

            return entity;
        }
    }
}
