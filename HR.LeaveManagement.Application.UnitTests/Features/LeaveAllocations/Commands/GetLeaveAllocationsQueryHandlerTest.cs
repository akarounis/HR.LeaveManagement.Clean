using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Commands;

public class GetLeaveAllocationsQueryHandlerTest
{
    private readonly Mock<ILeaveAllocationRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveAllocationsQueryHandler>> _mockAppLogger;

    public GetLeaveAllocationsQueryHandlerTest()
    {
        _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveAllocationProfile>();
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveAllocationsQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveAllocationsTest()
    {
        var records = await _mockRepo.Object.GetAsync();

        // Act
        var handler = new GetLeaveAllocationsQueryHandler(_mapper,
            _mockRepo.Object,            
            _mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveAllocationsQuery(), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<LeaveAllocationDto>>();
        result.Count.ShouldBe(records.Count());
    }
}
