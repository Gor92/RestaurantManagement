using NSwag.Annotations;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.API.ViewModels;
using RestaurantManagement.BLL.Managers.Contracts;
using RestaurantManagement.Core.Services.Contracts;

namespace RestaurantManagement.API.Controllers
{
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public OrderController(IOrderManager orderManager, IAuthService authService, IMapper mapper)
        {
            _orderManager = orderManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("createOrder")]
        [OpenApiOperation("Create Order", "Endpoint to create a new Order")]
        [Produces("application/json")]
        public async Task<IActionResult> AddOrder([FromBody] OrderReadonlyViewModel orderReadonlyViewModel, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderReadonlyViewModel, Order>(orderReadonlyViewModel);
            var orderDetails = _mapper.Map<OrderDetailsReadonlyViewModel, OrderDetails>(orderReadonlyViewModel.OrderDetailsReadonlyViewModel);

            var createdOrder = await _orderManager.AddOrder(_authService.GetUserId(), order, orderDetails, cancellationToken);
            return Ok(createdOrder);
        }

        [HttpPut("updateOrder")]
        [OpenApiOperation("Update Order", "Endpoint to update an existing Order")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDetailsUpdateViewModel orderDetailsUpdateViewModel, CancellationToken cancellationToken)
        {
            var orderDetails = _mapper.Map<OrderDetailsReadonlyViewModel, OrderDetails>(orderDetailsUpdateViewModel.OrderDetailsReadonlyViewModels);
            var updatedOrder = await _orderManager.AddOrderDetailsToOrder(_authService.GetUserId(), orderDetailsUpdateViewModel.OrderId, orderDetails, cancellationToken);
            return Ok(updatedOrder);
        }
    }
}
