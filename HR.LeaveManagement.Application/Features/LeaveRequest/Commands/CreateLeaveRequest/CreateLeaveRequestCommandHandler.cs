using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    public CreateLeaveRequestCommandHandler(
        IMapper mapper, 
        IAppLogger<CreateLeaveRequestCommandHandler> logger, 
        ILeaveRequestRepository leaveRequestRepository, 
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender)
    {
        _mapper = mapper;
        _logger = logger;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
    }
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateLeaveRequestValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        // If validation fails, log error messages and throw exception
        if (validationResult.Errors.Any())
        {                
            string errorMessages = "";
            foreach (var error in validationResult.Errors)
            {
                errorMessages = error.ErrorMessage + "\n";
            }                

            _logger.LongWarning($"Validation error occured while sending request to create a new LeaveRequest: {errorMessages}");
            throw new BadRequestException("Invalid LeaveRequest", validationResult);
        }

        // Get leave type for request
        var leaveType = _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get employees

        // Get Period

        // Convert to Domain Entity Objects (dto)
        var leaveRequestToCreate = _mapper.Map<Domain.LeaveRequest>(request);

        // Add to DB
        await _leaveRequestRepository.CreateAsync(leaveRequestToCreate);

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D}" +
                            $"has been submitted succesfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LongWarning($"{ex.Message}");
        }

        return Unit.Value;        
    }
}
