using AutoMapper;
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

public class GetLeaveRequestDetailsQueryHandlerTests
{
    private readonly Mock<ILeaveRequestRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveRequestDetailsQueryHandler>> _mockAppLogger;
    public GetLeaveRequestDetailsQueryHandlerTests()
    {
        _mockRepo = MockLeaveRequestRepository.GetMockLeaveRequestRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveRequestProfile>();
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveRequestDetailsQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveRequestDetailsTest()
    {
        // Act
        var handler = new GetLeaveRequestDetailsQueryHandler(_mapper,
            _mockRepo.Object,
            _mockAppLogger.Object);

        var request = new GetLeaveRequestDetailsQuery();
        request.Id = 1;
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<LeaveRequestDetailsDto>();
        result.ShouldNotBeNull();
    }
}
