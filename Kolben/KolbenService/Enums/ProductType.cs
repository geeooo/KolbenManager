using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Enums
{
    public enum ProductType
    {
        [Description("Boisson")]
        Drink = 0,
        [Description("Matière première")]
        RawMaterial = 1,
        [Description("Non consommable")]
        NotConsumable = 2
    }
}
