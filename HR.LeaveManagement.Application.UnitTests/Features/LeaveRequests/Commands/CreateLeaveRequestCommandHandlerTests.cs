using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveRequests.Commands;

public class CreateLeaveRequestCommandHandlerTests
{
    private readonly Mock<ILeaveRequestRepository> _mockLeaveRequestRepo;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepo;

    private IMapper _mapper;
    private Mock<IAppLogger<CreateLeaveRequestCommandHandler>> _mockAppLogger;
    private readonly Mock<IEmailSender> _mockEmailSender;

    public CreateLeaveRequestCommandHandlerTests()
    {
        _mockLeaveRequestRepo = MockLeaveRequestRepository.GetMockLeaveRequestRepository();
        _mockLeaveTypeRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        _mockEmailSender = MockEmailSender.GetMockEmailSender();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveRequestProfile>();
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<CreateLeaveRequestCommandHandler>>();        
    }

    [Fact]
    public async Task CreateLeaveRequestTests()
    {
        // Act       
        var handler = new CreateLeaveRequestCommandHandler(_mapper,
            _mockLeaveRequestRepo.Object,
            _mockLeaveTypeRepo.Object,
            _mockEmailSender.Object,
            _mockAppLogger.Object);

        var command = new CreateLeaveRequestCommand
        {
            StartDate = DateTime.Parse("12/12/23"),
            EndDate = DateTime.Parse("26/12/23"),
            LeaveTypeId = 1
        };
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        //var createdEntry = await _mockLeaveRequestRepo.Object.GetAsync(result);
        //createdEntry.ShouldNotBeNull();
    }
}
