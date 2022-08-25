using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Persistence.Contracts;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails();
}