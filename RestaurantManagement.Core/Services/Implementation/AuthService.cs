using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Services.Contracts;

namespace RestaurantManagement.Core.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly StringValues _token;
        private readonly UserModel _userModel;
        private readonly IJWTTokenService _jWtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IJWTTokenService jWTTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _jWtTokenService = jWTTokenService;
            _httpContextAccessor = httpContextAccessor;
            _token = new StringValues();
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out _token))
                _userModel = _jWtTokenService.DecodeToken(_token.ToString());
            else
            {
                _userModel = new UserModel() { RestaurantId = 1, UserId = 1 };
            }
        }

        public string GetAuthToken()
        {
            return _token;
        }

        public int GetRestaurantId()
        {
            return _userModel.RestaurantId;
        }

        public string GetRoleName()
        {
            return "SuperAdmin";
        }

        public DateTime GetSystemDate()
        {
            return DateTime.UtcNow;
        }

        public int GetUserId()
        {
            return _userModel.UserId;
        }
    }
}
