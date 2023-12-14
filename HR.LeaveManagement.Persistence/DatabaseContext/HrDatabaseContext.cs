using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
    private readonly IUserService _userService;
    public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options,
        IUserService userService) : base(options)
    {
        _userService = userService;
    }

    public DbSet<LeaveType> LeaveTypes {  get; set; }   
    public DbSet <LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet <LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations from 'Configurations' folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly); 

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.UtcNow;
            entry.Entity.ModifiedBy = _userService.UserId;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                entry.Entity.CreatedBy = _userService.UserId;
            }

            if(entry.State == EntityState.Modified)
            {

            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}
