using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Models.OptionsModels;

namespace RestaurantManagement.Core.Services.Implementation
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtModel _jwtModel;
        public JwtTokenService(IOptions<JwtModel> options)
        {
            _jwtModel = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
        }

        public UserModel DecodeToken(string jwtToken)
        {
            var stream = jwtToken;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            var userIdValue = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;
            var restaurantIdValue = tokenS?.Claims.First(claim => claim.Type == "RestaurantId").Value;

            if (!string.IsNullOrEmpty(userIdValue) && !string.IsNullOrEmpty(restaurantIdValue))
            {
                var successParsingUser = int.TryParse(userIdValue, out var userId);
                var successParsingRestaurant = int.TryParse(restaurantIdValue, out var restaurantId);

                if (!successParsingUser || !successParsingRestaurant)
                    throw new UnauthorizedAccessException();

                return new UserModel()
                {
                    RestaurantId = restaurantId,
                    UserId = userId
                };
            }

            throw new UnauthorizedAccessException();
        }

        public string GenerateJwtToken(UserModel user)
        {

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("RestaurantId", user.RestaurantId.ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _jwtModel.Issuer,
                Audience = _jwtModel.Audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtModel.Key)),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
