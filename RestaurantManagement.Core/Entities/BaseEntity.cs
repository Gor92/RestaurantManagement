using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public interface IBaseEntity
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
