using Core.DTOs;

namespace Core.Interfaces.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<LoanDto> GetLoanByIdAsync(int id);
        Task<decimal> GetLoanBalanceAsync(int id);
        Task<decimal> GetCreditScoreAsync(int customerId);
    }
}
