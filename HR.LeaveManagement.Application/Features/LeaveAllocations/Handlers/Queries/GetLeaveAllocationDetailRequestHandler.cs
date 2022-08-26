using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries;

public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, LeaveAllocationDto>
{
    private readonly IMapper mapper;
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    
    public GetLeaveAllocationDetailRequestHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        this.mapper = mapper;
        this.leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
        return mapper.Map<LeaveAllocationDto>(leaveAllocation);
    }
}