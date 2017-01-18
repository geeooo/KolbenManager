using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolbenService.Database.IEntities
{
    public interface IEntityBase
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        DateTime? UpdateDate { get; set; }
        DateTime? SuppressionDate { get; set; }
        bool EditionDisabled { get; set; }
        bool SuppressionDisabled { get; set; }
    }
}
