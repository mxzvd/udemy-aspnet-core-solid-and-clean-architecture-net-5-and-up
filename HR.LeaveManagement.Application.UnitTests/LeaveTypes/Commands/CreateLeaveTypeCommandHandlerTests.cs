using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.Responses;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly IMapper mapper;
    private readonly Mock<IUnitOfWork> mockUow;
    private readonly CreateLeaveTypeDto leaveTypeDto;
    private readonly CreateLeaveTypeCommandHandler handler;

    public CreateLeaveTypeCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        mapper = mapperConfig.CreateMapper();
        handler = new CreateLeaveTypeCommandHandler(mapper, mockUow!.Object.LeaveTypeRepository);

        leaveTypeDto = new CreateLeaveTypeDto
        {
            DefaultDays = 15,
            Name = "Test DTO"
        };
    }

    [Fact]
    public async Task Valid_LeaveType_Added()
    {
        var result = await handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = leaveTypeDto }, CancellationToken.None);

        var leaveTypes = await mockUow.Object.LeaveTypeRepository.GetAll();

        result.ShouldBeOfType<BaseCommandResponse>();
        leaveTypes.Count.ShouldBe(4);
    }

    [Fact]
    public async Task Invalid_LeaveType_Added()
    {
        leaveTypeDto.DefaultDays = -1;
        var result = await handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = leaveTypeDto }, CancellationToken.None);

        var leaveTypes = await mockUow.Object.LeaveTypeRepository.GetAll();

        result.ShouldBeOfType<BaseCommandResponse>();
        leaveTypes.Count.ShouldBe(3);
    }
}