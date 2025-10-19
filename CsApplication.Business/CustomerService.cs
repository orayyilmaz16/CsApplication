

using AutoMapper;
using CsApplication.DataAccess;
using CsApplication.Domain;

namespace CsApplication.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddCustomer(CustomerDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);
            _unitOfWork.Customers.Add(entity);
            _unitOfWork.Complete();
        }

        public List<Customer> GetAllCustomers()
        {
            var entities = _unitOfWork.Customers.GetAll();
            return _mapper.Map<List<Customer>>(entities);
        }

        public Customer GetCustomerById(int id)
        {
            var entity = _unitOfWork.Customers.GetById(id);
            return _mapper.Map<CustomerDto>(entity);
        }

        public void UpdateCustomer(CustomerDto dto)
        {
            var existing = _unitOfWork.Customers.GetById(dto.Id);
            if (existing == null) throw new ArgumentException("Müşteri bulunamadı.");

            _mapper.Map(dto, existing);

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
