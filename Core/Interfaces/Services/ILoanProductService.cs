using Core.DTOs;

namespace Core.Interfaces.Services
{
    public interface ILoanProductService
    {
        Task<IEnumerable<LoanProductDto>> GetAllLoanProductsAsync();
        Task<LoanProductDto> GetLoanProductByIdAsync(int id);
    }
}
