using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;

namespace KolbenService.Database.Entities
{
    public class Bill : IEntityBase
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

        public int IdTable { get; set; }
        [Ignore]
        public Table Table { get; set; }
    }
}
