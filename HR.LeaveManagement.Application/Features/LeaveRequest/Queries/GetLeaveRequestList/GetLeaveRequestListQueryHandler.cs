using AutoMapper;
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

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestListQueryHandler> _logger;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IAppLogger<GetLeaveRequestListQueryHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        // Retrieve LeaveRequests
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

        // Return list of DTO objects
        _logger.LogInformation("Leave allocations were retrieved succesfully");
        return data;

    }
}
