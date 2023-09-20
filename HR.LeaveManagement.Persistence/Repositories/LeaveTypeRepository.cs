﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{    
    public LeaveTypeRepository(HrDatabaseContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return await _dbContext.LeaveTypes.AnyAsync(e => e.Name == name);                 
    }
}
