using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    ILeaveRequestRepository _leaveRequestRepository;
    ILeaveTypeRepository _leaveTypeRepository;
    IMapper _mapper;
    IEmailSender _emailSender;
    IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IAppLogger<UpdateLeaveRequestCommandHandler> logger, IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {

        // Validate incoming data
        var validator = new UpdateLeaveRequestCommandValidator(_leaveRequestRepository, _leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        // If validation fails, log error messages and throw exception
        if (validationResult.Errors.Any())
        {
            string errorMessages = "";
            foreach (var error in validationResult.Errors)
            {
                errorMessages = error.ErrorMessage + "\n";
            }

            _logger.LongWarning($"Validation error occured while sending request to update a LeaveRequest: {errorMessages}");
            throw new BadRequestException("Invalid LeaveRequest", validationResult);
        }

        // Get leave type for request
        var leaveType = _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get employees 

        // Get Period

        // Convert to Domain Entity Objects (dto)
        var leaveRequestToUpdate = _mapper.Map<Domain.LeaveRequest>(request);

        // Update DB
        await _leaveRequestRepository.UpdateAsync(leaveRequestToUpdate);

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D}" +
                            $"has been updated succesfully.",
                Subject = "Leave Request Updated"
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
