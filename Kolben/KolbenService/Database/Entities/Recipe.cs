using KolbenService.Database.IEntities;
using KolbenService.Enums;
using System;
using SQLite.Net.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities
{
    public class Recipe : IEntityBase
    {
        public Recipe()
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

        public string Name { get; set; }
        public RecipeType RecipeType { get; set; }
        public RecipeCategory RecipeCategory { get; set; }

        [Ignore]
        public virtual List<RecipeProduct> RecipeProducts { get; set; }
    }
}
