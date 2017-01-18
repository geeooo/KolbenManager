using KolbenService.Database.Entities;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.IO;
using Windows.Storage;
using KolbenService.Database.Entities.Typeof;
using KolbenService.Entities;
using KolbenService.Enums;

namespace KolbenService
{
    public class KolbenContext : SQLiteAsyncConnection, IDisposable
    {
        private static string _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "KolbenDatabase.sqlite");
        public KolbenContext() : base(new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(_databasePath, storeDateTimeAsTicks: false)))) { }

        public void Dispose() { }
    }

    internal class LocalDatabase
    {
        public static async void CreateDatabase()
        {
            using (var context = new KolbenContext())
            {
                try
                {
                    await context.CreateTableAsync<AccountingAccount>();
                    await context.CreateTableAsync<AccountingAccountEntry>();
                    await context.CreateTableAsync<AccountingAccountEntryDetail>();

                    await context.CreateTableAsync<Bill>();
                    await context.CreateTableAsync<BillProduct>();
                    await context.CreateTableAsync<BillRecipe>();

                    await context.CreateTableAsync<Booking>();

                    await context.CreateTableAsync<Product>();

                    await context.CreateTableAsync<Recipe>();
                    await context.CreateTableAsync<RecipeProduct>();

                    await context.CreateTableAsync<Stock>();

                    await context.CreateTableAsync<TypeofTVA>();
                    await context.CreateTableAsync<TypeofProductCategory>();
                    await context.CreateTableAsync<TypeofAccountingAccount>();

                    await context.CreateTableAsync<Table>();

                    await context.CreateTableAsync<Purchase>();
                    await context.CreateTableAsync<PurchaseDetail>();

                    Init();
                }
                catch (SQLiteException sqlException)
                {

                    throw;
                }
                catch (Exception exception)
                {

                    throw;
                }
            }
        }     

        private static async void Init()
        {
            #region TypeofAccountingAccount
            //TVA
            var tmpTVAtypeofAccountingAccount = await KolbenServiceUnit.TypeofAccountingAccountService.GetSingle(toaa => toaa.TypeofAccountingAccountCategory == Enums.TypeofAccountingAccountCategory.TVA);
            if(tmpTVAtypeofAccountingAccount == null)
            {
                var typeofAccountingAccount = new TypeofAccountingAccount()
                {
                    Name = "TVA",
                    Prefix = "501",
                    TypeofAccountingAccountCategory = TypeofAccountingAccountCategory.TVA,
                    SuppressionDisabled = true,
                    EditionDisabled = true
                };
                await KolbenServiceUnit.TypeofAccountingAccountService.Add(typeofAccountingAccount);
            }

            //Provider
            var tmpProvidertypeofAccountingAccount = await KolbenServiceUnit.TypeofAccountingAccountService.GetSingle(toaa => toaa.TypeofAccountingAccountCategory == Enums.TypeofAccountingAccountCategory.TVA);
            if (tmpProvidertypeofAccountingAccount == null)
            {
                var typeofAccountingAccount = new TypeofAccountingAccount()
                {
                    Name = "Fournisseur",
                    Prefix = "40",
                    TypeofAccountingAccountCategory = TypeofAccountingAccountCategory.Provider,
                    SuppressionDisabled = true,
                    EditionDisabled = true
                };
                await KolbenServiceUnit.TypeofAccountingAccountService.Add(typeofAccountingAccount);
            }
            #endregion
        }
    }
}
