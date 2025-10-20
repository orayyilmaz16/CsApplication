
using AutoMapper;
using CsApplication.Domain;

namespace CsApplication.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDto,Customer>().ReverseMap(); 
        }
    }
}
