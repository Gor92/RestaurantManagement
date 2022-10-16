using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.ViewModels;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.API.Controllers
{
    [Route("order/orderDetails")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailsBL _orderDetailsBl;

        public OrderDetailsController(IMapper mapper, IOrderDetailsBL orderDetailsBl)
        {
            _mapper = mapper;
            _orderDetailsBl = orderDetailsBl;
        }

        [HttpPost("add")]
        public IActionResult AddProductToOrder(OrderDetailsReadonlyViewModel detailsReadonlyViewModel)
        {
            return Ok();
        }
    }
}
