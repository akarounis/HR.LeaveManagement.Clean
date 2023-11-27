using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Comnmands;

public class CreateLeaveTypeCommandHandlerTest
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<CreateLeaveTypeHandler>> _mockAppLogger;

    public CreateLeaveTypeCommandHandlerTest()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<CreateLeaveTypeHandler>>();
    }

    [Fact]
    public async Task CreateLeaveTypeTests()
    {
        // Act       
        var handler = new CreateLeaveTypeHandler(_mapper,
            _mockRepo.Object,
            _mockAppLogger.Object);

        var command = new CreateLeaveTypeCommand
        {
            Name = "Party Leave",
            DefaultDays = 1
        };

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var createdEntry = await _mockRepo.Object.GetByIdAsync(result);
        createdEntry.ShouldNotBeNull();

    }
}
