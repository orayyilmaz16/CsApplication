using CsApplication.DataAccess;
using CsApplication.Domain;


namespace CsApplication.Business
{
    public class CustomerManager
    {
        private readonly CustomerRepository _repository = new();

        public void AddCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new Exception("Müşteri adı boş olamaz!");

            _repository.Add(customer);
        }

        public List<Customer> GetCustomers() => _repository.GetAll();
    }
}
