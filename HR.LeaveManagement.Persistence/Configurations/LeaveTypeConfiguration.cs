using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = DateTime.UtcNow
            },
            new LeaveType
            {
                Id= 2,
                Name = "Sick",
                DefaultDays = 2,
                DateCreated = DateTime.UtcNow
            },
            new LeaveType
            {
                Id = 3,
                Name = "Elections",
                DefaultDays = 1,
                DateCreated = DateTime.UtcNow
            },
            new LeaveType
            {
                Id = 4,
                Name = "Training",
                DefaultDays = 1,
                DateCreated = DateTime.UtcNow
            }
        );

        builder.Property(q=>q.Name)
            .IsRequired()
            .HasMaxLength( 100 );

    }
}
