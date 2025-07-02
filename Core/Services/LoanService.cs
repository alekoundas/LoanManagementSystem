using Core.DTOs;
using Core.Interfaces.Services;
using System.Net.Http.Json;

namespace Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoanService(ILoanRepository loanRepository, IHttpClientFactory httpClientFactory)
        {
            _loanRepository = loanRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _loanRepository.GetAllLoansAsync();
            return loans.Select(l => new LoanDto
            {
                LoanID = l.LoanID,
                CustomerID = l.CustomerID,
                LoanProductID = l.LoanProductID,
                ApprovedAmount = l.ApprovedAmount,
                DisbursementDate = l.DisbursementDate,
                MaturityDate = l.MaturityDate,
                InterestRate = l.InterestRate,
                CurrentBalance = l.CurrentBalance,
                LoanStatus = l.LoanStatus
            });
        }

        public async Task<LoanDto> GetLoanByIdAsync(int id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);
            if (loan == null) return null;
            return new LoanDto
            {
                LoanID = loan.LoanID,
                CustomerID = loan.CustomerID,
                LoanProductID = loan.LoanProductID,
                ApprovedAmount = loan.ApprovedAmount,
                DisbursementDate = loan.DisbursementDate,
                MaturityDate = loan.MaturityDate,
                InterestRate = loan.InterestRate,
                CurrentBalance = loan.CurrentBalance,
                LoanStatus = loan.LoanStatus
            };
        }

        public async Task<decimal> GetLoanBalanceAsync(int id)
        {
            return await _loanRepository.GetLoanBalanceAsync(id);
        }

        public async Task<decimal> GetCreditScoreAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient("CreditScoreService");
            var response = await client.PostAsync("/api/creditscore",
                new StringContent(System.Text.Json.JsonSerializer.Serialize(new { CustomerId = customerId }),
                System.Text.Encoding.UTF8, "application/json"));
            //response.MinorSuccessStatusCode(); TODO enable
            return await response.Content.ReadFromJsonAsync<decimal>();
        }
    }
}
