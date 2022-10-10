using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class RestaurantSettings : BaseEntity, IRestaurant
    {
        public int RestaurantId { get; set; }
        public string DbConnectionString { get; set; }
    }
}
