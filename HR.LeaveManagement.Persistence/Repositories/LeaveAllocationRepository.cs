using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext dbContext;

    public LeaveAllocationRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await dbContext.LeaveAllocations
        .Include(q => q.LeaveType)
        .FirstOrDefaultAsync(q => q.Id == id);
        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        var leaveAllocations = await dbContext.LeaveAllocations
        .Include(q => q.LeaveType)
        .ToListAsync();
        return leaveAllocations;
    }
}