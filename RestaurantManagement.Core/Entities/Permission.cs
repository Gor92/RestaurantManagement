using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Core.Entities
{
    public class Permission : BaseEntity
    {
        public AccessLevel AccessLevel { get; set; }
        public Resource Resource { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        public override string ToString()
        {
            return $"{Resource?.Type.ToString()}|{AccessLevel.ToString()}";
        }
    }
}
