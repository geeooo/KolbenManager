using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Enums
{
    public enum RecipeCategory
    {
        [Description("Entrée")]
        Starter = 0,
        [Description("Plat")]
        Dish = 1,
        [Description("Dessert")]
        Dessert = 2
    }
}
