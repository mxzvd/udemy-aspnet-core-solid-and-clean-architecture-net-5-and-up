using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;

public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.StartDate)
        .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be beofre {ComparisonValue}.");

        RuleFor(p => p.EndDate)
        .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(p => p.LeaveTypeId)
        .GreaterThan(0)
        .MustAsync(async (id, token) => {
            var leaveTypeExists = await leaveTypeRepository.Exists(id);
            return !leaveTypeExists;
        }).WithMessage("{PropertyName} does not exist.");
    }
}