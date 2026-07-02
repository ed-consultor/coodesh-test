using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
    public class UpdateOrderProfile : Profile
    {
        public UpdateOrderProfile()
        {
            CreateMap<UpdateOrderItem, OrderItem>();

            CreateMap<UpdateOrderCommand, Order>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Order, UpdateOrderResult>();
        }
    }
}
