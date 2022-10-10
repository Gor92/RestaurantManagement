using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.Repositories.Contracts
{
    public interface IOrderDetailsRepository:IGenericRepository<OrderDetails>
    {
    }
}
