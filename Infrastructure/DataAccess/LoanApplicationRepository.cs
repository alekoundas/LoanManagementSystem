using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess
{
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly string _connectionString;

        public LoanApplicationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync()
        {
            var loanApplications = new List<LoanApplication>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM LoanApp.LoanApplications", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        loanApplications.Add(MapLoanApplication(reader));
                    }
                }
            }
            return loanApplications;
        }

        public async Task<LoanApplication> GetLoanApplicationByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM LoanApp.LoanApplications WHERE ApplicationID = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapLoanApplication(reader);
                    }
                    return null;
                }
            }
        }

        public async Task AddLoanApplicationAsync(LoanApplication loanApplication)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO LoanApp.LoanApplications (CustomerID, LoanProductID, RequestedAmount, ApplicationDate, ApplicationStatus) " +
                    "VALUES (@CustomerID, @LoanProductID, @RequestedAmount, @ApplicationDate, @ApplicationStatus); " +
                    "SELECT SCOPE_IDENTITY()", connection);
                command.Parameters.AddWithValue("@CustomerID", loanApplication.CustomerID);
                command.Parameters.AddWithValue("@LoanProductID", loanApplication.LoanProductID);
                command.Parameters.AddWithValue("@RequestedAmount", loanApplication.RequestedAmount);
                command.Parameters.AddWithValue("@ApplicationDate", loanApplication.ApplicationDate);
                command.Parameters.AddWithValue("@ApplicationStatus", loanApplication.ApplicationStatus);
                loanApplication.ApplicationID = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        private LoanApplication MapLoanApplication(SqlDataReader reader)
        {
            return new LoanApplication
            {
                ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                LoanProductID = reader.GetInt32(reader.GetOrdinal("LoanProductID")),
                RequestedAmount = reader.GetDecimal(reader.GetOrdinal("RequestedAmount")),
                ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                ApplicationStatus = reader.GetString(reader.GetOrdinal("ApplicationStatus")),
                AssignedEmployeeID = reader.IsDBNull(reader.GetOrdinal("AssignedEmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("AssignedEmployeeID")),
                DecisionDate = reader.IsDBNull(reader.GetOrdinal("DecisionDate")) ? null : reader.GetDateTime(reader.GetOrdinal("DecisionDate")),
                ApprovedAmount = reader.IsDBNull(reader.GetOrdinal("ApprovedAmount")) ? null : reader.GetDecimal(reader.GetOrdinal("ApprovedAmount")),
                RejectionReason = reader.IsDBNull(reader.GetOrdinal("RejectionReason")) ? "" : reader.GetString(reader.GetOrdinal("RejectionReason"))
            };
        }
    }
}
