using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public LoanApplicationService(ILoanApplicationRepository loanApplicationRepository)
        {
            _loanApplicationRepository = loanApplicationRepository;
        }

        public async Task<IEnumerable<LoanApplicationDto>> GetAllLoanApplicationsAsync()
        {
            var loanApplications = await _loanApplicationRepository.GetAllLoanApplicationsAsync();
            return loanApplications.Select(la => new LoanApplicationDto
            {
                ApplicationID = la.ApplicationID,
                CustomerID = la.CustomerID,
                LoanProductID = la.LoanProductID,
                RequestedAmount = la.RequestedAmount,
                ApplicationDate = la.ApplicationDate,
                ApplicationStatus = la.ApplicationStatus,
                ApprovedAmount = la.ApprovedAmount
            });
        }

        public async Task<LoanApplicationDto> GetLoanApplicationByIdAsync(int id)
        {
            var loanApplication = await _loanApplicationRepository.GetLoanApplicationByIdAsync(id);
            if (loanApplication == null) return null;
            return new LoanApplicationDto
            {
                ApplicationID = loanApplication.ApplicationID,
                CustomerID = loanApplication.CustomerID,
                LoanProductID = loanApplication.LoanProductID,
                RequestedAmount = loanApplication.RequestedAmount,
                ApplicationDate = loanApplication.ApplicationDate,
                ApplicationStatus = loanApplication.ApplicationStatus,
                ApprovedAmount = loanApplication.ApprovedAmount
            };
        }

        public async Task AddLoanApplicationAsync(LoanApplicationDto loanApplicationDto)
        {
            var loanApplication = new LoanApplication
            {
                CustomerID = loanApplicationDto.CustomerID,
                LoanProductID = loanApplicationDto.LoanProductID,
                RequestedAmount = loanApplicationDto.RequestedAmount,
                ApplicationDate = DateTime.UtcNow,
                ApplicationStatus = "Pending"
            };
            await _loanApplicationRepository.AddLoanApplicationAsync(loanApplication);
        }
    }
}
