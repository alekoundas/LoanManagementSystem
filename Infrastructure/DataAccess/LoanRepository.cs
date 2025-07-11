using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Infrastructure.DataAccess
{
    public class LoanRepository : ILoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            var loans = new List<Loan>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM CoreLoan.Loans", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        loans.Add(MapLoan(reader));
                    }
                }
            }
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetLoanByCustomerIdAsync(int id)
        {
            var loans = new List<Loan>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM CoreLoan.Loans", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        loans.Add(MapLoan(reader));
                    }
                }
            }
            return loans;
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM CoreLoan.Loans WHERE LoanID = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapLoan(reader);
                    }
                    return null;
                }
            }
        }

        public async Task<decimal> GetLoanBalanceAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT CurrentBalance FROM CoreLoan.Loans WHERE LoanID = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var balance = await command.ExecuteScalarAsync();
                return balance != null ? Convert.ToDecimal(balance) : 0;
            }
        }

        private Loan MapLoan(SqlDataReader reader)
        {
            return new Loan
            {
                LoanID = reader.GetInt32(reader.GetOrdinal("LoanID")),
                ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                LoanProductID = reader.GetInt32(reader.GetOrdinal("LoanProductID")),
                ApprovedAmount = reader.GetDecimal(reader.GetOrdinal("ApprovedAmount")),
                DisbursementDate = reader.GetDateTime(reader.GetOrdinal("DisbursementDate")),
                MaturityDate = reader.GetDateTime(reader.GetOrdinal("MaturityDate")),
                InterestRate = reader.GetDecimal(reader.GetOrdinal("InterestRate")),
                CurrentBalance = reader.GetDecimal(reader.GetOrdinal("CurrentBalance")),
                OriginalTermMonths = reader.GetInt32(reader.GetOrdinal("OriginalTermMonths")),
                LoanStatus = reader.GetString(reader.GetOrdinal("LoanStatus")),
                LastPaymentDate = reader.IsDBNull(reader.GetOrdinal("LastPaymentDate")) ? null : reader.GetDateTime(reader.GetOrdinal("LastPaymentDate")),
                NextPaymentDueDate = reader.IsDBNull(reader.GetOrdinal("NextPaymentDueDate")) ? null : reader.GetDateTime(reader.GetOrdinal("NextPaymentDueDate"))
            };
        }
    }
}
