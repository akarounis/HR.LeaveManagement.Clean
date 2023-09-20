using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeHandler(ILeaveTypeRepository leaveTypeRepository) => 
        _leaveTypeRepository = leaveTypeRepository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Retrieve Domain Entity Object
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

        // Validate record
        var validator = new DeleteLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any()) throw new BadRequestException("Invalid LeaveType", validationResult);        

        // Delete from DB
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

        // Return Unit value
        return Unit.Value;
    }
}

