using KolbenService.Database.Entities.Typeof;
using KolbenService.Database.IEntities;
using KolbenService.Entities;
using KolbenService.Enums;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace KolbenService.Database.Entities
{
    public class AccountingAccountEntry : IEntityBase
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

        public string Label { get; set; }
        public int IdAccountingAccount { get; set; }
        public int IdPurchase { get; set; }
        public AccountingAccountEntryOperation AccountingAccountEntryOperation { get; set; }
        public DateTime Date { get; set; }

        [Ignore]
        public List<AccountingAccountEntryDetail> AccountingAccountEntryDetails { get; set; }

    }
}
