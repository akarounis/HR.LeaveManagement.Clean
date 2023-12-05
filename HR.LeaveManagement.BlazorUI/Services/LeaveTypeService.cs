using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;
    public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            await AddBearerToken();
            var createdLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypePOSTAsync(createdLeaveTypeCommand);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }                        
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.LeaveTypeDELETEAsync(id);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        await AddBearerToken();
        var leaveType = await _client.LeaveTypeGETAsync(id);
        var data = _mapper.Map<LeaveTypeVM>(leaveType);
        return data;
    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        await AddBearerToken();
        var leaveTypes = await _client.LeaveTypeAllAsync();
        var data = _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        return data;
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
   {
        try
        {
            await AddBearerToken();
            var updatedLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypePUTAsync(id.ToString(), updatedLeaveTypeCommand);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
