using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    
    public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        this.mapper = mapper;
        this.leaveRequestRepository = leaveRequestRepository;
        this.leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveRequestDtoValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);

        if (validationResult.IsValid)
        {
            var leaveRequest = mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
            leaveRequest = await leaveRequestRepository.Add(leaveRequest);

            response.Success = true;
            response.Message = "Request Created Successfully";
            response.Id = leaveRequest.Id;
        }
        else
        {
            response.Success = false;
            response.Message = "Request Failed";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        return response;
    }
}