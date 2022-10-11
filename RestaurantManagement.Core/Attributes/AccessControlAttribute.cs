namespace RestaurantManagement.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AccessControlAttribute : Attribute
    {
        public ResourceType ResourceType { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
