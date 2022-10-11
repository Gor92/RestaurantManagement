using RestaurantManagement.Core.Metadata;

namespace RestaurantManagement.Core.Entities
{
    public class RolePermission : BaseEntity, IRestaurant
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int RestaurantId { get; set; }
        public virtual Role Role { get; set; }
        //public virtual Permission Permission { get; set; }
    }
}
