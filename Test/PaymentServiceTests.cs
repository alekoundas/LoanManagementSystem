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
    public class PaymentServiceTests
    {
        private readonly IPaymentRepository _repo;
        private readonly PaymentService _service;

        public PaymentServiceTests()
        {
            _repo = Substitute.For<IPaymentRepository>();
            _service = new PaymentService(_repo);
        }

        [Fact]
        public async Task GetPaymentsByLoanIdAsync_ReturnsMappedDtos()
        {
            // Arrange
            var payments = new List<Payment>
        {
            new Payment
            {
                PaymentID = 1,
                LoanID = 10,
                PaymentDate = new DateTime(2024, 1, 1),
                PaymentAmount = 500.00m,
                PaymentType = "Principal"
            }
        };
            _repo.GetPaymentsByLoanIdAsync(10).Returns(payments);

            // Act
            var result = await _service.GetPaymentsByLoanIdAsync(10);

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal(1, dto.PaymentID);
            Assert.Equal(10, dto.LoanID);
            Assert.Equal(new DateTime(2024, 1, 1), dto.PaymentDate);
            Assert.Equal(500.00m, dto.PaymentAmount);
            Assert.Equal("Principal", dto.PaymentType);
        }

        [Fact]
        public async Task GetPaymentsByLoanIdAsync_ReturnsEmpty_WhenNoPayments()
        {
            // Arrange
            _repo.GetPaymentsByLoanIdAsync(99).Returns(new List<Payment>());

            // Act
            var result = await _service.GetPaymentsByLoanIdAsync(99);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddPaymentAsync_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var dto = new PaymentDto
            {
                LoanID = 20,
                PaymentAmount = 1000.00m,
                PaymentType = "Interest"
            };

            Payment captured = null;
            _repo.When(x => x.AddPaymentAsync(Arg.Any<Payment>()))
                .Do(call => captured = call.Arg<Payment>());

            // Act
            await _service.AddPaymentAsync(dto);

            // Assert
            Assert.NotNull(captured);
            Assert.Equal(20, captured.LoanID);
            Assert.Equal(1000.00m, captured.PaymentAmount);
            Assert.Equal("Interest", captured.PaymentType);
            Assert.True((DateTime.UtcNow - captured.PaymentDate).TotalSeconds < 5);
        }
    }
}