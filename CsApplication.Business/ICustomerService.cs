using CsApplication.Domain;
using System.Collections.Generic;

namespace CsApplication.Business
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerDto dto);
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(CustomerDto dto);
        Task DeleteCustomerAsync(int id);

    }
}
