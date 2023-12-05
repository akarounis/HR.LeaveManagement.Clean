namespace HR.LeaveManagement.BlazorUI.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task<bool> RegisterAsync(string firstname, string lastname, string userName, string email, string password);
    Task Logout();
}
