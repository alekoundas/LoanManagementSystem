using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsByLoanIdAsync(int loanId);
        Task AddPaymentAsync(Payment payment);
    }
}
