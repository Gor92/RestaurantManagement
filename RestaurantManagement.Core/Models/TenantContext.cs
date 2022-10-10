using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Core.Models
{
    public class TenantContext
    {
        public int RestaurantId { get; set; }
        public string ConnectionString { get; set; }
    }
}
