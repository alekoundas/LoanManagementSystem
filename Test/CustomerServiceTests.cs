using Core.Interfaces.Repository;
using Core.Entities;
using Core.Services;
using Moq;
using Xunit;

namespace Test
{

    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetAllCustomersAsync_ReturnsCustomerDtos()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetAllCustomersAsync())
                .ReturnsAsync(new List<Customer> { new Customer { CustomerID = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" } });
            var service = new CustomerService(mockRepo.Object);

            // Act
            var result = await service.GetAllCustomersAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result.First().FullName);
        }
    }
}