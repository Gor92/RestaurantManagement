namespace RestaurantManagement.Core.Entities
{
    public class Resource: BaseEntity
    {
        public ResourceType Type { get; set; }
        public string Description { get; set; }
        public AccessLevel SupportedAccess { get; set; }
    }
}
