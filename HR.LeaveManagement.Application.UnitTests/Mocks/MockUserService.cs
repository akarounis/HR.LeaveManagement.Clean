using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockUserService
    {
        public static Mock<IUserService> GetMockUserService()
        {
            var users = new List<Employee>
            {
                new Employee
                {
                    Id = "1",
                    Firstname = "Admin",
                    Lastname = "Test",
                    Email="admin@test.com"

                },
                new Employee
                {
                    Id = "2",
                    Firstname = "User",
                    Lastname = "Test",
                    Email="user@test.com"
                }
            };

            var mockService = new Mock<IUserService>();

            mockService.Setup(r=>r.UserId).Returns("2");   
            
            mockService.Setup(r=>r.GetEmployees()).ReturnsAsync(users);

            mockService.Setup(r => r.GetEmployee(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return users.FirstOrDefault(q => q.Id == id);
            });



            return mockService;
        }


    }
}
