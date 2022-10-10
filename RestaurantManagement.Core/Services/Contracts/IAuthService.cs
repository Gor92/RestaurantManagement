using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IAuthService
    {
        int GetUserId();
        int GetRestaurantId();
        string GetAuthToken();
        DateTime GetSystemDate();
        string GetRoleName();
    }
}
