using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Profile for mapping between Order entity and GetOrderResponse
/// </summary>
public class GetOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetOrder operation
    /// </summary>
    public GetOrderProfile()
    {
        CreateMap<OrderItem, OrderItemResult>();
        CreateMap<Order, GetOrderResult>();
    }
}
