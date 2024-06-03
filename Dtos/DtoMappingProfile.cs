using AutoMapper;

namespace ProjectWorkApi;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Order, OrderDto>();

        CreateMap<OrderDetailDto, OrderDetail>();
        CreateMap<OrderDetail, OrderDetailDto>();
    }
}
