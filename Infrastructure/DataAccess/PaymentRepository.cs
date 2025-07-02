using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Infrastructure.DataAccess
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByLoanIdAsync(int loanId)
        {
            var payments = new List<Payment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM CoreLoan.Payments WHERE LoanID = @LoanID", connection);
                command.Parameters.AddWithValue("@LoanID", loanId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        payments.Add(MapPayment(reader));
                    }
                }
            }
            return payments;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO CoreLoan.Payments (LoanID, PaymentDate, PaymentAmount, PaymentType) " +
                    "VALUES (@LoanID, @PaymentDate, @PaymentAmount, @PaymentType); " +
                    "SELECT SCOPE_IDENTITY()", connection);
                command.Parameters.AddWithValue("@LoanID", payment.LoanID);
                command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                command.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
                command.Parameters.AddWithValue("@PaymentType", payment.PaymentType);
                payment.PaymentID = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        private Payment MapPayment(SqlDataReader reader)
        {
            return new Payment
            {
                PaymentID = reader.GetInt32(reader.GetOrdinal("PaymentID")),
                LoanID = reader.GetInt32(reader.GetOrdinal("LoanID")),
                PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                PaymentAmount = reader.GetDecimal(reader.GetOrdinal("PaymentAmount")),
                PaymentType = reader.GetString(reader.GetOrdinal("PaymentType")),
                RecordedByEmployeeID = reader.IsDBNull(reader.GetOrdinal("RecordedByEmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("RecordedByEmployeeID"))
            };
        }
    }
}
