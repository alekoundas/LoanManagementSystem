using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess
{
    public class LoanProductRepository : ILoanProductRepository
    {
        private readonly string _connectionString;

        public LoanProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync()
        {
            var loanProducts = new List<LoanProduct>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Reference.LoanProducts", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        loanProducts.Add(MapLoanProduct(reader));
                    }
                }
            }
            return loanProducts;
        }

        public async Task<LoanProduct> GetLoanProductByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Reference.LoanProducts WHERE LoanProductID = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapLoanProduct(reader);
                    }
                    return null;
                }
            }
        }

        private LoanProduct MapLoanProduct(SqlDataReader reader)
        {
            return new LoanProduct
            {
                LoanProductID = reader.GetInt32(reader.GetOrdinal("LoanProductID")),
                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                InterestRate = reader.GetDecimal(reader.GetOrdinal("InterestRate")),
                MaxLoanAmount = reader.GetDecimal(reader.GetOrdinal("MaxLoanAmount")),
                MinCreditScore = reader.GetInt32(reader.GetOrdinal("MinCreditScore")),
                MinTermMonths = reader.GetInt32(reader.GetOrdinal("MinTermMonths")),
                MaxTermMonths = reader.GetInt32(reader.GetOrdinal("MaxTermMonths")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}
