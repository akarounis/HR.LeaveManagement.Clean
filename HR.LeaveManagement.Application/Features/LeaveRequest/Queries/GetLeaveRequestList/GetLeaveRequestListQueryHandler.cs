using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestListQueryHandler> _logger;
    private readonly IUserService _userService;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, 
        IMapper mapper, 
        IAppLogger<GetLeaveRequestListQueryHandler> logger,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _logger = logger;
        _userService = userService;
    }

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = new List<Domain.LeaveRequest>();
        var requests = new List<LeaveRequestDto>();

        // Check if it is logged in employee
        if(request.IsLoggedInUser)
        {
            // Retrieve LeaveRequests
            var userId = _userService.UserId;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await _userService.GetEmployee(userId);

            // Convert data objects to DTO objects
            requests = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // Fill requests with employee information
            foreach (var req in requests)
            {
                req.Employee = employee;
            }
        }
        else
        {

            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(); // Retrieve LeaveRequests
            requests = _mapper.Map<List<LeaveRequestDto>>(leaveRequests); // Convert data objects to DTO objects

            // Fill requests with employee information
            foreach (var req in requests)
            {
                req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
            }
        }        

        // Return list of DTO objects
        _logger.LogInformation("Leave requests were retrieved succesfully");
        return requests;

    }
}
