using AutoMapper;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
