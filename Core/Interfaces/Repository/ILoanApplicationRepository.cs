using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface ILoanApplicationRepository
    {
        Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync();
        Task<LoanApplication> GetLoanApplicationByIdAsync(int id);
        Task AddLoanApplicationAsync(LoanApplication loanApplication);
    }
}
