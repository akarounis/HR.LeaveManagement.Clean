using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public record UpdateLeaveTypeCommand : IRequest<Unit>
{
    public string Name { get; set; } = String.Empty;
    public int DefaultDays { get; set; }
}
