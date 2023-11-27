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

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveTypeHandler> _logger;

        public CreateLeaveTypeHandler(IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository, 
            IAppLogger<CreateLeaveTypeHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid LeaveType", validationResult);

            // Convert to Domain Entity Object (dto)
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            // Add to DB
            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);
            _logger.LogInformation("Leave type was succesfully added to db");

            // Return record id
            return leaveTypeToCreate.Id;
        }
    }
}
