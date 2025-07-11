using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Services;
using NSubstitute;
using Xunit;

namespace Test
{
    public class LoanServiceTests
    {
        private readonly ILoanRepository _repo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LoanService _service;

        public LoanServiceTests()
        {
            _repo = Substitute.For<ILoanRepository>();
            _httpClientFactory = Substitute.For<IHttpClientFactory>();
            _service = new LoanService(_repo, _httpClientFactory);
        }

        [Fact]
        public async Task GetAllLoansAsync_ReturnsMappedDtos()
        {
            // Arrange
            var loans = new List<Loan>
        {
            new Loan
            {
                LoanID = 1,
                CustomerID = 10,
                LoanProductID = 100,
                ApprovedAmount = 5000,
                DisbursementDate = new DateTime(2024, 1, 1),
                MaturityDate = new DateTime(2025, 1, 1),
                InterestRate = 3.5m,
                CurrentBalance = 4000,
                LoanStatus = "Active"
            }
        };
            _repo.GetAllLoansAsync().Returns(loans);

            // Act
            var result = await _service.GetAllLoansAsync();

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal(1, dto.LoanID);
            Assert.Equal(10, dto.CustomerID);
            Assert.Equal(100, dto.LoanProductID);
            Assert.Equal(5000, dto.ApprovedAmount);
            Assert.Equal(new DateTime(2024, 1, 1), dto.DisbursementDate);
            Assert.Equal(new DateTime(2025, 1, 1), dto.MaturityDate);
            Assert.Equal(3.5m, dto.InterestRate);
            Assert.Equal(4000, dto.CurrentBalance);
            Assert.Equal("Active", dto.LoanStatus);
        }

        [Fact]
        public async Task GetLoanByIdAsync_ReturnsMappedDto_WhenExists()
        {
            // Arrange
            var loan = new Loan
            {
                LoanID = 2,
                CustomerID = 20,
                LoanProductID = 200,
                ApprovedAmount = 10000,
                DisbursementDate = new DateTime(2024, 2, 2),
                MaturityDate = new DateTime(2026, 2, 2),
                InterestRate = 4.2m,
                CurrentBalance = 9500,
                LoanStatus = "Closed"
            };
            _repo.GetLoanByIdAsync(2).Returns(loan);

            // Act
            var result = await _service.GetLoanByIdAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.LoanID);
            Assert.Equal(20, result.CustomerID);
            Assert.Equal(200, result.LoanProductID);
            Assert.Equal(10000, result.ApprovedAmount);
            Assert.Equal(new DateTime(2024, 2, 2), result.DisbursementDate);
            Assert.Equal(new DateTime(2026, 2, 2), result.MaturityDate);
            Assert.Equal(4.2m, result.InterestRate);
            Assert.Equal(9500, result.CurrentBalance);
            Assert.Equal("Closed", result.LoanStatus);
        }

        [Fact]
        public async Task GetLoanByIdAsync_ReturnsNull_WhenNotExists()
        {
            // Arrange
            _repo.GetLoanByIdAsync(99).Returns((Loan)null);

            // Act
            var result = await _service.GetLoanByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetLoanByCustomerIdAsync_ReturnsMappedDtos_WhenExists()
        {
            // Arrange
            var loans = new List<Loan>
        {
            new Loan
            {
                LoanID = 3,
                CustomerID = 30,
                LoanProductID = 300,
                ApprovedAmount = 15000,
                DisbursementDate = new DateTime(2024, 3, 3),
                MaturityDate = new DateTime(2027, 3, 3),
                InterestRate = 5.1m,
                CurrentBalance = 14000,
                LoanStatus = "Active"
            }
        };
            _repo.GetLoanByCustomerIdAsync(30).Returns(loans);

            // Act
            var result = await _service.GetLoanByCustomerIdAsync(30);

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal(3, dto.LoanID);
            Assert.Equal(30, dto.CustomerID);
            Assert.Equal(300, dto.LoanProductID);
            Assert.Equal(15000, dto.ApprovedAmount);
            Assert.Equal(new DateTime(2024, 3, 3), dto.DisbursementDate);
            Assert.Equal(new DateTime(2027, 3, 3), dto.MaturityDate);
            Assert.Equal(5.1m, dto.InterestRate);
            Assert.Equal(14000, dto.CurrentBalance);
            Assert.Equal("Active", dto.LoanStatus);
        }

        [Fact]
        public async Task GetLoanByCustomerIdAsync_ReturnsNull_WhenNotExists()
        {
            // Arrange
            _repo.GetLoanByCustomerIdAsync(99).Returns((IEnumerable<Loan>)null);

            // Act
            var result = await _service.GetLoanByCustomerIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetLoanBalanceAsync_ReturnsBalance()
        {
            // Arrange
            _repo.GetLoanBalanceAsync(1).Returns(1234.56m);

            // Act
            var result = await _service.GetLoanBalanceAsync(1);

            // Assert
            Assert.Equal(1234.56m, result);
        }

    }
}