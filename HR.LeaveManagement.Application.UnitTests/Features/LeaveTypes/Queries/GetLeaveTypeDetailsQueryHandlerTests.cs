using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
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

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeDetailsQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>> _mockAppLogger;

    public static IEnumerable<object[]> UserIds = new List<object[]>();

    public GetLeaveTypeDetailsQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>>();

        InitializeDataAsync();
                
    }

    private void InitializeDataAsync()
    {        
        // Get each ID from repository to query for LeaveTypes
        UserIds = _mockRepo.Object.GetAsync().Result
            .Select(
                x => new object[] { x.Id })
            .ToList();
    }

    [Fact]
    //[Theory]
    //[MemberData(nameof(UserIds))]       
    public async Task GetLeaveTypeDetailsTests()
    {
        // Act
        var handler = new GetLeaveTypeDetailsQueryHandler(_mapper,
            _mockRepo.Object,
            _mockAppLogger.Object);

        var result = await handler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<LeaveTypeDetailsDto>();
    }
}
