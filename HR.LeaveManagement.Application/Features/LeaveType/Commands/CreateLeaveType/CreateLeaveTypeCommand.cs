﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public record CreateLeaveTypeCommand : IRequest<int>
{
    public string Name { get; set; } = String.Empty;
    public int DefaultDays { get; set; }
}
