
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Services;
using Moq;
using Xunit;

namespace Test
{

    public class PaymentServiceTests
    {
        [Fact]
        public async Task GetPaymentsByLoanIdAsync_ReturnsPaymentDtos()
        {
            // Arrange
            var mockRepo = new Mock<IPaymentRepository>();
            mockRepo.Setup(repo => repo.GetPaymentsByLoanIdAsync(1))
                .ReturnsAsync(new List<Payment> { new Payment { PaymentID = 1, LoanID = 1, PaymentAmount = 100m, PaymentType = "Regular" } });
            var service = new PaymentService(mockRepo.Object);

            // Act
            var result = await service.GetPaymentsByLoanIdAsync(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(100m, result.First().PaymentAmount);
        }
    }
}