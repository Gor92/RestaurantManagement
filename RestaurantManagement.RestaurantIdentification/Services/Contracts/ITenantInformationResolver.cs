using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;

namespace RestaurantManagement.RestaurantIdentification.Services.Contracts
{
    public interface ITenantInformationResolver
    {
        Expression<Func<RestaurantSettings, bool>> GetTenantSelector(HttpContext httpContext);
    }
}
