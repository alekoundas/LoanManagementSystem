using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Services;
using NSubstitute;
using Xunit;

namespace Test
{
    public class LoanApplicationServiceTests
{
    private readonly ILoanApplicationRepository _repo;
    private readonly LoanApplicationService _service;

    public LoanApplicationServiceTests()
    {
        _repo = Substitute.For<ILoanApplicationRepository>();
        _service = new LoanApplicationService(_repo);
    }

    [Fact]
    public async Task GetAllLoanApplicationsAsync_ReturnsMappedDtos()
    {
        // Arrange
        var applications = new List<LoanApplication>
        {
            new LoanApplication
            {
                ApplicationID = 1,
                CustomerID = 10,
                LoanProductID = 100,
                RequestedAmount = 5000,
                ApplicationDate = new DateTime(2024, 1, 1),
                ApplicationStatus = "Pending",
                ApprovedAmount = null
            }
        };
        _repo.GetAllLoanApplicationsAsync().Returns(applications);

        // Act
        var result = await _service.GetAllLoanApplicationsAsync();

        // Assert
        var dto = Assert.Single(result);
        Assert.Equal(1, dto.ApplicationID);
        Assert.Equal(10, dto.CustomerID);
        Assert.Equal(100, dto.LoanProductID);
        Assert.Equal(5000, dto.RequestedAmount);
        Assert.Equal(new DateTime(2024, 1, 1), dto.ApplicationDate);
        Assert.Equal("Pending", dto.ApplicationStatus);
        Assert.Null(dto.ApprovedAmount);
    }

    [Fact]
    public async Task GetLoanApplicationByIdAsync_ReturnsMappedDto_WhenExists()
    {
        // Arrange
        var application = new LoanApplication
        {
            ApplicationID = 2,
            CustomerID = 20,
            LoanProductID = 200,
            RequestedAmount = 10000,
            ApplicationDate = new DateTime(2024, 2, 2),
            ApplicationStatus = "Approved",
            ApprovedAmount = 9500
        };
        _repo.GetLoanApplicationByIdAsync(2).Returns(application);

        // Act
        var result = await _service.GetLoanApplicationByIdAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.ApplicationID);
        Assert.Equal(20, result.CustomerID);
        Assert.Equal(200, result.LoanProductID);
        Assert.Equal(10000, result.RequestedAmount);
        Assert.Equal(new DateTime(2024, 2, 2), result.ApplicationDate);
        Assert.Equal("Approved", result.ApplicationStatus);
        Assert.Equal(9500, result.ApprovedAmount);
    }

    [Fact]
    public async Task GetLoanApplicationByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange
        _repo.GetLoanApplicationByIdAsync(99).Returns((LoanApplication)null);

        // Act
        var result = await _service.GetLoanApplicationByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddLoanApplicationAsync_CallsRepositoryWithMappedEntity()
    {
        // Arrange
        var dto = new LoanApplicationDto
        {
            CustomerID = 30,
            LoanProductID = 300,
            RequestedAmount = 15000
        };

        LoanApplication captured = null;
        _repo.When(x => x.AddLoanApplicationAsync(Arg.Any<LoanApplication>()))
            .Do(call => captured = call.Arg<LoanApplication>());

        // Act
        await _service.AddLoanApplicationAsync(dto);

        // Assert
        Assert.NotNull(captured);
        Assert.Equal(30, captured.CustomerID);
        Assert.Equal(300, captured.LoanProductID);
        Assert.Equal(15000, captured.RequestedAmount);
        Assert.Equal("Pending", captured.ApplicationStatus);
        Assert.True((DateTime.UtcNow - captured.ApplicationDate).TotalSeconds < 5);
    }
}
}