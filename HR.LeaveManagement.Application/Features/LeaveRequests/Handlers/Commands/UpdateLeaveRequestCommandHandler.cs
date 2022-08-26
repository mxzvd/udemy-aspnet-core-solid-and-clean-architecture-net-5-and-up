using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public UpdateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        this.mapper = mapper;
        this.leaveRequestRepository = leaveRequestRepository;
        this.leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestDtoValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveRequest = await leaveRequestRepository.Get(request.Id);
        if (request.UpdateLeaveRequestDto != null)
        {
            mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
            await leaveRequestRepository.Update(leaveRequest);
        }
        else if (request.ChangeLeaveRequestApprovalDto != null)
        {
            await leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
        }
        return Unit.Value;
    }
}