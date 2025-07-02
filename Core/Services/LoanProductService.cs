using Core.DTOs;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class LoanProductService : ILoanProductService
    {
        private readonly ILoanProductRepository _loanProductRepository;

        public LoanProductService(ILoanProductRepository loanProductRepository)
        {
            _loanProductRepository = loanProductRepository;
        }

        public async Task<IEnumerable<LoanProductDto>> GetAllLoanProductsAsync()
        {
            var loanProducts = await _loanProductRepository.GetAllLoanProductsAsync();
            return loanProducts.Select(lp => new LoanProductDto
            {
                LoanProductID = lp.LoanProductID,
                ProductName = lp.ProductName,
                InterestRate = lp.InterestRate,
                MaxLoanAmount = lp.MaxLoanAmount,
                MinCreditScore = lp.MinCreditScore,
                IsActive = lp.IsActive
            });
        }

        public async Task<LoanProductDto> GetLoanProductByIdAsync(int id)
        {
            var loanProduct = await _loanProductRepository.GetLoanProductByIdAsync(id);
            if (loanProduct == null) return null;
            return new LoanProductDto
            {
                LoanProductID = loanProduct.LoanProductID,
                ProductName = loanProduct.ProductName,
                InterestRate = loanProduct.InterestRate,
                MaxLoanAmount = loanProduct.MaxLoanAmount,
                MinCreditScore = loanProduct.MinCreditScore,
                IsActive = loanProduct.IsActive
            };
        }
    }
}
