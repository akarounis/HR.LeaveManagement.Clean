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
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Blazored.Toast.Services;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public class CreateLeaveTypeBase : ComponentBase
{
    [Inject]
    NavigationManager NavigationManager { get; set; }
    [Inject]
    ILeaveTypeService leaveTypeService { get; set; }
    protected LeaveTypeVM LeaveType { get; set; } = new LeaveTypeVM();
    IToastService toastService { get; set; }

    public string Message { get; set; } = string.Empty;
    public bool Error { get; set; } = false;

    protected async Task CreateLeaveTypeFromFormAsync()
    {
        var result = await leaveTypeService.CreateLeaveType(LeaveType);

        if (result.Success)
        {
            toastService.ShowSuccess("Leave Type creeated succesfully");
            Message = $"Leave Type {LeaveType.Name} has been succesfully added";
            Error = false;
        }
        else
        {
            Message = result.Message;
            Error = true;
        }
    }
}