using RestaurantManagement.Core.Services.Contracts;

namespace RestaurantManagement.Core.Services.Implementation
{
    public class AuthService : IAuthService
    {
        public string GetAuthToken()
        {
            throw new NotImplementedException();
        }

        public int GetRestaurantId()
        {
            throw new NotImplementedException();
        }

        public string GetRoleName()
        {
            throw new NotImplementedException();
        }

        public DateTime GetSystemDate()
        {
            return DateTime.UtcNow;
        }

        public int GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}
