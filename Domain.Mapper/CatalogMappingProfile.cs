using AutoMapper;
using Data.Domain;
using Domain.Dto.CatalogService;

namespace Domain.Mapper
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CreateBrandDto, Brand>().ReverseMap();
        }
    }
}