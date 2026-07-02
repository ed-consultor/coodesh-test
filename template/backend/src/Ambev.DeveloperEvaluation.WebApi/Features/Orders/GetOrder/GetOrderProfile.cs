using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetOrder;
using AutoMapper;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder;

public class GetOrderProfile : Profile
{
    public GetOrderProfile()
    {
        CreateMap<GetOrderRequest, GetOrderCommand>();

        CreateMap<OrderItemResult, OrderItemResponse>();
        CreateMap<GetOrderResult, GetOrderResponse>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
