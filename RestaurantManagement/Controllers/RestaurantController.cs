using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Metadata;
using RestaurantManagement.API.ViewModels;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.API.Controllers
{
    [Route("restaurants")]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantBL _restaurantBL;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantBL restaurantBl,IAuthService authService,IMapper mapper)
        {
            _restaurantBL = restaurantBl;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetByIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantBL.GetByIdAsync(_authService.GetUserId(),restaurantId);
            var viewModel = _mapper.Map<Restaurant, RestaurantReadonlyViewModel>(restaurant);

            return Ok(viewModel);
        }
    }
}
