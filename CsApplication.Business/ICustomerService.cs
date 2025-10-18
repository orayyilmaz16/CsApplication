using CsApplication.Domain;

namespace CsApplication.Business
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        void DeleteCustomer(int id);
        Customer GetCustomerById(int id);
    }
}
