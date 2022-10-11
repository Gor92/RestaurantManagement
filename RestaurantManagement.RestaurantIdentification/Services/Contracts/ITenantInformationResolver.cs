using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.RestaurantIdentification.Services.Contracts
{
    public interface ITenantInformationResolver
    {
        Expression<Func<RestaurantSettings, bool>> GetTenantSelector(HttpContext httpContext);
    }
}
