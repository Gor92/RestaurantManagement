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
