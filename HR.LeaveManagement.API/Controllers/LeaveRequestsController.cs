using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
    {
        var leaveRequests = await mediator.Send(new GetLeaveRequestListRequest());
        return Ok(leaveRequests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveRequest = await mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
        return Ok(leaveRequest);
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
    {
        var command = new CreateLeaveRequestCommand { CreateLeaveRequestDto = leaveRequest };
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
    {
        var command = new UpdateLeaveRequestCommand { Id = id, UpdateLeaveRequestDto = leaveRequest };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("changeapproval/{id}")]
    public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApprovalDto)
    {
        var command = new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = changeLeaveRequestApprovalDto };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteLeaveRequestCommand { Id = id });
        return NoContent();
    }
}