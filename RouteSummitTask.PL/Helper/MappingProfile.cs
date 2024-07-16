using AutoMapper;

namespace RouteSummitTask.PL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (Status)src.Status));

            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
