using AutoMapper;

namespace ProjectWorkApi;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
/*        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src=>src.Role.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src=>src.Username.ToLower()));*/

        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();

        CreateMap<OrderDetailDto, OrderDetail>();
        CreateMap<OrderDetail, OrderDetailDto>();
    }
}
