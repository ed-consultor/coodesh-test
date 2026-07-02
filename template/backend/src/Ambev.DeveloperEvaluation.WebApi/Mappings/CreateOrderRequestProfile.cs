using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class CreateOrderRequestProfile : Profile
    {
        public CreateOrderRequestProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
        }
    }
}
