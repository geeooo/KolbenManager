using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;

namespace KolbenService.Database.Entities
{
    public class BillProduct : IEntityBase
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

        public int IdBill { get; set; }
        [Ignore]
        public Bill Bill { get; set; }

        public int IdProduct { get; set; }
        [Ignore]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

