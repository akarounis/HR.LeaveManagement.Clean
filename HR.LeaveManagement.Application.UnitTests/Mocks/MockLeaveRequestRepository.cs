using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveRequestRepository
{
    public static Mock<ILeaveRequestRepository> GetMockLeaveRequestRepository()
    {
        var leaveRequests = new List<LeaveRequest>
        {
            new LeaveRequest
            {
                Id = 1,
                StartDate = DateTime.Parse("12/12/23"),
                EndDate = DateTime.Parse("26/12/23"),
                LeaveType = new LeaveType
                {
                    Id = 1,
                    DefaultDays=10,
                    Name ="Test Vacation"
                },
                LeaveTypeId = 1,
                DateRequested = DateTime.Parse("12/11/23"),
                Cancelled = false
            },
            new LeaveRequest
            {
                Id = 2,
                StartDate = DateTime.Parse("2/12/23"),
                EndDate = DateTime.Parse("4/12/23"),
                LeaveType = new LeaveType
                {
                    Id = 2,
                    DefaultDays=15,
                    Name ="Test Sick"
                },
                LeaveTypeId = 2,
                DateRequested = DateTime.Parse("29/11/23"),
                Cancelled = false
            }
        };

        var mockRepo = new Mock<ILeaveRequestRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveRequests);

        mockRepo.Setup(r => r.GetLeaveRequestsWithDetails()).ReturnsAsync(leaveRequests);

        mockRepo.Setup(r => r.GetLeaveRequestWithDetails(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            return leaveRequests.FirstOrDefault(q => q.Id == id);
        });

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveRequest>()))
            .Returns((LeaveRequest leaveRequest) =>
            {
                leaveRequests.Add(leaveRequest);
                return Task.CompletedTask;
            });

        return mockRepo;
    }

    
}
