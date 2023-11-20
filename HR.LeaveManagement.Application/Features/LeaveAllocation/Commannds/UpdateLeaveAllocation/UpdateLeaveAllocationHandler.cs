using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.UpdateLeaveAllocation;

public class UpdateLeaveAllocationHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveAllocationHandler> _logger;

    public UpdateLeaveAllocationHandler(IMapper mapper, 
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveAllocationHandler> logger)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._logger = logger;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LongWarning("Validation error in update request for {0} - {1}",
                nameof(LeaveAllocation),
                request.Id);

            throw new BadRequestException("Invalid LeaveAllocation", validationResult);
        }

        // Get leave type
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Convert to Domain Entity Object (dto)
        var leaveAllocationToUpdate = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        _mapper.Map(request, leaveAllocationToUpdate);

        // Add to DB
        await _leaveAllocationRepository.UpdateAsync(leaveAllocationToUpdate);
        return Unit.Value;
    }
}
