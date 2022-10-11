namespace RestaurantManagement.Core.Entities
{
    public class Resource
    {
        public ResourceType Type { get; set; }
        public string Description { get; set; }
        public AccessLevel SupportedAccess { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
