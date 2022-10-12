using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.Core.Services.Contracts.BLs
{
    public interface IRestaurantBL
    {
        Task<Restaurant> AddAsync(int userId, Restaurant restaurant);

        Task<Restaurant> DeleteAsync(int userId, int restaurantId);

        Task<Restaurant> UpdateAsync(int userId, Restaurant restaurant);

        Task<Restaurant> GetByIdAsync(int userId, int restaurantId);

    }
}
