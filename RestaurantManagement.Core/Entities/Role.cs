using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class Role : BaseEntity, IRestaurant
    {
        public string Name { get; set; }
        public int? RestaurantId { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
