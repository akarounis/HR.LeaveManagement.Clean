﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest;

public record ChangeLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public bool Approved { get; set; }
}