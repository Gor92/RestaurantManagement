using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Table : BaseEntity, IRestaurant
    {
        public int RestaurantRelatedTableId { get; set; }
        public int RestaurantId { get; set; }
        public bool IsReserved { get; set; }
    }
}
