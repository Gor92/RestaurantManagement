using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Repositories.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.BLL.BLs
{
    public class OrderDetailsBL : IOrderDetailsBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsBL(IUnitOfWork unitOfWork,IOrderDetailsRepository orderDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _orderDetailsRepository = orderDetailsRepository;
        }
        public async Task<List<OrderDetails>> AddAsync(int userId, List<OrderDetails> orderDetails, CancellationToken cancellationToken)
        {
            _orderDetailsRepository.BulkInsert(orderDetails, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return orderDetails;
            
        }
    }
}
