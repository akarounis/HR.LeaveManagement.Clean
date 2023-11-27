using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypesDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveRequests.Queries;

public class GetLeaveRequestsQueryHandlerTests
{
    private readonly Mock<ILeaveRequestRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveRequestListQueryHandler>> _mockAppLogger;

    public GetLeaveRequestsQueryHandlerTests()
    {
        _mockRepo = MockLeaveRequestRepository.GetMockLeaveRequestRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveRequestProfile>();
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveRequestListQueryHandler>>();        
    }

    [Fact]
    public async Task GetLeaveRequestsTest()
    {
        var records = await _mockRepo.Object.GetAsync();        

        // Act
        var handler = new GetLeaveRequestListQueryHandler(_mockRepo.Object,
            _mapper,
            _mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveRequestListQuery(), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<LeaveRequestDto>>();
        result.Count.ShouldBe(records.Count());
    }
}
