﻿using KolbenService.Database.IEntities;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.Entities.Typeof
{
    public class TypeofTVA : IEntityBase
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
        public decimal Value { get; set; }
    }
}