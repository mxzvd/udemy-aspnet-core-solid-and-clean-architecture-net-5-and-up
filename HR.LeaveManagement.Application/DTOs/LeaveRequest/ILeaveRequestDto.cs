using HR.LeaveManagement.Application.DTOs.LeaveType;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest;

public interface ILeaveRequestDto
{
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
    LeaveTypeDto LeaveType { get; set; }
    int LeaveTypeId { get; set; }
    DateTime DateRequested { get; set; }
    string RequestComments { get; set; }
    DateTime? DateActioned { get; set; }
    bool? Approved { get; set; }
    bool Cancelled { get; set; }
}