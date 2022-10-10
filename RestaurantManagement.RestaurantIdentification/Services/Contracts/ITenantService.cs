using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.RestaurantIdentification.Services.Contracts
{
    public interface ITenantService
    {
        Task<TenantContext?> GetTenantAsync(CancellationToken cancellationToken);

    }
}
