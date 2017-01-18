using KolbenService.Database.Entities.Typeof;
using KolbenService.Database.IEntities;
using KolbenService.Enums;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities
{
    public class Product : IEntityBase
    {
        public Product()
        {
            Stocks = new List<Stock>();
            TypeofProductCategory = new TypeofProductCategory();
        }

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

        public int IdTypeofProductCategory { get; set; }
        [Ignore]
        public TypeofProductCategory TypeofProductCategory { get; set; }

        [Ignore]
        public List<Stock> Stocks { get; set; }
    }
}
