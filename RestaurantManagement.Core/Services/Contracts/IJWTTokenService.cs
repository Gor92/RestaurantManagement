using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Entities;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IJWTTokenService
    {
        string GenerateJWTToken(UserModel user);
        UserModel DecodeToken(string jWTToken);
    }
}
