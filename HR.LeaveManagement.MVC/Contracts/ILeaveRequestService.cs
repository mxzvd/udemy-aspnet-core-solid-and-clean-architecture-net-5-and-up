using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts;

public interface ILeaveRequestService
{
    Task<AdminLeaveRequestViewVM> GetAdminLevelRequestList();
    Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
    Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);
    Task<LeaveRequestVM> GetLeaveRequest(int id);
    Task DeleteLeaveRequest(int id);
    Task ApproveLeaveRequest(int id, bool approved);
}
