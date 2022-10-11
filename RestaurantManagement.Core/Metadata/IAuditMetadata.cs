namespace RestaurantManagement.Core.Metadata
{
    public interface IAuditMetadata
    {
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
    }
}
