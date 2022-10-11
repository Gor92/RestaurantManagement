using RestaurantManagement.Core.Models;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IJWTTokenService
    {
        string GenerateJWTToken(UserModel user);
        UserModel DecodeToken(string jWTToken);
    }
}
