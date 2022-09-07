using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypeController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LeaveTypeController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        this.mediator = mediator;
        this.httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        var leaveTypes = await mediator.Send(new GetLeaveTypeListRequest());
        return Ok(leaveTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveType = await mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
        return Ok(leaveType);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateLeaveTypeDto createLeaveType)
    {
        var command = new CreateLeaveTypeCommand { CreateLeaveTypeDto = createLeaveType };
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveType)
    {
        var command = new UpdateLeaveTypeCommand { LeaveTypeDto = leaveType };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteLeaveTypeCommand { Id = id });
        return NoContent();
    }
}