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
    public class CustomerServiceTests
    {
        private readonly ICustomerRepository _repo;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _repo = Substitute.For<ICustomerRepository>();
            _service = new CustomerService(_repo);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsMappedDtos()
        {
            // Arrange
            var customers = new List<Customer>
        {
            new Customer
            {
                CustomerID = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                CreditScore = 700,
                RegistrationDate = new DateTime(2024, 1, 1)
            }
        };
            _repo.GetAllCustomersAsync().Returns(customers);

            // Act
            var result = await _service.GetAllCustomersAsync();

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal(1, dto.CustomerID);
            Assert.Equal("John Doe", dto.FullName);
            Assert.Equal("john@example.com", dto.Email);
            Assert.Equal(700, dto.CreditScore);
            Assert.Equal(new DateTime(2024, 1, 1), dto.RegistrationDate);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsMappedDto_WhenExists()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerID = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane@example.com",
                CreditScore = 800
            };
            _repo.GetCustomerByIdAsync(2).Returns(customer);

            // Act
            var result = await _service.GetCustomerByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.CustomerID);
            Assert.Equal("Jane Smith", result.FullName);
            Assert.Equal("jane@example.com", result.Email);
            Assert.Equal(800, result.CreditScore);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsNull_WhenNotExists()
        {
            // Arrange
            _repo.GetCustomerByIdAsync(99).Returns((Customer)null);

            // Act
            var result = await _service.GetCustomerByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddCustomerAsync_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var dto = new CustomerDto
            {
                FullName = "Alice Johnson",
                Email = "alice@example.com",
                CreditScore = 650
            };

            Customer captured = null;
            _repo.When(x => x.AddCustomerAsync(Arg.Any<Customer>()))
                .Do(call => captured = call.Arg<Customer>());

            // Act
            await _service.AddCustomerAsync(dto);

            // Assert
            Assert.NotNull(captured);
            Assert.Equal("Alice", captured.FirstName);
            Assert.Equal("Johnson", captured.LastName);
            Assert.Equal("alice@example.com", captured.Email);
            Assert.Equal(650, captured.CreditScore);
            Assert.True((DateTime.UtcNow - captured.RegistrationDate).TotalSeconds < 5);
        }
    }
}