
using CustomerManagementSystem.Domain;

namespace CustomerManagementSystem.Business
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerDto dto);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(CustomerDto dto);
        Task DeleteCustomerAsync(int id);

    }
}
