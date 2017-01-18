using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities
{
    public class RecipeProduct : IEntityBase
    {
        public RecipeProduct()
        {
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

        public int IdRecipe { get; set; }
        [Ignore]
        public Recipe Recipe { get; set; }

        public int IdProduct { get; set; }
        [Ignore]
        public Product Product { get; set; }

        public decimal KgQuantity { get; set; }
        public decimal UnitQuantity { get; set; }
    }
}
