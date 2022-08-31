using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;

public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        Include(new ILeaveRequestDtoValidator(leaveTypeRepository));
    }
}