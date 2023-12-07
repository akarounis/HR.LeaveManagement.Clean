using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class CreateLeaveRequest
    {
        [Inject]
        ILeaveTypeService LeaveTypeService { get; set; }

        [Inject]
        ILeaveRequestService LeaveRequestService { get; set; }
        
        [Inject] 
        NavigationManager NavigationManager { get; set; }

        LeaveRequestVM LeaveRequest {  get; set; } = new LeaveRequestVM();

        List<LeaveTypeVM> LeaveTypes { get; set; } = new List<LeaveTypeVM>();

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }

        private async void HandleValidSubmit()
        {
            // Perform form submission here
            await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests/");
        }

    }
}