using HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commannds.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypesDetails;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationController : ControllerBase
{
    private IMediator _mediator;

    public LeaveAllocationController(IMediator mediator)
    {
        this._mediator = mediator;
    }    

    // GET: api/<LeaveAllocationController>
    [HttpGet]
    public async Task<List<LeaveAllocationDto>> Get()
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());
        return leaveAllocations;
    }

    // GET api/<LeaveAllocationController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id) 
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery { Id = id });
        return Ok(leaveAllocation);
    }

    // POST api/<LeaveAllocationController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveAllocationController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand() { Id = id });
        return NoContent();
    }
}
