

using CsApplication.DataAccess;
using CsApplication.Domain;

namespace CsApplication.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCustomer(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
        }

        public List<Customer> GetAllCustomers() => _unitOfWork.Customers.GetAll().ToList();

        public Customer GetCustomerById(int id) => _unitOfWork.Customers.GetById(id);

        public void UpdateCustomer(Customer customer)
        {
            var existing = _unitOfWork.Customers.GetById(customer.Id);
            if (existing == null) throw new ArgumentException("Müşteri bulunamadı.");

            existing.Name = customer.Name;
            existing.Email = customer.Email;

            _unitOfWork.Customers.Update(existing);
            _unitOfWork.Complete();
        }

        public void DeleteCustomer(int id)
        {
            var existing = _unitOfWork.Customers.GetById(id);
            if (existing != null)
            {
                _unitOfWork.Customers.Delete(existing);
                _unitOfWork.Complete();
            }
        }
    }

}
