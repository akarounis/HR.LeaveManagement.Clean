using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.ChangeLeaveRequest;

public class ChangeLeaveRequestCommandValidator : AbstractValidator<ChangeLeaveRequestCommand>
{
    public ChangeLeaveRequestCommandValidator()
    {
        RuleFor(p => p.Approved).NotNull().WithMessage("Approval status cannot be null");
    }
}
