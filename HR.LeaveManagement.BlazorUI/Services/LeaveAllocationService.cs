using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
    }

    public async Task<Response<Guid>> CreateLeaveAllocation(int leaveTypeId)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveAllocationCommand createLeaveAllocationCommand = new CreateLeaveAllocationCommand()
            {
                LeaveTypeId = leaveTypeId
            };

            await _client.LeaveAllocationPOSTAsync(createLeaveAllocationCommand);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
