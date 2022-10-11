namespace RestaurantManagement.Core.Models
{
    public class TenantDbParams
    {
        public TenantContext TenantContext { get; set; }
        public Dictionary<string, string> CustomHeaders { get; set; }
        public long? RequestLength { get; set; }

    }
}
