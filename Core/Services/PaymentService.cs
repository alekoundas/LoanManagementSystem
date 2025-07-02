using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByLoanIdAsync(int loanId)
        {
            var payments = await _paymentRepository.GetPaymentsByLoanIdAsync(loanId);
            return payments.Select(p => new PaymentDto
            {
                PaymentID = p.PaymentID,
                LoanID = p.LoanID,
                PaymentDate = p.PaymentDate,
                PaymentAmount = p.PaymentAmount,
                PaymentType = p.PaymentType
            });
        }

        public async Task AddPaymentAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                LoanID = paymentDto.LoanID,
                PaymentDate = DateTime.UtcNow,
                PaymentAmount = paymentDto.PaymentAmount,
                PaymentType = paymentDto.PaymentType
            };
            await _paymentRepository.AddPaymentAsync(payment);
        }
    }
}
