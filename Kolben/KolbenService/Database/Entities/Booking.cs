using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities
{
    public class Booking : IEntityBase
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

        public string Name { get; set; }
        public DateTime BookingDate { get; set; }
        public int PersonCount { get; set; }
    }
}
