using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class OrderDetailsBL : IOrderDetailsBL
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailsBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<OrderDetails>> AddAsync(int userId, List<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {

            await _unitOfWork.OrderDetailsRepository.BulkInsert(orderDetails, cancellationToken);

            return orderDetails;
            
        }
    }
}
