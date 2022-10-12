using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class AuthBL : IAuthBL
    {
        private readonly IJWTTokenService _jwtTokenService;

        public AuthBL(IJWTTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        public string GetAuthToken(UserModel userModel)
        {
            return _jwtTokenService.GenerateJWTToken(userModel);
        }
    }
}
