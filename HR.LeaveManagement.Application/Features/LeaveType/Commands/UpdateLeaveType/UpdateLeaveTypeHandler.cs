using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly IAppLogger<UpdateLeaveTypeHandler> _logger;

    public UpdateLeaveTypeHandler(IMapper mapper, 
        ILeaveTypeRepository typeRepository,
        IAppLogger<UpdateLeaveTypeHandler> logger)
    {
        _mapper = mapper;
        _typeRepository = typeRepository;
        this._logger = logger;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate data
        var validator = new UpdateLeaveTypeCommandValidator(_typeRepository);
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            _logger.LongWarning("Validation error in update request for {0} - {1}", 
                nameof(LeaveType), 
                request.Id);
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }
            

        // Convert request to Domain Entity Object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        // Update record
        await _typeRepository.UpdateAsync(leaveTypeToUpdate);

        // Return Unit value
        return Unit.Value;
    }
}
