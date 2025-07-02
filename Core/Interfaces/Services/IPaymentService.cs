using Core.DTOs;

namespace Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetPaymentsByLoanIdAsync(int loanId);
        Task AddPaymentAsync(PaymentDto paymentDto);
    }
}
