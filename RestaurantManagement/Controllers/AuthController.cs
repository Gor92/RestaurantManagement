using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using RestaurantManagement.API.ViewModels;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;

namespace RestaurantManagement.API.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthBL _authBl;

        public AuthController(IMapper mapper, IAuthBL authBl)
        {
            _mapper = mapper;
            _authBl = authBl;
        }

        [AllowAnonymous]
        [HttpPost("CreateToken")]
        public IActionResult GetAuthToken([FromBody] UserReadonlyViewModel user)
        {
            var userModel = _mapper.Map<UserReadonlyViewModel, UserModel>(user);

            var token = _authBl.GetAuthToken(userModel);
            return Ok(token);
        }

        //[AllowAnonymous]
        //public IActionResult Login(User user)
        //{
        //    return Ok();
        //}
    }
}
