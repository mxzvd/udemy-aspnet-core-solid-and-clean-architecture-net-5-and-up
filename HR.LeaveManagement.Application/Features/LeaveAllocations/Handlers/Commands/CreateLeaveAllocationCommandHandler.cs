using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ILeaveAllocationRepository leaveAllocationRepository;
    private readonly ILeaveTypeRepository leaveTypeRepository;
    
    public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        this.mapper = mapper;
        this.leaveAllocationRepository = leaveAllocationRepository;
        this.leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveAllocationDtoValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDto);

        if (validationResult.IsValid)
        {
            var leaveAllocation = mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
            leaveAllocation = await leaveAllocationRepository.Add(leaveAllocation);

            response.Id = leaveAllocation.Id;
            response.Success = true;
            response.Message = "Allocations Successful";
        }
        else
        {
            response.Success = false;
            response.Message = "Allocations Failed!";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        return response;
    }
}