using KolbenService.Database.Entities.Typeof;
using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;

namespace KolbenService.Entities
{
    public class AccountingAccountEntryDetail : IEntityBase
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

        public int IdAccountingAccountEntry { get; set; }
        public decimal Amount { get; set; }
        public int IdTypeofTVA { get; set; }
        [Ignore]
        public TypeofTVA TypeofTVA { get; set; }
    }
}

