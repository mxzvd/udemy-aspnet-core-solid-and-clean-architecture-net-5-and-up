using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveType;

public class LeaveTypeDto : BaseDto, ILeaveTypeDto
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}