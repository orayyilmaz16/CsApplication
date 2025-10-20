
using CsApplication.Domain;
using FluentValidation;

namespace CsApplication.Business.Validations
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Müşteri adı boş olamaz")
                .MinimumLength(3).WithMessage("Müşteri adı en az 3 karakter olmalı");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir müşteri ID giriniz.");


        }
    }
}
