using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.Metadata
{
    public interface IDateMetadata
    {
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
    }
}
