using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities
{
    public class Purchase : IEntityBase
    {
        #region Implementation of IEntity
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SuppressionDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool EditionDisabled { get; set; }
        public bool SuppressionDisabled { get; set; }
        #endregion

        public Purchase()
        {
            ProviderAccountingAccount = new AccountingAccount();
            DebitAccountingAccount = new AccountingAccount();
            PurchaseDetails = new List<PurchaseDetail>();
        }

        public string Label { get; set; }
        public bool Payed { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int IdProviderAccountingAccount { get; set; }
        [Ignore]
        public AccountingAccount ProviderAccountingAccount { get; set; }

        public int IdDebitAccountingAccount { get; set; }
        [Ignore]
        public AccountingAccount DebitAccountingAccount { get; set; }
        [Ignore]
        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
