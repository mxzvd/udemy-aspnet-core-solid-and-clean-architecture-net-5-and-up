using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    private readonly LeaveManagementDbContext dbContext;

    public LeaveTypeRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
}