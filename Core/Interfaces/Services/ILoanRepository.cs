using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan> GetLoanByIdAsync(int id);
        Task<decimal> GetLoanBalanceAsync(int id);
    }
}
