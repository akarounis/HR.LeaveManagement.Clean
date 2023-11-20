using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.DeleteLeaveAllocation;

public class DeleteLeaveAllocationHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Retrieve Domain Entity Object
        var leaveAllocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        // Validate record
        var validator = new DeleteLeaveAllocationCommandValidator(_leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any()) throw new BadRequestException("Invalid LeaveAllocation", validationResult);

        // Delete from DB
        await _leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

        // Return Unit value
        return Unit.Value;
    }
}
