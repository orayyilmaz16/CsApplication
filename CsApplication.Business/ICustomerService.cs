using CsApplication.Domain;

namespace CsApplication.Business
{
    public interface ICustomerService
    {
        void AddCustomer(CustomerDto dto);
        void UpdateCustomer(CustomerDto dto);
        List<CustomerDto> GetAllCustomers();
        void DeleteCustomer(int id);
        CustomerDto GetCustomerById(int id);
    }
}
