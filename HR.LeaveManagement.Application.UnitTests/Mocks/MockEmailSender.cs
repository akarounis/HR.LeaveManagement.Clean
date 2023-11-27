using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockEmailSender
{
    public static Mock<IEmailSender> GetMockEmailSender()
    {
        var mockEmailSender = new Mock<IEmailSender>();



        return mockEmailSender;
    }
}
