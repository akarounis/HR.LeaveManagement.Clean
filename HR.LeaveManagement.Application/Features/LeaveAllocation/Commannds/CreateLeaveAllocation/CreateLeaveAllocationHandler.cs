using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.CreateLeaveAllocation;

public class CreateLeaveAllocationHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>

{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userSevice;

    public CreateLeaveAllocationHandler(IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IUserService userSevice)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userSevice = userSevice;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateLeaveAllocationCommandValidator(_leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);

        // Get Leave type for allocations
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get Employees
        var employees = await _userSevice.GetEmployees();

        // Get Period       
        var period = DateTime.Now.Year;

        // Assign allocations IF an allocation doesn't already exist for period and leave type
        var allocations = new List<Domain.LeaveAllocation>();
        foreach(var emp in employees)
        {
            var allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, request.LeaveTypeId, period);

            if (!allocationExists)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = emp.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    DateCreated = DateTime.Now,
                    Period = period
                });
            }
        }

        if(allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);
        }        
        
        return Unit.Value;
    }
}
