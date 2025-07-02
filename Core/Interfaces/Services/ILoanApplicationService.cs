using Core.DTOs;

namespace Core.Interfaces.Services
{
    public interface ILoanApplicationService
    {
        Task<IEnumerable<LoanApplicationDto>> GetAllLoanApplicationsAsync();
        Task<LoanApplicationDto> GetLoanApplicationByIdAsync(int id);
        Task AddLoanApplicationAsync(LoanApplicationDto loanApplicationDto);
    }
}
