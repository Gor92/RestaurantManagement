using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.Metadata
{
    public interface IAuditMetadata
    {
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
    }
}
