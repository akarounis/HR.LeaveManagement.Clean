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

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }

    [Inject]
    public ILeaveAllocationService LeaveAllocationService { get; set; }

    public List<LeaveTypeVM> LeaveTypes { get; set; }

    public string Message { get; set; } = string.Empty;
    public bool Error { get; set; } = false;

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }

    protected void AllocateLeaveType(int id)
    {
        // Use Leave Alloction Service here
    }

    protected void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }

    protected void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo($"leavetypes/details/{id}");
    }

    protected async Task DeleteLeaveType(int id)
    {        
        var response = await LeaveTypeService.DeleteLeaveType(id);
        if (response.Success)
        {
            Error = false;
            Message = "Leave Type succesdfully deleted";
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
            Error = true;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypes();
    }
}