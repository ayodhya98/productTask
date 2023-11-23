using AutoMapper;
using DAL.Model;
using BLL.Dto;

namespace API.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Customer,CustomerDto>().ReverseMap();

        }
    }
}
