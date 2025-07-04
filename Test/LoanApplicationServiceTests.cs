
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Services;
using Moq;
using Xunit;

namespace Test
{

    public class LoanApplicationServiceTests
    {
        [Fact]
        public async Task GetAllLoanApplicationsAsync_ReturnsLoanApplicationDtos()
        {
            // Arrange
            var mockRepo = new Mock<ILoanApplicationRepository>();
            mockRepo.Setup(repo => repo.GetAllLoanApplicationsAsync())
                .ReturnsAsync(new List<LoanApplication> { new LoanApplication { ApplicationID = 1, CustomerID = 1, RequestedAmount = 5000m, ApplicationStatus = "Pending" } });
            var service = new LoanApplicationService(mockRepo.Object);

            // Act
            var result = await service.GetAllLoanApplicationsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(5000m, result.First().RequestedAmount);
        }
    }
}