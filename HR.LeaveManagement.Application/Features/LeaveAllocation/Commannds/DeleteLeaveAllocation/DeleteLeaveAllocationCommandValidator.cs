using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandValidator : AbstractValidator<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            RuleFor(p => p.Id)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();

            this._leaveAllocationRepository = leaveAllocationRepository;
        }
    }
}
