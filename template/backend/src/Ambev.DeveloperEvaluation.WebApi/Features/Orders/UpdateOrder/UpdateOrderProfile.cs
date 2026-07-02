using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder  
{
    /// <summary>
    /// Initializes the mappings for UpdateOrder feature
    /// </summary>
    public class UpdateOrderProfile : Profile
    {
        public UpdateOrderProfile()
        {
            CreateMap<UpdateOrderItemRequest, UpdateOrderItem>();
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<UpdateOrderResult, UpdateOrderResponse>();
        }
    }
}
