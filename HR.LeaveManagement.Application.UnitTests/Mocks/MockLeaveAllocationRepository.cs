using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveAllocationRepository
{
    public static Mock<ILeaveAllocationRepository> GetMockLeaveAllocationRepository()
    {
        var leaveAllocations = new List<LeaveAllocation>
        {
            new LeaveAllocation
            {
                Id = 1,
                NumberOfDays = 1,  
                LeaveType = new LeaveType
                {
                    Id = 1,
                    DefaultDays=10,
                    Name ="Test Vacation"
                },
                LeaveTypeId = 1,
                Period = 1
            },
            new LeaveAllocation
            {
                Id = 2,
                NumberOfDays = 5,
                LeaveType = new LeaveType
                {
                    Id = 2,
                    DefaultDays=15,
                    Name ="Test Sick"
                },
                LeaveTypeId = 2,
                Period = 5
            }
        };

        var mockRepo = new Mock<ILeaveAllocationRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveAllocations);

        mockRepo.Setup(r => r.GetLeaveAllocationsWithDetails()).ReturnsAsync(leaveAllocations);

        mockRepo.Setup(r => r.GetLeaveAllocationWithDetails(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            return leaveAllocations.FirstOrDefault(q => q.Id == id);
        });

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveAllocation>()))
            .Returns((LeaveAllocation leaveAllocation) =>
            {
                leaveAllocations.Add(leaveAllocation);
                return Task.CompletedTask;
            });

        return mockRepo;
    }
}
