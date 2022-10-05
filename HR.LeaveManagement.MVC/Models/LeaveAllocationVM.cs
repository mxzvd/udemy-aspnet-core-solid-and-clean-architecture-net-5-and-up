using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models;

public class LeaveAllocationVM
{
    public string Id { get; set; }

    [Display(Name = "Number of Days")]
    public int NumberOfDays { get; set; }
    
    public DateTime DateCreated { get; set; }
    public int Period { get; set; }

    public LeaveTypeVM LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
}

public class CreateLeaveAllocationVM
{
    public int LeaveTypeId { get; set; }
}

public class UpdateLeaveAllocationVM
{
    public int Id { get; set; }

    [Display(Name = "Number of Days")]
    [Range(1, 50, ErrorMessage = "Enter Valid Number")]
    public int NumberOfDays { get; set; }
    public LeaveTypeVM MyProperty { get; set; }
}

public class ViewLeaveAllocationsVM
{
    public string EmployeeId { get; set; }
    public List<LeaveAllocationVM> LeaveAllocations { get; set; }
}
