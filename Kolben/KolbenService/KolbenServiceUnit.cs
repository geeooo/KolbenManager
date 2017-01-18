using KolbenService.Services;
using KolbenService.Services.Typeof;

namespace KolbenService
{
    public static class KolbenServiceUnit
    {
        private static KolbenContext _context = new KolbenContext();

        private static AccountingAccountService _accountingAccountService;
        private static AccountingAccountEntryService _accountingAccountEntryService;
        private static AccountingAccountEntryDetailService _accountingAccountEntryDetailService;

        private static BillService _billService;
        private static BillProductService _billProductService;

        private static DatabaseService _databaseService;

        private static ProductService _productService;
        private static PurchaseService _purchaseService;
        private static PurchaseDetailService _purchaseDetailService;

        private static StockService _stockService;

        private static RecipeService _recipeService;
        private static RecipeProductService _recipeProductService;

        private static TableService _tableService;
        private static TypeofTVAService _typeofTVAService;
        private static TypeofProductCategoryService _typeofProductCategoryService;
        private static TypeofAccountingAccountService _typeofAccountingAccountService;

        public static AccountingAccountService AccountingAccountService
        {
            get
            {
                return _accountingAccountService != null ? _accountingAccountService : _accountingAccountService = new AccountingAccountService(_context);
            }
        }

        public static AccountingAccountEntryService AccountingAccountEntryService
        {
            get
            {
                return _accountingAccountEntryService != null ? _accountingAccountEntryService : _accountingAccountEntryService = new AccountingAccountEntryService(_context);
            }
        }

        public static AccountingAccountEntryDetailService AccountingAccountEntryDetailService
        {
            get
            {
                return _accountingAccountEntryDetailService != null ? _accountingAccountEntryDetailService : _accountingAccountEntryDetailService = new AccountingAccountEntryDetailService(_context);
            }
        }

        public static BillService BillService
        {
            get
            {
                return _billService != null ? _billService : _billService = new BillService(_context);
            }
        }
        public static BillProductService BillProductService
        {
            get
            {
                return _billProductService != null ? _billProductService : _billProductService = new BillProductService(_context);
            }
        }

        public static DatabaseService DatabaseService
        {
            get
            {
                return _databaseService != null ? _databaseService : _databaseService = new DatabaseService();
            }
        }

        public static StockService StockService
        {
            get
            {
                return _stockService != null ? _stockService : _stockService = new StockService(_context);
            }
        }

        public static ProductService ProductService
        {
            get
            {
                return _productService != null ? _productService : _productService = new ProductService(_context);
            }
        }

        public static PurchaseService PurchaseService
        {
            get
            {
                return _purchaseService != null ? _purchaseService : _purchaseService = new PurchaseService(_context);
            }
        }

        public static PurchaseDetailService PurchaseDetailService
        {
            get
            {
                return _purchaseDetailService != null ? _purchaseDetailService : _purchaseDetailService = new PurchaseDetailService(_context);
            }
        }

        public static RecipeService RecipesService
        {
            get
            {
                return _recipeService != null ? _recipeService : _recipeService = new RecipeService(_context);
            }
        }

        public static RecipeProductService RecipeProductService
        {
            get
            {
                return _recipeProductService != null ? _recipeProductService : _recipeProductService = new RecipeProductService(_context);
            }
        }

        public static TableService TableService
        {
            get
            {
                return _tableService != null ? _tableService : _tableService = new TableService(_context);
            }
        }

        public static TypeofTVAService TypeofTVAService
        {
            get
            {
                return _typeofTVAService != null ? _typeofTVAService : _typeofTVAService = new TypeofTVAService(_context);
            }
        }

        public static TypeofProductCategoryService TypeofProductCategoryService
        {
            get
            {
                return _typeofProductCategoryService != null ? _typeofProductCategoryService : _typeofProductCategoryService = new TypeofProductCategoryService(_context);
            }
        }

        public static TypeofAccountingAccountService TypeofAccountingAccountService
        {
            get
            {
                return _typeofAccountingAccountService != null ? _typeofAccountingAccountService : _typeofAccountingAccountService = new TypeofAccountingAccountService(_context);
            }
        }
    }
}
