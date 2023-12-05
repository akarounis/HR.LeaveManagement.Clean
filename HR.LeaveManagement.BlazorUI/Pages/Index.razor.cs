using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using HR.LeaveManagement.BlazorUI;
using HR.LeaveManagement.BlazorUI.Shared;
using HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using HR.LeaveManagement.BlazorUI.Contracts;
using System.Runtime.CompilerServices;
using HR.LeaveManagement.BlazorUI.Providers;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Index
{
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    protected async void Logout()
    {
        await AuthenticationService.Logout();
    }

    protected async void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    protected async void GoToRegister()
    {
        NavigationManager.NavigateTo("login/");
    }
}