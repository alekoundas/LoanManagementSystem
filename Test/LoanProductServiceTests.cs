
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Services;
using Moq;
using Xunit;

namespace Test
{

    public class LoanProductServiceTests
    {
        [Fact]
        public async Task GetAllLoanProductsAsync_ReturnsLoanProductDtos()
        {
            // Arrange
            var mockRepo = new Mock<ILoanProductRepository>();
            mockRepo.Setup(repo => repo.GetAllLoanProductsAsync())
                .ReturnsAsync(new List<LoanProduct> { new LoanProduct { LoanProductID = 1, ProductName = "Personal Loan", InterestRate = 5.5m, IsActive = true } });
            var service = new LoanProductService(mockRepo.Object);

            // Act
            var result = await service.GetAllLoanProductsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Personal Loan", result.First().ProductName);
        }
    }
}