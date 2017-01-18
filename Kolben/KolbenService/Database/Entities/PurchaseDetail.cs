using KolbenService.Database.Entities.Typeof;
using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities
{
    public class PurchaseDetail : IEntityBase
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

        public PurchaseDetail()
        {
            Product = new Product();
            TypeOfTVA = new TypeofTVA();
        }
        public int IdPurchase { get; set; }

        public int IdProduct { get; set; }
        [Ignore]
        public Product Product { get; set; }

        public decimal UnitQuantity { get; set; }
        public decimal KGQuantity { get; set; }
        public decimal Price { get; set; }

        public int IdTypeofTVA { get; set; }
        [Ignore]
        public TypeofTVA TypeOfTVA { get; set; }
    }
}
