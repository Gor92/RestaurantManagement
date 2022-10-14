using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class AuthBL : IAuthBL
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthBL(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        public string GetAuthToken(UserModel userModel)
        {
            return _jwtTokenService.GenerateJwtToken(userModel);
        }
    }
}
