﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public record CancelLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
