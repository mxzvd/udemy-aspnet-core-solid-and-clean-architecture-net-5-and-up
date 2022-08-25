using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Persistence.Contracts;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    Task<List<LeaveRequest>> GetLeaveRequestWithDetails();
}