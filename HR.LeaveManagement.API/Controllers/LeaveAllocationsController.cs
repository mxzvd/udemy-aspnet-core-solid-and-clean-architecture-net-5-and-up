using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        var leaveAllocation = await mediator.Send(new GetLeaveAllocationListRequest());
        return Ok(leaveAllocation);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
    {
        var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
        return Ok(leaveAllocation);
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto)
    {
        var command = new CreateLeaveAllocationCommand { CreateLeaveAllocationDto = createLeaveAllocationDto };
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto updateLeaveAllocationDto)
    {
        var command = new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = updateLeaveAllocationDto };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
        return NoContent();
    }
}