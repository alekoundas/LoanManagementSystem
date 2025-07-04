
using Core.Entities;
using Core.Services;
using Moq;
using Xunit;
using Core.Interfaces.Services;

namespace Test
{

    public class LoanServiceTests
    {
        [Fact]
        public async Task GetAllLoansAsync_ReturnsLoanDtos()
        {
            // Arrange
            var mockLoanRepo = new Mock<ILoanRepository>();
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockLoanRepo.Setup(repo => repo.GetAllLoansAsync())
                .ReturnsAsync(new List<Loan> { new Loan { LoanID = 1, CustomerID = 1, ApprovedAmount = 1000m, LoanStatus = "Active" } });
            var service = new LoanService(mockLoanRepo.Object, mockHttpClientFactory.Object);

            // Act
            var result = await service.GetAllLoansAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(1000m, result.First().ApprovedAmount);
        }
    }
}