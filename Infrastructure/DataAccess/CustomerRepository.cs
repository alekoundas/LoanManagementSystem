using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.Data.SqlClient;

namespace Infrastructure.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM CoreLoan.Customers", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(MapCustomer(reader));
                    }
                }
            }
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM CoreLoan.Customers WHERE CustomerID = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapCustomer(reader);
                    }
                    return null;
                }
            }
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO CoreLoan.Customers (FirstName, LastName, Email, CreditScore, RegistrationDate) " +
                    "VALUES (@FirstName, @LastName, @Email, @CreditScore, @RegistrationDate); " +
                    "SELECT SCOPE_IDENTITY()", connection);
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@CreditScore", (object?)customer.CreditScore ?? DBNull.Value);
                command.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);
                customer.CustomerID = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        private Customer MapCustomer(SqlDataReader reader)
        {
            return new Customer
            {
                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? "" : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                City = reader.IsDBNull(reader.GetOrdinal("City")) ? "" : reader.GetString(reader.GetOrdinal("City")),
                State = reader.IsDBNull(reader.GetOrdinal("State")) ? "" : reader.GetString(reader.GetOrdinal("State")),
                ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? "" : reader.GetString(reader.GetOrdinal("ZipCode")),
                CreditScore = reader.IsDBNull(reader.GetOrdinal("CreditScore")) ? null : reader.GetInt32(reader.GetOrdinal("CreditScore")),
                RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
            };
        }
    }
}
