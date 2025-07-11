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
    public class LoanProductServiceTests
    {
        private readonly ILoanProductRepository _repo;
        private readonly LoanProductService _service;

        public LoanProductServiceTests()
        {
            _repo = Substitute.For<ILoanProductRepository>();
            _service = new LoanProductService(_repo);
        }

        [Fact]
        public async Task GetAllLoanProductsAsync_ReturnsMappedDtos()
        {
            // Arrange
            var products = new List<LoanProduct>
        {
            new LoanProduct
            {
                LoanProductID = 1,
                ProductName = "Home Loan",
                InterestRate = 3.5m,
                MaxLoanAmount = 200000m,
                MinCreditScore = 650,
                IsActive = true
            }
        };
            _repo.GetAllLoanProductsAsync().Returns(products);

            // Act
            var result = await _service.GetAllLoanProductsAsync();

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal(1, dto.LoanProductID);
            Assert.Equal("Home Loan", dto.ProductName);
            Assert.Equal(3.5m, dto.InterestRate);
            Assert.Equal(200000m, dto.MaxLoanAmount);
            Assert.Equal(650, dto.MinCreditScore);
            Assert.True(dto.IsActive);
        }

        [Fact]
        public async Task GetLoanProductByIdAsync_ReturnsMappedDto_WhenExists()
        {
            // Arrange
            var product = new LoanProduct
            {
                LoanProductID = 2,
                ProductName = "Car Loan",
                InterestRate = 4.2m,
                MaxLoanAmount = 50000m,
                MinCreditScore = 600,
                IsActive = false
            };
            _repo.GetLoanProductByIdAsync(2).Returns(product);

            // Act
            var result = await _service.GetLoanProductByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.LoanProductID);
            Assert.Equal("Car Loan", result.ProductName);
            Assert.Equal(4.2m, result.InterestRate);
            Assert.Equal(50000m, result.MaxLoanAmount);
            Assert.Equal(600, result.MinCreditScore);
            Assert.False(result.IsActive);
        }

        [Fact]
        public async Task GetLoanProductByIdAsync_ReturnsNull_WhenNotExists()
        {
            // Arrange
            _repo.GetLoanProductByIdAsync(99).Returns((LoanProduct)null);

            // Act
            var result = await _service.GetLoanProductByIdAsync(99);

            // Assert
            Assert.Null(result);
        }
    }
}