using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class UserRolePermission : BaseEntity, IRestaurant
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int RestaurantId { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public Resource Resource { get; set; }
    }
}
