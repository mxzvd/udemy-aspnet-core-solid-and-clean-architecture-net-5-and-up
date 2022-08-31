using Microsoft.EntityFrameworkCore;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly LeaveManagementDbContext dbContext;

    public LeaveRequestRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
    {
        leaveRequest.Approved = approvalStatus;
        dbContext.Entry(leaveRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await dbContext.LeaveRequests
        .Include(p => p.LeaveType)
        .FirstOrDefaultAsync(q => q.Id == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await dbContext.LeaveRequests.Where(q => q.RequestingEmployeeId == userId)
        .Include(q => q.LeaveType)
        .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequest = await dbContext.LeaveRequests
        .Include(p => p.LeaveType)
        .ToListAsync();
        return leaveRequest;
    }
}