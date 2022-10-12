using NSwag.Annotations;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.API.ViewModels;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.API.Controllers
{
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL _orderBL;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public OrderController(IOrderBL orderBL, IAuthService authService, IMapper mapper)
        {
            _orderBL = orderBL;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateOrder")]
        [OpenApiOperation("Create Order", "Endpoint to create a new Order")]
        [Produces("application/json")]
        public async Task<IActionResult> AddOrder([FromBody]OrderReadonlyViewModel orderReadonlyViewModel, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderReadonlyViewModel, Order>(orderReadonlyViewModel);
            var orderDetails = _mapper.Map<OrderDetailsReadonlyViewModel, OrderDetails>(orderReadonlyViewModel.OrderDetailsReadonlyViewModel);

            var createdOrder = await _orderBL.AddAsync(_authService.GetUserId(), order, orderDetails, cancellationToken);
            return Ok(createdOrder);
        }
    }
}
