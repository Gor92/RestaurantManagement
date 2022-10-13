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

        public AuthService(IJWTTokenService jWtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            var httpContextAccessor1 = httpContextAccessor;
            _token = new StringValues();
            if (httpContextAccessor1.HttpContext != null && httpContextAccessor1.HttpContext.Request.Headers.TryGetValue("Authorization", out _token))
                _userModel = jWtTokenService.DecodeToken(_token.ToString());
            else
            {
                throw new UnauthorizedAccessException();
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
