using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    /// <summary>
    /// Initializes the mappings for CreateOrder feature
    /// </summary>
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<CreateOrderItemRequest, CreateOrderItem>();
            CreateMap<CreateOrderRequest, CreateOrderCommand>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CreateOrderResult, CreateOrderResponse>();
        }
    }
}
