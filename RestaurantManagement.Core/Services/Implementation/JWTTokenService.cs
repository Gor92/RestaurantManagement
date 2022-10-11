using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Models.OptionsModels;

namespace RestaurantManagement.Core.Services.Implementation
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly TokenModel _tokenModel;
        private readonly HttpContext _httpContext;
        public JWTTokenService(IOptions<TokenModel> options)
        {
            _tokenModel = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
        }

        public UserModel DecodeToken(string jWTToken)
        {
            throw new NotImplementedException();
        }

        public string GenerateJWTToken(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
