
using AutoMapper;
using CsApplication.DataAccess;
using CsApplication.Domain;
using FluentValidation;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace CsApplication.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _validator;
        private readonly ILogger<CustomerService> _logger;
       

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper,IValidator<CustomerDto> validator,ILogger<CustomerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
       
        }

        public async Task AddCustomerAsync(CustomerDto dto)
        {
            _logger.LogInformation("AddCustomerAsync çağrıldı. DTO: {@Dto}", dto);

            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    _logger.LogWarning("Validasyon hatası: {ErrorMessage}", error.ErrorMessage);
                return;
            }

            try
            {
                var entity = _mapper.Map<Customer>(dto);
                await _unitOfWork.Customers.AddAsync(entity);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Müşteri başarıyla eklendi. Id: {CustomerId}", entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            _logger.LogInformation("GetAllCustomersAsync çağrıldı.");

            var entities = await _unitOfWork.Customers.GetAllAsync();

            // IEnumerable<CustomerDto> olarak map et
            return _mapper.Map<IEnumerable<CustomerDto>>(entities);
        }


        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            _logger.LogInformation("GetCustomerByIdAsync çağrıldı. Id: {Id}", id);

            var entity = await _unitOfWork.Customers.GetByIdAsync(id);
            return _mapper.Map<CustomerDto?>(entity);
        }

        public async Task UpdateCustomerAsync(CustomerDto dto)
        {
            _logger.LogInformation("UpdateCustomerAsync çağrıldı. DTO: {@Dto}", dto);

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _logger.LogWarning("Validasyon hatası: {ErrorMessage}", error.ErrorMessage);
                return;
            }

            var existing = await _unitOfWork.Customers.GetByIdAsync(dto.Id);
            if (existing == null)
            {
                _logger.LogWarning("Müşteri bulunamadı. Id: {Id}", dto.Id);
                throw new ArgumentException("Müşteri bulunamadı.");
            }

            try
            {
                _mapper.Map(dto, existing);
                await _unitOfWork.Customers.UpdateAsync(existing);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Müşteri güncellendi. Id: {Id}", dto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            _logger.LogInformation("DeleteCustomerAsync çağrıldı. Id: {Id}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Geçersiz müşteri ID: {Id}", id);
                throw new ArgumentException("Geçersiz müşteri ID");
            }

            var existing = await _unitOfWork.Customers.GetByIdAsync(id);
            if (existing != null)
            {
                try
                {
                   await _unitOfWork.Customers.DeleteAsync(existing);
                    await _unitOfWork.CompleteAsync();

                    _logger.LogInformation("Müşteri silindi. Id: {Id}", id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Müşteri silinirken hata oluştu. Id: {Id}", id);
                    throw;
                }
            }
            else
            {
                _logger.LogWarning("Silinmek istenen müşteri bulunamadı. Id: {Id}", id);
            }
        }

    }

}
