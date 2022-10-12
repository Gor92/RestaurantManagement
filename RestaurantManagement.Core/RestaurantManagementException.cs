namespace RestaurantManagement.Core;

public class RestaurantManagementException : Exception
{
    public string ErrorMessage { get; set; }
    public ErrorType ErrorType { get; set; }

    public RestaurantManagementException(string errorMessage, ErrorType errorType)
    {
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }
}