using RestaurantManagement.Core.Models;

namespace RestaurantManagement.Core.Services.Contracts.BLs
{
    public interface IAuthBL
    {
        string GetAuthToken(UserModel userModel);
    }
}
