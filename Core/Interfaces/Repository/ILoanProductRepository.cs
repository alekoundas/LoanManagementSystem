using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface ILoanProductRepository
    {
        Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync();
        Task<LoanProduct> GetLoanProductByIdAsync(int id);
    }
}
