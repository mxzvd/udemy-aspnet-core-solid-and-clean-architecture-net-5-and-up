using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
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

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDtoValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveRequest = mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await leaveRequestRepository.Add(leaveRequest);
        return leaveRequest.Id;
    }
}