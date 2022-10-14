namespace RestaurantManagement.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public long RowVersion { get; set; }
    }

    public interface IBaseEntity
    {
        public int Id { get; set; }
        public long RowVersion { get; set; }
    }
}
