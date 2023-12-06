using HR.LeaveManagement.BlazorUI.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using HR.LeaveManagement.BlazorUI.Models;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Login
{
    public LoginVM Model { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    public string Message { get; set; }

    public Login()
    {

    }

    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }

    protected async Task HandleLogin()
    {
        if(await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Username or password unknown";
    }
}