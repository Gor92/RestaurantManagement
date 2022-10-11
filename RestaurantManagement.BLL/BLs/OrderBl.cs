using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class OrderBL : IOrderBL
    {
        public Task<Order> AddAsync(int userId, Order order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
