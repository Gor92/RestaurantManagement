using RestaurantManagement.Core;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs;
public class RestaurantBL : IRestaurantBL
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantBL(IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
    {
        _unitOfWork = unitOfWork;
        _restaurantRepository = restaurantRepository;
    }
    public async Task<Restaurant> AddAsync(int userId, Restaurant restaurant)
    {
        restaurant = _restaurantRepository.Insert(restaurant);
        await _unitOfWork.SaveChangesAsync();

        return restaurant;
    }

    public async Task<Restaurant> DeleteAsync(int userId, int restaurantId)
    {
        var restaurant = await GetByIdAsync(userId, restaurantId);
        _restaurantRepository.Remove(restaurant);

        return restaurant;
    }

    public async Task<Restaurant> UpdateAsync(int userId, Restaurant restaurant)
    {
        restaurant = _restaurantRepository.Update(restaurant);

        await _unitOfWork.SaveChangesAsync();
        return restaurant;
    }

    public async Task<Restaurant> GetByIdAsync(int userId, int restaurantId)
    {
        var rest = await _restaurantRepository.GetByIdAsync(restaurantId);

        if (rest == null)
            throw new RestaurantManagementException($"Restaurant with {restaurantId} not found", ErrorType.NotFound);

        return rest;
    }
}