using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Repositories.Abstraction;

namespace RestaurantManagement.Core.Repositories.Contracts
{
    public interface IRestaurantSettingsRepository : IGenericRepository<RestaurantSettings>
    {
    }
}
