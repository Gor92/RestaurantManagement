using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Core.Models
{
    public class TenantDbParams
    {
        public TenantContext TenantContext { get; set; }
        public Dictionary<string, string> CustomHeaders { get; set; }
        public long? RequestLength { get; set; }

    }
}
