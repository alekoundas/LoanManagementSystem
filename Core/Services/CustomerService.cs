using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => new CustomerDto
            {
                CustomerID = c.CustomerID,
                FullName = $"{c.FirstName} {c.LastName}",
                Email = c.Email,
                CreditScore = c.CreditScore
            });
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null) return null;
            return new CustomerDto
            {
                CustomerID = customer.CustomerID,
                FullName = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email,
                CreditScore = customer.CreditScore
            };
        }

        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                FirstName = customerDto.FullName.Split(' ')[0],
                LastName = customerDto.FullName.Split(' ')[1],
                Email = customerDto.Email,
                CreditScore = customerDto.CreditScore,
                RegistrationDate = DateTime.UtcNow
            };
            await _customerRepository.AddCustomerAsync(customer);
        }
    }
}
