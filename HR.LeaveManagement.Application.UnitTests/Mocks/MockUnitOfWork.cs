using HR.LeaveManagement.Application.Contracts.Persistence;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var mockLeaveTypeRepository = MockLeaveTypeRepository.GetLeaveTypeRepository();
        
        mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveTypeRepository.Object);

        return mockUow;
    }
}