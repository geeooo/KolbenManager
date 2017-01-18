using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolben.Utils
{
    public class EnumComboboxBinding
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public Type EnumType { get; set; }

        public override bool Equals(object obj)
        {
            var enumComboboxBinding = (EnumComboboxBinding)obj;
            return Description.Equals(enumComboboxBinding.Description) && Value.Equals(enumComboboxBinding.Value);
        }
    }
}
