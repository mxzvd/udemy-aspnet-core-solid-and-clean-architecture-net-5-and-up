using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations.Entities;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Vacation"
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 12,
                Name = "Sick"
            }
        );
    }
}