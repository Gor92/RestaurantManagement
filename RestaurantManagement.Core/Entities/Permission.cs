namespace RestaurantManagement.Core.Entities
{
    public class Permission
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
