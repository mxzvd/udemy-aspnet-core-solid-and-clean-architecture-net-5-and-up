using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeListRequestHandlerTests
{
    private readonly IMapper mapper;
    private readonly Mock<ILeaveTypeRepository> mockRepo;

    public GetLeaveTypeListRequestHandlerTests()
    {
        mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
        
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypeListRequestHandler(mapper, mockRepo.Object);
        var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}