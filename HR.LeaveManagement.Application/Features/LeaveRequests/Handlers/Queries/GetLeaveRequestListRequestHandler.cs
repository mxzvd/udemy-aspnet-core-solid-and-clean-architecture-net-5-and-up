using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
{
    private readonly IMapper mapper;
    private readonly ILeaveRequestRepository leaveRequestRepository;

    public GetLeaveRequestListRequestHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        this.mapper = mapper;
        this.leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();
        return mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
    }
}
