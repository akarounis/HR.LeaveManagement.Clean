using MediatR;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.CreateLeaveAllocation;

public record CreateLeaveAllocationCommand() : IRequest<Unit>
{
    public int LeaveTypeId { get; set; } 
}

