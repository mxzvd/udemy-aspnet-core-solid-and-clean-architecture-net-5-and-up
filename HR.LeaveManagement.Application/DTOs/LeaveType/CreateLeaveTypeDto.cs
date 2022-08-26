namespace HR.LeaveManagement.Application.DTOs.LeaveType;

public class CreateLeaveTypeDto : ILeaveTypeDto
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}