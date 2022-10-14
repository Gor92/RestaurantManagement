using RestaurantManagement.Core.Models;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(UserModel user);
        UserModel DecodeToken(string jwtToken);
    }
}
