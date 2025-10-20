
using AutoMapper;
using CustomerManagementSystem.Domain;


namespace CustomerManagementSystem.Business { 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDto,Customer>().ReverseMap(); 
        }
    }
}
