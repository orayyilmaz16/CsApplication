
using AutoMapper;
using CsApplication.DataAccess;
using CsApplication.Domain;
using FluentValidation;

namespace CsApplication.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _validator;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper,IValidator<CustomerDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public void AddCustomer(CustomerDto dto)
        {
            var result = _validator.Validate(dto);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine($"Hata: {error.ErrorMessage}");
                return;
                
            }

            var entity = _mapper.Map<Customer>(dto);
            _unitOfWork.Customers.Add(entity);
            _unitOfWork.Complete();
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var entities = _unitOfWork.Customers.GetAll();
            return _mapper.Map<List<CustomerDto>>(entities);
        }

        public CustomerDto GetCustomerById(int id)
        {
            var entity = _unitOfWork.Customers.GetById(id);
            return _mapper.Map<CustomerDto>(entity);
        }

        public void UpdateCustomer(CustomerDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    Console.WriteLine($"Hata: {error.ErrorMessage}");
                return;
            }


            var existing = _unitOfWork.Customers.GetById(dto.Id);
            if (existing == null) throw new ArgumentException("Müşteri bulunamadı.");

            _mapper.Map(dto, existing);
            _unitOfWork.Customers.Update(existing);
            _unitOfWork.Complete();
        }

        public void DeleteCustomer(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Geçersiz müşteri ID");

            var existing = _unitOfWork.Customers.GetById(id);
            if (existing != null)
            {
                _unitOfWork.Customers.Delete(existing);
                _unitOfWork.Complete();
            }
        }
    }

}
