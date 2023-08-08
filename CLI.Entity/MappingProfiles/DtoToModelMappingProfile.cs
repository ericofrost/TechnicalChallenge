using AutoMapper;
using CLI.Entity.Dto;
using CLI.Entity.ExternalServices.Requests;

namespace CLI.Entity.MappingProfiles
{
    /// <summary>
    /// Dto to model mapping profile
    /// </summary>
    public class DtoToModelMappingProfile : Profile
    {
        public DtoToModelMappingProfile()
        {
            this.CreateMap<CreateCustomerDto, CreateCustomerRequest>()
            .ForMember(dest => dest.TenantId, opt => opt.Ignore())
            .ReverseMap();

            this.CreateMap<AddProducToWishListDto, AddProductToWishListRequest>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
            .ReverseMap();

        }
    }
}
