using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<CreateOrderItem, OrderItem>();

            CreateMap<CreateOrderCommand, Order>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Order, CreateOrderResult>();
        }
    }
}
