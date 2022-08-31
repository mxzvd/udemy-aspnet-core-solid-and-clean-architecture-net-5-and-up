using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;

public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.LeaveTypeId)
        .GreaterThan(0)
        .MustAsync(async (id, token) => {
            var doesExist = await leaveTypeRepository.Exists(id);
            return doesExist;
        }).WithMessage("{PropertyName} does not exist.");
    }
}