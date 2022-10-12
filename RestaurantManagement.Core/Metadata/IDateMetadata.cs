namespace RestaurantManagement.Core.Metadata
{
    public interface IDateMetadata
    {
        DateTimeOffset CreateDate { get; set; }
        DateTimeOffset UpdateDate { get; set; }
    }
}
