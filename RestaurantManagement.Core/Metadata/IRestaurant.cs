using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.Core.Metadata
{
    public interface IRestaurant: IBaseEntity
    {
        public int? RestaurantId { get; set; }
    }
}
