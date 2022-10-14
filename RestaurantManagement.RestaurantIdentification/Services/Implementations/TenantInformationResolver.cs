using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.RestaurantIdentification.Services.Contracts;

namespace RestaurantManagement.RestaurantIdentification.Services.Implementations
{
    public class TenantInformationResolver : ITenantInformationResolver
    {
        private readonly IJwtTokenService _jwtTokenService;

        public TenantInformationResolver(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public Expression<Func<RestaurantSettings, bool>> GetTenantSelector(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Authorization", out var token);

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException();

            var userModel = _jwtTokenService.DecodeToken(token);

            if (userModel == null)
                throw new ArgumentNullException(nameof(userModel));

            return x => x.RestaurantId == userModel.RestaurantId;
        }
    }
}
