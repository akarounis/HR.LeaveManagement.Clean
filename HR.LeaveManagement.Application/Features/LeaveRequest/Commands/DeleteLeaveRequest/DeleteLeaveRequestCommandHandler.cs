using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;    
    private readonly IAppLogger<DeleteLeaveRequestCommandHandler> _logger;
    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IAppLogger<DeleteLeaveRequestCommandHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;    
        _logger = logger;
    }

    public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // Convert to domain object
        var leaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

        // Delete from DB
        await _leaveRequestRepository.DeleteAsync(leaveRequestToDelete);
    }
}
