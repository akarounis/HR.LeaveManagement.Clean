﻿using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest;

public class ChangeLeaveRequestCommandHandler : IRequestHandler<ChangeLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<ChangeLeaveRequestCommandHandler> _logger;
    public ChangeLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender, 
        IAppLogger<ChangeLeaveRequestCommandHandler> logger,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _logger = logger;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new ChangeLeaveRequestCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        // If validation fails, log error messages and throw exception
        if (validationResult.Errors.Any())
        {
            string errorMessages = "";
            foreach (var error in validationResult.Errors)
            {
                errorMessages = error.ErrorMessage + "\n";
            }

            _logger.LongWarning($"Validation error occured while sending request to change a LeaveRequest: {errorMessages}");
            throw new BadRequestException("Invalid LeaveRequest", validationResult);
        }

        var requestToChange = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (requestToChange != null) { throw new NotFoundException(nameof(request), request.Id); }

        requestToChange.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(requestToChange);

        // if request is approved, get and update the employee's allocations
        if (request.Approved)
        {
            int daysRequested = (int)(requestToChange.EndDate - requestToChange.StartDate).TotalDays;
            var allocation = await _leaveAllocationRepository.GetUserAllocations
                (requestToChange.RequestingEmployeeId, requestToChange.LeaveTypeId);
            allocation.NumberOfDays -= daysRequested;

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        try
        {
            // Send confirmation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {requestToChange.StartDate:D} to {requestToChange.EndDate:D}" +
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
