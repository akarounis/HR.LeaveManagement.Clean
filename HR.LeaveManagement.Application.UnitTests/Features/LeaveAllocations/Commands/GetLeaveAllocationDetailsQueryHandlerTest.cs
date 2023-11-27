using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
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

public class GetLeaveAllocationDetailsQueryHandlerTest
{
    private readonly Mock<ILeaveAllocationRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveAllocationDetailsQueryHandler>> _mockAppLogger;

    public GetLeaveAllocationDetailsQueryHandlerTest()
    {
        _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveAllocationProfile>();
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveAllocationDetailsQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveAllocationDetailsTest()
    {
        // Act
        var handler = new GetLeaveAllocationDetailsQueryHandler(_mapper,
            _mockRepo.Object,
            _mockAppLogger.Object);

        var request = new GetLeaveAllocationDetailsQuery();
        request.Id = 1;
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<LeaveAllocationDetailsDto>();        
    }
}
